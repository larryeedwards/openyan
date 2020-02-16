using System;
using UnityEngine;

// Token: 0x020005A1 RID: 1441
public class YanvaniaBlackHoleScript : MonoBehaviour
{
	// Token: 0x060022F8 RID: 8952 RVA: 0x001B77B0 File Offset: 0x001B5BB0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			this.SpawnTimer -= Time.deltaTime;
			if (this.SpawnTimer <= 0f && this.Attacks < 5)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHoleAttack, base.transform.position, Quaternion.identity);
				this.SpawnTimer = 0.5f;
				this.Attacks++;
			}
		}
	}

	// Token: 0x04003BE6 RID: 15334
	public GameObject BlackHoleAttack;

	// Token: 0x04003BE7 RID: 15335
	public int Attacks;

	// Token: 0x04003BE8 RID: 15336
	public float SpawnTimer;

	// Token: 0x04003BE9 RID: 15337
	public float Timer;
}
