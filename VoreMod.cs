using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace VoreMod
{
	public class VoreMod : Mod
	{
		internal static VoreMod Instance;

		public VoreUI voreUI;

		public List<VorePlugin> plugins = new List<VorePlugin>();

		public bool cleanVoreGore = false;

		public GameTime lastTime;

		bool pluginsLoaded = false;

		public HashSet<int> activeGore = new HashSet<int>();

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
				pluginsLoaded = true;
			}
			ILEditingInitialize();
		}

		public static void ILEditingInitialize()
		{
			On.Terraria.Player.KillMe += (orig, player, reason, damage, hitDirection, pvp) =>
			{
				player.GetModPlayer<VorePlayer>().KillMe(reason, damage, hitDirection);
			};
		}

		public override void Unload()
		{
			voreUI = null;
			foreach (VorePlugin plugin in plugins)
				plugin.Unregister();
			plugins.Clear();
			pluginsLoaded = false;
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
