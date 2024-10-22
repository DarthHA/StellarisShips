using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Armor
{
    public class ArmorM1 : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 1;
        public sealed override string TypeName => "Armor";
        public sealed override string ExtraInfo => "SML";
        public virtual int Defense => 10;
        public override long Value => 15 * 150;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.Defense, Defense);
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus").Value, Defense);
        }
    }

    public class ArmorM2 : ArmorM1
    {
        public override int Level => 2;
        public override int Defense => 20;
        public override long Value => (int)(19.5f * 150);
        public override int Progress => 3;
    }
    public class ArmorM3 : ArmorM1
    {
        public override int Level => 3;
        public override int Defense => 30;
        public override long Value => 23 * 225;
        public override int Progress => 4;
    }
    public class ArmorM4 : ArmorM1
    {
        public override int Level => 4;
        public override int Defense => 40;
        public override long Value => (int)(25.5f * 375);
        public override int Progress => 6;
    }
    public class ArmorM5 : ArmorM1
    {
        public override int Level => 5;
        public override int Defense => 55;
        public override long Value => 33 * 450;
        public override int Progress => 8;
    }
    public class ArmorM6 : ArmorM1
    {
        public override int Level => 6;
        public override int Defense => 70;
        public override long Value => (int)(43.5f * 600);
        public override int Progress => 9;
        public override string SpecialUnLock => "DragonArmor";
    }
}