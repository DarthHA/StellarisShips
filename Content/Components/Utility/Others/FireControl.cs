using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class FireControl : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "FireControl";
        public override string ExtraInfo => "A";
        public override long Value => 10 * 600;
        public override int Progress => 4;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponCrit, 5f);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Sensor2"), 5f);
        }
    }
}