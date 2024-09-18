using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Shield
{
    public class ShieldL1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 1;
        public override string TypeName => "Shield";
        public override string ExtraInfo => "SML";
        public virtual int Shield => 900;
        public virtual float ShieldRegen => 18;
        public override long Value => 20 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.ShieldPlus").Value, Shield, ShieldRegen);
        }
    }

    public class ShieldL2 : ShieldL1
    {
        public override int Level => 2;
        public override int Shield => 1200;
        public override float ShieldRegen => 24.3f;
        public override long Value => 26 * 200;
        public override int Progress => 3;
    }

    public class ShieldL3 : ShieldL1
    {
        public override int Level => 3;
        public override int Shield => 1500;
        public override float ShieldRegen => 30.6f;
        public override long Value => 30 * 300;
        public override int Progress => 4;
    }

    public class ShieldL4 : ShieldL1
    {
        public override int Level => 4;
        public override int Shield => 1980;
        public override float ShieldRegen => 39.6f;
        public override long Value => 34 * 500;
        public override int Progress => 6;
    }

    public class ShieldL5 : ShieldL1
    {
        public override int Level => 5;
        public override int Shield => 2640;
        public override float ShieldRegen => 16.6f;
        public override long Value => 44 * 600;
        public override int Progress => 8;
    }

    public class ShieldL6 : ShieldL1
    {
        public override int Level => 6;
        public override int Shield => 3360;
        public override float ShieldRegen => 66.6f;
        public override long Value => 57 * 800;
        public override int Progress => 9;
    }
}