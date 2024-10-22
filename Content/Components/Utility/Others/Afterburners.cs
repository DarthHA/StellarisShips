using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Others
{
    public abstract class BaseAfterburners : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Accessory;
        public sealed override string TypeName => "Afterburners";
        public sealed override string ExtraInfo => "A";
        public virtual float Evasion => 0;
        public virtual float Speed => 0;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonusEvasion(BonusID.Evasion, Evasion);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.SpeedMult, Speed);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Thruster"), Math.Round(Speed * 100, 1), Evasion);
        }
    }

    public class Afterburners1 : BaseAfterburners
    {
        public override int Level => 1;
        public override float Evasion => 5f;
        public override float Speed => 0.1f;
        public override long Value => 4 * 500;
        public override int Progress => 1;
    }

    public class Afterburners2 : BaseAfterburners
    {
        public override int Level => 2;
        public override float Evasion => 10f;
        public override float Speed => 0.15f;
        public override long Value => 6 * 800;
        public override int Progress => 5;
    }


}