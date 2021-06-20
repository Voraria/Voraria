using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ModLoader.Config.UI;

namespace VoreMod
{
	[Label("Settings")]
	public class VoreConfig : ModConfig
	{
		public static VoreConfig Instance => ModContent.GetInstance<VoreConfig>();
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("Interactions")]

		[Label("Can be a predator")]
		public VoreTagList CanSwallow = new VoreTagList();
		[Label("Can be prey")]
		public VoreTagList CanBeSwallowed = new VoreTagList();
		[Label("Can digest prey")]
		public VoreTagList CanDigest = new VoreTagList();
		[Label("Can be digested")]
		public VoreTagList CanBeDigested = new VoreTagList();
		[Label("Can struggle and escape")]
		public VoreTagList CanStruggle = new VoreTagList();
		[Label("Can randomly vore on contact")]
		public VoreTagList CanRandomVore = new VoreTagList();

		[Header("Sounds")]

		[Label("Oral vore gulping")]
		[DefaultValue(true)]
		public bool SoundsSwallowGulping;
		[Label("Digestion gurgling")]
		[DefaultValue(true)]
		public bool SoundsDigestionGurgling;
		[Label("Digestion burping")]
		[DefaultValue(true)]
		public bool SoundsDigestionBurping;
		[Label("Digestion farting")]
		[DefaultValue(true)]
		public bool SoundsDigestionFarting;
		[Label("Disposal belching")]
		[DefaultValue(true)]
		public bool SoundsDisposalBelching;
		[Label("Disposal farting")]
		[DefaultValue(true)]
		public bool SoundsDisposalFarting;
		[Label("Regurgitation puking")]
		[DefaultValue(true)]
		public bool SoundsRegurgitatePuking;

		[Header("Tweaks")]

		[Label("Always struggle (even with friendly preds)")]
		[DefaultValue(false)]
		public bool TweakAlwaysStruggle;
		[Label("Predators digest dropped coins/items")]
		[DefaultValue(false)]
		public bool TweakItemsAreFood;
		[Label("Reenable monster gore")]
		[DefaultValue(false)]
		public bool TweakEnableGore;

		[Header("Debug")]

		[Label("Enable extra debug info")]
		[DefaultValue(false)]
		public bool DebugInfo;
		[Label("Always show bellies as full")]
		[DefaultValue(false)]
		public bool DebugFullBellies;
		[Label("Never show bellies")]
		[DefaultValue(false)]
		public bool DebugNoBellies;
		[Label("Never show prey for semi-transparent preds")]
		[DefaultValue(false)]
		public bool DebugNoLayeredPrey;
		[Label("Disable prey capacity limit")]
		[DefaultValue(false)]
		public bool DebugNoPreyCapacityLimit;
		[Label("Disable struggle checks (auto-success)")]
		[DefaultValue(false)]
		public bool DebugNoStruggle;
	}

	public class VoreTagList
	{
		[Label("Tag combinations to allow (default: all)")]
		public List<VoreTagListItem> allow = new List<VoreTagListItem>();
		[Label("Tag combinations to disallow (default: none)")]
		public List<VoreTagListItem> disallow = new List<VoreTagListItem>();

		public bool Match(VoreEntity entity)
		{
			bool matches = allow.Count == 0;
			foreach (VoreTagListItem item in allow)
				matches |= item.Match(entity);
			foreach (VoreTagListItem item in disallow)
				matches &= !item.Match(entity);
			return matches;
		}
	}

	public class VoreTagListItem
	{
		[Label("Filter on any of these tags instead of all")]
		public bool matchAny;
		[Label("Tags")]
		public EntityTagObject tags = new EntityTagObject();

		public bool Match(VoreEntity entity)
		{
			return matchAny ? entity.GetTags().HasAny(tags) : entity.GetTags().HasAll(tags);
		}

		public override string ToString()
		{
			string tagStr = ((EntityTags)tags).ToString();
			if (tagStr.Contains(", "))
			{
				int index = tagStr.LastIndexOf(", ");
				tagStr = tagStr.Substring(0, index) + (matchAny ? " or " : " and ") + tagStr.Substring(index + 2);
			}
			return "Creatures matching " + (matchAny ? "any" : "all") + " of " + tagStr;
		}
	}
}
