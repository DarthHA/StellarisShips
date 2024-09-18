using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace StellarisShips.System
{
    public class EverythingLibrary : ModSystem
    {
        public static Dictionary<string, BaseComponent> Components = new();
        public static Dictionary<string, BaseSection> Sections = new();
        public static Dictionary<string, BaseShip> Ships = new();
        public static Dictionary<string, BaseWeaponUnit> WeaponUnits = new();
        public static Dictionary<string, BaseDialog> Dialogs = new();
        public override void Load()
        {
            Components = new();
            Sections = new();
            Ships = new();
            WeaponUnits = new();
            Dialogs = new();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && typeof(BaseComponent).IsAssignableFrom(type))
                {
                    BaseComponent instance = (BaseComponent)Activator.CreateInstance(type);
                    instance.GetIcon();
                    Components.Add(instance.InternalName, instance);
                }
                if (type.IsClass && !type.IsAbstract && typeof(BaseSection).IsAssignableFrom(type))
                {
                    BaseSection instance = (BaseSection)Activator.CreateInstance(type);
                    instance.GetIcon();
                    Sections.Add(instance.InternalName, instance);
                }
                if (type.IsClass && !type.IsAbstract && typeof(BaseShip).IsAssignableFrom(type))
                {
                    BaseShip instance = (BaseShip)Activator.CreateInstance(type);
                    instance.GetIcon();
                    Ships.Add(instance.InternalName, instance);
                }
                if (type.IsClass && !type.IsAbstract && typeof(BaseWeaponUnit).IsAssignableFrom(type))
                {
                    BaseWeaponUnit instance = (BaseWeaponUnit)Activator.CreateInstance(type);
                    WeaponUnits.Add(instance.InternalName, instance);
                }
                if (type.IsClass && !type.IsAbstract && typeof(BaseDialog).IsAssignableFrom(type))
                {
                    BaseDialog instance = (BaseDialog)Activator.CreateInstance(type);
                    Dialogs.Add(instance.InternalName, instance);
                }
            }
        }

        public override void Unload()
        {
            Components.Clear();
            Sections.Clear();
            Ships.Clear();
            WeaponUnits.Clear();
            Dialogs.Clear();
        }
    }
}
