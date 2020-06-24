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
					.Layout(SpriteLayout.SizeY).Frames(6).Offset(4f, 8f).FrameOffset(0f, -2f, 3, 5).FrameOffset(0f, -2f, 9, 11))
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
			);
	}
}