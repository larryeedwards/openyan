using System;
using UnityEngine;

// Token: 0x020005A2 RID: 1442
public class YanvaniaBossHeadScript : MonoBehaviour
{
	// Token: 0x060022FA RID: 8954 RVA: 0x001B784A File Offset: 0x001B5C4A
	private void Update()
	{
		this.Timer -= Time.deltaTime;
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x001B7860 File Offset: 0x001B5C60
	private void OnTriggerEnter(Collider other)
	{
		if (this.Timer <= 0f && this.Dracula.NewTeleportEffect == null && other.gameObject.name == "Heart")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, base.transform.position, Quaternion.identity);
			this.Timer = 1f;
			this.Dracula.TakeDamage();
		}
	}

	// Token: 0x04003BEA RID: 15338
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003BEB RID: 15339
	public GameObject HitEffect;

	// Token: 0x04003BEC RID: 15340
	public float Timer;
}
