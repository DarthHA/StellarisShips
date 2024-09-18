using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Broadside42 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Broadside_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M, ComponentTypes.Weapon_M, ComponentTypes.Weapon_L, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 211), new(0, 181), new(23, 128), new(-23, 128) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "MMLL";
    }
}
