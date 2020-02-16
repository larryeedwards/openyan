using System;
using UnityEngine;

// Token: 0x02000523 RID: 1315
public class StolenPhoneSpotScript : MonoBehaviour
{
	// Token: 0x06002057 RID: 8279 RVA: 0x00151890 File Offset: 0x0014FC90
	private void Update()
	{
		if (this.Prompt.Yandere.Inventory.RivalPhone)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				if (SchemeGlobals.GetSchemeStage(1) == 6)
				{
					SchemeGlobals.SetSchemeStage(1, 7);
					this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
				this.Prompt.Yandere.SmartphoneRenderer.material.mainTexture = this.Prompt.Yandere.YanderePhoneTexture;
				this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.RivalPhone = false;
				this.Prompt.Yandere.RivalPhone = false;
				this.RivalPhone.transform.parent = null;
				if (this.PhoneSpot == null)
				{
					this.RivalPhone.transform.position = base.transform.position;
				}
				else
				{
					this.RivalPhone.transform.position = this.PhoneSpot.position;
				}
				this.RivalPhone.transform.eulerAngles = base.transform.eulerAngles;
				this.RivalPhone.SetActive(true);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04002D50 RID: 11600
	public PromptScript Prompt;

	// Token: 0x04002D51 RID: 11601
	public GameObject RivalPhone;

	// Token: 0x04002D52 RID: 11602
	public Transform PhoneSpot;
}
