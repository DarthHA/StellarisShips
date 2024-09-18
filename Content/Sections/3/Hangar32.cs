using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Hangar32 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Hangar_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_P, ComponentTypes.Weapon_P, ComponentTypes.Weapon_H };

        public override List<Vector2> WeaponPos => new() { new(10, 174), new(-10, 174), new(0, 193) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_M, ComponentTypes.Defense_M, ComponentTypes.Defense_M,
            ComponentTypes.Defense_M
        };

        public override string ExtraInfo => "PPH";
    }
}
