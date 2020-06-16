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

        public override int GetEscapeLimit(VoreEntity prey) => (int)System.Math.Round((Main.expertMode ? npc.defDefense * 3 : npc.defDefense * 10) * GetLifeRatio());

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

        public override bool ShouldShowPrey() => EntityTagLists.IsSlime(npc.type);

        public override float GetScale() => EntityTagLists.IsEye(npc.type) || EntityTagLists.IsSlime(npc.type) ? 1f + GetBellyRatio() * 25f / npc.width : 1f;

        public override EntityTags GetTags()
        {
            EntityTags tags = EntityTags.None;
            if (EntityTagLists.IsTownNPC(npc.type) || npc.townNPC) tags |= EntityTags.TownNPC;
            else tags |= EntityTags.Monster;
            if (EntityTagLists.IsMale(npc.type)) tags |= EntityTags.Male;
            if (EntityTagLists.IsFemale(npc.type)) tags |= EntityTags.Female;
            if (EntityTagLists.IsGenderless(npc.type)) tags |= EntityTags.Genderless;
            if (EntityTagLists.IsBats(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsHornet(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsZombie(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsSkeleton(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsUndead(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsWorm(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsBeast(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsReptile(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsEye(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsSlime(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsConstruct(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsPlant(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsAmbient(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsFish(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsInsect(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsFlying(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsMimic(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsProjectile(npc.type)) tags |= EntityTags.TownNPC;
            if (EntityTagLists.IsBoss(npc.type) || npc.boss) tags |= EntityTags.TownNPC;
            return tags;
        }

        public override bool EligibleForVore()
        {
            if (EntityTagLists.IsProjectile(npc.type)) return false;
            switch (npc.type)
            {
                case NPCID.DungeonGuardian:
                // case NPCID.CultistTablet:
                case NPCID.PirateShip:
                case NPCID.PirateShipCannon:
                case NPCID.ForceBubble:
                case NPCID.DD2EterniaCrystal:
                case NPCID.DD2LanePortal:

                case NPCID.TargetDummy:
                    return false;
            }
            return true;
        }
    }
}
