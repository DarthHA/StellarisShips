using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class ConnecterItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.WorksInVoidBag[Item.type] = true;
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
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
            if (!UIManager.ShipDesignVisible && !UIManager.ShipBuildVisible)
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
                    else
                    {
                        ShipBuildUI.Start("NormalStart");
                    }
                }
            }
            return false;
        }
    }
}
