using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.UI;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class ConnecterItem : ModItem
    {
        int MiscTimer = 0;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.WorksInVoidBag[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ProjectileID.BeeArrow;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (SomeUtils.AnyBosses())
            {
                Main.NewText(Language.GetTextValue("Mods.StellarisShips.UI.NoSignal"), Color.Orange);
                return false;
            }
            if (UIManager.LeaderUIVisible)
            {
                LeaderUI.Close();
            }
            if (!UIManager.AnyUIVisible())
            {
                UIManager.ShipBuildVisible = true;
                UIManager.ResetClick();
                SomeUtils.PlaySound(SoundPath.UI + "Click");
                SomeUtils.PlaySoundRandom(SoundPath.Other + "Voice", 5);
                if (ProgressHelper.FirstContract)
                {
                    ProgressHelper.FirstContract = false;
                    ShipBuildUI.Start("FirstContract");
                    ProgressHelper.CurrentProgress = ProgressHelper.GetCurrentProgress();
                    ProgressHelper.HasNotification = false;
                }
                else
                {
                    if (ProgressHelper.HasNotification)
                    {
                        ProgressHelper.CurrentProgress = ProgressHelper.GetCurrentProgress();
                        ProgressHelper.HasNotification = false;
                        ShipBuildUI.Start("NewItem");
                    }
                    else if (player.HasItem(ModContent.ItemType<Rubricator>()) && ProgressHelper.DiscoveredMR == 0)
                    {
                        ShipBuildUI.Start("AddMRTech1");
                    }
                    else if (ProgressHelper.DiscoveredMR == 1)
                    {
                        ShipBuildUI.Start("AddMRTech2");
                    }
                    else
                    {
                        ShipBuildUI.Start("NormalStart");
                    }
                }
            }
            return false;
        }

        public override void PostUpdate()
        {
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            SomeUtils.AddLight(Item.Center, Color.LightSkyBlue, 1.5f * a + 0.5f);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)Math.Sin(MiscTimer / 120f * MathHelper.TwoPi);
            EasyDraw.AnotherDraw(BlendState.Additive);
            spriteBatch.Draw(ModContent.Request<Texture2D>("StellarisShips/Content/Items/ConnecterItem_Glow").Value, position, frame, Color.White * a, 0, origin, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Content/Items/ConnecterItem_Glow").Value;
            spriteBatch.Draw(tex, Item.Center - Main.screenPosition, null, Color.White * a, rotation, tex.Size() / 2f, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }
    }
}
