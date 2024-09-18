using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Torpedo32 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Torpedo_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_G, ComponentTypes.Weapon_G };

        public override List<Vector2> WeaponPos => new() { new(15, 218), new(-15, 218), new(0, 186), new(0, 152) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_M, ComponentTypes.Defense_M, ComponentTypes.Defense_M,
            ComponentTypes.Defense_M
        };

        public override string ExtraInfo => "SSGG";
    }
}
