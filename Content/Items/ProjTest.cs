using Microsoft.Xna.Framework;
using StellarisShips.Content.Projectiles;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class ProjTest : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Deprecated[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 50;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noUseGraphic = true;
            Item.damage = 114;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Master;
            Item.shoot = ProjectileID.BeeArrow;
            Item.shootSpeed = 10;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ScourgeMissileProj.Summon(source, position, velocity, damage, Color.Green, 1, 10, 1000, -1, 480, 0.05f, false, 0);
            return false;
        }

    }
}
