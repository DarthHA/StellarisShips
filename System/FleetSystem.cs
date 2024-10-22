
using Microsoft.Xna.Framework;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace StellarisShips.System
{
    public class FleetSystem : ModSystem
    {
        /// <summary>
        /// 队首的坐标
        /// </summary>
        public static Vector2 TipPos = new();
        /// <summary>
        /// 队伍角度
        /// </summary>
        public static float TipRotation = new();
        /// <summary>
        /// 所有舰船的最低速度
        /// </summary>
        public static float LowestSpeed = -1;

        /// <summary>
        /// 是否跟随
        /// </summary>
        public static bool Following = true;
        /// <summary>
        /// 是否攻击
        /// </summary>
        public static bool Passive = false;

        /// <summary>
        /// 舰队光环效果
        /// </summary>
        public static List<string> GlobalEffects = new();

        //对应舰容一排舰船最多数
        public static Dictionary<int, int> ShipLineCount = new();

        //对应舰船占据面积
        public static Dictionary<int, Vector2> ShipSize = new();

        public override void Load()
        {
            ShipLineCount = new() { { 1, 12 }, { 2, 10 }, { 4, 6 }, { 8, 4 }, { 16, 4 } };
            ShipSize = new() { { 1, new(70, 32) }, { 2, new(120, 50) }, { 4, new(260, 100) }, { 8, new(280, 150) }, { 16, new(480, 180) } };
        }

        public override void Unload()
        {
            ShipSize.Clear();
            ShipLineCount.Clear();
            GlobalEffects.Clear();
        }

        public override void PreUpdateEntities()
        {
            GlobalEffects.Clear();

            if (Main.LocalPlayer.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs != "")
            {
                GlobalEffects.Add(Main.LocalPlayer.GetModPlayer<ShipControlPlayer>().CurrentShroudBuffs);
            }

            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive())
                {
                    if (ship.GetShipNPC().AuraType != "")
                    {
                        if (ship.GetShipNPC().AuraType == ModifierID.SubspaceSnare)        //亚空间陷阱只会在进战时生效
                        {
                            if (ship.GetShipNPC().shipAI == ShipAI.Attack)
                            {
                                GlobalEffects.Add(ship.GetShipNPC().AuraType);
                            }
                        }
                        else
                        {
                            GlobalEffects.Add(ship.GetShipNPC().AuraType);
                        }
                    }
                }
            }

            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive(true))
                {
                    ship.GetShipNPC().SpecialBuff.Clear();
                }
            }

            foreach (NPC ship in Main.ActiveNPCs)
            {
                if (ship.ShipActive(true))
                {
                    foreach (string modifier in GlobalEffects)
                    {
                        EverythingLibrary.Modifiers[modifier].ApplyBonus(ship.GetShipNPC().SpecialBuff);
                    }
                }
            }

        }


        public override void PostUpdatePlayers()
        {
            if (Following)
            {
                if (Main.LocalPlayer.velocity.Length() > 0.1f)
                {
                    TipRotation = new Vector2(Math.Sign(Main.LocalPlayer.direction + 0.01f), 0).ToRotation();
                }
                TipPos = Main.LocalPlayer.Center - TipRotation.ToRotationVector2() * 100;
            }


            Dictionary<int, List<Vector2>> shipTargetPos = new();
            Dictionary<int, int> shipCount = new();
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ShipActive())
                {
                    int cp = EverythingLibrary.Ships[npc.GetShipNPC().ShipGraph.ShipType].CP;
                    if (!shipCount.TryAdd(cp, 1))
                    {
                        shipCount[cp] += 1;
                    }
                }
            }
            shipCount = shipCount.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);

            Vector2 UnitX = -TipRotation.ToRotationVector2();
            Vector2 UnitY = (TipRotation + MathHelper.Pi / 2f * TipRotation.ToRotationVector2().X).ToRotationVector2();
            float CurrentLength = 0;

            List<int> CPs = shipCount.Keys.ToList();
            for(int i = 0; i < CPs.Count; i++)
            {
                int CP = CPs[i];
                CurrentLength += ShipSize[CP].X * 0.5f;
                for (int j = 1; j <= shipCount[CP]; j++)
                {
                    if (!shipTargetPos.TryGetValue(CP, out List<Vector2> _))
                    {
                        shipTargetPos.Add(CP, new());
                    }

                    shipTargetPos[CP].Add(GetPos(TipPos + UnitX * CurrentLength, UnitX, UnitY, j, ShipLineCount[CP], shipCount[CP], ShipSize[CP].X + 30, ShipSize[CP].Y + 30));
                }
                CurrentLength += (ShipSize[CP].X + 30) * ((shipCount[CP] / ShipLineCount[CP]) + 0.5f - (shipCount[CP] % ShipLineCount[CP] == 0 ? 1 : 0));
            }

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ShipActive())
                {
                    int CP = EverythingLibrary.Ships[npc.GetShipNPC().ShipGraph.ShipType].CP;
                    if (shipTargetPos.TryGetValue(CP, out List<Vector2> value))
                    {
                        if (value.Count > 0)
                        {
                            npc.GetShipNPC().ShapePos = value[0];
                            value.RemoveAt(0);
                        }
                    }
                }
            }

            LowestSpeed = -1;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<ShipNPC>())
                {
                    if (npc.GetShipNPC().ShipGraph.ShipType != "")
                    {
                        if (LowestSpeed == -1 || LowestSpeed > npc.GetShipNPC().MaxSpeed)
                        {
                            LowestSpeed = npc.GetShipNPC().MaxSpeed;
                        }
                    }
                }
            }

        }

        private static Vector2 GetPos(Vector2 TipPos, Vector2 UnitX, Vector2 UnitY, int num, int Line, int maxNum, float xLen, float yLen)
        {
            Vector2 X, Y;
            int x, y, xM, yM;
            x = num % Line;
            y = num / Line;
            xM = maxNum % Line;
            yM = maxNum / Line;
            if (x == 0)
            {
                x = Line;
                y--;
            }
            if (xM == 0)
            {
                xM = Line;
                yM--;
            }

            int x1 = Line;
            if (y == yM) x1 = xM;      //最后一行自动整队

            X = UnitX * y * xLen;
            if ((int)(x1 / 2f) * 2 == x1)  //偶数
            {
                Y = -UnitY * yLen * ((x1 / 2) + 0.5f) + UnitY * yLen * x;
            }
            else
            {
                Y = -UnitY * yLen * ((x1 + 1) / 2) + UnitY * yLen * x;
            }

            return TipPos + X + Y;
        }


        public override void ClearWorld()
        {
            Following = true;
            Passive = false;
        }
    }

}
