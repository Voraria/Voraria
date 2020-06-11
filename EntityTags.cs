
namespace VoreMod
{
    [System.Flags]
    public enum EntityTags
    {
        None = 0,
        Player = 1 << 0,
        TownNPC = 1 << 1,
        Monster = 1 << 2,
        Male = 1 << 3,
        Female = 1 << 4,
        Genderless = 1 << 5,
        Worm = 1 << 6,
        Zombie = 1 << 7,
        Skeleton = 1 << 8,
        Eye = 1 << 9,
        Slime = 1 << 10,
        Spider = 1 << 11,
        All = ~0,
    }

    public class EntityTagObject
    {
        public bool Player;
        public bool TownNPC;
        public bool Monster;
        public bool Male;
        public bool Female;
        public bool Genderless;
        public bool Worm;
        public bool Zombie;
        public bool Skeleton;
        public bool Eye;
        public bool Slime;
        public bool Spider;

        public static implicit operator EntityTags(EntityTagObject obj)
        {
            EntityTags tags = EntityTags.None;
            if (obj.Player) tags |= EntityTags.Player;
            if (obj.TownNPC) tags |= EntityTags.TownNPC;
            if (obj.Monster) tags |= EntityTags.Monster;
            if (obj.Male) tags |= EntityTags.Male;
            if (obj.Female) tags |= EntityTags.Female;
            if (obj.Genderless) tags |= EntityTags.Genderless;
            if (obj.Worm) tags |= EntityTags.Worm;
            if (obj.Zombie) tags |= EntityTags.Zombie;
            if (obj.Skeleton) tags |= EntityTags.Skeleton;
            if (obj.Eye) tags |= EntityTags.Eye;
            if (obj.Slime) tags |= EntityTags.Slime;
            if (obj.Spider) tags |= EntityTags.Spider;
            return tags;
        }

        public static implicit operator EntityTagObject(EntityTags tags)
        {
            EntityTagObject obj = new EntityTagObject()
            {
                Player = tags.HasAll(EntityTags.Player),
                TownNPC = tags.HasAll(EntityTags.TownNPC),
                Monster = tags.HasAll(EntityTags.Monster),
                Male = tags.HasAll(EntityTags.Male),
                Female = tags.HasAll(EntityTags.Female),
                Genderless = tags.HasAll(EntityTags.Genderless),
                Worm = tags.HasAll(EntityTags.Worm),
                Zombie = tags.HasAll(EntityTags.Zombie),
                Skeleton = tags.HasAll(EntityTags.Skeleton),
                Eye = tags.HasAll(EntityTags.Eye),
                Slime = tags.HasAll(EntityTags.Slime),
                Spider = tags.HasAll(EntityTags.Spider),
            };
            return obj;
        }
    }
}
