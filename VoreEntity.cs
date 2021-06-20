using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Microsoft.Xna.Framework.Audio;

namespace VoreMod
{
	public abstract class VoreEntity
	{
		CharmEffects charms;

		public Vector2 swallowedLocation;

		bool digesting;
		int digestCounter;

		int digestValue;

		int regenCounter;

		int noiseCounter;
		int noiseRate;

		int struggleCounter;

		int graceCounter;

		float bellyRatio;

		VoreEntity pred;
		List<VoreEntity> preys = new List<VoreEntity>();

		public VoreEntity GetPredator() => pred;

		public IReadOnlyList<VoreEntity> GetAllPrey(bool includeChildren = false) => includeChildren ? new List<VoreEntity>(preys) : preys.Where(p => !p.IsChild()).ToList();

		public int GetPreyCount(bool includeChildren)
		{
			int count = 0;
			foreach (VoreEntity prey in preys)
			{
				if (includeChildren || !prey.IsChild()) count++;
			}
			return count;
		}

		public float GetDigestionRatio() => IsSwallowed() ? (float)digestValue / (float)pred.GetDigestionLimit(this) : 0f;

		public float GetLifeRatio() => (float)GetLife() / (float)GetLifeMax();

		public bool HasSwallowed(VoreEntity prey) => prey.IsSwallowedBy(this);

		public bool HasSwallowedAny() => preys.Count > 0;

		public bool IsSwallowed() => pred != null;

		public bool IsSwallowedBy(VoreEntity otherPred) => otherPred == pred;

		public bool CanSwallow(VoreEntity prey) => !IsSwallowed() && (GetPreyCount(false) < GetCapacity() || VoreConfig.Instance.DebugNoPreyCapacityLimit) && prey.CanBeSwallowed() && VoreConfig.Instance.CanSwallow.Match(this) && EligibleForVore();

		public bool CanBeSwallowed() => !IsSwallowed() && VoreConfig.Instance.CanBeSwallowed.Match(this) && EligibleForVore();

		public bool CanRegurgitate(VoreEntity prey) => !IsSwallowed() && prey.IsSwallowedBy(this) && prey.CanBeRegurgitated() && EligibleForVore();

		public bool CanRegurgitateAny() => HasSwallowedAny() && preys.Any(p => CanRegurgitate(p));

		public bool CanBeRegurgitated() => IsSwallowed() && EligibleForVore();

		public bool IsBeingDigested() => IsSwallowed() && digesting;

		public bool IsDigestingAny() => HasSwallowedAny() && preys.Any(p => p.IsBeingDigested());

		public bool CanDigest(VoreEntity prey) => prey.IsSwallowedBy(this) && prey.CanBeDigested() && VoreConfig.Instance.CanDigest.Match(this) && EligibleForVore();

		public bool CanDigestAny() => HasSwallowedAny() && preys.Any(p => CanDigest(p));

		public bool CanBeDigested() => IsSwallowed() && !IsBeingDigested() && VoreConfig.Instance.CanBeDigested.Match(this) && EligibleForVore();

		public bool CanDispose(VoreEntity prey) => prey.IsSwallowedBy(this) && prey.CanBeDisposed() && EligibleForVore();

		public bool CanDisposeAny() => HasSwallowedAny() && preys.Any(p => CanDispose(p));

		public bool CanBeDisposed() => IsSwallowed() && IsBeingDigested() && GetLife() <= 1 && GetDigestionRatio() >= 1f && EligibleForVore();

		public bool CanDamage(VoreEntity target) => !IsSwallowed() && graceCounter == 0 && target.CanBeDamaged();

		public bool CanBeDamaged() => !IsSwallowed() && graceCounter == 0;

		public bool CanStruggle() => IsSwallowed() && GetLife() > 1 && VoreConfig.Instance.CanStruggle.Match(this) && EligibleForVore();

		public bool CanRandomVore(VoreEntity target) => CanSwallow(target) && VoreConfig.Instance.CanRandomVore.Match(this) && EligibleForVore();

		public virtual bool ShouldDigest(VoreEntity prey) => CanDigest(prey) && charms.acid > 0 && (IsHostileTo(prey) || prey.charms.hunger != ItemTier.None);

		public virtual bool ShouldDispose(VoreEntity prey) => CanDispose(prey);

		public virtual bool ShouldStruggle() => CanStruggle() && IsHostileTo(GetPredator());

		public void Swallow(VoreEntity prey)
		{
			if (IsChild()) GetParent().AddPrey(prey);
			else AddPrey(prey);
		}

		public void Regurgitate(VoreEntity prey)
		{
			if (IsSwallowed())
			{
				RemovePrey(prey);
				GetPredator().Swallow(prey);
			}
			else
			{
				RegurgitatePrey(prey);
			}
		}

		public void RegurgitateLast()
		{
			VoreEntity prey = preys.LastOrDefault(p => CanRegurgitate(p));
			Regurgitate(prey);
		}

		public virtual void Dispose(VoreEntity prey)
		{
			DisposePrey(prey);
		}

		public void Digest(VoreEntity prey)
		{
			prey.digesting = true;
		}

		public bool AttemptRandomVore(VoreEntity target)
		{
			if (CanRandomVore(target))
			{
				if (Main.rand.NextFloat() <= target.GetRandomVoreChance(this))
				{
					Swallow(target);
				}
			}
			return false;
		}

		public void Death()
		{
			foreach (VoreEntity prey in GetAllPrey(true))
			{
				if (IsSwallowed())
				{
					RemovePrey(prey);
					pred.Swallow(prey);
				}
				else
				{
					Regurgitate(prey);
				}
			}
			if (IsSwallowed()) pred.Regurgitate(this);
		}

		public void ApplyCharm(CharmEffect effect, ItemTier tier)
		{
			charms[effect] = (ItemTier)Math.Max((int)charms[effect], (int)tier);
		}

		public void DigestTick()
		{
			digestCounter = 0;

			int digestDamage = (int)pred.charms.acid + (int)charms.hunger;
			int digestHeal = (int)pred.charms.life + (int)charms.life;
			int digestManaRegen = (int)pred.charms.mana + (int)charms.mana;

			digestDamage += (int)Math.Ceiling(GetLifeMax() * 0.05f * Math.Sqrt(GetDigestionRatio()));

			int digestAmount = digestDamage + (int)(Math.Sqrt(GetLifeMax()) * 0.05f);

			digestValue = (int)MathHelper.Clamp(digestValue + digestAmount, 0, pred.GetDigestionLimit(this));
			if (VoreConfig.Instance.DebugInfo) Main.NewText(GetName() + " is digesting! (" + digestValue + "/" + pred.GetDigestionLimit(this) + ")");

			if (GetLife() > 1)
			{
				if (digestDamage > 0) SetLife(Math.Max(1, GetLife() - digestDamage));
				if (digestHeal > 0) pred.SetLife(Math.Min(pred.GetLifeMax(), pred.GetLife() + digestHeal));
				if (digestManaRegen > 0) pred.SetMana(Math.Min(pred.GetManaMax(), pred.GetMana() + digestManaRegen));
			}
		}

		public void RegenTick()
		{
			regenCounter = 0;

			int lifeRegen = (int)pred.charms.life + (int)charms.life;
			int manaRegen = (int)pred.charms.mana + (int)charms.mana;

			if (lifeRegen > 0) SetLife(Math.Min(GetLifeMax(), GetLife() + lifeRegen));
			if (manaRegen > 0) SetMana(Math.Min(GetManaMax(), GetMana() + manaRegen));
			if (lifeRegen > 0) pred.SetLife(Math.Min(pred.GetLifeMax(), pred.GetLife() + lifeRegen));
			if (manaRegen > 0) pred.SetMana(Math.Min(pred.GetManaMax(), pred.GetMana() + manaRegen));
		}

		public void DigestionNoiseTick()
		{
			int noiseMinTime = IsDigestingAny() ? 60 : 120;
			int noiseMaxTime = IsDigestingAny() ? 120 : 480;

			noiseCounter = 0;
			noiseRate = Main.rand.Next(noiseMinTime, noiseMaxTime);

			PlaySound(GetDigestionNoise());
		}

		public void StruggleTick()
		{
			struggleCounter = 0;

			bool escape = VoreConfig.Instance.DebugNoStruggle || (!VoreConfig.Instance.TweakAlwaysStruggle && !GetPredator().IsHostileTo(this) && charms.hunger == 0);

			if (!escape)
			{
				int struggleBonus = GetStruggleBonus(GetPredator());
				int escapeLimit = GetPredator().GetEscapeLimit(this);
				int struggleRoll = Main.rand.Next(0, struggleBonus);
				int escapeRoll = Main.rand.Next(0, escapeLimit);

				if (IsLocalPlayer() || GetPredator().IsLocalPlayer())
				{
					if (VoreConfig.Instance.DebugInfo)
						Main.NewText(GetName() + " is struggling! " + struggleRoll + " (" + struggleBonus + ") >= " + escapeRoll + " (" + escapeLimit + ")");
					else
						Main.NewText(GetName() + " is struggling!");
				}

				if (struggleRoll >= escapeRoll)
				{
					digestValue = (int)MathHelper.Clamp(digestValue - struggleBonus, 0, pred.GetDigestionLimit(this));
					escape = digestValue <= 0;

					SoundEngine.PlaySound(GetPredator().GetHitSound(), GetPosition());
				}
			}

			if (escape)
			{
				if (IsLocalPlayer() || GetPredator().IsLocalPlayer()) Main.NewText(GetName() + " escaped!");

				GetPredator().Regurgitate(this);
			}
		}

		public void ResetTick()
		{
			if (!IsValid()) return;

			foreach (VoreEntity prey in GetAllPrey(true))
			{
				if (!prey.IsValid()) RemovePrey(prey);
			}
			if (pred != null && !pred.IsValid()) pred.RemovePrey(this);

			charms = GetBaseCharms();
		}

		public void UpdateTick()
		{
			if (!IsValid()) return;

			if (IsSwallowed())
			{
				SetStateSwallowed();
				SetPosition(pred.GetBellyLocation());
			}

			if (HasSwallowedAny())
			{
				if (noiseCounter >= noiseRate) DigestionNoiseTick();
				else noiseCounter++;

				foreach (VoreEntity prey in GetAllPrey())
				{
					if (ShouldDigest(prey)) Digest(prey);
					if (ShouldDispose(prey)) Dispose(prey);
				}
			}

			if (IsSwallowed() && !IsBeingDigested() && !IsChild())
			{
				int regenRate = 30;

				if (regenCounter >= regenRate) RegenTick();
				else regenCounter++;
			}

			if (IsBeingDigested() && !IsChild())
			{
				int digestRate = 60;

				if (digestCounter >= digestRate) DigestTick();
				else digestCounter++;
			}

			if (ShouldStruggle() && !IsChild())
			{
				int struggleRate = 90;

				if (struggleCounter >= struggleRate) StruggleTick();
				else struggleCounter++;
			}

			if (graceCounter > 0) graceCounter--;

			float targetRatio = GetAllPrey().Count > 0 ? GetAllPrey().Sum(p => p.GetSizeFactor() * (float)(Math.Sqrt(1f - p.GetDigestionRatio()) * 0.5f + p.GetLifeRatio() * 0.5f)) : 0f;
			if (targetRatio < bellyRatio) bellyRatio = MathHelper.Max(targetRatio, bellyRatio - 0.05f);
			if (targetRatio > bellyRatio) bellyRatio = MathHelper.Min(targetRatio, bellyRatio + 0.02f);
		}

		public virtual bool HasSprites(SpriteType type) => GetBellyRatio() > 0f && GetSprites(type) != null && !VoreConfig.Instance.DebugNoBellies;

		public abstract List<VoreSprite> GetSprites(SpriteType type);

		public virtual float GetBellyRatio() => VoreConfig.Instance.DebugFullBellies ? float.PositiveInfinity : bellyRatio;

		public virtual Vector2 GetBellyLocation() => GetPosition() + new Vector2(0f, -5f);

		public virtual Vector2 GetRegurgitateLocation() => GetPosition() - new Vector2(0f, 20f);

		public virtual Vector2 GetDigestLocation() => GetPosition();

		public virtual int GetDigestionLimit(VoreEntity prey) => GetEscapeLimit(prey) * 2;

		private void ResetSelf()
		{
			if (pred != null) RestoreState();
			pred = null;
			digesting = false;
			digestCounter = 0;
			digestValue = 0;
			noiseCounter = 0;
			struggleCounter = 0;
		}

		private void AddPrey(VoreEntity prey)
		{
			prey.ResetSelf();
			prey.pred = this;
			prey.swallowedLocation = prey.GetPosition();

			preys.Add(prey);
			prey.BackupState();
			prey.SetStateSwallowed();
			PlaySound(GetSwallowNoise());
			prey.OnSwallowedBy(this);
		}

		private void RemovePrey(VoreEntity prey)
		{
			prey.ResetSelf();
			preys.Remove(prey);
		}

		private void RegurgitatePrey(VoreEntity prey)
		{
			prey.graceCounter = 30;
			prey.RestoreState();
			prey.SetPosition(GetRegurgitateLocation());
			prey.Knockback(new Vector2(GetDirection() * 5f, -2.5f));
			PlaySound(GetRegurgitateNoise());
			RemovePrey(prey);
			prey.OnRegurgitatedBy(this);
		}

		private void DisposePrey(VoreEntity prey)
		{
			prey.graceCounter = 30;
			prey.RestoreState();
			prey.SetPosition(GetDigestLocation());
			PlaySound(GetDisposalNoise());
			RemovePrey(prey);
			prey.OnDisposedBy(this);
			if (prey is VoreEntityPlayer)
			{
				// nothing
			}
			else
			{
				prey.SetLife(1);
				prey.Damage(this, 1, 0f);
			}
			int soulChance = ((int)charms.soul + (int)prey.charms.soul) * 10;
			if (Main.hardMode && Main.rand.Next(100) < soulChance)
			{
				WeightedRandom<int> drop = new WeightedRandom<int>();
				drop.Add(ItemID.SoulofFlight, 1);
				drop.Add(ItemID.SoulofLight, 1);
				drop.Add(ItemID.SoulofNight, 1);
				if (soulChance >= 50)
				{
					drop.Add(ItemID.SoulofMight, 1 / 3);
					drop.Add(ItemID.SoulofSight, 1 / 3);
					drop.Add(ItemID.SoulofFright, 1 / 3);
				}
				Item.NewItem(prey.GetPosition(), drop.Get());
			}
		}

		private void PlaySound(string name)
		{
			return;
			if (Main.dedServ || string.IsNullOrEmpty(name)) return;
			Mod mod = ModLoader.GetMod(nameof(VoreMod));
			var sound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/" + name);
			SoundEngine.PlaySound(sound.WithVolume(0.75f).WithPitchVariance(0.25f), GetPosition());
		}

		private bool IsLocalPlayer() => !Main.dedServ && Main.LocalPlayer.GetEntity() == this;

		public virtual void OnSwallowedBy(VoreEntity pred) { }

		public virtual void OnRegurgitatedBy(VoreEntity pred) { }

		public virtual void OnDisposedBy(VoreEntity pred) { }

		public virtual IEnumerable<VoreEntity> GetSiblings() { yield break; }

		public virtual bool IsParent() => false;

		public virtual VoreEntity GetParent() => null;

		public virtual IEnumerable<VoreEntity> GetChildren() { yield break; }

		public abstract bool IsValid();

		public abstract int GetID();

		public abstract EntityTags GetTags();

		public virtual bool IsChild() => false;

		public abstract string GetName();

		public abstract int GetLife();

		public abstract void SetLife(int life);

		public abstract int GetLifeMax();

		public abstract int GetMana();

		public abstract void SetMana(int mana);

		public abstract int GetManaMax();

		public abstract LegacySoundStyle GetHitSound();

		public abstract int GetDirection();

		public abstract void SetPosition(Vector2 position);

		public abstract Vector2 GetPosition();

		public abstract float GetSizeFactor();

		public abstract void Damage(VoreEntity damager, int damage, float knockback);

		public abstract void Knockback(Vector2 knockback);

		public abstract void Heal(int healing);

		public abstract void BackupState();

		public abstract void RestoreState();

		public abstract void SetStateSwallowed();

		public abstract int GetStruggleBonus(VoreEntity pred);

		public abstract int GetEscapeLimit(VoreEntity prey);

		public abstract bool IsHostileTo(VoreEntity other);

		public abstract float GetRandomVoreChance(VoreEntity pred);

		public abstract int GetCapacity();

		public abstract CharmEffects GetBaseCharms();

		public virtual bool ShouldShowPrey() => false;

		public virtual bool ShouldShowWhileSwallowed() => IsSwallowed() && GetPredator().ShouldShowPrey() && !VoreConfig.Instance.DebugNoLayeredPrey;

		public virtual float GetScale() => IsSwallowed() ? 0.75f : 1f;

		public virtual bool EligibleForVore() => true;

		public virtual DialogueTags GetDialogueTags()
		{
			DialogueTags tags = DialogueTags.None;
			if (Main.LocalPlayer.Male) tags |= DialogueTags.MalePlayer;
			if (!Main.LocalPlayer.Male) tags |= DialogueTags.FemalePlayer;

			if (Main.dayTime)
			{
				tags |= DialogueTags.Day;
				if (Main.time >= 0.0 * 3600.0 && Main.time <= 3.0 * 3600.0) tags |= DialogueTags.Morning; // Morning: 4:30 am to 7:30 am
				if (Main.time >= 11.0 * 3600.0 && Main.time <= 15.0 * 3600.0) tags |= DialogueTags.Evening;

			}
			else
			{
				tags |= DialogueTags.Night;
			}

			if (Main.bloodMoon) tags |= DialogueTags.BloodMoon;
			if (BirthdayParty.PartyIsUp) tags |= DialogueTags.Party;
			if (LanternNight.LanternsUp) tags |= DialogueTags.LanternNight;
			if (Main.invasionType == InvasionID.GoblinArmy) tags |= DialogueTags.GoblinArmy;
			if (Main.slimeRain) tags |= DialogueTags.SlimeRain;
			if (Main.LocalPlayer.ZoneOldOneArmy || DD2Event.Ongoing || Main.invasionType == InvasionID.CachedOldOnesArmy) tags |= DialogueTags.OldOnesArmy;
			if (Main.invasionType == InvasionID.SnowLegion) tags |= DialogueTags.FrostLegion;
			if (Main.eclipse) tags |= DialogueTags.SolarEclipse;
			if (Main.invasionType == InvasionID.PirateInvasion) tags |= DialogueTags.PirateInvasion;
			if (Main.invasionType == InvasionID.CachedPumpkinMoon || Main.pumpkinMoon) tags |= DialogueTags.PumpkinMoon;
			if (Main.invasionType == InvasionID.CachedFrostMoon || Main.snowMoon) tags |= DialogueTags.FrostMoon;
			if (Main.invasionType == InvasionID.MartianMadness) tags |= DialogueTags.MartianMadness;
			if (Main.LocalPlayer.ZoneTowerNebula || Main.LocalPlayer.ZoneTowerSolar || Main.LocalPlayer.ZoneTowerStardust || Main.LocalPlayer.ZoneTowerVortex) tags |= DialogueTags.LunarEvent;

			if (Main.IsItAHappyWindyDay) tags |= DialogueTags.WindyDay;
			if (Main.IsItRaining) tags |= DialogueTags.Rain;
			if (Main.LocalPlayer.ZoneSandstorm) tags |= DialogueTags.Sandstorm;
			if (Main.IsItStorming) tags |= DialogueTags.Thunderstorm;

			if (NPC.FindFirstNPC(NPCID.ArmsDealer) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.ArmsDealer)].homeless) tags |= DialogueTags.ArmsDealerPresent;
			if (NPC.FindFirstNPC(NPCID.Clothier) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Clothier)].homeless) tags |= DialogueTags.ClothierPresent;
			if (NPC.FindFirstNPC(NPCID.Cyborg) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Cyborg)].homeless) tags |= DialogueTags.CyborgPresent;
			if (NPC.FindFirstNPC(NPCID.Demolitionist) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Demolitionist)].homeless) tags |= DialogueTags.DemolitionistPresent;
			if (NPC.FindFirstNPC(NPCID.Dryad) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Dryad)].homeless) tags |= DialogueTags.DryadPresent;
			if (NPC.FindFirstNPC(NPCID.DyeTrader) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.DyeTrader)].homeless) tags |= DialogueTags.DyeTraderPresent;
			if (NPC.FindFirstNPC(NPCID.GoblinTinkerer) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.GoblinTinkerer)].homeless) tags |= DialogueTags.GoblinTinkererPresent;
			if (NPC.FindFirstNPC(NPCID.Golfer) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Golfer)].homeless) tags |= DialogueTags.GolferPresent;
			if (NPC.FindFirstNPC(NPCID.Guide) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Guide)].homeless) tags |= DialogueTags.GuidePresent;
			if (NPC.FindFirstNPC(NPCID.Mechanic) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Mechanic)].homeless) tags |= DialogueTags.MechanicPresent;
			if (NPC.FindFirstNPC(NPCID.Nurse) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Nurse)].homeless) tags |= DialogueTags.NursePresent;
			if (NPC.FindFirstNPC(NPCID.Painter) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Painter)].homeless) tags |= DialogueTags.PainterPresent;
			if (NPC.FindFirstNPC(NPCID.PartyGirl) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.PartyGirl)].homeless) tags |= DialogueTags.PartyGirlPresent;
			if (NPC.FindFirstNPC(NPCID.Pirate) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Pirate)].homeless) tags |= DialogueTags.PiratePresent;
			if (NPC.FindFirstNPC(NPCID.SantaClaus) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.SantaClaus)].homeless) tags |= DialogueTags.SantaPresent;
			if (NPC.FindFirstNPC(NPCID.Steampunker) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Steampunker)].homeless) tags |= DialogueTags.SteampunkerPresent;
			if (NPC.FindFirstNPC(NPCID.Stylist) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Stylist)].homeless) tags |= DialogueTags.StylistPresent;
			if (NPC.FindFirstNPC(NPCID.DD2Bartender) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.DD2Bartender)].homeless) tags |= DialogueTags.TavernkeepPresent;
			if (NPC.FindFirstNPC(NPCID.TaxCollector) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.TaxCollector)].homeless) tags |= DialogueTags.TaxCollectorPresent;
			if (NPC.FindFirstNPC(NPCID.TravellingMerchant) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.TravellingMerchant)].homeless) tags |= DialogueTags.TravellingMerchantPresent;
			if (NPC.FindFirstNPC(NPCID.Truffle) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.TravellingMerchant)].homeless) tags |= DialogueTags.TrufflePresent;
			if (NPC.FindFirstNPC(NPCID.WitchDoctor) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.WitchDoctor)].homeless) tags |= DialogueTags.WitchDoctorPresent;
			if (NPC.FindFirstNPC(NPCID.Wizard) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.Wizard)].homeless) tags |= DialogueTags.WizardPresent;
			if (NPC.FindFirstNPC(NPCID.BestiaryGirl) >= 0 && !Main.npc[NPC.FindFirstNPC(NPCID.BestiaryGirl)].homeless) tags |= DialogueTags.ZoologistPresent;

			return tags;
		}

		string GetSwallowNoise()
		{
			WeightedRandom<string> noises = new WeightedRandom<string>();
			if (VoreConfig.Instance.SoundsSwallowGulping)
			{
				noises.Add("Gulp");
			}
			if (noises.elements.Count == 0) return null;
			return noises.Get();
		}

		string GetRegurgitateNoise()
		{
			WeightedRandom<string> noises = new WeightedRandom<string>();
			if (VoreConfig.Instance.SoundsRegurgitatePuking)
			{
				noises.Add("Puke");
			}
			if (noises.elements.Count == 0) return null;
			return noises.Get();
		}

		string GetDigestionNoise()
		{
			WeightedRandom<string> noises = new WeightedRandom<string>();
			if (VoreConfig.Instance.SoundsDigestionGurgling)
			{
				noises.Add("afewlargergroans");
				noises.Add("agroan");
				noises.Add("blrp");
				noises.Add("blrrpgrougl");
				noises.Add("blrrrrp");
				noises.Add("brbrbrbrblrbrgblgr");
				noises.Add("burblegoingdown");
				noises.Add("burblingIthink");
				noises.Add("burblywhine");
				noises.Add("fewgroans");
				noises.Add("glorp");
				noises.Add("glorpgrowl");
				noises.Add("glowrpblorp");
				noises.Add("groooooorwp");
				noises.Add("gwouuurg");
				noises.Add("hardglrn");
				noises.Add("littlerumble-longer");
				noises.Add("littlerumble");
				noises.Add("singlebworb");
				noises.Add("singlegroan");
				noises.Add("someburbling-deeper");
				noises.Add("someburbling");
				noises.Add("somesquirts-take2");
				noises.Add("somesquirts");
				noises.Add("squirtsandgurgling");
				noises.Add("squirtsthenrumble");
			}
			if (VoreConfig.Instance.SoundsDigestionBurping)
			{
				noises.Add("burp", 0.5);
				noises.Add("burp-short", 0.5);
				noises.Add("wetbelch", 0.25);
			}
			if (VoreConfig.Instance.SoundsDigestionFarting)
			{
				noises.Add("fart", 0.25);
				noises.Add("toot");
			}
			if (noises.elements.Count == 0) return null;
			return noises.Get();
		}

		string GetDisposalNoise()
		{
			WeightedRandom<string> noises = new WeightedRandom<string>();
			if (VoreConfig.Instance.SoundsDisposalBelching)
			{
				if (GetTags().HasAll(EntityTags.Female)) noises.Add("BelchF");
				if (GetTags().HasAll(EntityTags.Male)) noises.Add("BelchM");
				noises.Add("wetbelch", 0.5);
			}
			if (VoreConfig.Instance.SoundsDisposalFarting)
			{
				noises.Add("fart-long");
				noises.Add("fart");
			}
			if (noises.elements.Count == 0) return null;
			return noises.Get();
		}

		public static implicit operator VoreEntity(NPC npc) => npc.GetEntity();

		public static implicit operator VoreEntity(Player player) => player.GetEntity();

		public override string ToString() => GetName();
	}
}
