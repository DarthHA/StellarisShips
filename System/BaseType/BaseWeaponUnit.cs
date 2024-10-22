

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
                return (int)(component.MinDamage + Main.rand.Next(component.MaxDamage - component.MinDamage + 1) * DamageBonus);
            }
        }

        public int AttackCD
        {
            get
            {
                return (int)((EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).AttackCD / AttackCDBonus);
            }
        }
        public float Crit
        {
            get
            {
                return (EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).Crit + CritBonus;
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
