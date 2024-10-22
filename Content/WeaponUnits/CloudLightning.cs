using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class CloudLightningUnit : BaseWeaponUnit
    {
        public override string InternalName => "CloudLightning";

        private int LastTarget = -1;
        private int ReAttackTime = 0;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                LastTarget = -1;
                ReAttackTime = 0;
                if (CurrentCooldown > 26)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 21)
                {
                    CurrentCooldown = 26;
                }
                else
                {
                    CurrentCooldown = AttackCD;
                }
            }
            else                   //进战状态
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    Rotation = (Main.npc[target].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if ((int)CurrentCooldown == 20)
                    {
                        Color color = Color.LightSkyBlue;

                        if (LastTarget == target)   //连续攻击同一敌人伤害提高
                        {
                            if (ReAttackTime < 6) ReAttackTime++;
                        }
                        else
                        {
                            ReAttackTime = 0;
                        }
                        int damage = RandomDamage;
                        damage += (int)(damage * 0.1f * ReAttackTime);
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        int protmp = DamageProj.Summon(ship.GetSource_FromAI(), Main.npc[target].Center, damage, crit, 0f);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        Vector2 ShootPos = RelativePos;
                        ArcProj.Summon(ship, ShootPos, Rotation.ToRotationVector2() * 10, Main.npc[target].Center, color, new Vector2(0.5f, 0.4f));
                        ArcProj.Summon(ship, ShootPos, Rotation.ToRotationVector2() * 10, Main.npc[target].Center, color, new Vector2(0.5f, 0.4f));

                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Arc", 4, shipNPC.GetPosOnShip(RelativePos));
                        LastTarget = target;
                    }

                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD;
                    }
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown > 26)
                    {
                    }
                    else if (CurrentCooldown > 21)
                    {
                        CurrentCooldown = 26;
                    }
                    else
                    {
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown > 0 && CurrentCooldown <= 25)      //中5开亮，后20帧变暗
            {
                Color color = Color.LightSkyBlue;
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) + Rotation.ToRotationVector2() * 6;

                float t1 = MathHelper.Lerp(0.75f, 1f, MathHelper.Clamp((25f - CurrentCooldown) / 5f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 5f, 0f, 1f));

                Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.18f * k;
                    Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, color * (1f - k) * t2, 0, OffSet, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, Color.White * t2, 0, OffSet, t1 * 0.06f, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }


        }

    }

}