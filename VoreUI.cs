using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace VoreMod
{
    public class VoreUI : UIState
    {
        const float ROW_SPACING = 5f;

        public UserInterface ui = new UserInterface();

        UIPanel container;
        List<UIText> textRows = new List<UIText>();
        List<UIProgressBar> progressRows = new List<UIProgressBar>();

        UIProgressBar playerProgress;

        public override void OnInitialize()
        {
            container = new UIPanel();
            container.Left.Set(20f, 0f);
            container.Top.Set(120f, 0f);
            Append(container);
            playerProgress = new UIProgressBar();
            playerProgress.Left.Set(0f, 0f);
            playerProgress.Top.Set(-20f, 1f);
            playerProgress.Width.Set(0f, 1f);
            playerProgress.Height.Set(15f, 0f);
            Append(playerProgress);
        }

        public override void Update(GameTime gameTime)
        {
            container.Top.Set(Main.playerInventory ? 256f : 120f, 0f);
            // 425f if a chest/container is also open

            VoreEntity entity = Main.LocalPlayer.GetEntity();
            var preys = entity.GetAllPrey(VoreConfig.Instance.DebugInfo);
            int count = preys.Count;

            float playerProgressValue = Main.LocalPlayer.GetEntity().GetDigestionRatio();

            if (playerProgressValue > 0f && playerProgress.Parent == null) Append(playerProgress);
            else if (playerProgressValue == 0f && playerProgress.Parent == this) RemoveChild(playerProgress);

            if (count > 0 && container.Parent == null) Append(container);
            else if (count == 0 && container.Parent == this) RemoveChild(container);

            while (textRows.Count > count)
            {
                UIText text = textRows[textRows.Count - 1];
                textRows.Remove(text);
                text.Remove();
            }
            while (textRows.Count < count)
            {
                UIText text = new UIText("");
                text.Left.Set(0f, 0f);
                textRows.Add(text);
                container.Append(text);
            }
            while (progressRows.Count > count)
            {
                UIProgressBar progressBar = progressRows[progressRows.Count - 1];
                progressRows.Remove(progressBar);
                progressBar.Remove();
            }
            while (progressRows.Count < count)
            {
                UIProgressBar progressBar = new UIProgressBar();
                progressBar.Left.Set(-100f, 1f);
                progressBar.Width.Set(100f, 0f);
                progressBar.Height.Set(20f, 0f);
                progressRows.Add(progressBar);
                container.Append(progressBar);
            }
            float w = 0f;
            float y = 0f;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i > 0) y += ROW_SPACING;
                    textRows[i].SetText(preys[i].GetName() + " (" + preys[i].GetLife() + "/" + preys[i].GetLifeMax() + ")");
                    textRows[i].Top.Set(y, 0f);
                    textRows[i].Recalculate();
                    progressRows[i].SetProgress(preys[i].GetDigestionRatio());
                    progressRows[i].Top.Set(y, 0f);
                    w = MathHelper.Max(w, textRows[i].MinWidth.Pixels);
                    y += textRows[i].MinHeight.Pixels;
                }
            }
            container.Width.Set(w + container.PaddingLeft + container.PaddingRight + 105f, 0f);
            container.Height.Set(y + container.PaddingTop + container.PaddingBottom, 0f);

            playerProgress.SetProgress(playerProgressValue);
        }

        public void UpdateUI(GameTime gameTime)
        {
            if (ui.CurrentState != null) ui.Update(gameTime);
        }

        public void Show()
        {
            ui.SetState(this);
        }

        public void Hide()
        {
            ui.SetState(null);
        }

        public void ApplyToInterfaceLayers(List<GameInterfaceLayer> layers, GameTime lastTime)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Fancy UI"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(nameof(VoreMod) + ":" + nameof(VoreUI), delegate
                {
                    if (lastTime != null && ui.CurrentState != null)
                    {
                        ui.Draw(Main.spriteBatch, lastTime);
                    }
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}