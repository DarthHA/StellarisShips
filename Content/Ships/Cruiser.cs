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
    public class Cruiser : BaseShip
    {
        public override string InternalName => ShipIDs.Cruiser;
        public override int PartCount => 3;
        public override List<List<string>> CanUseSections => new()
        {
            new() { SectionIDs.Cruiser_Artillery_Bow, SectionIDs.Cruiser_Broadside_Bow, SectionIDs.Cruiser_Torpedo_Bow},
            new() { SectionIDs.Cruiser_Artillery_Core, SectionIDs.Cruiser_Broadside_Core ,SectionIDs.Cruiser_Hangar_Core, SectionIDs.Cruiser_Torpedo_Core},
            new() { SectionIDs.Cruiser_Broadside_Stern, SectionIDs.Cruiser_Gunship_Stern},
        };
        public override List<string> CanUseComputer => new() { ComputerID.Line, ComputerID.Picket, ComputerID.Artillery, ComputerID.Carrier, ComputerID.Bomber };
        public override string InitialComputer => ComputerID.Line;
        public override int CP => 4;
        public override int Length => 260;
        public override int Width => 100;
        public override float WeaponScale => 0.5f;
        public override float ShipScale => 0.55f;

        public override int BaseHull => 5400;
        public override int BaseSpeed => 12;
        public override int BaseEvasion => 10;

        public override long Value => 120 * 1000;

        public override int Progress => 5;

        public override void DrawTrail(SpriteBatch spriteBatch, Vector2 screenPos, NPC ship)
        {
            int trailLength = 65;
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
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 8f + (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 16f);
            DrawOffgas(GetTailPos(ship, ship.Center, ship.rotation) + ship.rotation.ToRotationVector2() * 8f - (ship.rotation + MathHelper.Pi / 2f).ToRotationVector2() * 16f);

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
            DrawTrail(6, Color.Cyan);
            DrawTrail(4, Color.White * 0.75f);
            DrawTrail(2, Color.White * 0.75f);
        }


        public Vector2 GetTailPos(NPC ship, Vector2 shipPos, float rot)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            return shipPos - shipNPC.ShipTexture.Width * rot.ToRotationVector2() * 0.75f * shipNPC.shipScale * 0.5f * 0.9f;
        }
    }
}