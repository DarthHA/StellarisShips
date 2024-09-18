using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerCarrier1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerCarrier";
        public virtual int ExtraStrikers => 1;
        public virtual float DamageH => 0.3f;
        public virtual int Aggro => 600;
        public override long Value => 0 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().ExtraStriker += ExtraStrikers;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.WeaponDamage_H, DamageH);
            ship.GetShipNPC().Aggro -= Aggro;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerCarrier"),
               ExtraStrikers, Math.Round(DamageH * 100f, 1), Aggro);
        }
    }

    public class ComputerCarrier2 : ComputerCarrier1
    {
        public override int Level => 2;
        public override int ExtraStrikers => 1;
        public override float DamageH => 0.6f;
        public override int Aggro => 700;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerCarrier3 : ComputerCarrier1
    {
        public override int Level => 3;
        public override int ExtraStrikers => 2;
        public override float DamageH => 0.8f;
        public override int Aggro => 800;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerCarrier4 : ComputerCarrier1
    {
        public override int Level => 4;
        public override int ExtraStrikers => 3;
        public override float DamageH => 1f;
        public override int Aggro => 900;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }

    public class ComputerCarrier5 : ComputerCarrier1
    {
        public override int Level => 5;
        public override int ExtraStrikers => 4;
        public override float DamageH => 1.2f;
        public override int Aggro => 1200;
        public override long Value => 40 * 200;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiComputer";
    }
}