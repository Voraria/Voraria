
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
        LanternNight = 1 << 8, // Not implemented yet; waiting for 1.4
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

        WindyDay = 1 << 19, // Not implemented yet; waiting for 1.4
        Rain = 1 << 20,
        Sandstorm = 1 << 21,
        Thunderstorm = 1 << 22,
        MeteorShower = 1 << 23, // Not implemented yet; waiting for 1.4
    }
}
