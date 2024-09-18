using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class ShieldHitEffect : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public int ownerID = -1;
        public float ShieldRadius = 100;        //1:0.6
        public float Rot = 0;
        public float Radius2 = 0.9f;
        public string Upgrade = "";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 1000;
        }
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
            if (ownerID == -1)
            {
                Projectile.Kill();
                return;
            }

            if (!Main.npc[ownerID].active || Main.npc[ownerID].type != ModContent.NPCType<ShipNPC>())
            {
                Projectile.Kill();
                return;
            }

            if (!Main.npc[ownerID].ShipActive())
            {
                Projectile.Kill();
                return;
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] > 20) Projectile.Kill();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public static void Summon(NPC ship, Vector2 Pos, float radius, float rot, string upgrade = "")
        {
            int protmp = Projectile.NewProjectile(ship.GetSource_FromAI(), Pos, Vector2.Zero, ModContent.ProjectileType<ShieldHitEffect>(), 0, 0, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                (Main.projectile[protmp].ModProjectile as ShieldHitEffect).ownerID = ship.whoAmI;
                (Main.projectile[protmp].ModProjectile as ShieldHitEffect).ShieldRadius = radius;
                (Main.projectile[protmp].ModProjectile as ShieldHitEffect).Rot = rot;
                (Main.projectile[protmp].ModProjectile as ShieldHitEffect).Radius2 = 0.8f + 0.2f * Main.rand.NextFloat();
                (Main.projectile[protmp].ModProjectile as ShieldHitEffect).Upgrade = upgrade;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float alpha = MathHelper.Clamp((20 - Projectile.ai[0]) / 10f, 0f, 1f);

            float realRadius = ShieldRadius / 0.6f;
            float k = realRadius / 600f;
            float r1 = 0.2f * k * Projectile.ai[0] / 20f;
            Vector2 hitpos = Rot.ToRotationVector2() * Radius2;
            DrawAll(Projectile.Center, hitpos, r1, 0.1f * k, Color.White * alpha);
            return false;
        }


        private void DrawAll(Vector2 DrawPos, Vector2 HitPos, float r1, float r2, Color color)
        {
            float realRadius = ShieldRadius / 0.6f;
            float k = realRadius / 600f;
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/ShieldEffect" + Upgrade, AssetRequestMode.ImmediateLoad).Value;
            EasyDraw.AnotherDraw(SpriteSortMode.Immediate, BlendState.Additive);
            StellarisShips.HitEffect.Parameters["color"].SetValue(color.ToVector4());
            StellarisShips.HitEffect.Parameters["center"].SetValue(new Vector2(0.5f, 0.5f) + HitPos * k * 0.5f);
            StellarisShips.HitEffect.Parameters["r1"].SetValue(r1);
            StellarisShips.HitEffect.Parameters["r2"].SetValue(r2);
            StellarisShips.HitEffect.CurrentTechnique.Passes[0].Apply();
            Main.spriteBatch.Draw(tex, DrawPos - Main.screenPosition, null, Color.White, 0, tex.Size() / 2f, realRadius / 300f, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        }
    }
}
