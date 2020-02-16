using System;
using UnityEngine;

// Token: 0x02000564 RID: 1380
[Serializable]
public class AudioClipArrayWrapper : ArrayWrapper<AudioClip>
{
	// Token: 0x060021E5 RID: 8677 RVA: 0x0019ABFB File Offset: 0x00198FFB
	public AudioClipArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x0019AC04 File Offset: 0x00199004
	public AudioClipArrayWrapper(AudioClip[] elements) : base(elements)
	{
	}
}
