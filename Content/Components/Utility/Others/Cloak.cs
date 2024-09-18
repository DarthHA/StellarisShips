using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public abstract class BaseCloak : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Accessory;
        public sealed override string TypeName => "Cloak";
        public sealed override string ExtraInfo => "A";
        public virtual int Aggro => 200;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().Aggro -= Aggro;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Cloak"), Aggro);
        }
    }


    public class Cloak1 : BaseCloak
    {
        public override int Level => 1;
        public override int Aggro => 400;
        public override long Value => 10 * 400;
        public override int Progress => 1;
    }

    public class Cloak2 : BaseCloak
    {
        public override int Level => 2;
        public override int Aggro => 600;
        public override long Value => 15 * 500;
        public override int Progress => 4;
    }

    public class Cloak3 : BaseCloak
    {
        public override int Level => 3;
        public override int Aggro => 800;
        public override long Value => 20 * 600;
        public override int Progress => 7;
    }
    public class Cloak4 : BaseCloak
    {
        public override int Level => 4;
        public override int Aggro => 1000;
        public override long Value => 25 * 700;
        public override int Progress => 9;
    }

}