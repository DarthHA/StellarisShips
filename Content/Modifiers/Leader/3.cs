using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
namespace StellarisShips.Content.Modifiers.Leader
{
    public class Modifier_Artillerist : BaseModifier
    {
        public override string ID => ModifierID.Artillerist;
        public override List<string> ConflictModifiers => new() { ModifierID.Artillerist2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.1f);
        }
    }
    public class Modifier_Artillerist2 : BaseModifier
    {
        public override string ID => ModifierID.Artillerist2;
        public override List<string> ConflictModifiers => new() { ModifierID.Artillerist };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.1f);
            effects.AddBonus(BonusID.AllWeaponCrit, 0.1f);
        }
    }
    public class Modifier_JuryRigger : BaseModifier
    {
        public override string ID => ModifierID.JuryRigger;
        public override List<string> ConflictModifiers => new() { ModifierID.JuryRigger2, ModifierID.Wrecker, ModifierID.Wrecker2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.VampireHeal, 0.1f);
        }
    }
    public class Modifier_JuryRigger2 : BaseModifier
    {
        public override string ID => ModifierID.JuryRigger2;
        public override List<string> ConflictModifiers => new() { ModifierID.JuryRigger, ModifierID.Wrecker, ModifierID.Wrecker2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.VampireHeal, 0.2f);
        }
    }
    public class Modifier_Wrecker : BaseModifier
    {
        public override string ID => ModifierID.Wrecker;
        public override List<string> ConflictModifiers => new() { ModifierID.Wrecker2, ModifierID.JuryRigger, ModifierID.JuryRigger2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.12f);
        }
    }
    public class Modifier_Wrecker2 : BaseModifier
    {
        public override string ID => ModifierID.Wrecker2;
        public override List<string> ConflictModifiers => new() { ModifierID.Wrecker, ModifierID.JuryRigger, ModifierID.JuryRigger2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponDamage, 0.24f);
        }
    }
    public class Modifier_Demolisher : BaseModifier
    {
        public override string ID => ModifierID.Demolisher;
        public override List<string> ConflictModifiers => new() { ModifierID.Demolisher2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.DamageToNonBoss, 0.2f);
        }
    }
    public class Modifier_Demolisher2 : BaseModifier
    {
        public override string ID => ModifierID.Demolisher2;
        public override List<string> ConflictModifiers => new() { ModifierID.Demolisher };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.DamageToNonBoss, 0.4f);
        }
    }
}
