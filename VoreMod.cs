using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using VoreMod.NPCs;
using Terraria.ID;

namespace VoreMod
{
    public class VoreMod : Mod
    {
        public static VoreMod Instance => (VoreMod)ModLoader.GetMod(nameof(VoreMod));

        public VoreUI voreUI;

        public List<VorePlugin> plugins = new List<VorePlugin>();

        GameTime lastTime;

        bool pluginsLoaded = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                voreUI = new VoreUI();
                voreUI.Activate();
                voreUI.Show();
            }

            if (!pluginsLoaded)
            {
                RegisterPlugin(new Plugins.TerrariaPlugin());
                RegisterPlugin(new Plugins.VoreModPlugin());
                RegisterPlugin(new Plugins.CalamityModPlugin());
                pluginsLoaded = true;
            }
        }

        public override void Unload()
        {
            voreUI = null;
            VorePlayer.BellyLayer = null;
            foreach (VorePlugin plugin in plugins) plugin.Unregister();
            plugins.Clear();
            pluginsLoaded = false;
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

        public static void RegisterPlugin(VorePlugin plugin)
        {
            Instance.plugins.Add(plugin);
            plugin.Register();
        }

        public static void UnregisterPlugin(VorePlugin plugin)
        {
            plugin.Unregister();
            Instance.plugins.Remove(plugin);
        }

        public static IEnumerable<VorePlugin> GetValidPlugins()
        {
            foreach (VorePlugin plugin in Instance.plugins)
            {
                if (plugin.IsValid()) yield return plugin;
            }
        }

        public static EntityTags? GetPluginTags(NPC npc)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                EntityTags? tags = plugin.GetTags(npc);
                if (tags != null) return tags;
            }
            return null;
        }

        public static CharmEffects? GetPluginCharmEffects(NPC npc)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                CharmEffects? charmEffects = plugin.GetCharmEffects(npc);
                if (charmEffects != null) return charmEffects;
            }
            return null;
        }

        public static List<VoreSprite> GetPluginSprites(SpriteType type, NPC npc)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                List<VoreSprite> sprites = plugin.GetSprites(type, npc);
                if (sprites != null) return sprites;
            }
            return null;
        }

        public static List<VoreSprite> GetPluginSprites(SpriteType type, Item item)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                List<VoreSprite> sprites = plugin.GetSprites(type, item);
                if (sprites != null) return sprites;
            }
            return null;
        }

        public static List<VoreDialogue> GetPluginDialogues(DialogueType type, VoreEntity entity)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                List<VoreDialogue> dialogues = plugin.GetDialogues(type, entity);
                if (dialogues != null) return dialogues;
            }
            return null;
        }
    }
}
