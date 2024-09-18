using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class HealEffect : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] <= 10) Projectile.scale = Projectile.ai[0] / 10f;
            if (Projectile.ai[0] > 40) { Projectile.Opacity = (50 - Projectile.ai[0]) / 10f; Projectile.velocity *= 0.99f; }
            if (Projectile.ai[0] > 50) Projectile.Kill();
        }

        public static void Summon(IEntitySource entitySource, Vector2 Pos, float VelY)
        {
            Projectile.NewProjectile(entitySource, Pos, new Vector2(0, -VelY), ModContent.ProjectileType<HealEffect>(), 0, 0, Main.myPlayer);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/Common/HealEffect", AssetRequestMode.ImmediateLoad).Value;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White * 0.9f * Projectile.Opacity, 0, tex.Size() / 2, Projectile.scale * 0.5f, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            return false;
        }

    }
}
