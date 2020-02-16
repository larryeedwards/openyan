using System;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class CirnoIceAttackScript : MonoBehaviour
{
	// Token: 0x060017D8 RID: 6104 RVA: 0x000BE56D File Offset: 0x000BC96D
	private void Start()
	{
		Physics.IgnoreLayerCollision(18, 13, true);
		Physics.IgnoreLayerCollision(18, 18, true);
	}

	// Token: 0x060017D9 RID: 6105 RVA: 0x000BE584 File Offset: 0x000BC984
	private void OnCollisionEnter(Collision collision)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.IceExplosion, base.transform.position, Quaternion.identity);
		if (collision.gameObject.layer == 9)
		{
			StudentScript component = collision.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.SpawnAlarmDisc();
				component.BecomeRagdoll();
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040017FD RID: 6141
	public GameObject IceExplosion;
}
