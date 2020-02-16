using System;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class ScrollingTexture : MonoBehaviour
{
	// Token: 0x06001FD1 RID: 8145 RVA: 0x0014624A File Offset: 0x0014464A
	private void Start()
	{
		this.MyRenderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x00146258 File Offset: 0x00144658
	private void Update()
	{
		this.Offset += Time.deltaTime * this.Speed;
		this.MyRenderer.material.SetTextureOffset("_MainTex", new Vector2(this.Offset, this.Offset));
	}

	// Token: 0x04002BBB RID: 11195
	public Renderer MyRenderer;

	// Token: 0x04002BBC RID: 11196
	public float Offset;

	// Token: 0x04002BBD RID: 11197
	public float Speed;
}
