using Terraria;
using Terraria.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace VoreMod
{
	public static class Extensions
	{
		public static VoreEntity GetEntity(this Player player)
		{
			VorePlayer modPlayer = player.GetModPlayer<VorePlayer>();
			if (modPlayer.entity == null) modPlayer.entity = new VoreEntityPlayer(player);
			return modPlayer.entity;
		}

		public static VoreEntity GetEntity(this NPC npc)
		{
			VoreNPC modNPC = npc.GetGlobalNPC<VoreNPC>();
			if (modNPC.entity == null) modNPC.entity = new VoreEntityNPC(npc);
			return modNPC.entity;
		}

		public static bool HasAll(this EntityTags self, EntityTags tags)
		{
			return (self & tags) == tags;
		}

		public static bool HasAny(this EntityTags self, EntityTags tags)
		{
			return (self & tags) != 0;
		}

		public static bool HasAll(this DialogueTags self, DialogueTags tags)
		{
			return (self & tags) == tags;
		}

		public static bool HasAny(this DialogueTags self, DialogueTags tags)
		{
			return (self & tags) != 0;
		}

		public static List<T> SafeConcat<T>(this List<T> list, IEnumerable<T> other)
		{
			if (other != null) list.AddRange(other);
			return list;
		}

		public static List<T> EmptyToNull<T>(this List<T> list)
		{
			if (list.Count == 0) return null;
			return list;
		}

		public static U MaxOrDefault<T, U>(this IEnumerable<T> self, System.Func<T, U> selector, U defaultValue = default(U))
		{
			if (self.Any()) return self.Max(selector);
			return defaultValue;
		}

		public static void AddAll(this WeightedRandom<string> self, List<VoreDialogue> dialogues, VoreEntity pred, VoreEntity prey)
		{
			foreach (VoreDialogue dialogue in dialogues) self.Add(dialogue.GetText(pred, prey), dialogue.GetWeight());
		}

		public static bool TryAddAll(this WeightedRandom<string> self, List<VoreDialogue> dialogues, VoreEntity pred, VoreEntity prey)
		{
			if (dialogues == null || dialogues.Count == 0) return false;
			self.AddAll(dialogues, pred, prey);
			return true;
		}
	}
}
