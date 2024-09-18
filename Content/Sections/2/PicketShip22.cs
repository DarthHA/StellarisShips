﻿using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;

namespace StellarisShips.Content.Sections
{
    public class Picketship22 : BaseSection
    {
        public override string InternalName => SectionIDs.Destroyer_PicketShip_Stern;

        public override List<string> WeaponSlot => new() { ComponentTypes.Weapon_P, ComponentTypes.Weapon_P };

        public override List<Vector2> WeaponPos => new() { new(17, 49), new(-17, 49) };

        public override List<string> UtilitySlot => new()
        {
            ComponentTypes.Accessory, ComponentTypes.Accessory
        };

        public override string ExtraInfo => "PP";

        public override int TailDrawOffSet => 0;
    }
}