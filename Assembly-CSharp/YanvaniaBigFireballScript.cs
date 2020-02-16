using System;
using UnityEngine;

// Token: 0x0200059F RID: 1439
public class YanvaniaBigFireballScript : MonoBehaviour
{
	// Token: 0x060022F2 RID: 8946 RVA: 0x001B7608 File Offset: 0x001B5A08
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(15);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BE3 RID: 15331
	public GameObject Explosion;
}
