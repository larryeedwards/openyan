using System;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class ExpressionMaskScript : MonoBehaviour
{
	// Token: 0x06001987 RID: 6535 RVA: 0x000EDC74 File Offset: 0x000EC074
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (this.ID < 3)
			{
				this.ID++;
			}
			else
			{
				this.ID = 0;
			}
			switch (this.ID)
			{
			case 0:
				this.Mask.material.mainTextureOffset = Vector2.zero;
				break;
			case 1:
				this.Mask.material.mainTextureOffset = new Vector2(0f, 0.5f);
				break;
			case 2:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0f);
				break;
			case 3:
				this.Mask.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
				break;
			}
		}
	}

	// Token: 0x04001E40 RID: 7744
	public Renderer Mask;

	// Token: 0x04001E41 RID: 7745
	public int ID;
}
