using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class BlendshapeScript : MonoBehaviour
{
	// Token: 0x0600174B RID: 5963 RVA: 0x000B7AB4 File Offset: 0x000B5EB4
	private void LateUpdate()
	{
		this.Happiness += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(0, this.Happiness);
		this.Blink += Time.deltaTime * 10f;
		this.MyMesh.SetBlendShapeWeight(8, 100f);
	}

	// Token: 0x040016C3 RID: 5827
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x040016C4 RID: 5828
	public float Happiness;

	// Token: 0x040016C5 RID: 5829
	public float Blink;
}
