using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Interceptor22 : BaseSection
    {
        public override string InternalName => SectionIDs.Destroyer_Gunship_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_M };

        public override List<Vector2> WeaponPos => new() { new(0, 28) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory,
            ComponentTypes.Accessory,
            ComponentTypes.Accessory
        };

        public override string ExtraInfo => "M";

        public override int TailDrawOffSet => 0;
    }
}
