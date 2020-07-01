
using Terraria.ID;

namespace VoreMod.Plugins
{
    public class MonsterGirlEnemiesPlugin : VorePlugin
    {
        public override string Name => "MonsterGirlEnemies";

        public override Builder Build(Builder builder) => builder
            .NPC(NPCID.LunarTowerNebula, nameof(NPCID.LunarTowerNebula), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Boss)
                .Sprite(SpriteType.Belly, "LunarTowerNebulaBelly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(1)))

            .NPC(NPCID.LunarTowerSolar, nameof(NPCID.LunarTowerSolar), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Boss)
                .Sprite(SpriteType.Belly, "LunarTowerSolarBelly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(1)))

            .NPC(NPCID.LunarTowerStardust, nameof(NPCID.LunarTowerStardust), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Boss)
                .Sprite(SpriteType.Belly, "LunarTowerStardustBelly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(1)))

            .NPC(NPCID.LunarTowerVortex, nameof(NPCID.LunarTowerVortex), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Boss)
                .Sprite(SpriteType.Belly, "LunarTowerVortexBelly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(1)))

            .NPC(NPCID.Shark, nameof(NPCID.Shark), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Beast)
                .Sprite(SpriteType.Belly, "SharkBelly", sprite => sprite
                    .Layout(SpriteLayout.AnimY).Frames(4)))

            .NPC(NPCID.Wolf, nameof(NPCID.Wolf), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster | EntityTags.Beast)
                .Sprite(SpriteType.Belly, "WolfBelly", sprite => sprite
                    .Layout(SpriteLayout.AnimY).Frames(9)));
    }
}