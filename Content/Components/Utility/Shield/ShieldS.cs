using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Shield
{
    public class ShieldS1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 1;
        public override string TypeName => "Shield";
        public override string ExtraInfo => "SML";
        public virtual int Shield => 150;
        public virtual float ShieldRegen => 3;
        public override long Value => 10 * 100;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.Shield, Shield);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.ShieldRegen, ShieldRegen);
        }
        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.ShieldPlus").Value, Shield, ShieldRegen);
        }
    }

    public class ShieldS2 : ShieldS1
    {
        public override int Level => 2;
        public override int Shield => 200;
        public override float ShieldRegen => 3.9f;
        public override long Value => 13 * 100;
        public override int Progress => 3;
    }

    public class ShieldS3 : ShieldS1
    {
        public override int Level => 3;
        public override int Shield => 250;
        public override float ShieldRegen => 5.2f;
        public override long Value => 15 * 150;
        public override int Progress => 4;
    }

    public class ShieldS4 : ShieldS1
    {
        public override int Level => 4;
        public override int Shield => 330;
        public override float ShieldRegen => 6.6f;
        public override long Value => 17 * 250;
        public override int Progress => 6;
    }

    public class ShieldS5 : ShieldS1
    {
        public override int Level => 5;
        public override int Shield => 440;
        public override float ShieldRegen => 8.7f;
        public override long Value => 22 * 300;
        public override int Progress => 8;
    }

    public class ShieldS6 : ShieldS1
    {
        public override int Level => 6;
        public override int Shield => 560;
        public override float ShieldRegen => 11.1f;
        public override long Value => 29 * 400;
        public override int Progress => 9;
    }

    public class ShieldS7 : ShieldS1
    {
        public override int Level => 7;
        public override int Shield => 720;
        public override float ShieldRegen => 12.4f;
        public override long Value => 38 * 600;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiShield";
    }
}