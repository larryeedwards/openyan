using System;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class FallingOsanaScript : MonoBehaviour
{
	// Token: 0x06001998 RID: 6552 RVA: 0x000EEEAC File Offset: 0x000ED2AC
	private void Update()
	{
		if (base.transform.parent.position.y > 0f)
		{
			this.Osana.CharacterAnimation.Play(this.Osana.IdleAnim);
			base.transform.parent.position += new Vector3(0f, -1.0001f, 0f);
		}
		if (base.transform.parent.position.y < 0f)
		{
			base.transform.parent.position = new Vector3(base.transform.parent.position.x, 0f, base.transform.parent.position.z);
			UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.parent.position, Quaternion.identity);
		}
	}

	// Token: 0x04001E84 RID: 7812
	public StudentScript Osana;

	// Token: 0x04001E85 RID: 7813
	public GameObject GroundImpact;
}
