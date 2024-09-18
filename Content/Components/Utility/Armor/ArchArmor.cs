using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.Localization;

namespace StellarisShips.Content.Components.Utility.Armor
{
    public class ArchArmorS : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_S;
        public override int Level => 1;
        public sealed override string TypeName => "ArchArmor";
        public sealed override string ExtraInfo => "SML";
        public int Defense => 40;
        public int Shield => 300;
        public float ShieldRegen => 6;
        public override long Value => 35 * 400;
        public override int MRValue => 20;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.defense += Defense;
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus2").Value, Defense, Shield, ShieldRegen);
        }
    }

    public class ArchArmorM : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_M;
        public override int Level => 1;
        public sealed override string TypeName => "ArchArmor";
        public sealed override string ExtraInfo => "SML";
        public int Defense => 50;
        public int Shield => 800;
        public float ShieldRegen => 15;
        public override long Value => 52 * 600;
        public override int MRValue => 40;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.defense += Defense;
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus2").Value, Defense, Shield, ShieldRegen);
        }
    }

    public class ArchArmorL : BaseComponent
    {
        public sealed override string EquipType => ComponentTypes.Defense_L;
        public override int Level => 1;
        public sealed override string TypeName => "ArchArmor";
        public sealed override string ExtraInfo => "SML";
        public int Defense => 60;
        public int Shield => 1800;
        public float ShieldRegen => 36;
        public override long Value => 69 * 800;
        public override int MRValue => 80;
        public sealed override void ApplyEquip(NPC ship)
        {
            ship.defense += Defense;
            ship.GetShipNPC().MaxShield += Shield;
            ship.GetShipNPC().ShieldRegen += ShieldRegen;
        }

        public override void ModifyDesc(ref string desc)
        {
            desc += "\n" + string.Format(Language.GetOrRegister("Mods.StellarisShips.ExtraDesc.DefensePlus2").Value, Defense, Shield, ShieldRegen);
        }
    }
}
