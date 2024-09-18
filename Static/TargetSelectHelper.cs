using Microsoft.Xna.Framework;
using StellarisShips.Static;
using StellarisShips.System;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

internal static class TargetSelectHelper
{

    /// <summary>
    /// 考虑碰撞体积后的距离判定，用于舰船攻击AI
    /// </summary>
    /// <param name="npc1"></param>
    /// <param name="npc2"></param>
    /// <returns></returns>
    public static float DistanceWithWidth(this NPC npc1, NPC npc2)
    {
        float r1 = (float)Math.Sqrt(npc1.width * npc1.width + npc1.height * npc1.height) / 2f;
        float r2 = (float)Math.Sqrt(npc2.width * npc2.width + npc2.height * npc2.height) / 2f;
        if (npc1.damage == 0 && npc2.damage == 0)        //无碰撞伤害时就转小圈
        {
            if (!npc1.ShipActive())
            {
                r1 = 0;
            }
            if (!npc2.ShipActive())
            {
                r2 = 0;
            }
        }
        float result = npc1.Distance(npc2.Center) - r1 - r2;
        if (result < 1) result = 1;
        return result;
    }

    /// <summary>
    /// 先进索敌（大嘘），用于舰船和导弹索敌AI
    /// </summary>
    /// <param name="CompareCenter">这个计算最近距离用的</param>
    /// <param name="RangeCenter">这个计算有效范围用的</param>
    /// <param name="maxRange"></param>
    /// <param name="SelectedTarget"></param>
    /// <returns></returns>
    public static int GetClosestTarget(Vector2 CompareCenter, Vector2 RangeCenter, float maxRange, int SelectedTarget, bool CanSeeThroughTiles = true)
    {
        if (SelectedTarget != -1)
        {
            if (Main.npc[SelectedTarget].active && Main.npc[SelectedTarget].CanBeChasedBy())
            {
                if (Main.npc[SelectedTarget].Distance(RangeCenter) <= maxRange)
                {
                    if (CanSeeThroughTiles || Collision.CanHitLine(Main.npc[SelectedTarget].position, Main.npc[SelectedTarget].width, Main.npc[SelectedTarget].height, RangeCenter, 1, 1))
                    {
                        return SelectedTarget;
                    }
                }
            }
        }

        int target = -1;
        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy())
            {
                if (npc.Distance(RangeCenter) <= maxRange)
                {
                    if (CanSeeThroughTiles || Collision.CanHitLine(npc.position, npc.width, npc.height, RangeCenter, 1, 1))
                    {
                        if (target == -1 || npc.Distance(CompareCenter) < Main.npc[target].Distance(CompareCenter))
                        {
                            target = npc.whoAmI;
                        }
                    }
                }
            }
        }

        return target;
    }

    /// <summary>
    /// 计算体积的更精确索敌（大嘘x2,用于武器索敌AI
    /// </summary>
    /// <param name="ship"></param>
    /// <param name="maxRange"></param>
    /// <param name="minRange"></param>
    /// <param name="SelectedTarget"></param>
    /// <returns></returns>
    public static int GetClosestTargetWithWidth(this NPC ship, float maxRange, float minRange = 0, int SelectedTarget = -1)
    {
        if (SelectedTarget != -1)
        {
            if (Main.npc[SelectedTarget].active && Main.npc[SelectedTarget].CanBeChasedBy())
            {
                if (Main.npc[SelectedTarget].DistanceWithWidth(ship) <= maxRange && Main.npc[SelectedTarget].DistanceWithWidth(ship) >= minRange)
                {
                    return SelectedTarget;
                }
            }
        }

        int target = -1;
        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy())
            {
                if (npc.DistanceWithWidth(ship) <= maxRange && npc.DistanceWithWidth(ship) >= minRange)
                {
                    if (target == -1 || npc.DistanceWithWidth(ship) < Main.npc[target].DistanceWithWidth(ship))
                    {
                        target = npc.whoAmI;
                    }
                }
            }
        }

        return target;
    }

    /// <summary>
    /// 点防御的计算体积的更精确索敌（大嘘x3,用于点防御AI
    /// </summary>
    /// <param name="ship"></param>
    /// <param name="maxRange"></param>
    /// <param name="minRange"></param>
    /// <param name="SelectedTarget1"></param>
    /// <returns></returns>
    public static int GetClosestTargetWithWidthForPointDefense(this NPC ship, float maxRange, float minRange = 0, int SelectedTarget1 = -1, int SelectedTarget2 = -1)
    {
        List<int> sampler = new();

        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy() || NPCID.Sets.ProjectileNPC[npc.type])
            {
                if (npc.DistanceWithWidth(ship) <= maxRange && npc.DistanceWithWidth(ship) >= minRange && (Collision.CanHitLine(ship.position, ship.width, ship.height, npc.position, npc.width, npc.height) || ship.GetShipNPC().CanSeeThroughTiles))
                {
                    sampler.Add(npc.whoAmI);
                }
            }
        }

        if ((sampler.Contains(SelectedTarget1) && sampler.Contains(SelectedTarget2) && (sampler.Count == 2 || sampler.Count == 1)) ||
           ((sampler.Contains(SelectedTarget1) || sampler.Contains(SelectedTarget2)) && sampler.Count == 1))          //有且仅有主要目标时选取主要目标，优先级2>1
        {
            int sampler2 = -1;
            if (sampler.Contains(SelectedTarget1)) sampler2 = SelectedTarget1;
            if (sampler.Contains(SelectedTarget2)) sampler2 = SelectedTarget2;
            return sampler2;
        }



        sampler.Remove(SelectedTarget1);
        sampler.Remove(SelectedTarget2);

        int target = -1;
        foreach (int wmi in sampler)
        {
            if (target == -1 || Main.npc[wmi].DistanceWithWidth(ship) < Main.npc[target].DistanceWithWidth(ship))
            {
                target = wmi;
            }
        }

        return target;
    }


    /// <summary>
    /// 计算体积和发射扇面的更精确索敌（大嘘x4,用于X槽武器索敌AI
    /// </summary>
    /// <param name="ship"></param>
    /// <param name="maxRange"></param>
    /// <param name="minRange"></param>
    /// <param name="SelectedTarget"></param>
    /// <returns></returns>
    public static int GetClosestTargetWithWidthAndAngle(this NPC ship, float angle, float maxRange, float minRange = 0, int SelectedTarget = -1)
    {
        bool WithinAngle(float angle, float rot, Vector2 v)
        {
            Vector2 v1 = rot.ToRotationVector2();
            float cos = (v.X * v1.X + v.Y * v1.Y) / v.Length();
            return Math.Acos(cos) < angle / 2f;
        }
        if (SelectedTarget != -1)
        {
            if (Main.npc[SelectedTarget].active && Main.npc[SelectedTarget].CanBeChasedBy() && WithinAngle(angle, ship.rotation, Main.npc[SelectedTarget].Center - ship.Center))
            {
                if (Main.npc[SelectedTarget].DistanceWithWidth(ship) <= maxRange && Main.npc[SelectedTarget].DistanceWithWidth(ship) >= minRange)
                {
                    return SelectedTarget;
                }
            }
        }

        int target = -1;
        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy())
            {
                if (npc.DistanceWithWidth(ship) <= maxRange && npc.DistanceWithWidth(ship) >= minRange && WithinAngle(angle, ship.rotation, npc.Center - ship.Center))
                {
                    if (target == -1 || npc.DistanceWithWidth(ship) < Main.npc[target].DistanceWithWidth(ship))
                    {
                        target = npc.whoAmI;
                    }
                }
            }
        }

        return target;
    }

    /// <summary>
    /// 点防御的计算体积的更精确索敌（大嘘x5,用于点防御AI
    /// </summary>
    /// <param name="ship"></param>
    /// <param name="maxRange"></param>
    /// <param name="minRange"></param>
    /// <param name="SelectedTarget1"></param>
    /// <returns></returns>
    public static int GetClosestTargetWithWidthForPointDefense(this NPC ship, float maxRange, float minRange = 0, List<int> SelectTargets = null)
    {
        List<int> sampler = new();

        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy() || NPCID.Sets.ProjectileNPC[npc.type])
            {
                if (npc.DistanceWithWidth(ship) <= maxRange && npc.DistanceWithWidth(ship) >= minRange && (Collision.CanHitLine(ship.position, ship.width, ship.height, npc.position, npc.width, npc.height) || ship.GetShipNPC().CanSeeThroughTiles))
                {
                    sampler.Add(npc.whoAmI);
                }
            }
        }

        bool HasOnlyMainTarget = true;
        foreach (int sam in sampler)
        {
            if (!SelectTargets.Contains(sam))
            {
                HasOnlyMainTarget = false;
                break;
            }
        }
        if (HasOnlyMainTarget)          //有且仅有主要目标时选取主要目标，优先级越往后越大
        {
            int sampler2 = -1;
            foreach (int mainTarget in SelectTargets)
            {
                if (sampler.Contains(mainTarget)) sampler2 = mainTarget;
            }
            return sampler2;
        }

        foreach (int shouldRemove in SelectTargets)            //移除主目标
        {
            sampler.Remove(shouldRemove);
        }

        int target = -1;
        foreach (int wmi in sampler)
        {
            if (target == -1 || Main.npc[wmi].DistanceWithWidth(ship) < Main.npc[target].DistanceWithWidth(ship))
            {
                target = wmi;
            }
        }

        return target;
    }


    /// <summary>
    /// 拦截机的计算体积的更精确索敌（大嘘x3,用于点防御AI
    /// </summary>
    /// <param name="ship"></param>
    /// <param name="maxRange"></param>
    /// <param name="minRange"></param>
    /// <param name="SelectedTarget"></param>
    /// <returns></returns>
    public static int GetClosestTargetWithWidthForStriker(this NPC ship)
    {
        Vector2 DetectCenter = FleetSystem.Following ? Main.LocalPlayer.Center : FleetSystem.TipPos;
        Vector2 CompareCenter = ship.Center;

        List<int> sampler = new();

        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (npc.CanBeChasedBy() || NPCID.Sets.ProjectileNPC[npc.type])
            {
                if (npc.Distance(DetectCenter) <= ship.GetShipNPC().DetectRange && (ship.GetShipNPC().CanSeeThroughTiles || Collision.CanHitLine(DetectCenter, 1, 1, npc.position, npc.width, npc.height)))
                {
                    sampler.Add(npc.whoAmI);
                }
            }
        }

        if (sampler.Contains(ship.GetShipNPC().CurrentTarget) && sampler.Count == 1)          //有且仅有主要目标时选取主要目标，优先级2>1
        {
            return ship.GetShipNPC().CurrentTarget;
        }

        sampler.Remove(ship.GetShipNPC().CurrentTarget);

        int target = -1;
        foreach (int wmi in sampler)
        {
            if (target == -1 || Main.npc[wmi].Distance(CompareCenter) < Main.npc[target].Distance(CompareCenter))
            {
                target = wmi;
            }
        }

        return target;
    }
}