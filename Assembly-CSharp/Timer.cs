using System;
using UnityEngine;

// Token: 0x02000585 RID: 1413
[Serializable]
public class Timer
{
	// Token: 0x0600223D RID: 8765 RVA: 0x0019BA25 File Offset: 0x00199E25
	public Timer(float targetSeconds)
	{
		this.currentSeconds = 0f;
		this.targetSeconds = targetSeconds;
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x0600223E RID: 8766 RVA: 0x0019BA3F File Offset: 0x00199E3F
	public float CurrentSeconds
	{
		get
		{
			return this.currentSeconds;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x0600223F RID: 8767 RVA: 0x0019BA47 File Offset: 0x00199E47
	public float TargetSeconds
	{
		get
		{
			return this.targetSeconds;
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x06002240 RID: 8768 RVA: 0x0019BA4F File Offset: 0x00199E4F
	public bool IsDone
	{
		get
		{
			return this.currentSeconds >= this.targetSeconds;
		}
	}

	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x06002241 RID: 8769 RVA: 0x0019BA62 File Offset: 0x00199E62
	public float Progress
	{
		get
		{
			return Mathf.Clamp01(this.currentSeconds / this.targetSeconds);
		}
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x0019BA76 File Offset: 0x00199E76
	public void Reset()
	{
		this.currentSeconds = 0f;
	}

	// Token: 0x06002243 RID: 8771 RVA: 0x0019BA83 File Offset: 0x00199E83
	public void SubtractTarget()
	{
		this.currentSeconds -= this.targetSeconds;
	}

	// Token: 0x06002244 RID: 8772 RVA: 0x0019BA98 File Offset: 0x00199E98
	public void Tick(float dt)
	{
		this.currentSeconds += dt;
	}

	// Token: 0x04003777 RID: 14199
	[SerializeField]
	private float currentSeconds;

	// Token: 0x04003778 RID: 14200
	[SerializeField]
	private float targetSeconds;
}
