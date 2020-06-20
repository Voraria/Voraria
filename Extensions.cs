using Terraria;
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

        public static List<T> SafeConcat<T>(this List<T> list, IEnumerable<T> other)
        {
            if (other != null) list.AddRange(other);
            return list;
        }

        public static U MaxOrDefault<T, U>(this IEnumerable<T> self, System.Func<T, U> selector, U defaultValue = default(U))
        {
            if (self.Any()) return self.Max(selector);
            return defaultValue;
        }
    }
}
