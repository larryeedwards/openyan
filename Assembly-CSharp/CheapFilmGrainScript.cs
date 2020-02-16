using System;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class CheapFilmGrainScript : MonoBehaviour
{
	// Token: 0x060017C8 RID: 6088 RVA: 0x000BDD7E File Offset: 0x000BC17E
	private void Update()
	{
		this.MyRenderer.material.mainTextureScale = new Vector2(UnityEngine.Random.Range(this.Floor, this.Ceiling), UnityEngine.Random.Range(this.Floor, this.Ceiling));
	}

	// Token: 0x040017E2 RID: 6114
	public Renderer MyRenderer;

	// Token: 0x040017E3 RID: 6115
	public float Floor = 100f;

	// Token: 0x040017E4 RID: 6116
	public float Ceiling = 200f;
}
