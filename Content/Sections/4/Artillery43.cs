using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Artillery43 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Artillery_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_L };

        public override List<Vector2> WeaponPos => new() { new(0, 38) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "L";

        public override int TailDrawOffSet => 0;
    }
}
