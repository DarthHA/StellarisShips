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
    public class AutoCannonUnit : BaseWeaponUnit
    {
        public override string InternalName => "AutoCannon";

        public Vector2? TargetPos = null;

        public int AttackTimer = 0;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (CurrentCooldown > 0) CurrentCooldown--;
            if (shipNPC.CurrentTarget == -1)
            {
                AttackTimer = 0;
                Rotation = ship.rotation;
                TargetPos = null;
            }
            else                   //进战状态
            {
                AttackTimer++;
                int target = ship.GetClosestTargetWithWidth(MaxRange, MinRange, shipNPC.CurrentTarget);
                if (target != -1)
                {
                    if (AttackTimer % 50 == 1)
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "AutoCannon", 1, shipNPC.GetPosOnShip(RelativePos));
                    }
                    TargetPos = Main.npc[target].Center;
                    Rotation = (TargetPos.Value - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    if (CurrentCooldown <= 0)
                    {
                        int damage = RandomDamage;
                        float distanceFactor = 2f - Main.npc[target].DistanceWithWidth(ship) / (MaxRange + ship.Size.Distance(Vector2.Zero) / 2f);
                        if (distanceFactor < 1f) distanceFactor = 1;
                        damage = (int)(damage * distanceFactor);
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        int protmp = DamageProj.Summon(ship.GetSource_FromAI(), TargetPos.Value, damage, crit, 0.5f);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "AutoCannon", 3, shipNPC.GetPosOnShip(RelativePos));
                    }
                    Color BulletColor = Color.White;
                    switch (Level)
                    {
                        case 1:
                            BulletColor = Color.Orange;
                            break;
                        case 2:
                            BulletColor = Color.LightGreen;
                            break;
                        case 3:
                            BulletColor = Color.LightBlue;
                            break;
                        case 4:
                            BulletColor = Color.LimeGreen;
                            break;
                    }
                    SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos.Value, BulletColor);
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
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
                Color BulletColor = Color.White;
                switch (Level)
                {
                    case 1:
                        BulletColor = Color.Orange;
                        break;
                    case 2:
                        BulletColor = Color.LightGreen;
                        break;
                    case 3:
                        BulletColor = Color.LightBlue;
                        break;
                    case 4:
                        BulletColor = Color.LimeGreen;
                        break;
                }
                int num = 1;
                switch (EquipType)
                {
                    case ComponentTypes.Weapon_S:
                        num = 1;
                        break;
                    case ComponentTypes.Weapon_M:
                        num = 2;
                        break;
                    case ComponentTypes.Weapon_L:
                        num = 3;
                        break;
                }

                EasyDraw.AnotherDraw(BlendState.Additive);
                for (int t = 0; t < num; t++)
                {
                    Texture2D tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Common/BulletHell").Value;
                    float dist = TargetPos.Value.Distance(SelfPos);
                    float rot = (TargetPos.Value - SelfPos).ToRotation();
                    Vector2 UnitX = Vector2.Normalize(TargetPos.Value - SelfPos);
                    Vector2 UnitY = (rot + MathHelper.Pi / 2).ToRotationVector2();
                    Vector2 OffSet = UnitY * (int)(t - (num - 1f) / 2f) * 4f;
                    for (int i = 0; i < (int)(dist / 5f); i++)       //400,10,5,200
                    {
                        int MaxDevide = 40;
                        int progress = i - AttackTimer * 2 + t * MaxDevide / num;
                        while (progress < 0) progress += MaxDevide;
                        while (progress >= MaxDevide) progress -= MaxDevide;
                        Vector2 DrawPos = SelfPos + UnitX * 5f * i + OffSet - screenPos;
                        Rectangle DrawRec = new(progress * tex1.Width / MaxDevide, 0, tex1.Width / MaxDevide, tex1.Height);
                        Vector2 DrawOrigin = new(0, tex1.Height / 2);
                        spriteBatch.Draw(tex1, DrawPos, DrawRec, BulletColor, rot, DrawOrigin, 0.5f, SpriteEffects.None, 0);
                    }

                    //末端加上粒子
                    Vector2 End = TargetPos.Value + OffSet;
                    int progress2 = AttackTimer % 10;

                    float t1 = MathHelper.Lerp(0.75f, 1f, MathHelper.Clamp(progress2 / 5f, 0f, 1f));
                    float t2 = MathHelper.Lerp(1f, 0.75f, MathHelper.Clamp((10 - progress2) / 5f, 0f, 1f));

                    Texture2D tex2 = ModContent.Request<Texture2D>("StellarisShips/Images/Effects/CrispCircle").Value;
                    Vector2 OffSet2 = tex2.Size() / 2f;
                    for (float k = 0.5f; k <= 1.5f; k += 0.05f)
                    {
                        float scale = 0.1f * k;
                        Main.spriteBatch.Draw(tex2, End - screenPos, null, BulletColor * (1f - k) * t2, 0, OffSet2, t1 * scale, SpriteEffects.FlipHorizontally, 0);
                    }

                    Main.spriteBatch.Draw(tex2, End - screenPos, null, Color.White * t2, 0, OffSet2, t1 * 0.035f, SpriteEffects.FlipHorizontally, 0);


                }
                EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            }
        }
    }

}