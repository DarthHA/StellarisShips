using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerSwarm1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerSwarm";
        public virtual float AttackSpeed => 0.1f;
        public virtual float Evasion => 5f;
        public virtual float Speed => 0.05f;
        public override long Value => 0 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponAttackCD, AttackSpeed);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.Speed, Speed);
            ship.GetShipNPC().Evasion = (1 - (1 - ship.GetShipNPC().Evasion / 100f) * (1 - Evasion / 100f)) * 100f;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerSwarm"),
               Math.Round(AttackSpeed * 100f, 1), Math.Round(Evasion, 1), Math.Round(Speed * 100f, 1));
        }
    }

    public class ComputerSwarm2 : ComputerSwarm1
    {
        public override int Level => 2;
        public override float AttackSpeed => 0.15f;
        public override float Evasion => 10f;
        public override float Speed => 0.1f;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerSwarm3 : ComputerSwarm1
    {
        public override int Level => 3;
        public override float AttackSpeed => 0.20f;
        public override float Evasion => 15f;
        public override float Speed => 0.15f;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerSwarm4 : ComputerSwarm1
    {
        public override int Level => 4;
        public override float AttackSpeed => 0.25f;
        public override float Evasion => 20f;
        public override float Speed => 0.2f;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }

    public class ComputerSwarm5 : ComputerSwarm1
    {
        public override int Level => 5;
        public override float AttackSpeed => 0.3f;
        public override float Evasion => 25f;
        public override float Speed => 0.25f;
        public override long Value => 40 * 200;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiComputer";
    }
}