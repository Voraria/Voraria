
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
        Bat = 1 << 12,
        Hornet = 1 << 13,
        Undead = 1 << 14,
        Beast = 1 << 15,
        Reptile = 1 << 16,
        Construct = 1 << 17,
        Plant = 1 << 18,
        Critter = 1 << 19,
        Fish = 1 << 20,
        Insect = 1 << 21,
        Flying = 1 << 22,
        Mimic = 1 << 23,
        Projectile = 1 << 24,
        Boss = 1 << 25,
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
        public bool Bat;
        public bool Hornet;
        public bool Undead;
        public bool Beast;
        public bool Reptile;
        public bool Construct;
        public bool Plant;
        public bool Critter;
        public bool Fish;
        public bool Insect;
        public bool Flying;
        public bool Mimic;
        public bool Projectile;
        public bool Boss;

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
            if (obj.Bat) tags |= EntityTags.Bat;
            if (obj.Hornet) tags |= EntityTags.Hornet;
            if (obj.Undead) tags |= EntityTags.Undead;
            if (obj.Beast) tags |= EntityTags.Beast;
            if (obj.Reptile) tags |= EntityTags.Reptile;
            if (obj.Construct) tags |= EntityTags.Construct;
            if (obj.Plant) tags |= EntityTags.Plant;
            if (obj.Critter) tags |= EntityTags.Critter;
            if (obj.Fish) tags |= EntityTags.Fish;
            if (obj.Insect) tags |= EntityTags.Insect;
            if (obj.Flying) tags |= EntityTags.Flying;
            if (obj.Mimic) tags |= EntityTags.Mimic;
            if (obj.Projectile) tags |= EntityTags.Projectile;
            if (obj.Boss) tags |= EntityTags.Boss;
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
                Bat = tags.HasAll(EntityTags.Bat),
                Hornet = tags.HasAll(EntityTags.Hornet),
                Undead = tags.HasAll(EntityTags.Undead),
                Beast = tags.HasAll(EntityTags.Beast),
                Reptile = tags.HasAll(EntityTags.Reptile),
                Construct = tags.HasAll(EntityTags.Construct),
                Plant = tags.HasAll(EntityTags.Plant),
                Critter = tags.HasAll(EntityTags.Critter),
                Fish = tags.HasAll(EntityTags.Fish),
                Insect = tags.HasAll(EntityTags.Insect),
                Flying = tags.HasAll(EntityTags.Flying),
                Mimic = tags.HasAll(EntityTags.Mimic),
                Projectile = tags.HasAll(EntityTags.Projectile),
                Boss = tags.HasAll(EntityTags.Boss),
            };
            return obj;
        }
    }
}
