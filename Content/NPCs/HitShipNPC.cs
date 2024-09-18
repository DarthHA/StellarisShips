﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace StellarisShips.Content.NPCs
{
    public class HitShipNPC : GlobalNPC
    {
        public override void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
        {
            if (target.type == ModContent.NPCType<ShipNPC>())
            {
                target.GetShipNPC().HurtTimer = 180;
                if (target.life > 0)
                {
                    target.GetShipNPC().CurrentShield -= hit.Damage;
                    if (target.GetShipNPC().CurrentShield < 0)
                    {
                        target.GetShipNPC().CurrentShield = 0;
                    }
                    target.GetShipNPC().ImmuneTime = target.GetShipNPC().MaxImmuneTime;
                }
                else
                {
                    target.GetShipNPC().CurrentShield = 0;
                }
                if (hit.Damage > 1)      //闪避时不生效
                {
                    SoundEngine.PlaySound(SoundID.NPCHit4, target.Center);
                }
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.type == ModContent.NPCType<ShipNPC>())
            {
                npc.GetShipNPC().HurtTimer = 180;
                if (npc.life > 0)
                {
                    npc.GetShipNPC().CurrentShield -= damageDone;
                    if (npc.GetShipNPC().CurrentShield < 0)
                    {
                        npc.GetShipNPC().CurrentShield = 0;
                    }
                    npc.GetShipNPC().ImmuneTime = npc.GetShipNPC().MaxImmuneTime;
                }
                else
                {
                    npc.GetShipNPC().CurrentShield = 0;
                }

                if (projectile.penetrate > 0 || projectile.tileCollide) projectile.penetrate = 0;

                if (npc.GetShipNPC().CurrentShield > 0)
                {
                    float rot = (projectile.Center - npc.Center).ToRotation();
                    float r = new Vector2(npc.GetShipNPC().ShipWidth, npc.GetShipNPC().ShipLength).Distance(Vector2.Zero) / 2f + 40f;
                    float hitr = projectile.Distance(npc.Center);
                    if (projectile.Distance(npc.Center) > r * 0.9f) hitr = r * 0.9f;
                    if (npc.GetShipNPC().ShieldDRLevel > 0)
                    {
                        ShieldHitEffect.Summon(npc, npc.Center, r, hitr, rot, npc.GetShipNPC().ShieldDRLevel.ToString());
                    }
                    else
                    {
                        ShieldHitEffect.Summon(npc, npc.Center, r, hitr, rot);
                    }
                    SomeUtils.PlaySoundRandom(SoundPath.Shield, 6, npc.Center + rot.ToRotationVector2() * r);

                }
                else
                {
                    if (hit.Damage > 1)      //闪避时不生效
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit4, npc.Center);
                    }
                }
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)  //使用玩家所受的弹幕伤害
        {
            if (npc.type == ModContent.NPCType<ShipNPC>())
            {
                modifiers.SourceDamage *= SomeUtils.ProjWorldDamage;
            }
        }

        public override bool CanBeHitByNPC(NPC npc, NPC attacker)
        {
            if (npc.type == ModContent.NPCType<ShipNPC>())
            {
                if (npc.GetShipNPC().ImmuneTime > 0) return false;
            }
            return true;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (npc.type == ModContent.NPCType<ShipNPC>())
            {
                if (npc.GetShipNPC().ImmuneTime > 0) return false;
            }
            return null;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.type == ModContent.NPCType<ShipNPC>())
            {
                //根据难度修正防御加成和玩家相同
                modifiers.DefenseEffectiveness *= (SomeUtils.ProjWorldDamage + 1) / 2f;
                if (npc.GetShipNPC().CurrentShield > 0)
                {
                    modifiers.FinalDamage *= 1 - npc.GetShipNPC().ShieldDR;
                }
                if (FleetSystem.AuraType.Contains(AuraID.QuantumDestabilizer))    //量子去稳器
                {
                    modifiers.FinalDamage *= 1f - 0.15f;
                }

                float evasionRate = npc.GetShipNPC().Evasion / 100f;
                if (FleetSystem.AuraType.Contains(AuraID.AncientTargetScrambler))    //远古索敌扰频器
                {
                    evasionRate = 1 - (1 - evasionRate) * (1 - 0.3f);
                }
                modifiers.ModifyHitInfo += (ref NPC.HitInfo info) =>
                {
                    if (Main.rand.NextFloat() < evasionRate)
                    {
                        info.Damage = 1;
                        info.Crit = false;
                    };
                    info.HideCombatText = true;
                };
            }
            else
            {
                if (!npc.friendly)                //护盾挡板
                {
                    if (FleetSystem.AuraType.Contains(AuraID.ShieldDampener))
                    {
                        modifiers.FinalDamage *= 1f + 0.1f;
                    }
                }
            }
        }


        public override void FindFrame(NPC npc, int frameHeight)
        {
            if (npc.type != ModContent.NPCType<ShipNPC>() && npc.type != ModContent.NPCType<FallenShip>() && !npc.friendly)
            {
                if (FleetSystem.AuraType.Contains(AuraID.SubspaceSnare))                //亚空间陷阱
                {
                    npc.position -= npc.velocity * 0.3f;
                }
            }
        }
    }

    public class DrawTrailSystem : ModSystem
    {
        public override void Load()
        {
            On_Main.DoDraw_DrawNPCsOverTiles+= DrawTrails;
        }
        public override void Unload()
        {
            On_Main.DoDraw_DrawNPCsOverTiles -= DrawTrails;
        }

        private void DrawTrails(On_Main.orig_DoDraw_DrawNPCsOverTiles orig, Main self)
        {
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
            try
            {
                foreach (NPC ship in Main.ActiveNPCs)
                {
                    if (ship.ShipActive() && !ship.IsABestiaryIconDummy)
                    {
                        EverythingLibrary.Ships[ship.GetShipNPC().ShipGraph.ShipType].DrawTrail(Main.spriteBatch, Main.screenPosition, ship);
                    }
                }
            }
            catch (Exception e)
            {
                TimeLogger.DrawException(e);
            }
            Main.spriteBatch.End();
            orig.Invoke(self);
        }
    }

}