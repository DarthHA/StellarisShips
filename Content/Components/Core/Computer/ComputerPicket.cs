using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerPicket1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerPicket";
        public virtual float Crit => 2.5f;
        public virtual float AttackSpeed => 0.025f;
        public virtual float Evasion => 2.5f;
        public virtual int Aggro => 400;
        public override long Value => 0 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponAttackCD, AttackSpeed);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponCrit, Crit);
            ship.GetShipNPC().Evasion = (1 - (1 - ship.GetShipNPC().Evasion / 100f) * (1 - Evasion / 100f)) * 100f;
            ship.GetShipNPC().Aggro += Aggro;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerPicket"),
               Math.Round(AttackSpeed * 100f, 1), Crit, Math.Round(Evasion, 1), Aggro);
        }
    }

    public class ComputerPicket2 : ComputerPicket1
    {
        public override int Level => 2;
        public override float Crit => 5f;
        public override float AttackSpeed => 0.05f;
        public override float Evasion => 5f;
        public override int Aggro => 500;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerPicket3 : ComputerPicket1
    {
        public override int Level => 3;
        public override float Crit => 10f;
        public override float AttackSpeed => 0.1f;
        public override float Evasion => 10f;
        public override int Aggro => 600;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerPicket4 : ComputerPicket1
    {
        public override int Level => 4;
        public override float Crit => 15f;
        public override float AttackSpeed => 0.15f;
        public override float Evasion => 15f;
        public override int Aggro => 700;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }
}