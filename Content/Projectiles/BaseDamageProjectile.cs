using StellarisShips.Content.Items;
using StellarisShips.System;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public abstract class BaseDamageProjectile : ModProjectile
    {
        public string SourceName = "";
        public bool Crit = false;

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<ShipEmblem>()))
            {
                Projectile.npcProj = true;
                Projectile.DamageType = DamageClass.Summon;
            }
        }

        public sealed override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //用于伤害计数
            if (BattleStatSystem.BossBattleActive)
            {
                if (!BattleStatSystem.WeaponDamage.TryAdd(SourceName, damageDone))
                {
                    BattleStatSystem.WeaponDamage[SourceName] += damageDone;
                    if (SourceName == "") Main.NewText("未知武器：" + Lang.GetProjectileName(Projectile.type));
                }
            }

            //威慑值
            if (!ProgressHelper.FirstContractShroud)
            {
                ProgressHelper.PsychoPower += Math.Min(damageDone, target.lifeMax);
                if (ProgressHelper.PsychoPower > ProgressHelper.GetMaxPsychoPower()) ProgressHelper.PsychoPower = ProgressHelper.GetMaxPsychoPower();
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
            modifiers.ModifyHitInfo += (ref NPC.HitInfo info) =>
            {
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<ShipEmblem>()))
                {
                    info.Damage = (int)Main.LocalPlayer.GetTotalDamage(DamageClass.Summon).ApplyTo(info.Damage);
                }
            };
        }

        public virtual void SafeModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void SafeOnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public virtual void SafeSetDefaults()
        {

        }


    }
}
