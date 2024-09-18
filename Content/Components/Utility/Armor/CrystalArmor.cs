using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Armor
{
    public class CrystalArmorS1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 1;
        public sealed override string TypeName => "CrystalArmor";
        public sealed override string ExtraInfo => "SML";
        public virtual int Hull => 260;
        public override long Value => 13 * 100;
        public override int Progress => 3;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.lifeMax += Hull;
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.HullPlus").Value, Hull);
        }
    }

    public class CrystalArmorS2 : CrystalArmorS1
    {
        public override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 2;
        public override int Hull => 440;
        public override long Value => 17 * 250;
        public override int Progress => 6;
    }

    public class CrystalArmorM1 : CrystalArmorS1
    {
        public override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 1;
        public override int Hull => 650;
        public override long Value => (int)(19.5f * 150);
        public override int Progress => 3;
    }

    public class CrystalArmorM2 : CrystalArmorS1
    {
        public override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 2;
        public override int Hull => 1100;
        public override long Value => (int)(25.5f * 375);
        public override int Progress => 6;
    }

    public class CrystalArmorL1 : CrystalArmorS1
    {
        public override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 1;
        public override int Hull => 1560;
        public override long Value => 26 * 200;
        public override int Progress => 3;
    }

    public class CrystalArmorL2 : CrystalArmorS1
    {
        public override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 2;
        public override int Hull => 2640;
        public override long Value => 34 * 500;
        public override int Progress => 6;
    }
}
