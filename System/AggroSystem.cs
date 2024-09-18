using Microsoft.Xna.Framework;
using StellarisShips.Content.NPCs;
using StellarisShips.Static;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.System
{

    public class TargetSelectNPC : GlobalNPC
    {
        public static Vector2? OldPos = null;
        public static Vector2? OldVel = null;

        public override bool PreAI(NPC npc)
        {
            if (npc.friendly || npc.townNPC || npc.type == ModContent.NPCType<ShipNPC>()) return true;
            if (ShapeSystem.Passive) return true;        //被动状态不吸引仇恨
            float SavedDist = 114514;
            int RealTarget = -1;
            foreach (NPC target in Main.ActiveNPCs)
            {
                if (target.ShipActive())
                {
                    float realDist = target.Distance(npc.Center) - target.GetShipNPC().Aggro;
                    if (RealTarget == -1 || realDist < SavedDist)
                    {
                        RealTarget = target.whoAmI;
                        SavedDist = realDist;
                    }
                }
            }

            if (RealTarget == -1)
            {
                return true;
            }
            if (SavedDist >= Main.LocalPlayer.Distance(npc.Center))
            {
                return true;
            }
            OldPos = Main.LocalPlayer.Center;
            OldVel = Main.LocalPlayer.velocity;
            Main.LocalPlayer.Center = Main.npc[RealTarget].Center;
            Main.LocalPlayer.velocity = Main.npc[RealTarget].velocity;
            SafePlayer.FuckingInvincible = true;
            SlimeFix(npc);
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (OldPos.HasValue)
            {
                Main.LocalPlayer.Center = OldPos.Value;
                OldPos = null;
            }
            if (OldVel.HasValue)
            {
                Main.LocalPlayer.velocity = OldVel.Value;
                OldVel = null;
            }
            SafePlayer.FuckingInvincible = false;
        }


        private static void SlimeFix(NPC npc)
        {
            if (npc.aiStyle == NPCAIStyleID.Slime)
            {
                Main.LocalPlayer.npcTypeNoAggro[npc.type] = false;
                npc.TargetClosest();
            }
        }

    }


    public class TargetSelectProj : GlobalProjectile
    {
        public static Vector2? OldPos = null;
        public static Vector2? OldVel = null;
        public override bool PreAI(Projectile projectile)
        {
            if (!projectile.hostile) return true;
            if (ShapeSystem.Passive) return true;        //被动状态不吸引仇恨
            float SavedDist = 114514;
            int RealTarget = -1;
            foreach (NPC target in Main.ActiveNPCs)
            {
                if (target.ShipActive())
                {
                    float realDist = target.Distance(projectile.Center) - target.GetShipNPC().Aggro;
                    if (RealTarget == -1 || realDist < SavedDist)
                    {
                        RealTarget = target.whoAmI;
                        SavedDist = realDist;
                    }
                }
            }

            if (RealTarget == -1)
            {
                return true;
            }
            if (SavedDist >= Main.LocalPlayer.Distance(projectile.Center))
            {
                return true;
            }
            OldPos = Main.LocalPlayer.Center;
            OldVel = Main.LocalPlayer.velocity;
            Main.LocalPlayer.Center = Main.npc[RealTarget].Center;
            Main.LocalPlayer.velocity = Main.npc[RealTarget].velocity;
            SafePlayer.FuckingInvincible = true;
            return true;
        }
        public override void PostAI(Projectile projectile)
        {
            if (OldPos.HasValue)
            {
                Main.LocalPlayer.Center = OldPos.Value;
                OldPos = null;
            }
            if (OldVel.HasValue)
            {
                Main.LocalPlayer.velocity = OldVel.Value;
                OldVel = null;
            }
            SafePlayer.FuckingInvincible = false;
        }

    }


    public class SafePlayer : ModPlayer
    {
        public static bool FuckingInvincible = false;
        public override void Load()
        {
            On_Player.AddBuff += AddBuffHook;
        }
        public override void Unload()
        {
            On_Player.AddBuff -= AddBuffHook;
        }
        internal static void AddBuffHook(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
        {
            if (FuckingInvincible) return;
            orig.Invoke(self, type, timeToAdd, quiet, foodHack);

        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (FuckingInvincible) return false;
            return true;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (FuckingInvincible) return false;
            return true;
        }

        public override bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
        {
            if (FuckingInvincible) return true;
            return false;
        }
    }

}
