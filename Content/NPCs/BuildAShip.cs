﻿using Microsoft.Xna.Framework;
using StellarisShips.Content.Components.Weapons;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public partial class ShipNPC : ModNPC
    {
        public static int BuildAShip(Terraria.DataStructures.IEntitySource source, Vector2 Pos, ShipGraph graph, string Name)
        {
            if (graph.ShipType == "") return -1;
            int npctmp = NPC.NewNPC(source, (int)Pos.X, (int)Pos.Y, ModContent.NPCType<ShipNPC>());
            if (npctmp >= 0 && npctmp < 200)
            {
                NPC npc = Main.npc[npctmp];
                ShipNPC shipNPC = Main.npc[npctmp].GetShipNPC();
                shipNPC.ShipGraph = graph.Copy();
                shipNPC.ShipName = Name;
                npc.GivenName = Name;
                //计算核心部件和通用部件加成
                foreach (string coreComponent in shipNPC.ShipGraph.CoreComponent)
                {
                    if (coreComponent != "")
                    {
                        EverythingLibrary.Components[coreComponent].ApplyEquip(npc);
                    }
                }
                foreach (SectionForSave section in shipNPC.ShipGraph.Parts)
                {
                    foreach (string component in section.UtilitySlot)
                    {
                        if (component != "")
                        {
                            EverythingLibrary.Components[component].ApplyEquip(npc);
                        }
                    }
                }
                //载入武器数据
                foreach (SectionForSave section in shipNPC.ShipGraph.Parts)
                {
                    for (int i = 0; i < section.WeaponSlot.Count; i++)
                    {
                        if (section.WeaponSlot[i] != "")
                        {
                            EverythingLibrary.Components[section.WeaponSlot[i]].ApplyEquip(npc);
                            BaseWeaponUnit instance = BaseWeaponUnit.NewWeaponUnit(EverythingLibrary.Components[section.WeaponSlot[i]].TypeName);
                            instance.RelativePos = EverythingLibrary.Sections[section.InternalName].WeaponPos[i];
                            instance.ComponentName = section.WeaponSlot[i];
                            instance.DamageBonus = (EverythingLibrary.Components[section.WeaponSlot[i]] as BaseWeaponComponent).GetDamageBonus(shipNPC.StaticBuff);
                            instance.AttackCDBonus = (EverythingLibrary.Components[section.WeaponSlot[i]] as BaseWeaponComponent).GetAttackCDBonus(shipNPC.StaticBuff);
                            instance.CritBonus = (EverythingLibrary.Components[section.WeaponSlot[i]] as BaseWeaponComponent).GetCritBonus(shipNPC.StaticBuff);
                            instance.RangeBonus = (EverythingLibrary.Components[section.WeaponSlot[i]] as BaseWeaponComponent).GetRangeBonus(shipNPC.StaticBuff);

                            EverythingLibrary.Components[section.WeaponSlot[i]].ModifyWeaponUnit(instance, npc);
                            shipNPC.weapons.Add(instance);
                        }
                    }
                }

                //计算舰船本身加成
                shipNPC.ResetShipStat();

                //参数重整
                npc.width = npc.height = EverythingLibrary.Ships[graph.ShipType].Width;
                npc.life = npc.lifeMax;
                shipNPC.CurrentShield = shipNPC.MaxShield;
                npc.Center = Pos;

                //绘制相关捷径录入
                List<string> sectionList = new();
                foreach (SectionForSave str in shipNPC.ShipGraph.Parts)
                {
                    sectionList.Add(str.InternalName);
                }
                shipNPC.ShipTexture = LibraryHelpers.GetShipTexture(sectionList);
                shipNPC.shipScale = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].ShipScale;
                shipNPC.weaponScale = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].WeaponScale;
                shipNPC.TailDrawOffset = EverythingLibrary.Sections[shipNPC.ShipGraph.Parts[shipNPC.ShipGraph.Parts.Count - 1].InternalName].TailDrawOffSet;
                shipNPC.TailType = shipNPC.ShipGraph.Parts[shipNPC.ShipGraph.Parts.Count - 1].InternalName;
                shipNPC.ShipWidth = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Width;
                shipNPC.ShipLength = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Length;
            }
            return npctmp;
        }


    }
}
