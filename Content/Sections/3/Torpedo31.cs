using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Torpedo31 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Torpedo_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_G };

        public override List<Vector2> WeaponPos => new() { new(0, 333), new(0, 310), new(0, 282) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_M, ComponentTypes.Defense_M, ComponentTypes.Defense_M,
            ComponentTypes.Defense_M
        };

        public override string ExtraInfo => "SSG";
    }
}
