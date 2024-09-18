using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core.Computer
{
    public class ComputerLine1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Computer;
        public override int Level => 1;
        public override string TypeName => "ComputerLine";
        public virtual float Crit => 5f;
        public virtual float AttackSpeed => 0.05f;
        public override long Value => 0 * 200;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().ComputerType = TypeName;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponAttackCD, AttackSpeed);
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.AllWeaponCrit, Crit);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.ComputerLine"),
               Math.Round(AttackSpeed * 100f, 1), Crit);
        }
    }

    public class ComputerLine2 : ComputerLine1
    {
        public override int Level => 2;
        public override float Crit => 10f;
        public override float AttackSpeed => 0.1f;
        public override long Value => 5 * 200;
        public override int Progress => 3;
    }

    public class ComputerLine3 : ComputerLine1
    {
        public override int Level => 3;
        public override float Crit => 15f;
        public override float AttackSpeed => 0.15f;
        public override long Value => 10 * 200;
        public override int Progress => 5;
    }

    public class ComputerLine4 : ComputerLine1
    {
        public override int Level => 4;
        public override float Crit => 20f;
        public override float AttackSpeed => 0.2f;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }

    public class ComputerLine5 : ComputerLine1
    {
        public override int Level => 5;
        public override float Crit => 25f;
        public override float AttackSpeed => 0.25f;
        public override long Value => 40 * 200;
        public override int Progress => 9;
        public override string SpecialUnLock => "PsiComputer";
    }
}