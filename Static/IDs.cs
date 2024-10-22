
using System.Collections.Generic;

namespace StellarisShips.Static
{
    public static class ShipIDs
    {
        public const string Corvette = "Corvette";
        public const string Destroyer = "Destroyer";
        public const string Cruiser = "Cruiser";
        public const string Battleship = "Battleship";
        public const string Titan = "Titan";
    }

    public static class SectionIDs
    {
        //护卫舰
        public const string Corvette_Interceptor_Core = "Interceptor11";
        public const string Corvette_PicketShip_Core = "PicketShip11";
        public const string Corvette_MissileBoat_Core = "MissileBoat11";
        //驱逐舰
        public const string Destroyer_Artillery_Bow = "Artillery21";
        public const string Destroyer_Gunship_Bow = "Gunship21";
        public const string Destroyer_PicketShip_Bow = "PicketShip21";

        public const string Destroyer_Gunship_Stern = "Gunship22";
        public const string Destroyer_Interceptor_Stern = "Interceptor22";
        public const string Destroyer_PicketShip_Stern = "PicketShip22";
        //巡洋舰
        public const string Cruiser_Artillery_Bow = "Artillery31";
        public const string Cruiser_Broadside_Bow = "Broadside31";
        public const string Cruiser_Torpedo_Bow = "Torpedo31";

        public const string Cruiser_Artillery_Core = "Artillery32";
        public const string Cruiser_Hangar_Core = "Hangar32";
        public const string Cruiser_Broadside_Core = "Broadside32";
        public const string Cruiser_Torpedo_Core = "Torpedo32";

        public const string Cruiser_Broadside_Stern = "Broadside33";
        public const string Cruiser_Gunship_Stern = "Gunship33";
        //战列舰
        public const string Battleship_Artillery_Bow = "Artillery41";
        public const string Battleship_Broadside_Bow = "Broadside41";
        public const string Battleship_Hangar_Bow = "Hangar41";
        public const string Battleship_SpinalMount_Bow = "SpinalMount41";

        public const string Battleship_Artillery_Core = "Artillery42";
        public const string Battleship_Broadside_Core = "Broadside42";
        public const string Battleship_Carrier_Core = "Carrier42";
        public const string Battleship_Hangar_Core = "Hangar42";

        public const string Battleship_Artillery_Stern = "Artillery43";
        public const string Battleship_Broadside_Stern = "Broadside43";

        //泰坦
        public const string Titan_Bow = "Titan51";
        public const string Titan_Core = "Titan52";
        public const string Titan_Stern = "Titan53";
    }

    public static class ComponentTypes
    {
        //武器槽
        public const string Weapon_S = "S";
        public const string Weapon_M = "M";
        public const string Weapon_L = "L";
        public const string Weapon_X = "X";
        public const string Weapon_P = "P";
        public const string Weapon_G = "G";
        public const string Weapon_H = "H";
        public const string Weapon_T = "T";
        //通用槽(后面就是乱写了)
        public const string Defense_S = "B";
        public const string Defense_M = "C";
        public const string Defense_L = "D";
        public const string Accessory = "A";
        //核心槽
        public const string Reactor = "E";
        public const string Thruster = "F";
        public const string Sensor = "I";
        public const string Computer = "J";
        public const string FTLDrive = "K";
        public const string Aura = "O";
    }

    public static class BonusID
    {
        //槽位武器伤害
        public const string WeaponDamage_S = "WeaponDamage_S";
        public const string WeaponDamage_M = "WeaponDamage_M";
        public const string WeaponDamage_L = "WeaponDamage_L";
        public const string WeaponDamage_X = "WeaponDamage_X";
        public const string WeaponDamage_P = "WeaponDamage_P";
        public const string WeaponDamage_G = "WeaponDamage_G";
        public const string WeaponDamage_H = "WeaponDamage_H";
        public const string WeaponDamage_T = "WeaponDamage_T";

        //槽位武器攻击间隔
        public const string WeaponAttackCD_S = "WeaponAttackCD_S";
        public const string WeaponAttackCD_M = "WeaponAttackCD_M";
        public const string WeaponAttackCD_L = "WeaponAttackCD_L";
        public const string WeaponAttackCD_X = "WeaponAttackCD_X";
        public const string WeaponAttackCD_G = "WeaponAttackCD_G";
        public const string WeaponAttackCD_T = "WeaponAttackCD_T";

        //槽位武器暴击
        public const string WeaponCrit_S = "WeaponCrit_S";
        public const string WeaponCrit_M = "WeaponCrit_M";
        public const string WeaponCrit_L = "WeaponCrit_L";
        public const string WeaponCrit_X = "WeaponCrit_X";
        public const string WeaponCrit_P = "WeaponCrit_P";
        public const string WeaponCrit_G = "WeaponCrit_G";
        public const string WeaponCrit_H = "WeaponCrit_H";
        public const string WeaponCrit_T = "WeaponCrit_T";

        //槽位武器射程
        public const string WeaponRange_S = "WeaponRange_S";
        public const string WeaponRange_M = "WeaponRange_M";
        public const string WeaponRange_L = "WeaponRange_L";
        public const string WeaponRange_X = "WeaponRange_X";
        public const string WeaponRange_P = "WeaponRange_P";
        public const string WeaponRange_G = "WeaponRange_G";
        public const string WeaponRange_T = "WeaponRange_T";

        //全部武器数据
        public const string AllWeaponDamage = "AllWeaponDamage";
        public const string AllWeaponAttackCD = "AllWeaponAttackCD";
        public const string AllWeaponCrit = "AllWeaponCrit";
        public const string AllWeaponRange = "AllWeaponRange";

        //舰船数据
        public const string ExtraHull = "ExtraHull";
        public const string HullMult = "HullMult";
        public const string Defense = "Defense";
        public const string Shield = "Shield";
        public const string ShieldMult = "ShieldMult";
        public const string HullRegenPercent = "HullRegenPercent";
        public const string ShieldRegen = "ShieldRegen";
        public const string ShieldRegenPercent = "ShieldRegenPercent";
        public const string BaseSpeed = "BaseSpeed";
        public const string SpeedMult = "SpeedMult";
        public const string SpeedMultFinal = "SpeedMultFinal";
        public const string BaseEvasion = "BaseEvasion";
        public const string EscapeChance = "EscapeChance";
        public const string FTLMaxCooldown = "FTLMaxCooldown";
        public const string DetectRange = "DetectRange";
        public const string MaxImmuneTime = "MaxImmuneTime";
        public const string ExtraStriker = "ExtraStriker";
        public const string Aggro = "Aggro";
        public const string DamageToNonBoss = "DamageToNonBoss";
        public const string DamageToBoss = "DamageToBoss";
        public const string VampireHeal = "VampireHeal";
        public const string DamageToOldOnes = "DamageToOldOnes";
        public const string DamageInMushroom = "DamageInMushroom";

        //这个取最大值
        public const string ShieldDRLevel = "ShieldDRLevel";
        public const string FTLLevel = "FTLLevel";

        //这个取反向乘算
        public const string Evasion = "Evasion";
        public const string ShieldDR = "ShieldDR";
    }

    public static class ComputerID
    {
        public const string Artillery = "ComputerArtillery";
        public const string Carrier = "ComputerCarrier";
        public const string Line = "ComputerLine";
        public const string Picket = "ComputerPicket";
        public const string Swarm = "ComputerSwarm";
        public const string Bomber = "ComputerBomber";
    }

    public static class SoundPath
    {
        public const string Fire = "StellarisShips/Sounds/Fire/";
        public const string Explosion = "StellarisShips/Sounds/Explosion/";
        public const string Hit = "StellarisShips/Sounds/Hit/";
        public const string Shield = "StellarisShips/Sounds/Shield/";
        public const string UI = "StellarisShips/Sounds/UI/";
        public const string Other = "StellarisShips/Sounds/Other/";
    }

    public static class ModifierID
    {
        public const string QuantumDestabilizer = "QuantumDestabilizer";
        public const string ShieldDampener = "ShieldDampener";
        public const string SubspaceSnare = "SubspaceSnare";
        public const string InspiringPresence = "InspiringPresence";
        public const string AncientTargetScrambler = "AncientTargetScrambler";
        public const string NanobotCloud = "NanobotCloud";
        public const string TargetingGrid = "TargetingGrid";

        public const string ShroudASPDUp = "ShroudASPDUp";
        public const string ShroudAtkUp = "ShroudAtkUp";
        public const string ShroudEvasionUp = "ShroudEvasionUp";
        public const string ShroudRegenUp = "ShroudRegenUp";
        public const string ShroudShieldUp = "ShroudShieldUp";
        public const string ShroudSpeedUp = "ShroudSpeedUp";

        //领袖特质
        //正面
        public const string Aggressiveness = "Aggressiveness";      //和Prudence冲突
        public const string Aggressiveness2 = "Aggressiveness2";
        public const string Prudence = "Prudence";            //和Aggressiveness冲突
        public const string Prudence2 = "Prudence2";
        public const string Engineer = "Engineer";
        public const string Engineer2 = "Engineer2";
        public const string Trickster = "Trickster";           //和Unyielding,Reckless冲突
        public const string Trickster2 = "Trickster2";
        public const string Unyielding = "Unyielding";          //和Trickster,Negligence,Anxiety冲突
        public const string Unyielding2 = "Unyielding2";
        public const string GaleSpeed = "GaleSpeed";       //和Lethargic冲突
        public const string GaleSpeed2 = "GaleSpeed2";
        //负面
        public const string Negligence = "Negligence";          //和Unyielding冲突
        public const string Negligence2 = "Negligence2";
        public const string Reckless = "Reckless";           //和Trickster冲突
        public const string Reckless2 = "Reckless2";
        public const string Anxiety = "Anxiety";           //和Unyielding冲突
        public const string Anxiety2 = "Anxiety2";
        public const string Lethargic = "Lethargic";        //和GaleSpeed冲突
        public const string Lethargic2 = "Lethargic2";
        public const string Disgusting = "Disgusting";
        public const string Disgusting2 = "Disgusting2";
        //老练
        public const string Artillerist = "Artillerist";
        public const string Artillerist2 = "Artillerist2";
        public const string JuryRigger = "JuryRigger";           //和JuryRigger冲突
        public const string JuryRigger2 = "JuryRigger2";
        public const string Wrecker = "Wrecker";                 //和Wrecker冲突
        public const string Wrecker2 = "Wrecker2";
        public const string Demolisher = "Demolisher";
        public const string Demolisher2 = "Demolisher2";
        //专精特质
        public const string GunshipFocus = "GunshipFocus";
        public const string GunshipFocus2 = "GunshipFocus2";
        public const string ArtilleryFocus = "ArtilleryFocus";
        public const string ArtilleryFocus2 = "ArtilleryFocus2";
        public const string CarrierFocus = "CarrierFocus";
        public const string CarrierFocus2 = "CarrierFocus2";
        public const string GuidanceSystemFocus = "GuidanceSystemFocus";
        public const string GuidanceSystemFocus2 = "GuidanceSystemFocus2";
        //特殊特质(0级)
        public const string GuideInsight = "GuideInsight";
        public const string DisgustingAngler = "DisgustingAngler";
        public const string EvilCleaner = "EvilCleaner";
        public const string TavernKeeperInsight = "TavernKeeperInsight";
        public const string SkeletronCurse = "SkeletronCurse";
        public const string HellHeart = "HellHeart";
        public const string Cyborg = "Cyborg";
        public const string GreatPrincess = "GreatPrincess";
        public const string Slimy = "Slimy";
        public const string Cute = "Cute";
        public const string MagicShield = "MagicShield";
        public const string MushroomMind = "MushroomMind";

        public readonly static List<string> Common = new() { Aggressiveness, Aggressiveness2, Prudence, Prudence2, Engineer, Engineer2, Trickster, Trickster2, Unyielding, Unyielding2 };
        public readonly static List<string> Negative = new() { Negligence, Negligence2, Reckless, Reckless2, Anxiety, Anxiety2, Lethargic, Lethargic2, Disgusting, Disgusting2 };
        public readonly static List<string> Veteran = new() 
        { Artillerist, Artillerist2, JuryRigger, JuryRigger2, Wrecker, Wrecker2, Demolisher, Demolisher2, GaleSpeed, GaleSpeed2,
            GunshipFocus, ArtilleryFocus, CarrierFocus, GuidanceSystemFocus, GunshipFocus2, ArtilleryFocus2, CarrierFocus2, GuidanceSystemFocus2 };
        public readonly static List<string> Special = new() { GuideInsight, DisgustingAngler, EvilCleaner, TavernKeeperInsight, SkeletronCurse, HellHeart, Cyborg, GreatPrincess, Slimy, Cute, MagicShield, MushroomMind };
    }
}