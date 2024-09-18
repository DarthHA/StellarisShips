
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
        }

        public override void Unload()
        {
            _ShipDesignUI?.Deactivate();
            _ShipDesignUI = null;
            _ShipDesignUIInterface = null;

            _ShipBuildUI?.Deactivate();
            _ShipBuildUI = null;
            _ShipBuildUIInterface = null;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int DrawingUIIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (DrawingUIIndex != -1)
            {
                layers.Insert(DrawingUIIndex, new LegacyGameInterfaceLayer(
                    "StellarisShipsMod: ShipDesignUI",
                    delegate
                    {
                        _ShipDesignUIInterface.Draw(Main.spriteBatch, new GameTime());
                        _ShipBuildUIInterface.Draw(Main.spriteBatch, new GameTime());
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
                ShipDesignVisible = false;
            }

            if (!Main.gameMenu && !Main.LocalPlayer.IsDead() && ShipBuildVisible)
            {
                _ShipBuildUIInterface?.Update(gameTime);
            }
            else
            {
                ShipBuildVisible = false;
            }
        }

        private static bool LastLeftUnPressed = false;
        public static bool LeftClicked = false;
        private static bool LastRightUnPressed = false;
        public static bool RightClicked = false;

        public override void PreSaveAndQuit()
        {
            ShipDesignUI.AllClear(true);
            ShipBuildUI.AllClear(true);
            ShipDesignVisible = false;
            ShipBuildVisible = false;
        }

        public static void ResetClick()
        {
            LastLeftUnPressed = false;
            LastRightUnPressed = false;
            LeftClicked = false;
            RightClicked = false;
        }
    }

}