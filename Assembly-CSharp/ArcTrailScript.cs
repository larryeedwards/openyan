using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class ArcTrailScript : MonoBehaviour
{
	// Token: 0x0600170E RID: 5902 RVA: 0x000B26FE File Offset: 0x000B0AFE
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Trail.material.SetColor("_TintColor", ArcTrailScript.TRAIL_TINT_COLOR);
		}
	}

	// Token: 0x04001647 RID: 5703
	private static readonly Color TRAIL_TINT_COLOR = new Color(1f, 0f, 0f, 1f);

	// Token: 0x04001648 RID: 5704
	public TrailRenderer Trail;
}
