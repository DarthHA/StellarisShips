using System.Collections.Generic;

namespace StellarisShips.System
{
    public class ShipGraph
    {
        /// <summary>
        /// 取名，一般从随机词库里选，主要为了区分
        /// </summary>
        public string GraphName = "";
        /// <summary>
        /// 船的类型
        /// </summary>
        public string ShipType = "";
        /// <summary>
        /// 核心组件
        /// </summary>
        public List<string> CoreComponent = new();
        /// <summary>
        /// 区段，用于储存其他组件
        /// </summary>
        public List<SectionForSave> Parts = new();

        /// <summary>
        /// 常规造价
        /// </summary>
        public long Value = 0;

        /// <summary>
        /// 稀有文物造价
        /// </summary>
        public int MRValue = 0;

        public ShipGraph Copy()
        {
            ShipGraph other = new();
            other.GraphName = GraphName;
            other.ShipType = ShipType;
            other.Value = Value;
            other.MRValue = MRValue;
            foreach (string component in CoreComponent)
            {
                other.CoreComponent.Add(component);
            }
            foreach (SectionForSave section in Parts)
            {
                other.Parts.Add(section.Copy());
            }
            return other;
        }
    }
}
