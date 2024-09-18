using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class Rubricator : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.rare = ItemRarityID.Master;
        }

        public override void PostUpdate()
        {
            SomeUtils.AddLight(Item.Center, Color.Purple, 3);
            if (ProgressHelper.DiscoveredMR > 0)
            {
                Item.TurnToAir();
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (ProgressHelper.DiscoveredMR > 0)
            {
                Item.TurnToAir();
            }
        }
    }
}
