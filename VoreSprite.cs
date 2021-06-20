
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace VoreMod
{
	public class VoreSprite
	{
		string path;
		SpriteType type;
		SpriteLayout layout = SpriteLayout.SizeY;
		int sizeFrames = 1;
		ColorMode colorMode = ColorMode.Default;
		Color color = Color.White;
		Vector2 baseOffset = Vector2.Zero;
		Dictionary<int, Vector2> extraOffsets = new Dictionary<int, Vector2>();

		public VoreSprite(string path, SpriteType type)
		{
			this.path = path;
			this.type = type;
		}

		public Texture2D GetTexture()
		{
			Texture2D tex = ModContent.GetTexture(path).Value;
			if (tex == null) VoreMod.Instance.Logger.WarnFormat("Unable to find texture at {2}", path);
			return tex;
		}

		public Color GetColor() => color;

		public ColorMode GetColorMode() => colorMode;

		public Vector2 GetOffset(int animFrame)
		{
			return baseOffset + (extraOffsets.ContainsKey(animFrame) ? extraOffsets[animFrame] : Vector2.Zero);
		}

		public Rectangle GetRect(Texture2D tex, float ratio, int animFrames, int animFrame)
		{
			int sizeFrame = (int)(ratio * sizeFrames * 0.999f);

			switch (layout)
			{
				case SpriteLayout.SizeX:
					{
						int frameWidth = tex.Width / sizeFrames;
						return new Rectangle(frameWidth * sizeFrame, 0, frameWidth, tex.Height);
					}
				case SpriteLayout.SizeY:
					{
						int frameHeight = tex.Height / sizeFrames;
						return new Rectangle(0, frameHeight * sizeFrame, tex.Width, frameHeight);
					}
				case SpriteLayout.AnimX:
					{
						int frameWidth = tex.Width / animFrames;
						return new Rectangle(frameWidth * animFrame, 0, frameWidth, tex.Height);
					}
				case SpriteLayout.AnimY:
					{
						int frameHeight = tex.Height / animFrames;
						return new Rectangle(0, frameHeight * animFrame, tex.Width, frameHeight);
					}
				case SpriteLayout.SizeXAnimY:
					{
						int frameWidth = tex.Width / sizeFrames;
						int frameHeight = tex.Height / animFrames;
						return new Rectangle(frameWidth * sizeFrame, frameHeight * animFrame, frameWidth, frameHeight);
					}
				case SpriteLayout.AnimXSizeY:
					{
						int frameWidth = tex.Width / animFrames;
						int frameHeight = tex.Height / sizeFrames;
						return new Rectangle(frameWidth * animFrame, frameHeight * sizeFrame, frameWidth, frameHeight);
					}
				default: throw new System.ArgumentOutOfRangeException("Unknown sprite layout: " + layout);
			}
		}

		public class Builder
		{
			VoreSprite sprite;

			public Builder(VoreSprite sprite)
			{
				this.sprite = sprite;
			}

			public Builder Layout(SpriteLayout format)
			{
				sprite.layout = format;
				return this;
			}

			public Builder Frames(int frames)
			{
				sprite.sizeFrames = frames;
				return this;
			}

			public Builder Offset(float x, float y)
			{
				sprite.baseOffset = new Vector2(x, y);
				return this;
			}

			public Builder FrameOffset(float x, float y, int frame)
			{
				return FrameOffset(x, y, frame, frame);
			}

			public Builder FrameOffset(float x, float y, int startFrame, int endFrame)
			{
				for (int i = startFrame; i <= endFrame; i++)
				{
					sprite.extraOffsets[i] = new Vector2(x, y);
				}
				return this;
			}

			public Builder Color(byte r, byte g, byte b)
			{
				sprite.color = new Color(r, g, b);
				return this;
			}

			public Builder ColorMode(ColorMode mode)
			{
				sprite.colorMode = mode;
				return this;
			}

			public static implicit operator VoreSprite(Builder builder) => builder.sprite;
		}
	}

}