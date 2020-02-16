using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[Serializable]
public class BMSymbol
{
	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000ECA RID: 3786 RVA: 0x000772B7 File Offset: 0x000756B7
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000772DB File Offset: 0x000756DB
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000ECC RID: 3788 RVA: 0x000772E3 File Offset: 0x000756E3
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000772EB File Offset: 0x000756EB
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000772F3 File Offset: 0x000756F3
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000ECF RID: 3791 RVA: 0x000772FB File Offset: 0x000756FB
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00077303 File Offset: 0x00075703
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0007730B File Offset: 0x0007570B
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x00077314 File Offset: 0x00075714
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((!(atlas != null)) ? null : atlas.GetSprite(this.spriteName));
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x04000D8B RID: 3467
	public string sequence;

	// Token: 0x04000D8C RID: 3468
	public string spriteName;

	// Token: 0x04000D8D RID: 3469
	private UISpriteData mSprite;

	// Token: 0x04000D8E RID: 3470
	private bool mIsValid;

	// Token: 0x04000D8F RID: 3471
	private int mLength;

	// Token: 0x04000D90 RID: 3472
	private int mOffsetX;

	// Token: 0x04000D91 RID: 3473
	private int mOffsetY;

	// Token: 0x04000D92 RID: 3474
	private int mWidth;

	// Token: 0x04000D93 RID: 3475
	private int mHeight;

	// Token: 0x04000D94 RID: 3476
	private int mAdvance;

	// Token: 0x04000D95 RID: 3477
	private Rect mUV;
}
