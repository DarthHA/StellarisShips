using System.Collections.Generic;
using Terraria.Localization;


namespace StellarisShips.System.BaseType
{
    public abstract class BaseDialog
    {
        public virtual string InternalName => "";
        /// <summary>
        /// 按钮显示文本
        /// </summary>
        public virtual List<string> ButtonNames => new();

        /// <summary>
        /// 按钮内部名
        /// </summary>
        public virtual List<string> ButtonInternalStrs => new();
        /// <summary>
        /// 用于初始化
        /// </summary>
        public virtual void SetUp()
        {

        }

        /// <summary>
        /// 用于更新
        /// </summary>
        public virtual void Update()
        {

        }
        /// <summary>
        /// 按钮单击事件
        /// </summary>
        /// <param name="internalStr"></param>
        public virtual void ClickEvent(string internalStr)
        {

        }

        public string GetDialogLocalize(string str)
        {
            return Language.GetTextValue("Mods.StellarisShips.DialogText." + str);
        }
    }
}
