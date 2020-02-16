using System;
using UnityEngine;

// Token: 0x02000233 RID: 563
[RequireComponent(typeof(UIBasicSprite))]
[AddComponentMenu("NGUI/Tween/Tween Fill")]
public class TweenFill : UITweener
{
	// Token: 0x0600115F RID: 4447 RVA: 0x0008BF0A File Offset: 0x0008A30A
	private void Cache()
	{
		this.mCached = true;
		this.mSprite = base.GetComponent<UISprite>();
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06001160 RID: 4448 RVA: 0x0008BF1F File Offset: 0x0008A31F
	// (set) Token: 0x06001161 RID: 4449 RVA: 0x0008BF54 File Offset: 0x0008A354
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				return this.mSprite.fillAmount;
			}
			return 0f;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				this.mSprite.fillAmount = value;
			}
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0008BF84 File Offset: 0x0008A384
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x0008BFA0 File Offset: 0x0008A3A0
	public static TweenFill Begin(GameObject go, float duration, float fill)
	{
		TweenFill tweenFill = UITweener.Begin<TweenFill>(go, duration, 0f);
		tweenFill.from = tweenFill.value;
		tweenFill.to = fill;
		if (duration <= 0f)
		{
			tweenFill.Sample(1f, true);
			tweenFill.enabled = false;
		}
		return tweenFill;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0008BFEC File Offset: 0x0008A3EC
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0008BFFA File Offset: 0x0008A3FA
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000F01 RID: 3841
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000F02 RID: 3842
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000F03 RID: 3843
	private bool mCached;

	// Token: 0x04000F04 RID: 3844
	private UIBasicSprite mSprite;
}
