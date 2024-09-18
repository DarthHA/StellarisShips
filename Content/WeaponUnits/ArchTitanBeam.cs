using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class ArchTitanBeamUnit : BaseWeaponUnit
    {
        public override string InternalName => "ArchTitanBeam";

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                if (CurrentCooldown > 136)
                {
                    CurrentCooldown--;
                }
                else if (CurrentCooldown > 26)
                {
                    CurrentCooldown = 136;
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
                    if ((int)CurrentCooldown == 135)
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.Other + "TitanCharge", 1, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if ((int)CurrentCooldown == 25)
                    {
                        Color color = Color.LightGreen;
                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        TitanBeamProj.Summon(ship, RelativePos, Main.npc[target].Center, color, damage, crit);
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "TitanBeam", 1, shipNPC.GetPosOnShip(RelativePos));
                    }

                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD;

                    }
                }
                else
                {
                    if (CurrentCooldown > 136)
                    {
                    }
                    else if (CurrentCooldown > 26)
                    {
                        CurrentCooldown = 136;
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
            if (shipNPC.CurrentTarget != -1 && CurrentCooldown <= 135)      //135-115 一级充能，115-95二级充能，95-75三级充能，75-55四级充能，55-35满充能,25激发，0消失
            {
                void DrawMain(Texture2D tex, float alpha)
                {
                    Vector2 DrawCenter = ship.Center - ship.rotation.ToRotationVector2() * shipNPC.TailDrawOffset * shipNPC.shipScale;
                    EasyDraw.AnotherDraw(BlendState.Additive);
                    spriteBatch.Draw(tex, DrawCenter - screenPos, null, Color.LightGreen * alpha, ship.rotation, new Vector2(tex.Width / 2, tex.Height / 2), 0.75f * shipNPC.shipScale, SpriteEffects.FlipHorizontally, 0);
                    EasyDraw.AnotherDraw(BlendState.AlphaBlend);
                };
                float t2;
                int t1;
                if (CurrentCooldown >= 115) { t1 = 1; t2 = (135 - CurrentCooldown) / 20f; }
                else if (CurrentCooldown >= 95) { t1 = 2; t2 = (115 - CurrentCooldown) / 20f; }
                else if (CurrentCooldown >= 75) { t1 = 3; t2 = (95 - CurrentCooldown) / 20f; }
                else if (CurrentCooldown >= 55) { t1 = 4; t2 = (75 - CurrentCooldown) / 20f; }
                else if (CurrentCooldown >= 35) { t1 = 5; t2 = (55 - CurrentCooldown) / 20f; }
                else { t1 = 5; t2 = 1; }
                if (t1 > 1)
                {
                    Texture2D tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Common/TitanCharge" + (t1 - 1).ToString(), AssetRequestMode.ImmediateLoad).Value;
                    if (CurrentCooldown < 35) tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Common/TitanCharge" + t1.ToString(), AssetRequestMode.ImmediateLoad).Value;
                    DrawMain(tex2, 0.75f);
                    DrawMain(tex2, 0.75f);
                }
                Texture2D tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Common/TitanCharge" + t1.ToString(), AssetRequestMode.ImmediateLoad).Value;
                DrawMain(tex1, t2 * 0.75f);
                DrawMain(tex1, t2 * 0.75f);
            }
        }
    }

}