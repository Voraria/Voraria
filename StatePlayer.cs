using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace VoreMod
{
    public class StatePlayer
    {
        public bool immune;
        public int immuneAlpha;
        public int lifeRegen;
        public float moveSpeed;
        public bool noKnockback;
        public bool noFallDmg;
        public bool noBuilding;
        public bool noItems;
        public int aggro;

        public static StatePlayer SwallowedState = new StatePlayer()
        {
            immune = true,
            immuneAlpha = 0,
            lifeRegen = 0,
            moveSpeed = 0,
            noKnockback = true,
            noFallDmg = true,
            noBuilding = true,
            noItems = true,
            aggro = int.MinValue,
        };

        public StatePlayer() { }

        public StatePlayer(Player player)
        {
            Backup(player);
        }

        public void Backup(Player player)
        {
            immune = player.immune;
            immuneAlpha = player.immuneAlpha;
            lifeRegen = player.lifeRegen;
            moveSpeed = player.moveSpeed;
            noKnockback = player.noKnockback;
            noFallDmg = player.noFallDmg;
            noBuilding = player.noBuilding;
            noItems = player.noItems;
            aggro = player.aggro;
        }

        public void Restore(Player player)
        {
            player.immune = immune;
            player.immuneAlpha = immuneAlpha;
            player.lifeRegen = lifeRegen;
            player.moveSpeed = moveSpeed;
            player.noKnockback = noKnockback;
            player.noFallDmg = noFallDmg;
            player.noBuilding = noBuilding;
            player.noItems = noItems;
            player.aggro = aggro;
        }
    }
}
