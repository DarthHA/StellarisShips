using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Broadside41 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Broadside_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_M, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(11, 256), new(-11, 256), new(0, 333), new(0, 293) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "SSML";
    }
}
