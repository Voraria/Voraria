using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoreMod
{
	public static class VoreModUtils
	{

	}

	public static class VoreModExtensions
	{
		public static VorePlayer Vore(this Player player)
		{
			return player.GetModPlayer<VorePlayer>();
		}
		public static VoreNPC Vore(this NPC npc)
		{
			return npc.GetGlobalNPC<VoreNPC>();
		}
	}
}
