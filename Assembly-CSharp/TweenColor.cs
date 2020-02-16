using System;
using UnityEngine;

// Token: 0x02000232 RID: 562
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x06001153 RID: 4435 RVA: 0x0008BC40 File Offset: 0x0008A040
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			return;
		}
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mSr != null)
		{
			return;
		}
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
			return;
		}
		this.mLight = base.GetComponent<Light>();
		if (this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06001154 RID: 4436 RVA: 0x0008BCD9 File Offset: 0x0008A0D9
	// (set) Token: 0x06001155 RID: 4437 RVA: 0x0008BCE1 File Offset: 0x0008A0E1
	[Obsolete("Use 'value' instead")]
	public Color color
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

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06001156 RID: 4438 RVA: 0x0008BCEC File Offset: 0x0008A0EC
	// (set) Token: 0x06001157 RID: 4439 RVA: 0x0008BD84 File Offset: 0x0008A184
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			if (this.mSr != null)
			{
				return this.mSr.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			else if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			else if (this.mSr != null)
			{
				this.mSr.color = value;
			}
			else if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0008BE4E File Offset: 0x0008A24E
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x0008BE68 File Offset: 0x0008A268
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration, 0f);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0008BEB4 File Offset: 0x0008A2B4
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x0008BEC2 File Offset: 0x0008A2C2
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0008BED0 File Offset: 0x0008A2D0
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0008BEDE File Offset: 0x0008A2DE
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000EFA RID: 3834
	public Color from = Color.white;

	// Token: 0x04000EFB RID: 3835
	public Color to = Color.white;

	// Token: 0x04000EFC RID: 3836
	private bool mCached;

	// Token: 0x04000EFD RID: 3837
	private UIWidget mWidget;

	// Token: 0x04000EFE RID: 3838
	private Material mMat;

	// Token: 0x04000EFF RID: 3839
	private Light mLight;

	// Token: 0x04000F00 RID: 3840
	private SpriteRenderer mSr;
}
