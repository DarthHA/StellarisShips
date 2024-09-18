using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using StellarisShips.Content.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Static
{
    public static class SomeUtils
    {
        public static bool IsDead(this Player player)
        {
            return !player.active || player.dead || player.ghost;
        }

        public static bool IsBoss(this NPC npc)
        {
            if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
            {
                return true;
            }
            return false;
        }

        public static bool AnyBosses()
        {
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.IsBoss())
                {
                    return true;
                }
            }
            return false;
        }

        public static ShipNPC GetShipNPC(this NPC npc)
        {
            if (npc.type != ModContent.NPCType<ShipNPC>()) return null;
            return npc.ModNPC as ShipNPC;
        }

        public static bool ShipActive(this NPC npc)
        {
            if (npc.active && npc.type == ModContent.NPCType<ShipNPC>())
            {
                ShipNPC shipNPC = npc.GetShipNPC();
                if (shipNPC.ShipGraph.ShipType != "" && shipNPC.shipAI != ShipAI.Missing)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetRandomName()
        {
            string result = "";
            do
            {
                LocalizedText localizedText = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Composition."), null);
                LocalizedText localizedText4 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Adjective."), null);
                LocalizedText localizedText2 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Location."), null);
                LocalizedText localizedText3 = Language.SelectRandom(Lang.CreateDialogFilter("RandomWorldName_Noun."), null);

                var obj = new
                {
                    Adjective = localizedText4.Value,
                    Location = localizedText2.Value,
                    Noun = localizedText3.Value
                };

                result = localizedText.FormatWith(obj);
                if (Main.rand.NextBool(10000))
                {
                    result = Language.GetTextValue("Mods.StellarisShips.WoCao");
                }
            }
            while (result.Length > 27);
            return result;
        }

        public static string BreakLongString(string inputStr, int textWidth)
        {
            if (Language.ActiveCulture.LegacyId == (int)GameCulture.CultureName.Chinese)
            {
                return BreakLongStringForCN(inputStr, textWidth);
            }

            List<string> tempList = new List<string>();//��ʱ���ƴ���ַ������б�
            List<string> lastList = new List<string>();//���յ�����
            int strLength = inputStr.Length;//��ȡҪ�����ַ����ĳ���
            if (strLength > textWidth)
            {
                string[] listArray = inputStr.Split(' ');//�Ȱ��ַ����ָ��һ�������ʣ���������������
                string joinStr = "";
                string theDeleteStr = "";//���������Ϊ���������ų����̶����ȵ��Ǹ����ʡ�
                for (int j = 0; j < listArray.Length; j++)
                {
                    tempList.Add(listArray[j]);//�ѷָ�õĵ��� һ��������list��������
                    joinStr = string.Join(" ", tempList.ToArray());//Ȼ��ת�����ַ���
                                                                   //ÿ����һ�������̶����ȱȽ�һ�£���С��ʱ�򣬼������ӣ�������ڵ�ʱ������ж�
                    if (joinStr.Length > textWidth)
                    {
                        //��Ϊ�����˹̶����ȣ����԰����һ������ɾ����ɾ������ַ���Ϊһ�������ļ�¼��
                        int lastSpaceIndex = joinStr.LastIndexOf(" ");
                        lastList.Add((theDeleteStr + " " + joinStr.Substring(0, lastSpaceIndex)).Trim());
                        theDeleteStr = listArray[j];
                        //�պ������һ����ʱ��
                        if (j == listArray.Length - 1)
                            lastList.Add(theDeleteStr);

                        tempList.Clear();//�����ʱlist
                    }
                    else if (j == listArray.Length - 1)//����������β��ʣ�µĵ������һ��
                    {
                        lastList.Add((theDeleteStr + " " + joinStr).Trim());
                        tempList.Clear();
                    }
                }
            }

            string result = "";
            foreach (string str in lastList)
            {
                result += str + "\n";
            }
            return result;



        }

        private static string BreakLongStringForCN(string inputStr, int textWidth)
        {
            return Terraria.GameContent.FontAssets.MouseText.Value.CreateWrappedText(inputStr, textWidth * 10);
        }

        public static void AddLightLine(Vector2 Begin, Vector2 End, Color color, float intensity = 1)
        {
            if (End == Begin)
            {
                AddLight(Begin, color, intensity);
                return;
            }
            float Length = End.Distance(Begin);
            Vector2 UnitX = Vector2.Normalize(End - Begin);
            int l = 0;
            do
            {
                Vector2 Pos = Begin + UnitX * l;
                AddLight(Pos, color, intensity);
                l += 16;
            } while (l < Length);
            AddLight(End, color, intensity);
        }

        public static void AddLight(Vector2 Pos, Color color, float intensity = 1)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;
            Lighting.AddLight(Pos, r * intensity, g * intensity, b * intensity);
        }

        public static SlotId PlaySoundRandom(string path, int MaxNum, Vector2? Pos = null, float Vol = 1f)
        {
            string no = (Main.rand.Next(MaxNum) + 1).ToString();
            return SoundEngine.PlaySound(new SoundStyle(path + no) { Volume = Vol }, Pos);
        }

        public static SlotId PlaySound(string path, Vector2? Pos = null, float Vol = 1f)
        {
            return SoundEngine.PlaySound(new SoundStyle(path) { Volume = Vol }, Pos);
        }


        public static float ProjWorldDamage
        {
            get
            {
                if (!Main.GameModeInfo.IsJourneyMode)
                {
                    return Main.GameModeInfo.EnemyDamageMultiplier;
                }
                return CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs;
            }
        }

    }

}