
namespace VoreMod
{
	public enum DialogueType
	{
		None,

		Chat, // Normal non-vore dialogue

		DigestedPlayer, // When the player dies due to being digested by an NPC

		PlayerAmulet, // When the player talks to the NPC while holding an amulet (looking to be pred)
		PlayerTalisman, // When the player talks to the NPC while holding a talisman (looking to be prey)

		PlayerVored, // When the player has swallowed the NPC
		PlayerDigesting, // When the NPC is being digested by the player
		PlayerNonFatal, // When the NPC is swallowed by but not being digested by the player

		VoredPlayer, // When the NPC has swallowed the player
		DigestingPlayer, // When the NPC is digesting the player
		NonFatalPlayer, // When the NPC has swallowed but is not digesting the player

		OtherVored, // When another mob has swallowed this NPC
		OtherDigesting, // When another mob is digesting this NPC
		OtherNonFatal, // When another mob has swallowed but is not digesting this NPC

		VoredOther, // When the NPC has swallowed something other than the player
		DigestingOther, // When the NPC is digesting something other than the player
		NonFatalOther, // When the NPC has swallowed but is not digesting something other than the player
	}
}
