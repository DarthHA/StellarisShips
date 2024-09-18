using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.Items
{
    public class Slot_A : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory[Item.type] = true;
        }
    }
    public class Slot_G : Slot_A
    {
    }
    public class Slot_H : Slot_A
    {
    }
    public class Slot_L : Slot_A
    {
    }
    public class Slot_M : Slot_A
    {
    }
    public class Slot_P : Slot_A
    {
    }
    public class Slot_S : Slot_A
    {
    }
    public class Slot_T : Slot_A
    {
    }
    public class Slot_X : Slot_A
    {
    }
}