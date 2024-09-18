using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace StellarisShips
{
    public class StellarisShips : Mod
    {
        public static Effect NormalVSEffect;
        public static Effect OffgasEffect;
        public static Effect SpherePerspective;
        public static Effect HitEffect;
        public override void Load()
        {
            NormalVSEffect = ModContent.Request<Effect>("StellarisShips/Effects/NormalTrailEffect", AssetRequestMode.ImmediateLoad).Value;
            OffgasEffect = ModContent.Request<Effect>("StellarisShips/Effects/OffgasEffect", AssetRequestMode.ImmediateLoad).Value;
            SpherePerspective = ModContent.Request<Effect>("StellarisShips/Effects/SpherePerspective", AssetRequestMode.ImmediateLoad).Value;
            HitEffect = ModContent.Request<Effect>("StellarisShips/Effects/HitEffect", AssetRequestMode.ImmediateLoad).Value;
        }

        public override void Unload()
        {
            OffgasEffect = null;
            NormalVSEffect = null;
            SpherePerspective = null;
            HitEffect = null;
        }
    }
}
