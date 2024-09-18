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
    public class Corvette : BaseShip
    {
        public override string InternalName => ShipIDs.Corvette;
        public override int PartCount => 1;
        public override List<List<string>> CanUseSections => new() { new() { SectionIDs.Corvette_Interceptor_Core, SectionIDs.Corvette_PicketShip_Core, SectionIDs.Corvette_MissileBoat_Core } };
        public override List<string> CanUseComputer => new() { ComputerID.Swarm, ComputerID.Picket, ComputerID.Bomber };
        public override string InitialComputer => ComputerID.Swarm;
        public override int CP => 1;
        public override int Length => 70;
        public override int Width => 32;
        public override float WeaponScale => 1f;
        public override float ShipScale => 0.25f;

        public override int BaseHull => 600;
        public override int BaseSpeed => 8;
        public override int BaseEvasion => 60;

        public override long Value => 30 * 400;

        public override void DrawTrail(SpriteBatch spriteBatch, Vector2 screenPos, NPC ship)
        {
            int trailLength = 40;
            Texture2D texExtra = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BlobGlow2", AssetRequestMode.ImmediateLoad).Value;
            List<CustomVertexInfo> vertexInfos = new();
            Vector2 UnitY = (ship.rotation + MathHelper.Pi / 2).ToRotationVector2();
            Vector2 CenterOffset = ship.Size / 2f;
            float width = 3;
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

            DrawUtils.DrawTrail(texExtra, vertexInfos, Main.spriteBatch, Color.Cyan * 0.8f, BlendState.Additive);

        }

        public Vector2 GetTailPos(NPC ship, Vector2 shipPos, float rot)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            return shipPos - shipNPC.ShipTexture.Width * rot.ToRotationVector2() * 0.75f * shipNPC.shipScale * 0.5f * 0.9f;
        }
    }
}