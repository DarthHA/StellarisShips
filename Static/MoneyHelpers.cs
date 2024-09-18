using StellarisShips.Content.NPCs;
using StellarisShips.System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Static
{
    internal static class MoneyHelpers
    {
        public static long GetCopper(long value)
        {
            return value - 1000000 * GetPlatinum(value) - 10000 * GetGold(value) - 100 * GetSilver(value);
        }

        public static int GetCurrentCommandPoint()
        {
            int result = 0;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.type == ModContent.NPCType<ShipNPC>())
                {
                    result += EverythingLibrary.Ships[ship.GetShipNPC().ShipGraph.ShipType].CP;
                }
            }
            return result;
        }
        public static long GetGold(long value)
        {
            return (value - 1000000 * GetPlatinum(value)) / 10000;
        }


        public static long GetPlatinum(long value)
        {
            return value / 1000000;
        }
        public static long GetSilver(long value)
        {
            return (value - 1000000 * GetPlatinum(value) - 10000 * GetGold(value)) / 100;
        }

        public static bool GiveMoney(this Player player, long value)
        {
            if (value <= 0)
                return false;

            Item[] array = new Item[58];
            for (int i = 0; i < 58; i++)
            {
                array[i] = new Item();
                array[i] = player.inventory[i].Clone();
            }

            long num = value;
            if (num < 1)
                num = 1L;

            long num2 = num;

            bool flag = false;
            while (num >= 1000000 && !flag)
            {
                int num3 = -1;
                for (int num4 = 53; num4 >= 0; num4--)
                {
                    if (num3 == -1 && (player.inventory[num4].type == 0 || player.inventory[num4].stack == 0))
                        num3 = num4;

                    while (player.inventory[num4].type == 74 && player.inventory[num4].stack < player.inventory[num4].maxStack && num >= 1000000)
                    {
                        player.inventory[num4].stack++;
                        num -= 1000000;
                        player.DoCoins(num4);
                        if (player.inventory[num4].stack == 0 && num3 == -1)
                            num3 = num4;
                    }
                }

                if (num >= 1000000)
                {
                    if (num3 == -1)
                    {
                        flag = true;
                        continue;
                    }

                    player.inventory[num3].SetDefaults(74);
                    num -= 1000000;
                }
            }

            while (num >= 10000 && !flag)
            {
                int num5 = -1;
                for (int num6 = 53; num6 >= 0; num6--)
                {
                    if (num5 == -1 && (player.inventory[num6].type == 0 || player.inventory[num6].stack == 0))
                        num5 = num6;

                    while (player.inventory[num6].type == 73 && player.inventory[num6].stack < player.inventory[num6].maxStack && num >= 10000)
                    {
                        player.inventory[num6].stack++;
                        num -= 10000;
                        player.DoCoins(num6);
                        if (player.inventory[num6].stack == 0 && num5 == -1)
                            num5 = num6;
                    }
                }

                if (num >= 10000)
                {
                    if (num5 == -1)
                    {
                        flag = true;
                        continue;
                    }

                    player.inventory[num5].SetDefaults(73);
                    num -= 10000;
                }
            }

            while (num >= 100 && !flag)
            {
                int num7 = -1;
                for (int num8 = 53; num8 >= 0; num8--)
                {
                    if (num7 == -1 && (player.inventory[num8].type == 0 || player.inventory[num8].stack == 0))
                        num7 = num8;

                    while (player.inventory[num8].type == 72 && player.inventory[num8].stack < player.inventory[num8].maxStack && num >= 100)
                    {
                        player.inventory[num8].stack++;
                        num -= 100;
                        player.DoCoins(num8);
                        if (player.inventory[num8].stack == 0 && num7 == -1)
                            num7 = num8;
                    }
                }

                if (num >= 100)
                {
                    if (num7 == -1)
                    {
                        flag = true;
                        continue;
                    }

                    player.inventory[num7].SetDefaults(72);
                    num -= 100;
                }
            }

            while (num >= 1 && !flag)
            {
                int num9 = -1;
                for (int num10 = 53; num10 >= 0; num10--)
                {
                    if (num9 == -1 && (player.inventory[num10].type == 0 || player.inventory[num10].stack == 0))
                        num9 = num10;

                    while (player.inventory[num10].type == 71 && player.inventory[num10].stack < player.inventory[num10].maxStack && num >= 1)
                    {
                        player.inventory[num10].stack++;
                        num--;
                        player.DoCoins(num10);
                        if (player.inventory[num10].stack == 0 && num9 == -1)
                            num9 = num10;
                    }
                }

                if (num >= 1)
                {
                    if (num9 == -1)
                    {
                        flag = true;
                        continue;
                    }

                    player.inventory[num9].SetDefaults(71);
                    num--;
                }
            }

            if (flag)
            {
                for (int j = 0; j < 58; j++)
                {
                    player.inventory[j] = array[j].Clone();
                }

                return false;
            }

            return true;
        }

        public static string ShowCoins(long value)
        {
            if (value == 0) return Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Free");
            string result = "";
            if (MoneyHelpers.GetPlatinum(value) > 0) result += MoneyHelpers.GetPlatinum(value) + "[i:74]";
            if (MoneyHelpers.GetGold(value) > 0) result += MoneyHelpers.GetGold(value) + "[i:73]";
            if (MoneyHelpers.GetSilver(value) > 0) result += MoneyHelpers.GetSilver(value) + "[i:72]";
            if (MoneyHelpers.GetCopper(value) > 0) result += MoneyHelpers.GetCopper(value) + "[i:71]";
            return result;
        }

        public static int GetCurrentMR()
        {
            int result = 0;
            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.type == ModContent.NPCType<ShipNPC>())
                {
                    result += ship.GetShipNPC().ShipGraph.MRValue;
                }
            }
            return result;
        }

        public static string ShowCoins(long value, int MRValue)
        {
            if (value == 0 && MRValue == 0) return Language.GetTextValue("Mods.StellarisShips.ExtraDesc.Free");
            string result = "";
            if (MoneyHelpers.GetPlatinum(value) > 0) result += MoneyHelpers.GetPlatinum(value) + "[i:74]";
            if (MoneyHelpers.GetGold(value) > 0) result += MoneyHelpers.GetGold(value) + "[i:73]";
            if (MoneyHelpers.GetSilver(value) > 0) result += MoneyHelpers.GetSilver(value) + "[i:72]";
            if (MoneyHelpers.GetCopper(value) > 0) result += MoneyHelpers.GetCopper(value) + "[i:71]";
            if (MRValue > 0) result += MRValue + "[i:StellarisShips/MR_Icon]";
            return result;
        }
    }
}