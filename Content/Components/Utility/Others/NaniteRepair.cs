using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class NaniteRepair1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "NaniteRepair";
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

    public class NaniteRepair2 : NaniteRepair1
    {
        public override int Level => 2;
        public override long Value => 50 * 600;
        public override int Progress => 8;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.HullRegen, 0.015f);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.TissueRepair"), 1.5f);
        }
    }



}