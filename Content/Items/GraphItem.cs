using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.UI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StellarisShips.Content.Items
{
    public class GraphItem : ModItem
    {
        public ShipGraph graph = new();
        public string DataDesc = "";

        int MiscTimer = 0;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.WorksInVoidBag[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.rare = ItemRarityID.Cyan;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                if (!player.inventory[58].IsAir)             //不能拿在手上使用
                {
                    return false;
                }
                if (UIManager.LeaderUIVisible)
                {
                    LeaderUI.Close();
                }
                if (!UIManager.AnyUIVisible())
                {
                    SomeUtils.PlaySound(SoundPath.UI + "Click");
                    UIManager.ShipDesignVisible = true;
                    UIManager.ResetClick();
                    if (graph.ShipType == "")
                    {
                        ShipDesignUI.StartForSelectShipType();
                    }
                    else
                    {
                        ShipDesignUI.StartForNormal(graph);
                    }
                }
            }
            else
            {
                Item item = null;
                for (int i = 0; i < 58; i++)
                {
                    if (player.inventory[i].type == ModContent.ItemType<ConnecterItem>() && player.inventory[i].stack > 0)
                    {
                        item = player.inventory[i];
                        break;
                    }
                }

                if (item != null)
                {
                    if (item.ModItem != null)
                    {
                        item.ModItem.Shoot(player, source, position, velocity, type, damage, knockback);
                    }
                }
            }
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            if (graph.ShipType != "")
            {
                string name = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphName"), graph.GraphName, EverythingLibrary.Ships[graph.ShipType].GetLocalizedName());
                Item.SetNameOverride(name);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name == "ItemName")
                {
                    tooltips.Insert(i + 1, new TooltipLine(Mod, "DataDesc", DataDesc));
                    break;
                }
            }
        }

        protected override bool CloneNewInstances => true;

        public override ModItem Clone(Item newEntity)
        {
            GraphItem newItem = (GraphItem)base.Clone(newEntity);

            newItem.graph = graph.Copy();
            newItem.DataDesc = DataDesc;
            return newItem;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("Graph", JsonConvert.SerializeObject(graph));
            tag.Add("DataDesc", DataDesc);
        }
        public override void LoadData(TagCompound tag)
        {
            graph = JsonConvert.DeserializeObject<ShipGraph>(tag.Get<string>("Graph"));
            DataDesc = tag.Get<string>("DataDesc");
        }


        public override void PostUpdate()
        {
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            SomeUtils.AddLight(Item.Center, Color.Cyan, 1.5f * a + 0.5f);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)Math.Sin(MiscTimer / 120f * MathHelper.TwoPi);
            EasyDraw.AnotherDraw(BlendState.Additive);
            spriteBatch.Draw(ModContent.Request<Texture2D>("StellarisShips/Content/Items/GraphItem_Glow").Value, position, frame, Color.White * a, 0, origin, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            MiscTimer = (MiscTimer + 1) % 120;
            float a = (float)(Math.Sin(MiscTimer / 120f * MathHelper.TwoPi) + 1) / 2f;
            EasyDraw.AnotherDraw(BlendState.Additive);
            Texture2D tex = ModContent.Request<Texture2D>("StellarisShips/Content/Items/GraphItem_Glow").Value;
            spriteBatch.Draw(tex, Item.Center - Main.screenPosition, null, Color.White * a, rotation, tex.Size() / 2f, scale, SpriteEffects.None, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }
    }
}
