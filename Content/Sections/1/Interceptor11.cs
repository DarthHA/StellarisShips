using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
namespace StellarisShips.Content.Sections
{
    public class Interceptor11 : BaseSection
    {
        public override string InternalName => SectionIDs.Corvette_Interceptor_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_S };

        public override List<Vector2> WeaponPos => new() { new(0, 168), new(0, 119), new(0, 83) };

        public override List<string> UtilitySlot => new() { ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Accessory, ComponentTypes.Accessory };

        public override string ExtraInfo => "SSS";

        public override int TailDrawOffSet => 0;
    }
}
