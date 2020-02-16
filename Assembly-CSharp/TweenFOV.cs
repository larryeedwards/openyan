using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06001167 RID: 4455 RVA: 0x0008C026 File Offset: 0x0008A426
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

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06001168 RID: 4456 RVA: 0x0008C04B File Offset: 0x0008A44B
	// (set) Token: 0x06001169 RID: 4457 RVA: 0x0008C053 File Offset: 0x0008A453
	[Obsolete("Use 'value' instead")]
	public float fov
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

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x0600116A RID: 4458 RVA: 0x0008C05C File Offset: 0x0008A45C
	// (set) Token: 0x0600116B RID: 4459 RVA: 0x0008C069 File Offset: 0x0008A469
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0008C077 File Offset: 0x0008A477
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0008C098 File Offset: 0x0008A498
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration, 0f);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0008C0E4 File Offset: 0x0008A4E4
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0008C0F2 File Offset: 0x0008A4F2
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0008C100 File Offset: 0x0008A500
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0008C10E File Offset: 0x0008A50E
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000F05 RID: 3845
	public float from = 45f;

	// Token: 0x04000F06 RID: 3846
	public float to = 45f;

	// Token: 0x04000F07 RID: 3847
	private Camera mCam;
}
