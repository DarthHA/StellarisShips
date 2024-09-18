using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Artillery21 : BaseSection
    {
        public override string InternalName => SectionIDs.Destroyer_Artillery_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 215) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S,
            ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S
        };

        public override string ExtraInfo => "L";
    }
}
