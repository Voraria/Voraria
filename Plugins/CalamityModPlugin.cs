

namespace VoreMod.Plugins
{
    public class CalamityModPlugin : VorePlugin
    {
        public override string Name => "CalamityMod";

        public override Builder Build(Builder builder) => builder
            .NPC("FAP", npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/DrunkPrincess_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY)
                    .Frames(6)
                )
            );
    }
}