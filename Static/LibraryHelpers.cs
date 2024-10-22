using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace StellarisShips.Static
{
    internal static class LibraryHelpers
    {
        public static void SetBonus(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            if (!bonusBuff.TryAdd(item, value))
            {
                bonusBuff[item] = value;
            }
        }


        public static void AddBonus(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            if (!bonusBuff.TryAdd(item, value))
            {
                bonusBuff[item] += value;
            }
        }

        public static void AddBonusEvasion(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            float value1 = bonusBuff.GetBonus(item);
            float value2 = value;
            float result = (1 - (1 - value1 / 100f) * (1 - value2 / 100f)) * 100f;
            if (!bonusBuff.TryAdd(item, result))
            {
                bonusBuff[item] = result;
            }
        }

        public static void AddBonusShieldDR(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            float value1 = bonusBuff.GetBonus(item);
            float value2 = value;
            float result = 1 - (1 - value1) * (1 - value2);
            if (!bonusBuff.TryAdd(item, result))
            {
                bonusBuff[item] = result;
            }
        }

        public static void AddBonusLevel(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            float value1 = bonusBuff.GetBonus(item);
            float value2 = value;
            float result = Math.Max(value1, value2);
            if (!bonusBuff.TryAdd(item, result))
            {
                bonusBuff[item] = result;
            }
        }


        public static float GetBonus(this Dictionary<string, float> bonusBuff, string item, bool AddOne = false)
        {
            float One = AddOne ? 1 : 0;
            if (bonusBuff.TryGetValue(item, out float value))
            {
                return One + value;
            }
            return One;
        }


        public static List<BaseComponent> FindMaxLevelComponent(bool removeByProgress = false)
        {
            List<BaseComponent> result = new();
            foreach (BaseComponent component1 in EverythingLibrary.Components.Values)
            {
                if (!removeByProgress || component1.CanUnlock())
                {
                    if (result.Count == 0)
                    {
                        result.Add(component1);
                    }
                    else
                    {
                        int Succession = -1;
                        bool FindTheSame = false;
                        for (int i = 0; i < result.Count; i++)
                        {
                            BaseComponent component2 = result[i];
                            if (component1.TypeName == component2.TypeName && component1.EquipType == component2.EquipType)
                            {
                                FindTheSame = true;
                                if (component1.Level > component2.Level)
                                {
                                    Succession = i;
                                    break;
                                }
                            }

                        }
                        if (!FindTheSame)
                        {
                            result.Add(component1);
                        }
                        else if (Succession != -1)
                        {
                            result.RemoveAt(Succession);
                            result.Add(component1);
                        }

                    }
                }
            }
            return result;
        }

        public static string FindMaxLevelComponent(string typeName, string EquipType, bool RemoveByProgress = false)
        {
            BaseComponent result = null;
            foreach (BaseComponent component1 in EverythingLibrary.Components.Values)
            {
                if (!RemoveByProgress || component1.CanUnlock())
                {
                    if ((component1.TypeName == typeName || typeName == "") && (component1.EquipType == EquipType || EquipType == ""))
                    {
                        if (result == null)
                        {
                            result = component1;
                        }
                        else
                        {
                            if (component1.Level > result.Level)
                            {
                                result = component1;
                            }
                        }
                    }
                }
            }
            return result.InternalName;
        }

        public static List<BaseComponent> FindTheSameTypeAndSlotComponent(BaseComponent component, bool RemoveByProgress = false)
        {
            string TypeName = component.TypeName;
            string EType = component.EquipType;
            List<BaseComponent> groups = new();
            foreach (BaseComponent c in EverythingLibrary.Components.Values)
            {
                if (!RemoveByProgress || c.CanUnlock())
                {
                    if (c.TypeName == TypeName && c.EquipType == EType)
                    {
                        groups.Add(c);
                    }
                }
            }
            groups.Sort((a, b) => { return b.Level.CompareTo(a.Level); });
            return groups;
        }

        public static Texture2D GetGunTex(string component)
        {
            if (component == "") return null;
            string path = "";
            switch (EverythingLibrary.Components[component].EquipType)
            {
                case ComponentTypes.Weapon_S:
                    if (EverythingLibrary.Components[component].IsExplosive)
                    {
                        path = "SE_Gun";
                    }
                    else
                    {
                        path = "S_Gun";
                    }
                    break;
                case ComponentTypes.Weapon_M:
                case ComponentTypes.Weapon_G:
                    if (EverythingLibrary.Components[component].IsExplosive)
                    {
                        path = "G_Gun";
                    }
                    else
                    {
                        path = "M_Gun";
                    }
                    break;
                case ComponentTypes.Weapon_L:
                    path = "L_Gun";
                    break;
                case ComponentTypes.Weapon_P:
                    path = "P_Gun";
                    break;
            }
            if (path != "")
            {
                return ModContent.Request<Texture2D>("StellarisShips/Images/Ships/" + path, AssetRequestMode.ImmediateLoad).Value;
            }
            return null;
        }

        public static Texture2D GetShipTexture(List<string> sections)
        {
            string finder = "";
            for (int i = 0; i < sections.Count; i++)
            {
                finder += sections[i];
                if (i != sections.Count - 1) finder += "_";
            }
            return ModContent.Request<Texture2D>("StellarisShips/Images/Ships/" + finder, AssetRequestMode.ImmediateLoad).Value;
        }

        public static bool IsUtility(string str)
        {
            switch (str)
            {
                case ComponentTypes.Defense_L:
                case ComponentTypes.Defense_M:
                case ComponentTypes.Defense_S:
                case ComponentTypes.Accessory:
                    return true;
            }
            return false;
        }

        public static bool IsWeapon(string t)
        {
            switch (t)
            {
                case ComponentTypes.Weapon_S:
                case ComponentTypes.Weapon_M:
                case ComponentTypes.Weapon_L:
                case ComponentTypes.Weapon_X:
                case ComponentTypes.Weapon_T:
                case ComponentTypes.Weapon_P:
                case ComponentTypes.Weapon_H:
                case ComponentTypes.Weapon_G:
                    return true;
            }
            return false;
        }

        public static List<string> GetLockedTech()
        {
            List<string> result = new();
            foreach (string tech in EverythingLibrary.LockedTech)
            {
                if (!ProgressHelper.UnlockTech.Contains(tech))
                {
                    if (!result.Contains(tech))
                    {
                        result.Add(tech);
                    }
                }
            }
            return result;
        }

        //给武器根据槽位一个防御力效果修正，用于某些武器（比如机关炮）
        public static float GetDefModifier(string slot)
        {
            switch (slot)
            {
                case ComponentTypes.Weapon_S:
                case ComponentTypes.Weapon_P:
                    return 1f;
                case ComponentTypes.Weapon_M:
                case ComponentTypes.Weapon_G:
                    return 2f;
                case ComponentTypes.Weapon_L:
                case ComponentTypes.Weapon_H:
                    return 4f;
                case ComponentTypes.Weapon_X:
                    return 8f;
                default:
                    return 1f;

            }
        }

        public static Dictionary<string, float> Merge(this Dictionary<string, float> dic1, Dictionary<string, float> dic2)
        {
            Dictionary<string, float> result = new();
            foreach (string key in dic1.Keys)
            {
                result.Add(key, dic1[key]);
            }
            foreach (string key in dic2.Keys)
            {
                if (!result.TryAdd(key, dic2[key]))
                {
                    switch (key)
                    {
                        case BonusID.ShieldDRLevel:
                        case BonusID.FTLLevel:
                            result[key] = Math.Max(dic1[key], dic2[key]);
                            break;
                        case BonusID.Evasion:
                            result[key] = (1 - (1 - dic1[key] / 100f) * (1 - dic2[key] / 100f)) * 100f;
                            break;
                        case BonusID.ShieldDR:
                            result[key] = 1 - (1 - dic1[key]) * (1 - dic2[key]);
                            break;
                        default:
                            result[key] += dic2[key];
                            break;
                    }
                }
            }
            return result;
        }
    }
}