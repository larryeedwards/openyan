using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class RealTime : MonoBehaviour
{
	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06001017 RID: 4119 RVA: 0x00083145 File Offset: 0x00081545
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06001018 RID: 4120 RVA: 0x0008314C File Offset: 0x0008154C
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
