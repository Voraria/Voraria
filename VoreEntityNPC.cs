using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using VoreMod.NPCs;
using System.Linq;
using System.Collections.Generic;
using Terraria.Audio;

namespace VoreMod
{
    public class VoreEntityNPC : VoreEntity
    {
        NPC npc;
        StateNPC npcBackup;

        public VoreEntityNPC(NPC npc)
        {
            this.npc = npc;
        }

        public NPC GetNPC() => npc;

        public override void OnSwallowedBy(VoreEntity pred)
        {
            if (IsChild())
            {
                if (!GetParent().IsSwallowedBy(pred)) pred.Swallow(GetParent());
            }
            if (IsParent())
            {
                foreach (VoreEntity child in GetChildren())
                {
                    if (!child.IsSwallowedBy(pred)) pred.Swallow(child);
                }
            }
        }

        public override void OnRegurgitatedBy(VoreEntity pred)
        {
            if (IsChild())
            {
                if (GetParent().IsSwallowedBy(pred)) pred.Regurgitate(GetParent());
            }
            if (IsParent())
            {
                foreach (VoreEntity child in GetChildren())
                {
                    if (child.IsSwallowedBy(pred)) pred.Regurgitate(child);
                }
            }
        }

        public override void OnDisposedBy(VoreEntity pred)
        {
            if (IsChild())
            {
                if (GetParent().IsSwallowedBy(pred)) pred.Dispose(GetParent());
            }
            if (IsParent())
            {
                foreach (VoreEntity child in GetChildren())
                {
                    if (child.IsSwallowedBy(pred)) pred.Dispose(child);
                }
            }
        }

        public override bool IsChild() => GetNPC().realLife >= 0 && GetNPC().realLife != GetNPC().whoAmI;

        public override IEnumerable<VoreEntity> GetSiblings()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] != GetNPC() && Main.npc[i].realLife == GetNPC().realLife) yield return Main.npc[i];
            }
        }

        public override bool IsParent() => GetChildren().Any();

        public override VoreEntity GetParent() => IsChild() ? Main.npc[GetNPC().realLife] : null;

        public override IEnumerable<VoreEntity> GetChildren()
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] == GetNPC()) continue;
                if (Main.npc[i].realLife == GetNPC().whoAmI) yield return Main.npc[i];
            }
        }

        public override bool IsValid() => npc != null && npc.active;

        public override int GetID() => npc.whoAmI;

        public override string GetName() => VoreConfig.Instance.DebugInfo ? "[" + GetNPC().whoAmI + "] {" + GetNPC().type + "} " + GetNPC().GivenOrTypeName : GetNPC().GivenOrTypeName;

        public override int GetLife() => GetNPC().life;

        public override void SetLife(int life)
        {
            GetNPC().life = life;
        }

        public override int GetLifeMax() => GetNPC().lifeMax;

        public override int GetMana() => 0;

        public override void SetMana(int mana) { }

        public override int GetManaMax() => 0;

        public override LegacySoundStyle GetHitSound() => GetNPC().HitSound;

        public override int GetDirection() => GetNPC().direction;

        public override Vector2 GetPosition() => GetNPC().Bottom;

        public override void SetPosition(Vector2 position)
        {
            GetNPC().Bottom = position;
        }

        public override float GetSizeFactor() => npc.width * npc.height / 1200f;

        public override void Damage(VoreEntity damager, int damage, float knockback)
        {
            if (damager is VoreEntityPlayer)
            {
                VoreEntityPlayer player = damager as VoreEntityPlayer;
                GetNPC().PlayerInteraction(player.GetPlayer().whoAmI);
            }
            GetNPC().StrikeNPC(damage, knockback, damager.GetDirection(), false, false);
        }

        public override void Knockback(Vector2 knockback)
        {
            GetNPC().velocity += knockback;
        }

        public override void Heal(int healing)
        {
            GetNPC().life += healing;
            GetNPC().HealEffect(healing, true);
        }

        public override void BackupState()
        {
            npcBackup = new StateNPC(GetNPC());
        }

        public override void RestoreState()
        {
            npcBackup.Restore(GetNPC());
        }

        public override void SetStateSwallowed()
        {
            StateNPC.SwallowedState.Restore(GetNPC());
        }

        public override bool HasBelly()
        {
            if (!base.HasBelly()) return false;
            if (npc.townNPC) return true;
            switch (npc.type)
            {
                case NPCID.Nymph:
                case NPCID.DesertLamiaDark:
                case NPCID.DesertLamiaLight:
                case NPCID.Harpy:
                case NPCID.Werewolf:
                    return true;
            }
            return false;
        }

        public override Texture2D GetBellyTexture()
        {
            if (npc.type == ModContent.NPCType<Succubus>())
            {
                return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Succubus_Belly");
            }
            switch (npc.type)
            {
				#region Town NPCs
				case NPCID.ArmsDealer: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/ArmsDealer_Belly");
                case NPCID.Clothier: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Clothier_Belly");
                case NPCID.Cyborg: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Cyborg_Belly");
                case NPCID.Demolitionist: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Demolitionist_Belly");
                case NPCID.Dryad: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Dryad_Belly");
                case NPCID.DyeTrader: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/DyeTrader_Belly");
                case NPCID.GoblinTinkerer: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/GoblinTinkerer_Belly");
                case NPCID.Guide: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Guide_Belly");
                case NPCID.Mechanic: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Mechanic_Belly");
                case NPCID.Merchant: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Merchant_Belly");
                case NPCID.Nurse: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Nurse_Belly");
                case NPCID.OldMan: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/OldMan_Belly");
                case NPCID.Painter: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Painter_Belly");
                case NPCID.PartyGirl: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/PartyGirl_Belly");
                case NPCID.Pirate: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Pirate_Belly");
                case NPCID.SantaClaus: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Santa_Belly");
                case NPCID.Steampunker: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Steampunker_Belly");
                case NPCID.Stylist: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Stylist_Belly");
                case NPCID.DD2Bartender: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Tavernkeep_Belly");
                case NPCID.TaxCollector: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/TaxCollector_Belly");
                case NPCID.TravellingMerchant: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/TravelingMerchant_Belly");
                case NPCID.Truffle: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Truffle_Belly");
                case NPCID.WitchDoctor: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/WitchDoctor_Belly");
                case NPCID.Wizard: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Wizard_Belly");
				#endregion

				case NPCID.Nymph: return ModContent.GetTexture(nameof(VoreMod) + "/Belly");
                case NPCID.DesertLamiaLight:
                case NPCID.DesertLamiaDark:
                    return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Lamia_Belly");
                case NPCID.Harpy: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Harpy_Belly");
                case NPCID.Werewolf: return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Werewolf_Belly");
            }
			if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP")) return ModContent.GetTexture(nameof(VoreMod) + "/NPCs/Calamity/DrunkPrincess_Belly");
            if (npc.townNPC) return ModContent.GetTexture(nameof(VoreMod) + "/Belly");
            return null;
        }

        public override Rectangle GetBellyRect()
        {
            Texture2D texture = GetBellyTexture();
            float ratio = MathHelper.Clamp(GetBellyRatio(), 0f, 1f);

            switch (npc.type)
            {
                case NPCID.Werewolf:
                    {
                        int frame = 0;
                        if (ratio >= 0.75f) frame = 3;
                        else if (ratio >= 0.5f) frame = 2;
                        else if (ratio >= 0.25f) frame = 1;
                        return new Rectangle((texture.Width / 4) * frame, npc.frame.Top, npc.frame.Width, npc.frame.Height);
                    }
            }
            return base.GetBellyRect();
        }

        public override Vector2 GetBellyOffset()
        {
            int frame = npc.frame.Top / (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]);
            int dir = npc.spriteDirection;

            if (npc.townNPC)
            {
                bool bounce = false;

                if (npc.type == ModContent.NPCType<Succubus>())
                {
                    bounce = (frame >= 3 && frame <= 5) || (frame >= 9 && frame <= 11);
                }

                switch (npc.type)
                {
                    case NPCID.Nurse:
                    case NPCID.Mechanic:
                    case NPCID.PartyGirl:
                    case NPCID.Stylist:
                    case NPCID.Dryad:
                    case NPCID.Steampunker:
                        bounce = (frame >= 3 && frame <= 5) || (frame >= 9 && frame <= 11);
                        break;
                }

                Vector2 offset = new Vector2(4f * dir, 8f + (bounce ? -2f : 0f));
                if (npc.type == NPCID.Painter) offset.X -= 3f * dir;
                if (npc.type == NPCID.DD2Bartender) offset.X -= 4f * dir;
                if (npc.type == NPCID.Truffle) offset.X += 2f * dir;
                if (npc.type == NPCID.WitchDoctor) offset.X -= 5f * dir;
                if (npc.type == NPCID.TaxCollector) offset.X -= 1f * dir;
                if (npc.type == NPCID.Wizard) offset.Y -= 2f;
                if (npc.type == NPCID.Angler)
                {
                    offset.X += 2f * dir;
                    offset.Y += 2f;
                }
                return offset;
            }
            switch (npc.type)
            {
                case NPCID.Nymph: return new Vector2(3.5f * dir, 8f);
                case NPCID.DesertLamiaLight:
                case NPCID.DesertLamiaDark:
                    return new Vector2((9f + (frame == 3 || frame == 4 || frame == 7 ? 2f : 0f)) * dir, 6f + (frame == 8 ? -8f : 0f));
                case NPCID.Harpy:
                    return new Vector2(4.5f * dir, 4.5f);
                case NPCID.Werewolf:
                    return new Vector2(-2f * dir, -4f);
            }
            return Vector2.Zero;
        }

        public override Color GetBellyColor()
        {
            switch (npc.type)
            {
                case NPCID.Nymph: return new Color(182, 186, 146);
            }

            return Color.White;
        }

        public override int GetStruggleBonus(VoreEntity pred) => npc.modNPC is Succubus ? int.MaxValue : (Main.expertMode ? npc.defDamage / 3 : npc.defDamage);

        public override int GetEscapeLimit(VoreEntity prey) => Main.expertMode ? npc.defDefense * 3 : npc.defDefense * 10;

		public override bool ShouldDigest(VoreEntity prey)
		{
			if (npc.type == ModLoader.GetMod("CalamityMod")?.NPCType("FAP"))
				return true;
			return base.ShouldDigest(prey);
		}

		public override bool IsHostileTo(VoreEntity other)
        {
            if (other is VoreEntityNPC)
            {
                return (other as VoreEntityNPC).GetNPC().friendly != GetNPC().friendly;
            }
            else if (other is VoreEntityPlayer)
            {
                return !GetNPC().friendly;
            }
            return true;
        }

        public override float GetRandomVoreChance(VoreEntity pred) => 0.75f;

        public override int GetCapacity() => GetNPC().modNPC is Succubus ? int.MaxValue : 1;

        public override CharmEffects GetBaseCharms()
        {
            return new CharmEffects()
            {
                life = GetNPC().type == NPCID.Nurse || GetNPC().type == NPCID.WitchDoctor ? ItemTier.SilverTungsten : ItemTier.CopperTin,
                mana = GetNPC().type == NPCID.Dryad || GetNPC().type == NPCID.Wizard ? ItemTier.SilverTungsten : ItemTier.None,
                acid = GetNPC().type == ModContent.NPCType<Succubus>() ? ItemTier.Hellstone : ItemTier.CopperTin,
                soul = GetNPC().type == ModContent.NPCType<Succubus>() ? ItemTier.Hellstone : ItemTier.None,
                hunger = ItemTier.CopperTin
            };
        }

        public override bool ShouldShowPrey() => IsSlime();

        public override float GetScale() => IsEye() || IsSlime() ? 1f + GetBellyRatio() * 25f / npc.width : 1f;

        public override EntityTags GetTags()
        {
            EntityTags tags = EntityTags.None;
            if (IsTownNPC()) tags |= EntityTags.TownNPC;
            else tags |= EntityTags.Monster;
            if (IsMale()) tags |= EntityTags.Male;
            if (IsFemale()) tags |= EntityTags.Female;
            if (!IsMale() && !IsFemale()) tags |= EntityTags.Genderless;
            if (IsWorm()) tags |= EntityTags.Worm;
            if (IsZombie()) tags |= EntityTags.Zombie;
            if (IsSkeleton()) tags |= EntityTags.Skeleton;
            if (IsEye()) tags |= EntityTags.Eye;
            if (IsSlime()) tags |= EntityTags.Slime;
            if (IsSpider()) tags |= EntityTags.Spider;
            return tags;
        }

        public override bool EligibleForVore()
        {
            if (npc.boss) return true;
            if (npc.realLife >= 0 && Main.npc[npc.realLife].boss) return true;
            switch (npc.type)
            {
                case NPCID.BurningSphere:
                case NPCID.ChaosBall:
                case NPCID.WaterSphere:
                case NPCID.VileSpit:
                case NPCID.Spore:
                case NPCID.DetonatingBubble:
                case NPCID.MoonLordLeechBlob:
                case NPCID.SolarFlare:
                case NPCID.SolarGoop:
                case NPCID.AncientLight:
                case NPCID.AncientDoom:

                case NPCID.DungeonGuardian:
                // case NPCID.CultistTablet:
                case NPCID.PirateShip:
                case NPCID.PirateShipCannon:
                case NPCID.ForceBubble:
                case NPCID.LunarTowerNebula:
                case NPCID.LunarTowerSolar:
                case NPCID.LunarTowerStardust:
                case NPCID.LunarTowerVortex:
                case NPCID.DD2EterniaCrystal:
                case NPCID.DD2LanePortal:

				case NPCID.TargetDummy:
                    return false;
            }
            return true;
        }

        private bool IsTownNPC()
        {
            return npc.townNPC;
        }

        private bool IsMale()
		{
			switch (npc.type)
			{
				case NPCID.BigRainZombie:
				case NPCID.SmallRainZombie:
				case NPCID.BigPantlessSkeleton:
				case NPCID.SmallPantlessSkeleton:
				case NPCID.BigMisassembledSkeleton:
				case NPCID.SmallMisassembledSkeleton:
				case NPCID.BigHeadacheSkeleton:
				case NPCID.SmallHeadacheSkeleton:
				case NPCID.BigSkeleton:
				case NPCID.SmallSkeleton:
				case NPCID.BigTwiggyZombie:
				case NPCID.SmallTwiggyZombie:
				case NPCID.BigSwampZombie:
				case NPCID.SmallSwampZombie:
				case NPCID.BigSlimedZombie:
				case NPCID.SmallSlimedZombie:
				case NPCID.BigPincushionZombie:
				case NPCID.SmallPincushionZombie:
				case NPCID.BigBaldZombie:
				case NPCID.SmallBaldZombie:
				case NPCID.BigZombie:
				case NPCID.SmallZombie:
				case NPCID.HeavySkeleton:
				case NPCID.BigBoned:
				case NPCID.ShortBones:
				case NPCID.Zombie:
				case NPCID.EyeofCthulhu:
				case NPCID.Merchant:
				case NPCID.ArmsDealer:
				case NPCID.Skeleton:
				case NPCID.Guide:
				case NPCID.FireImp:
				case NPCID.GoblinPeon:
				case NPCID.GoblinThief:
				case NPCID.GoblinWarrior:
				case NPCID.AngryBones:
				case NPCID.DarkCaster:
				case NPCID.CursedSkull:
				case NPCID.SkeletronHead:
				case NPCID.SkeletronHand:
				case NPCID.OldMan:
				case NPCID.Demolitionist:
				case NPCID.UndeadMiner:
				case NPCID.Tim:
				case NPCID.KingSlime:
				case NPCID.DoctorBones:
				case NPCID.TheGroom:
				case NPCID.Clothier:
				case NPCID.Demon:
				case NPCID.VoodooDemon:
				case NPCID.DungeonGuardian:
				case NPCID.ArmoredSkeleton:
				case NPCID.Mummy:
				case NPCID.DarkMummy:
				case NPCID.LightMummy:
				case NPCID.Wraith:
				case NPCID.Werewolf:
				case NPCID.BoundGoblin:
				case NPCID.BoundWizard:
				case NPCID.GoblinTinkerer:
				case NPCID.Wizard:
				case NPCID.Clown:
				case NPCID.SkeletonArcher:
				case NPCID.GoblinArcher:
				case NPCID.ChaosElemental:
				case NPCID.Retinazer:
				case NPCID.Spazmatism:
				case NPCID.SkeletronPrime:
				case NPCID.PrimeCannon:
				case NPCID.PrimeSaw:
				case NPCID.PrimeVice:
				case NPCID.PrimeLaser:
				case NPCID.BaldZombie:
				case NPCID.PossessedArmor:
				case NPCID.SantaClaus:
				case NPCID.SnowmanGangsta:
				case NPCID.MisterStabby:
				case NPCID.SnowBalla:
				case NPCID.RedDevil:
				case NPCID.Vampire:
				case NPCID.Truffle:
				case NPCID.ZombieEskimo:
				case NPCID.Frankenstein:
				case NPCID.SwampThing:
				case NPCID.UndeadViking:
				case NPCID.RuneWizard:
				case NPCID.FaceMonster:
				case NPCID.FloatyGross:
				case NPCID.PincushionZombie:
				case NPCID.SlimedZombie:
				case NPCID.SwampZombie:
				case NPCID.TwiggyZombie:
				case NPCID.ArmoredViking:
				case NPCID.Lihzahrd:
				case NPCID.LihzahrdCrawler:
				case NPCID.HeadacheSkeleton:
				case NPCID.MisassembledSkeleton:
				case NPCID.PantlessSkeleton:
				case NPCID.IcyMerman:
				case NPCID.DyeTrader:
				case NPCID.Cyborg:
				case NPCID.PirateDeckhand:
				case NPCID.PirateCorsair:
				case NPCID.PirateDeadeye:
				case NPCID.PirateCaptain:
				case NPCID.ZombieRaincoat:
				case NPCID.Painter:
				case NPCID.WitchDoctor:
				case NPCID.Pirate:
				case NPCID.IceGolem:
				case NPCID.Golem:
				case NPCID.GolemHead:
				case NPCID.GolemFistLeft:
				case NPCID.GolemFistRight:
				case NPCID.GolemHeadFree:
				case NPCID.Eyezor:
				case NPCID.Reaper:
				case NPCID.ZombieMushroom:
				case NPCID.ZombieMushroomHat:
				case NPCID.BrainofCthulhu:
				case NPCID.Creeper:
				case NPCID.RustyArmoredBonesAxe:
				case NPCID.RustyArmoredBonesFlail:
				case NPCID.RustyArmoredBonesSword:
				case NPCID.RustyArmoredBonesSwordNoArmor:
				case NPCID.BlueArmoredBones:
				case NPCID.BlueArmoredBonesMace:
				case NPCID.BlueArmoredBonesNoPants:
				case NPCID.BlueArmoredBonesSword:
				case NPCID.HellArmoredBones:
				case NPCID.HellArmoredBonesSpikeShield:
				case NPCID.HellArmoredBonesMace:
				case NPCID.HellArmoredBonesSword:
				case NPCID.RaggedCaster:
				case NPCID.RaggedCasterOpenCoat:
				case NPCID.Necromancer:
				case NPCID.NecromancerArmored:
				case NPCID.DiabolistRed:
				case NPCID.DiabolistWhite:
				case NPCID.BoneLee:
				case NPCID.GiantCursedSkull:
				case NPCID.Paladin:
				case NPCID.SkeletonSniper:
				case NPCID.TacticalSkeleton:
				case NPCID.SkeletonCommando:
				case NPCID.AngryBonesBig:
				case NPCID.AngryBonesBigMuscle:
				case NPCID.AngryBonesBigHelmet:
				case NPCID.Scarecrow1:
				case NPCID.Scarecrow2:
				case NPCID.Scarecrow3:
				case NPCID.Scarecrow4:
				case NPCID.Scarecrow5:
				case NPCID.Scarecrow6:
				case NPCID.Scarecrow7:
				case NPCID.Scarecrow8:
				case NPCID.Scarecrow9:
				case NPCID.Scarecrow10:
				case NPCID.HeadlessHorseman:
				case NPCID.Ghost:
				case NPCID.SkeletonTopHat:
				case NPCID.SkeletonAstonaut:
				case NPCID.SkeletonAlien:
				case NPCID.MourningWood:
				case NPCID.Splinterling:
				case NPCID.Pumpking:
				case NPCID.PumpkingBlade:
				case NPCID.Poltergeist:
				case NPCID.ZombieElf:
				case NPCID.ZombieElfBeard:
				case NPCID.GingerbreadMan:
				case NPCID.Yeti:
				case NPCID.Everscream:
				case NPCID.SantaNK1:
				case NPCID.Nutcracker:
				case NPCID.NutcrackerSpinning:
				case NPCID.Krampus:
				case NPCID.TravellingMerchant:
				case NPCID.Angler:
				case NPCID.DukeFishron:
				case NPCID.SleepingAngler:
				case NPCID.MoonLordHead:
				case NPCID.MoonLordHand:
				case NPCID.MoonLordCore:
				case NPCID.MoonLordFreeEye:
				case NPCID.ArmedZombie:
				case NPCID.ArmedZombieEskimo:
				case NPCID.ArmedZombiePincussion:
				case NPCID.ArmedZombieSlimed:
				case NPCID.ArmedZombieSwamp:
				case NPCID.ArmedZombieTwiggy:
				case NPCID.TaxCollector:
				case NPCID.BoneThrowingSkeleton:
				case NPCID.BoneThrowingSkeleton2:
				case NPCID.BoneThrowingSkeleton3:
				case NPCID.BoneThrowingSkeleton4:
				case NPCID.SkeletonMerchant:
				case NPCID.Butcher:
				case NPCID.CreatureFromTheDeep:
				case NPCID.Fritz:
				case NPCID.Nailhead:
				case NPCID.Psycho:
				case NPCID.DrManFly:
				case NPCID.ThePossessed:
				case NPCID.ShadowFlameApparition:
				case NPCID.GreekSkeleton:
				case NPCID.GraniteGolem:
				case NPCID.BloodZombie:
				case NPCID.DesertGhoul:
				case NPCID.DesertGhoulCorruption:
				case NPCID.DesertGhoulCrimson:
				case NPCID.DesertGhoulHallow:
				case NPCID.DesertDjinn:
				case NPCID.DemonTaxCollector:
				case NPCID.DD2Bartender:
				case NPCID.DD2GoblinT1:
				case NPCID.DD2GoblinT2:
				case NPCID.DD2GoblinT3:
				case NPCID.DD2GoblinBomberT1:
				case NPCID.DD2GoblinBomberT2:
				case NPCID.DD2GoblinBomberT3:
				case NPCID.DD2JavelinstT1:
				case NPCID.DD2JavelinstT2:
				case NPCID.DD2JavelinstT3:
				case NPCID.DD2DarkMageT1:
				case NPCID.DD2DarkMageT3:
				case NPCID.DD2SkeletonT1:
				case NPCID.DD2SkeletonT3:
				case NPCID.DD2KoboldWalkerT2:
				case NPCID.DD2KoboldWalkerT3:
				case NPCID.DD2KoboldFlyerT2:
				case NPCID.DD2KoboldFlyerT3:
				case NPCID.DD2OgreT2:
				case NPCID.DD2OgreT3:
				case NPCID.BartenderUnconscious:
					return true;
			}
			return false;
		}

        private bool IsFemale()
        {
            if (npc.type == ModContent.NPCType<Succubus>()) return true;
            switch (npc.type)
            {
                //case NPCID.Zoologist:
                case NPCID.Nurse:
                case NPCID.Dryad:
                case NPCID.Stylist:
                case NPCID.Mechanic:
                case NPCID.PartyGirl:
                case NPCID.Steampunker:
                case NPCID.BigFemaleZombie:
                case NPCID.SmallFemaleZombie:
                case NPCID.GoblinSorcerer:
                case NPCID.GoblinScout:
                case NPCID.Harpy:
                case NPCID.LostGirl:
                case NPCID.Nymph:
                case NPCID.FemaleZombie:
                case NPCID.PirateCrossbower:
                case NPCID.ZombieXmas:
                case NPCID.ZombieElfGirl:
                case NPCID.IceQueen:
                case NPCID.ElfArcher:
                case NPCID.ArmedZombieCenx:
                case NPCID.GoblinSummoner:
                case NPCID.Medusa:
                case NPCID.DesertLamiaLight:
                case NPCID.DesertLamiaDark:
                case NPCID.TheBride:
                case NPCID.SandElemental:
                    return true;
            }
            return false;
        }

        private bool IsWorm()
        {
            switch (npc.type)
            {
                case NPCID.DevourerHead:
                case NPCID.DevourerBody:
                case NPCID.DevourerTail:
                case NPCID.GiantWormHead:
                case NPCID.GiantWormBody:
                case NPCID.GiantWormTail:
                case NPCID.EaterofWorldsHead:
                case NPCID.EaterofWorldsBody:
                case NPCID.EaterofWorldsTail:
                case NPCID.BoneSerpentHead:
                case NPCID.BoneSerpentBody:
                case NPCID.BoneSerpentTail:
                case NPCID.WyvernHead:
                case NPCID.WyvernLegs:
                case NPCID.WyvernBody:
                case NPCID.WyvernBody2:
                case NPCID.WyvernBody3:
                case NPCID.WyvernTail:
                case NPCID.DiggerHead:
                case NPCID.DiggerBody:
                case NPCID.DiggerTail:
                case NPCID.SeekerHead:
                case NPCID.SeekerBody:
                case NPCID.SeekerTail:
                case NPCID.LeechHead:
                case NPCID.LeechBody:
                case NPCID.LeechTail:
                case NPCID.TheDestroyer:
                case NPCID.TheDestroyerBody:
                case NPCID.TheDestroyerTail:
                case NPCID.StardustWormHead:
                case NPCID.StardustWormBody:
                case NPCID.StardustWormTail:
                case NPCID.SolarCrawltipedeHead:
                case NPCID.SolarCrawltipedeBody:
                case NPCID.SolarCrawltipedeTail:
                case NPCID.CultistDragonHead:
                case NPCID.CultistDragonBody1:
                case NPCID.CultistDragonBody2:
                case NPCID.CultistDragonBody3:
                case NPCID.CultistDragonBody4:
                case NPCID.CultistDragonTail:
                case NPCID.DuneSplicerHead:
                case NPCID.DuneSplicerBody:
                case NPCID.DuneSplicerTail:
                case NPCID.TombCrawlerHead:
                case NPCID.TombCrawlerBody:
                case NPCID.TombCrawlerTail:
                    return true;
            }
            return false;
        }

        private bool IsEye()
        {
            switch (npc.type)
            {
                case NPCID.DemonEye2:
                case NPCID.PurpleEye2:
                case NPCID.GreenEye2:
                case NPCID.DialatedEye2:
                case NPCID.SleepyEye2:
                case NPCID.CataractEye2:
                case NPCID.DemonEye:
                case NPCID.EyeofCthulhu:
                case NPCID.ServantofCthulhu:
                case NPCID.WanderingEye:
                case NPCID.CataractEye:
                case NPCID.SleepyEye:
                case NPCID.DialatedEye:
                case NPCID.GreenEye:
                case NPCID.PurpleEye:
                case NPCID.DemonEyeOwl:
                case NPCID.DemonEyeSpaceship:
                case NPCID.Drippler:
                    return true;
            }
            return false;
        }

        private bool IsZombie()
        {
            switch (npc.type)
            {
                case NPCID.BigRainZombie:
                case NPCID.SmallRainZombie:
                case NPCID.BigFemaleZombie:
                case NPCID.SmallFemaleZombie:
                case NPCID.BigTwiggyZombie:
                case NPCID.SmallTwiggyZombie:
                case NPCID.BigSwampZombie:
                case NPCID.SmallSwampZombie:
                case NPCID.BigSlimedZombie:
                case NPCID.SmallSlimedZombie:
                case NPCID.BigPincushionZombie:
                case NPCID.SmallPincushionZombie:
                case NPCID.BigBaldZombie:
                case NPCID.SmallBaldZombie:
                case NPCID.BigZombie:
                case NPCID.SmallZombie:
                case NPCID.Zombie:
                case NPCID.DoctorBones:
                case NPCID.TheGroom:
                case NPCID.BaldZombie:
                case NPCID.ZombieEskimo:
                case NPCID.Frankenstein:
                case NPCID.PincushionZombie:
                case NPCID.SlimedZombie:
                case NPCID.SwampZombie:
                case NPCID.TwiggyZombie:
                case NPCID.FemaleZombie:
                case NPCID.ZombieRaincoat:
                case NPCID.Eyezor:
                case NPCID.ZombieMushroom:
                case NPCID.ZombieMushroomHat:
                case NPCID.ZombieDoctor:
                case NPCID.ZombieSuperman:
                case NPCID.ZombiePixie:
                case NPCID.ZombieXmas:
                case NPCID.ZombieSweater:
                case NPCID.ZombieElf:
                case NPCID.ZombieElfBeard:
                case NPCID.ZombieElfGirl:
                case NPCID.ArmedZombie:
                case NPCID.ArmedZombieEskimo:
                case NPCID.ArmedZombiePincussion:
                case NPCID.ArmedZombieSlimed:
                case NPCID.ArmedZombieTwiggy:
                case NPCID.ArmedZombieCenx:
                case NPCID.BloodZombie:
                case NPCID.TheBride:
                case NPCID.DD2SkeletonT1:
                case NPCID.DD2SkeletonT3:
                    return true;
            }
            return false;
        }

        private bool IsSkeleton()
        {
            switch (npc.type)
            {
                case NPCID.BigPantlessSkeleton:
                case NPCID.SmallPantlessSkeleton:
                case NPCID.BigMisassembledSkeleton:
                case NPCID.SmallMisassembledSkeleton:
                case NPCID.BigHeadacheSkeleton:
                case NPCID.SmallHeadacheSkeleton:
                case NPCID.BigSkeleton:
                case NPCID.SmallSkeleton:
                case NPCID.HeavySkeleton:
                case NPCID.BigBoned:
                case NPCID.ShortBones:
                case NPCID.AngryBones:
                case NPCID.DarkCaster:
                case NPCID.UndeadMiner:
                case NPCID.Tim:
                case NPCID.ArmoredSkeleton:
                case NPCID.UndeadViking:
                case NPCID.RuneWizard:
                case NPCID.ArmoredViking:
                case NPCID.HeadacheSkeleton:
                case NPCID.MisassembledSkeleton:
                case NPCID.PantlessSkeleton:
                case NPCID.RustyArmoredBonesAxe:
                case NPCID.RustyArmoredBonesFlail:
                case NPCID.RustyArmoredBonesSword:
                case NPCID.RustyArmoredBonesSwordNoArmor:
                case NPCID.BlueArmoredBones:
                case NPCID.BlueArmoredBonesMace:
                case NPCID.BlueArmoredBonesNoPants:
                case NPCID.BlueArmoredBonesSword:
                case NPCID.HellArmoredBones:
                case NPCID.HellArmoredBonesSpikeShield:
                case NPCID.HellArmoredBonesMace:
                case NPCID.HellArmoredBonesSword:
                case NPCID.RaggedCaster:
                case NPCID.RaggedCasterOpenCoat:
                case NPCID.Necromancer:
                case NPCID.NecromancerArmored:
                case NPCID.DiabolistRed:
                case NPCID.DiabolistWhite:
                case NPCID.BoneLee:
                case NPCID.Paladin:
                case NPCID.SkeletonSniper:
                case NPCID.TacticalSkeleton:
                case NPCID.SkeletonCommando:
                case NPCID.AngryBonesBig:
                case NPCID.AngryBonesBigMuscle:
                case NPCID.AngryBonesBigHelmet:
                case NPCID.SkeletonTopHat:
                case NPCID.SkeletonAstonaut:
                case NPCID.SkeletonAlien:
                case NPCID.BoneThrowingSkeleton:
                case NPCID.BoneThrowingSkeleton2:
                case NPCID.BoneThrowingSkeleton3:
                case NPCID.BoneThrowingSkeleton4:
                case NPCID.SkeletonMerchant:
                case NPCID.GreekSkeleton:
                    return true;
            }
            return false;
        }

        private bool IsSlime()
        {
            switch (npc.type)
            {
                case NPCID.BigCrimslime:
                case NPCID.LittleCrimslime:
                case NPCID.GreenSlime:
                case NPCID.BlueSlime:
                case NPCID.RedSlime:
                case NPCID.PurpleSlime:
                case NPCID.YellowSlime:
                case NPCID.BlackSlime:
                case NPCID.IceSlime:
                case NPCID.SandSlime:
                case NPCID.JungleSlime:
                case NPCID.SpikedIceSlime:
                case NPCID.SpikedJungleSlime:
                case NPCID.MotherSlime:
                case NPCID.BabySlime:
                case NPCID.LavaSlime:
                case NPCID.DungeonSlime:
                case NPCID.Pinky:
                case NPCID.KingSlime:
                case NPCID.SlimeSpiked:
                case NPCID.UmbrellaSlime:
                case NPCID.SlimeMasked:
                case NPCID.SlimeRibbonWhite:
                case NPCID.SlimeRibbonYellow:
                case NPCID.SlimeRibbonGreen:
                case NPCID.SlimeRibbonRed:
                case NPCID.ToxicSludge:
                case NPCID.CorruptSlime:
                case NPCID.Slimeling:
                case NPCID.Slimer:
                case NPCID.Slimer2:
                case NPCID.Crimslime:
                case NPCID.Gastropod:
                case NPCID.IlluminantSlime:
                case NPCID.RainbowSlime:
                    return true;
            }
            return false;
        }

        private bool IsSpider()
        {
            switch (npc.type)
            {
                case NPCID.BlackRecluse:
                case NPCID.WallCreeper:
                case NPCID.WallCreeperWall:
                case NPCID.JungleCreeper:
                case NPCID.JungleCreeperWall:
                case NPCID.BlackRecluseWall:
                case NPCID.BloodCrawler:
                case NPCID.BloodCrawlerWall:
                    return true;
            }
            return false;
        }
    }
}
