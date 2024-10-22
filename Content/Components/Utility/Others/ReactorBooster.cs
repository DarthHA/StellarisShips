using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class ReactorBooster1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "ReactorBooster";
        public override string ExtraInfo => "A";
        public virtual float Damage => 0.025f;
        public virtual float Speed => 0.025f;

        public override long Value => 5 * 400;


        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.AllWeaponDamage, Damage);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.SpeedMult, Speed);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Reactor"), Math.Round(Damage * 100, 1), Math.Round(Speed * 100, 1));
        }
    }

    public class ReactorBooster2 : ReactorBooster1
    {
        public override int Level => 2;
        public override float Damage => 0.05f;
        public override float Speed => 0.05f;
        public override long Value => 10 * 600;
        public override int Progress => 4;
    }

    public class ReactorBooster3 : ReactorBooster1
    {
        public override int Level => 3;
        public override float Damage => 0.075f;
        public override float Speed => 0.075f;
        public override long Value => 15 * 600;
        public override int Progress => 7;
    }
}