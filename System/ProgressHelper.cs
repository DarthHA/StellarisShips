using StellarisShips.Content.Items;
using StellarisShips.Static;
using StellarisShips.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.System
{
    public class ProgressHelper : ModSystem
    {
        public static bool FirstContract = true;
        public static int CurrentProgress = 1;
        public static bool HasNotification = false;
        public static int DiscoveredMR = 0;

        public static int GetMaxCommandPoint()
        {
            if (CurrentProgress >= 7) return 80;
            if (CurrentProgress >= 4) return 40;
            return 20;
        }

        public static int GetMaxMinorArtifact()
        {
            if (CurrentProgress < 9) return 0;
            return 3000;
        }

        public static int GetCurrentProgress()
        {
            if (NPC.downedMoonlord) return 9;
            if (NPC.downedGolemBoss) return 8;
            if (NPC.downedPlantBoss) return 7;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) return 6;
            if (NPC.downedMechBossAny) return 5;
            if (Main.hardMode) return 4;
            if (NPC.downedBoss3) return 3;
            if (NPC.downedBoss2) return 2;
            return 1;
        }

        public static int GetOpinion()
        {
            return GetCurrentProgress() * 10;
        }

        public override void PostUpdateNPCs()
        {
            if (CurrentProgress != GetCurrentProgress())
            {
                if (Main.LocalPlayer.HasItem(ModContent.ItemType<ConnecterItem>()))
                {
                    if (!HasNotification)
                    {
                        SomeUtils.PlaySound(SoundPath.UI + "Notification");
                        HasNotification = true;

                        if (GetCurrentProgress() == 2 || GetCurrentProgress() == 5 || GetCurrentProgress() == 8)
                        {
                            InGameNotificationsTracker.AddNotification(new NewShipNotification());
                        }
                        else if (GetCurrentProgress() == 4 || GetCurrentProgress() == 7)
                        {
                            InGameNotificationsTracker.AddNotification(new CPExpandNotification());
                        }
                        InGameNotificationsTracker.AddNotification(new NewTechNotification());
                    }
                }
            }
        }

        public override void ClearWorld()
        {
            FirstContract = true;
            CurrentProgress = 1;
            HasNotification = false;
            DiscoveredMR = 0;
        }
    }
}
