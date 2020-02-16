using System;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
public class RainbowScript : MonoBehaviour
{
	// Token: 0x06001F06 RID: 7942 RVA: 0x0013C600 File Offset: 0x0013AA00
	private void Start()
	{
		this.MyRenderer.material.color = Color.red;
		this.cyclesPerSecond = 0.25f;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x0013C624 File Offset: 0x0013AA24
	private void Update()
	{
		this.percent = (this.percent + Time.deltaTime * this.cyclesPerSecond) % 1f;
		this.MyRenderer.material.color = Color.HSVToRGB(this.percent, 1f, 1f);
	}

	// Token: 0x04002974 RID: 10612
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x04002975 RID: 10613
	[SerializeField]
	private float cyclesPerSecond;

	// Token: 0x04002976 RID: 10614
	[SerializeField]
	private float percent;
}
