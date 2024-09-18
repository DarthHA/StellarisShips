using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.UI;
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

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.WorksInVoidBag[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.rare = ItemRarityID.Cyan;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!player.inventory[58].IsAir)             //不能拿在手上使用
            {
                return false;
            }
            if (!UIManager.ShipDesignVisible && !UIManager.ShipBuildVisible)
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


        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            SomeUtils.AddLight(Item.Center, Color.Cyan);
        }
    }
}
