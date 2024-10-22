
using Microsoft.Xna.Framework;
using StellarisShips.Static;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace StellarisShips.UI
{
    public class UIManager : ModSystem
    {
        public static bool ShipDesignVisible = false;
        public static UserInterface _ShipDesignUIInterface;
        static ShipDesignUI _ShipDesignUI;

        public static bool ShipBuildVisible = false;
        public static UserInterface _ShipBuildUIInterface;
        static ShipBuildUI _ShipBuildUI;

        public static bool ShroudVisible = false;
        public static UserInterface _ShroudUIInterface;
        static ShroudUI _ShroudUI;

        public static bool LeaderUIVisible = false;
        public static UserInterface _LeaderUIInterface;
        static LeaderUI _LeaderUI;

        public override void Load()
        {
            _ShipDesignUI = new ShipDesignUI();
            _ShipDesignUI.Activate();
            _ShipDesignUIInterface = new UserInterface();
            _ShipDesignUIInterface.SetState(_ShipDesignUI);

            _ShipBuildUI = new ShipBuildUI();
            _ShipBuildUI.Activate();
            _ShipBuildUIInterface = new UserInterface();
            _ShipBuildUIInterface.SetState(_ShipBuildUI);

            _ShroudUI = new ShroudUI();
            _ShroudUI.Activate();
            _ShroudUIInterface = new UserInterface();
            _ShroudUIInterface.SetState(_ShroudUI);

            _LeaderUI = new LeaderUI();
            _LeaderUI.Activate();
            _LeaderUIInterface = new UserInterface();
            _LeaderUIInterface.SetState(_LeaderUI);
        }

        public override void Unload()
        {
            _ShipDesignUI?.Deactivate();
            _ShipDesignUI = null;
            _ShipDesignUIInterface = null;

            _ShipBuildUI?.Deactivate();
            _ShipBuildUI = null;
            _ShipBuildUIInterface = null;

            _ShroudUI?.Deactivate();
            _ShroudUI = null;
            _ShroudUIInterface = null;

            _LeaderUI?.Deactivate();
            _LeaderUI = null;
            _LeaderUIInterface = null;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int DrawingUIIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));
            if (DrawingUIIndex != -1)
            {
                layers.Insert(DrawingUIIndex + 1, new LegacyGameInterfaceLayer(
                    "StellarisShipsMod: SomeUI",
                    delegate
                    {
                        _ShipDesignUIInterface.Draw(Main.spriteBatch, new GameTime());
                        _ShipBuildUIInterface.Draw(Main.spriteBatch, new GameTime());
                        _ShroudUIInterface.Draw(Main.spriteBatch, new GameTime());
                        _LeaderUIInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            LeftClicked = false;
            if (Main.mouseLeftRelease)
            {
                LastLeftUnPressed = true;
            }
            else
            {
                if (LastLeftUnPressed)
                {
                    LeftClicked = true;
                }
                LastLeftUnPressed = false;
            }

            RightClicked = false;
            if (Main.mouseRightRelease)
            {
                LastRightUnPressed = true;
            }
            else
            {
                if (LastRightUnPressed)
                {
                    RightClicked = true;
                }
                LastRightUnPressed = false;
            }

            if (!Main.gameMenu && !Main.LocalPlayer.IsDead() && ShipDesignVisible)
            {
                _ShipDesignUIInterface?.Update(gameTime);
            }
            else
            {
                ShipDesignUI.Close();
            }

            if (!Main.gameMenu && !Main.LocalPlayer.IsDead() && ShipBuildVisible)
            {
                _ShipBuildUIInterface?.Update(gameTime);
            }
            else
            {
                ShipBuildUI.Close();
            }

            if (!Main.gameMenu && !Main.LocalPlayer.IsDead() && ShroudVisible)
            {
                _ShroudUIInterface?.Update(gameTime);
            }
            else
            {
                ShroudUI.Close();
            }

            if (!Main.gameMenu && !Main.LocalPlayer.IsDead() && LeaderUIVisible)
            {
                _LeaderUIInterface?.Update(gameTime);
            }
            else
            {
                LeaderUI.Close();
            }
        }

        private static bool LastLeftUnPressed = false;
        public static bool LeftClicked = false;
        private static bool LastRightUnPressed = false;
        public static bool RightClicked = false;

        public override void PreSaveAndQuit()
        {
            ShipDesignUI.Close();
            ShipBuildUI.Close();
            ShroudUI.Close();
            LeaderUI.Close();
        }

        public static void ResetClick()
        {
            LastLeftUnPressed = false;
            LastRightUnPressed = false;
            LeftClicked = false;
            RightClicked = false;
        }


        public static bool AnyUIVisible()
        {
            return ShipDesignVisible || ShipBuildVisible || ShroudVisible || LeaderUIVisible;
        }
    }

}