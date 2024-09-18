using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Gunship33 : BaseSection
    {
        public override string InternalName => SectionIDs.Cruiser_Gunship_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S };

        public override List<Vector2> WeaponPos => new() { new(0, 70), new(0, 41) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "SS";

        public override int TailDrawOffSet => 0;
    }
}
