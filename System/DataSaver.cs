using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StellarisShips.System
{
    public class DataSaver : ModSystem
    {
        public override void LoadWorldData(TagCompound tag)
        {
            List<Vector2> shipPos = new();
            List<string> shipGraphs = new();
            List<int> shipHull = new();
            List<int> shipShield = new();
            shipPos = tag.Get<List<Vector2>>("ShipPos");
            shipGraphs = tag.Get<List<string>>("ShipGraphs");
            shipHull = tag.Get<List<int>>("ShipHull");
            shipShield = tag.Get<List<int>>("ShipShield");
            for (int i = 0; i < shipGraphs.Count; i++)
            {
                ShipGraph graph = JsonConvert.DeserializeObject<ShipGraph>(shipGraphs[i]);
                int npctmp = ShipNPC.BuildAShip(null, shipPos[i], graph);
                if (npctmp >= 0 && npctmp < 200)
                {
                    Main.npc[npctmp].rotation = Main.rand.NextFloat() * MathHelper.TwoPi;
                    Main.npc[npctmp].GetShipNPC().CurrentShield = shipShield[i];
                    Main.npc[npctmp].life = shipHull[i] + shipShield[i];
                }
            }

            ShapeSystem.TipPos = tag.Get<Vector2>("TipPos");
            ShapeSystem.TipRotation = tag.GetFloat("TipRotation");
            ShapeSystem.Following = tag.GetBool("Following");
            ShapeSystem.Passive = tag.GetBool("Passive");

            ProgressHelper.FirstContract = tag.GetBool("FirstContract");
            ProgressHelper.CurrentProgress = tag.GetInt("CurrentProgress");
            ProgressHelper.DiscoveredMR = tag.GetInt("DiscoveredMR");
        }


        public override void SaveWorldData(TagCompound tag)
        {
            List<string> shipGraphs = new();
            List<Vector2> shipPos = new();
            List<int> shipHull = new();
            List<int> shipShield = new();
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>())
                {
                    if (npc.GetShipNPC().ShipGraph != null && npc.GetShipNPC().ShipGraph.ShipType != "")
                    {
                        shipGraphs.Add(JsonConvert.SerializeObject(npc.GetShipNPC().ShipGraph));
                        shipShield.Add(npc.GetShipNPC().CurrentShield);
                        shipHull.Add(npc.life - npc.GetShipNPC().CurrentShield);
                        if (npc.GetShipNPC().shipAI == ShipAI.Missing)           //失踪
                        {
                            shipPos.Add(new Vector2(50 + Main.rand.Next(Main.maxTilesX - 100), 50 + Main.rand.Next(Main.maxTilesY - 100)) * 16f);
                        }
                        else
                        {
                            shipPos.Add(npc.Center);
                        }
                    }
                }
            }
            tag.Add("ShipGraphs", shipGraphs);
            tag.Add("ShipPos", shipPos);
            tag.Add("ShipHull", shipHull);
            tag.Add("ShipShield", shipShield);

            tag.Add("TipPos", ShapeSystem.TipPos);
            tag.Add("TipRotation", ShapeSystem.TipRotation);
            tag.Add("Following", ShapeSystem.Following);
            tag.Add("Passive", ShapeSystem.Passive);

            tag.Add("FirstContract", ProgressHelper.FirstContract);
            tag.Add("CurrentProgress", ProgressHelper.CurrentProgress);
            tag.Add("DiscoveredMR", ProgressHelper.DiscoveredMR);
        }

    }
}
