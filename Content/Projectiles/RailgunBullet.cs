using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class RailgunBullet : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public bool Crit = false;
        public float ModifiedScale = 1f;
        public Color color = Color.White;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CanDistortWater[Projectile.type] = true;
        }


        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 100;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 999;
            Projectile.penetrate = -1;
            Projectile.npcProj = true;
            Projectile.Opacity = 0;
        }

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.Opacity = MathHelper.Clamp(Projectile.Opacity + 0.2f, 0f, 1f);
                SomeUtils.AddLightLine(Projectile.Center - Projectile.rotation.ToRotationVector2() * 40 * ModifiedScale, Projectile.Center + Projectile.rotation.ToRotationVector2() * 10 * ModifiedScale, color);
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.ai[1]++;
                if (Projectile.ai[1] < 5)
                {
                    Projectile.scale = 0.5f + Projectile.ai[1] / 5f * 1.5f;
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
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 vel = (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * (Main.rand.NextFloat() * 2f + 1f);
                        int num110 = Dust.NewDust(Projectile.Center, 0, 0, 303, 0f, 0f, 0, color, 1f);    //73?
                        Main.dust[num110].scale = 1f + Main.rand.NextFloat() * 0.8f;
                        Main.dust[num110].position = Projectile.Center;
                        Main.dust[num110].velocity = vel;
                        Main.dust[num110].noGravity = true;
                    }
                }
                SomeUtils.AddLight(Projectile.Center, color, 1);
            }

            Vector2 Center = Projectile.Center;
            Projectile.width = (int)(12 * Projectile.scale * ModifiedScale);
            Projectile.height = (int)(12 * Projectile.scale * ModifiedScale);
            Projectile.Center = Center;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] == 0)
            {
                Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BlobGlow").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = new(Tex1.Width * 0.85f, Tex1.Height / 2f);
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    Vector2 scale = new Vector2(4f, 0.3f) * 0.15f * k * ModifiedScale;
                    Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * new Vector2(4f, 0.3f) * 0.05f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
            else
            {
                Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex1.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.15f * k * ModifiedScale;
                    Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * 0.05f * ModifiedScale, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
            return false;
        }


        public static void Summon(IEntitySource entitySource, Vector2 Pos, Vector2 velocity, int dmg, Color color, float scale, bool crit = false, float kb = 0)
        {
            int protmp = Projectile.NewProjectile(entitySource, Pos, velocity, ModContent.ProjectileType<RailgunBullet>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp <= 1000)
            {
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as RailgunBullet).color = color;
                (Main.projectile[protmp].ModProjectile as RailgunBullet).ModifiedScale = scale;
                (Main.projectile[protmp].ModProjectile as RailgunBullet).Crit = crit;
            }
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
            modifiers.DefenseEffectiveness *= 0.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0)
            {
                SomeUtils.PlaySoundRandom(SoundPath.Hit + "Railgun", 2, Projectile.Center);
                Projectile.ai[0] = 1;
                Projectile.timeLeft = 240;
            }
        }

        public override bool? CanDamage()
        {
            if (Projectile.ai[0] == 1) return false;
            return null;
        }
    }
}
