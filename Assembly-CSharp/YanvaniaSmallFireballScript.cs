using System;
using UnityEngine;

// Token: 0x020005AD RID: 1453
public class YanvaniaSmallFireballScript : MonoBehaviour
{
	// Token: 0x0600231D RID: 8989 RVA: 0x001B9A7C File Offset: 0x001B7E7C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (other.gameObject.name == "YanmontChan")
		{
			other.gameObject.GetComponent<YanvaniaYanmontScript>().TakeDamage(10);
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003C42 RID: 15426
	public GameObject Explosion;
}
