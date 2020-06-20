
using Terraria.ID;

namespace VoreMod.Plugins
{
    public class TerrariaPlugin : VorePlugin
    {
        public override string Name => "Terraria";

        public override Builder Build(Builder builder) => builder
        #region Town NPCs
            .NPC(NPCID.ArmsDealer, nameof(NPCID.ArmsDealer), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/ArmsDealer_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Clothier, nameof(NPCID.Clothier), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Clothier_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Cyborg, nameof(NPCID.Cyborg), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Cyborg_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Demolitionist, nameof(NPCID.Demolitionist), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Demolitionist_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Dryad, nameof(NPCID.Dryad), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .CharmEffects(new CharmEffects() { life = ItemTier.CopperTin, mana = ItemTier.SilverTungsten, acid = ItemTier.CopperTin, hunger = ItemTier.CopperTin })
                .Sprite(SpriteType.Belly, "TownNPCs/Dryad_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.DyeTrader, nameof(NPCID.DyeTrader), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/DyeTrader_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.GoblinTinkerer, nameof(NPCID.GoblinTinkerer), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/GoblinTinkerer_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(2f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Guide, nameof(NPCID.Guide), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Guide_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Mechanic, nameof(NPCID.Mechanic), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Mechanic_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Nurse, nameof(NPCID.Nurse), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .CharmEffects(new CharmEffects() { life = ItemTier.SilverTungsten, acid = ItemTier.CopperTin, hunger = ItemTier.CopperTin })
                .Sprite(SpriteType.Belly, "TownNPCs/Nurse_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.OldMan, nameof(NPCID.OldMan), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/OldMan_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Painter, nameof(NPCID.Painter), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Painter_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(1f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.PartyGirl, nameof(NPCID.PartyGirl), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/PartyGirl_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Pirate, nameof(NPCID.Pirate), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Pirate_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.SantaClaus, nameof(NPCID.SantaClaus), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Santa_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Steampunker, nameof(NPCID.Steampunker), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Steampunker_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Stylist, nameof(NPCID.Stylist), npc => npc
                .Tags(EntityTags.Female | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Stylist_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.DD2Bartender, nameof(NPCID.DD2Bartender), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Tavernkeep_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(0f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.TaxCollector, nameof(NPCID.TaxCollector), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/TaxCollector_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(3f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.TravellingMerchant, nameof(NPCID.TravellingMerchant), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/TravelingMerchant_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Truffle, nameof(NPCID.Truffle), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .Sprite(SpriteType.Belly, "TownNPCs/Truffle_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(6f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.WitchDoctor, nameof(NPCID.WitchDoctor), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .CharmEffects(new CharmEffects() { life = ItemTier.SilverTungsten, acid = ItemTier.CopperTin, hunger = ItemTier.CopperTin })
                .Sprite(SpriteType.Belly, "TownNPCs/WitchDoctor_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(-1f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

            .NPC(NPCID.Wizard, nameof(NPCID.Wizard), npc => npc
                .Tags(EntityTags.Male | EntityTags.TownNPC)
                .CharmEffects(new CharmEffects() { life = ItemTier.CopperTin, mana = ItemTier.SilverTungsten, acid = ItemTier.CopperTin, hunger = ItemTier.CopperTin })
                .Sprite(SpriteType.Belly, "TownNPCs/Wizard_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 6f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))
        #endregion

        #region Monsters
            .NPC(NPCID.Harpy, nameof(NPCID.Harpy), npc => npc
                .Tags(EntityTags.Female | EntityTags.Flying | EntityTags.Monster)
                .Sprite(SpriteType.Belly, "Humanoids/Harpy_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(4.5f, 4.5f)))

            .NPC(NPCID.Werewolf, nameof(NPCID.Werewolf), npc => npc
                .Tags(EntityTags.Monster | EntityTags.Beast)
                .Sprite(SpriteType.Belly, "Humanoids/Werewolf_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeXAnimY).Frames(4).Offset(-2f, -4f)))

            .NPC(NPCID.Nymph, nameof(NPCID.Nymph), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster)
                .Sprite(SpriteType.Belly, "Humanoids/Nymph_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(3.5f, 8f).Color(182, 186, 146)))

            .NPC(NPCID.DesertLamiaLight, nameof(NPCID.DesertLamiaLight), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster)
                .Sprite(SpriteType.Belly, "Humanoids/Lamia_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(9f, 6f).FrameOffset(2f, 0f, 3, 4).FrameOffset(2f, 0f, 7).FrameOffset(0f, -8f, 8)))

            .NPC(NPCID.DesertLamiaDark, nameof(NPCID.DesertLamiaDark), npc => npc
                .Tags(EntityTags.Female | EntityTags.Monster)
                .Sprite(SpriteType.Belly, "Humanoids/Lamia_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY).Frames(6).Offset(9f, 6f).FrameOffset(2f, 0f, 3, 4).FrameOffset(2f, 0f, 7).FrameOffset(0f, -8f, 8)))
        #endregion

        #region Vanity
            .Item(ItemID.TheBrideDress, nameof(ItemID.TheBrideDress), item => item
                .Sprite(SpriteType.Belly, "Clothing/WeddingDress_Belly", sprite => sprite
                    .Layout(SpriteLayout.SizeY)
                    .Frames(15)
                    .ColorMode(ColorMode.Skin)
                )
                .Sprite(SpriteType.Belly, "Clothing/WeddingDress_Belly_Overlay", sprite => sprite
                    .Layout(SpriteLayout.SizeY)
                    .Frames(15)
                    .ColorMode(ColorMode.Dye)
                )
            );
        #endregion
    }
}