using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class PDefenseUnit : BaseWeaponUnit
    {
        public override string InternalName => "PDefense";

        public Vector2? TargetPos = null;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                Rotation = ship.rotation;
                TargetPos = null;
                if (CurrentCooldown > 11)
                {
                    CurrentCooldown--;
                }
                else
                {
                    CurrentCooldown = 11;
                }
            }
            else                   //½øÕ½×´Ì¬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                int target = ship.GetClosestTargetWithWidthForPointDefense(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    TargetPos = Main.npc[target].Center;
                    Rotation = (TargetPos.Value - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if ((int)CurrentCooldown == 7)
                    {
                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        if (!Main.npc[target].IsBoss()) damage = (int)(damage * 1.3f);
                        float IgnoreDefense = Main.npc[target].IsBoss() ? 1f : 0f;
                        int protmp = DamageProj.Summon(ship.GetSource_FromAI(), TargetPos.Value, damage, crit, IgnoreDefense);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "PDefense", 4, shipNPC.GetPosOnShip(RelativePos));
                    }
                    if (CurrentCooldown < 10)
                    {
                        Color LaserColor = Color.White;
                        switch (Level)
                        {
                            case 1:
                                LaserColor = Color.Red;
                                break;
                            case 2:
                                LaserColor = Color.LightBlue;
                                break;
                            case 3:
                                LaserColor = Color.White;
                                break;
                        }
                        SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos.Value, LaserColor);
                    }
                    if (CurrentCooldown <= 0)
                    {
                        CurrentCooldown = AttackCD;// * (0.8f + 0.4f * Main.rand.NextFloat());
                    }
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown < 11)
                    {
                        CurrentCooldown = 11;
                    }
                    TargetPos = null;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (TargetPos.HasValue && CurrentCooldown > 0 && CurrentCooldown <= 10)
            {
                float t = MathHelper.Lerp(0f, 1f, MathHelper.Clamp((10 - CurrentCooldown) / 3f, 0f, 1f));
                if (CurrentCooldown < 7) t = MathHelper.Lerp(0f, 1f, MathHelper.Clamp(CurrentCooldown / 7f, 0f, 1f));

                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos);
                Color LaserColor = Color.White;
                int width = 2;
                switch (Level)
                {
                    case 1:
                        LaserColor = Color.Red;
                        break;
                    case 2:
                        LaserColor = Color.LightBlue;
                        break;
                    case 3:
                        LaserColor = Color.White;
                        break;
                }
                LaserColor *= t;
                /*
                EasyDraw.AnotherDraw(BlendState.Additive);
                Vector2 CenterOffset = new(width / 2f, width / 2f);
                Vector2 UnitY = ((TargetPos.Value - SelfPos).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2() * (width / 2f + 1f);
                Utils.DrawLine(spriteBatch, TargetPos.Value - CenterOffset, SelfPos - CenterOffset, LaserColor, LaserColor, width);
                Utils.DrawLine(spriteBatch, TargetPos.Value + UnitY - CenterOffset, SelfPos + UnitY - CenterOffset, LaserColor * 0.75f, LaserColor * 0.75f, width);
                Utils.DrawLine(spriteBatch, TargetPos.Value - UnitY - CenterOffset, SelfPos - UnitY - CenterOffset, LaserColor * 0.75f, LaserColor * 0.75f, width);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
                */
                Texture2D Tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/BloomLine").Value;
                void DrawMain(Color color, float scale)
                {
                    List<CustomVertexInfo> vertexInfos = new();
                    float width2 = scale * width;
                    Vector2 UnitY = ((TargetPos.Value - SelfPos).ToRotation() + MathHelper.Pi / 2f).ToRotationVector2();
                    vertexInfos.Add(new CustomVertexInfo(SelfPos + UnitY * width2, Color.White, new Vector3(0, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(SelfPos - UnitY * width2, Color.White, new Vector3(0, 1f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(TargetPos.Value + UnitY * width2, Color.White, new Vector3(1, 0f, 1)));
                    vertexInfos.Add(new CustomVertexInfo(TargetPos.Value - UnitY * width2, Color.White, new Vector3(1, 1f, 1)));
                    DrawUtils.DrawTrail(Tex1, vertexInfos, Main.spriteBatch, color, BlendState.Additive);
                }
                for (float i = 0; i < 1; i += 1f / 5f)
                {
                    DrawMain(LaserColor * (1 - i), 0.5f + i * 5f);
                }
                DrawMain(Color.White, 0.5f);

                Texture2D Tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                EasyDraw.AnotherDraw(BlendState.Additive);
                for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                {
                    float scale = 0.02f * k * width;
                    Main.spriteBatch.Draw(Tex2, TargetPos.Value - screenPos, null, LaserColor * (1f - k), 0, Tex2.Size() / 2f, scale, SpriteEffects.FlipHorizontally, 0);
                }

                Main.spriteBatch.Draw(Tex2, TargetPos.Value - screenPos, null, Color.White, 0, Tex2.Size() / 2f, 0.005f * width, SpriteEffects.FlipHorizontally, 0);
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
        }
    }

}