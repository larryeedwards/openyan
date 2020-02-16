using System;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class ChainScript : MonoBehaviour
{
	// Token: 0x060017BE RID: 6078 RVA: 0x000BD404 File Offset: 0x000BB804
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.MysteriousKeys > 0)
			{
				AudioSource.PlayClipAtPoint(this.ChainRattle, base.transform.position);
				this.Unlocked++;
				this.Chains[this.Unlocked].SetActive(false);
				this.Prompt.Yandere.Inventory.MysteriousKeys--;
				if (this.Unlocked == 5)
				{
					this.Tarp.Prompt.enabled = true;
					this.Tarp.enabled = true;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x040017BE RID: 6078
	public PromptScript Prompt;

	// Token: 0x040017BF RID: 6079
	public TarpScript Tarp;

	// Token: 0x040017C0 RID: 6080
	public AudioClip ChainRattle;

	// Token: 0x040017C1 RID: 6081
	public GameObject[] Chains;

	// Token: 0x040017C2 RID: 6082
	public int Unlocked;
}
