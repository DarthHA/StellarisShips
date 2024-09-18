using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Armor
{
    public class ArmorS1 : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 1;
        public sealed override string TypeName => "Armor";
        public sealed override string ExtraInfo => "SML";
        public virtual int Defense => 8;
        public override long Value => 10 * 100;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.defense += Defense;
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus").Value, Defense);
        }
    }

    public class ArmorS2 : ArmorS1
    {
        public override int Level => 2;
        public override int Defense => 16;
        public override long Value => 13 * 100;
        public override int Progress => 3;
    }
    public class ArmorS3 : ArmorS1
    {
        public override int Level => 3;
        public override int Defense => 24;
        public override long Value => 15 * 150;
        public override int Progress => 4;
    }
    public class ArmorS4 : ArmorS1
    {
        public override int Level => 4;
        public override int Defense => 32;
        public override long Value => 17 * 250;
        public override int Progress => 6;
    }
    public class ArmorS5 : ArmorS1
    {
        public override int Level => 5;
        public override int Defense => 40;
        public override long Value => 22 * 300;
        public override int Progress => 8;
    }
    public class ArmorS6 : ArmorS1
    {
        public override int Level => 6;
        public override int Defense => 48;
        public override long Value => 29 * 400;
        public override int Progress => 9;
    }
}