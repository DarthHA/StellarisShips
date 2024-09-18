using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class ArmorHardener1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "ArmorHardener";
        public override string ExtraInfo => "A";
        public virtual int MaxImmuneTime => 4;
        public override long Value => 10 * 600;
        public override int Progress => 5;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().MaxImmuneTime += MaxImmuneTime;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ArmorHardener"), MaxImmuneTime);
        }
    }

    public class ArmorHardener2 : ArmorHardener1
    {
        public override int Level => 2;
        public override int MaxImmuneTime => 8;
        public override long Value => 25 * 600;
        public override int Progress => 8;
    }
}