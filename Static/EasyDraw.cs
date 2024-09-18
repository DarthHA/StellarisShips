using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria;

namespace StellarisShips.Static
{
    public static class EasyDraw
    {
        public static void AnotherDraw(BlendState blendState)
        {
            if (IsDrawBegin())
            {
                Main.spriteBatch.End();
            }
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            SamplerState samplerState = (SamplerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            DepthStencilState depthStencilState = (DepthStencilState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            RasterizerState rasterizerState = (RasterizerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            Effect effect = (Effect)fieldInfo.GetValue(Main.spriteBatch);


            fieldInfo = Main.spriteBatch.GetType().GetField("transformMatrix", BindingFlags.NonPublic | BindingFlags.Instance);
            Matrix matrix = (Matrix)fieldInfo.GetValue(Main.spriteBatch);


            Main.spriteBatch.Begin(SpriteSortMode.Deferred, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void AnotherDraw(SpriteSortMode spriteSortMode)
        {
            if (IsDrawBegin())
            {
                Main.spriteBatch.End();
            }
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("blendState", BindingFlags.NonPublic | BindingFlags.Instance);
            BlendState blendState = (BlendState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            SamplerState samplerState = (SamplerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            DepthStencilState depthStencilState = (DepthStencilState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            RasterizerState rasterizerState = (RasterizerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            Effect effect = (Effect)fieldInfo.GetValue(Main.spriteBatch);


            fieldInfo = Main.spriteBatch.GetType().GetField("transformMatrix", BindingFlags.NonPublic | BindingFlags.Instance);
            Matrix matrix = (Matrix)fieldInfo.GetValue(Main.spriteBatch);


            Main.spriteBatch.Begin(spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void AnotherDraw(SpriteSortMode spriteSortMode, BlendState blendState)
        {
            if (IsDrawBegin())
            {
                Main.spriteBatch.End();
            }

            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            SamplerState samplerState = (SamplerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            DepthStencilState depthStencilState = (DepthStencilState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            RasterizerState rasterizerState = (RasterizerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            Effect effect = (Effect)fieldInfo.GetValue(Main.spriteBatch);


            fieldInfo = Main.spriteBatch.GetType().GetField("transformMatrix", BindingFlags.NonPublic | BindingFlags.Instance);
            Matrix matrix = (Matrix)fieldInfo.GetValue(Main.spriteBatch);


            Main.spriteBatch.Begin(spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void AnotherDraw(BlendState blendState, Matrix matrix)
        {
            if (IsDrawBegin())
            {
                Main.spriteBatch.End();
            }
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            SamplerState samplerState = (SamplerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            DepthStencilState depthStencilState = (DepthStencilState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            RasterizerState rasterizerState = (RasterizerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            Effect effect = (Effect)fieldInfo.GetValue(Main.spriteBatch);

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }

        public static void AnotherDraw(SpriteSortMode spriteSortMode, BlendState blendState, Matrix matrix)
        {
            if (IsDrawBegin())
            {
                Main.spriteBatch.End();
            }
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("samplerState", BindingFlags.NonPublic | BindingFlags.Instance);
            SamplerState samplerState = (SamplerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("depthStencilState", BindingFlags.NonPublic | BindingFlags.Instance);
            DepthStencilState depthStencilState = (DepthStencilState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("rasterizerState", BindingFlags.NonPublic | BindingFlags.Instance);
            RasterizerState rasterizerState = (RasterizerState)fieldInfo.GetValue(Main.spriteBatch);

            fieldInfo = Main.spriteBatch.GetType().GetField("customEffect", BindingFlags.NonPublic | BindingFlags.Instance);
            Effect effect = (Effect)fieldInfo.GetValue(Main.spriteBatch);


            Main.spriteBatch.Begin(spriteSortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }


        public static Matrix GetMatrix()
        {
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("transformMatrix", BindingFlags.NonPublic | BindingFlags.Instance);
            return (Matrix)fieldInfo.GetValue(Main.spriteBatch);
        }

        /// <summary>
        /// 获得混合模式
        /// </summary>
        /// <returns></returns>
        public static BlendState GetBlendState()
        {
            if (!IsDrawBegin())
            {
                return BlendState.AlphaBlend;
            }
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("blendState", BindingFlags.NonPublic | BindingFlags.Instance);
            return (BlendState)fieldInfo.GetValue(Main.spriteBatch);
        }


        /// <summary>
        /// 判断绘制是否在进行
        /// </summary>
        /// <returns></returns>
        public static bool IsDrawBegin()
        {
            FieldInfo fieldInfo = Main.spriteBatch.GetType().GetField("beginCalled", BindingFlags.NonPublic | BindingFlags.Instance);
            return (bool)fieldInfo.GetValue(Main.spriteBatch);
        }



    }
}