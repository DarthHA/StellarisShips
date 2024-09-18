using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Carrier42 : BaseSection
    {
        public override string InternalName => SectionIDs.Battleship_Carrier_Core;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_S, ComponentTypes.Weapon_S, ComponentTypes.Weapon_P, ComponentTypes.Weapon_P, ComponentTypes.Weapon_H, ComponentTypes.Weapon_H };

        public override List<Vector2> WeaponPos => new() { new(18, 208), new(-18, 208), new(20, 123), new(-20, 123), new(54, 162), new(-54, 162) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Defense_L, ComponentTypes.Defense_L, ComponentTypes.Defense_L
        };

        public override string ExtraInfo => "SSPPHH";
    }
}
