using Terraria;

namespace VoreMod
{
	public class DialogueHelpers
	{
		public static string GetNPCName(int type)
		{
			return Main.npc[NPC.FindFirstNPC(type)].GivenName;
		}
	}
}
