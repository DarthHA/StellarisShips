using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Broadside43 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Broadside_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M, ComponentTypes.Weapon_M };

        public override List<Vector2> WeaponPos => new() { new(0, 53), new(0, 24) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "MM";

        public override int TailDrawOffSet => 10;
    }
}
