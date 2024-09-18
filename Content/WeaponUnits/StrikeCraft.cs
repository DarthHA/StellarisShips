using Microsoft.Xna.Framework;
using StellarisShips.Content.Components.Weapons;
using StellarisShips.Content.NPCs;
using StellarisShips.Content.Projectiles;
using StellarisShips.Static;
using StellarisShips.System;
using StellarisShips.System.BaseType;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StellarisShips.Content.WeaponUnits
{
    public class StrikeCraftUnit : BaseWeaponUnit
    {
        public override string InternalName => "StrikeCraft";

        public List<long> UUIDs = new();

        public override void Update(NPC ship)
        {
            ShipNPC shipNPC = ship.GetShipNPC();
            if (shipNPC.CurrentTarget == -1)
            {
                CurrentCooldown = 60;
            }
            else                   //½øÕ½×´Ì¬
            {
                if (CurrentCooldown > 0) CurrentCooldown--;
                if (CurrentCooldown <= 0)
                {
                    int MaxStriker = 4 + ship.GetShipNPC().ExtraStriker;
                    int MinDmg = (int)((EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).MinDamage * DamageBonus / 4f);
                    int MaxDmg = (int)((EverythingLibrary.Components[ComponentName] as BaseWeaponComponent).MaxDamage * DamageBonus / 4f);
                    float Speed = 15;
                    switch (Level)
                    {
                        case 1:
                            Speed = 20f;
                            break;
                        case 2:
                            Speed = 22.5f;
                            break;
                        case 3:
                            Speed = 25f;
                            break;
                    }
                    List<long> NewUUIDs = new();
                    foreach (Projectile strikers in Main.ActiveProjectiles)
                    {
                        if (strikers.type == ModContent.ProjectileType<StrikeCraftProj>())
                        {
                            if (UUIDs.Contains((strikers.ModProjectile as StrikeCraftProj).UUID))
                            {
                                NewUUIDs.Add((strikers.ModProjectile as StrikeCraftProj).UUID);
                            }
                        }
                    }
                    UUIDs.Clear();
                    foreach (long uuid in NewUUIDs)
                    {
                        UUIDs.Add(uuid);
                    }

                    if (UUIDs.Count < MaxStriker)
                    {
                        Vector2 ShootVel;
                        int dir = Math.Sign(RelativePos.X);
                        if (dir == 0) ShootVel = ship.rotation.ToRotationVector2() * (Main.rand.Next(7) + 3);
                        else ShootVel = ship.rotation.ToRotationVector2() * (Main.rand.Next(7) + 3) + (ship.rotation - dir * MathHelper.Pi / 2f).ToRotationVector2() * (Main.rand.Next(7) + 3);
                        long uuid = StrikeCraftProj.Summon(ship, shipNPC.GetPosOnShip(RelativePos), RelativePos, ShootVel, MaxDmg, MinDmg, Crit, Speed, 300, Level);
                        if (uuid != -1) UUIDs.Add(uuid);
                    }

                    CurrentCooldown = 60;
                }
            }
        }
    }

}