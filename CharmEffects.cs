using System;

namespace VoreMod
{
    public struct CharmEffects
    {
        public ItemTier acid;
        public ItemTier life;
        public ItemTier mana;
        public ItemTier hunger;
        public ItemTier soul;

        public ItemTier this[CharmEffect index]
        {
            get
            {
                switch (index)
                {
                    case CharmEffect.Acid: return acid;
                    case CharmEffect.Life: return life;
                    case CharmEffect.Mana: return mana;
                    case CharmEffect.Hunger: return hunger;
                    case CharmEffect.Soul: return soul;
                    default: throw new IndexOutOfRangeException("" + index);
                }
            }
            set
            {
                switch (index)
                {
                    case CharmEffect.Acid: acid = value; break;
                    case CharmEffect.Life: life = value; break;
                    case CharmEffect.Mana: mana = value; break;
                    case CharmEffect.Hunger: hunger = value; break;
                    case CharmEffect.Soul: soul = value; break;
                    default: throw new IndexOutOfRangeException("" + index);
                }
            }
        }
    }
}
