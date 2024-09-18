using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class ArchLaserExplosion : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CanDistortWater[Projectile.type] = true;
        }


        public Color color = Color.Orange;
        public float ExplosionScale = 1f;

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 480;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] < 5)
            {
                Projectile.scale = 0.5f + Projectile.ai[1] / 5f * 3.5f * ExplosionScale;
            }
            else if (Projectile.ai[1] < 20)
            {
                Projectile.Opacity = MathHelper.Lerp(1, 0, (Projectile.ai[1] - 5) / 15f);
            }
            else
            {
                Projectile.Kill();
            }
            SomeUtils.AddLight(Projectile.Center, color, 2 * ExplosionScale);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Vector2 OffSet = Tex1.Size() / 2f;
            for (float k = 0.5f; k <= 1.5f; k += 0.05f)
            {
                float scale = 0.075f * k;
                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
            }

            Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * 0.025f, SpriteEffects.FlipHorizontally, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);

            return false;
        }

        public static void Summon(IEntitySource entitySource, Vector2 Pos, Color color, float explosionScale = 1f)
        {
            int protmp = Projectile.NewProjectile(entitySource, Pos, Vector2.Zero, ModContent.ProjectileType<ArchLaserExplosion>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp <= 1000)
            {
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as ArchLaserExplosion).color = color;
                (Main.projectile[protmp].ModProjectile as ArchLaserExplosion).ExplosionScale = explosionScale;
            }
        }
    }
}
