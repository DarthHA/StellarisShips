using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class DisruptorUnit : BaseWeaponUnit
    {
        public override string InternalName => "Disruptor";
        public Vector2? TargetPos = null;
        float MiscTimer = 0;

        private int LastTarget = -1;
        private int ReAttackTime = 0;

        public override void Update(NPC ship)
        {
            if (++MiscTimer >= 60) MiscTimer = 0;
            if (CurrentCooldown > 0) CurrentCooldown--;
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                Rotation = ship.rotation;
                TargetPos = null;
                LastTarget = -1;
                ReAttackTime = 0;
            }
            else                   //进战状态
            {
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    TargetPos = Main.npc[target].Center;
                    Rotation = (TargetPos.Value - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown <= 0)
                    {
                        if (LastTarget == target)   //连续攻击同一敌人伤害提高
                        {
                            if (ReAttackTime < 12) ReAttackTime++;
                        }
                        else
                        {
                            ReAttackTime = 0;
                        }
                        int damage = RandomDamage;
                        damage += (int)(damage * 0.05f * ReAttackTime);
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        DamageProj.Summon(ship.GetSource_FromAI(), TargetPos.Value, damage, crit, 0f);
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Disruptor", 3, shipNPC.GetPosOnShip(RelativePos));
                        LastTarget = target;
                    }

                    Color LaserColor = Color.White;
                    switch (Level)
                    {
                        case 1:
                            LaserColor = Color.LightGoldenrodYellow;
                            break;
                        case 2:
                            LaserColor = Color.LightBlue;
                            break;
                        case 3:
                            LaserColor = Color.LightGreen;
                            break;
                    }
                    SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos.Value, LaserColor);
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    LastTarget = -1;
                    ReAttackTime = 0;
                    TargetPos = null;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (TargetPos.HasValue)
            {
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos);
                Color LaserColor = Color.White;
                int width = 1;
                switch (EquipType)
                {
                    case ComponentTypes.Weapon_S:
                        width = 3;
                        break;
                    case ComponentTypes.Weapon_M:
                        width = 5;
                        break;
                }
                switch (Level)
                {
                    case 1:
                        LaserColor = Color.Orange;
                        break;
                    case 2:
                        LaserColor = Color.LightBlue;
                        break;
                    case 3:
                        LaserColor = Color.LightGreen;
                        break;
                }
                LaserColor *= 1f + 0.2f * (float)Math.Sin(MiscTimer / 60f * MathHelper.TwoPi);
                /*
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 CenterOffset = new(width / 2f, width / 2f);
                Vector2 UnitY = ((TargetPos.Value - SelfPos).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2() * (width / 2f + 1f);
                Utils.DrawLine(spriteBatch, TargetPos.Value - CenterOffset, SelfPos - CenterOffset, LaserColor * 0.6f, LaserColor * 0.6f, width);
                Utils.DrawLine(spriteBatch, TargetPos.Value - CenterOffset + UnitY, SelfPos + UnitY - CenterOffset, LaserColor * 0.5f, LaserColor * 0.25f, width);
                Utils.DrawLine(spriteBatch, TargetPos.Value - CenterOffset - UnitY, SelfPos - UnitY - CenterOffset, LaserColor * 0.5f, LaserColor * 0.25f, width);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
                */

                Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomLine2").Value;
                void DrawMain(Color color, float scale)
                {
                    List<CustomVertexInfo> vertexInfos = new();
                    float width2 = scale * width;
                    Vector2 UnitY = ((TargetPos.Value - SelfPos).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                    vertexInfos.Add(new CustomVertexInfo(SelfPos + UnitY * width2, Color.White, new Vector3(0, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(SelfPos - UnitY * width2, Color.White, new Vector3(0, 1f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(TargetPos.Value + UnitY * width2, Color.White, new Vector3(1, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(TargetPos.Value - UnitY * width2, Color.White, new Vector3(1, 1f, 1)));
                    DrawUtils.DrawTrail(Tex1, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
                }

                for (float i = 0; i < 1; i += 1f / 4f)
                {
                    DrawMain(LaserColor * (1 - i), 0.5f + i * 5f);
                }

                Texture2D Tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.02f * k * width;
                    Main.spriteBatch.Draw(Tex2, TargetPos.Value - screenPos, null, LaserColor * (1f - k), 0, Tex2.Size() / 2f, scale, SpriteEffects.FlipHorizontally, 0);
                }

                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
        }
    }

}