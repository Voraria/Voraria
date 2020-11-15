using System.Collections.Generic;
using Terraria.ID;
using Terraria.Utilities;

namespace VoreMod
{
	public class VoreDialogue
	{
		DialogueType type;
		string text;
		VoreType voreType = VoreType.All;
		double weight = 1;
		DialogueTags tags = DialogueTags.None;

		public VoreDialogue(DialogueType type, string text)
		{
			this.type = type;
			this.text = text;
		}

		public DialogueType GetDialogueType() => type;

		public string GetText(VoreEntity pred, VoreEntity prey) => string.Format(text
			.Replace("{Pred}", "{0}")
			.Replace("{Prey}", "{1}")
			.Replace("{Dryad}", DialogueHelpers.GetNPCName(NPCID.Dryad))
			.Replace("{Nurse}", DialogueHelpers.GetNPCName(NPCID.Nurse))
			.Replace("{Stylist}", DialogueHelpers.GetNPCName(NPCID.Stylist)),
			pred, prey);

		public VoreType GetVoreType() => voreType;

		public double GetWeight() => weight;

		public DialogueTags GetTags() => tags;

		public class Builder
		{
			VoreDialogue dialogue;

			public Builder(VoreDialogue dialogue)
			{
				this.dialogue = dialogue;
			}

			public Builder VoreType(VoreType voreType)
			{
				dialogue.voreType = voreType;
				return this;
			}

			public Builder Weight(double weight)
			{
				dialogue.weight = weight;
				return this;
			}

			public Builder Tags(params DialogueTags[] tags)
			{
				foreach (DialogueTags tag in tags) dialogue.tags |= tag;
				return this;
			}

			public static implicit operator VoreDialogue(Builder builder) => builder.dialogue;
		}
	}
}
