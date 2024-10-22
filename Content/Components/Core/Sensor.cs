using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core
{
    public abstract class BaseSensor : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Sensor;
        public sealed override string TypeName => "Sensor";
        public virtual int DetectRange => 0;
        public virtual float Crit => 0;
        public virtual bool CanSeeThroughTiles => false;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.AllWeaponCrit, Crit);
            ship.GetShipNPC().StaticBuff.AddBonus(BonusID.DetectRange, DetectRange);
            ship.GetShipNPC().CanSeeThroughTiles = ship.GetShipNPC().CanSeeThroughTiles || CanSeeThroughTiles;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Sensor1"), DetectRange);
            if (Crit > 0)
            {
                desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Sensor2"), Crit);
            }
            if (CanSeeThroughTiles)
            {
                desc += "\n" + Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Sensor3");
            }
        }
    }

    public class Sensor1 : BaseSensor
    {
        public override int Level => 1;
        public override int DetectRange => 400;
        public override float Crit => 2.5f;
        public override bool CanSeeThroughTiles => false;
        public override long Value => 2 * 200;
    }

    public class Sensor2 : BaseSensor
    {
        public override int Level => 2;
        public override int DetectRange => 600;
        public override float Crit => 5;
        public override bool CanSeeThroughTiles => false;
        public override long Value => 4 * 200;
        public override int Progress => 2;
    }

    public class Sensor3 : BaseSensor
    {
        public override int Level => 3;
        public override int DetectRange => 800;
        public override float Crit => 10;
        public override bool CanSeeThroughTiles => true;
        public override long Value => 6 * 200;
        public override int Progress => 4;
    }

    public class Sensor4 : BaseSensor
    {
        public override int Level => 4;
        public override int DetectRange => 1200;
        public override float Crit => 15;
        public override bool CanSeeThroughTiles => true;
        public override long Value => 8 * 200;
        public override int Progress => 7;
    }
}