using System;

// Token: 0x02000280 RID: 640
[Serializable]
public class UISpriteData
{
	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06001455 RID: 5205 RVA: 0x0009D7FA File Offset: 0x0009BBFA
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06001456 RID: 5206 RVA: 0x0009D81D File Offset: 0x0009BC1D
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x0009D840 File Offset: 0x0009BC40
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x0009D85F File Offset: 0x0009BC5F
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x06001459 RID: 5209 RVA: 0x0009D87E File Offset: 0x0009BC7E
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x0009D8A0 File Offset: 0x0009BCA0
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x0009D949 File Offset: 0x0009BD49
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x0400112F RID: 4399
	public string name = "Sprite";

	// Token: 0x04001130 RID: 4400
	public int x;

	// Token: 0x04001131 RID: 4401
	public int y;

	// Token: 0x04001132 RID: 4402
	public int width;

	// Token: 0x04001133 RID: 4403
	public int height;

	// Token: 0x04001134 RID: 4404
	public int borderLeft;

	// Token: 0x04001135 RID: 4405
	public int borderRight;

	// Token: 0x04001136 RID: 4406
	public int borderTop;

	// Token: 0x04001137 RID: 4407
	public int borderBottom;

	// Token: 0x04001138 RID: 4408
	public int paddingLeft;

	// Token: 0x04001139 RID: 4409
	public int paddingRight;

	// Token: 0x0400113A RID: 4410
	public int paddingTop;

	// Token: 0x0400113B RID: 4411
	public int paddingBottom;
}
