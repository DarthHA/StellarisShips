using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;


namespace StellarisShips.Content.Sections
{
    public class MissileBoat11 : BaseSection
    {
        public override string InternalName => SectionIDs.Corvette_MissileBoat_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_G };

        public override List<Vector2> WeaponPos => new() { new(0, 124), new(0, 79) };

        public override List<string> UtilitySlot => new() { ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Defense_S, ComponentTypes.Accessory, ComponentTypes.Accessory };

        public override string ExtraInfo => "SG";

        public override int TailDrawOffSet => 0;
    }
}
