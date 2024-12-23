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
    public class FlakUnit : BaseWeaponUnit
    {
        public override string InternalName => "Flak";

        public Vector2? TargetPos1 = null;

        public Vector2? TargetPos2 = null;

        public int AttackTimer = 0;

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (CurrentCooldown > 0) CurrentCooldown--;
            if (shipNPC.CurrentTarget == -1)
            {
                AttackTimer = 0;
                Rotation = ship.rotation;
                TargetPos1 = null;
                TargetPos2 = null;
            }
            else                   //进战状态
            {
                AttackTimer++;
                int target1 = ship.GetClosestTargetWithWidthForPointDefense(MaxRange, MinRange, shipNPC.CurrentTarget);
                int target2 = ship.GetClosestTargetWithWidthForPointDefense(MaxRange, MinRange, target1, shipNPC.CurrentTarget);
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
                        BulletColor = Color.IndianRed;
                        break;
                    case 4:
                        BulletColor = Color.Red;
                        break;
                }

                if (target1 != -1)
                {
                    if (AttackTimer % 50 == 1)
                    {
                        SomeUtils.PlaySoundRandom(SoundPath.Fire + "AutoCannon", 1, shipNPC.GetPosOnShip(RelativePos));
                    }
                    TargetPos1 = Main.npc[target1].Center;
                    Rotation = (TargetPos1.Value - shipNPC.GetPosOnShip(RelativePos)).ToRotation();

                    SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos1.Value, BulletColor);
                }
                else
                {
                    Rotation = (Main.npc[shipNPC.CurrentTarget].Center - shipNPC.GetPosOnShip(RelativePos)).ToRotation();
                    TargetPos1 = null;
                }

                if (target2 != -1)
                {
                    TargetPos2 = Main.npc[target2].Center;
                    SomeUtils.AddLightLine(shipNPC.GetPosOnShip(RelativePos), TargetPos2.Value, BulletColor);
                }
                else
                {
                    TargetPos2 = null;
                }

                if (CurrentCooldown <= 0)
                {
                    bool success = false;
                    if (TargetPos1.HasValue)
                    {
                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        float IgnoreDefense = Main.npc[target2].IsBoss() ? 1f : 0f;
                        int protmp = DamageProj.Summon(ship.GetSource_FromAI(), TargetPos1.Value, damage, crit, IgnoreDefense);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        success = true;
                    }
                    if (TargetPos2.HasValue && target2 != target1)           //不能攻击相同目标
                    {
                        int damage = RandomDamage;
                        bool crit = Main.rand.NextFloat() < Crit / 100f;
                        float IgnoreDefense = Main.npc[target2].IsBoss() ? 1f : 0f;
                        int protmp = DamageProj.Summon(ship.GetSource_FromAI(), TargetPos2.Value, damage, crit, IgnoreDefense);
                        if (protmp >= 0 && protmp < 1000) (Main.projectile[protmp].ModProjectile as BaseDamageProjectile).SourceName = EverythingLibrary.Components[ComponentName].GetLocalizedName();
                        success = true;
                    }
                    if (success)
                    {
                        CurrentCooldown = AttackCD * (0.8f + 0.4f * Main.rand.NextFloat());
                    }

                }


            }
        }

        public override void Draw(SpriteBatch spriteBatch, NPC ship, Vector2 screenPos)
        {
            ShipNPC shipNPC = ship.GetShipNPC();

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
                    BulletColor = Color.IndianRed;
                    break;
                case 4:
                    BulletColor = Color.Red;
                    break;
            }

            Texture2D tex1 = ModContent.Request<Texture2D>("StellarisShips/Images/Common/BulletHell2").Value;
            EasyDraw.AnotherDraw(BlendState.Additive);
            void DrawLine(Vector2 targetPos)
            {
                float dist = targetPos.Distance(SelfPos);
                float rot = (targetPos - SelfPos).ToRotation();
                Vector2 UnitX = Vector2.Normalize(targetPos - SelfPos);
                for (int i = 0; i < (int)(dist / 5f); i++)       //400,10,5,200
                {
                    int MaxDevide = 40;
                    int progress = i - AttackTimer + MaxDevide;
                    while (progress < 0) progress += MaxDevide;
                    while (progress >= MaxDevide) progress -= MaxDevide;
                    Vector2 DrawPos = SelfPos + UnitX * 5f * i - screenPos;
                    Rectangle DrawRec = new(progress * tex1.Width / MaxDevide, 0, tex1.Width / MaxDevide, tex1.Height);
                    Vector2 DrawOrigin = new(0, tex1.Height / 2);
                    spriteBatch.Draw(tex1, DrawPos, DrawRec, BulletColor, rot, DrawOrigin, 0.5f, SpriteEffects.None, 0);
                }

                //末端加上粒子
                Vector2 End = targetPos;
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
            if (TargetPos1.HasValue)
            {
                DrawLine(TargetPos1.Value);
            }
            if (TargetPos2.HasValue)
            {
                DrawLine(TargetPos2.Value);
            }
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
        }

    }

}