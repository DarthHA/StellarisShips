

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.Components.Weapons;
using StellarisShips.Static;
using System;
using Terraria;

namespace StellarisShips.System.BaseType
{
    public abstract class BaseWeaponUnit
    {
        public virtual string InternalName => "";
        /// <summary>
        /// 武器冷却
        /// </summary>
        public float CurrentCooldown = 60;
        /// <summary>
        /// 相对舰体坐标
        /// </summary>
        public Vector2 RelativePos = Vector2.Zero;
        /// <summary>
        /// 炮台角度
        /// </summary>
        public float Rotation = 0;

        /// <summary>
        /// 显示炮塔的名称，不填就不显示
        /// </summary>
        public string CannonName = "";

        /// <summary>
        /// 归属的武器部件
        /// </summary>
        public string ComponentName = "";

        public int RandomDamage
        {
            get
            {
                BaseWeaponComponent component = EverythingLibrary.Components[ComponentName] as BaseWeaponComponent;
                float AuraBonus = FleetSystem.GlobalEffects.ContainsKey(AuraID.ShroudAtkUp) ? 0.25f : 0f;         //虚境伤害加成
                return (int)(component.MinDamage + Main.rand.Next(component.MaxDamage - component.MinDamage + 1) * (DamageBonus + AuraBonus));
            }
        }

        public int AttackCD
        {
            get
            {
                float AuraBonus = FleetSystem.GlobalEffects.ContainsKey(AuraID.InspiringPresence) ? 0.1f : 0f;   //振奋人心
                AuraBonus += FleetSystem.GlobalEffects.ContainsKey(AuraID.ShroudASPDUp) ? 0.25f : 0f;         //虚境攻速加成
                if (EverythingLibrary.Components[ComponentName].EquipType == ComponentTypes.Weapon_H || EverythingLibrary.Components[ComponentName].EquipType == ComponentTypes.Weapon_P)
                    AuraBonus = 0f;  //P槽和H槽不吃攻速
                float FinalBonus = AttackCDBonus + AuraBonus;
                return (int)((EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).AttackCD / FinalBonus);
            }
        }
        public float Crit
        {
            get
            {
                float AuraBonus = FleetSystem.GlobalEffects.ContainsKey(AuraID.TargetingGrid) ? 10f : 0f;      //目标网格
                return (EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).Crit + CritBonus + AuraBonus;
            }
        }

        public float MinRange => (EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).MinRange;

        public float MaxRange => (EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).MaxRange;

        public float DamageBonus = 1;
        public float AttackCDBonus = 1;
        public float CritBonus = 0;

        public int Level => EverythingLibrary.Components[ComponentName].Level;

        public string EquipType => EverythingLibrary.Components[ComponentName].EquipType;

        public virtual void Update(NPC ship)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {

        }

        public static BaseWeaponUnit NewWeaponUnit(string name)
        {
            if (EverythingLibrary.WeaponUnits.TryGetValue(name, out BaseWeaponUnit value))
            {
                BaseWeaponUnit instance = (BaseWeaponUnit)Activator.CreateInstance(value.GetType());
                return instance;
            }
            return null;
        }
    }
}
