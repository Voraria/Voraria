
using Terraria;
using Terraria.ID;
using static Terraria.ID.NPCID;

namespace VoreMod {
	public static class EntityTagLists
	{

		public static bool IsPlayer(int npcID)
		{

			return false;
		}

		public static bool IsTownNPC(int npcID)
		{

			switch(npcID)
			{
				case Merchant:
				case Nurse:
				case ArmsDealer:
				case Dryad:
				case Guide:
				case Clothier:
				case BoundGoblin:
				case BoundWizard:
				case GoblinTinkerer:
				case Wizard:
				case BoundMechanic:
				case Mechanic:
				case SantaClaus:
				case Truffle:
				case Steampunker:
				case DyeTrader:
				case PartyGirl:
				case Cyborg:
				case Painter:
				case WitchDoctor:
				case Pirate:
				case Stylist:
				case WebbedStylist:
				case TravellingMerchant:
				case SleepingAngler:
				case Angler:
				case TaxCollector:
				case SkeletonMerchant:
				case BartenderUnconscious:
				case Golfer:
				case GolferRescue:
				case BestiaryGirl:
				case Princess:
					return true;
			}

			return false;
		}

		public static bool IsMale(int npcID)
		{

			switch(npcID)
			{
				case BigRainZombie:
				case SmallRainZombie:
				case BigPantlessSkeleton:
				case SmallPantlessSkeleton:
				case BigMisassembledSkeleton:
				case SmallMisassembledSkeleton:
				case BigHeadacheSkeleton:
				case SmallHeadacheSkeleton:
				case BigSkeleton:
				case SmallSkeleton:
				case BigTwiggyZombie:
				case SmallTwiggyZombie:
				case BigSwampZombie:
				case SmallSwampZombie:
				case BigSlimedZombie:
				case SmallSlimedZombie:
				case BigPincushionZombie:
				case SmallPincushionZombie:
				case BigBaldZombie:
				case SmallBaldZombie:
				case BigZombie:
				case SmallZombie:
				case Zombie:
				case Merchant:
				case ArmsDealer:
				case Guide:
				case FireImp:
				case GoblinPeon:
				case GoblinThief:
				case GoblinWarrior:
				case OldMan:
				case Demolitionist:
				case UndeadMiner:
				case Tim:
				case KingSlime:
				case DoctorBones:
				case TheGroom:
				case Clothier:
				case GoblinScout:
				case Werewolf:
				case BoundGoblin:
				case BoundWizard:
				case GoblinTinkerer:
				case Wizard:
				case Clown:
				case GoblinArcher:
				case BaldZombie:
				case SantaClaus:
				case RedDevil:
				case Vampire:
				case Truffle:
				case ZombieEskimo:
				case Frankenstein:
				case UndeadViking:
				case RuneWizard:
				case ArmoredViking:
				case IcyMerman:
				case DyeTrader:
				case Cyborg:
				case PirateDeckhand:
				case PirateCorsair:
				case PirateDeadeye:
				case PirateCaptain:
				case ZombieRaincoat:
				case Painter:
				case WitchDoctor:
				case Pirate:
				case Paladin:
				case HeadlessHorseman:
				case ZombieDoctor:
				case ZombieSuperman:
				case ZombiePixie:
				case SkeletonTopHat:
				case SkeletonAstonaut :
				case SkeletonAlien:
				case ZombieSweater:
				case ZombieElf:
				case ZombieElfBeard:
				case Krampus:
				case TravellingMerchant:
				case Angler:
				case DukeFishron:
				case SleepingAngler:
				case ArmedZombie:
				case ArmedZombieEskimo:
				case ArmedZombiePincussion :
				case ArmedZombieSlimed:
				case ArmedZombieSwamp:
				case ArmedZombieTwiggy:
				case TaxCollector:
				case Butcher:
				case Fritz:
				case Nailhead :
				case Psycho:
				case DrManFly:
				case BloodZombie:
				case SolarSpearman:
				case DesertDjinn:
				case DemonTaxCollector:
				case DD2GoblinT1:
				case DD2GoblinT2:
				case DD2GoblinT3:
				case DD2GoblinBomberT1:
				case DD2GoblinBomberT2:
				case DD2GoblinBomberT3:
				case DD2OgreT2:
				case DD2OgreT3:
				case BartenderUnconscious:
				case 586:
				case 588:
				case 589:
				case 590:
				case 591:
					return true;
			}

			return false;
		}

		public static bool IsFemale(int npcID)
		{

			switch(npcID)
			{
				case BigFemaleZombie:
				case SmallFemaleZombie:
				case MotherSlime:
				case Nurse:
				case Dryad:
				case GoblinSorcerer:
				case Harpy:
				case BoundMechanic:
				case Mechanic:
				case Steampunker:
				case LostGirl:
				case Nymph:
				case FemaleZombie:
				case PartyGirl:
				case PirateCrossbower:
				case QueenBee:
				case ZombieXmas:
				case ZombieElfGirl:
				case IceQueen:
				case ElfArcher:
				case Stylist:
				case WebbedStylist:
				case VortexHornetQueen:
				case ArmedZombieCenx:
				case ThePossessed:
				case GoblinSummoner:
				case Medusa:
				case DesertLamiaLight:
				case DesertLamiaDark:
				case TheBride:
				case SandElemental:
				case DD2Betsy:
				case BestiaryGirl:
				case HallowBoss:
				case QueenSlimeBoss:
				case Princess:
					return true;
			}

			return false;
		}

		public static bool IsGenderless(int npcID)
		{

			switch(npcID)
			{
				case BigHornetStingy:
				case LittleHornetStingy:
				case BigHornetSpikey:
				case LittleHornetSpikey:
				case BigHornetLeafy:
				case LittleHornetLeafy:
				case BigHornetHoney:
				case LittleHornetHoney:
				case BigHornetFatty:
				case LittleHornetFatty:
				case DemonEye2:
				case PurpleEye2:
				case GreenEye2:
				case DialatedEye2:
				case SleepyEye2:
				case CataractEye2:
				case BigCrimslime:
				case LittleCrimslime:
				case BigCrimera:
				case LittleCrimera:
				case GiantMossHornet:
				case BigMossHornet:
				case LittleMossHornet:
				case TinyMossHornet:
				case BigStinger:
				case LittleStinger:
				case HeavySkeleton:
				case BigBoned:
				case ShortBones:
				case BigEater:
				case LittleEater:
				case JungleSlime:
				case YellowSlime:
				case RedSlime:
				case PurpleSlime:
				case BlackSlime:
				case BabySlime:
				case Pinky:
				case GreenSlime:
				case Slimer2:
				case Slimeling:
				case BlueSlime:
				case DemonEye:
				case EyeofCthulhu :
				case ServantofCthulhu:
				case EaterofSouls:
				case DevourerHead:
				case DevourerBody:
				case DevourerTail:
				case GiantWormHead:
				case GiantWormBody:
				case GiantWormTail:
				case EaterofWorldsHead :
				case EaterofWorldsBody:
				case EaterofWorldsTail:
				case Skeleton:
				case MeteorHead:
				case BurningSphere:
				case ChaosBall:
				case AngryBones:
				case DarkCaster:
				case WaterSphere:
				case CursedSkull:
				case SkeletronHead:
				case SkeletronHand:
				case BoneSerpentHead:
				case BoneSerpentBody:
				case BoneSerpentTail:
				case Hornet:
				case ManEater:
				case Bunny:
				case CorruptBunny:
				case CaveBat:
				case JungleBat:
				case Goldfish:
				case Snatcher:
				case CorruptGoldfish:
				case Piranha:
				case LavaSlime:
				case Hellbat:
				case Vulture:
				case Demon:
				case BlueJellyfish:
				case PinkJellyfish:
				case Shark:
				case VoodooDemon:
				case Crab:
				case DungeonGuardian:
				case Antlion:
				case SpikeBall:
				case DungeonSlime:
				case BlazingWheel:
				case Bird:
				case Pixie:
				case ArmoredSkeleton:
				case Mummy:
				case DarkMummy:
				case LightMummy:
				case CorruptSlime:
				case Wraith:
				case CursedHammer:
				case EnchantedSword:
				case Mimic:
				case Unicorn:
				case WyvernHead:
				case WyvernLegs:
				case WyvernBody:
				case WyvernBody2:
				case WyvernBody3:
				case WyvernTail:
				case GiantBat:
				case Corruptor:
				case DiggerHead:
				case DiggerBody:
				case DiggerTail:
				case SeekerHead:
				case SeekerBody:
				case SeekerTail:
				case Clinger:
				case AnglerFish:
				case GreenJellyfish:
				case SkeletonArcher:
				case VileSpit:
				case WallofFlesh:
				case WallofFleshEye:
				case TheHungry:
				case TheHungryII:
				case LeechHead:
				case LeechBody:
				case LeechTail:
				case ChaosElemental:
				case Slimer:
				case Gastropod:
				case Retinazer:
				case Spazmatism:
				case SkeletronPrime:
				case PrimeCannon:
				case PrimeSaw:
				case PrimeVice:
				case PrimeLaser:
				case WanderingEye:
				case TheDestroyer:
				case TheDestroyerBody:
				case TheDestroyerTail:
				case IlluminantBat:
				case IlluminantSlime:
				case Probe:
				case PossessedArmor:
				case ToxicSludge:
				case SnowmanGangsta:
				case MisterStabby:
				case SnowBalla:
				case IceSlime:
				case Penguin:
				case PenguinBlack:
				case IceBat:
				case Lavabat:
				case GiantFlyingFox:
				case GiantTortoise:
				case IceTortoise:
				case Wolf:
				case Arapaima:
				case VampireBat:
				case BlackRecluse:
				case WallCreeper:
				case WallCreeperWall:
				case SwampThing:
				case CorruptPenguin:
				case IceElemental:
				case PigronCorruption:
				case PigronHallow:
				case Crimera:
				case Herpling:
				case AngryTrapper:
				case MossHornet:
				case Derpling:
				case CrimsonAxe:
				case PigronCrimson:
				case FaceMonster:
				case FloatyGross:
				case Crimslime:
				case SpikedIceSlime:
				case SnowFlinx:
				case PincushionZombie:
				case SlimedZombie:
				case SwampZombie:
				case TwiggyZombie:
				case CataractEye:
				case SleepyEye:
				case DialatedEye:
				case GreenEye:
				case PurpleEye:
				case Lihzahrd:
				case LihzahrdCrawler:
				case HeadacheSkeleton:
				case MisassembledSkeleton:
				case PantlessSkeleton:
				case SpikedJungleSlime:
				case Moth:
				case Bee:
				case BeeSmall:
				case CochinealBeetle :
				case CyanBeetle:
				case LacBeetle:
				case SeaSnail:
				case Squid:
				case FlyingFish:
				case UmbrellaSlime:
				case FlyingSnake:
				case GoldfishWalker:
				case HornetFatty:
				case HornetHoney:
				case HornetLeafy:
				case HornetSpikey:
				case HornetStingy:
				case JungleCreeper:
				case JungleCreeperWall:
				case BlackRecluseWall:
				case BloodCrawler:
				case BloodCrawlerWall:
				case BloodFeeder:
				case BloodJelly:
				case IceGolem:
				case RainbowSlime:
				case Golem:
				case GolemHead:
				case GolemFistLeft:
				case GolemFistRight:
				case GolemHeadFree:
				case AngryNimbus:
				case Eyezor:
				case Parrot:
				case Reaper:
				case ZombieMushroom:
				case ZombieMushroomHat:
				case FungoFish:
				case AnomuraFungus:
				case MushiLadybug:
				case FungiBulb:
				case GiantFungiBulb:
				case FungiSpore:
				case Plantera:
				case PlanterasHook:
				case PlanterasTentacle:
				case Spore:
				case BrainofCthulhu:
				case Creeper:
				case IchorSticker:
				case RustyArmoredBonesAxe:
				case RustyArmoredBonesFlail:
				case RustyArmoredBonesSword:
				case RustyArmoredBonesSwordNoArmor:
				case BlueArmoredBones:
				case BlueArmoredBonesMace:
				case BlueArmoredBonesNoPants:
				case BlueArmoredBonesSword:
				case HellArmoredBones:
				case HellArmoredBonesSpikeShield:
				case HellArmoredBonesMace:
				case HellArmoredBonesSword:
				case RaggedCaster:
				case RaggedCasterOpenCoat:
				case Necromancer:
				case NecromancerArmored:
				case DiabolistRed:
				case DiabolistWhite:
				case BoneLee:
				case DungeonSpirit:
				case GiantCursedSkull:
				case SkeletonSniper:
				case TacticalSkeleton:
				case SkeletonCommando:
				case AngryBonesBig:
				case AngryBonesBigMuscle:
				case AngryBonesBigHelmet:
				case BirdBlue:
				case BirdRed:
				case Squirrel:
				case Mouse:
				case Raven:
				case SlimeMasked:
				case BunnySlimed:
				case HoppinJack:
				case Scarecrow1:
				case Scarecrow2:
				case Scarecrow3:
				case Scarecrow4:
				case Scarecrow5:
				case Scarecrow6:
				case Scarecrow7:
				case Scarecrow8:
				case Scarecrow9:
				case Scarecrow10:
				case Ghost:
				case DemonEyeOwl:
				case DemonEyeSpaceship:
				case MourningWood:
				case Splinterling:
				case Pumpking:
				case PumpkingBlade:
				case Hellhound:
				case Poltergeist:
				case SlimeRibbonWhite:
				case SlimeRibbonYellow:
				case SlimeRibbonGreen:
				case SlimeRibbonRed:
				case BunnyXmas:
				case PresentMimic:
				case GingerbreadMan:
				case Yeti:
				case Everscream:
				case SantaNK1:
				case ElfCopter:
				case Nutcracker:
				case NutcrackerSpinning:
				case Flocko:
				case Firefly:
				case Butterfly:
				case Worm:
				case LightningBug:
				case Snail:
				case GlowingSnail:
				case Frog:
				case Duck:
				case Duck2:
				case DuckWhite:
				case DuckWhite2:
				case ScorpionBlack:
				case Scorpion:
				case DetonatingBubble:
				case Sharkron:
				case Sharkron2:
				case TruffleWorm:
				case TruffleWormDigger:
				case Grasshopper:
				case ChatteringTeethBomb:
				case CultistArcherBlue:
				case CultistArcherWhite:
				case BrainScrambler:
				case RayGunner:
				case MartianOfficer:
				case ForceBubble:
				case GrayGrunt:
				case MartianEngineer:
				case MartianTurret:
				case MartianDrone:
				case GigaZapper:
				case ScutlixRider:
				case Scutlix:
				case MartianSaucer:
				case MartianSaucerTurret:
				case MartianSaucerCannon:
				case MartianSaucerCore:
				case MoonLordHead:
				case MoonLordHand:
				case MoonLordCore:
				case MartianProbe:
				case MoonLordFreeEye:
				case MoonLordLeechBlob:
				case StardustWormHead:
				case StardustWormBody:
				case StardustWormTail:
				case StardustCellBig:
				case StardustCellSmall:
				case StardustJellyfishBig:
				case StardustSpiderBig:
				case StardustSpiderSmall:
				case StardustSoldier:
				case SolarCrawltipedeHead:
				case SolarCrawltipedeBody:
				case SolarCrawltipedeTail:
				case SolarDrakomire:
				case SolarDrakomireRider:
				case SolarSroller:
				case SolarCorite:
				case SolarSolenian:
				case NebulaBrain:
				case NebulaHeadcrab:
				case LunarTowerVortex:
				case NebulaBeast:
				case NebulaSoldier:
				case VortexRifleman:
				case VortexHornet:
				case VortexLarva:
				case VortexSoldier:
				case CultistTablet:
				case CultistDevote:
				case CultistBoss:
				case CultistBossClone:
				case GoldBird:
				case GoldBunny:
				case GoldButterfly:
				case GoldFrog:
				case GoldGrasshopper:
				case GoldMouse:
				case GoldWorm:
				case BoneThrowingSkeleton:
				case BoneThrowingSkeleton2:
				case BoneThrowingSkeleton3:
				case BoneThrowingSkeleton4:
				case SkeletonMerchant:
				case CultistDragonHead:
				case CultistDragonBody1:
				case CultistDragonBody2:
				case CultistDragonBody3:
				case CultistDragonBody4:
				case CultistDragonTail:
				case CreatureFromTheDeep:
				case CrimsonBunny:
				case CrimsonGoldfish:
				case DeadlySphere:
				case CrimsonPenguin:
				case ShadowFlameApparition:
				case BigMimicCorruption:
				case BigMimicCrimson:
				case BigMimicHallow:
				case BigMimicJungle:
				case Mothron:
				case MothronEgg:
				case MothronSpawn:
				case GreekSkeleton:
				case GraniteGolem:
				case GraniteFlyer:
				case EnchantedNightcrawler:
				case Grubby:
				case Sluggy:
				case Buggy:
				case TargetDummy:
				case Drippler:
				case PirateShip:
				case PirateShipCannon:
				case LunarTowerStardust:
				case Crawdad:
				case Crawdad2:
				case GiantShelly:
				case GiantShelly2:
				case Salamander:
				case Salamander2:
				case Salamander3:
				case Salamander4:
				case Salamander5:
				case Salamander6:
				case Salamander7:
				case Salamander8:
				case Salamander9:
				case LunarTowerNebula:
				case WalkingAntlion:
				case FlyingAntlion:
				case DuneSplicerHead:
				case DuneSplicerBody:
				case DuneSplicerTail:
				case TombCrawlerHead:
				case TombCrawlerBody:
				case TombCrawlerTail:
				case SolarFlare:
				case LunarTowerSolar:
				case SolarGoop:
				case MartianWalker:
				case AncientCultistSquidhead:
				case AncientLight:
				case AncientDoom:
				case DesertGhoul:
				case DesertGhoulCorruption:
				case DesertGhoulCrimson:
				case DesertGhoulHallow:
				case DesertScorpionWalk:
				case DesertScorpionWall:
				case DesertBeast:
				case SlimeSpiked:
				case SandSlime:
				case SquirrelRed:
				case SquirrelGold:
				case PartyBunny:
				case SandShark:
				case SandsharkCorrupt:
				case SandsharkCrimson:
				case SandsharkHallow:
				case Tumbleweed:
				case DD2EterniaCrystal:
				case DD2LanePortal:
				case DD2Bartender:
				case DD2WyvernT1:
				case DD2WyvernT2:
				case DD2WyvernT3:
				case DD2JavelinstT1:
				case DD2JavelinstT2:
				case DD2JavelinstT3:
				case DD2DarkMageT1:
				case DD2DarkMageT3:
				case DD2SkeletonT1:
				case DD2SkeletonT3:
				case DD2WitherBeastT2:
				case DD2WitherBeastT3:
				case DD2DrakinT2:
				case DD2DrakinT3:
				case DD2KoboldWalkerT2:
				case DD2KoboldWalkerT3:
				case DD2KoboldFlyerT2:
				case DD2KoboldFlyerT3:
				case DD2LightningBugT3:
				case GiantFlyingAntlion:
				case GiantWalkingAntlion:
				case 582:
				case 583:
				case 584:
				case 585:
				case 587:
				case 592:
				case 593:
				case 594:
				case 595:
				case 596:
				case 597:
				case 598:
				case 599:
				case 600:
				case 601:
				case 602:
				case 603:
				case 604:
				case 605:
				case 606:
				case 607:
				case 608:
				case 609:
				case 610:
				case 611:
				case 612:
				case 613:
				case 614:
				case 615:
				case 616:
				case 617:
				case 618:
				case 619:
				case 620:
				case 621:
				case 622:
				case 623:
				case 624:
				case 625:
				case 626:
				case 627:
				case 628:
				case 629:
				case 630:
				case 631:
				case 632:
				case 634:
				case 635:
				case 637:
				case 638:
				case 639:
				case 640:
				case 641:
				case 642:
				case 643:
				case 644:
				case 645:
				case 646:
				case 647:
				case 648:
				case 649:
				case 650:
				case 651:
				case 652:
				case 653:
				case 654:
				case 655:
				case 656:
				case 658:
				case 659:
				case 660:
				case 661:
				case 662:
					return true;
			}

			return false;
		}

		public static bool IsBats(int npcID)
		{

			switch(npcID)
			{
				case CaveBat:
				case JungleBat:
				case Hellbat:
				case GiantBat:
				case IlluminantBat:
				case IceBat:
				case Lavabat:
				case VampireBat:
				case 583:
				case 584:
				case 585:
				case 634:
					return true;
			}

			return false;
		}

		public static bool IsHornet(int npcID)
		{

			switch(npcID)
			{
				case BigHornetStingy:
				case LittleHornetStingy:
				case BigHornetSpikey:
				case LittleHornetSpikey:
				case BigHornetLeafy:
				case LittleHornetLeafy:
				case BigHornetHoney:
				case LittleHornetHoney:
				case BigHornetFatty:
				case LittleHornetFatty:
				case GiantMossHornet:
				case BigMossHornet:
				case LittleMossHornet:
				case TinyMossHornet:
				case BigStinger:
				case LittleStinger:
				case Hornet:
				case MossHornet:
				case Bee:
				case BeeSmall:
				case QueenBee:
				case HornetFatty:
				case HornetHoney:
				case HornetLeafy:
				case HornetSpikey:
				case HornetStingy:
				case VortexHornetQueen:
				case VortexHornet:
				case VortexLarva:
					return true;
			}

			return false;
		}

		public static bool IsZombie(int npcID)
		{

			switch(npcID)
			{
				case BigRainZombie:
				case SmallRainZombie:
				case BigFemaleZombie:
				case SmallFemaleZombie:
				case BigTwiggyZombie:
				case SmallTwiggyZombie:
				case BigSwampZombie:
				case SmallSwampZombie:
				case BigSlimedZombie:
				case SmallSlimedZombie:
				case BigPincushionZombie:
				case SmallPincushionZombie:
				case BigBaldZombie:
				case SmallBaldZombie:
				case BigZombie:
				case SmallZombie:
				case Zombie:
				case TheGroom:
				case BaldZombie:
				case ZombieEskimo:
				case Frankenstein:
				case PincushionZombie:
				case SlimedZombie:
				case SwampZombie:
				case TwiggyZombie:
				case FemaleZombie:
				case ZombieRaincoat:
				case ZombieMushroom:
				case ZombieMushroomHat:
				case ZombieDoctor:
				case ZombieSuperman:
				case ZombiePixie:
				case ZombieXmas:
				case ZombieSweater:
				case ZombieElf:
				case ZombieElfBeard:
				case ZombieElfGirl:
				case ArmedZombie:
				case ArmedZombieEskimo:
				case ArmedZombiePincussion :
				case ArmedZombieSlimed:
				case ArmedZombieSwamp:
				case ArmedZombieTwiggy:
				case ArmedZombieCenx:
				case BloodZombie:
				case TheBride:
				case 586:
				case 590:
				case 591:
				case 632:
					return true;
			}

			return false;
		}

		public static bool IsSkeleton(int npcID)
		{

			switch(npcID)
			{
				case BigPantlessSkeleton:
				case SmallPantlessSkeleton:
				case BigMisassembledSkeleton:
				case SmallMisassembledSkeleton:
				case BigHeadacheSkeleton:
				case SmallHeadacheSkeleton:
				case BigSkeleton:
				case SmallSkeleton:
				case HeavySkeleton:
				case BigBoned:
				case ShortBones:
				case Skeleton:
				case AngryBones:
				case DarkCaster:
				case CursedSkull:
				case SkeletronHead:
				case SkeletronHand:
				case BoneSerpentHead:
				case BoneSerpentBody:
				case BoneSerpentTail:
				case DoctorBones:
				case ArmoredSkeleton:
				case SkeletonArcher:
				case HeadacheSkeleton:
				case MisassembledSkeleton:
				case PantlessSkeleton:
				case RustyArmoredBonesAxe:
				case RustyArmoredBonesFlail:
				case RustyArmoredBonesSword:
				case RustyArmoredBonesSwordNoArmor:
				case BlueArmoredBones:
				case BlueArmoredBonesMace:
				case BlueArmoredBonesNoPants:
				case BlueArmoredBonesSword:
				case HellArmoredBones:
				case HellArmoredBonesSpikeShield:
				case HellArmoredBonesMace:
				case HellArmoredBonesSword:
				case BoneLee:
				case GiantCursedSkull:
				case SkeletonSniper:
				case TacticalSkeleton:
				case SkeletonCommando:
				case AngryBonesBig:
				case AngryBonesBigMuscle:
				case AngryBonesBigHelmet:
				case SkeletonTopHat:
				case SkeletonAstonaut :
				case SkeletonAlien:
				case BoneThrowingSkeleton:
				case BoneThrowingSkeleton2:
				case BoneThrowingSkeleton3:
				case BoneThrowingSkeleton4:
				case SkeletonMerchant:
				case GreekSkeleton:
				case DD2SkeletonT1:
				case DD2SkeletonT3:
				case 635:
					return true;
			}

			return false;
		}

		public static bool IsUndead(int npcID)
		{

			switch(npcID)
			{
				case CursedSkull:
				case UndeadMiner:
				case Tim:
				case DungeonGuardian:
				case Mummy:
				case DarkMummy:
				case LightMummy:
				case Wraith:
				case Clown:
				case PossessedArmor:
				case Vampire:
				case SwampThing:
				case UndeadViking:
				case RuneWizard:
				case ArmoredViking:
				case Reaper:
				case RaggedCaster:
				case RaggedCasterOpenCoat:
				case Necromancer:
				case NecromancerArmored:
				case DiabolistRed:
				case DiabolistWhite:
				case DungeonSpirit:
				case HeadlessHorseman:
				case Ghost:
				case Poltergeist:
				case Butcher:
				case CreatureFromTheDeep:
				case Fritz:
				case Nailhead :
				case Psycho:
				case DrManFly:
				case ThePossessed:
				case DesertGhoul:
				case DesertGhoulCorruption:
				case DesertGhoulCrimson:
				case DesertGhoulHallow:
				case DemonTaxCollector:
				case 630:
				case 662:
					return true;
			}

			return false;
		}

		public static bool IsWorm(int npcID)
		{

			switch(npcID)
			{
				case DevourerHead:
				case DevourerBody:
				case DevourerTail:
				case GiantWormHead:
				case GiantWormBody:
				case GiantWormTail:
				case EaterofWorldsHead:
				case EaterofWorldsBody:
				case EaterofWorldsTail:
				case BoneSerpentHead:
				case BoneSerpentBody:
				case BoneSerpentTail:
				case DiggerHead:
				case DiggerBody:
				case DiggerTail:
				case SeekerHead:
				case SeekerBody:
				case SeekerTail:
				case LeechHead:
				case LeechBody:
				case LeechTail:
				case StardustWormHead:
				case StardustWormBody:
				case StardustWormTail:
				case SolarCrawltipedeHead:
				case SolarCrawltipedeBody:
				case SolarCrawltipedeTail:
				case DuneSplicerHead:
				case DuneSplicerBody:
				case DuneSplicerTail:
				case TombCrawlerHead:
				case TombCrawlerBody:
				case TombCrawlerTail:
					return true;
			}

			return false;
		}

		public static bool IsBeast(int npcID)
		{

			switch(npcID)
			{
				case Unicorn:
				case Werewolf:
				case GiantFlyingFox:
				case Wolf:
				case CorruptPenguin:
				case PigronCorruption:
				case PigronHallow:
				case PigronCrimson:
				case SnowFlinx:
				case Hellhound:
				case Yeti:
				case Krampus:
				case Scutlix:
				case SolarDrakomire:
				case NebulaBeast:
				case CrimsonBunny:
				case CrimsonGoldfish:
				case CrimsonPenguin:
				case Medusa:
				case DesertBeast:
				case DD2Betsy:
				case DD2WitherBeastT2:
				case DD2WitherBeastT3:
				case DD2OgreT2:
				case DD2OgreT3:
				case 620:
				case 633:
					return true;
			}

			return false;
		}

		public static bool IsReptile(int npcID)
		{

			switch(npcID)
			{
				case WyvernHead:
				case WyvernLegs:
				case WyvernBody:
				case WyvernBody2:
				case WyvernBody3:
				case WyvernTail:
				case GiantTortoise:
				case IceTortoise:
				case Lihzahrd:
				case LihzahrdCrawler:
				case FlyingSnake:
				case CultistDragonHead:
				case CultistDragonBody1:
				case CultistDragonBody2:
				case CultistDragonBody3:
				case CultistDragonBody4:
				case CultistDragonTail:
				case Salamander:
				case Salamander2:
				case Salamander3:
				case Salamander4:
				case Salamander5:
				case Salamander6:
				case Salamander7:
				case Salamander8:
				case Salamander9:
				case DesertLamiaLight:
				case DesertLamiaDark:
				case DD2Betsy:
				case DD2WyvernT1:
				case DD2WyvernT2:
				case DD2WyvernT3:
				case DD2DrakinT2:
				case DD2DrakinT3:
				case 621:
				case 622:
				case 623:
					return true;
			}

			return false;
		}

		public static bool IsSpider(int npcID)
		{

			switch(npcID)
			{
				case BlackRecluse:
				case WallCreeper:
				case WallCreeperWall:
				case JungleCreeper:
				case JungleCreeperWall:
				case BlackRecluseWall:
				case BloodCrawler:
				case BloodCrawlerWall:
					return true;
			}

			return false;
		}

		public static bool IsEye(int npcID)
		{

			switch(npcID)
			{
				case DemonEye2:
				case PurpleEye2:
				case GreenEye2:
				case DialatedEye2:
				case SleepyEye2:
				case CataractEye2:
				case DemonEye:
				case EyeofCthulhu :
				case ServantofCthulhu:
				case Demon:
				case WallofFleshEye:
				case TheHungry:
				case TheHungryII:
				case Gastropod:
				case Retinazer:
				case Spazmatism:
				case WanderingEye:
				case CataractEye:
				case SleepyEye:
				case DialatedEye:
				case GreenEye:
				case PurpleEye:
				case Eyezor:
				case Creeper:
				case DemonEyeOwl:
				case DemonEyeSpaceship:
				case MoonLordFreeEye:
				case NebulaBrain:
				case NebulaHeadcrab:
				case Drippler:
				case 587:
					return true;
			}

			return false;
		}

		public static bool IsSlime(int npcID)
		{

			switch(npcID)
			{
				case BigCrimslime:
				case LittleCrimslime:
				case JungleSlime:
				case YellowSlime:
				case RedSlime:
				case PurpleSlime:
				case BlackSlime:
				case BabySlime:
				case Pinky:
				case GreenSlime:
				case Slimer2:
				case Slimeling:
				case BlueSlime:
				case MotherSlime:
				case KingSlime:
				case LavaSlime:
				case DungeonSlime:
				case CorruptSlime:
				case Slimer:
				case IlluminantSlime:
				case ToxicSludge:
				case IceSlime:
				case Herpling:
				case Crimslime:
				case SpikedIceSlime:
				case SpikedJungleSlime:
				case UmbrellaSlime:
				case RainbowSlime:
				case SlimeMasked:
				case BunnySlimed:
				case HoppinJack:
				case SlimeRibbonWhite:
				case SlimeRibbonYellow:
				case SlimeRibbonGreen:
				case SlimeRibbonRed:
				case SlimeSpiked:
				case SandSlime:
				case QueenSlimeBoss:
				case QueenSlimeMinionBlue:
				case QueenSlimeMinionPink:
				case QueenSlimeMinionPurple:
					return true;
			}

			return false;
		}

		public static bool IsConstruct(int npcID)
		{

			switch(npcID)
			{
				case MeteorHead:
				case BurningSphere:
				case BlazingWheel:
				case CursedHammer:
				case EnchantedSword:
				case ChaosElemental:
				case Retinazer:
				case Spazmatism:
				case SkeletronPrime:
				case PrimeCannon:
				case PrimeSaw:
				case PrimeVice:
				case PrimeLaser:
				case TheDestroyer:
				case TheDestroyerBody:
				case TheDestroyerTail:
				case Probe:
				case SnowmanGangsta:
				case MisterStabby:
				case SnowBalla:
				case IceElemental:
				case CrimsonAxe:
				case IceGolem:
				case Golem:
				case GolemHead:
				case GolemFistLeft:
				case GolemFistRight:
				case GolemHeadFree:
				case AngryNimbus:
				case Scarecrow1:
				case Scarecrow2:
				case Scarecrow3:
				case Scarecrow4:
				case Scarecrow5:
				case Scarecrow6:
				case Scarecrow7:
				case Scarecrow8:
				case Scarecrow9:
				case Scarecrow10:
				case GingerbreadMan:
				case IceQueen:
				case SantaNK1:
				case ElfCopter:
				case Nutcracker:
				case NutcrackerSpinning:
				case Flocko:
				case ChatteringTeethBomb:
				case MartianTurret:
				case MartianDrone:
				case MartianSaucer:
				case MartianSaucerTurret:
				case MartianSaucerCannon:
				case MartianSaucerCore:
				case MartianProbe:
				case SolarSroller:
				case SolarCorite:
				case CultistTablet:
				case DeadlySphere:
				case GraniteGolem:
				case GraniteFlyer:
				case TargetDummy:
				case PirateShip:
				case PirateShipCannon:
				case MartianWalker:
				case AncientCultistSquidhead:
				case SandElemental:
				case Tumbleweed:
				case DD2EterniaCrystal:
				case DD2LanePortal:
				case 594:
				case 631:
					return true;
			}

			return false;
		}

		public static bool IsPlant(int npcID)
		{

			switch(npcID)
			{
				case ManEater:
				case Snatcher:
				case Clinger:
				case AngryTrapper:
				case AnomuraFungus:
				case FungiBulb:
				case GiantFungiBulb:
				case FungiSpore:
				case Plantera:
				case PlanterasHook:
				case PlanterasTentacle:
				case Spore:
				case HoppinJack:
				case MourningWood:
				case Splinterling:
				case Pumpking:
				case PumpkingBlade:
				case Everscream:
					return true;
			}

			return false;
		}

		public static bool IsAmbient(int npcID)
		{

			switch(npcID)
			{
				case Bunny:
				case CorruptBunny:
				case Goldfish:
				case CorruptGoldfish:
				case Crab:
				case Bird:
				case Penguin:
				case PenguinBlack:
				case GoldfishWalker:
				case BirdBlue:
				case BirdRed:
				case Squirrel:
				case Mouse:
				case Raven:
				case BunnyXmas:
				case Firefly:
				case Butterfly:
				case Worm:
				case LightningBug:
				case Snail:
				case GlowingSnail:
				case Frog:
				case Duck:
				case Duck2:
				case DuckWhite:
				case DuckWhite2:
				case ScorpionBlack:
				case Scorpion:
				case TruffleWorm:
				case TruffleWormDigger:
				case Grasshopper:
				case GoldBird:
				case GoldBunny:
				case GoldButterfly:
				case GoldFrog:
				case GoldGrasshopper:
				case GoldMouse:
				case GoldWorm:
				case EnchantedNightcrawler:
				case Grubby:
				case Sluggy:
				case Buggy:
				case SquirrelRed:
				case SquirrelGold:
				case PartyBunny:
				case 595:
				case 596:
				case 597:
				case 598:
				case 599:
				case 600:
				case 601:
				case 602:
				case 603:
				case 604:
				case 605:
				case 606:
				case 607:
				case 608:
				case 609:
				case 610:
				case 611:
				case 612:
				case 613:
				case 614:
				case 615:
				case 616:
				case 617:
				case 624:
				case 625:
				case 626:
				case 627:
				case 628:
				case 637:
				case 638:
				case 639:
				case 640:
				case 641:
				case 642:
				case 643:
				case 644:
				case 645:
				case 646:
				case 647:
				case 648:
				case 649:
				case 650:
				case 651:
				case 652:
				case 653:
				case 654:
				case 655:
				case 656:
				case 661:
					return true;
			}

			return false;
		}

		public static bool IsFish(int npcID)
		{

			switch(npcID)
			{
				case Goldfish:
				case Piranha:
				case BlueJellyfish:
				case PinkJellyfish:
				case Shark:
				case AnglerFish:
				case GreenJellyfish:
				case Arapaima:
				case IcyMerman:
				case SeaSnail:
				case Squid:
				case FlyingFish:
				case GoldfishWalker:
				case BloodFeeder:
				case BloodJelly:
				case FungoFish:
				case DukeFishron:
				case Sharkron:
				case Sharkron2:
				case SandShark:
				case SandsharkCorrupt:
				case SandsharkCrimson:
				case SandsharkHallow:
				case 586:
				case 592:
				case 593:
				case 615:
				case 616:
				case 617:
				case 618:
				case 619:
				case 620:
				case 625:
				case 626:
				case 627:
					return true;
			}

			return false;
		}

		public static bool IsInsect(int npcID)
		{

			switch(npcID)
			{
				case Antlion:
				case BlackRecluse:
				case WallCreeper:
				case WallCreeperWall:
				case Derpling:
				case Moth:
				case CochinealBeetle :
				case CyanBeetle:
				case LacBeetle:
				case SeaSnail:
				case JungleCreeper:
				case JungleCreeperWall:
				case BlackRecluseWall:
				case BloodCrawler:
				case BloodCrawlerWall:
				case MushiLadybug:
				case IchorSticker:
				case Mothron:
				case MothronEgg:
				case MothronSpawn:
				case Crawdad:
				case Crawdad2:
				case GiantShelly:
				case GiantShelly2:
				case WalkingAntlion:
				case FlyingAntlion:
				case DesertScorpionWalk:
				case DesertScorpionWall :
				case DD2LightningBugT3:
				case GiantWalkingAntlion:
				case GiantFlyingAntlion:
				case 582:
					return true;
			}

			return false;
		}

		public static bool IsFlying(int npcID)
		{

			switch(npcID)
			{
				case Harpy:
				case Vulture:
				case Bird:
				case Pixie:
				case WyvernHead:
				case WyvernLegs:
				case WyvernBody:
				case WyvernBody2:
				case WyvernBody3:
				case WyvernTail:
				case GiantFlyingFox:
				case QueenBee:
				case FlyingFish:
				case FlyingSnake:
				case Parrot:
				case DukeFishron:
				case Sharkron:
				case Sharkron2:
				case MartianProbe:
				case StardustCellBig:
				case StardustCellSmall:
				case StardustJellyfishBig:
				case StardustSpiderBig:
				case CultistDragonHead:
				case CultistDragonBody1:
				case CultistDragonBody2:
				case CultistDragonBody3:
				case CultistDragonBody4:
				case CultistDragonTail:
				case Mothron:
				case MothronSpawn:
				case FlyingAntlion:
				case DD2Betsy:
				case DD2WyvernT1:
				case DD2WyvernT2:
				case DD2WyvernT3:
				case DD2KoboldFlyerT2:
				case DD2KoboldFlyerT3:
				case DD2LightningBugT3:
				case GiantFlyingAntlion:
				case 587:
				case 636:
					return true;
			}

			return false;
		}

		public static bool IsMimic(int npcID)
		{

			switch(npcID)
			{
				case Mimic:
				case PresentMimic:
				case BigMimicCorruption:
				case BigMimicCrimson:
				case BigMimicHallow:
				case BigMimicJungle:
				case 629:
					return true;
			}

			return false;
		}

		public static bool IsProjectile(int npcID)
		{

			switch(npcID)
			{
				case BurningSphere:
				case ChaosBall:
				case WaterSphere:
				case SpikeBall:
				case VileSpit:
				case FungiSpore:
				case Spore:
				case DetonatingBubble:
				case MoonLordLeechBlob:
				case StardustSpiderSmall:
				case DeadlySphere:
				case ShadowFlameApparition:
				case SolarFlare:
				case SolarGoop:
				case AncientLight:
				case AncientDoom:
					return true;
			}

			return false;
		}

		public static bool IsBoss(int npcID)
		{

			switch(npcID)
			{
				case EyeofCthulhu:
				case EaterofWorldsHead:
				case EaterofWorldsBody:
				case EaterofWorldsTail:
				case SkeletronHead:
				case SkeletronHand:
				case KingSlime:
				case DungeonGuardian:
				case WallofFlesh:
				case WallofFleshEye:
				case TheHungry:
				case TheHungryII:
				case Retinazer:
				case Spazmatism:
				case SkeletronPrime:
				case PrimeCannon:
				case PrimeSaw:
				case PrimeVice:
				case PrimeLaser:
				case TheDestroyer:
				case TheDestroyerBody:
				case TheDestroyerTail:
				case Probe:
				case QueenBee:
				case Golem:
				case GolemHead:
				case GolemFistLeft:
				case GolemFistRight:
				case GolemHeadFree:
				case Plantera:
				case PlanterasHook:
				case PlanterasTentacle:
				case Spore:
				case BrainofCthulhu:
				case Creeper:
				case MourningWood:
				case Splinterling:
				case Pumpking:
				case PumpkingBlade:
				case Everscream:
				case IceQueen:
				case SantaNK1:
				case DukeFishron:
				case MartianSaucer:
				case MartianSaucerTurret:
				case MartianSaucerCannon:
				case MartianSaucerCore:
				case MoonLordHead:
				case MoonLordHand:
				case MoonLordCore:
				case MoonLordFreeEye:
				case LunarTowerVortex:
				case Mothron:
				case LunarTowerStardust:
				case LunarTowerNebula:
				case LunarTowerSolar:
				case 618:
				case 636:
				case 657:
					return true;
			}

			return false;
		}
	}
}
