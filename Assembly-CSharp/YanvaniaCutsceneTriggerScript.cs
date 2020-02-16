using System;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class YanvaniaCutsceneTriggerScript : MonoBehaviour
{
	// Token: 0x06002305 RID: 8965 RVA: 0x001B7C7C File Offset: 0x001B607C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "YanmontChan")
		{
			this.BossBattleWall.SetActive(true);
			this.Yanmont.EnterCutscene = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BF9 RID: 15353
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003BFA RID: 15354
	public GameObject BossBattleWall;
}
