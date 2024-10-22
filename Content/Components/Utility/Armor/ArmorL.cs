using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Armor
{
    public class ArmorL1 : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 1;
        public sealed override string TypeName => "Armor";
        public sealed override string ExtraInfo => "SML";
        public virtual int Defense => 12;
        public override long Value => 20 * 200;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.defense += Defense;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus").Value, Defense);
        }
    }

    public class ArmorL2 : ArmorL1
    {
        public override int Level => 2;
        public override int Defense => 24;
        public override long Value => 26 * 200;
        public override int Progress => 3;
    }
    public class ArmorL3 : ArmorL1
    {
        public override int Level => 3;
        public override int Defense => 36;
        public override long Value => 30 * 300;
        public override int Progress => 4;
    }
    public class ArmorL4 : ArmorL1
    {
        public override int Level => 4;
        public override int Defense => 48;
        public override long Value => 34 * 500;
        public override int Progress => 6;
    }
    public class ArmorL5 : ArmorL1
    {
        public override int Level => 5;
        public override int Defense => 66;
        public override long Value => 44 * 600;
        public override int Progress => 8;
    }
    public class ArmorL6 : ArmorL1
    {
        public override int Level => 6;
        public override int Defense => 84;
        public override long Value => 57 * 800;
        public override int Progress => 9;
        public override string SpecialUnLock => "DragonArmor";
    }
}