using Terraria;

namespace StellarisShips.Static
{
    public static class TextHelper
    {

        public static int GetTextWidth(string text)
        {
            int width = 0;
            int oldWidth = 0;
            foreach (char c in text)
            {
                if (c == '\n')
                {
                    if (width > oldWidth)
                    {
                        oldWidth = width;
                    }
                    width = 0;
                }
                else
                {
                    width += Terraria.GameContent.FontAssets.MouseText.Value.MeasureString(c.ToString()).ToPoint().X;
                }
            }
            return int.Max(width, oldWidth);
        }


    }
}
