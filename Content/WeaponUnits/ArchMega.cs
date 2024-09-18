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
    public class ArchMegaUnit : BaseWeaponUnit
    {
        public override string InternalName => "ArchMega";

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                if (CurrentCooldown > 21)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 16)
                {
                    CurrentCooldown = 21;
                }
                else
                {
                    CurrentCooldown = AttackCD;
                }
            }
            else                   //½øÕ½×´Ì¬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    if ((int)CurrentCooldown == 15)
                    {
                        float scaleSlot = 2.25f;

                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        float t = Main.npc[target].Distance(shipNPC.GetPosOnShip(RelativePos)) / 50f;
                        Vector2 ShootVel = Vector2.Normalize(Main.npc[target].Center + (Main.npc[target].position - Main.npc[target].oldPosition) * t - shipNPC.GetPosOnShip(RelativePos)) * 50f;
                        MegaBullet.Summon(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(RelativePos), ShootVel, damage, scaleSlot, crit, true);
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Mega", 1, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD;
                    }
                }
                else
                {
                    if (CurrentCooldown > 21)
                    {

                    }
                    else if (CurrentCooldown > 16)
                    {
                        CurrentCooldown = 21;
                    }
                    else
                    {
                        CurrentCooldown = AttackCD;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown > 0 && CurrentCooldown <= 20)
            {
                Color color = Color.Orange;
                float scaleSlot = 2.5f;

                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) + ship.rotation.ToRotationVector2() * 12;

                float t1 = MathHelper.Lerp(0.1f, 1f, MathHelper.Clamp((20f - CurrentCooldown) / 5f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 15f, 0f, 1f));

                Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.225f * k * scaleSlot;
                    Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, color * (1f - k) * t2, 0, OffSet, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, Color.White * t2, 0, OffSet, t1 * 0.075f * scaleSlot, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }

        }
    }

}