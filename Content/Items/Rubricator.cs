using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using StellarisShips.System;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class Rubricator : ModItem
    {
        int MiscTimer = 0;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
        }

        public override void PostUpdate()
        {
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            SomeUtils.AddLight(Item.Center, Color.Purple, 1.5f * a + 0.5f);
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
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            EasyDraw.AnotherDraw(BlendState.Additive);
            spriteBatch.Draw(ModContent.Request<Texture2D>("StellarisShips/Content/Items/Rubricator_Glow").Value, position, frame, Color.White * a, 0, origin, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)Math.Sin(MiscTimer / 120f * MathHelper.TwoPi);
            EasyDraw.AnotherDraw(BlendState.Additive);
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Content/Items/Rubricator_Glow").Value;
            spriteBatch.Draw(tex, Item.Center - Main.screenPosition, null, Color.White * a, rotation, tex.Size() / 2f, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }
    }
}
