using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Amplify Motion")]
public class AmplifyMotionEffect : AmplifyMotionEffectBase
{
	// Token: 0x1700030A RID: 778
	// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000A1E41 File Offset: 0x000A0241
	public new static AmplifyMotionEffect FirstInstance
	{
		get
		{
			return (AmplifyMotionEffect)AmplifyMotionEffectBase.FirstInstance;
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x060014FA RID: 5370 RVA: 0x000A1E4D File Offset: 0x000A024D
	public new static AmplifyMotionEffect Instance
	{
		get
		{
			return (AmplifyMotionEffect)AmplifyMotionEffectBase.Instance;
		}
	}
}
