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
        public virtual string AuraType => "";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = AuraType;
        }
    }

    public class QuantumDestabilizer : BaseAura
    {
        public override string TypeName => "QuantumDestabilizer";
        public override string AuraType => ModifierID.QuantumDestabilizer;
    }

    public class ShieldDampener : BaseAura
    {
        public override string TypeName => "ShieldDampener";
        public override string AuraType => ModifierID.ShieldDampener;
    }

    public class SubspaceSnare : BaseAura
    {
        public override string TypeName => "SubspaceSnare";
        public override string AuraType => ModifierID.SubspaceSnare;
    }

    public class InspiringPresence : BaseAura
    {
        public override string TypeName => "InspiringPresence";
        public override void ApplyEquip(NPC ship)
        {
            ship.GetShipNPC().AuraType = ModifierID.InspiringPresence;
        }
    }

    public class AncientTargetScrambler : BaseAura
    {
        public override string TypeName => "AncientTargetScrambler";
        public override int MRValue => 120;
        public override string AuraType => ModifierID.AncientTargetScrambler;
    }

    public class NanobotCloud : BaseAura
    {
        public override string TypeName => "NanobotCloud";
        public override string AuraType => ModifierID.NanobotCloud;
    }

    public class TargetingGrid : BaseAura
    {
        public override string TypeName => "TargetingGrid";
        public override string AuraType => ModifierID.TargetingGrid;
    }

    public class NoAura : BaseAura
    {
        public override string TypeName => "NoAura";
        public override long Value => 0;
        public override string AuraType => "";
    }
}