using System;
using System.Collections.Generic;

// Token: 0x020001F6 RID: 502
[Serializable]
public class BMGlyph
{
	// Token: 0x06000EC6 RID: 3782 RVA: 0x000770FC File Offset: 0x000754FC
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null && previousChar != 0)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0007715C File Offset: 0x0007555C
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x000771D8 File Offset: 0x000755D8
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x04000D81 RID: 3457
	public int index;

	// Token: 0x04000D82 RID: 3458
	public int x;

	// Token: 0x04000D83 RID: 3459
	public int y;

	// Token: 0x04000D84 RID: 3460
	public int width;

	// Token: 0x04000D85 RID: 3461
	public int height;

	// Token: 0x04000D86 RID: 3462
	public int offsetX;

	// Token: 0x04000D87 RID: 3463
	public int offsetY;

	// Token: 0x04000D88 RID: 3464
	public int advance;

	// Token: 0x04000D89 RID: 3465
	public int channel;

	// Token: 0x04000D8A RID: 3466
	public List<int> kerning;
}
