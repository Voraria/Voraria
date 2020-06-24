

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
					.Frames(6))
				.Dialogue(DialogueType.VoredPlayer, "Mmmf... part of me wants a chaser, but part of me wants to keep your awesome flavor on my tongue...~")
				.Dialogue(DialogueType.VoredPlayer, "Still think I can't stomach you and a- " + $"[c/00FF00:*HIC!*]" + " -keg of beer, buster?")
				.Dialogue(DialogueType.VoredPlayer, "Will you quit your bellyachin' in there? You'll probably come right back to fondle the bod you're adding to, anyway.~")
				.Dialogue(DialogueType.VoredPlayer, "...huh? Let you out? I don't remember sayin' anything about lettin' you out...")
				.Dialogue(DialogueType.VoredPlayer, "Keep it up in there, it feels awesome... but quit screamin' or I'm drowning you in a keg's worth of beer in there.")
				.Dialogue(DialogueType.VoredOther, "Hey, can you get me a keg of wine? I need a chaser, just ate a big meal...")
				.Dialogue(DialogueType.VoredOther, "What? This nerd dared me to stomach them and a full bottle of everclear! That's free food and free money, I'm not gonna turn it down!")
				.Dialogue(DialogueType.VoredOther, "Haha, look at this gut... it's like I've got my own personal beer keg right here.~")
				.Dialogue(DialogueType.NonFatalOther, "God, my back hurts so much more with this chump hanging off me. They're havin' fun, though, so...who really cares?")
				.Dialogue(DialogueType.DigestingOther, "God, my back hurts so much more with this chump hanging off me. They're gonna make my assets even heavier, too...")
				.Dialogue(DialogueType.PlayerAmulet, "Don't you go gettin' any ideas...these jugs are ONLY for show and for showing off!")
				.Dialogue(DialogueType.PlayerAmulet, "Get that hungry glint outta your eyes. Have a drink, it'll be more satisfying.")
				.Dialogue(DialogueType.PlayerAmulet, "Yeah, don't think I don't see you. You want a taste of this, don't you?")
				.Dialogue(DialogueType.PlayerTalisman, "Hmhm... betcha a bottle of booze you can't last a day in my gut.~")
				.Dialogue(DialogueType.PlayerTalisman, "Huh? Whaddaya mean I can't stomach you with a keg of booze? Is that a challenge!?")
				.Dialogue(DialogueType.PlayerTalisman, "You know, I could probably eat you and then forget you went in there within the hour... crazy, huh?~")
				.Dialogue(DialogueType.PlayerTalisman, "Whuh? You wanna add to THESE girls? Kid, I've already got back problems WITHOUT you hanging on my gut... but alright.~")
				.Dialogue(DialogueType.PlayerTalisman, "A nap? Inside me? Alright, but fair warning: once you go in there, you're not getting back out...")
			)

			.NPC("THIEF", npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.Dialogue(DialogueType.VoredPlayer, "...I wonder if I could use this belly to pass as being pregnant and get some sweet deals...")
				.Dialogue(DialogueType.DigestingPlayer, "If anyone asks, I'm not stealing you. I'm just borrowing you until you digest.")
				.Dialogue(DialogueType.DigestingPlayer, "You'd be surprised, but you're not the first person I've eaten... and you won't be the last.")
				.Dialogue(DialogueType.DigestingPlayer, "Honestly? I just hope any hip sizes I gain from you don't ruin my next heist...")
				.Dialogue(DialogueType.NonFatalPlayer, "There you go... take a nap, relax in the best lootin' bag there is.")
				.Dialogue(DialogueType.NonFatalPlayer, "Yeah, that's right...take it easy, let my belly steal away your troubles for a while.~")
				.Dialogue(DialogueType.PlayerTalisman, "You know, you smell good enough to eat...how about you let me \"steal\" you away for a little bit?~")
				.Dialogue(DialogueType.PlayerTalisman, "Hey, you wanna help me practice swallowin' stuff? A thief's gotta do it more than you think...")
				.Dialogue(DialogueType.PlayerTalisman, "Fun fact: I've stolen a couple of really pricey things before, and now they're just belly fat. Shame, too, could've made 'em into more money instead of more me.")
				.Dialogue(DialogueType.PlayerTalisman, "The feeling of having someone in my gut is more priceless than any relic, I swear... mind gettin' in for a bit?")
			);
	}
}