using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class ScourgeMissileProj : BaseDamageProjectile
    {
        public class ParticleUnit(Vector2 pos, float timeLeft)
        {
            public Vector2 Pos = pos;
            public float TimeLeft = timeLeft;
        }


        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CanDistortWater[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public List<ParticleUnit> particles = new();

        public Color color = Color.Orange;

        public float DetectRange = 600;
        public float HomingFactor = 0.05f;
        public float MaxSpeed = 10;
        public int Target = -1;
        public int LastTarget = -1;
        public float ExplosionScale = 1f;

        /// <summary>
        /// 重定向次数
        /// </summary>
        public int RedirectChance = 3;

        public override void SafeSetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 480;
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
                if (RedirectChance > 0)
                {
                    Target = TargetSelectHelper.GetClosestTarget(Projectile.Center, Projectile.Center, DetectRange, Target);
                }
                else
                {
                    Target = -1;
                }
                if (Target != -1)
                {
                    Vector2 TargetVec = Vector2.Normalize(Main.npc[Target].Center - Projectile.Center);
                    Projectile.velocity = Projectile.velocity * (1 - HomingFactor) + TargetVec * MaxSpeed * HomingFactor;
                    if (Projectile.velocity.Length() > MaxSpeed) Projectile.velocity = Vector2.Normalize(Projectile.velocity) * MaxSpeed;
                    if (LastTarget != Target)
                    {
                        RedirectChance--;
                    }
                    LastTarget = Target;
                }
                else
                {
                    RedirectChance = 0;
                    LastTarget = Target;
                    if (Projectile.timeLeft > 0) Projectile.timeLeft -= 1;
                }


                Vector2 Pos = Projectile.Center;
                particles.Add(new ParticleUnit(Pos + (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * Main.rand.NextFloat() * 2, 15));
                particles.Add(new ParticleUnit(Pos + (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * Main.rand.NextFloat() * 2 + Projectile.velocity * 0.5f, 15));


                for (int i = particles.Count - 1; i >= 0; i--)
                {
                    particles[i].TimeLeft--;
                    if (particles[i].TimeLeft <= 0)
                    {
                        particles.RemoveAt(i);
                    }
                }

                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.Opacity = MathHelper.Clamp(Projectile.Opacity + 0.3f, 0f, 1f);

                if (Projectile.timeLeft <= 2)
                {
                    Projectile.ai[0] = 1;
                    Projectile.timeLeft = 480;
                }
                SomeUtils.AddLight(Projectile.Center, color, 1);
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
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
                        int num110 = Dust.NewDust(Projectile.Center, 30, 30, 15, 0f, 0f, 0, Color.LightSeaGreen, 1f);    //31

                        Main.dust[num110].scale = 1f + Main.rand.NextFloat() * 0.4f;
                        Main.dust[num110].position = Projectile.Center;
                        Main.dust[num110].velocity = vel;
                        Main.dust[num110].noGravity = true;
                    }
                }
                SomeUtils.AddLight(Projectile.Center, color, 2 * ExplosionScale);
            }

            Vector2 Center = Projectile.Center;
            Projectile.width = (int)(12 * Projectile.scale);
            Projectile.height = (int)(12 * Projectile.scale);
            Projectile.Center = Center;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[0] == 0)
            {
                Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = new(Tex1.Width * 0.75f, Tex1.Height / 2f);
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    Vector2 scale = new Vector2(1.5f, 0.75f) * 0.12f * k;
                    Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, color * (1f - k) * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex1, Projectile.Center - Main.screenPosition, null, Color.White * Projectile.Opacity, Projectile.rotation, OffSet, Projectile.scale * new Vector2(1.5f, 0.75f) * 0.04f, SpriteEffects.FlipHorizontally, 0);

                foreach (ParticleUnit particle in particles)
                {
                    float scale = 0.045f * particle.TimeLeft / 15f;
                    Main.spriteBatch.Draw(Tex1, particle.Pos - Main.screenPosition, null, color, 0, Tex1.Size() / 2f, Projectile.scale * scale, SpriteEffects.None, 0);
                    Main.spriteBatch.Draw(Tex1, particle.Pos - Main.screenPosition, null, Color.White, 0, Tex1.Size() / 2f, Projectile.scale * scale * 0.8f, SpriteEffects.None, 0);
                }

                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
            else
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
            }
            return false;
        }


        public static int Summon(IEntitySource entitySource, Vector2 Pos, Vector2 velocity, int dmg, Color color, float explosionScale = 1f, float maxSpeed = 10, float detectRange = 1000, int target = -1, int timeLeft = 480, float homingFactor = 0.05f, bool crit = false, float kb = 0)
        {
            int protmp = Projectile.NewProjectile(entitySource, Pos, velocity, ModContent.ProjectileType<ScourgeMissileProj>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                Main.projectile[protmp].Center = Pos;
                Main.projectile[protmp].timeLeft = timeLeft;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).color = color;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).Crit = crit;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).DetectRange = detectRange;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).HomingFactor = homingFactor;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).MaxSpeed = maxSpeed;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).Target = target;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).LastTarget = target;
                (Main.projectile[protmp].ModProjectile as ScourgeMissileProj).ExplosionScale = explosionScale;
            }
            return protmp;
        }

        public override void SafeModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            float DamageBonus = 0.5f + 1.5f * target.life / target.lifeMax;
            if (target.IsBoss())
            {
                DamageBonus *= 1.2f;
            }
            modifiers.SourceDamage *= DamageBonus;
        }

        public override void SafeOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0)
            {
                SomeUtils.PlaySoundRandom(SoundPath.Hit + "Scourge", 1, Projectile.Center);
                Projectile.rotation = Main.rand.NextFloat() * MathHelper.TwoPi;
                Projectile.ai[0] = 1;
                Projectile.timeLeft = 480;
            }
        }
    }
}
