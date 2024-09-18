using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StellarisShips.Content.NPCs
{
    public class FallenShip : ModNPC
    { //舰船图纸
        public ShipGraph ShipGraph = new();

        #region 以下参数用于绘制，可以减少调用量
        public Texture2D ShipTexture;
        public float shipScale;
        public float weaponScale;
        public int TailDrawOffset;
        public string TailType;
        #endregion

        public List<BaseWeaponUnit> weapons = new();

        public override void SetStaticDefaults()
        {
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

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
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.chaseable = false;
            NPC.canDisplayBuffs = false;
            NPC.CanBeReplacedByOtherNPCs = false;
            NPC.dontTakeDamage = true;
            NPC.dontTakeDamageFromHostiles = true;
        }

        List<Vector2> SmokePos = new();

        public override void AI()
        {
            if (ShipGraph.ShipType == "")
            {
                NPC.active = false;
                return;
            }
            NPC.ai[0]++;
            NPC.velocity = new Vector2(0, 0.5f);
            if (Main.rand.NextBool(15))
            {
                Vector2 UnitX = NPC.rotation.ToRotationVector2();
                Vector2 UnitY = UnitX.RotatedBy(MathHelper.Pi / 2f);
                Vector2 ExplosionPos = NPC.Center + NPC.width * (Main.rand.NextFloat() - 0.5f) * UnitX + NPC.height * (Main.rand.NextFloat() - 0.5f) * UnitY;
                float scale = NPC.width / 36f;
                float ExplosionScale = scale * 0.2f * (Main.rand.NextFloat() + 0.5f);
                Color color = Utils.SelectRandom(Main.rand, Color.Orange, Color.Cyan, Color.SeaGreen, Color.IndianRed, Color.White);

                ShipExplosion.Summon(NPC, ExplosionPos, color, ExplosionScale);
                SomeUtils.PlaySoundRandom(SoundPath.Explosion, 8, ExplosionPos);
            }

            foreach (Vector2 smoke in SmokePos)
            {
                if (Main.rand.NextBool(2))
                {
                    int num110 = Dust.NewDust(NPC.Center + smoke, 0, 0, 303, 0f, 0f, 0, default, 1f);    //73?
                    Main.dust[num110].scale = 1f + Main.rand.NextFloat() * 0.8f;
                    Main.dust[num110].noGravity = true;
                }
            }

            if (NPC.ai[0] > 300)
            {
                float scale = NPC.width / 36f * 1.2f;
                Color color = Utils.SelectRandom(Main.rand, Color.Orange, Color.Cyan, Color.SeaGreen, Color.IndianRed, Color.White);
                ShipExplosion.Summon(NPC, NPC.Center, color, scale);
                SomeUtils.PlaySoundRandom(SoundPath.Other + "UnitLost", 1, NPC.Center);
                NPC.StrikeInstantKill();
            }
        }

        public static int BuildAFallenShip(IEntitySource source, Vector2 Pos, float rotation, ShipGraph graph)
        {
            if (graph.ShipType == "") return -1;
            int npctmp = NPC.NewNPC(source, (int)Pos.X, (int)Pos.Y, ModContent.NPCType<FallenShip>());
            if (npctmp >= 0 && npctmp < 200)
            {
                NPC npc = Main.npc[npctmp];
                FallenShip shipNPC = Main.npc[npctmp].ModNPC as FallenShip;
                shipNPC.ShipGraph = graph.Copy();
                //载入武器数据
                List<float> weaponRanges = new();
                foreach (SectionForSave section in shipNPC.ShipGraph.Parts)
                {
                    for (int i = 0; i < section.WeaponSlot.Count; i++)
                    {
                        if (section.WeaponSlot[i] != "")
                        {
                            BaseWeaponUnit instance = BaseWeaponUnit.NewWeaponUnit(EverythingLibrary.Components[section.WeaponSlot[i]].TypeName);
                            instance.RelativePos = EverythingLibrary.Sections[section.InternalName].WeaponPos[i];
                            instance.ComponentName = section.WeaponSlot[i];
                            EverythingLibrary.Components[section.WeaponSlot[i]].ModifyWeaponUnit(instance, npc);
                            shipNPC.weapons.Add(instance);
                        }
                    }
                }

                //部分参数录入
                npc.rotation = rotation;
                npc.width = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Length;
                npc.height = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].Width;
                npc.Center = Pos;


                float area = npc.width * npc.height / 2000;
                for (int i = 0; i < area; i++)
                {
                    Vector2 UnitX = npc.rotation.ToRotationVector2();
                    Vector2 UnitY = UnitX.RotatedBy(MathHelper.Pi / 2f);
                    Vector2 smokePos = npc.width * (Main.rand.NextFloat() - 0.5f) * UnitX * 0.8f + npc.height * (Main.rand.NextFloat() - 0.5f) * UnitY * 0.8f;
                    shipNPC.SmokePos.Add(smokePos);
                }

                //绘制相关捷径录入
                List<string> sectionList = new();
                foreach (SectionForSave str in shipNPC.ShipGraph.Parts)
                {
                    sectionList.Add(str.InternalName);
                }
                shipNPC.ShipTexture = LibraryHelpers.GetShipTexture(sectionList);
                shipNPC.shipScale = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].ShipScale;
                shipNPC.weaponScale = EverythingLibrary.Ships[shipNPC.ShipGraph.ShipType].WeaponScale;
                shipNPC.TailDrawOffset = EverythingLibrary.Sections[shipNPC.ShipGraph.Parts[shipNPC.ShipGraph.Parts.Count - 1].InternalName].TailDrawOffSet;
                shipNPC.TailType = shipNPC.ShipGraph.Parts[shipNPC.ShipGraph.Parts.Count - 1].InternalName;
            }
            return npctmp;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (ShipGraph.ShipType == "")
            {
                return false;
            }

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
            return false;
        }

        private void DrawWeapons(SpriteBatch spriteBatch, Vector2 screenPos)
        {
            foreach (BaseWeaponUnit weapon in weapons)
            {
                if (weapon.CannonName != "")
                {
                    Texture2D texWeapon = ModContent.Request<Texture2D>("StellarisShips/Images/Ships/" + weapon.CannonName + "_Gun", AssetRequestMode.ImmediateLoad).Value;
                    Vector2 DrawPos = NPC.Center + NPC.rotation.ToRotationVector2() * weapon.RelativePos.Y * shipScale + (NPC.rotation - MathHelper.Pi / 2).ToRotationVector2() * weapon.RelativePos.X * shipScale - NPC.rotation.ToRotationVector2() * ShipTexture.Width * 0.75f * shipScale * 0.5f;
                    float Facing = weapon.Rotation;
                    spriteBatch.Draw(texWeapon, DrawPos - screenPos, null, Color.White, Facing, texWeapon.Size() / 2f, shipScale * weaponScale, SpriteEffects.FlipHorizontally, 0);
                }
            }
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool CheckActive() => false;

        public override void ModifyTypeName(ref string typeName)
        {
            if (ShipGraph.ShipType != "")
            {
                typeName = ShipGraph.GraphName;
            }
        }
    }
}
