using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Hangar42 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Hangar_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M, ComponentTypes.Weapon_M, ComponentTypes.Weapon_M, ComponentTypes.Weapon_M, ComponentTypes.Weapon_H };

        public override List<Vector2> WeaponPos => new() { new(14, 210), new(-14, 210), new(17, 125), new(-17, 125), new(0, 165) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "MMMMH";
    }
}
