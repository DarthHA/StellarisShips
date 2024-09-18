using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class FTLLight : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

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
            if (Projectile.ai[0] > 30)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float t = 1f - Projectile.ai[0] / 30f;
            Color color = Color.Cyan;
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomFlare").Value;
            Texture2D Tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/LifeStar").Value;

            EasyDraw.AnotherDraw(BlendState.Additive);
            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 0.3f * k * ModifiedScale;
                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * t, Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
            }

            Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * t, Projectile.rotation, Tex1.Size() / 2f, Projectile.scale * 0.1f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);

            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 3f * k * ModifiedScale;
                Main.spriteBatch.Draw(Tex2, Projectile.Center - Main.screenPosition, null, color * (1f - k) * t, 0, Tex2.Size() / 2f, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
            }

            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            return false;
        }


        public static void Summon(IEntitySource entitySource, Vector2 Pos, float scale)
        {
            if (Pos.Distance(Main.LocalPlayer.Center) > Main.screenWidth + 50) return;
            int protmp = Projectile.NewProjectile(entitySource, Pos, Vector2.Zero, ModContent.ProjectileType<FTLLight>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp <= 1000)
            {
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as FTLLight).ModifiedScale = scale;
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

    }
}
