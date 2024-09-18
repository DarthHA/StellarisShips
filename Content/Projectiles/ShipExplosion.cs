using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class ShipExplosion : ModProjectile
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

            if (Projectile.ai[1] == 10)
            {
                for (int i = 0; i < (int)(24 * ExplosionScale * ExplosionScale); i++)
                {
                    Vector2 vel = (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * (Main.rand.NextFloat() * 4f + 2f) * ExplosionScale;
                    int num110 = Dust.NewDust(Projectile.Center, 0, 0, 303, 0f, 0f, 0, default, 1f);    //73?
                    Main.dust[num110].scale = 1f + Main.rand.NextFloat() * 0.8f;
                    Main.dust[num110].position = Projectile.Center;
                    Main.dust[num110].velocity = vel;
                    Main.dust[num110].noGravity = true;
                }
            }
            SomeUtils.AddLight(Projectile.Center, color, 2 * ExplosionScale);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomFlare").Value;
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

        public static void Summon(NPC ship, Vector2 Pos, Color color, float explosionScale = 1f)
        {
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), Pos, Vector2.Zero, ModContent.ProjectileType<ShipExplosion>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as ShipExplosion).color = color;
                (Main.projectile[protmp].ModProjectile as ShipExplosion).ExplosionScale = explosionScale;
            }
        }
    }
}
