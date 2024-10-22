using Microsoft.Xna.Framework;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public enum ShipAI
    {
        /// <summary>
        /// 闲置行动
        /// </summary>
        Default,
        /// <summary>
        /// 列阵移动
        /// </summary>
        MoveInShape,
        /// <summary>
        /// 自由移动
        /// </summary>
        MoveFree,
        /// <summary>
        /// 发动攻击
        /// </summary>
        Attack,
        /// <summary>
        /// 失踪状态
        /// </summary>
        Missing,
        /// <summary>
        /// FTL行进
        /// </summary>
        FTLMove
    }
    public partial class ShipNPC : ModNPC
    {
        /// <summary>
        /// 舰船状态机
        /// </summary>
        public ShipAI shipAI = ShipAI.Default;

        public override void AI()
        {
            if (ShipGraph.ShipType == "")
            {
                NPC.active = false;
                return;
            }
            AddLight();

            if (HurtTimer > 0) HurtTimer--;
            if (ImmuneTime > 0 && NPC.immune[Main.myPlayer] == 0 && NPC.immune[255] == 0) ImmuneTime--;
            if (shipAI != ShipAI.FTLMove && shipAI != ShipAI.Missing) if (FTLCooldown > 0) FTLCooldown--;

            ResetShipStat();
            UpdateHullAndShieldRegen();
            ModifyScaleForShield();


            SelectTarget();
            if (shipAI == ShipAI.Default)
            {
                NPC.velocity *= 0.8f;
                NPC.rotation = SmoothRotation(NPC.rotation, FleetSystem.TipRotation, GetFinalSpeed(MaxSpeed) / 200f);

                if (CurrentTarget != -1 && !FleetSystem.Passive)
                {
                    shipAI = ShipAI.Attack;
                }
                else if (NPC.Distance(ShapePos) > 100)
                {
                    shipAI = ShipAI.MoveInShape;
                }
            }
            else if (shipAI == ShipAI.MoveFree)
            {
                float MoveVec = (ShapePos - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (ShapePos.Distance(NPC.Center) > 4000 && FTLLevel > 1 && FTLCooldown <= 0)
                {
                    float dist = 400 + Main.rand.Next(700);// + (5 - FTLLevel) * 400;
                    FTLMove(ShapePos + Vector2.Normalize(NPC.Center - ShapePos) * dist);
                }
                if (CalcDifference(NPC.rotation, MoveVec) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }

                if (CurrentTarget != -1 && !FleetSystem.Passive)
                {
                    shipAI = ShipAI.Attack;
                }
                else if (NPC.Distance(ShapePos) <= GetFinalSpeed(MaxSpeed))
                {
                    if (NPC.velocity.Length() > GetFinalSpeed(MaxSpeed))
                    {
                        NPC.velocity = Vector2.Normalize(NPC.velocity) * GetFinalSpeed(MaxSpeed);
                    }
                    if (NPC.Distance(ShapePos) <= 10)
                    {
                        shipAI = ShipAI.Default;
                    }
                }
            }
            else if (shipAI == ShipAI.MoveInShape)       //列队行进
            {
                float MoveVec = (ShapePos - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(FleetSystem.LowestSpeed) / 100f);
                if (ShapePos.Distance(NPC.Center) > 4000 && FTLLevel > 1 && FTLCooldown <= 0)
                {
                    float dist = 750;// + (5 - FTLLevel) * 400;
                    FTLMove(ShapePos + Vector2.Normalize(NPC.Center - ShapePos) * dist);
                }

                if (CalcDifference(NPC.rotation, MoveVec) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(FleetSystem.LowestSpeed) / 120f, 0, GetFinalSpeed(FleetSystem.LowestSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
                if (CurrentTarget != -1 && !FleetSystem.Passive)
                {
                    shipAI = ShipAI.Attack;
                }
                else if (NPC.Distance(ShapePos) <= GetFinalSpeed(FleetSystem.LowestSpeed))
                {
                    if (NPC.velocity.Length() > GetFinalSpeed(FleetSystem.LowestSpeed) / 3)
                    {
                        NPC.velocity = Vector2.Normalize(NPC.velocity) * GetFinalSpeed(FleetSystem.LowestSpeed) / 3;
                    }
                    if (NPC.Distance(ShapePos) <= 10)
                    {
                        shipAI = ShipAI.Default;
                    }
                }
            }
            else if (shipAI == ShipAI.FTLMove)
            {
                NPC.velocity *= 0.95f;
                float MoveVec = (FTLTargetPos - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                FTLTimer--;
                if (FTLTimer == 15)
                {
                    SomeUtils.PlaySound(SoundPath.Other + "FTL", FTLTargetPos);
                }
                if (FTLTimer <= 0)
                {
                    NPC.Center = FTLTargetPos;
                    for (int i = 0; i < NPC.oldPos.Length; i++)
                    {
                        NPC.oldPos[i] = NPC.position;
                        NPC.oldRot[i] = NPC.rotation;
                    }

                    FTLCooldown = FTLMaxCooldown;
                    FTLTimer = 0;
                    shipAI = ShipAI.MoveFree;
                }
            }
            else if (shipAI == ShipAI.Attack)
            {
                if (FleetSystem.Passive && SomeUtils.AnyBosses())//紧急脱离
                {
                    shipAI = ShipAI.Missing;
                    MissingTimer = CalcMissingTime();
                    NPC.dontTakeDamage = true;
                    NPC.dontTakeDamageFromHostiles = true;
                    float scale = ShipLength / 70f;
                    FTLLight2.Summon(NPC.GetSource_FromAI(), NPC.Center, scale);
                    return;
                }
                else if (CurrentTarget == -1)
                {
                    shipAI = ShipAI.MoveFree;
                }
                else
                {
                    if (Main.npc[CurrentTarget].Distance(NPC.Center) > 4000 && FTLLevel > 1 && FTLCooldown <= 0)
                    {
                        float dist = 400 + Main.rand.Next(700);// + (5 - FTLLevel) * 400;
                        FTLMove(Main.npc[CurrentTarget].Center + Vector2.Normalize(NPC.Center - Main.npc[CurrentTarget].Center) * dist);
                    }
                    switch (ComputerType)
                    {
                        case ComputerID.Swarm:
                            SwarmAI();
                            break;
                        case ComputerID.Picket:
                            PicketAI();
                            break;
                        case ComputerID.Line:
                            LineAI();
                            break;
                        case ComputerID.Artillery:
                            ArtilleryAI();
                            break;
                        case ComputerID.Carrier:
                            CarrierAI();
                            break;
                        case ComputerID.Bomber:
                            BomberAI();
                            break;
                    }
                }
            }
            else if (shipAI == ShipAI.Missing)
            {
                NPC.velocity = Vector2.Zero;
                NPC.dontTakeDamageFromHostiles = true;
                NPC.dontTakeDamage = true;
                MissingTimer--;
                if (MissingTimer <= 0)
                {
                    NPC.dontTakeDamageFromHostiles = false;
                    NPC.dontTakeDamage = false;
                    shipAI = ShipAI.Default;
                    NPC.rotation = Main.rand.NextFloat() * MathHelper.TwoPi;
                    NPC.Center = new Vector2(50 + Main.rand.Next(Main.maxTilesX - 100), 50 + Main.rand.Next(Main.maxTilesY - 100)) * 16f;
                    for (int i = 0; i < NPC.oldPos.Length; i++)
                    {
                        NPC.oldPos[i] = NPC.position;
                        NPC.oldRot[i] = NPC.rotation;
                    }
                    HurtTimer = 180;
                }
            }
            foreach (BaseWeaponUnit weapon in weapons)
            {
                weapon.Update(NPC);
            }
        }

        public void SwarmAI()
        {
            NPC target = Main.npc[CurrentTarget];
            if (target.DistanceWithWidth(NPC) > MinRange * 0.9f)//当目标距离大于最小距离，冲锋至最小距离
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
            }
            else            //当目标距离小于最小距离，盘旋
            {
                float r1 = (target.Center - NPC.Center).ToRotation();
                float r2 = NPC.rotation;
                float r3 = r1 - r2;
                while (r3 >= MathHelper.Pi) r3 -= MathHelper.TwoPi;
                while (r3 <= -MathHelper.Pi) r3 += MathHelper.TwoPi;
                float MoveVec = (target.Center - NPC.Center).ToRotation() - Math.Sign(r3) * MathHelper.Pi / 2;
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 100f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 360f, 0, GetFinalSpeed(MaxSpeed));

            }
        }

        public void BomberAI()
        {
            NPC target = Main.npc[CurrentTarget];
            if (target.DistanceWithWidth(NPC) > MinRange * 0.9f)//当目标距离大于最小距离，冲锋至最小距离
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) < MinRange / 2)            //当目标距离小于最小距离的一半，保持最小距离
            {
                float MoveVec = (NPC.Center - target.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (CalcDifference(MoveVec, NPC.rotation) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else
            {
                NPC.velocity *= 0.8f;
                NPC.rotation = SmoothRotation(NPC.rotation, (target.Center - NPC.Center).ToRotation(), GetFinalSpeed(MaxSpeed) / 200f);
            }
        }

        public void PicketAI()
        {
            NPC target = Main.npc[CurrentTarget];
            if (target.DistanceWithWidth(NPC) > MidRange * 2) //当目标距离大于中距离的两倍，冲锋至中距离
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 90f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) > MidRange * 0.9f) //当目标距离大于中距离，冲锋至中距离并盘旋
            {
                float r1 = (target.Center - NPC.Center).ToRotation();
                float r2 = NPC.rotation;
                float r3 = r1 - r2;
                while (r3 >= MathHelper.Pi) r3 -= MathHelper.TwoPi;
                while (r3 <= -MathHelper.Pi) r3 += MathHelper.TwoPi;
                float MoveVec = (target.Center - NPC.Center).ToRotation() - Math.Sign(r3) * MathHelper.Pi / 3;
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 90f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) < 60)               //当目标距离小于60，盘旋保持60
            {
                float r1 = (NPC.Center - target.Center).ToRotation();
                float r2 = NPC.rotation;
                float r3 = r1 - r2;
                while (r3 >= MathHelper.Pi) r3 -= MathHelper.TwoPi;
                while (r3 <= -MathHelper.Pi) r3 += MathHelper.TwoPi;
                float MoveVec = (NPC.Center - target.Center).ToRotation() - Math.Sign(r3) * MathHelper.Pi / 3;
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (CalcDifference(MoveVec, NPC.rotation) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 90f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else
            {
                NPC.velocity *= 0.97f;
            }
        }

        public void LineAI()
        {
            NPC target = Main.npc[CurrentTarget];
            if (target.DistanceWithWidth(NPC) > MidRange) //当目标距离大于中距离，冲锋至中距离
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 90f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) < MidRange / 2)             //当目标距离小于中距离的一半，保持中距离
            {
                float MoveVec = (NPC.Center - target.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (CalcDifference(MoveVec, NPC.rotation) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 90f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else
            {
                NPC.velocity *= 0.97f;
                NPC.rotation = SmoothRotation(NPC.rotation, (target.Center - NPC.Center).ToRotation(), GetFinalSpeed(MaxSpeed) / 200f);
            }
        }

        public void ArtilleryAI()
        {
            NPC target = Main.npc[CurrentTarget];
            if (target.DistanceWithWidth(NPC) > MaxRange) //当目标距离大于远距离，冲锋至远距离
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) < MidRange * 0.9f)             //当目标距离小于中距离，保持中距离
            {
                float MoveVec = (NPC.Center - target.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (CalcDifference(MoveVec, NPC.rotation) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
            }
            else
            {
                NPC.velocity *= 0.97f;
                NPC.rotation = SmoothRotation(NPC.rotation, (target.Center - NPC.Center).ToRotation(), GetFinalSpeed(MaxSpeed) / 200f);
            }
        }

        public void CarrierAI()
        {
            NPC target = Main.npc[CurrentTarget];
            float range = float.Max(MaxRange, 400);
            if (target.DistanceWithWidth(NPC) > range * 1.5f) //当目标距离大于远距离1.5倍，冲锋至远距离1.5倍
            {
                float MoveVec = (target.Center - NPC.Center).ToRotation();
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) > range) //当目标距离大于远距离，冲锋至远距离并盘旋
            {
                float r1 = (target.Center - NPC.Center).ToRotation();
                float r2 = NPC.rotation;
                float r3 = r1 - r2;
                while (r3 >= MathHelper.Pi) r3 -= MathHelper.TwoPi;
                while (r3 <= -MathHelper.Pi) r3 += MathHelper.TwoPi;
                float MoveVec = (target.Center - NPC.Center).ToRotation() - Math.Sign(r3) * MathHelper.Pi / 3;
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);

                NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
            }
            else if (target.DistanceWithWidth(NPC) < range * 0.9f)             //当目标距离小于远距离，盘旋保持远距离
            {
                float r1 = (NPC.Center - target.Center).ToRotation();
                float r2 = NPC.rotation;
                float r3 = r1 - r2;
                while (r3 >= MathHelper.Pi) r3 -= MathHelper.TwoPi;
                while (r3 <= -MathHelper.Pi) r3 += MathHelper.TwoPi;
                float MoveVec = (NPC.Center - target.Center).ToRotation() - Math.Sign(r3) * MathHelper.Pi / 3;
                NPC.rotation = SmoothRotation(NPC.rotation, MoveVec, GetFinalSpeed(MaxSpeed) / 200f);
                if (CalcDifference(MoveVec, NPC.rotation) < MathHelper.Pi / 32f)
                {
                    NPC.velocity = NPC.rotation.ToRotationVector2() * MathHelper.Clamp(NPC.velocity.Length() + GetFinalSpeed(MaxSpeed) / 120f, 0, GetFinalSpeed(MaxSpeed));
                }
                else
                {
                    NPC.velocity *= 0.97f;
                }
            }
            else
            {
                NPC.velocity *= 0.97f;
                //NPC.rotation = SmoothRotation(NPC.rotation, (target.Center - NPC.Center).ToRotation(), MaxSpeed / 200f);
            }
        }

        public void SelectTarget()
        {
            if (FleetSystem.Passive || shipAI == ShipAI.Missing)
            {
                CurrentTarget = -1;
                return;
            }
            Vector2 Center = FleetSystem.Following ? Main.LocalPlayer.Center : FleetSystem.TipPos;
            if (Main.LocalPlayer.MinionAttackTargetNPC >= 0 && Main.LocalPlayer.MinionAttackTargetNPC < 200)
            {
                CurrentTarget = Main.LocalPlayer.MinionAttackTargetNPC;
            }
            CurrentTarget = TargetSelectHelper.GetClosestTarget(NPC.Center, Center, DetectRange, CurrentTarget, CanSeeThroughTiles);
        }

        private static float SmoothRotation(float current, float target, float speed)
        {
            // 计算最短旋转角度
            float difference = target - current;
            while (difference > MathHelper.Pi)
            {
                difference -= MathHelper.TwoPi;
            }
            while (difference < -MathHelper.Pi)
            {
                difference += MathHelper.TwoPi;
            }

            // 插值计算
            float newRotation = current + MathHelper.Clamp(difference, -speed, speed);
            return newRotation;
        }

        private static float CalcDifference(float current, float target)
        {
            float difference = target - current;
            while (difference > MathHelper.Pi)
            {
                difference -= MathHelper.TwoPi;
            }
            while (difference < -MathHelper.Pi)
            {
                difference += MathHelper.TwoPi;
            }
            return (float)Math.Abs(difference);
        }

        private int CalcMissingTime()
        {
            if (FTLLevel == 1)        //特别慢
            {
                return (270 + Main.rand.Next(30)) * 60;
            }
            if (FTLLevel == 6)      //灵能跃迁引擎
            {
                return Main.rand.Next(30) * 60;
            }
            return (30 + Main.rand.Next(30) + (5 - FTLLevel) * Main.rand.Next(60)) * 60;
        }

        private void FTLMove(Vector2 TargetPos)
        {
            float scale = ShipLength / 70f;
            shipAI = ShipAI.FTLMove;
            FTLTimer = 25;
            FTLTargetPos = TargetPos;
            FTLLight2.Summon(NPC.GetSource_FromAI(), TargetPos, scale);
            FTLLight2.Summon(NPC.GetSource_FromAI(), NPC.Center, scale);
        }

        private void AddLight()
        {
            //舰体发光
            int width = ShipWidth;
            int length = ShipLength;
            Vector2 UnitX = NPC.rotation.ToRotationVector2();
            for (int i = -length / 16; i <= length / 16; i++)
            {
                for (int j = -length / 16; j <= length / 16; j++)
                {
                    Vector2 Pos = NPC.Center + new Vector2(i * 16 + 8, j * 16 + 8);
                    float t = 0;
                    if (Collision.CheckAABBvLineCollision(Pos, new Vector2(1, 1), NPC.Center - UnitX * length / 2f, NPC.Center + UnitX * length / 2f, width, ref t))
                    {
                        Lighting.AddLight(Pos, 1f, 1f, 1.2f);
                    }
                }
            }
            Lighting.AddLight(NPC.Center, 1f, 1f, 1.2f);

            //尾迹发光
            foreach (Vector2 v in NPC.oldPos)
            {
                if (v != Vector2.Zero)
                {
                    SomeUtils.AddLight(v + NPC.Size / 2, Color.Cyan, 2);
                }
            }
        }

        /// <summary>
        /// 修改着弹面积
        /// </summary>
        private void ModifyScaleForShield()
        {
            Vector2 Center = NPC.Center;
            if (CurrentShield > 0)
            {
                NPC.width = NPC.height = ShipLength;
            }
            else
            {
                NPC.width = NPC.height = ShipWidth;
            }
            NPC.Center = Center;
        }

        private float GetFinalSpeed(float speed)
        {
            float modifier = 1f;
            if (FleetSystem.GlobalEffects.Contains(ModifierID.ShroudSpeedUp))
            {
                modifier += 0.25f;
            }
            return speed * modifier;
        }
    }
}
