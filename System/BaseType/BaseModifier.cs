using System.Collections.Generic;
using Terraria.Localization;
namespace StellarisShips.System.BaseType
{
    public abstract class BaseModifier
    {
        public virtual string ID => "";
        public virtual bool Negative => false;
        public virtual List<string> ConflictModifiers => new();
        public virtual int Cost => 1;
        public virtual void ApplyBonus(Dictionary<string, float> effects)
        {

        }

        public string GetLocalizedName()
        {
            return Language.GetTextValue("Mods.StellarisShips.Name." + ID);
        }

        public string GetLocalizedDesc()
        {
            return Language.GetTextValue("Mods.StellarisShips.Desc." + ID);
        }
    }
}
