using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class LanceProj : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";
        public float ModifiedScale = 1f;
        public Color LanceColor = Color.LightSkyBlue;

        public Vector2 RelaPos = Vector2.Zero;
        public Vector2 TargetPos = Vector2.Zero;
        public int ownerID = -1;

        public bool Crit = false;

        public List<Vector2> SegPoints = new();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.npcProj = true;
            Projectile.Opacity = 0;
            Projectile.scale = 20f;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 100;
        }

        public override void AI()
        {

            if (ownerID == -1 || !Main.npc[ownerID].active || Main.npc[ownerID].type != ModContent.NPCType<ShipNPC>())
            {
                Projectile.Kill();
                return;
            }
            NPC owner = Main.npc[ownerID];
            ShipNPC shipNPC = owner.GetShipNPC();
            if (!owner.ShipActive())                //失踪
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = shipNPC.GetPosOnShip(RelaPos);

            SomeUtils.AddLightLine(Projectile.Center, TargetPos + Vector2.Normalize(TargetPos - Projectile.Center) * 2000, LanceColor, 5);

            if (SegPoints.Count <= 0)
            {
                float Len = TargetPos.Distance(Projectile.Center) + 2000;
                Vector2 UnitX = Vector2.Normalize(TargetPos - Projectile.Center);
                Vector2 UnitY = (UnitX.ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                float X = 0;
                bool screen = false;
                do
                {
                    X += 3;
                    float Y = (float)Math.Sin(X / 40f * MathHelper.TwoPi) * 8;
                    SegPoints.Add(new Vector2(X, Y));
                    if (InScreen(Projectile.Center + UnitX * X + UnitY * Y))
                    {
                        screen = true;
                    }
                    else
                    {
                        if (screen) break;
                    }
                } while (X <= Len);
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] <= 5)
            {
                Projectile.scale = Projectile.ai[0] * 4;
            }
            else
            {
                Projectile.scale = 25 - Projectile.ai[0];
            }
            if (Projectile.ai[0] >= 25) Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomLine").Value;
            //Texture2D Tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
            void DrawMain(Color color, float scale)
            {
                List<CustomVertexInfo> vertexInfos = new();
                float width = scale * ModifiedScale;
                Vector2 UnitY = ((TargetPos - Projectile.Center).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                Vector2 ExtraStage = Vector2.Normalize(TargetPos - Projectile.Center) * 2000;
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitY * width, Color.White, new Vector3(0, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center - UnitY * width, Color.White, new Vector3(0, 1f, 1)));
                vertexInfos.Add(new CustomVertexInfo(TargetPos + ExtraStage + UnitY * width, Color.White, new Vector3(1, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(TargetPos + ExtraStage - UnitY * width, Color.White, new Vector3(1, 1f, 1)));
                DrawUtils.DrawTrail(Tex1, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
            }
            for (float i = 0; i < 1; i += 1f / (Projectile.scale + 1f))
            {
                DrawMain(LanceColor * (1 - i), 4f + i * 40f);
            }
            DrawMain(Color.White, 2.5f);

            if (SegPoints.Count > 2)
            {
                void DrawSpine(Color color, float scale)
                {
                    List<CustomVertexInfo> vertexInfos = new();
                    float width = scale * ModifiedScale;
                    Vector2 UnitX = Vector2.Normalize(TargetPos - Projectile.Center);
                    Vector2 UnitY = ((TargetPos - Projectile.Center).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                    vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitY * width, Color.White, new Vector3(0, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(Projectile.Center - UnitY * width, Color.White, new Vector3(0, 1f, 1)));
                    for (int i = 1; i < SegPoints.Count - 1; i++)
                    {
                        Vector2 UnitY2 = ((SegPoints[i - 1].X * UnitX + SegPoints[i - 1].Y * UnitY) + (SegPoints[i + 1].X * UnitX + SegPoints[i + 1].Y * UnitY) - 2 * (SegPoints[i].X * UnitX + SegPoints[i].Y * UnitY)).ToRotation().ToRotationVector2();
                        vertexInfos.Add(new CustomVertexInfo(Projectile.Center + (SegPoints[i].X * UnitX + SegPoints[i].Y * UnitY) + UnitY2 * width, Color.White, new Vector3((float)i / SegPoints.Count, 0f, 1)));
                        vertexInfos.Add(new CustomVertexInfo(Projectile.Center + (SegPoints[i].X * UnitX + SegPoints[i].Y * UnitY) - UnitY2 * width, Color.White, new Vector3((float)i / SegPoints.Count, 1f, 1)));
                    }
                    vertexInfos.Add(new CustomVertexInfo(Projectile.Center + (SegPoints[SegPoints.Count - 1].X * UnitX + SegPoints[SegPoints.Count - 1].Y * UnitY) + UnitY * width, Color.White, new Vector3(1, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(Projectile.Center + (SegPoints[SegPoints.Count - 1].X * UnitX + SegPoints[SegPoints.Count - 1].Y * UnitY) - UnitY * width, Color.White, new Vector3(1, 1f, 1)));

                    DrawUtils.DrawTrail(Tex1, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
                }
                for (float i = 0; i < 1; i += 2f / (Projectile.scale + 1f))
                {
                    DrawSpine(LanceColor * (1 - i), 0.5f + i * 5f);
                }
                DrawSpine(Color.White, 0.5f);
            }

            return false;
        }

        public static void Summon(NPC ship, Vector2 relaPos, Vector2 targetPos, Color color, int dmg, bool crit = false, float kb = 0)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(relaPos), Vector2.Zero, ModContent.ProjectileType<LanceProj>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as LanceProj).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as LanceProj).TargetPos = targetPos;
                (Main.projectile[protmp].ModProjectile as LanceProj).RelaPos = relaPos;
                (Main.projectile[protmp].ModProjectile as LanceProj).LanceColor = color;
                (Main.projectile[protmp].ModProjectile as LanceProj).Crit = crit;
            }
        }


        public override bool ShouldUpdatePosition()
        {
            return false;
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float t = 0;
            int width = 16;
            Vector2 ExtraStage = Vector2.Normalize(TargetPos - Projectile.Center) * 2000;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, TargetPos + ExtraStage, width, ref t);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Crit)
            {
                modifiers.SetCrit();
            }
            else
            {
                modifiers.DisableCrit();
            }
            modifiers.DefenseEffectiveness *= 2;

        }

        public bool InScreen(Vector2 Pos)
        {
            Rectangle rec = new((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            return rec.Contains(Pos.ToPoint());
        }
    }
}
