using StellarisShips.Content.WeaponUnits;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Content.Components.Weapons.Torpedo
{
    public class TorpedoEX1 : BaseWeaponComponent          //DPS:50
    {
        public override string EquipType => ComponentTypes.Weapon_G;
        public override int Level => 1;
        public override string TypeName => "Torpedo2";
        public override string ExtraInfo => "G";

        public override int AttackCD => 360;            //30ртио
        public override float Crit => 20;
        public override int MaxDamage => 420;
        public override int MinDamage => 180;
        public override float MaxRange => 600;
        public override float MinRange => 225;

        public virtual float DetectRange => 800;
        public virtual float HomingFactor => 0.3f;
        public virtual float MaxSpeed => 8f;
        public virtual float ExplosionScale => 0.75f;
        public virtual int TimeLeft => 360;

        public override List<string> DamageTag => new() { BonusID.AllWeaponDamage, BonusID.WeaponDamage_G };
        public override List<string> AttackCDTag => new() { BonusID.AllWeaponAttackCD, BonusID.WeaponAttackCD_G };
        public override List<string> CritTag => new() { BonusID.AllWeaponCrit, BonusID.WeaponCrit_G };
        public override List<string> RangeTag => new() { BonusID.AllWeaponRange, BonusID.WeaponRange_G };

        public override long Value => 88 * 500;
        public override int Progress => 3;
        public override void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {
            weapon.CannonName = "G";
            (weapon as TorpedoEXUnit).DetectRange = DetectRange;
            (weapon as TorpedoEXUnit).HomingFactor = HomingFactor;
            (weapon as TorpedoEXUnit).MaxSpeed = MaxSpeed;
            (weapon as TorpedoEXUnit).ExplosionScale = ExplosionScale;
            (weapon as TorpedoEXUnit).TimeLeft = TimeLeft;
        }
    }

    public class TorpedoEX2 : TorpedoEX1          //DPS:140
    {
        public override int Level => 2;
        public override float DetectRange => 1000;
        public override float ExplosionScale => 1f;
        public override int MaxDamage => 1260;
        public override int MinDamage => 420;
        public override long Value => 114 * 800;
        public override int Progress => 7;
    }
}