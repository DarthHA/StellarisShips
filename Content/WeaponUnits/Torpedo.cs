using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class TorpedoUnit : BaseWeaponUnit
    {
        public override string InternalName => "Torpedo";

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
                if (CurrentCooldown > 41)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 21)
                {
                    CurrentCooldown = 41;
                }
                else
                {
                    CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                }
            }
            else                   //½øÕ½×´Ì¬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    Rotation = (Main.npc[target].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if ((int)CurrentCooldown == 20)
                    {
                        Color color = Color.White;
                        switch (Level)
                        {
                            case 1:
                                color = Color.White;
                                break;
                            case 2:
                                color = Color.LightBlue;
                                break;
                            case 3:
                                color = Color.LightGreen;
                                break;
                        }

                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        Vector2 ShootVel = Rotation.ToRotationVector2() * 10f;
                        TorpedoProj.Summon(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(RelativePos), ShootVel, damage, color,
                            ExplosionScale, MaxSpeed, DetectRange, target, TimeLeft, HomingFactor,
                            crit);
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Torpedo", 3, shipNPC.GetPosOnShip(RelativePos));
                    }

                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                    }
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown > 41)
                    {
                    }
                    else if (CurrentCooldown > 21)
                    {
                        CurrentCooldown = 41;
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

                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) - Rotation.ToRotationVector2() * 3;

                float t1 = MathHelper.Lerp(0.1f, 1f, MathHelper.Clamp((40f - CurrentCooldown) / 20f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 20f, 0f, 1f));

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