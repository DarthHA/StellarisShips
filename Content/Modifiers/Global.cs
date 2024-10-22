using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers
{
    public class Modifier_QuantumDestabilizer : BaseModifier
    {
        public override string ID => ModifierID.QuantumDestabilizer;
    }

    public class Modifier_ShieldDampener : BaseModifier
    {
        public override string ID => ModifierID.ShieldDampener;
    }

    public class Modifier_SubspaceSnare : BaseModifier
    {
        public override string ID => ModifierID.SubspaceSnare;
    }

    public class Modifier_InspiringPresence : BaseModifier
    {
        public override string ID => ModifierID.InspiringPresence;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.15f);
        }
    }

    public class Modifier_AncientTargetScrambler : BaseModifier
    {
        public override string ID => ModifierID.AncientTargetScrambler;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonusEvasion(BonusID.Evasion, 40f);
        }
    }

    public class Modifier_NanobotCloud : BaseModifier
    {
        public override string ID => ModifierID.NanobotCloud;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.02f);
        }
    }

    public class Modifier_TargetingGrid : BaseModifier
    {
        public override string ID => ModifierID.TargetingGrid;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponCrit, 15f);
        }
    }

    public class Modifier_ShroudASPDUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudASPDUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.25f);
        }
    }

    public class Modifier_ShroudAtkUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudAtkUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.25f);
        }
    }

    public class Modifier_ShroudRegenUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudRegenUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.03f);
        }
    }

    public class Modifier_ShroudShieldUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudShieldUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.ShieldMult, 0.4f);
        }
    }

    public class Modifier_ShroudSpeedUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudSpeedUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, 0.25f);
        }
    }

    public class Modifier_ShroudEvasionUp : BaseModifier
    {
        public override string ID => ModifierID.ShroudEvasionUp;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonusEvasion(BonusID.Evasion, 30f);
        }
    }
}
