using Microsoft.Xna.Framework;
using StellarisShips.Content.Items;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

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
                SoundEngine.PlaySound(SoundID.NPCHit4, target.Center);
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

                if (npc.GetShipNPC().CurrentShield > 0)
                {
                    float rot = (projectile.Center - npc.Center).ToRotation();
                    float r = new Vector2(npc.GetShipNPC().ShipWidth, npc.GetShipNPC().ShipLength).Distance(Vector2.Zero) / 2f + 40f;
                    if (npc.GetShipNPC().ShieldDRLevel > 0)
                    {
                        ShieldHitEffect.Summon(npc, npc.Center, r, rot, npc.GetShipNPC().ShieldDRLevel.ToString());
                    }
                    else
                    {
                        ShieldHitEffect.Summon(npc, npc.Center, r, rot);
                    }
                    SomeUtils.PlaySoundRandom(SoundPath.Shield, 6, npc.Center + rot.ToRotationVector2() * r);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.NPCHit4, npc.Center);
                }
                if (projectile.penetrate > 0) projectile.penetrate = 0;
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
                float evasionRate = npc.GetShipNPC().Evasion / 100f;
                modifiers.ModifyHitInfo += (ref NPC.HitInfo info) =>
                {
                    if (Main.rand.NextFloat() < evasionRate)
                    {
                        info.Damage = 1;
                    };
                    info.HideCombatText = true;
                };
            }
        }


    }


}