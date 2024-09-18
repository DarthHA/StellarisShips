using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class EEncoder : BaseComponent
    {
        public override string EquipType => ComponentTypes.Accessory;
        public override int Level => 1;
        public override string TypeName => "EEncoder";
        public override string ExtraInfo => "A";
        public float Evasion = 15f;
        public override long Value => 20 * 800;
        public override int Progress => 9;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().Evasion = (1 - (1 - ship.GetShipNPC().Evasion / 100f) * (1 - Evasion / 100f)) * 100f;
        }


        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.EEncoder"), Evasion);
        }
    }
}