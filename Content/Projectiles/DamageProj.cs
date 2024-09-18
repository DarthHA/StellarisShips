using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public class DamageProj : ModProjectile
    {
        public override string Texture => "StellarisShips/Images/PlaceHolder";

        public bool Crit = false;
        public float DefenseEffectiveness = 1f;

        private bool CanHit = true;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CanDistortWater[Projectile.type] = true;
        }

        public override void SetDefaults()
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
            Projectile.localNPCHitCooldown = 10;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public static void Summon(IEntitySource entitySource, Vector2 Pos, int dmg, bool crit = false, float defenseEffectiveness = 1f, float kb = 0, int width = 1, int height = 1)
        {
            int protmp = Projectile.NewProjectile(entitySource, Pos, Vector2.Zero, ModContent.ProjectileType<DamageProj>(), dmg, kb, Main.myPlayer);
            if (protmp >= 0 && protmp <= 1000)
            {
                Main.projectile[protmp].width = width;
                Main.projectile[protmp].height = height;
                Main.projectile[protmp].Center = Pos;
                (Main.projectile[protmp].ModProjectile as DamageProj).Crit = crit;
                (Main.projectile[protmp].ModProjectile as DamageProj).DefenseEffectiveness = defenseEffectiveness;
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

            modifiers.DefenseEffectiveness *= DefenseEffectiveness;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            CanHit = false;
        }

        public override bool? CanDamage()
        {
            if (!CanHit) return false;
            return null;
        }
    }
}
