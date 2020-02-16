using System;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class GreenRoomScript : MonoBehaviour
{
	// Token: 0x06001C5A RID: 7258 RVA: 0x000FD596 File Offset: 0x000FB996
	private void Start()
	{
		this.QualityManager.Obscurance.enabled = false;
		this.UpdateColor();
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x000FD5AF File Offset: 0x000FB9AF
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.UpdateColor();
		}
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x000FD5C8 File Offset: 0x000FB9C8
	private void UpdateColor()
	{
		this.ID++;
		if (this.ID > 7)
		{
			this.ID = 0;
		}
		this.Renderers[0].material.color = this.Colors[this.ID];
		this.Renderers[1].material.color = this.Colors[this.ID];
	}

	// Token: 0x04002086 RID: 8326
	public QualityManagerScript QualityManager;

	// Token: 0x04002087 RID: 8327
	public Color[] Colors;

	// Token: 0x04002088 RID: 8328
	public Renderer[] Renderers;

	// Token: 0x04002089 RID: 8329
	public int ID;
}
