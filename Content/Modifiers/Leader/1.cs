using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers.Leader
{
    public class Modifier_Aggressiveness : BaseModifier
    {
        public override string ID => ModifierID.Aggressiveness;
        public override List<string> ConflictModifiers => new() { ModifierID.Aggressiveness2, ModifierID.Prudence, ModifierID.Prudence2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.1f);
        }
    }
    public class Modifier_Aggressiveness2 : BaseModifier
    {
        public override string ID => ModifierID.Aggressiveness2;
        public override List<string> ConflictModifiers => new() { ModifierID.Aggressiveness, ModifierID.Prudence, ModifierID.Prudence2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.1f);
            effects.AddBonus(BonusID.AllWeaponDamage, 0.1f);
        }
    }
    public class Modifier_Prudence : BaseModifier
    {
        public override string ID => ModifierID.Prudence;
        public override List<string> ConflictModifiers => new() { ModifierID.Prudence2, ModifierID.Aggressiveness, ModifierID.Aggressiveness2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponRange, 0.2f);
        }
    }
    public class Modifier_Prudence2 : BaseModifier
    {
        public override string ID => ModifierID.Prudence2;
        public override List<string> ConflictModifiers => new() { ModifierID.Prudence, ModifierID.Aggressiveness, ModifierID.Aggressiveness2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponRange, 0.4f);
            effects.AddBonus(BonusID.EscapeChance, 0.1f);
        }
    }
    public class Modifier_Engineer : BaseModifier
    {
        public override string ID => ModifierID.Engineer;
        public override List<string> ConflictModifiers => new() { ModifierID.Engineer2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.01f);
        }
    }
    public class Modifier_Engineer2 : BaseModifier
    {
        public override string ID => ModifierID.Engineer2;
        public override List<string> ConflictModifiers => new() { ModifierID.Engineer };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.02f);
        }
    }
    public class Modifier_Trickster : BaseModifier
    {
        public override string ID => ModifierID.Trickster;
        public override List<string> ConflictModifiers => new() { ModifierID.Trickster2, ModifierID.Unyielding, ModifierID.Unyielding, ModifierID.Reckless, ModifierID.Reckless2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.Aggro, -400);
        }
    }
    public class Modifier_Trickster2 : BaseModifier
    {
        public override string ID => ModifierID.Trickster2;
        public override List<string> ConflictModifiers => new() { ModifierID.Trickster, ModifierID.Unyielding, ModifierID.Unyielding, ModifierID.Reckless, ModifierID.Reckless2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.Aggro, -800);
            effects.AddBonus(BonusID.EscapeChance, 0.2f);
        }
    }
    public class Modifier_Unyielding : BaseModifier
    {
        public override string ID => ModifierID.Unyielding;
        public override List<string> ConflictModifiers => new() { ModifierID.Unyielding2, ModifierID.Trickster, ModifierID.Trickster2, ModifierID.Negligence, ModifierID.Negligence2, ModifierID.Anxiety, ModifierID.Anxiety2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullMult, 0.2f);
            effects.AddBonus(BonusID.EscapeChance, -0.2f);
        }
    }
    public class Modifier_Unyielding2 : BaseModifier
    {
        public override string ID => ModifierID.Unyielding2;
        public override List<string> ConflictModifiers => new() { ModifierID.Unyielding, ModifierID.Trickster, ModifierID.Trickster2, ModifierID.Negligence, ModifierID.Negligence2, ModifierID.Anxiety, ModifierID.Anxiety2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullMult, 0.3f);
            effects.AddBonus(BonusID.EscapeChance, -0.4f);
        }
    }
    public class Modifier_GaleSpeed : BaseModifier
    {
        public override string ID => ModifierID.GaleSpeed;
        public override List<string> ConflictModifiers => new() { ModifierID.GaleSpeed2, ModifierID.Lethargic, ModifierID.Lethargic2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, 0.1f);
            effects.AddBonusEvasion(BonusID.Evasion, 5f);
        }
    }
    public class Modifier_GaleSpeed2 : BaseModifier
    {
        public override string ID => ModifierID.GaleSpeed2;
        public override List<string> ConflictModifiers => new() { ModifierID.GaleSpeed, ModifierID.Lethargic, ModifierID.Lethargic2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, 0.2f);
            effects.AddBonusEvasion(BonusID.Evasion, 10f);
        }
    }
}
