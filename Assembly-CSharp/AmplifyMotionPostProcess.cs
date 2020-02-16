using System;
using UnityEngine;

// Token: 0x02000294 RID: 660
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public sealed class AmplifyMotionPostProcess : MonoBehaviour
{
	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06001524 RID: 5412 RVA: 0x000A2DFE File Offset: 0x000A11FE
	// (set) Token: 0x06001525 RID: 5413 RVA: 0x000A2E06 File Offset: 0x000A1206
	public AmplifyMotionEffectBase Instance
	{
		get
		{
			return this.m_instance;
		}
		set
		{
			this.m_instance = value;
		}
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000A2E0F File Offset: 0x000A120F
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_instance != null)
		{
			this.m_instance.PostProcess(source, destination);
		}
	}

	// Token: 0x040011F7 RID: 4599
	private AmplifyMotionEffectBase m_instance;
}
