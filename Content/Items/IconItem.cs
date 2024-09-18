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
    public class Slot_G : Slot_A { }
    public class Slot_H : Slot_A { }
    public class Slot_L : Slot_A { }
    public class Slot_M : Slot_A { }
    public class Slot_P : Slot_A { }
    public class Slot_S : Slot_A { }
    public class Slot_T : Slot_A { }
    public class Slot_X : Slot_A { }

    public class MR_Icon : Slot_A { }
    public class Aggro_Icon : Slot_A { }
    public class Armor_Icon : Slot_A { }
    public class Cloak_Icon : Slot_A { }
    public class Cost_Icon : Slot_A { }
    public class Damage_Icon : Slot_A { }
    public class Detect_Icon : Slot_A { }
    public class Evasion_Icon : Slot_A { }
    public class Hull_Icon : Slot_A { }
    public class HullRegen_Icon : Slot_A { }
    public class Power_Icon : Slot_A { }
    public class Shield_Icon : Slot_A { }
    public class ShieldRegen_Icon : Slot_A { }
    public class Special_Icon : Slot_A { }
    public class Speed_Icon : Slot_A { }
    public class Influence_Icon : Slot_A { }
}
