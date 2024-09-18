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
    public class Destroyer : BaseShip
    {
        public override string InternalName => ShipIDs.Destroyer;
        public override int PartCount => 2;
        public override List<List<string>> CanUseSections => new()
        {
            new() { SectionIDs.Destroyer_Artillery_Bow, SectionIDs.Destroyer_Gunship_Bow, SectionIDs.Destroyer_PicketShip_Bow},
            new() { SectionIDs.Destroyer_Gunship_Stern, SectionIDs.Destroyer_Interceptor_Stern ,SectionIDs.Destroyer_PicketShip_Stern}
        };
        public override List<string> CanUseComputer => new() { ComputerID.Line, ComputerID.Picket, ComputerID.Artillery };
        public override string InitialComputer => ComputerID.Picket;
        public override int CP => 2;
        public override int Length => 120;
        public override int Width => 50;
        public override float WeaponScale => 0.8f;
        public override float ShipScale => 0.35f;

        public override int BaseHull => 1800;
        public override int BaseSpeed => 14;
        public override int BaseEvasion => 35;

        public override long Value => 60 * 500;

        public override int Progress => 2;

        public override void DrawTrail(SpriteBatch spriteBatch, Vector2 screenPos, NPC ship)
        {
            int trailLength = 50;
            ShipNPC shipNPC = ship.GetShipNPC();
            Texture2D texTrail = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/TrackEffect", AssetRequestMode.ImmediateLoad).Value;
            void DrawOffgas(Vector2 DrawPos)
            {
                Vector2 scale = new(0.75f * shipNPC.shipScale * 1.1f, 0.75f * shipNPC.shipScale * 1.1f);
                scale.X *= MathHelper.Clamp(ship.velocity.Length() / shipNPC.BaseSpeed * 4, 0.1f, 1f);
                EasyDraw.AnotherDraw(SpriteSortMode.Immediate, BlendState.Additive);
                StellarisShips.OffgasEffect.Parameters["color"].SetValue(Color.Cyan.ToVector4());
                StellarisShips.OffgasEffect.Parameters["r"].SetValue(Main.rand.NextFloat());
                StellarisShips.OffgasEffect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(texTrail, DrawPos - screenPos, null, Color.Cyan, ship.rotation, new Vector2(texTrail.Width, texTrail.Height / 2), scale, SpriteEffects.None, 0);
                EasyDraw.AnotherDraw(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            };
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation));
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 8f + (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 8f);
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 8f - (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 8f);

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
            DrawTrail(4, Color.Cyan);
            DrawTrail(2, Color.White * 0.75f);

        }

        public Vector2 GetTailPos(NPC ship, Vector2 shipPos, float rot)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            return shipPos - shipNPC.ShipTexture.Width * rot.ToRotationVector2() * 0.75f * shipNPC.shipScale * 0.5f * 0.9f;
        }
    }
}