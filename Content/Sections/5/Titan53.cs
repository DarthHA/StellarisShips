using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Titan53 : BaseSection
    {
        public override string InternalName => SectionIDs.Titan_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(14, 26), new(-14, 26) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "LL";

        public override int TailDrawOffSet => 20;
    }
}
