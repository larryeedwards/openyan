using System;
using UnityEngine;

// Token: 0x020003C0 RID: 960
[Serializable]
public class WitnessMemory
{
	// Token: 0x06001961 RID: 6497 RVA: 0x000EC950 File Offset: 0x000EAD50
	public WitnessMemory()
	{
		this.memories = new float[Enum.GetValues(typeof(WitnessMemoryType)).Length];
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] = float.PositiveInfinity;
		}
		this.memorySpan = 1800f;
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000EC9B3 File Offset: 0x000EADB3
	public bool Remembers(WitnessMemoryType type)
	{
		return this.memories[(int)type] < this.memorySpan;
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000EC9C5 File Offset: 0x000EADC5
	public void Refresh(WitnessMemoryType type)
	{
		this.memories[(int)type] = 0f;
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x000EC9D4 File Offset: 0x000EADD4
	public void Tick(float dt)
	{
		for (int i = 0; i < this.memories.Length; i++)
		{
			this.memories[i] += dt;
		}
	}

	// Token: 0x04001E08 RID: 7688
	[SerializeField]
	private float[] memories;

	// Token: 0x04001E09 RID: 7689
	[SerializeField]
	private float memorySpan;

	// Token: 0x04001E0A RID: 7690
	private const float LongMemorySpan = 28800f;

	// Token: 0x04001E0B RID: 7691
	private const float MediumMemorySpan = 7200f;

	// Token: 0x04001E0C RID: 7692
	private const float ShortMemorySpan = 1800f;

	// Token: 0x04001E0D RID: 7693
	private const float VeryShortMemorySpan = 120f;
}
