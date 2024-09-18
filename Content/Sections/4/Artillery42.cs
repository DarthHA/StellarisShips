using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Artillery42 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Artillery_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L, ComponentTypes.Weapon_L, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 203), new(0, 164), new(0, 125) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "LLL";
    }
}
