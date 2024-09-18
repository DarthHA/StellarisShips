﻿
using Microsoft.Xna.Framework;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.System
{
    public class ShapeSystem : ModSystem
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

        //一排的舰船最多数
        private const int CorvetteLine = 12;
        private const int DestroyerLine = 10;
        private const int CruiserLine = 6;
        private const int BattleshipLine = 4;

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
            int CorvetteCount = 0;
            int DestroyerCount = 0;
            int CruiserCount = 0;
            int BattleshipCount = 0;
            List<Vector2> CorvettePos = new();
            List<Vector2> DestroyerPos = new();
            List<Vector2> CruiserPos = new();
            List<Vector2> BattleshipPos = new();
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ShipActive())
                {
                    switch (npc.GetShipNPC().ShipGraph.ShipType)
                    {
                        case "Corvette":
                            CorvetteCount++;
                            break;
                        case "Destroyer":
                            DestroyerCount++;
                            break;
                        case "Cruiser":
                            CruiserCount++;
                            break;
                        case "Battleship":
                            BattleshipCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
            Vector2 UnitX = -TipRotation.ToRotationVector2();
            Vector2 UnitY = (TipRotation + MathHelper.Pi / 2f * TipRotation.ToRotationVector2().X).ToRotationVector2();
            float CurrentLength = 0;
            for (int i = 1; i <= CorvetteCount; i++)
            {
                CorvettePos.Add(GetPos(TipPos + UnitX * CurrentLength, UnitX, UnitY, i, CorvetteLine, CorvetteCount, EverythingLibrary.Ships["Corvette"].Length + 30, EverythingLibrary.Ships["Corvette"].Width + 30));
            }

            if (CorvetteCount > 0) CurrentLength += (EverythingLibrary.Ships["Corvette"].Length + 30) * ((CorvetteCount / CorvetteLine) + 0.5f - (CorvetteCount % CorvetteLine == 0 ? 1 : 0));
            if (DestroyerCount > 0) CurrentLength += EverythingLibrary.Ships["Destroyer"].Length * 0.5f;
            for (int i = 1; i <= DestroyerCount; i++)
            {
                DestroyerPos.Add(GetPos(TipPos + UnitX * CurrentLength, UnitX, UnitY, i, DestroyerLine, DestroyerCount, EverythingLibrary.Ships["Destroyer"].Length + 30, EverythingLibrary.Ships["Destroyer"].Width + 30));
            }
            if (DestroyerCount > 0) CurrentLength += (EverythingLibrary.Ships["Destroyer"].Length + 30) * ((DestroyerCount / DestroyerLine) + 0.5f - (DestroyerCount % DestroyerLine == 0 ? 1 : 0));
            if (CruiserCount > 0) CurrentLength += EverythingLibrary.Ships["Cruiser"].Length * 0.5f;
            for (int i = 1; i <= CruiserCount; i++)
            {
                CruiserPos.Add(GetPos(TipPos + UnitX * CurrentLength, UnitX, UnitY, i, CruiserLine, CruiserCount, EverythingLibrary.Ships["Cruiser"].Length + 30, EverythingLibrary.Ships["Cruiser"].Width + 30));
            }
            if (CruiserCount > 0) CurrentLength += (EverythingLibrary.Ships["Cruiser"].Length + 30) * ((CruiserCount / CruiserLine) + 0.5f - (CruiserCount % CruiserLine == 0 ? 1 : 0));
            if (BattleshipCount > 0) CurrentLength += EverythingLibrary.Ships["Battleship"].Length * 0.5f;
            for (int i = 1; i <= BattleshipCount; i++)
            {
                BattleshipPos.Add(GetPos(TipPos + UnitX * CurrentLength, UnitX, UnitY, i, BattleshipLine, BattleshipCount, EverythingLibrary.Ships["Battleship"].Length + 30, EverythingLibrary.Ships["Battleship"].Width + 30));
            }

            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.ShipActive())
                {
                    switch (npc.GetShipNPC().ShipGraph.ShipType)
                    {
                        case "Corvette":
                            npc.GetShipNPC().ShapePos = CorvettePos[0];
                            CorvettePos.RemoveAt(0);
                            break;
                        case "Destroyer":
                            npc.GetShipNPC().ShapePos = DestroyerPos[0];
                            DestroyerPos.RemoveAt(0);
                            break;
                        case "Cruiser":
                            npc.GetShipNPC().ShapePos = CruiserPos[0];
                            CruiserPos.RemoveAt(0);
                            break;
                        case "Battleship":
                            npc.GetShipNPC().ShapePos = BattleshipPos[0];
                            BattleshipPos.RemoveAt(0);
                            break;
                        default:
                            break;
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
                        if (LowestSpeed == -1 || LowestSpeed > npc.GetShipNPC().BaseSpeed)
                        {
                            LowestSpeed = npc.GetShipNPC().BaseSpeed;
                        }
                    }
                }
            }

        }

        private Vector2 GetPos(Vector2 TipPos, Vector2 UnitX, Vector2 UnitY, int num, int Line, int maxNum, float xLen, float yLen)
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
