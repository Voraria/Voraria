
namespace VoreMod
{
	[System.Flags]
	public enum DialogueTags
	{
		None = 0,

		MalePlayer = 1 << 0,
		FemalePlayer = 1 << 1,

		Morning = 1 << 2,
		Day = 1 << 3,
		Evening = 1 << 4,
		Night = 1 << 5,

		BloodMoon = 1 << 6,
		Party = 1 << 7,
		LanternNight = 1 << 8,
		GoblinArmy = 1 << 9,
		SlimeRain = 1 << 10,
		OldOnesArmy = 1 << 11,
		FrostLegion = 1 << 12,
		SolarEclipse = 1 << 13,
		PirateInvasion = 1 << 14,
		PumpkinMoon = 1 << 15,
		FrostMoon = 1 << 16,
		MartianMadness = 1 << 17,
		LunarEvent = 1 << 18,

		WindyDay = 1 << 19,
		Rain = 1 << 20,
		Sandstorm = 1 << 21,
		Thunderstorm = 1 << 22,
		MeteorShower = 1 << 23,

		ArmsDealerPresent = 1 << 24,
		ClothierPresent = 1 << 25,
		CyborgPresent = 1 << 26,
		DemolitionistPresent = 1 << 27,
		DryadPresent = 1 << 28,
		DyeTraderPresent = 1 << 29,
		GoblinTinkererPresent = 1 << 30,
		GolferPresent = 1 << 31,
		GuidePresent = 1 << 32,
		MechanicPresent = 1 << 33,
		NursePresent = 1 << 34,
		PainterPresent = 1 << 35,
		PartyGirlPresent = 1 << 36,
		PiratePresent = 1 << 37,
		SantaPresent = 1 << 38,
		SteampunkerPresent = 1 << 39,
		StylistPresent = 1 << 40,
		TavernkeepPresent = 1 << 41,
		TaxCollectorPresent = 1 << 42,
		TravellingMerchantPresent = 1 << 43,
		TrufflePresent = 1 << 44,
		WitchDoctorPresent = 1 << 45,
		WizardPresent = 1 << 46,
		ZoologistPresent = 1 << 47,
	}
}
