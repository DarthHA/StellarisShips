using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class FTLLight2 : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";         //25帧

        public float ModifiedScale = 1f;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 1)
            {
                Projectile.rotation = Main.rand.NextFloat() * MathHelper.TwoPi;
            }
            Projectile.rotation += MathHelper.Pi / 36f;
            Projectile.scale = 3f - 2.5f * Projectile.ai[0] / 30f;
            if (Projectile.ai[0] == 25)
            {
                FTLLight.Summon(Projectile.GetSource_Death(), Projectile.Center, ModifiedScale);
            }
            if (Projectile.ai[0] > 30)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float t = 0.25f + Projectile.ai[0] / 30f * 0.75f;
            Color color = Color.Cyan * 0.8f;
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/SpinnyNoise").Value;

            EasyDraw.AnotherDraw(BlendState.Additive);

            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 0.3f * k * ModifiedScale;
                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * t, Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
            }
            Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * 0.4f * t, Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * 0.1f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);

            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 0.3f * k * ModifiedScale;
                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * t, -Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
            }
            Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * 0.4f * t, -Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * 0.1f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);


            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            return false;
        }


        public static void Summon(IEntitySource entitySource, Vector2 Pos, float scale)
        {
            Rectangle ScreenRec = new Rectangle((int)Main.screenPosition.X - 100 - Main.screenWidth / 2, (int)Main.screenPosition.Y - 100 - Main.screenHeight / 2, Main.screenWidth * 2 + 200, Main.screenHeight * 2 + 200);
            if (!ScreenRec.Contains(Pos.ToPoint())) return;
            int protmp = Projectile.NewProjectile(entitySource, Pos, Vector2.Zero, ModContent.ProjectileType<FTLLight2>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as FTLLight2).ModifiedScale = scale;
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {

        }
    }
}
