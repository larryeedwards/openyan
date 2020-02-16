using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class GazerHairScript : MonoBehaviour
{
	// Token: 0x060019D4 RID: 6612 RVA: 0x000F3584 File Offset: 0x000F1984
	private void Update()
	{
		this.ID = 0;
		while (this.ID < this.Weight.Length)
		{
			this.Weight[this.ID] = Mathf.MoveTowards(this.Weight[this.ID], this.TargetWeight[this.ID], Time.deltaTime * this.Strength);
			if (this.Weight[this.ID] == this.TargetWeight[this.ID])
			{
				this.TargetWeight[this.ID] = UnityEngine.Random.Range(0f, 100f);
			}
			this.MyMesh.SetBlendShapeWeight(this.ID, this.Weight[this.ID]);
			this.ID++;
		}
	}

	// Token: 0x04001F4A RID: 8010
	public SkinnedMeshRenderer MyMesh;

	// Token: 0x04001F4B RID: 8011
	public float[] TargetWeight;

	// Token: 0x04001F4C RID: 8012
	public float[] Weight;

	// Token: 0x04001F4D RID: 8013
	public float Strength = 100f;

	// Token: 0x04001F4E RID: 8014
	public int ID;
}
