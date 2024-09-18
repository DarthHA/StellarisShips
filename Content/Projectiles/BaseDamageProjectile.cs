using StellarisShips.System;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public abstract class BaseDamageProjectile : ModProjectile
    {
        public string SourceName = "";
        public bool Crit = false;

        public sealed override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //用于伤害计数
            if (!BattleStatSystem.WeaponDamage.TryAdd(SourceName, damageDone))
            {
                BattleStatSystem.WeaponDamage[SourceName] += damageDone;
                if (SourceName == "") Main.NewText("未知武器：" + Lang.GetProjectileName(Projectile.type));
            }
            SafeOnHitNPC(target, hit, damageDone);
        }

        public sealed override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Crit)
            {
                modifiers.SetCrit();
            }
            else
            {
                modifiers.DisableCrit();
            }
            SafeModifyHitNPC(target, ref modifiers);
        }

        public virtual void SafeModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void SafeOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
    }
}
