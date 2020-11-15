

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
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.VoredPlayer, "Mmmf... part of me wants a chaser, but part of me wants to keep your awesome flavor on my tongue...~")
				.Dialogue(DialogueType.VoredPlayer, "Still think I can't stomach you and a- [c/00CC00:*HIC!*] -keg of beer, buster?")
				.Dialogue(DialogueType.VoredPlayer, "Will you quit your bellyachin' in there? You'll probably come right back to fondle the princess you're adding to, anyway.~")
				.Dialogue(DialogueType.VoredPlayer, "...huh? Let you out? I don't remember sayin' anything about lettin' you out...not my fault you're too busy staring at my rack to realize your fate.")
				.Dialogue(DialogueType.VoredPlayer, "Keep it up in there, it feels awesome...but quit screamin' or I'm drowning you in a lake's worth of beer.")
				.Dialogue(DialogueType.VoredOther, "Hey, can you get me a keg of wine? I need a chaser, just ate a big meal...")
				.Dialogue(DialogueType.VoredOther, "What? This nerd dared me to stomach them and a full bottle of everclear! That's free food and free money, I'm not gonna turn it down!")
				.Dialogue(DialogueType.VoredOther, "Haha, look at this gut...it's like I've got my own personal beer keg right here.~")
				.Dialogue(DialogueType.VoredOther, "God, my back hurts so much more with this chump hanging off me. They're gonna make my assets even heavier, too...")
				.Dialogue(DialogueType.VoredOther, "[c/00CC00:*BUOORP!*] ...ooo, sorry that got in your face. This princess gets hella gassy after her meals...and hella horny...")
				.Dialogue(DialogueType.PlayerVored, "...you better be enjoying your meal out there, or so help me I'll MAKE YOU ONE when I get out.")
				.Dialogue(DialogueType.PlayerVored, "Can't believe you'd go so far as to eat a princess...your gluttony doesn't have any end, does it?")
				.Dialogue(DialogueType.PlayerNonFatal, "......this...isn't so bad when I'm not being turned into belly fat, I guess...")
				.Dialogue(DialogueType.PlayerNonFatal, "Fine. You wanna hold me in here, you're gonna be my royal limo. To the castle, or no more Cirrus meat for you!")
				.Dialogue(DialogueType.PlayerDigesting, "HEY, what gives!? You BETTER let me outta here RIGHT NOW, buster! The goddess of your world is NOT food!")
				.Dialogue(DialogueType.PlayerDigesting, "Wh- you're really digesting me!? I'll give you a hell of a hangover, you greedy little...!")
				.Dialogue(DialogueType.PlayerDigesting, "I shouldn't be in here, I shouldn't be a meal for some chump off the streets...I SHOULD BE THE ONE DIGESTING YOU!")
				.Dialogue(DialogueType.PlayerAmulet, "Don't you go gettin' any ideas...these jugs are ONLY for show and for showing off!")
				.Dialogue(DialogueType.PlayerAmulet, "Get that hungry glint outta your eyes. Have a drink, it'll be more satisfying.")
				.Dialogue(DialogueType.PlayerAmulet, "Yeah, don't think I don't see you. You want a taste of this, don't you, ya little glutton?")
				.Dialogue(DialogueType.PlayerTalisman, "Hmhm... betcha a bottle of booze you can't last a day in my gut.~")
				.Dialogue(DialogueType.PlayerTalisman, "Huh? Whaddaya mean I can't stomach you with a keg of booze? Is that a challenge!?")
				.Dialogue(DialogueType.PlayerTalisman, "You know, I could probably eat you and then forget you went in there within the hour...crazy, huh?~")
				.Dialogue(DialogueType.PlayerTalisman, "Whuh? You wanna add to THESE girls? Kid, I've already got back problems WITHOUT you hanging on my gut...but alright.~")
				.Dialogue(DialogueType.PlayerTalisman, "A nap? Inside me? Alright, but fair warning: once you go in there, you're not getting back out...")
			)

			.NPC("THIEF", npc => npc
				.Tags(EntityTags.Female | EntityTags.TownNPC)
				.Sprite(SpriteType.Belly, "TownNPCs/Bandit_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.VoredPlayer, "...I wonder if I could use this belly to pass as bein' pregnant and get some sweet free stuff...")
				.Dialogue(DialogueType.VoredPlayer, "The best part of this is, I don't even have to worry about tearing...stole these extra-stretchy clothes off one of my first meals.")
				.Dialogue(DialogueType.DigestingPlayer, "If anyone asks, I'm not stealing you. I'm just borrowing you until you digest.")
				.Dialogue(DialogueType.DigestingPlayer, "...alright, {Pred}, startin' after this one digests, you REALLY gotta lay off the adventurers...gettin' too fat to sneak anywhere.")
				.Dialogue(DialogueType.DigestingPlayer, "Honestly? I just hope any hip sizes I gain from you don't ruin my next heist...")
				.Dialogue(DialogueType.NonFatalPlayer, "There you go... take a nap, relax in the best lootin' bag there is.")
				.Dialogue(DialogueType.NonFatalPlayer, "Yeah, that's right...take it easy, let my belly steal away your troubles for a while.~")
				.Dialogue(DialogueType.NonFatalPlayer, "Fun fact: I've stolen a couple of really pricey things before, and now they're just belly fat. Just be glad you won't end up like those things.")
				.Dialogue(DialogueType.PlayerAmulet, "You hungry over there, {Prey}? Could use a bit of practice with gettin' outta tight spaces...")
				.Dialogue(DialogueType.PlayerTalisman, "You know, you smell good enough to eat...how about you let me \"steal\" you away for a little bit?~")
				.Dialogue(DialogueType.PlayerTalisman, "Hey, you wanna help me practice swallowin' stuff? A thief's gotta do it more than you think...")
				.Dialogue(DialogueType.PlayerTalisman, "The feeling of having someone in my gut is more priceless than any relic, I swear... mind gettin' in for a bit?")
			);
	}
}