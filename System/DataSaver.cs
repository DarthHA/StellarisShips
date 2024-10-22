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
            List<string> shipData = new();
            shipData = tag.Get<List<string>>("ShipData");
            foreach (string str in shipData)
            {
                ShipSaveUnit data = JsonConvert.DeserializeObject<ShipSaveUnit>(str);
                ShipGraph graph = JsonConvert.DeserializeObject<ShipGraph>(data.shipGraph);
                int npctmp = ShipNPC.BuildAShip(null, data.shipPos, graph, data.shipName);
                if (npctmp >= 0 && npctmp < 200)
                {
                    Main.npc[npctmp].rotation = data.shipRotation;
                    Main.npc[npctmp].GetShipNPC().CurrentShield = data.shipShield;
                    Main.npc[npctmp].life = data.shipHull + data.shipShield;

                    Main.npc[npctmp].GetShipNPC().LeaderMod = data.leaderMod;
                    Main.npc[npctmp].GetShipNPC().LeaderModNPC = data.leaderModNPC;
                    Main.npc[npctmp].GetShipNPC().LeaderName = data.leaderName;
                    Main.npc[npctmp].GetShipNPC().LeaderVanillaType = data.leaderVanillaType;
                    Main.npc[npctmp].GetShipNPC().FindToAssign();
                }
            }

            FleetSystem.TipPos = tag.Get<Vector2>("TipPos");
            FleetSystem.TipRotation = tag.GetFloat("TipRotation");
            FleetSystem.Following = tag.GetBool("Following");
            FleetSystem.Passive = tag.GetBool("Passive");

            ProgressHelper.FirstContract = tag.GetBool("FirstContract");
            ProgressHelper.CurrentProgress = tag.GetInt("CurrentProgress");
            ProgressHelper.DiscoveredMR = tag.GetInt("DiscoveredMR");
            ProgressHelper.UnlockTech = tag.Get<List<string>>("UnlockTech");
            ProgressHelper.FirstContractShroud = tag.GetBool("FirstContractShroud");
            ProgressHelper.PsychoPower = tag.GetInt("PsychoPower");
        }


        public override void SaveWorldData(TagCompound tag)
        {
            List<string> shipData = new();
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>())
                {
                    if (npc.GetShipNPC().ShipGraph != null && npc.GetShipNPC().ShipGraph.ShipType != "")
                    {
                        ShipSaveUnit data = new();
                        data.shipGraph = JsonConvert.SerializeObject(npc.GetShipNPC().ShipGraph);
                        data.shipShield = npc.GetShipNPC().CurrentShield;
                        data.shipHull = npc.life - npc.GetShipNPC().CurrentShield;
                        data.shipName = npc.GetShipNPC().ShipName;
                        data.shipRotation = npc.rotation;
                        if (npc.GetShipNPC().shipAI == ShipAI.Missing)           //失踪
                        {
                            data.shipPos = new Vector2(50 + Main.rand.Next(Main.maxTilesX - 100), 50 + Main.rand.Next(Main.maxTilesY - 100)) * 16f;
                        }
                        else
                        {
                            data.shipPos = npc.Center;
                        }

                        data.leaderMod = npc.GetShipNPC().LeaderMod;
                        data.leaderModNPC = npc.GetShipNPC().LeaderModNPC;
                        data.leaderName = npc.GetShipNPC().LeaderName;
                        data.leaderVanillaType = npc.GetShipNPC().LeaderVanillaType;

                        shipData.Add(JsonConvert.SerializeObject(data));
                    }
                }
            }
            tag.Add("ShipData", shipData);

            tag.Add("TipPos", FleetSystem.TipPos);
            tag.Add("TipRotation", FleetSystem.TipRotation);
            tag.Add("Following", FleetSystem.Following);
            tag.Add("Passive", FleetSystem.Passive);

            tag.Add("FirstContract", ProgressHelper.FirstContract);
            tag.Add("CurrentProgress", ProgressHelper.CurrentProgress);
            tag.Add("DiscoveredMR", ProgressHelper.DiscoveredMR);
            tag.Add("UnlockTech", ProgressHelper.UnlockTech);
            tag.Add("FirstContractShroud", ProgressHelper.FirstContractShroud);
            tag.Add("PsychoPower", ProgressHelper.PsychoPower);
        }

    }

    public class ShipSaveUnit
    {
        public Vector2 shipPos = Vector2.Zero;
        public float shipRotation = 0;
        public string shipGraph = "";
        public int shipHull = 0;
        public int shipShield = 0;
        public string shipName = "";

        public string leaderName = "";
        public int leaderVanillaType = -1;
        public string leaderMod = "";
        public string leaderModNPC = "";
    }
}
