using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001A6 RID: 422
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Interaction/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00068DDD File Offset: 0x000671DD
	public bool isActive
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00068DE5 File Offset: 0x000671E5
	public void ResetToBeginning()
	{
		this.Finish();
		this.mReset = true;
		this.mActive = true;
		this.mNextChar = 0f;
		this.mCurrentOffset = 0;
		this.Update();
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00068E14 File Offset: 0x00067214
	public void Finish()
	{
		if (this.mActive)
		{
			this.mActive = false;
			if (!this.mReset)
			{
				this.mCurrentOffset = this.mFullText.Length;
				this.mFade.Clear();
				this.mLabel.text = this.mFullText;
			}
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
		}
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00068EA9 File Offset: 0x000672A9
	private void OnEnable()
	{
		this.mReset = true;
		this.mActive = true;
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00068EB9 File Offset: 0x000672B9
	private void OnDisable()
	{
		this.Finish();
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00068EC4 File Offset: 0x000672C4
	private void Update()
	{
		if (!this.mActive)
		{
			return;
		}
		if (this.mReset)
		{
			this.mCurrentOffset = 0;
			this.mReset = false;
			this.mLabel = base.GetComponent<UILabel>();
			this.mFullText = this.mLabel.processedText;
			this.mFade.Clear();
			if (this.keepFullDimensions && this.scrollView != null)
			{
				this.scrollView.UpdatePosition();
			}
		}
		if (string.IsNullOrEmpty(this.mFullText))
		{
			return;
		}
		int length = this.mFullText.Length;
		while (this.mCurrentOffset < length && this.mNextChar <= RealTime.time)
		{
			int num = this.mCurrentOffset;
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			if (this.mLabel.supportEncoding)
			{
				while (NGUIText.ParseSymbol(this.mFullText, ref this.mCurrentOffset))
				{
				}
			}
			this.mCurrentOffset++;
			if (this.mCurrentOffset > length)
			{
				break;
			}
			float num2 = 1f / (float)this.charsPerSecond;
			char c = (num >= length) ? '\n' : this.mFullText[num];
			if (c == '\n')
			{
				num2 += this.delayOnNewLine;
			}
			else if (num + 1 == length || this.mFullText[num + 1] <= ' ')
			{
				if (c == '.')
				{
					if (num + 2 < length && this.mFullText[num + 1] == '.' && this.mFullText[num + 2] == '.')
					{
						num2 += this.delayOnPeriod * 3f;
						num += 2;
					}
					else
					{
						num2 += this.delayOnPeriod;
					}
				}
				else if (c == '!' || c == '?')
				{
					num2 += this.delayOnPeriod;
				}
			}
			if (this.mNextChar == 0f)
			{
				this.mNextChar = RealTime.time + num2;
			}
			else
			{
				this.mNextChar += num2;
			}
			if (this.fadeInTime != 0f)
			{
				TypewriterEffect.FadeEntry item = default(TypewriterEffect.FadeEntry);
				item.index = num;
				item.alpha = 0f;
				item.text = this.mFullText.Substring(num, this.mCurrentOffset - num);
				this.mFade.Add(item);
			}
			else
			{
				this.mLabel.text = ((!this.keepFullDimensions) ? this.mFullText.Substring(0, this.mCurrentOffset) : (this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset)));
				if (!this.keepFullDimensions && this.scrollView != null)
				{
					this.scrollView.UpdatePosition();
				}
				if (this.Voice != null)
				{
					base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
					base.GetComponent<AudioSource>().PlayOneShot(this.Voice);
				}
			}
		}
		if (this.mCurrentOffset >= length && this.mFade.size == 0)
		{
			this.mLabel.text = this.mFullText;
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
			this.mActive = false;
		}
		else if (this.mFade.size != 0)
		{
			int i = 0;
			while (i < this.mFade.size)
			{
				TypewriterEffect.FadeEntry value = this.mFade[i];
				value.alpha += RealTime.deltaTime / this.fadeInTime;
				if (value.alpha < 1f)
				{
					this.mFade[i] = value;
					i++;
				}
				else
				{
					this.mFade.RemoveAt(i);
				}
			}
			if (this.mFade.size == 0)
			{
				if (this.keepFullDimensions)
				{
					this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset) + "[00]" + this.mFullText.Substring(this.mCurrentOffset);
				}
				else
				{
					this.mLabel.text = this.mFullText.Substring(0, this.mCurrentOffset);
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < this.mFade.size; j++)
				{
					TypewriterEffect.FadeEntry fadeEntry = this.mFade[j];
					if (j == 0)
					{
						stringBuilder.Append(this.mFullText.Substring(0, fadeEntry.index));
					}
					stringBuilder.Append('[');
					stringBuilder.Append(NGUIText.EncodeAlpha(fadeEntry.alpha));
					stringBuilder.Append(']');
					stringBuilder.Append(fadeEntry.text);
				}
				if (this.keepFullDimensions)
				{
					stringBuilder.Append("[00]");
					stringBuilder.Append(this.mFullText.Substring(this.mCurrentOffset));
				}
				this.mLabel.text = stringBuilder.ToString();
			}
		}
		else if (this.mCurrentOffset == this.mFullText.Length)
		{
			TypewriterEffect.current = this;
			EventDelegate.Execute(this.onFinished);
			TypewriterEffect.current = null;
			this.mActive = false;
		}
	}

	// Token: 0x04000B39 RID: 2873
	public static TypewriterEffect current;

	// Token: 0x04000B3A RID: 2874
	public int charsPerSecond = 20;

	// Token: 0x04000B3B RID: 2875
	public float fadeInTime;

	// Token: 0x04000B3C RID: 2876
	public float delayOnPeriod;

	// Token: 0x04000B3D RID: 2877
	public float delayOnNewLine;

	// Token: 0x04000B3E RID: 2878
	public UIScrollView scrollView;

	// Token: 0x04000B3F RID: 2879
	public bool keepFullDimensions;

	// Token: 0x04000B40 RID: 2880
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000B41 RID: 2881
	public UILabel mLabel;

	// Token: 0x04000B42 RID: 2882
	public string mFullText = string.Empty;

	// Token: 0x04000B43 RID: 2883
	public int mCurrentOffset;

	// Token: 0x04000B44 RID: 2884
	private float mNextChar;

	// Token: 0x04000B45 RID: 2885
	private bool mReset = true;

	// Token: 0x04000B46 RID: 2886
	public bool mActive;

	// Token: 0x04000B47 RID: 2887
	public bool delayOnComma;

	// Token: 0x04000B48 RID: 2888
	private BetterList<TypewriterEffect.FadeEntry> mFade = new BetterList<TypewriterEffect.FadeEntry>();

	// Token: 0x04000B49 RID: 2889
	public AudioClip Voice;

	// Token: 0x020001A7 RID: 423
	private struct FadeEntry
	{
		// Token: 0x04000B4A RID: 2890
		public int index;

		// Token: 0x04000B4B RID: 2891
		public string text;

		// Token: 0x04000B4C RID: 2892
		public float alpha;
	}
}
