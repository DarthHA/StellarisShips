using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class ShieldHardener1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "ShieldHardener";
        public override string ExtraInfo => "A";
        public virtual float DR => 0.05f;
        public override long Value => 10 * 600;
        public override int Progress => 5;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ShieldDR += DR;
            ship.GetShipNPC().ShieldDRLevel = Math.Max(ship.GetShipNPC().ShieldDRLevel, Level);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ShieldHardener"), Math.Round(DR * 100, 1));
        }
    }

    public class ShieldHardener2 : ShieldHardener1
    {
        public override int Level => 2;
        public override float DR => 0.075f;
        public override long Value => 25 * 600;
        public override int Progress => 8;
    }
}