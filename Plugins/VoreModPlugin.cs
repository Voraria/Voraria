using VoreMod.NPCs.VoreMod.TownNPCs;

using ET = VoreMod.EntityTags;
using DT = VoreMod.DialogueTags;

namespace VoreMod.Plugins
{
	public class VoreModPlugin : VorePlugin
	{
		public override string Name => nameof(VoreMod);

		public override Builder Build(Builder builder) => builder
			.NPC(nameof(Succubus), npc => npc
				.Tags(ET.Female, ET.TownNPC)
				.CharmEffects(new CharmEffects() { life = ItemTier.CopperTin, acid = ItemTier.Hellstone, soul = ItemTier.Hellstone, hunger = ItemTier.CopperTin })
				.Sprite(SpriteType.Belly, "TownNPCs/Succubus_Belly", sprite => sprite
					.Layout(SpriteLayout.SizeY)
					.Frames(15)
					.Offset(13f, 8f))
				.Dialogue(DialogueType.Chat, "I could really go for a meal right now... You offering?")
				.Dialogue(DialogueType.Chat, "Man, I'm starving...")
				.Dialogue(DialogueType.Chat, "In this world, it's eat or be eaten.")
				.Dialogue(DialogueType.Chat, "So you interested in vore, or what?")
				.Dialogue(DialogueType.Chat, "Got some charms I'm looking to sell. Might be useful to a morsel-- I mean, mortal-- like you.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} was reduced to {Pred}'s demonic assfat.")
				.Dialogue(DialogueType.DigestedPlayer, "{Pred} allowed {Prey} to ride in her gut for a little too long.")
				.Dialogue(DialogueType.DigestedPlayer, "{Prey} helped fatten up {Pred}'s thick demonic thighs.")
				.Dialogue(DialogueType.PlayerDigesting, "A-Are you digesting me?!")
				.Dialogue(DialogueType.PlayerDigesting, "H-Hey! I'm a predator, not prey! Y-You can't digest me like this!")
				.Dialogue(DialogueType.PlayerDigesting, "W-Well, you're more like a succubus than I thought...I'm not getting out of here alive, am I?")
				.Dialogue(DialogueType.PlayerNonFatal, "Your stomach is pretty cozy. I could get used to this.")
				.Dialogue(DialogueType.PlayerNonFatal, "You know, normally I'm the one eating people, but I'm okay with this.")
				.Dialogue(DialogueType.PlayerNonFatal, "It's a little tight in here, but I'll manage, don't worry.")
				.Dialogue(DialogueType.VoredPlayer, "Ahh...delicious!~ Thanks for the snack, {Prey}.")
				.Dialogue(DialogueType.VoredPlayer, "Mhm, you were tasty, {Prey}... " + $"[c/00FF00:*BURP!*]")
				.Dialogue(DialogueType.VoredPlayer, "What a meal, thanks for being food.")
				.Dialogue(DialogueType.DigestingPlayer, "That's it, churn up for me, digest, you'll make fine assfat for me.")
				.Dialogue(DialogueType.DigestingPlayer, "What's the matter? Why all the struggles? You're just food now - accept it and digest like a good little meal.")
				.Dialogue(DialogueType.DigestingPlayer, "Quit struggling, and digest. Honestly, preythings these days are so rude.")
				.Dialogue(DialogueType.NonFatalPlayer, "You were delicious, dear. Now just relax and enjoy yourself.")
				.Dialogue(DialogueType.NonFatalPlayer, "What a tasty snack you made, mortal. We should do this more often!")
				.Dialogue(DialogueType.NonFatalPlayer, $"[c/00FF00:*BWOORP!*]" + "...heh, excuse me.")
				.Dialogue(DialogueType.DigestingOther, "Don't mind the belly, just digesting lunch.")
				.Dialogue(DialogueType.DigestingOther, "What's up? Just digesting some prey, don't worry.")
				.Dialogue(DialogueType.DigestingOther, "Does it really matter who they were? They're succubus fat now!")
				.Dialogue(DialogueType.NonFatalOther, "Mhm, you want in too?")
				.Dialogue(DialogueType.NonFatalOther, "Ah, this lovely gut-slut here is just enjoying my stomach. How about you join them?")
				.Dialogue(DialogueType.NonFatalOther, "My delicious little meal here is quite safe, don't worry.")
				.Dialogue(DialogueType.PlayerTalisman, "You look so delicious with that talisman... Why not slide on into my stomach?")
				.Dialogue(DialogueType.PlayerTalisman, "Mhm, a mortal that knows its place in my belly --- come on, let's tuck you away.")
				.Dialogue(DialogueType.PlayerTalisman, "Oh, does a free meal come my way? Delicious...~")
				.Dialogue(DialogueType.PlayerAmulet, "Does the prey want to play predator? Oh, alright, {Pred}.")
				.Dialogue(DialogueType.PlayerAmulet, "You look hungry. I take it you want some thick demon thighs for dinner?")
				.Dialogue(DialogueType.PlayerAmulet, "Hah, you'll make a predator yet --- eat or be eaten, you know which one you want.")
				.Dialogue(DialogueType.Chat, "This is a fun party. Although normally we succubi eat people at parties, not cake.", d => d.Tags(DT.Party))
				.Dialogue(DialogueType.Chat, "Hey, did you know succubi parties involve eating the guests? Don’t be surprised if someone goes missing later.", d => d.Tags(DT.Party))
				.Dialogue(DialogueType.Chat, "This cake is okay, but I’d rather eat you.", d => d.Tags(DT.Party))
				.Dialogue(DialogueType.Chat, "Those are some pretty lanterns, I’ve gotta say. Celebrating your victories seems like a good thing to do - but why not celebrate your meals too?", d => d.Tags(DT.LanternNight))
				.Dialogue(DialogueType.Chat, "So, we’re just… tossing lanterns into the sky? You mortals have strange customs. Although you still taste great.", d => d.Tags(DT.LanternNight))
				.Dialogue(DialogueType.Chat, "Ugh, this weather is so wet! Why not come get out of the rain in my belly?", d => d.Tags(DT.Rain))
				.Dialogue(DialogueType.Chat, "Sandstorms are, like, the worst. I can’t even think of a good vore-related comment to make. They just suck.", d => d.Tags(DT.Sandstorm))
				.Dialogue(DialogueType.Chat, "Windy out. I might go try eat some of those slimes on their balloons.", d => d.Tags(DT.WindyDay))
				.Dialogue(DialogueType.Chat, "This weather is awful! You should come and keep safe in my stomach.", d => d.Tags(DT.Thunderstorm))
				.Dialogue(DialogueType.Chat, "Oooh, so many shooting stars. That’s almost as pretty as a full belly.", d => d.Tags(DT.MeteorShower))
				.Dialogue(DialogueType.Chat, "It’s a Blood Moon! So much prey!", d => d.Tags(DT.BloodMoon))
				.Dialogue(DialogueType.Chat, "There are a lot of zombies tonight. I hope they don’t give me indigestion.", d => d.Tags(DT.BloodMoon))
				.Dialogue(DialogueType.Chat, "Fear me lesser creature! I will devour your soul! Heh, just messing with you… But I’m still going to eat you if you don’t stop bothering me.", d => d.Tags(DT.BloodMoon))
				.Dialogue(DialogueType.Chat, "Blood Moons… One of those nights you just want to sleep someone away and pretend the world outside isn’t real.", d => d.Tags(DT.BloodMoon))
				.Dialogue(DialogueType.Chat, "Goblins aren’t all that nutritious, luckily there’s a lot of them.", d => d.Tags(DT.GoblinArmy))
				.Dialogue(DialogueType.Chat, "So many goblins. I’m going to enjoy eating so many.", d => d.Tags(DT.GoblinArmy))
				.Dialogue(DialogueType.Chat, "Goblins are dangerous, even if they’re pretty easy to swallow. Careful out there, {Prey}.", d => d.Tags(DT.GoblinArmy))
				.Dialogue(DialogueType.Chat, "It’s raining slimes! The surface world is so strange at times.", d => d.Tags(DT.SlimeRain))
				.Dialogue(DialogueType.Chat, "I heard slimes are made of digestive juices. So it’s kind of like it’s raining gelatinous stomachs right now. Rad!", d => d.Tags(DT.SlimeRain))
				.Dialogue(DialogueType.Chat, "All those slimes raining down are going to digest a lot of people. Try not to end up one of them.", d => d.Tags(DT.SlimeRain))
				.Dialogue(DialogueType.Chat, "Is this what that tavern keep keeps yapping on about? Just eat the damn army and be done with it.", d => d.Tags(DT.OldOnesArmy))
				.Dialogue(DialogueType.Chat, "No idea what that crystal is that that army is after, but hey, if it brings free food out, I’m not going to complain.", d => d.Tags(DT.OldOnesArmy))
				.Dialogue(DialogueType.Chat, "It’s a solar eclipse. That’s pretty. But the inside of my stomach is prettier.", d => d.Tags(DT.SolarEclipse))
				.Dialogue(DialogueType.Chat, "There are some strange things outside right now. I’m not sure even I want to eat those… Go on, be brave, have a taste.", d => d.Tags(DT.SolarEclipse))
				.Dialogue(DialogueType.Chat, "I think I might sit this one out. Tell me how the monsters taste!", d => d.Tags(DT.SolarEclipse))
				.Dialogue(DialogueType.Chat, "Pirates think they’re really something, huh? Let’s feast!", d => d.Tags(DT.PirateInvasion))
				.Dialogue(DialogueType.Chat, "A martian invasion?! Well, this should be a new taste to enjoy. Let’s go fill our bellies!", d => d.Tags(DT.MartianMadness))
				.Dialogue(DialogueType.Chat, "Y’know, the things around those pillars are really strange. Try not to get eaten out there, will you? I’d rather like to eat you myself once you finish with them.", d => d.Tags(DT.LunarEvent))
			);
	}
}