using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class TorpedoEXUnit : BaseWeaponUnit
    {
        public override string InternalName => "Torpedo2";

        public float DetectRange = 600;
        public float HomingFactor = 0.05f;
        public float MaxSpeed = 10;
        public float ExplosionScale = 1f;
        public int TimeLeft = 480;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                Rotation = ship.rotation;
                if (CurrentCooldown > 31)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 21)
                {
                    CurrentCooldown = 31;
                }
                else
                {
                    CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                }
            }
            else                   //��ս״̬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    float t = Main.npc[target].Distance(shipNPC.GetPosOnShip(RelativePos)) / 8f;
                    Rotation = (Main.npc[target].Center + Main.npc[target].velocity * t - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if ((int)CurrentCooldown == 20)
                    {
                        Color color = Color.White;
                        switch (Level)
                        {
                            case 1:
                                color = Color.LightGreen;
                                break;
                            case 2:
                                color = Color.SkyBlue;
                                break;
                        }

                        int damage = RandomDamage / 5;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        Vector2 ShootVel = Rotation.ToRotationVector2() * 20f;
                        int protmp = TorpedoEXProj.Summon(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(RelativePos), ShootVel, damage, color,
                            ExplosionScale, MaxSpeed, DetectRange, target, TimeLeft, HomingFactor,
                            crit);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Torpedo2", 3, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                    }
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown > 31)
                    {
                    }
                    else if (CurrentCooldown > 21)
                    {
                        CurrentCooldown = 31;
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
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown > 0 && CurrentCooldown <= 40)
            {
                Color color = Color.White;
                switch (Level)
                {
                    case 1:
                        color = Color.LightGreen;
                        break;
                    case 2:
                        color = Color.LightBlue;
                        break;
                }
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) + Rotation.ToRotationVector2() * 10;

                float t1 = MathHelper.Lerp(0.1f, 1f, MathHelper.Clamp((30f - CurrentCooldown) / 10f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 20f, 0f, 1f));

                Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.2f * k;
                    Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, color * (1f - k) * t2, 0, OffSet, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, Color.White * t2, 0, OffSet, t1 * 0.07f, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }

        }
    }

}