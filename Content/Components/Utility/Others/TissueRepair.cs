using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class TissueRepair : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "TissueRepair";
        public override string ExtraInfo => "A";
        public override long Value => 20 * 600;
        public override int Progress => 3;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.HullRegen, 0.0075f);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.TissueRepair"), 0.75f);
        }
    }
}