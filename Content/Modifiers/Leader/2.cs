

using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers.Leader
{
    public class Modifier_Negligence : BaseModifier
    {
        public override string ID => ModifierID.Negligence;
        public override List<string> ConflictModifiers => new() { ModifierID.Negligence2, ModifierID.Unyielding, ModifierID.Unyielding2 };
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullMult, -0.1f);
            effects.AddBonus(BonusID.ShieldMult, -0.1f);
        }
    }
    public class Modifier_Negligence2 : BaseModifier
    {
        public override string ID => ModifierID.Negligence2;
        public override List<string> ConflictModifiers => new() { ModifierID.Negligence, ModifierID.Unyielding, ModifierID.Unyielding2 };
        public override int Cost => 2;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullMult, -0.2f);
            effects.AddBonus(BonusID.ShieldMult, -0.2f);
        }
    }
    public class Modifier_Reckless : BaseModifier
    {
        public override string ID => ModifierID.Reckless;
        public override List<string> ConflictModifiers => new() { ModifierID.Reckless2, ModifierID.Trickster, ModifierID.Trickster2 };
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.EscapeChance, -0.3f);
        }
    }
    public class Modifier_Reckless2 : BaseModifier
    {
        public override string ID => ModifierID.Reckless2;
        public override List<string> ConflictModifiers => new() { ModifierID.Reckless, ModifierID.Trickster, ModifierID.Trickster2 };
        public override int Cost => 2;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.EscapeChance, -0.6f);
        }
    }
    public class Modifier_Anxiety : BaseModifier
    {
        public override string ID => ModifierID.Anxiety;
        public override List<string> ConflictModifiers => new() { ModifierID.Anxiety2, ModifierID.Unyielding, ModifierID.Unyielding2 };
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, -0.1f);
        }
    }
    public class Modifier_Anxiety2 : BaseModifier
    {
        public override string ID => ModifierID.Anxiety2;
        public override List<string> ConflictModifiers => new() { ModifierID.Anxiety, ModifierID.Unyielding, ModifierID.Unyielding2 };
        public override int Cost => 2;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, -0.2f);
        }
    }
    public class Modifier_Lethargic : BaseModifier
    {
        public override string ID => ModifierID.Lethargic;
        public override List<string> ConflictModifiers => new() { ModifierID.Lethargic2, ModifierID.GaleSpeed, ModifierID.GaleSpeed2 };
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, -0.1f);
            effects.AddBonus(BonusID.Evasion, -5f);
        }
    }
    public class Modifier_Lethargic2 : BaseModifier
    {
        public override string ID => ModifierID.Lethargic2;
        public override List<string> ConflictModifiers => new() { ModifierID.Lethargic, ModifierID.GaleSpeed, ModifierID.GaleSpeed2 };
        public override int Cost => 2;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, -0.2f);
            effects.AddBonus(BonusID.Evasion, -10f);
        }
    }
    public class Modifier_Disgusting : BaseModifier
    {
        public override string ID => ModifierID.Disgusting;
        public override List<string> ConflictModifiers => new() { ModifierID.Disgusting2 };
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.Aggro, 400);
        }
    }
    public class Modifier_Disgusting2 : BaseModifier
    {
        public override string ID => ModifierID.Disgusting2;
        public override List<string> ConflictModifiers => new() { ModifierID.Disgusting };
        public override int Cost => 2;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.Aggro, 800);
        }
    }
}
