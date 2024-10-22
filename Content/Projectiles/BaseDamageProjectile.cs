using StellarisShips.Content.Items;
using StellarisShips.Static;
using StellarisShips.System;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Projectiles
{
    public abstract class BaseDamageProjectile : ModProjectile
    {
        public string SourceName = "";
        public bool Crit = false;
        public string SourceShipName = "";
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_Parent)
            {
                EntitySource_Parent parentSource = source as EntitySource_Parent;
                if (parentSource.Entity is NPC && (parentSource.Entity as NPC).ShipActive(true))
                {
                    SourceShipName = (parentSource.Entity as NPC).GetShipNPC().ShipName;
                }
                else if (parentSource.Entity is Projectile)
                {
                    Projectile proj = parentSource.Entity as Projectile;
                    if (proj.ModProjectile != null && proj.ModProjectile is BaseDamageProjectile)
                    {
                        SourceShipName = (proj.ModProjectile as BaseDamageProjectile).SourceShipName;
                    }
                }

            }
        }

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

            if (target.life <= damageDone)
            {
                foreach (NPC npc in Main.ActiveNPCs)
                {
                    if (npc.ShipActive(true))
                    {
                        if (npc.GetShipNPC().ShipName == SourceShipName)
                        {
                            float vampireHeal = npc.GetShipNPC().BonusBuff.GetBonus(BonusID.VampireHeal) * (npc.lifeMax - npc.GetShipNPC().MaxShield);
                            if (vampireHeal > 0)
                            {
                                npc.GetShipNPC().HealHull((int)vampireHeal);
                            }
                        }
                    }
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

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ShipActive(true))
                {
                    if (npc.GetShipNPC().ShipName == SourceShipName)
                    {
                        float m = 1f;
                        if (target.IsBoss())
                        {
                            m += npc.GetShipNPC().BonusBuff.GetBonus(BonusID.DamageToBoss);
                        }
                        else
                        {
                            m += npc.GetShipNPC().BonusBuff.GetBonus(BonusID.DamageToNonBoss);
                        }
                        if (NPCID.Sets.BelongsToInvasionOldOnesArmy[target.type])
                        {
                            m += npc.GetShipNPC().BonusBuff.GetBonus(BonusID.DamageToOldOnes);
                        }
                        if(Main.LocalPlayer.ZoneGlowshroom && Main.LocalPlayer.ZoneOverworldHeight)
                        {
                            m+= npc.GetShipNPC().BonusBuff.GetBonus(BonusID.DamageInMushroom);
                        }
                        modifiers.FinalDamage *= m;
                    }
                }
            }

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
