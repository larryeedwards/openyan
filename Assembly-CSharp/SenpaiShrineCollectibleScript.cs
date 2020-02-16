using System;
using UnityEngine;

// Token: 0x02000501 RID: 1281
public class SenpaiShrineCollectibleScript : MonoBehaviour
{
	// Token: 0x06001FDA RID: 8154 RVA: 0x0014671A File Offset: 0x00144B1A
	private void Start()
	{
		if (PlayerGlobals.GetShrineCollectible(this.ID))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x00146738 File Offset: 0x00144B38
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.ShrineCollectibles[this.ID] = true;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002BC8 RID: 11208
	public PromptScript Prompt;

	// Token: 0x04002BC9 RID: 11209
	public int ID;
}
