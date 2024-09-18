using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.System.BaseType
{
    public abstract class BaseSection
    {
        /// <summary>
        /// 内部名
        /// </summary>
        public virtual string InternalName => "";
        /// <summary>
        /// 承载的武器槽
        /// </summary>
        public virtual List<string> WeaponSlot => new();
        /// <summary>
        /// 武器相对舰身位置
        /// </summary>
        public virtual List<Vector2> WeaponPos => new();

        /// <summary>
        /// 承载的通用槽
        /// </summary>
        public virtual List<string> UtilitySlot => new();

        /// <summary>
        /// 额外信息
        /// </summary>
        public virtual string ExtraInfo => "";

        /// <summary>
        /// 为了从艉处对其不同舰船，需要加这样一个OffSet，只在艉部件处加，从尾部算起的相对坐标减去这个值就是绝对坐标
        /// </summary>
        public virtual int TailDrawOffSet => 0;

        public string GetLocalizedName()
        {
            return Language.GetOrRegister("Mods.StellarisShips.Name." + InternalName).Value;
        }

        public Texture2D GetIcon()
        {
            return ModContent.Request<Texture2D>("StellarisShips/Images/OtherIcons/Section", AssetRequestMode.ImmediateLoad).Value;
        }

    }
}
