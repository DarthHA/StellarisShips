using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class ShieldRecharger : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "ShieldRecharger";
        public override string ExtraInfo => "A";
        public override long Value => 15 * 600;
        public override int Progress => 3;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.ShieldMult, 0.1f);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ShieldRecharger"), 10f);
        }
    }
}