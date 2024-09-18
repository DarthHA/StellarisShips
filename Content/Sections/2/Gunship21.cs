using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Gunship21 : BaseSection
    {
        public override string InternalName => SectionIDs.Destroyer_Gunship_Bow;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_M };

        public override List<Vector2> WeaponPos => new() { new(0, 261), new(0, 220), new(0, 176) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S,
            ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S
        };

        public override string ExtraInfo => "SSM";
    }
}
