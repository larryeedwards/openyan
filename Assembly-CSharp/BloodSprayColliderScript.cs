using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class BloodSprayColliderScript : MonoBehaviour
{
	// Token: 0x0600175D RID: 5981 RVA: 0x000B86FC File Offset: 0x000B6AFC
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 13)
		{
			YandereScript component = other.gameObject.GetComponent<YandereScript>();
			if (component != null)
			{
				component.Bloodiness = 100f;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
