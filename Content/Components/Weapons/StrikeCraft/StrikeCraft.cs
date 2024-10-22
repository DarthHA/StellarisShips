using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Weapons.StrikeCraft
{
    public class StrikeCraft1 : BaseWeaponComponent          //DPS:160
    {
        public override string EquipType => ComponentTypes.Weapon_H;
        public override int Level => 1;
        public override string TypeName => "StrikeCraft";
        public override string ExtraInfo => "H";

        public override int AttackCD => 30;
        public override float Crit => 20;
        public override int MaxDamage => 90;
        public override int MinDamage => 70;
        public override float MaxRange => 1000;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_H };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_H };

        public override long Value => 68 * 400;
        public override int Progress => 4;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "";
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.WeaponStatStriker"), MinDamage, MaxDamage, Crit, AttackCD, (float)Math.Round(DPS(), 1));
        }
    }

    public class StrikeCraft2 : StrikeCraft1          //DPS:240
    {
        public override int Level => 2;
        public override int MaxDamage => 150;
        public override int MinDamage => 90;
        public override long Value => 78 * 600;
        public override int Progress => 6;
    }

    public class StrikeCraft3 : StrikeCraft1          //DPS:320
    {
        public override int Level => 3;
        public override int MaxDamage => 180;
        public override int MinDamage => 140;
        public override long Value => 88 * 800;
        public override int Progress => 8;
    }

    public class StrikeCraft4 : StrikeCraft1          //DPS:320
    {
        public override int Level => 4;
        public override int MaxDamage => 225;
        public override int MinDamage => 161;
        public override long Value => 93 * 1000;
        public override int Progress => 9;
        public override string SpecialUnLock => "SkrandStrikeCraft";
    }
}