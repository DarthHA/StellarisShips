using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Weapons.Ancient
{
    public class ArchStrikeCraft : BaseWeaponComponent          //DPS:160
    {
        public override string EquipType => ComponentTypes.Weapon_H;
        public override int Level => 1;
        public override string TypeName => "ArchStrikeCraft";
        public override string ExtraInfo => "H";

        public override int AttackCD => 30;
        public override float Crit => 20;
        public override int MaxDamage => 300;
        public override int MinDamage => 250;
        public override float MaxRange => 1000;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_H };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_H };

        public override long Value => 106 * 1000;
        public override int MRValue => 120;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.WeaponStatStriker"), MinDamage, MaxDamage, Crit, AttackCD, (float)Math.Round(DPS(), 1));
        }
    }
}
