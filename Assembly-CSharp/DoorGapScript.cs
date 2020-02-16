using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class DoorGapScript : MonoBehaviour
{
	// Token: 0x060018E1 RID: 6369 RVA: 0x000E4339 File Offset: 0x000E2739
	private void Start()
	{
		this.Papers[1].gameObject.SetActive(false);
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000E4350 File Offset: 0x000E2750
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Phase == 1)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.Yandere.Inventory.AnswerSheet = false;
				this.Papers[1].gameObject.SetActive(true);
				SchemeGlobals.SetSchemeStage(5, 6);
				this.Schemes.UpdateInstructions();
				base.GetComponent<AudioSource>().Play();
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Yandere.Inventory.DuplicateSheet = true;
				this.Papers[2].gameObject.SetActive(false);
				this.RummageSpot.Prompt.Label[0].text = "     Return Answer Sheet";
				this.RummageSpot.Prompt.enabled = true;
				SchemeGlobals.SetSchemeStage(5, 7);
				this.Schemes.UpdateInstructions();
			}
			this.Phase++;
		}
		if (this.Phase == 2)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 4f)
			{
				this.Prompt.Label[0].text = "     Pick Up Sheets";
				this.Prompt.enabled = true;
				this.Phase = 2;
			}
			else if (this.Timer > 3f)
			{
				Transform transform = this.Papers[2];
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, -0.166f, Time.deltaTime * 10f));
			}
			else if (this.Timer > 1f)
			{
				Transform transform2 = this.Papers[1];
				transform2.localPosition = new Vector3(transform2.localPosition.x, transform2.localPosition.y, Mathf.Lerp(transform2.localPosition.z, 0.166f, Time.deltaTime * 10f));
			}
		}
	}

	// Token: 0x04001CA4 RID: 7332
	public RummageSpotScript RummageSpot;

	// Token: 0x04001CA5 RID: 7333
	public SchemesScript Schemes;

	// Token: 0x04001CA6 RID: 7334
	public PromptScript Prompt;

	// Token: 0x04001CA7 RID: 7335
	public Transform[] Papers;

	// Token: 0x04001CA8 RID: 7336
	public float Timer;

	// Token: 0x04001CA9 RID: 7337
	public int Phase = 1;
}
