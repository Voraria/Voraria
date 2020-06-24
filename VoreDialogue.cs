using System.Collections.Generic;
using Terraria.Utilities;

namespace VoreMod
{

	public class VoreDialogue
	{
		DialogueType type;
		string text;
		VoreType voreType = VoreType.All;
		double weight = 1;

		public VoreDialogue(DialogueType type, string text)
		{
			this.type = type;
			this.text = text;
		}

		public DialogueType GetDialogueType() => type;

		public string GetText(VoreEntity pred, VoreEntity prey) => string.Format(text.Replace("{Pred}", "{0}").Replace("{Prey}", "{1}"), pred, prey);

		public VoreType GetVoreType() => voreType;

		public double GetWeight() => weight;

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

			public static implicit operator VoreDialogue(Builder builder) => builder.dialogue;
		}
	}
}
