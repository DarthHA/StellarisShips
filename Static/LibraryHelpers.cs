using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace StellarisShips.Static
{
    internal static class LibraryHelpers
    {

        public static void AddBonus(this Dictionary<string, float> bonusBuff, string item, float value)
        {
            if (!bonusBuff.TryAdd(item, value))
            {
                bonusBuff[item] += value;
            }
        }

        public static float ApplyBonus(this Dictionary<string, float> bonusBuff, string item, bool AddOne = true)
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
    }
}