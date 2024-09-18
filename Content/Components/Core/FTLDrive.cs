using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core
{
    public class FTLDrive1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.FTLDrive;
        public override int Level => 1;
        public override string TypeName => "FTLDrive";
        public override long Value => 0 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().FTLLevel = Level;
        }

        public override void ModifyDesc(ref string desc)
        {
            if (Level > 1)
            {
                desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.FTL"), Level * 30);
            }
        }
    }

    public class FTLDrive2 : FTLDrive1
    {
        public override int Level => 2;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class FTLDrive3 : FTLDrive1
    {
        public override int Level => 3;
        public override long Value => 10 * 200;
        public override int Progress => 4;
    }

    public class FTLDrive4 : FTLDrive1
    {
        public override int Level => 4;
        public override long Value => 15 * 200;
        public override int Progress => 6;
    }

    public class FTLDrive5 : FTLDrive1
    {
        public override int Level => 5;
        public override long Value => 20 * 200;
        public override int Progress => 8;
    }
}