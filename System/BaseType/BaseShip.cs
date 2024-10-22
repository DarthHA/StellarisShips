using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;


namespace StellarisShips.System.BaseType
{
    public abstract class BaseShip
    {
        public virtual string InternalName => "";
        public virtual int PartCount => 1;
        public virtual int CP => 1;
        public virtual int Width => 10;
        public virtual int Length => 10;
        public virtual List<List<string>> CanUseSections => new();
        public virtual List<string> CanUseComputer => new();
        public virtual string InitialComputer => "";
        /// <summary>
        /// 炮台缩放
        /// </summary>
        public virtual float WeaponScale => 1f;

        /// <summary>
        /// 舰船缩放
        /// </summary>
        public virtual float ShipScale => 1f;

        /// <summary>
        /// 在舰船设计界面的Offset
        /// </summary>
        public virtual float ShipDesignViewOffSet => 0;

        public virtual int BaseHull => 100;
        public virtual int BaseDefense => 0;
        public virtual int BaseShield => 0;
        public virtual int BaseEvasion => 5;
        public virtual int BaseSpeed => 100;

        public virtual long Value => Item.buyPrice(0, 1, 0, 0);

        /// <summary>
        /// 解锁时期
        /// </summary>
        public virtual int Progress => 1;

        /// <summary>
        /// 是否有光环槽
        /// </summary>
        public virtual bool HasAura => false;

        /// <summary>
        /// 最大限造数
        /// </summary>
        public virtual int MaxCount => 99;

        public virtual void DrawTrail(SpriteBatch spriteBatch, Vector2 screenPos, NPC ship)
        {

        }

        public string GetLocalizedName()
        {
            return Language.GetOrRegister("Mods.StellarisShips.Name." + InternalName).Value;
        }

        public string GetLocalizedDescription()
        {
            return string.Format("[c/ffd700:{0}]", GetLocalizedName()) + "\n" + Language.GetOrRegister("Mods.StellarisShips.Desc." + InternalName).Value
                + "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Value"), MoneyHelpers.ShowCoins(Value));
        }

        public Texture2D GetIcon()
        {
            return ModContent.Request<Texture2D>("StellarisShips/Images/OtherIcons/Ship_" + InternalName, AssetRequestMode.ImmediateLoad).Value;
        }

        public bool CanUnlock()
        {
            return ProgressHelper.CurrentProgress >= Progress;
        }
    }
}
