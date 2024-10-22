using System.Collections.Generic;
namespace StellarisShips.System.BaseType
{
    public abstract class BaseModifier
    {
        public virtual string ID => "";
        public virtual void ApplyBonus(Dictionary<string, float> effects)
        {

        }
    }
}
