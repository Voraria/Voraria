
using VoreMod.NPCs.VoreMod.TownNPCs;

namespace VoreMod.Plugins
{
    public class VoreModPlugin : VorePlugin
    {
        public override string Name => nameof(VoreMod);

        public override Builder Build(Builder builder) => builder
            .NPC(nameof(Succubus), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .CharmEffects(new CharmEffects() { life = ItemTier.CopperTin, acid = ItemTier.Hellstone, soul = ItemTier.Hellstone, hunger = ItemTier.CopperTin })
                .Sprite(SpriteType.Belly, "TownNPCs/Succubus_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)
                )
            );
    }
}