using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class LaserUnit : BaseWeaponUnit
    {
        public override string InternalName => "Laser";

        public Vector2? TargetPos = null;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (CurrentCooldown > 0) CurrentCooldown--;
            if (shipNPC.CurrentTarget == -1)
            {
                Rotation = ship.rotation;
                TargetPos = null;
            }
            else                   //进战状态
            {
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    TargetPos = Main.npc[target].Center;
                    Rotation = (TargetPos.Value - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown <= 0)
                    {
                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        DamageProj.Summon(ship.GetSource_FromAI(), TargetPos.Value, damage, crit, 1f);
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "Laser", 6, shipNPC.GetPosOnShip(RelativePos));
                    }

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
                            LaserColor = Color.MediumPurple;
                            break;
                        case 4:
                            LaserColor = Color.LightGreen;
                            break;
                        case 5:
                            LaserColor = Color.Orange;
                            break;
                    }
                    SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos.Value, LaserColor);
                }
                else
                {
                    TargetPos = null;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (TargetPos.HasValue)
            {
                Vector2 SelfPos = shipNPC.GetPosOnShip(RelativePos);
                Color LaserColor = Color.White;
                int width = 1;
                switch (EquipType)
                {
                    case ComponentTypes.Weapon_S:
                        width = 2;
                        break;
                    case ComponentTypes.Weapon_M:
                        width = 4;
                        break;
                    case ComponentTypes.Weapon_L:
                        width = 6;
                        break;
                }
                switch (Level)
                {
                    case 1:
                        LaserColor = Color.Red;
                        break;
                    case 2:
                        LaserColor = Color.LightBlue;
                        break;
                    case 3:
                        LaserColor = Color.MediumPurple;
                        break;
                    case 4:
                        LaserColor = Color.LightGreen;
                        break;
                    case 5:
                        LaserColor = Color.Orange;
                        break;
                }

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