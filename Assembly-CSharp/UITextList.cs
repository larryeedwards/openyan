using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000283 RID: 643
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06001463 RID: 5219 RVA: 0x0009E2F0 File Offset: 0x0009C6F0
	protected BetterList<UITextList.Paragraph> paragraphs
	{
		get
		{
			if (this.mParagraphs == null && !UITextList.mHistory.TryGetValue(base.name, out this.mParagraphs))
			{
				this.mParagraphs = new BetterList<UITextList.Paragraph>();
				UITextList.mHistory.Add(base.name, this.mParagraphs);
			}
			return this.mParagraphs;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x06001464 RID: 5220 RVA: 0x0009E34A File Offset: 0x0009C74A
	public bool isValid
	{
		get
		{
			return this.textLabel != null && this.textLabel.ambigiousFont != null;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06001465 RID: 5221 RVA: 0x0009E371 File Offset: 0x0009C771
	// (set) Token: 0x06001466 RID: 5222 RVA: 0x0009E37C File Offset: 0x0009C77C
	public float scrollValue
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			value = Mathf.Clamp01(value);
			if (this.isValid && this.mScroll != value)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.value = value;
				}
				else
				{
					this.mScroll = value;
					this.UpdateVisibleText();
				}
			}
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001467 RID: 5223 RVA: 0x0009E3D7 File Offset: 0x0009C7D7
	protected float lineHeight
	{
		get
		{
			return (!(this.textLabel != null)) ? 20f : ((float)this.textLabel.fontSize + this.textLabel.effectiveSpacingY);
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001468 RID: 5224 RVA: 0x0009E40C File Offset: 0x0009C80C
	protected int scrollHeight
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			return Mathf.Max(0, this.mTotalLines - num);
		}
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x0009E44D File Offset: 0x0009C84D
	public void Clear()
	{
		this.paragraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x0009E460 File Offset: 0x0009C860
	private void Start()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		if (this.scrollBar != null)
		{
			EventDelegate.Add(this.scrollBar.onChange, new EventDelegate.Callback(this.OnScrollBar));
		}
		this.textLabel.overflowMethod = UILabel.Overflow.ClampContent;
		if (this.style == UITextList.Style.Chat)
		{
			this.textLabel.pivot = UIWidget.Pivot.BottomLeft;
			this.scrollValue = 1f;
		}
		else
		{
			this.textLabel.pivot = UIWidget.Pivot.TopLeft;
			this.scrollValue = 0f;
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0009E503 File Offset: 0x0009C903
	private void Update()
	{
		if (this.isValid && (this.textLabel.width != this.mLastWidth || this.textLabel.height != this.mLastHeight))
		{
			this.Rebuild();
		}
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0009E544 File Offset: 0x0009C944
	public void OnScroll(float val)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			val *= this.lineHeight;
			this.scrollValue = this.mScroll - val / (float)scrollHeight;
		}
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0009E57C File Offset: 0x0009C97C
	public void OnDrag(Vector2 delta)
	{
		int scrollHeight = this.scrollHeight;
		if (scrollHeight != 0)
		{
			float num = delta.y / this.lineHeight;
			this.scrollValue = this.mScroll + num / (float)scrollHeight;
		}
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0009E5B6 File Offset: 0x0009C9B6
	private void OnScrollBar()
	{
		this.mScroll = UIProgressBar.current.value;
		this.UpdateVisibleText();
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0009E5CE File Offset: 0x0009C9CE
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0009E5D8 File Offset: 0x0009C9D8
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.paragraphs.size < this.paragraphHistory)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		this.Rebuild();
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0009E63C File Offset: 0x0009CA3C
	protected void Rebuild()
	{
		if (this.isValid)
		{
			this.mLastWidth = this.textLabel.width;
			this.mLastHeight = this.textLabel.height;
			this.textLabel.UpdateNGUIText();
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
			this.mTotalLines = 0;
			for (int i = 0; i < this.paragraphs.size; i++)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[i];
				string text;
				NGUIText.WrapText(paragraph.text, out text, false, true, false);
				paragraph.lines = text.Split(new char[]
				{
					'\n'
				});
				this.mTotalLines += paragraph.lines.Length;
			}
			this.mTotalLines = 0;
			int j = 0;
			int size = this.mParagraphs.size;
			while (j < size)
			{
				this.mTotalLines += this.mParagraphs.buffer[j].lines.Length;
				j++;
			}
			if (this.scrollBar != null)
			{
				UIScrollBar uiscrollBar = this.scrollBar as UIScrollBar;
				if (uiscrollBar != null)
				{
					uiscrollBar.barSize = ((this.mTotalLines != 0) ? (1f - (float)this.scrollHeight / (float)this.mTotalLines) : 1f);
				}
			}
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x0009E7B0 File Offset: 0x0009CBB0
	protected void UpdateVisibleText()
	{
		if (this.isValid)
		{
			if (this.mTotalLines == 0)
			{
				this.textLabel.text = string.Empty;
				return;
			}
			int num = Mathf.FloorToInt((float)this.textLabel.height / this.lineHeight);
			int num2 = Mathf.Max(0, this.mTotalLines - num);
			int num3 = Mathf.RoundToInt(this.mScroll * (float)num2);
			if (num3 < 0)
			{
				num3 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			int size = this.paragraphs.size;
			while (num > 0 && num4 < size)
			{
				UITextList.Paragraph paragraph = this.mParagraphs.buffer[num4];
				int num5 = 0;
				int num6 = paragraph.lines.Length;
				while (num > 0 && num5 < num6)
				{
					string value = paragraph.lines[num5];
					if (num3 > 0)
					{
						num3--;
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append("\n");
						}
						stringBuilder.Append(value);
						num--;
					}
					num5++;
				}
				num4++;
			}
			this.textLabel.text = stringBuilder.ToString();
		}
	}

	// Token: 0x04001154 RID: 4436
	public UILabel textLabel;

	// Token: 0x04001155 RID: 4437
	public UIProgressBar scrollBar;

	// Token: 0x04001156 RID: 4438
	public UITextList.Style style;

	// Token: 0x04001157 RID: 4439
	public int paragraphHistory = 100;

	// Token: 0x04001158 RID: 4440
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x04001159 RID: 4441
	protected float mScroll;

	// Token: 0x0400115A RID: 4442
	protected int mTotalLines;

	// Token: 0x0400115B RID: 4443
	protected int mLastWidth;

	// Token: 0x0400115C RID: 4444
	protected int mLastHeight;

	// Token: 0x0400115D RID: 4445
	private BetterList<UITextList.Paragraph> mParagraphs;

	// Token: 0x0400115E RID: 4446
	private static Dictionary<string, BetterList<UITextList.Paragraph>> mHistory = new Dictionary<string, BetterList<UITextList.Paragraph>>();

	// Token: 0x02000284 RID: 644
	public enum Style
	{
		// Token: 0x04001160 RID: 4448
		Text,
		// Token: 0x04001161 RID: 4449
		Chat
	}

	// Token: 0x02000285 RID: 645
	protected class Paragraph
	{
		// Token: 0x04001162 RID: 4450
		public string text;

		// Token: 0x04001163 RID: 4451
		public string[] lines;
	}
}
