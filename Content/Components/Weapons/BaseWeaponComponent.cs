using Microsoft.Xna.Framework;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Weapons
{
    public abstract class BaseWeaponComponent : BaseComponent
    {
        public virtual int MaxDamage => 5;
        public virtual int MinDamage => 1;
        public virtual float Crit => 10;
        public virtual int AttackCD => 60;
        public virtual float MinRange => 0;
        public virtual float MaxRange => 100;

        //这些tag用来储存词条，这样就能实现加成效果
        public virtual List<string> DamageTag => new();
        public virtual List<string> AttackCDTag => new();
        public virtual List<string> CritTag => new();

        public float GetDamageBonus(Dictionary<string, float> bonusBuff)
        {
            float bonus = 1;
            foreach (string tag1 in bonusBuff.Keys)
            {
                foreach (string tag2 in DamageTag)
                {
                    if (tag1 == tag2)
                    {
                        bonus += bonusBuff[tag1];
                        break;
                    }
                }

            }
            return bonus;
        }

        public float GetAttackCDBonus(Dictionary<string, float> bonusBuff)
        {
            float bonus = 1;
            foreach (string tag1 in bonusBuff.Keys)
            {
                foreach (string tag2 in AttackCDTag)
                {
                    if (tag1 == tag2)
                    {
                        bonus += bonusBuff[tag1];
                        break;
                    }
                }

            }
            return bonus;
        }

        public float GetCritCDBonus(Dictionary<string, float> bonusBuff)
        {
            float bonus = 0;
            foreach (string tag1 in bonusBuff.Keys)
            {
                foreach (string tag2 in CritTag)
                {
                    if (tag1 == tag2)
                    {
                        bonus += bonusBuff[tag1];
                        break;
                    }
                }

            }
            return bonus;
        }

        public float DPS(Dictionary<string, float> bonusBuff)
        {
            float damageBonus = GetDamageBonus(bonusBuff);
            float attackCDBonus = GetAttackCDBonus(bonusBuff);
            float critBonus = GetCritCDBonus(bonusBuff);


            float BaseDmg = (MaxDamage + MinDamage) / 2f * damageBonus;
            int BaseAttackCD = (int)(AttackCD / attackCDBonus);
            float BaseCrit = MathHelper.Clamp(Crit + critBonus, 0, 100f);

            return BaseDmg * (1 + BaseCrit / 100f) * (60f / BaseAttackCD);
        }

        public float DPS()
        {
            return (MaxDamage + MinDamage) / 2f * (1 + Crit / 100f) * (60f / AttackCD);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.WeaponStat"), MinDamage, MaxDamage, Crit, AttackCD, MinRange, MaxRange, (float)Math.Round(DPS(), 1));
        }
    }
}