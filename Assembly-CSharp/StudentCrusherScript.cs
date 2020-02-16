using System;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class StudentCrusherScript : MonoBehaviour
{
	// Token: 0x06002079 RID: 8313 RVA: 0x00153F90 File Offset: 0x00152390
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.layer == 9)
		{
			StudentScript component = other.transform.root.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (this.Mecha.Speed > 0.9f)
				{
					UnityEngine.Object.Instantiate<GameObject>(component.BloodyScream, base.transform.position + Vector3.up, Quaternion.identity);
					component.BecomeRagdoll();
				}
				if (this.Mecha.Speed > 5f)
				{
					component.Ragdoll.Dismember();
				}
			}
		}
	}

	// Token: 0x04002DDD RID: 11741
	public MechaScript Mecha;
}
