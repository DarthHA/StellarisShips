using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;

namespace StellarisShips.Content.Components.Core
{
    public abstract class BaseAura : BaseComponent
    {
        public override string EquipType => ComponentTypes.Aura;
        public override int Level => 1;
        public override long Value => 10 * 10000;
    }
    public class QuantumDestabilizer : BaseAura
    {
        public override string TypeName => "QuantumDestabilizer";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.QuantumDestabilizer;
        }
    }

    public class ShieldDampener : BaseAura
    {
        public override string TypeName => "ShieldDampener";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.ShieldDampener;
        }
    }

    public class SubspaceSnare : BaseAura
    {
        public override string TypeName => "SubspaceSnare";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.SubspaceSnare;
        }
    }

    public class InspiringPresence : BaseAura
    {
        public override string TypeName => "InspiringPresence";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.InspiringPresence;
        }
    }

    public class AncientTargetScrambler : BaseAura
    {
        public override string TypeName => "AncientTargetScrambler";
        public override int MRValue => 120;
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.AncientTargetScrambler;
        }
    }

    public class NanobotCloud : BaseAura
    {
        public override string TypeName => "NanobotCloud";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.NanobotCloud;
        }
    }

    public class TargetingGrid : BaseAura
    {
        public override string TypeName => "TargetingGrid";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraID.TargetingGrid;
        }
    }

    public class NoAura : BaseAura
    {
        public override string TypeName => "NoAura";
        public override long Value => 0;
    }
}