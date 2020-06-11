using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace VoreMod.Items
{
    public class AmuletThroatCopper : AmuletThroatBase
    {
        public override ItemTier Tier => ItemTier.CopperTin;
        public override int Metal => ItemID.CopperBar;
        public override int Capacity => 1;
        public override int EscapeLimit => 20;
    }
}
