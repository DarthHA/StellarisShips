using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class EDecoder : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "EDecoder";
        public override string ExtraInfo => "A";
        public override long Value => 20 * 800;
        public override int Progress => 9;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponCrit, 10f);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Sensor2"), 10f);
        }
    }
}