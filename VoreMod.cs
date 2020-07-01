using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI;

namespace VoreMod
{
    public class VoreMod : Mod
    {
        internal static VoreMod Instance;

        public VoreUI voreUI;

        public List<VorePlugin> plugins = new List<VorePlugin>();

        public bool cleanVoreGore = false;

        GameTime lastTime;

        bool pluginsLoaded = false;

        HashSet<int> activeGore = new HashSet<int>();

        public VoreMod()
        {
            Instance = this;
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

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

        public override void PostUpdateEverything()
        {
            for (int i = 0; i < Main.gore.Length; i++)
            {
                Gore gore = Main.gore[i];
                if (gore.active)
                {
                    if (cleanVoreGore && !activeGore.Contains(i))
                    {
                        gore.active = false;
                    }
                    activeGore.Add(i);
                }
                else
                {
                    activeGore.Remove(i);
                }
            }
            cleanVoreGore = false;
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            Main.LocalPlayer.GetModPlayer<VorePlayer>().DrawUI(spriteBatch);
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

        public static List<VoreDialogue> GetPluginDialogues(DialogueType type, VoreEntity entity, DialogueTags tags)
        {
            foreach (VorePlugin plugin in GetValidPlugins())
            {
                List<VoreDialogue> dialogues = plugin.GetDialogues(type, entity, tags);
                if (dialogues != null) return dialogues;
            }
            return null;
        }
    }
}
