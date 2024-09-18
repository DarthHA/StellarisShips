using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Artillery31 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Artillery_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 284) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_M, ComponentTypes.Defense_M, ComponentTypes.Defense_M,
            ComponentTypes.Defense_M
        };

        public override string ExtraInfo => "L";
    }
}
