using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers
{
    public class QuantumDestabilizer : BaseModifier
    {
        public override string ID =>  ModifierID.QuantumDestabilizer;
    }

    public class ShieldDampener : BaseModifier
    {
        public override string ID => ModifierID.ShieldDampener;
    }

    public class SubspaceSnare : BaseModifier
    {
        public override string ID => ModifierID.SubspaceSnare;
    }

    public class InspiringPresence : BaseModifier
    {
        public override string ID => ModifierID.InspiringPresence;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.15f);
        }
    }

    public class AncientTargetScrambler : BaseModifier
    {
        public override string ID => ModifierID.AncientTargetScrambler;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonusEvasion(BonusID.Evasion, 40f);
        }
    }

    public class NanobotCloud : BaseModifier
    {
        public override string ID => ModifierID.NanobotCloud;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.02f);
        }
    }

    public class TargetingGrid : BaseModifier
    {
        public override string ID => ModifierID.TargetingGrid;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponCrit, 15f);
        }
    }

    public class ShroudASPDUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudASPDUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.25f);
        }
    }

    public class ShroudAtkUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudAtkUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.25f);
        }
    }

    public class ShroudRegenUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudRegenUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.03f);
        }
    }

    public class ShroudShieldUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudShieldUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.ShieldMult, 0.4f);
        }
    }

    public class ShroudSpeedUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudSpeedUp;
    }


}
