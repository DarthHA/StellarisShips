using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public class Taunt : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Accessory;
        public sealed override string TypeName => "Taunt";
        public sealed override string ExtraInfo => "A";
        public override int Level => 1;
        public virtual int Aggro => 500;
        public override long Value => 10 * 400;
        public override int Progress => 1;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().Aggro += Aggro;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Taunt"), Aggro);
        }
    }
}