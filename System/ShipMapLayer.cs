using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using Terraria;
using Terraria.DataStructures;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.System
{
    public class BeeMapLayer : ModMapLayer
    {
        public override void Draw(ref MapOverlayDrawContext context, ref string text)
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>())
                {
                    if (npc.GetShipNPC().ShipGraph.ShipType != "")
                    {
                        Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Images/OtherIcons/" + npc.GetShipNPC().ShipGraph.ShipType + "_MapIcon").Value;
                        if (npc.GetShipNPC().shipAI == ShipAI.Missing)
                        {
                            tex = ModContent.Request<Texture2D>("StellarisShips/Images/OtherIcons/Missing_MapIcon").Value;
                        }
                        context.Draw(tex, npc.Center / 16f, new SpriteFrame(1, 1, 0, 0), Alignment.Center);
                    }
                }

            }

        }
    }

}