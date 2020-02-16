using System;
using UnityEngine;

// Token: 0x020005AE RID: 1454
public class YanvaniaTeleportEffectScript : MonoBehaviour
{
	// Token: 0x0600231F RID: 8991 RVA: 0x001B9B28 File Offset: 0x001B7F28
	private void Start()
	{
		this.FirstBeam.material.color = new Color(this.FirstBeam.material.color.r, this.FirstBeam.material.color.g, this.FirstBeam.material.color.b, 0f);
		this.SecondBeam.material.color = new Color(this.SecondBeam.material.color.r, this.SecondBeam.material.color.g, this.SecondBeam.material.color.b, 0f);
		this.FirstBeam.transform.localScale = new Vector3(0f, this.FirstBeam.transform.localScale.y, 0f);
		this.SecondBeamParent.transform.localScale = new Vector3(this.SecondBeamParent.transform.localScale.x, 0f, this.SecondBeamParent.transform.localScale.z);
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x001B9C7F File Offset: 0x001B807F
	private void Update()
	{
	}

	// Token: 0x04003C43 RID: 15427
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003C44 RID: 15428
	public Transform SecondBeamParent;

	// Token: 0x04003C45 RID: 15429
	public Renderer SecondBeam;

	// Token: 0x04003C46 RID: 15430
	public Renderer FirstBeam;

	// Token: 0x04003C47 RID: 15431
	public bool InformedDracula;

	// Token: 0x04003C48 RID: 15432
	public float Timer;
}
