using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class LanceUnit : BaseWeaponUnit
    {
        public override string InternalName => "Lance";


        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                if (CurrentCooldown > 76)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 26)
                {
                    CurrentCooldown = 76;
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
                    if ((int)CurrentCooldown == 75)
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.Other + "Charge", 1, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if ((int)CurrentCooldown == 25)
                    {
                        Color color = Color.White;
                        switch (Level)
                        {
                            case 1:
                                color = Color.IndianRed;
                                break;
                            case 2:
                                color = Color.LightSkyBlue;
                                break;
                        }

                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        LanceProj.Summon(ship, RelativePos, Main.npc[target].Center, color, damage, crit);
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Lance", 3, shipNPC.GetPosOnShip(RelativePos));
                    }

                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD;

                    }
                }
                else
                {
                    if (CurrentCooldown > 76)
                    {
                    }
                    else if (CurrentCooldown > 26)
                    {
                        CurrentCooldown = 76;
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
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown > 0 && CurrentCooldown <= 75)      //前50帧闪烁，中5开亮，后20帧变暗
            {
                Color color = Color.White;
                switch (Level)
                {
                    case 1:
                        color = Color.IndianRed;
                        break;
                    case 2:
                        color = Color.LightSkyBlue;
                        break;
                }
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos) + ship.rotation.ToRotationVector2() * 12;

                float t1 = MathHelper.Lerp(0.75f, 1f, MathHelper.Clamp((25f - CurrentCooldown) / 5f, 0f, 1f));
                float t2 = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 5f, 0f, 1f));
                if (CurrentCooldown > 25)
                {
                    float c = (float)Math.Sin((CurrentCooldown - 25f) / 10f * MathHelper.TwoPi);
                    t1 = 0.75f + 0.25f * c;
                    t2 = 1;
                }

                Texture2D Tex = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 OffSet = Tex.Size() / 2f;
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.45f * k;
                    Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, color * (1f - k) * t2, 0, OffSet, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex, SelfPos - screenPos, null, Color.White * t2, 0, OffSet, t1 * 0.15f, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }

        }
    }

}