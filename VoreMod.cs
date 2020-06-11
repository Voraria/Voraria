using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace VoreMod
{
    public class VoreMod : Mod
    {
        public VoreUI voreUI;

        GameTime lastTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                voreUI = new VoreUI();
                voreUI.Activate();
                voreUI.Show();
            }
        }

        public override void Unload()
        {
            voreUI = null;
            VorePlayer.BellyLayer = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            lastTime = gameTime;
            if (voreUI != null) voreUI.UpdateUI(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (voreUI != null) voreUI.ApplyToInterfaceLayers(layers, lastTime);
        }
    }
}
