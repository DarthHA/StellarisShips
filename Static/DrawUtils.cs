
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace StellarisShips.Static
{
    public static class DrawUtils
    {

        public static void DrawTrail(Texture2D tex, List<CustomVertexInfo> bars, SpriteBatch spriteBatch, Color color, BlendState blendState)
        {
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            if (bars.Count > 2)
            {
                for (int k = 0; k < bars.Count - 2; k += 2)
                {
                    triangleList.Add(bars[k]);
                    triangleList.Add(bars[k + 2]);
                    triangleList.Add(bars[k + 1]);
                    triangleList.Add(bars[k + 1]);
                    triangleList.Add(bars[k + 2]);
                    triangleList.Add(bars[k + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;

                /*
                RasterizerState rasterizerState = new();
                rasterizerState.CullMode = CullMode.None;
                rasterizerState.FillMode = FillMode.WireFrame;
                Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;
                */
                Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
                Vector2 screenSize = new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom;
                Matrix projection = Matrix.CreateOrthographicOffCenter(0f, screenSize.X, screenSize.Y, 0f, 0f, 1f);
                Vector2 screenPos = vector - screenSize / 2f;
                Matrix model = Matrix.CreateTranslation(new Vector3(-screenPos.X, -screenPos.Y, 0f));
                if (Main.LocalPlayer.gravDir != 1)
                {
                    projection *= Matrix.CreateScale(1f, -1f, 1f);
                }
                StellarisShips.NormalTrailEffect.Parameters["uTransform"].SetValue(model * projection);
                StellarisShips.NormalTrailEffect.Parameters["color"].SetValue(color.ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = tex;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                StellarisShips.NormalTrailEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(0, triangleList.ToArray(), 0, triangleList.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }

        public static void DrawSphere(Texture2D tex, Vector2 Center, float radius, Color color)
        {
            // 生成两个三角形，注意这里的纹理坐标是 [-1, 1] 这个范围哦
            List<CustomVertexInfo> triangleList = new();
            triangleList.Add(new CustomVertexInfo(Center - new Vector2(radius, radius), Color.White, new Vector3(-1, 1, 0)));
            triangleList.Add(new CustomVertexInfo(Center - new Vector2(radius, -radius), Color.White, new Vector3(-1, -1, 0)));
            triangleList.Add(new CustomVertexInfo(Center - new Vector2(-radius, -radius), Color.White, new Vector3(1, -1, 0)));

            triangleList.Add(new CustomVertexInfo(Center - new Vector2(radius, radius), Color.White, new Vector3(-1, 1, 0)));
            triangleList.Add(new CustomVertexInfo(Center - new Vector2(-radius, -radius), Color.White, new Vector3(1, -1, 0)));
            triangleList.Add(new CustomVertexInfo(Center - new Vector2(-radius, radius), Color.White, new Vector3(1, 1, 0)));

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
            // 干掉注释掉就可以只显示三角形栅格
            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;


            Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
            Vector2 screenSize = new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0f, screenSize.X, screenSize.Y, 0f, 0f, 1f);
            Vector2 screenPos = vector - screenSize / 2f;
            Matrix model = Matrix.CreateTranslation(new Vector3(-screenPos.X, -screenPos.Y, 0f));

            if (Main.LocalPlayer.gravDir != 1)
            {
                projection *= Matrix.CreateScale(1f, -1f, 1f);
            }

            // 把变换和所需信息丢给shader
            StellarisShips.SpherePerspective.Parameters["uTransform"].SetValue(model * projection);
            StellarisShips.SpherePerspective.Parameters["circleCenter"].SetValue(new Vector3(0, 0, -2));
            StellarisShips.SpherePerspective.Parameters["radiusOfCircle"].SetValue(1f);
            StellarisShips.SpherePerspective.Parameters["color"].SetValue(color.ToVector4());
            Main.graphics.GraphicsDevice.Textures[0] = tex;
            Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            StellarisShips.SpherePerspective.CurrentTechnique.Passes[0].Apply();


            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

            Main.graphics.GraphicsDevice.RasterizerState = originalState;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
        }


        public static void DrawLoopTrail(Texture2D tex, List<CustomVertexInfo> bars, SpriteBatch spriteBatch, Color color, BlendState blendState, float LaserLength, float ImageLength, float Progress)
        {
            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
            if (bars.Count > 2)
            {
                for (int k = 0; k < bars.Count - 2; k += 2)
                {
                    triangleList.Add(bars[k]);
                    triangleList.Add(bars[k + 2]);
                    triangleList.Add(bars[k + 1]);
                    triangleList.Add(bars[k + 1]);
                    triangleList.Add(bars[k + 2]);
                    triangleList.Add(bars[k + 3]);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                Vector2 vector = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
                Vector2 screenSize = new Vector2(Main.screenWidth, Main.screenHeight) / Main.GameViewMatrix.Zoom;
                Matrix projection = Matrix.CreateOrthographicOffCenter(0f, screenSize.X, screenSize.Y, 0f, 0f, 1f);
                Vector2 screenPos = vector - screenSize / 2f;
                Matrix model = Matrix.CreateTranslation(new Vector3(-screenPos.X, -screenPos.Y, 0f));
                if (Main.LocalPlayer.gravDir != 1)
                {
                    projection *= Matrix.CreateScale(1f, -1f, 1f);
                }
                StellarisShips.LoopTrailEffect.Parameters["progress"].SetValue(Progress);
                StellarisShips.LoopTrailEffect.Parameters["laserLength"].SetValue(LaserLength);
                StellarisShips.LoopTrailEffect.Parameters["imageLength"].SetValue(ImageLength);
                StellarisShips.LoopTrailEffect.Parameters["uTransform"].SetValue(model * projection);
                StellarisShips.LoopTrailEffect.Parameters["color"].SetValue(color.ToVector4());
                Main.graphics.GraphicsDevice.Textures[0] = tex;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                StellarisShips.LoopTrailEffect.CurrentTechnique.Passes[0].Apply();
                Main.graphics.GraphicsDevice.DrawUserPrimitives(0, triangleList.ToArray(), 0, triangleList.Count / 3);
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
        }

    }
}
