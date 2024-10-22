using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core
{
    public class Reactor1 : BaseComponent
    {
        public override string EquipType => ComponentTypes.Reactor;
        public override int Level => 1;
        public override string TypeName => "Reactor";
        public virtual float Damage => 0.05f;
        public virtual float Speed => 0.05f;
        public override long Value => 5 * 300;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.AllWeaponDamage, Damage);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.SpeedMult, Speed);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Reactor"), Math.Round(Damage * 100, 1), Math.Round(Speed * 100, 1));
        }

    }

    public class Reactor2 : Reactor1
    {
        public override int Level => 2;
        public override float Damage => 0.1f;
        public override float Speed => 0.1f;
        public override long Value => 10 * 300;
        public override int Progress => 3;
    }
    public class Reactor3 : Reactor1
    {
        public override int Level => 3;
        public override float Damage => 0.15f;
        public override float Speed => 0.15f;
        public override long Value => 15 * 300;
        public override int Progress => 4;
    }
    public class Reactor4 : Reactor1
    {
        public override int Level => 4;
        public override float Damage => 0.2f;
        public override float Speed => 0.2f;
        public override long Value => 20 * 300;
        public override int Progress => 6;
    }
    public class Reactor5 : Reactor1
    {
        public override int Level => 5;
        public override float Damage => 0.25f;
        public override float Speed => 0.25f;
        public override long Value => 25 * 300;
        public override int Progress => 8;
    }
    public class Reactor6 : Reactor1
    {
        public override int Level => 6;
        public override float Damage => 0.4f;
        public override float Speed => 0.4f;
        public override long Value => 30 * 300;
        public override int Progress => 9;
    }
}