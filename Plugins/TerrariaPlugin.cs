
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
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f)))

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
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} is now keeping the corruption off of {Pred}'s hips.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} was purified by {Pred}'s cleansing belly.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} has been gifted a life as dryad fat.")
				.Dialogue(DialogueType.VoredOther, "The enemies of this world's purity cannot be allowed to continue. This one was quite tasty though.")
				.Dialogue(DialogueType.VoredOther, $"[c/00CC00:*BURP!*]" + "Aha...excuse me. One cannot purify the world on an empty stomach!")
				.Dialogue(DialogueType.VoredOther, "Yes, hero? Oh, this one? They were quite tasty, are you looking to join them?")
				.Dialogue(DialogueType.PlayerTalisman, "Hmm... you know, it's been a long time since I've had a live meal...")
				.Dialogue(DialogueType.PlayerTalisman, "Why is my stomach rumbling, you ask? I'm a tree nymph, we eat other creatures rather often.")
				.Dialogue(DialogueType.PlayerTalisman, "You've been off so much purifying the world... how about cleansing my stomach for a change?")
				.Dialogue(DialogueType.PlayerAmulet, "As champion of this world, you deserve a rewarding meal. Perhaps I can please your pallet.")
				.Dialogue(DialogueType.PlayerAmulet, "This world must be kept pure, lest the evils of the world spread further. A hero like you can't work at such a task on an empty stomach - let me help!")
				.Dialogue(DialogueType.PlayerAmulet, "Ah, a Throat Amulet! Is the hero of our world hungry? You don't need to ask twice!"))

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
					.Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11))
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} was guided through {Pred}'s insides.")
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} gave {Prey} a guided tour of his stomach.")
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} has guided {Prey} through digestion - by digesting them."))

			.NPC(NPCID.Mechanic, nameof(NPCID.Mechanic), npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/Mechanic_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} got {Pred}'s stomach too wired up."))

			.NPC(NPCID.Nurse, nameof(NPCID.Nurse), npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.CharmEffects(new CharmEffects() { life = ItemTier.SilverTungsten, acid = ItemTier.CopperTin, hunger = ItemTier.CopperTin })
				.Sprite(SpriteType.Belly, "TownNPCs/Nurse_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f)))

			.NPC(NPCID.OldMan, nameof(NPCID.OldMan), npc => npc
				.Tags(EntityTags.Male | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/OldMan_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11)))

			.NPC(NPCID.Painter, nameof(NPCID.Painter), npc => npc
				.Tags(EntityTags.Male | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/Painter_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f)))

			.NPC(NPCID.PartyGirl, nameof(NPCID.PartyGirl), npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/PartyGirl_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} stuffed herself like a Pigronata with {Prey}.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} spent too long at the party in {Pred}'s belly.")
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} gobbled up {Prey} like a big birthday cake.")
				.Dialogue(DialogueType.PlayerDigesting, "U-uhhh...I dunno if I like this party anymore...")
				.Dialogue(DialogueType.PlayerDigesting, "You're...you're gonna let me out soon, right? Hello? {Pred}?")
				.Dialogue(DialogueType.PlayerDigesting, "Mm...w--well, I hope you enjoyed me, at least! Like a big, delicious cake...")
				.Dialogue(DialogueType.PlayerNonFatal, "Ooo, I always love these sorts of parties! They're so comfy and warm...")
				.Dialogue(DialogueType.PlayerNonFatal, "Hey, can you send down some balloons and cupcakes? I'm kinda hungry too...")
				.Dialogue(DialogueType.PlayerNonFatal, "Mmm...might take a nap in here...so nice and cozy...")
				.Dialogue(DialogueType.VoredPlayer, "Ooo, you went down like a big, delicious cake! I SO wanna eat you again later...")
				.Dialogue(DialogueType.VoredPlayer, "You havin' fun at the party in my belly, {Prey}?")
				.Dialogue(DialogueType.VoredPlayer, "Oof, I feel so heavy now...I hope I can still have parties with you in there...")
				.Dialogue(DialogueType.VoredPlayer, "Hehee, look at how big you made me! Like a Pigronata full of candy!~")
				.Dialogue(DialogueType.DigestingPlayer, "Mmm... I really want cupcakes for dessert...not that they'd taste better than you, {Prey}.")
				.Dialogue(DialogueType.DigestingPlayer, "Aww, trying to leave so soon? The party's not over yet, silly! You gotta clean up after any good rave...")
				.Dialogue(DialogueType.DigestingPlayer, "Well, look at it this way: once you're belly fat, you'll always help me stay the life of the party! :D")
				.Dialogue(DialogueType.NonFatalPlayer, "See? It's not so bad! Just relax and have a great time!")
				.Dialogue(DialogueType.NonFatalPlayer, "Aren't my belly's parties the best? I try to invite other people, but they don't seem too excited...")
				.Dialogue(DialogueType.NonFatalPlayer, "You know, you can sleep in there if you want...slumber parties are always great, especially when they're so tasty and filling!")
				.Dialogue(DialogueType.NonFatalPlayer, "I should totally eat some balloons and party favors...no party's complete without 'em, after all!")
				.Dialogue(DialogueType.VoredOther, "Oohhh, it's always so fun to throw a party in my belly! You wanna join in?")
				.Dialogue(DialogueType.VoredOther, "Hey, do you have some balloons for me to eat? I think this tum needs to be a little more festive...")
				.Dialogue(DialogueType.VoredOther, "Happy birthday to you, happy " + $"[c/00BB00:*BURP!*]" + "-day to you...")
				.Dialogue(DialogueType.PlayerTalisman, "Ooo, that necklace looks cool...and you smell like a tasty cupcake with it on, too!")
				.Dialogue(DialogueType.PlayerTalisman, "Heyyyy! There's a party in my belly, and you're invited! Wanna stop by?")
				.Dialogue(DialogueType.PlayerTalisman, "You smell like a yummy cake...come on, lemme have a taste! Just one bite, I promise!")
				.Dialogue(DialogueType.PlayerAmulet, "You look...hungry...r-really hungry...m-maybe I can give you a cake or something?")
				.Dialogue(DialogueType.PlayerAmulet, "H-huh? A sweet snack to eat? W-well, I have some sweets for you to munch on, maybe?")
				.Dialogue(DialogueType.PlayerAmulet, "Wait, a party in YOUR belly?...that sounds awesome, lemme in!"))

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
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.DigestingPlayer, "Bloody 'ell, quit your bellyachin' and digest in there!", d => d.Tags(new DialogueTags[] { DialogueTags.BloodMoon })))

			.NPC(NPCID.Stylist, nameof(NPCID.Stylist), npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/Stylist_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} let {Prey} marinate for a while in her stomach acids.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} got a free 'cut courtesy of {Pred}'s gut.")
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} just couldn't resist {Prey}'s delectable hair.")
				.Dialogue(DialogueType.VoredPlayer, "Mmmf...something about your scalp just makes it so...irresistibly tasty...")
				.Dialogue(DialogueType.VoredPlayer, "This is MUCH better than just doing your hair with scissors and stuff. Much more filling, too...")
				.Dialogue(DialogueType.DigestingPlayer, "Is...is this how all those spiders feel when they eat their prey? I'll have to try this a bit more myself...")
				.Dialogue(DialogueType.DigestingPlayer, "W-well, hey, you've got an easy cut. Just stick your head in those acids for a bit, your hair'll be nice and short!")
				.Dialogue(DialogueType.DigestingPlayer, "Look, we'll cut a deal: you stay and digest, and I give you a discount on your next cut. Will that make you stop squirming?")
				.Dialogue(DialogueType.NonFatalPlayer, "Now, be careful not to rub against the walls too much. You wouldn't want your hair to get ruined.")
				.Dialogue(DialogueType.NonFatalPlayer, "Alright, sit in there and marinate for now, I'll drink some water to rinse your color out in about 25 minutes...")
				.Dialogue(DialogueType.NonFatalPlayer, "Just relax and let your split ends melt away, honey...we GOTTA get you up to speed on the local gossip.", d => d.Tags(DialogueTags.FemalePlayer))
				.Dialogue(DialogueType.NonFatalPlayer, "Yeah, girl, {Nurse} ate her last boyfriend like a snack...wonder if she's happy with the extra fat he left for her.", d => d.Tags(new DialogueTags[] { DialogueTags.FemalePlayer, DialogueTags.NursePresent }))
				.Dialogue(DialogueType.VoredOther, "Oh, you want a haircut? Just sit in the chair, I'll do yours once I'm done with this one...")
				.Dialogue(DialogueType.VoredOther, "Does this belly make my hair look bad? Give me an honest answer, I won't judge.")
				.Dialogue(DialogueType.VoredOther, "Alright, just a minute, I'll be with you once my current client's done... what kinda cut do you want?")
				.Dialogue(DialogueType.DigestingOther, "Go on, sit in the chair. Don't mind the rumbling, just giving another client a more...thorough cut.")
				.Dialogue(DialogueType.DigestingOther, "Mmmf...alright, come here. I'll get that pesky hair off your head, have some dessert while I'm at it.")
				.Dialogue(DialogueType.DigestingOther, "Give me a nice, quality belly rub to help with my previous client's cut, and your next cut's free. Sound good?")
				.Dialogue(DialogueType.PlayerAmulet, "H-huh? Me? I mean...as long as my hair doesn't get too ruined, I guess it's okay.")
				.Dialogue(DialogueType.PlayerAmulet, "Really? Right now? I just got a perm, your gut's gonna mess it all up!")
				.Dialogue(DialogueType.PlayerAmulet, "...fiiiine, you can eat me...but if my hair gets digested, I'm blaming you!")
				.Dialogue(DialogueType.PlayerTalisman, "...mm, normally I'm not too confident with internal cuts, but since you saved my life and all...you can have special treatment.")
				.Dialogue(DialogueType.PlayerTalisman, "Just a little off the top? In a belly? Hun, you're gonna lose a lot more than you asked for...")
				.Dialogue(DialogueType.PlayerTalisman, "A-a private 'cut?...alright, just stand here for a moment while I get ready.")
				.Dialogue(DialogueType.PlayerTalisman, "Ooh...a special internal cut for the party? I-I'm sure my belly and I can provide, just a minute...", d => d.Tags(DialogueTags.Party)))
				

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
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f)))

			.NPC(NPCID.Truffle, nameof(NPCID.Truffle), npc => npc
				.Tags(EntityTags.Male | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/Truffle_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f)))

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

		#region Armor pieces
			.Item(ItemID.TheBrideDress, nameof(ItemID.TheBrideDress), item => item
				.Sprite(SpriteType.Belly, "Clothing/WeddingDress_Belly_Overlay", sprite => sprite
					.Layout(SpriteLayout.SizeY).Frames(15).Offset(14f, 10f).FrameOffset(0f, -2f, 7, 9).FrameOffset(0f, -2f, 14, 16).ColorMode(ColorMode.Dye)))

			.Item(ItemID.HuntressAltShirt, nameof(ItemID.HuntressAltShirt), item => item
				.Sprite(SpriteType.Belly, "Clothing/RedRidingDress_Belly_Overlay", sprite => sprite
					.Layout(SpriteLayout.SizeY).Frames(15).Offset(14f, 10f).FrameOffset(0f, -2f, 7, 9).FrameOffset(0f, -2f, 14, 16).ColorMode(ColorMode.Dye)))

			.Item(ItemID.PrincessDress, nameof(ItemID.PrincessDress), item => item
				.Sprite(SpriteType.Belly, "Clothing/PrincessDressClothier_Belly_Overlay", sprite => sprite
					.Layout(SpriteLayout.SizeY).Frames(15).Offset(14f, 10f).FrameOffset(0f, -2f, 7, 9).FrameOffset(0f, -2f, 14, 16).ColorMode(ColorMode.Dye)));
		#endregion
	}
}
