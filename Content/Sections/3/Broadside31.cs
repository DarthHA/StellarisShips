using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Broadside31 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Broadside_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M, ComponentTypes.Weapon_M };

        public override List<Vector2> WeaponPos => new() { new(0, 314), new(0, 286) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_M, ComponentTypes.Defense_M, ComponentTypes.Defense_M,
            ComponentTypes.Defense_M
        };

        public override string ExtraInfo => "MM";
    }
}
