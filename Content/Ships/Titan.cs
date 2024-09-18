using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.Ships
{
    public class Titan : BaseShip
    {
        public override string InternalName => ShipIDs.Titan;
        public override int PartCount => 3;
        public override List<List<string>> CanUseSections => new()
        {
            new() { SectionIDs.Titan_Bow},
            new() { SectionIDs.Titan_Core},
            new() { SectionIDs.Titan_Stern},
        };
        public override List<string> CanUseComputer => new() { ComputerID.Artillery, ComputerID.Line };
        public override string InitialComputer => ComputerID.Artillery;
        public override int CP => 16;
        public override int Length => 480;
        public override int Width => 180;
        public override float WeaponScale => 0.3f;
        public override float ShipScale => 0.9f;

        public override int BaseHull => 54000;
        public override int BaseSpeed => 4;
        public override int BaseEvasion => 0;

        public override long Value => 480 * 1750;

        public override int Progress => 9;

        public override bool HasAura => true;

        public override int MaxCount => 1;

        public override void DrawTrail(SpriteBatch spriteBatch, Vector2 screenPos, NPC ship)
        {
            int trailLength = 100;
            ShipNPC shipNPC = ship.GetShipNPC();
            Texture2D texTrail = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/TrackEffect", AssetRequestMode.ImmediateLoad).Value;
            void DrawOffgas(Vector2 DrawPos)
            {
                Vector2 scale = new(0.75f * shipNPC.shipScale * 1.1f, 0.75f * shipNPC.shipScale * 1.1f);
                scale.X *= MathHelper.Clamp(ship.velocity.Length() / shipNPC.BaseSpeed * 2, 0.1f, 1f);
                EasyDraw.AnotherDraw(SpriteSortMode.Immediate, BlendState.Additive);
                StellarisShips.OffgasEffect.Parameters["color"].SetValue(Color.Cyan.ToVector4());
                StellarisShips.OffgasEffect.Parameters["r"].SetValue(Main.rand.NextFloat());
                StellarisShips.OffgasEffect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(texTrail, DrawPos - screenPos, null, Color.Cyan, ship.rotation, new Vector2(texTrail.Width, texTrail.Height / 2), scale, SpriteEffects.None, 0);
                EasyDraw.AnotherDraw(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            }
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 2f);
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 9f + (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 10f);
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 9f - (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 10f);

            Texture2D texExtra = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BlobGlow2", AssetRequestMode.ImmediateLoad).Value;
            void DrawTrail(float width, Color color)
            {
                List<CustomVertexInfo> vertexInfos = new();
                Vector2 UnitY = (ship.rotation + MathHelper.Pi / 2).ToRotationVector2();
                Vector2 CenterOffset = ship.Size / 2f;
                vertexInfos.Add(new CustomVertexInfo(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 5 + UnitY * width, Color.White, new Vector3(0, 0f, 1)));
                vertexInfos.Add(new CustomVertexInfo(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 5 - UnitY * width, Color.White, new Vector3(0, 1f, 1)));
                float realLen = -1;
                for (int i = trailLength - 1; i >= 0; i--)
                {
                    if (ship.oldPos[i] == Vector2.Zero)
                    {
                        continue;
                    }
                    else
                    {
                        if (realLen == -1)
                        {
                            realLen = i + 1;
                            break;
                        }
                    }
                }
                for (int i = 0; i < realLen; i++)
                {
                    float progress = 0.1f + i / (realLen - 1f) * 0.9f;
                    UnitY = (ship.oldRot[i] + MathHelper.Pi / 2).ToRotationVector2();
                    vertexInfos.Add(new CustomVertexInfo(GetTailPos(ship, ship.oldPos[i] + CenterOffset, ship.oldRot[i]) + UnitY * width, Color.White, new Vector3(progress, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(GetTailPos(ship, ship.oldPos[i] + CenterOffset, ship.oldRot[i]) - UnitY * width, Color.White, new Vector3(progress, 1f, 1)));
                }
                DrawUtils.DrawTrail(texExtra, vertexInfos, Main.spriteBatch, color * 0.8f, BlendState.Additive);
            }
            DrawTrail(12, Color.Cyan);
            DrawTrail(9, Color.White * 0.75f);
            DrawTrail(5, Color.White * 0.75f);

        }

        public Vector2 GetTailPos(NPC ship, Vector2 shipPos, float rot)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            return shipPos - shipNPC.ShipTexture.Width * rot.ToRotationVector2() * 0.75f * shipNPC.shipScale * 0.5f * 0.9f - rot.ToRotationVector2() * shipNPC.TailDrawOffset * shipNPC.shipScale;
        }
    }
}