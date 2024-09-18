using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Shield
{
    public class ShieldM1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 1;
        public override string TypeName => "Shield";
        public override string ExtraInfo => "SML";
        public virtual int Shield => 400;
        public virtual float ShieldRegen => 7.5f;
        public override long Value => 15 * 150;
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

    public class ShieldM2 : ShieldM1
    {
        public override int Level => 2;
        public override int Shield => 500;
        public override float ShieldRegen => 9.6f;
        public override long Value => (int)(19.5f * 150);
        public override int Progress => 3;
    }

    public class ShieldM3 : ShieldM1
    {
        public override int Level => 3;
        public override int Shield => 630;
        public override float ShieldRegen => 12.9f;
        public override long Value => 23 * 225;
        public override int Progress => 4;
    }

    public class ShieldM4 : ShieldM1
    {
        public override int Level => 4;
        public override int Shield => 830;
        public override float ShieldRegen => 16.5f;
        public override long Value => (int)(25.5f * 375);
        public override int Progress => 6;
    }

    public class ShieldM5 : ShieldM1
    {
        public override int Level => 5;
        public override int Shield => 1100;
        public override float ShieldRegen => 21.9f;
        public override long Value => 33 * 450;
        public override int Progress => 8;
    }

    public class ShieldM6 : ShieldM1
    {
        public override int Level => 6;
        public override int Shield => 1400;
        public override float ShieldRegen => 27.9f;
        public override long Value => (int)(43.5f * 600);
        public override int Progress => 9;
    }
}