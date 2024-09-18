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
    public class TitanBeamProj : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";
        public float ModifiedScale = 1f;
        public Color BeamColor = Color.LightSkyBlue;

        public Vector2 RelaPos = Vector2.Zero;
        public Vector2 TargetPos = Vector2.Zero;
        public int ownerID = -1;

        public bool Crit = false;


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
            Projectile.localNPCHitCooldown = 999;
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

            SomeUtils.AddLightLine(Projectile.Center, TargetPos + Vector2.Normalize(TargetPos - Projectile.Center) * 2000, BeamColor, 5);

            Projectile.ai[0]++;
            if (Projectile.ai[0] <= 5)
            {
                Projectile.scale = Projectile.ai[0] * 20;
            }
            else
            {
                Projectile.scale = 125 - Projectile.ai[0] * 5;
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
                Vector2 UnitX = Vector2.Normalize(TargetPos - Projectile.Center);
                Vector2 UnitY = (UnitX.ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                float TotalLength = 2000 + TargetPos.Distance(Projectile.Center);
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitY * width / 4, Color.White, new Vector3(0, 0.375f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center - UnitY * width / 4, Color.White, new Vector3(0, 0.625f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitX * 40 + UnitY * width, Color.White, new Vector3(0.1f, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitX * 40 - UnitY * width, Color.White, new Vector3(0.1f, 1f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitX * TotalLength + UnitY * width, Color.White, new Vector3(1, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(Projectile.Center + UnitX * TotalLength - UnitY * width, Color.White, new Vector3(1, 1f, 1)));
                DrawUtils.DrawTrail(Tex1, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
            }

            for (float i = 0; i < 1; i += 1f / (Projectile.scale + 1f))
            {
                DrawMain(BeamColor * (1 - i), 8f + i * 80f);
            }
            DrawMain(Color.White, 4f);


            float t = MathHelper.Clamp(Projectile.ai[0] / 5f, 0f, 1f);
            if (Projectile.ai[0] > 5) t = (25 - Projectile.ai[0]) / 20f;
            Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Vector2 OffSet = Tex.Size() / 2f;
            for (float k = 0.5f; k <= 1.5f; k += 0.025f)
            {
                float scale = 0.45f * k * ModifiedScale * 1.2f;
                Main.spriteBatch.Draw(Tex, Projectile.Center - Main.screenPosition, null, BeamColor * (1f - k), 0, OffSet, t * scale, SpriteEffects.FlipHorizontally, 0);
            }
            Main.spriteBatch.Draw(Tex, Projectile.Center - Main.screenPosition, null, Color.White, 0, OffSet, t * 0.15f * ModifiedScale * 1.2f, SpriteEffects.FlipHorizontally, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            return false;
        }

        public static void Summon(NPC ship, Vector2 relaPos, Vector2 targetPos, Color color, int dmg, bool crit = false, float kb = 0)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(relaPos), Vector2.Zero, ModContent.ProjectileType<TitanBeamProj>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as TitanBeamProj).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as TitanBeamProj).TargetPos = targetPos;
                (Main.projectile[protmp].ModProjectile as TitanBeamProj).RelaPos = relaPos;
                (Main.projectile[protmp].ModProjectile as TitanBeamProj).BeamColor = color;
                (Main.projectile[protmp].ModProjectile as TitanBeamProj).Crit = crit;
            }
        }


        public override bool ShouldUpdatePosition()
        {
            return false;
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float t = 0;
            int width = 50;
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

    }
}
