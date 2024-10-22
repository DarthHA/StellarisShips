using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public partial class ShipNPC : ModNPC
    {
        public string LeaderMod = "";
        public string LeaderModNPC = "";
        public int LeaderVanillaType = -1;
        public int LeaderWhoAmI = -1;
        public string LeaderName = "";

        public bool HasLeader()
        {
            CheckLeader();
            return (LeaderMod != "" && LeaderModNPC != "") || LeaderVanillaType != -1;
        }

        public int GetLeader()
        {
            if (!HasLeader()) return -1;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (LeaderModNPC != "")
                {
                    if (npc.ModNPC != null)
                    {
                        if (npc.ModNPC.Mod.Name == LeaderMod && npc.ModNPC.Name == LeaderModNPC)
                        {
                            return npc.whoAmI;
                        }
                    }
                }
                else
                {
                    if (npc.type == LeaderVanillaType)
                    {
                        return npc.whoAmI;
                    }
                }
            }
            return -1;
        }

        public void AssignLeader(NPC npc)
        {
            if (!npc.CountAsTownNPC()) return;

            LeaderWhoAmI = npc.whoAmI;
            if (npc.ModNPC != null)
            {
                LeaderMod = npc.ModNPC.Mod.Name;
                LeaderModNPC = npc.ModNPC.Name;
                LeaderVanillaType = -1;
            }
            else
            {
                LeaderVanillaType = npc.type;
                LeaderMod = "";
                LeaderModNPC = "";
            }
            LeaderName = npc.GivenOrTypeName;

            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive(true) && ship.whoAmI != NPC.whoAmI)
                {
                    if (ship.GetShipNPC().GetLeader() != -1)
                    {
                        if (ship.GetShipNPC().GetLeader() == npc.whoAmI)
                        {
                            ship.GetShipNPC().ClearLeader();
                        }
                    }
                }
            }
        }

        public void CheckLeader()
        {
            bool ShouldClear = false;
            if (LeaderWhoAmI == -1)
            {
                ShouldClear = true;
            }
            else
            {
                if (!Main.npc[LeaderWhoAmI].CountAsTownNPC())
                {
                    ShouldClear = true;
                }
                else
                {
                    if (Main.npc[LeaderWhoAmI].ModNPC != null)
                    {
                        if (Main.npc[LeaderWhoAmI].ModNPC.Name != LeaderModNPC || Main.npc[LeaderWhoAmI].ModNPC.Mod.Name != LeaderMod || LeaderVanillaType != -1)
                        {
                            ShouldClear = true;
                        }
                    }
                    else
                    {
                        if (Main.npc[LeaderWhoAmI].type != LeaderVanillaType || LeaderMod != "" || LeaderModNPC != "")
                        {
                            ShouldClear = true;
                        }
                    }
                }
            }
            if (ShouldClear)
            {
                ClearLeader();
            }
        }

        public void ClearLeader()
        {
            LeaderMod = "";
            LeaderModNPC = "";
            LeaderVanillaType = -1;
            LeaderWhoAmI = -1;
            LeaderName = "";
        }

        /// <summary>
        /// 用于进图加载
        /// </summary>
        public void FindToAssign()
        {
            int result = -1;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.CountAsTownNPC())
                {
                    if (npc.GivenOrTypeName == LeaderName)
                    {
                        if (LeaderModNPC != "")
                        {
                            if (npc.ModNPC != null)
                            {
                                if (npc.ModNPC.Mod.Name == LeaderMod && npc.ModNPC.Name == LeaderModNPC)
                                {
                                    result = npc.whoAmI;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (LeaderVanillaType == npc.type)
                            {
                                result = npc.whoAmI;
                                break;
                            }
                        }
                    }
                }
            }
            LeaderWhoAmI = result;
            CheckLeader();
        }
    }
}
