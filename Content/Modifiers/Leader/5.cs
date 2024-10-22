using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Modifiers.Leader
{
    public class Modifier_GuideInsight : BaseModifier
    {
        public override string ID => ModifierID.GuideInsight;
        public override int Cost => 0;
    }
    public class Modifier_DisgustingAngler : BaseModifier
    {
        public override string ID => ModifierID.DisgustingAngler;
        public override int Cost => 0;
        public override bool Negative => true;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.Aggro, 9999);
        }
    }
    public class Modifier_EvilCleaner : BaseModifier
    {
        public override string ID => ModifierID.EvilCleaner;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.DamageToBoss, 0.15f);
        }
    }
    public class Modifier_TavernKeeperInsight : BaseModifier
    {
        public override string ID => ModifierID.TavernKeeperInsight;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.DamageToOldOnes, 0.2f);
        }
    }
    public class Modifier_SkeletronCurse : BaseModifier
    {
        public override string ID => ModifierID.SkeletronCurse;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponAttackCD, 0.05f);
        }
    }
    public class Modifier_HellHeart : BaseModifier
    {
        public override string ID => ModifierID.HellHeart;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.SpeedMultFinal, 0.1f);
            effects.AddBonusEvasion(BonusID.Evasion, 5f);
            effects.AddBonus(BonusID.HullMult, -0.05f);
        }
    }
    public class Modifier_Cyborg : BaseModifier
    {
        public override string ID => ModifierID.Cyborg;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.AllWeaponCrit, 0.05f);
        }
    }
    public class Modifier_GreatPrincess : BaseModifier
    {
        public override string ID => ModifierID.GreatPrincess;
        public override int Cost => 0;
    }

    public class Modifier_Cute : BaseModifier
    {
        public override string ID => ModifierID.Cute;
        public override int Cost => 0;
    }
    public class Modifier_Slimy : BaseModifier
    {
        public override string ID => ModifierID.Slimy;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.HullRegenPercent, 0.03f);
        }
    }
    public class Modifier_MushroomMind : BaseModifier
    {
        public override string ID => ModifierID.MushroomMind;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonus(BonusID.DamageInMushroom, 0.1f);
        }
    }
    public class Modifier_MagicShield : BaseModifier
    {
        public override string ID => ModifierID.MagicShield;
        public override int Cost => 0;
        public override void ApplyBonus(Dictionary<string, float> effects)
        {
            effects.AddBonusShieldDR(BonusID.ShieldDR, 0.05f);
        }
    }
}
