using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
	// Token: 0x1700021F RID: 543
	// (get) Token: 0x0600118C RID: 4492 RVA: 0x0008C96F File Offset: 0x0008AD6F
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x0600118D RID: 4493 RVA: 0x0008C994 File Offset: 0x0008AD94
	// (set) Token: 0x0600118E RID: 4494 RVA: 0x0008C99C File Offset: 0x0008AD9C
	[Obsolete("Use 'value' instead")]
	public float orthoSize
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

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x0600118F RID: 4495 RVA: 0x0008C9A5 File Offset: 0x0008ADA5
	// (set) Token: 0x06001190 RID: 4496 RVA: 0x0008C9B2 File Offset: 0x0008ADB2
	public float value
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x0008C9C0 File Offset: 0x0008ADC0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x0008C9E0 File Offset: 0x0008ADE0
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration, 0f);
		tweenOrthoSize.from = tweenOrthoSize.value;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0008CA2C File Offset: 0x0008AE2C
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0008CA3A File Offset: 0x0008AE3A
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000F24 RID: 3876
	public float from = 1f;

	// Token: 0x04000F25 RID: 3877
	public float to = 1f;

	// Token: 0x04000F26 RID: 3878
	private Camera mCam;
}
