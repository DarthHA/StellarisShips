
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class StrikeCraftProj : BaseDamageProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        /// <summary>
        /// 随机生成的UUID用于武器模块识别对应的舰载机
        /// </summary>
        public long UUID = 0;
        /// <summary>
        /// 所属舰船
        /// </summary>
        public int ownerID = -1;

        public int MaxDmg = 10;
        public int MinDmg = 1;
        public float WeaponCrit = 50;
        public float MaxSpeed = 20;
        public int AttackCooldown = 10;
        public float AttackRange = 300;
        public int Level = 1;                //五级为文物小飞机，六级为虫群舰载机
        public Vector2 ReturnCenter = Vector2.Zero;
        public bool Destroyed = false;

        public Vector2 AttackRelaPos = Vector2.Zero;

        private const float DamageModifier = 1.2f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
        }

        public override void SafeSetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 999999;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.npcProj = true;
            Projectile.netImportant = true;
        }



        public override void AI()
        {

            if (ownerID == -1 || !Main.npc[ownerID].active || Main.npc[ownerID].type != ModContent.NPCType<ShipNPC>())
            {
                Destroyed = true;
                Projectile.Kill();
                return;
            }

            NPC owner = Main.npc[ownerID];
            ShipNPC shipNPC = owner.GetShipNPC();
            if (!owner.ShipActive() || Projectile.Distance(owner.Center) > 5000)                //失踪或者距离过远
            {
                Destroyed = true;
                Projectile.Kill();
                return;
            }
            else if (shipNPC.CurrentTarget == -1)              //脱战状态
            {
                Vector2 TargetPos = shipNPC.GetPosOnShip(ReturnCenter);
                float MaxSpeed2 = owner.velocity.Length() + MaxSpeed;
                if (TargetPos.Distance(Projectile.Center) <= MaxSpeed2 / 2f)
                {
                    Destroyed = false;
                    Projectile.Kill();
                }
                else
                {
                    Vector2 TargetVel = Vector2.Normalize(TargetPos - Projectile.Center) * MaxSpeed2;
                    Projectile.velocity = Projectile.velocity * 0.85f + TargetVel * 0.15f;
                    if (Projectile.velocity.Length() > MaxSpeed2) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * MaxSpeed2;
                    Projectile.rotation = Projectile.velocity.ToRotation();
                }
                AttackCooldown = 30;
            }
            else              //临战状态
            {
                if (AttackCooldown > 0) AttackCooldown--;
                int targetID = TargetSelectHelper.GetClosestTargetWithWidthForStriker(owner);
                if (Level == 5 || Level == 6) targetID = shipNPC.CurrentTarget;               //钻孔无人机和虫群舰载机优先攻击主目标
                if (targetID != -1)
                {
                    NPC target = Main.npc[targetID];

                    Vector2 TargetPos = target.Center;
                    Vector2 TargetVel = Vector2.Normalize(TargetPos - Projectile.Center) * MaxSpeed;
                    if (TargetPos.Distance(Projectile.Center) > 300)
                    {
                        Projectile.velocity = Projectile.velocity * 0.95f + TargetVel * 0.05f;
                    }
                    else
                    {
                        if (Projectile.velocity.Length() < MaxSpeed * 0.7f) Projectile.velocity *= 1.1f;
                    }

                    if (Projectile.velocity.Length() > MaxSpeed) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * MaxSpeed;
                    Projectile.rotation = Projectile.velocity.ToRotation();

                    //攻击

                    if (target.Distance(Projectile.Center) <= AttackRange)
                    {
                        if (AttackCooldown <= 0)
                        {
                            int damage = Main.rand.Next(MaxDmg - MinDmg + 1) + MinDmg;
                            damage = (int)(damage * DamageModifier);
                            bool crit = Main.rand.NextFloat() < WeaponCrit / 100f;
                            int protmp = DamageProj.Summon(Projectile.GetSource_FromThis(), TargetPos, damage, crit, 0f);
                            if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = SourceName;
                            AttackCooldown = (int)((Main.rand.NextFloat() * 0.4f + 0.8f) * 30f);
                            SomeUtils.PlaySoundRandom(SoundPath.Fire + "Laser", 6, Projectile.Center);
                        }

                        Color LaserColor = Color.White;
                        switch (Level)
                        {
                            case 1:
                                LaserColor = Color.SkyBlue;
                                break;
                            case 2:
                                LaserColor = Color.MediumPurple;
                                break;
                            case 3:
                                LaserColor = Color.LightGreen;
                                break;
                            case 4:
                                LaserColor = Color.Orange;
                                break;
                            case 5:
                                LaserColor = Color.LightGreen;
                                break;
                        }

                        SomeUtils.AddLightLine(Projectile.Center, TargetPos, LaserColor);
                    }
                }
            }
            SomeUtils.AddLight(Projectile.Center, Color.White);
        }

        /// <summary>
        /// 注意：五级为远古小飞机
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="Center"></param>
        /// <param name="returnCenter"></param>
        /// <param name="velocity"></param>
        /// <param name="maxDmg"></param>
        /// <param name="minDmg"></param>
        /// <param name="crit"></param>
        /// <param name="speed"></param>
        /// <param name="attackRange"></param>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public static int Summon(NPC ship, Vector2 Center, Vector2 returnCenter, Vector2 velocity, int maxDmg, int minDmg, float crit, float speed, float attackRange, int lvl)
        {
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), Center, velocity, ModContent.ProjectileType<StrikeCraftProj>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).MaxDmg = maxDmg;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).MinDmg = minDmg;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).WeaponCrit = crit;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).MaxSpeed = speed;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).AttackRange = attackRange;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).Level = lvl;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).ReturnCenter = returnCenter;
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).AttackRelaPos = new Vector2(Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f) * 0.75f;
                long uuid = Main.rand.Next(1145141919);
                (Main.projectile[protmp].ModProjectile as StrikeCraftProj).UUID = uuid;
            }
            return protmp;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (ownerID == -1 || !Main.npc[ownerID].active || Main.npc[ownerID].type != ModContent.NPCType<ShipNPC>())
            {
                return false;
            }

            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/Common/StrikeCraft", AssetRequestMode.ImmediateLoad).Value;
            DrawTrail(Main.spriteBatch);

            //绘制激光
            DrawLaser(Main.spriteBatch);

            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, 0.25f, SpriteEffects.None, 0);
            return false;
        }

        public void DrawLaser(SpriteBatch spriteBatch)
        {
            ShipNPC shipNPC = Main.npc[ownerID].GetShipNPC();
            if (shipNPC.CurrentTarget != -1)
            {
                int targetID = TargetSelectHelper.GetClosestTargetWithWidthForStriker(shipNPC.NPC);
                if (Level == 5 || Level == 6) targetID = shipNPC.CurrentTarget;              //钻孔无人机和虫群舰载机优先攻击主目标

                if (targetID != -1)
                {
                    NPC target = Main.npc[targetID];

                    if (target.Distance(Projectile.Center) < AttackRange)
                    {
                        Color LaserColor = Color.White;
                        switch (Level)
                        {
                            case 1:
                                LaserColor = Color.SkyBlue;
                                break;
                            case 2:
                                LaserColor = Color.MediumPurple;
                                break;
                            case 3:
                                LaserColor = Color.LightGreen;
                                break;
                            case 4:
                                LaserColor = Color.Orange;
                                break;
                            case 5:
                                LaserColor = Color.LightGreen;
                                break;
                        }

                        EasyDraw.AnotherDraw(BlendState.Additive);
                        Utils.DrawLine(spriteBatch, target.Center + AttackRelaPos * target.Size, Projectile.Center, LaserColor, LaserColor, 1);
                        EasyDraw.AnotherDraw(BlendState.AlphaBlend);
                    }
                }
            }
        }

        public void DrawTrail(SpriteBatch spriteBatch)
        {
            Texture2D texExtra = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BlobGlow2", AssetRequestMode.ImmediateLoad).Value;
            List<CustomVertexInfo> vertexInfos = new();
            Vector2 UnitY = (Projectile.rotation + MathHelper.Pi / 2).ToRotationVector2();
            Vector2 CenterOffset = Projectile.Size / 2f;
            float width = 1;
            vertexInfos.Add(new CustomVertexInfo(Projectile.Center + Projectile.rotation.ToRotationVector2() * 2 + UnitY * width, Color.White, new Vector3(0, 0f, 1)));
            vertexInfos.Add(new CustomVertexInfo(Projectile.Center + Projectile.rotation.ToRotationVector2() * 2 - UnitY * width, Color.White, new Vector3(0, 1f, 1)));
            float realLen = -1;
            for (int i = Projectile.oldPos.Length - 1; i >= 0; i--)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                {
                    continue;
                }
                else
                {
                    if (realLen == -1)
                    {
                        realLen = i + 1;
                        break;
                    }
                }
            }
            for (int i = 0; i < realLen; i++)
            {
                float progress = 0.1f + i / (realLen - 1f) * 0.9f;
                UnitY = (Projectile.oldRot[i] + MathHelper.Pi / 2).ToRotationVector2();
                vertexInfos.Add(new CustomVertexInfo(Projectile.oldPos[i] + CenterOffset + UnitY * width, Color.White, new Vector3(progress, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.oldPos[i] + CenterOffset - UnitY * width, Color.White, new Vector3(progress, 1f, 1)));
            }

            DrawUtils.DrawTrail(texExtra, vertexInfos, Main.spriteBatch, Color.Cyan * 0.8f, BlendState.Additive);

        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (Destroyed)
            {
                for (int i = 0; i < 12; i++)
                {
                    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 87, 0f, 0f, 0, default, 1f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 2f;
                }
            }
        }

        public void Boom()
        {
            Destroyed = true;
            Projectile.Kill();
        }
    }
}
