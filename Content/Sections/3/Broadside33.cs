using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Broadside33 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Broadside_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M };

        public override List<Vector2> WeaponPos => new() { new(0, 71) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "M";

        public override int TailDrawOffSet => 16;
    }
}
