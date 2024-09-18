using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Hangar41 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Hangar_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M, ComponentTypes.Weapon_P, ComponentTypes.Weapon_P, ComponentTypes.Weapon_H };

        public override List<Vector2> WeaponPos => new() { new(0, 334), new(11, 253), new(-11, 253), new(0, 302) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "MPPH";
    }
}
