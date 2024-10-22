using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class DamageProj : BaseDamageProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public float DefenseEffectiveness = 1f;

        private bool CanHit = true;

        public bool OneHit = true;

        public bool IgnoreSuperArmor = false;

        public int OriginalSourceDamage = 0;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CanDistortWater[Projectile.type] = true;
        }

        public override void SafeSetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.npcProj = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 99999;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public static int Summon(IEntitySource entitySource, Vector2 Pos, int dmg, bool crit = false, float defenseEffectiveness = 1f, float kb = 0, int width = 1, int height = 1, bool oneHit = true)
        {
            int protmp = Projectile.NewProjectile(entitySource, Pos, Vector2.Zero, ModContent.ProjectileType<DamageProj>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp < 1000)
            {
                Main.projectile[protmp].width = width;
                Main.projectile[protmp].height = height;
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as DamageProj).Crit = crit;
                (Main.projectile[protmp].ModProjectile as DamageProj).DefenseEffectiveness = defenseEffectiveness;
                (Main.projectile[protmp].ModProjectile as DamageProj).OneHit = oneHit;
                if (oneHit) Main.projectile[protmp].penetrate = 1;
            }
            return protmp;
        }

        public override void SafeModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (DefenseEffectiveness >= 0)
            {
                modifiers.DefenseEffectiveness *= DefenseEffectiveness;
            }
            else
            {
                if (modifiers.DefenseEffectiveness.Value > 0)
                {
                    modifiers.DefenseEffectiveness *= DefenseEffectiveness;
                }
            }

            if (IgnoreSuperArmor && modifiers.SuperArmor)
            {
                modifiers.ModifyHitInfo += KillSuperArmor;
            }

            void KillSuperArmor(ref NPC.HitInfo info)
            {
                int dmg = info.SourceDamage - (int)(9999f / 2f * DefenseEffectiveness);
                if (dmg < 1) dmg = 1;
                info.Damage = info.Crit ? dmg * 2 : dmg;
            }
        }



        public override void SafeOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (OneHit)
            {
                CanHit = false;
            }
        }

        public override bool? CanDamage()
        {
            if (!CanHit) return false;
            return null;
        }
    }
}
