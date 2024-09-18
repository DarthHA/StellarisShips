using StellarisShips.Content.Buffs;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.System
{
    public class BattleStatSystem : ModSystem
    {
        public static Dictionary<string, int> WeaponDamage = new();
        public static int ShieldHitDamage = 0;
        public static int HullHitDamage = 0;
        public static Dictionary<string, int> ShipInBattle = new();
        public static Dictionary<string, int> LoseShip = new();
        public static bool BossBattleActive = false;
        public static string StatText = "";


        public override void PostUpdateNPCs()
        {
            if (!BossBattleActive)
            {
                if (SomeUtils.AnyBosses())
                {
                    //在这里记录参战舰船
                    ClearStat();
                    foreach (NPC ship in Main.ActiveNPCs)
                    {
                        if (ship.type == ModContent.NPCType<ShipNPC>())
                        {
                            string name = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ship.GetShipNPC().ShipGraph.GraphName, EverythingLibrary.Ships[ship.GetShipNPC().ShipGraph.ShipType].GetLocalizedName());
                            if (!ShipInBattle.TryAdd(name, 1))
                            {
                                ShipInBattle[name] += 1;
                            }
                        }
                    }

                    BossBattleActive = true;
                }
            }
            else
            {
                if (!SomeUtils.AnyBosses())
                {
                    //在这里输出数据
                    string f = Language.GetTextValue("Mods.StellarisShips.BuffExtraDesc.BattleStatDesc");
                    string str1 = "", str2 = "";
                    foreach (string name in ShipInBattle.Keys)
                    {
                        LoseShip.TryGetValue(name, out int value);
                        if (value > 0)
                        {
                            str1 += string.Format("{0}:  [c/ff0066:{1}]/{2}\n", name, ShipInBattle[name] - value, ShipInBattle[name]);
                        }
                        else
                        {
                            str1 += string.Format("{0}:  {1}/{2}\n", name, ShipInBattle[name] - value, ShipInBattle[name]);
                        }
                    }
                    foreach (string name in WeaponDamage.Keys)
                    {
                        str2 += string.Format("{0}:  {1}\n", name, WeaponDamage[name]);
                    }
                    StatText = string.Format(f, str1, ShieldHitDamage, HullHitDamage, str2);
                    SomeUtils.PlaySound(SoundPath.UI + "Notification");
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<BattleStatBuff>(), 120 * 60);
                    ClearStat();
                    BossBattleActive = false;
                }
            }

        }

        public static void ClearStat()
        {
            WeaponDamage.Clear();
            ShieldHitDamage = 0;
            HullHitDamage = 0;
            ShipInBattle.Clear();
            LoseShip.Clear();
        }

        public override void PreSaveAndQuit()
        {
            ClearStat();
            StatText = "";
            BossBattleActive = false;
        }
    }
}
