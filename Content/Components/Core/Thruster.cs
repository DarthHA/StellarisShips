using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Core
{
    public abstract class BaseThruster : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Thruster;
        public sealed override string TypeName => "Thruster";
        public virtual float Evasion => 0;
        public virtual float Speed => 0;
        public sealed override void ApplyEquip(NPC ship)
        {
            string type = ship.GetShipNPC().ShipGraph.ShipType;
            float RealEvasion = Evasion;
            switch (type)
            {
                case ShipIDs.Corvette:
                    break;
                case ShipIDs.Destroyer:
                    RealEvasion = Evasion * 0.8f;
                    break;
                case ShipIDs.Cruiser:
                    RealEvasion = Evasion * 0.6f;
                    break;
                case ShipIDs.Battleship:
                    RealEvasion = Evasion * 0.4f;
                    break;
            }
            if (RealEvasion < 1) RealEvasion = 1;
            ship.GetShipNPC().Evasion += RealEvasion;
            ship.GetShipNPC().BonusBuff.AddBonus(BonusID.Speed, Speed);
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Thruster"), Speed * 100, Evasion);
        }


    }
    public class Thruster1 : BaseThruster
    {
        public override int Level => 1;
        public override float Evasion => 1;
        public override float Speed => 0.25f;
        public override long Value => 5 * 200;
    }
    public class Thruster2 : BaseThruster
    {
        public override int Level => 2;
        public override float Evasion => 5;
        public override float Speed => 0.5f;
        public override long Value => 10 * 200;
        public override int Progress => 2;
    }
    public class Thruster3 : BaseThruster
    {
        public override int Level => 3;
        public override float Evasion => 10;
        public override float Speed => 0.75f;
        public override long Value => 15 * 200;
        public override int Progress => 4;
    }
    public class Thruster4 : BaseThruster
    {
        public override int Level => 4;
        public override float Evasion => 15;
        public override float Speed => 1f;
        public override long Value => 20 * 200;
        public override int Progress => 7;
    }
    public class Thruster5 : BaseThruster
    {
        public override int Level => 5;
        public override float Evasion => 20;
        public override float Speed => 1.25f;
        public override long Value => 25 * 200;
        public override int Progress => 9;
    }
}