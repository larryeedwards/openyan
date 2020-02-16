using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F5 RID: 501
[Serializable]
public class BMFont
{
	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00076F52 File Offset: 0x00075352
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00076F62 File Offset: 0x00075362
	// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x00076F6A File Offset: 0x0007536A
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00076F73 File Offset: 0x00075373
	// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00076F7B File Offset: 0x0007537B
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00076F84 File Offset: 0x00075384
	// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00076F8C File Offset: 0x0007538C
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00076F95 File Offset: 0x00075395
	// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00076F9D File Offset: 0x0007539D
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00076FA6 File Offset: 0x000753A6
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00076FC4 File Offset: 0x000753C4
	// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00076FCC File Offset: 0x000753CC
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00076FD5 File Offset: 0x000753D5
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00076FE0 File Offset: 0x000753E0
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0007707C File Offset: 0x0007547C
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00077086 File Offset: 0x00075486
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x000770A0 File Offset: 0x000754A0
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04000D7A RID: 3450
	[HideInInspector]
	[SerializeField]
	private int mSize = 16;

	// Token: 0x04000D7B RID: 3451
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x04000D7C RID: 3452
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x04000D7D RID: 3453
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x04000D7E RID: 3454
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x04000D7F RID: 3455
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x04000D80 RID: 3456
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
