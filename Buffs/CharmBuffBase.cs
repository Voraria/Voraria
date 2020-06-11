using Terraria;
using Terraria.ModLoader;

namespace VoreMod.Buffs
{
    public abstract class CharmBuffBase : ModBuff
    {
        public override void SetDefaults()
        {
            this.canBeCleared = false;
            Main.persistentBuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = nameof(VoreMod) + "/Buffs/Charm" + Effect + "Buff";
            return base.Autoload(ref name, ref texture);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Update(npc);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Update(player);
        }

        public void Update(VoreEntity entity)
        {
            entity.ApplyCharm(Effect, Tier);
        }

        public abstract CharmEffect Effect { get; }
        public abstract ItemTier Tier { get; }
    }
}
