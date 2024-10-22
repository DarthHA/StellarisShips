using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public class LeaderNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool IsALeader = false;

        public override bool PreAI(NPC npc)
        {
            if (GetShip(npc) != -1)
            {
                if (!IsALeader)
                {
                    npc.dontTakeDamageFromHostiles = true;
                    npc.dontTakeDamage = true;
                    IsALeader = true;
                }
            }
            else
            {
                if (IsALeader)
                {
                    NPC npctmp = new();
                    npctmp.SetDefaults(npc.type);
                    npc.dontTakeDamageFromHostiles = npctmp.dontTakeDamageFromHostiles;
                    npc.dontTakeDamage = npctmp.dontTakeDamage;
                    IsALeader = false;
                }
            }

            if (IsALeader)
            {
                npc.velocity = Vector2.Zero;
                return false;
            }

            return true;
        }

        public override bool? CanChat(NPC npc)
        {
            if (IsALeader) return false;
            return null;
        }

        public override bool? CanBeCaughtBy(NPC npc, Item item, Player player)
        {
            if (IsALeader) return false;
            return null;
        }

        public override bool? CanGoToStatue(NPC npc, bool toKingStatue)
        {
            if (IsALeader) return false;
            return null;
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (IsALeader) return false;
            return null;
        }
        public override void ModifyHoverBoundingBox(NPC npc, ref Rectangle boundingBox)
        {
            if (IsALeader)
            {
                boundingBox = new Rectangle(0, 0, 0, 0);
            }
        }

        public static int GetShip(NPC npc)
        {
            if (!npc.CountAsTownNPC()) return -1;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive(true))
                {
                    if (ship.GetShipNPC().GetLeader() == npc.whoAmI)
                    {
                        return ship.whoAmI;
                    }
                }
            }
            return -1;
        }
    }

    public class DisableNPCHeadDraw : ModSystem
    {
        internal static bool DrawInMap = false;
        public override void Load()
        {
            On_Main.DrawMap += On_Main_DrawMap;
            On_Main.DrawNPCHeadFriendly += On_Main_DrawNPCHeadFriendly;
            On_Main.DrawNPC += On_Main_DrawNPC;
        }


        public override void Unload()
        {
            On_Main.DrawMap -= On_Main_DrawMap;
            On_Main.DrawNPCHeadFriendly -= On_Main_DrawNPCHeadFriendly;
            On_Main.DrawNPC -= On_Main_DrawNPC;
        }

        private void On_Main_DrawMap(On_Main.orig_DrawMap orig, Main self, GameTime gameTime)
        {
            DrawInMap = true;
            orig.Invoke(self, gameTime);
            DrawInMap = false;
        }

        private void On_Main_DrawNPCHeadFriendly(On_Main.orig_DrawNPCHeadFriendly orig, Entity theNPC, byte alpha, float headScale, SpriteEffects dir, int townHeadId, float x, float y)
        {
            if (DrawInMap)
            {
                if (theNPC is NPC)
                {
                    NPC npc = theNPC as NPC;
                    if (npc.TryGetGlobalNPC(out LeaderNPC gNPC))
                    {
                        if (gNPC.IsALeader)
                        {
                            return;
                        }
                    }
                }
            }
            orig.Invoke(theNPC, alpha, headScale, dir, townHeadId, x, y);
        }

        private void On_Main_DrawNPC(On_Main.orig_DrawNPC orig, Main self, int iNPCIndex, bool behindTiles)
        {
            if (Main.npc[iNPCIndex].active)
            {
                NPC npc = Main.npc[iNPCIndex];
                if (npc.TryGetGlobalNPC(out LeaderNPC gNPC))
                {
                    if (gNPC.IsALeader)
                    {
                        return;
                    }
                }
            }
            orig.Invoke(self, iNPCIndex, behindTiles);
        }
    }
}
