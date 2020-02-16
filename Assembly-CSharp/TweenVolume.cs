using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x1700022B RID: 555
	// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0008D1D0 File Offset: 0x0008B5D0
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.GetComponent<AudioSource>();
				if (this.mSource == null)
				{
					this.mSource = base.GetComponent<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0008D240 File Offset: 0x0008B640
	// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0008D248 File Offset: 0x0008B648
	[Obsolete("Use 'value' instead")]
	public float volume
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

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0008D251 File Offset: 0x0008B651
	// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0008D279 File Offset: 0x0008B679
	public float value
	{
		get
		{
			return (!(this.audioSource != null)) ? 0f : this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0008D298 File Offset: 0x0008B698
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0008D2D4 File Offset: 0x0008B6D4
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration, 0f);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		if (targetVolume > 0f)
		{
			AudioSource audioSource = tweenVolume.audioSource;
			audioSource.enabled = true;
			audioSource.Play();
		}
		return tweenVolume;
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x0008D321 File Offset: 0x0008B721
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0008D32F File Offset: 0x0008B72F
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04000F3C RID: 3900
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04000F3D RID: 3901
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04000F3E RID: 3902
	private AudioSource mSource;
}
