using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Artillery41 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Artillery_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 303), new(0, 264) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "LL";
    }
}
