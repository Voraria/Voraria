using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace VoreMod
{
    public class StateNPC
    {
        public bool boss;
        public bool noTileCollide;
        public bool noGravity;
        public int damage;
        public bool dontTakeDamage;
        public int lifeRegen;

        public static StateNPC SwallowedState = new StateNPC()
        {
            boss = false,
            noTileCollide = true,
            noGravity = true,
            damage = 0,
            dontTakeDamage = true,
            lifeRegen = 0,
        };

        public StateNPC() { }

        public StateNPC(NPC npc)
        {
            Backup(npc);
        }

        public void Backup(NPC npc)
        {
            boss = npc.boss;
            noTileCollide = npc.noTileCollide;
            noGravity = npc.noGravity;
            damage = npc.damage;
            dontTakeDamage = npc.dontTakeDamage;
            lifeRegen = npc.lifeRegen;
        }

        public void Restore(NPC npc)
        {
            npc.boss = boss;
            npc.noTileCollide = noTileCollide;
            npc.noGravity = noGravity;
            npc.damage = damage;
            npc.dontTakeDamage = dontTakeDamage;
            npc.lifeRegen = lifeRegen;
        }
    }

}
