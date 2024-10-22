

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public partial class ShipNPC : ModNPC
    {
        //舰船图纸
        public ShipGraph ShipGraph = new();

        /// <summary>
        /// 舰船名称
        /// </summary>
        public string ShipName = "";

        /// <summary>
        /// 最大航速，也就是攻击时的航速
        /// </summary>
        public float MaxSpeed = 10;

        /// <summary>
        /// 基础航速，不加任何加成的，用于自由和列阵移动
        /// </summary>
        public float BaseSpeed = 10;
        /// <summary>
        /// 闪避
        /// </summary>
        public float Evasion = 0;
        /// <summary>
        /// 最大护盾量
        /// </summary>
        public int MaxShield = 0;

        /// <summary>
        /// 当前护盾量
        /// </summary>
        public int CurrentShield = 0;

        /// <summary>
        /// 护盾恢复速度
        /// </summary>
        public float ShieldRegen = 0;
        /// <summary>
        /// 船体恢复速度
        /// </summary>
        public float HullRegen = 0;

        /// <summary>
        /// 自修复计时器
        /// </summary>
        private int RegenTimer = 0;
        /// <summary>
        /// 船体自回血累计池，满1就回
        /// </summary>
        private float HullRegenProgress = 0;
        /// <summary>
        /// 护盾自回血累计池，满1就回
        /// </summary>
        private float ShieldRegenProgress = 0;

        /// <summary>
        /// 列阵时的目标坐标
        /// </summary>
        public Vector2 ShapePos = Vector2.Zero;

        /// <summary>
        /// 当前索敌目标
        /// </summary>
        public int CurrentTarget = -1;

        /// <summary>
        /// 索敌范围,基础200
        /// </summary>
        public int DetectRange = 400;

        /// <summary>
        /// 是否可以索敌物块后的敌人
        /// </summary>
        public bool CanSeeThroughTiles = false;

        /// <summary>
        /// 受伤时显示血条
        /// </summary>
        public int HurtTimer = 0;

        /// <summary>
        /// 所有武器的最大距离
        /// </summary>
        public int MaxRange = 600;
        /// <summary>
        /// 所有武器的最小距离
        /// </summary>
        public int MinRange = 0;
        /// <summary>
        /// 所有武器的中位距离
        /// </summary>
        public int MidRange = 300;

        /// <summary>
        /// 作战电脑类型
        /// </summary>
        public string ComputerType = "";

        /// <summary>
        /// 失踪剩余时间
        /// </summary>
        public int MissingTimer = 0;

        /// <summary>
        /// FTL等级
        /// </summary>
        public int FTLLevel = 0;

        /// <summary>
        /// FTL冷却
        /// </summary>
        public int FTLCooldown = 0;

        /// <summary>
        /// FTL最大冷却
        /// </summary>
        public int FTLMaxCooldown = 60 * 60;

        /// <summary>
        /// 发动FTL的准备时间
        /// </summary>
        public int FTLTimer = 0;

        /// <summary>
        /// FTL目标坐标
        /// </summary>
        public Vector2 FTLTargetPos = Vector2.Zero;

        /// <summary>
        /// 紧急超光速脱离概率
        /// </summary>
        public float EscapeChance = 0;

        /// <summary>
        /// 仇恨
        /// </summary>
        public int Aggro = 0;

        /// <summary>
        /// 最大无敌时间
        /// </summary>
        public int MaxImmuneTime = 0;

        /// <summary>
        /// 特殊计算的无敌时间
        /// </summary>
        public int ImmuneTime = 0;

        /// <summary>
        /// 护盾硬化效果，存在护盾时获得减伤
        /// </summary>
        public float ShieldDR = 0;

        /// <summary>
        /// 护盾染色用
        /// </summary>
        public float ShieldDRLevel = 0;

        /// <summary>
        /// 舰载机额外数量
        /// </summary>
        public int ExtraStriker = 0;


        /// <summary>
        /// 用于储存和计算增益，为整体乘算局部加算
        /// </summary>
        public Dictionary<string, float> BonusBuff = new();

        /// <summary>
        /// 可以随时修改的增益，为整体乘算局部加算
        /// </summary>
        public Dictionary<string, float> SpecialBuff = new();

        /// <summary>
        /// 携带光环种类
        /// </summary>
        public string AuraType = "";

        #region 以下参数用于绘制，可以减少调用量
        public Texture2D ShipTexture;
        public float shipScale;
        public float weaponScale;
        public int TailDrawOffset;
        public string TailType;
        public int ShipWidth;
        public int ShipLength;
        #endregion

        public List<BaseWeaponUnit> weapons = new();

        public override void SetStaticDefaults()
        {
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.TrailCacheLength[NPC.type] = 100;
            NPCID.Sets.TrailingMode[NPC.type] = 3;

            NPCID.Sets.MustAlwaysDraw[NPC.type] = true;
            NPCID.Sets.CannotDropSouls[NPC.type] = true;
            NPCID.Sets.NeverDropsResourcePickups[NPC.type] = true;
            NPCID.Sets.PositiveNPCTypesExcludedFromDeathTally[NPC.type] = true;
            NPCID.Sets.TeleportationImmune[NPC.type] = true;

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset[NPC.type] = drawModifier;

        }
        public override void SetDefaults()
        {
            NPC.width = 10;
            NPC.height = 10;
            NPC.damage = 0;
            NPC.lifeMax = 1;
            NPC.dontCountMe = true;
            NPC.friendly = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.trapImmune = true;
            NPC.aiStyle = -1;
            NPC.chaseable = false;
            NPC.canDisplayBuffs = false;
            NPC.CanBeReplacedByOtherNPCs = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (ShipGraph.ShipType == "" || NPC.IsABestiaryIconDummy || shipAI == ShipAI.Missing)
            {
                return false;
            }
            //绘制尾迹
            //EverythingLibrary.Ships[ShipGraph.ShipType].DrawTrail(spriteBatch, screenPos, NPC);
            //绘制船
            List<string> sectionList = new();
            foreach (SectionForSave str in ShipGraph.Parts)
            {
                sectionList.Add(str.InternalName);
            }
            Vector2 DrawCenter = NPC.Center - NPC.rotation.ToRotationVector2() * TailDrawOffset * shipScale;
            EasyDraw.AnotherDraw(BlendState.NonPremultiplied);
            spriteBatch.Draw(ShipTexture, DrawCenter - screenPos, null, Color.White, NPC.rotation, new Vector2(ShipTexture.Width / 2, ShipTexture.Height / 2), 0.75f * shipScale, SpriteEffects.FlipHorizontally, 0);
            EasyDraw.AnotherDraw(BlendState.AlphaBlend);
            //绘制武器
            DrawWeapons(spriteBatch, screenPos);
            //绘制血条
            DrawDescAndHealthBar(spriteBatch);

            //测试列队
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, ShapePos - screenPos, new Rectangle(0, 0, 5, 5), Color.White);
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, ShapeSystem.TipPos - screenPos, new Rectangle(0, 0, 5, 5), Color.Red);
            return false;
        }

        private void DrawWeapons(SpriteBatch spriteBatch, Vector2 screenPos)
        {
            foreach (BaseWeaponUnit weapon in weapons)
            {
                weapon.Draw(spriteBatch, NPC, screenPos);
                if (weapon.CannonName != "")
                {
                    Texture2D texWeapon = ModContent.Request<Texture2D>("StellarisShips/Images/Ships/" + weapon.CannonName + "_Gun", AssetRequestMode.ImmediateLoad).Value;
                    Vector2 DrawPos = NPC.Center + NPC.rotation.ToRotationVector2() * weapon.RelativePos.Y * shipScale + (NPC.rotation - MathHelper.Pi / 2).ToRotationVector2() * weapon.RelativePos.X * shipScale - NPC.rotation.ToRotationVector2() * ShipTexture.Width * 0.75f * shipScale * 0.5f;
                    float Facing = weapon.Rotation;
                    spriteBatch.Draw(texWeapon, DrawPos - screenPos, null, Color.White, Facing, texWeapon.Size() / 2f, shipScale * weaponScale, SpriteEffects.FlipHorizontally, 0);
                }
            }
        }

        public Vector2 GetPosOnShip(Vector2 vec)
        {
            List<string> sectionList = new();
            foreach (SectionForSave str in ShipGraph.Parts)
            {
                sectionList.Add(str.InternalName);
            }
            Texture2D shipTex = LibraryHelpers.GetShipTexture(sectionList);
            float shipScale = EverythingLibrary.Ships[ShipGraph.ShipType].ShipScale;
            Vector2 DrawPos = NPC.Center + NPC.rotation.ToRotationVector2() * vec.Y * shipScale + (NPC.rotation - MathHelper.Pi / 2).ToRotationVector2() * vec.X * shipScale - NPC.rotation.ToRotationVector2() * shipTex.Width * 0.75f * shipScale * 0.5f;
            return DrawPos;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void UpdateLifeRegen(ref int damage)       //禁用所有Dot和Hot
        {
            NPC.lifeRegen = 0;
            NPC.lifeRegenCount = 0;
            NPC.friendlyRegen = 0;
        }

        public void UpdateHullAndShieldRegen()
        {
            if (shipAI == ShipAI.Missing) return;
            if (++RegenTimer > 60)
            {
                RegenTimer = 0;
                if (shipAI != ShipAI.Attack)
                {
                    ShieldRegenProgress += ShieldRegen;
                    if (ShieldRegenProgress >= 1)
                    {
                        int ShieldRegenAmount = (int)ShieldRegenProgress;
                        ShieldRegenProgress -= ShieldRegenAmount;
                        if (ShieldRegenAmount > MaxShield - CurrentShield) ShieldRegenAmount = MaxShield - CurrentShield;
                        CurrentShield += ShieldRegenAmount;
                        NPC.life += ShieldRegenAmount;
                    }
                }
                else
                {
                    ShieldRegenProgress = 0;
                }

                float AuraBonus = FleetSystem.GlobalEffects.ContainsKey(AuraID.NanobotCloud) ? (NPC.lifeMax - MaxShield) * 0.015f : 0;       //纳米云
                AuraBonus += FleetSystem.GlobalEffects.ContainsKey(AuraID.ShroudRegenUp) ? (NPC.lifeMax - MaxShield) * 0.03f : 0;            //虚境回血
                HullRegenProgress += HullRegen + AuraBonus;
                if (HullRegenProgress >= 1)
                {
                    int HullRegenAmount = (int)HullRegenProgress;
                    HullRegenProgress -= HullRegenAmount;
                    if (HullRegenAmount > NPC.lifeMax - MaxShield - (NPC.life - CurrentShield)) HullRegenAmount = NPC.lifeMax - MaxShield - (NPC.life - CurrentShield);
                    NPC.life += HullRegenAmount;
                }
            }

            if (shipAI != ShipAI.Attack && HullRegen > 0)         //脱战非满血显示治疗特效
            {
                if (Main.rand.NextBool(40))
                {
                    if (NPC.life - CurrentShield < NPC.lifeMax - MaxShield)
                    {
                        Vector2 ShipPos = NPC.Center +
                            NPC.rotation.ToRotationVector2() * ShipLength * (Main.rand.NextFloat() - 0.5f) * 0.8f +
                            NPC.rotation.ToRotationVector2().RotatedBy(MathHelper.Pi / 2f) * ShipWidth * (Main.rand.NextFloat() - 0.5f) * 0.8f;
                        HealEffect.Summon(NPC.GetSource_FromAI(), ShipPos, 0.5f);
                    }
                }
            }
            if (CurrentShield > MaxShield) CurrentShield = MaxShield;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
        }

        public override bool CheckActive() => false;

        private void DrawDescAndHealthBar(SpriteBatch spriteBatch)
        {
            PlayerInput.SetZoom_Unscaled();
            PlayerInput.SetZoom_MouseInWorld();
            Vector2 mousePos = new(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y);
            if (Main.LocalPlayer.gravDir == -1f)
            {
                mousePos.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }
            PlayerInput.SetZoom_UI();

            Vector2 UnitX = NPC.rotation.ToRotationVector2();
            float tmp = 0;
            bool colliding = Collision.CheckAABBvLineCollision(mousePos, new Vector2(1, 1), NPC.Center - UnitX * ShipLength / 2f, NPC.Center + UnitX * ShipLength / 2f, ShipWidth, ref tmp);
            if (colliding || HurtTimer > 0)
            {
                DrawHPBar();
            }
            if (colliding)
            {
                if (!Main.mouseText && !Main.LocalPlayer.mouseInterface)
                {
                    Main.instance.MouseTextHackZoom(string.Format(Language.GetTextValue("Mods.StellarisShips.NPCExtraDesc"), ShipName, string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipGraph.GraphName, EverythingLibrary.Ships[ShipGraph.ShipType].GetLocalizedName()),
                        NPC.life - CurrentShield, NPC.lifeMax - MaxShield, CurrentShield, MaxShield), 0, Main.LocalPlayer.difficulty, null);
                    Main.mouseText = true;
                }
            }
            PlayerInput.SetZoom_Unscaled();
            PlayerInput.SetZoom_MouseInWorld();
        }


        private void DrawHPBar()
        {
            if (Main.HealthBarDrawSettings == 0)
            {
                return;
            }
            float scale = 1f;
            if (Main.HealthBarDrawSettings == 1)
            {
                float DrawPosY = NPC.Center.Y + ShipWidth / 2f + 10 + NPC.gfxOffY;
                if (Main.LocalPlayer.gravDir != 1)
                {
                    DrawPosY = NPC.Center.Y - ShipWidth / 2f - 10 + NPC.gfxOffY;
                    DrawPosY = Main.screenHeight - (DrawPosY - Main.screenPosition.Y) + Main.screenPosition.Y;
                }
                DrawHullBar(NPC.Center.X, DrawPosY, 1f, scale, false);
                DrawShieldBar(NPC.Center.X, DrawPosY, 1f, scale, false);
            }
            else if (Main.HealthBarDrawSettings == 2)
            {
                float DrawPosY = NPC.Center.Y - ShipWidth / 2f - 10 + NPC.gfxOffY;
                if (Main.LocalPlayer.gravDir != 1)
                {
                    DrawPosY = NPC.Center.Y + ShipWidth / 2f + 10 + NPC.gfxOffY;
                    DrawPosY = Main.screenHeight - (DrawPosY - Main.screenPosition.Y) + Main.screenPosition.Y;
                }
                DrawHullBar(NPC.Center.X, DrawPosY, 1f, scale, false);
                DrawShieldBar(NPC.Center.X, DrawPosY, 1f, scale, false);
            }
        }

        private void DrawHullBar(float X, float Y, float alpha, float scale = 1f, bool noFlip = false)
        {
            if (NPC.life <= 0) return;
            float HullPercent = MathHelper.Clamp((float)NPC.life / NPC.lifeMax, 0, 1f);
            int HullWidth = (int)(36f * HullPercent);
            HullPercent = MathHelper.Clamp((float)(NPC.life - CurrentShield) / (NPC.lifeMax - MaxShield), 0, 1f);
            float num3 = X - 18f * scale;
            float num4 = Y;
            if (Main.player[Main.myPlayer].gravDir == -1f && !noFlip)
            {
                num4 -= Main.screenPosition.Y;
                num4 = Main.screenPosition.Y + Main.screenHeight - num4;
            }
            float num5 = 0f;
            float num6 = 255f;
            HullPercent -= 0.1f;
            float num7;
            float num8;
            if ((double)HullPercent > 0.5f)
            {
                num7 = 255f;
                num8 = 255f * (1f - HullPercent) * 2f;
            }
            else
            {
                num7 = 255f * HullPercent * 2f;
                num8 = 255f;
            }
            float num9 = 0.95f;
            num8 = num8 * alpha * num9;
            num7 = num7 * alpha * num9;
            num6 = num6 * alpha * num9;
            if (num8 < 0f)
            {
                num8 = 0f;
            }
            if (num8 > 255f)
            {
                num8 = 255f;
            }
            if (num7 < 0f)
            {
                num7 = 0f;
            }
            if (num7 > 255f)
            {
                num7 = 255f;
            }
            if (num6 < 0f)
            {
                num6 = 0f;
            }
            if (num6 > 255f)
            {
                num6 = 255f;
            }
            Color color = new((int)(byte)num8, (int)(byte)num7, (int)(byte)num5, (int)(byte)num6);
            if (HullWidth < 3)
            {
                HullWidth = 3;
            }
            if (HullWidth < 34)
            {
                if (HullWidth < 36)
                {
                    Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)HullWidth * scale, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(2, 0, 2, TextureAssets.Hb2.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
                }
                if (HullWidth < 34)
                {
                    Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)(HullWidth + 2) * scale, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(HullWidth + 2, 0, 36 - HullWidth - 2, TextureAssets.Hb2.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
                }
                if (HullWidth > 2)
                {
                    Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, HullWidth - 2, TextureAssets.Hb1.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
                }
                Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X + (float)(HullWidth - 2) * scale, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(32, 0, 2, TextureAssets.Hb1.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
                return;
            }
            if (HullWidth < 36)
            {
                Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)HullWidth * scale, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(HullWidth, 0, 36 - HullWidth, TextureAssets.Hb2.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
            }
            Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X, num4 - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, HullWidth, TextureAssets.Hb1.Height())), color, 0f, new Vector2(0f, 0f), scale, 0, 0f);
        }

        private void DrawShieldBar(float X, float Y, float opalcity, float scale = 1f, bool noFlip = false)
        {
            if (CurrentShield <= 0) return;
            int Hull = NPC.life - CurrentShield;
            int MaxHull = NPC.lifeMax - MaxShield;
            if (Hull <= 0) return;
            float HullMaxHealth = (float)MaxHull / NPC.lifeMax;
            float HullPercent = MathHelper.Clamp((float)Hull / MaxHull, 0, 1f);
            float ShieldPercent = MathHelper.Clamp((float)CurrentShield / MaxShield, 0, 1f);

            int HullWidth = (int)(36f * HullPercent * HullMaxHealth);
            int ShieldWidth = (int)(36 * ShieldPercent * (1 - HullMaxHealth)) + 1;
            float Left = X - 18f * scale;
            float Top = Y;
            if (Main.player[Main.myPlayer].gravDir == -1f && !noFlip)
            {
                Top -= Main.screenPosition.Y;
                Top = Main.screenPosition.Y + Main.screenHeight - Top;
            }
            Color shieldColor = Color.Lerp(Color.White, Color.SkyBlue, ShieldPercent) * opalcity;

            Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(Left - Main.screenPosition.X + HullWidth * scale, Top - Main.screenPosition.Y), new Rectangle(HullWidth, 0, ShieldWidth, TextureAssets.Hb2.Height()), shieldColor, 0f, Vector2.Zero, scale, 0, 0f);

        }

        public override void ModifyTypeName(ref string typeName)
        {
            if (ShipGraph.ShipType != "")
            {
                typeName = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipGraph.GraphName, EverythingLibrary.Ships[ShipGraph.ShipType].GetLocalizedName());
            }
        }

        public override void OnKill()
        {
            FallenShip.BuildAFallenShip(NPC.GetSource_Death(), NPC.Center, NPC.rotation, ShipGraph);
            if (BattleStatSystem.BossBattleActive)
            {
                string name = string.Format(Language.GetTextValue("Mods.StellarisShips.UI.GraphNameUI"), ShipGraph.GraphName, EverythingLibrary.Ships[ShipGraph.ShipType].GetLocalizedName());
                if (!BattleStatSystem.LoseShip.TryAdd(name, 1))
                {
                    BattleStatSystem.LoseShip[name] += 1;
                }
            }
        }

        public override bool CheckDead()
        {
            if (Main.rand.NextFloat() <= EscapeChance)          //紧急脱离
            {
                NPC.life = 1;
                CurrentShield = 0;
                shipAI = ShipAI.Missing;
                MissingTimer = CalcMissingTime();
                NPC.dontTakeDamage = true;
                NPC.dontTakeDamageFromHostiles = true;
                float scale = ShipLength / 70f;
                FTLLight2.Summon(NPC.GetSource_FromAI(), NPC.Center, scale);
                SomeUtils.PlaySound(SoundPath.Other + "FTL", FTLTargetPos);
                return false;
            }
            return true;
        }
    }
}