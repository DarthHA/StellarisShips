using StellarisShips.Content.Items;
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
        public virtual float HullRegen => 0.0075f;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.HullRegen, HullRegen);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.TissueRepair"), HullRegen * 100f);
        }
    }

    public class NaniteRepair2 : NaniteRepair1
    {
        public override int Level => 2;
        public override long Value => 50 * 600;
        public override int Progress => 8;
        public override float HullRegen => 0.015f;
    }

    public class NaniteRepair3 : NaniteRepair1
    {
        public override int Level => 3;
        public override long Value => 100 * 600;
        public override int Progress => 9;
        public override float HullRegen => 0.0225f;
        public override string SpecialUnLock => "CetanaNanoRepair";
    }

}