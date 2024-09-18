using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.System.BaseType
{
    public abstract class BaseComponent
    {
        /// <summary>
        /// 配件大类名
        /// </summary>
        public virtual string TypeName => "";
        /// <summary>
        /// 所需槽位
        /// </summary>
        public virtual string EquipType => "";
        /// <summary>
        /// 配件等级，用于归类和排序
        /// </summary>
        public virtual int Level => 0;

        /// <summary>
        /// 根据所有信息获取的内部名,举例，S_Laser_1就是S槽1级激光（红激光）
        /// </summary>
        public string InternalName
        {
            get { return EquipType + "_" + TypeName + "_" + Level.ToString(); }
        }

        /// <summary>
        /// 额外信息
        /// </summary>
        public virtual string ExtraInfo => "";

        /// <summary>
        /// 是否为爆炸武器，用于区分炮台绘制
        /// </summary>
        public virtual bool IsExplosive => false;

        /// <summary>
        /// 钱币造价
        /// </summary>
        public virtual long Value => Item.buyPrice(0, 1, 0, 0);

        /// <summary>
        /// 稀有文物造价
        /// </summary>
        public virtual int MRValue => 0;

        /// <summary>
        /// 解锁时期
        /// </summary>
        public virtual int Progress => 1;

        /// <summary>
        /// 特殊方法解锁的Key，需要记录
        /// </summary>
        public virtual string SpecialUnLock => "";

        public virtual void ApplyEquip(NPC ship)
        {

        }

        public virtual void ModifyWeaponUnit(BaseWeaponUnit weapon, NPC ship)
        {

        }

        public virtual void ModifyDesc(ref string desc)
        {

        }

        public Texture2D GetIcon()
        {
            return ModContent.Request<Texture2D>("StellarisShips/Images/Components/" + TypeName + "_" + Level.ToString()).Value;
        }

        public string GetLocalizedName()
        {
            return Language.GetOrRegister("Mods.StellarisShips.Name." + InternalName).Value;
        }

        public string GetLocalizedDescription()
        {
            string result = string.Format("[c/ffd700:{0}]", GetLocalizedName());
            ModifyDesc(ref result);
            result += "\n" + string.Format(Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Value"), MoneyHelpers.ShowCoins(Value, MRValue));
            result = result.Replace("[i:StellarisShips/MR_Icon]", "MR_");
            result += "\n" + Language.GetOrRegister("Mods.StellarisShips.Desc." + TypeName + "_" + Level.ToString()).Value;
            return result;
        }

        public bool CanUnlock()
        {
            if (SpecialUnLock != "")
            {
                return ProgressHelper.UnlockTech.Contains(SpecialUnLock);
            }
            if (MRValue > 0)
            {
                return ProgressHelper.DiscoveredMR >= 2;
            }
            return ProgressHelper.CurrentProgress >= Progress;
        }
    }
}