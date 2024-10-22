using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers.Leader
{
    public class Modifier_GunshipFocus : BaseModifier
    {
        public override string ID => ModifierID.GunshipFocus;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_S, 0.1f);
            effects.AddBonus(BonusID.WeaponAttackCD_S, 0.1f);
            effects.AddBonus(BonusID.WeaponDamage_P, 0.075f);
            effects.AddBonus(BonusID.WeaponDamage_M, 0.075f);
            effects.AddBonus(BonusID.WeaponAttackCD_M, 0.075f);
        }
    }
    public class Modifier_GunshipFocus2 : BaseModifier
    {
        public override string ID => ModifierID.GunshipFocus2;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_S, 0.2f);
            effects.AddBonus(BonusID.WeaponAttackCD_S, 0.2f);
            effects.AddBonus(BonusID.WeaponDamage_P, 0.2f);
            effects.AddBonus(BonusID.WeaponDamage_M, 0.15f);
            effects.AddBonus(BonusID.WeaponAttackCD_M, 0.15f);
        }
    }
    public class Modifier_ArtilleryFocus : BaseModifier
    {
        public override string ID => ModifierID.ArtilleryFocus;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_L, 0.20f);
            effects.AddBonus(BonusID.WeaponDamage_X, 0.20f);
            effects.AddBonus(BonusID.WeaponDamage_T, 0.20f);
        }
    }
    public class Modifier_ArtilleryFocus2 : BaseModifier
    {
        public override string ID => ModifierID.ArtilleryFocus2;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_L, 0.35f);
            effects.AddBonus(BonusID.WeaponDamage_X, 0.35f);
            effects.AddBonus(BonusID.WeaponDamage_T, 0.35f);
        }
    }
    public class Modifier_CarrierFocus : BaseModifier
    {
        public override string ID => ModifierID.CarrierFocus;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_H, 0.3f);
        }
    }
    public class Modifier_CarrierFocus2 : BaseModifier
    {
        public override string ID => ModifierID.CarrierFocus2;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_H, 0.6f);
        }
    }
    public class Modifier_GuidanceSystemFocus : BaseModifier
    {
        public override string ID => ModifierID.GuidanceSystemFocus;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_G, 0.15f);
            effects.AddBonus(BonusID.WeaponAttackCD_G, 0.15f);
        }
    }
    public class Modifier_GuidanceSystemFocus2 : BaseModifier
    {
        public override string ID => ModifierID.GuidanceSystemFocus2;
        public override List<string> ConflictModifiers => new() { ModifierID.GunshipFocus, ModifierID.GunshipFocus2, ModifierID.ArtilleryFocus, ModifierID.ArtilleryFocus2, ModifierID.CarrierFocus, ModifierID.CarrierFocus2, ModifierID.GuidanceSystemFocus, ModifierID.GuidanceSystemFocus2 };
        public override int Cost => 2;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.WeaponDamage_G, 0.3f);
            effects.AddBonus(BonusID.WeaponAttackCD_G, 0.3f);
        }
    }
}
