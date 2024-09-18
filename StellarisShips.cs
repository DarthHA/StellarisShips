using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria.ModLoader;

namespace StellarisShips
{
    public class StellarisShips : Mod
    {
        public static Effect NormalTrailEffect;
        public static Effect LoopTrailEffect;
        public static Effect OffgasEffect;
        public static Effect SpherePerspective;
        public static Effect HitEffect;
        public override void Load()
        {
            FieldInfo[] f = typeof(StellarisShips).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo info in f)
            {
                if (info.FieldType == typeof(Effect))
                {
                    info.SetValue(this, ModContent.Request<Effect>("StellarisShips/Effects/" + info.Name, AssetRequestMode.ImmediateLoad).Value);
                }
            }
        }

        public override void Unload()
        {
            FieldInfo[] f = typeof(StellarisShips).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo info in f)
            {
                if (info.FieldType == typeof(Effect))
                {
                    info.SetValue(this, null);
                }
            }
        }
    }
}
