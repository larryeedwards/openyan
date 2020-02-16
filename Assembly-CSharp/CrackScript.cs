using System;
using UnityEngine;

// Token: 0x0200037B RID: 891
public class CrackScript : MonoBehaviour
{
	// Token: 0x0600184F RID: 6223 RVA: 0x000D4DA1 File Offset: 0x000D31A1
	private void Update()
	{
		this.Texture.fillAmount += Time.deltaTime * 10f;
	}

	// Token: 0x04001AB7 RID: 6839
	public UITexture Texture;
}
