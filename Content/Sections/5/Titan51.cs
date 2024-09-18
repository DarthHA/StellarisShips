using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Titan51 : BaseSection
    {
        public override string InternalName => SectionIDs.Titan_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_T };

        public override List<Vector2> WeaponPos => new() { new(0, 280) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L,
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "T";
    }
}
