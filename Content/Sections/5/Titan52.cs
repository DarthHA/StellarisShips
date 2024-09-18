using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Titan52 : BaseSection
    {
        public override string InternalName => SectionIDs.Titan_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L, ComponentTypes.Weapon_L, ComponentTypes.Weapon_L, ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(16, 150), new(-16, 150), new(22, 112), new(-22, 112) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L,
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "LLLL";
    }
}
