using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06001149 RID: 4425 RVA: 0x0008B96D File Offset: 0x00089D6D
	// (set) Token: 0x0600114A RID: 4426 RVA: 0x0008B975 File Offset: 0x00089D75
	[Obsolete("Use 'value' instead")]
	public float alpha
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0008B980 File Offset: 0x00089D80
	private void Cache()
	{
		this.mCached = true;
		this.mRect = base.GetComponent<UIRect>();
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mRect == null && this.mSr == null)
		{
			this.mLight = base.GetComponent<Light>();
			if (this.mLight == null)
			{
				Renderer component = base.GetComponent<Renderer>();
				if (component != null)
				{
					this.mMat = component.material;
				}
				if (this.mMat == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
				}
			}
			else
			{
				this.mBaseIntensity = this.mLight.intensity;
			}
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x0600114C RID: 4428 RVA: 0x0008BA40 File Offset: 0x00089E40
	// (set) Token: 0x0600114D RID: 4429 RVA: 0x0008BAD0 File Offset: 0x00089ED0
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				return this.mRect.alpha;
			}
			if (this.mSr != null)
			{
				return this.mSr.color.a;
			}
			return (!(this.mMat != null)) ? 1f : this.mMat.color.a;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				this.mRect.alpha = value;
			}
			else if (this.mSr != null)
			{
				Color color = this.mSr.color;
				color.a = value;
				this.mSr.color = color;
			}
			else if (this.mMat != null)
			{
				Color color2 = this.mMat.color;
				color2.a = value;
				this.mMat.color = color2;
			}
			else if (this.mLight != null)
			{
				this.mLight.intensity = this.mBaseIntensity * value;
			}
		}
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0008BBA0 File Offset: 0x00089FA0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0008BBBC File Offset: 0x00089FBC
	public static TweenAlpha Begin(GameObject go, float duration, float alpha, float delay = 0f)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration, delay);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0008BC04 File Offset: 0x0008A004
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0008BC12 File Offset: 0x0008A012
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000EF2 RID: 3826
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000EF3 RID: 3827
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000EF4 RID: 3828
	private bool mCached;

	// Token: 0x04000EF5 RID: 3829
	private UIRect mRect;

	// Token: 0x04000EF6 RID: 3830
	private Material mMat;

	// Token: 0x04000EF7 RID: 3831
	private Light mLight;

	// Token: 0x04000EF8 RID: 3832
	private SpriteRenderer mSr;

	// Token: 0x04000EF9 RID: 3833
	private float mBaseIntensity = 1f;
}
