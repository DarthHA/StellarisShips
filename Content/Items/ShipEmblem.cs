using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class ShipEmblem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Master;
        }

    }
}
