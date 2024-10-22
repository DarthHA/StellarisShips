
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class ArcProj : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";
        public Vector2 ModifiedScale = new Vector2(1, 1);
        public Color ArcColor = Color.LightSkyBlue;

        public Vector2 RelaPos = Vector2.Zero;
        public Vector2 OffSetPos = Vector2.Zero;
        public Vector2 TargetPos = Vector2.Zero;
        public int ownerID = -1;

        public List<Vector2> SegPoints = new();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 1000;
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

            Projectile.Center = shipNPC.GetPosOnShip(RelaPos) + OffSetPos;

            SomeUtils.AddLightLine(Projectile.Center, TargetPos, ArcColor, 5);

            SegPoints.Clear();
            float Len = TargetPos.Distance(Projectile.Center);
            if (Len >= 20)
            {
                Vector2 UnitX = Vector2.Normalize(TargetPos - Projectile.Center);
                Vector2 UnitY = (UnitX.ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                float X = 0;
                do
                {
                    X += (Main.rand.NextFloat() * 20 + 1) * ModifiedScale.X;
                    float Y = (Main.rand.NextFloat() * 30 - 15) * ModifiedScale.Y;
                    SegPoints.Add(new Vector2(X, Y));
                } while (X <= Len);
                SegPoints.Add(Projectile.Center + new Vector2(Len, 0));
            }

            Projectile.ai[0]++;
            Projectile.scale = 20 - Projectile.ai[0];
            if (Projectile.ai[0] >= 20) Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (SegPoints.Count > 2)
            {
                Texture2D texExtra = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomLine").Value;
                void Draw(Color color, float scale)
                {
                    List<CustomVertexInfo> vertexInfos = new();
                    float width = scale * ModifiedScale.Y;
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

                    DrawUtils.DrawTrail(texExtra, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
                }

                for (float i = 0; i < 1; i += 1f / (Projectile.scale + 1f))
                {
                    Draw(ArcColor * (1 - i), 2.5f + i * 25f);
                }
                Draw(Color.White, 2.5f);
            }

            float t1 = Projectile.Opacity = MathHelper.Clamp((20 - Projectile.ai[0]) / 15f, 0f, 1f);
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Vector2 OffSet = Tex1.Size() / 2f;
            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 0.3f * k * ModifiedScale.Y;
                Main.spriteBatch.Draw(Tex1, TargetPos - Main.screenPosition, null, ArcColor * (1f - k) * t1, Projectile.rotation, OffSet, scale, SpriteEffects.FlipHorizontally, 0);
            }

            Main.spriteBatch.Draw(Tex1, TargetPos - Main.screenPosition, null, Color.White * t1, Projectile.rotation, OffSet, 0.1f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);

            return false;
        }

        public static void Summon(NPC ship, Vector2 relaPos,Vector2 offsetPos, Vector2 targetPos, Color color,float scale=1f)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(relaPos), Vector2.Zero, ModContent.ProjectileType<ArcProj>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as ArcProj).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as ArcProj).TargetPos = targetPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).RelaPos = relaPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).OffSetPos = offsetPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).ArcColor = color;
                (Main.projectile[protmp].ModProjectile as ArcProj).ModifiedScale = new Vector2(scale, scale);
            }
        }

        public static void Summon(NPC ship, Vector2 relaPos, Vector2 offsetPos, Vector2 targetPos, Color color, Vector2 scale)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(relaPos), Vector2.Zero, ModContent.ProjectileType<ArcProj>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as ArcProj).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as ArcProj).TargetPos = targetPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).RelaPos = relaPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).OffSetPos = offsetPos;
                (Main.projectile[protmp].ModProjectile as ArcProj).ArcColor = color;
                (Main.projectile[protmp].ModProjectile as ArcProj).ModifiedScale = scale;
            }
        }


        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
