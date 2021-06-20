using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using System;
using TerraUI.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VoreMod.Items.VoreMod.Charms;
using Terraria.ModLoader.IO;

namespace VoreMod.UI
{
	public class CharmSlot : UIItemSlot
	{
		public CharmEffect charm;

		public CharmSlot(CharmEffect charm) : base(Vector2.Zero)
		{
			this.charm = charm;
			Context = ItemSlot.Context.InventoryItem;
			ScaleToInventory = true;
			HoverText = charm.ToString() + " Charm";
			Conditions = IsValidItem;
			DrawBackground = DrawSlotBackground;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Main.EquipPage != 2) return;
			int i = (int)Math.Log((int)charm, 2.0);

			float scale = Main.inventoryScale;
			Main.inventoryScale = 0.85f;

			float x = Main.screenWidth - 92 - 94f;
			float y = (!Main.mapFullscreen && Main.mapStyle == 1) ? (256 + 174) : 174;

			BackOpacity = 0.8f;
			Position = new Vector2(x, y + (55.1f * i * Main.inventoryScale));

			base.Draw(spriteBatch);

			Main.inventoryScale = scale;

			Update();
		}

		private bool IsValidItem(Item item)
		{
			CharmBase modItem = Item.ModItem as CharmBase;
			return modItem != null && modItem.Effect == charm;
		}

		private void DrawSlotBackground(UIObject sender, SpriteBatch spriteBatch)
		{
			UIItemSlot slot = (UIItemSlot)sender;
			slot.OnDrawBackground(spriteBatch);
			if (slot.Item.stack == 0)
			{
				Texture2D tex = ModContent.GetTexture(nameof(VoreMod) + "/Items/VoreMod/Charms/Charm" + charm + "Iron").Value;
				Vector2 origin = tex.Size() / 2f * Main.inventoryScale;
				Vector2 position = slot.Rectangle.TopLeft() + slot.Rectangle.Size() * 0.5f - origin * 0.5f;

				spriteBatch.Draw(tex, position, null, Color.White * 0.35f, 0f, origin, Main.inventoryScale, SpriteEffects.None, 0f);
			}
		}
	}
}