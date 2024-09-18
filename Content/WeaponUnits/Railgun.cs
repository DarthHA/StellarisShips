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
    public class RailgunUnit : BaseWeaponUnit
    {
        public override string InternalName => "Railgun";

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                Rotation = ship.rotation;
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
                    CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                }
            }
            else                   //��ս״̬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    float t = Main.npc[target].Distance(shipNPC.GetPosOnShip(RelativePos)) / 30f;
                    Rotation = (Main.npc[target].Center + (Main.npc[target].position - Main.npc[target].oldPosition) * t - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if ((int)CurrentCooldown == 15)
                    {
                        float scaleSlot = 1f;
                        switch (EquipType)
                        {
                            case ComponentTypes.Weapon_S:
                                scaleSlot = 0.75f;
                                break;
                            case ComponentTypes.Weapon_M:
                                scaleSlot = 1.25f;
                                break;
                            case ComponentTypes.Weapon_L:
                                scaleSlot = 1.5f;
                                break;
                        }

                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        Vector2 ShootVel = Rotation.ToRotationVector2() * 30f;
                        RailgunBullet.Summon(ship.GetSource_FromAI(), shipNPC.GetPosOnShip(RelativePos), ShootVel, damage, Color.White, scaleSlot, crit);
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Railgun", 3, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                    }
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown > 21)
                    {
                    }
                    else if (CurrentCooldown > 16)
                    {
                        CurrentCooldown = 21;
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
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown > 0 && CurrentCooldown <= 20)
            {
                Color color = Color.White;
                float scaleSlot = 1f;
                switch (EquipType)
                {
                    case ComponentTypes.Weapon_S:
                        scaleSlot = 0.75f;
                        break;
                    case ComponentTypes.Weapon_M:
                        scaleSlot = 1.25f;
                        break;
                    case ComponentTypes.Weapon_L:
                        scaleSlot = 1.5f;
                        break;
                }
                switch (Level)
                {
                    case 1:
                        color = Color.Orange;
                        break;
                    case 2:
                        color = Color.LightGreen;
                        break;
                    case 3:
                        color = Color.MediumPurple;
                        break;
                    case 4:
                        color = Color.LimeGreen;
                        break;
                    case 5:
                        color = Color.LightBlue;
                        break;
                }
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) + Rotation.ToRotationVector2() * 8 * scaleSlot;

                float t1 = MathHelper.Lerp(0.1f, 1f, MathHelper.Clamp((20f - CurrentCooldown) / 5f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 15f, 0f, 1f));

                Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.15f * k * scaleSlot;
                    Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, color * (1f - k) * t2, 0, OffSet, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, Color.White * t2, 0, OffSet, t1 * 0.05f * scaleSlot, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }

        }
    }

}