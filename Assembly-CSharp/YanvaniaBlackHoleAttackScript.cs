using System;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class YanvaniaBlackHoleAttackScript : MonoBehaviour
{
	// Token: 0x060022F4 RID: 8948 RVA: 0x001B7670 File Offset: 0x001B5A70
	private void Start()
	{
		this.Yanmont = GameObject.Find("YanmontChan").GetComponent<YanvaniaYanmontScript>();
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x001B7688 File Offset: 0x001B5A88
	private void Update()
	{
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yanmont.transform.position + Vector3.up, Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) > 10f || this.Yanmont.EnterCutscene)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x001B7714 File Offset: 0x001B5B14
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			this.Yanmont.TakeDamage(20);
		}
		if (other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BlackExplosion, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BE4 RID: 15332
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003BE5 RID: 15333
	public GameObject BlackExplosion;
}
