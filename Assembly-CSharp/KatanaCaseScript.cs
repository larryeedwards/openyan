using System;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class KatanaCaseScript : MonoBehaviour
{
	// Token: 0x06001D54 RID: 7508 RVA: 0x00112A2E File Offset: 0x00110E2E
	private void Start()
	{
		this.CasePrompt.enabled = false;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x00112A3C File Offset: 0x00110E3C
	private void Update()
	{
		if (this.Key.activeInHierarchy && this.KeyPrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = true;
			this.CasePrompt.HideButton[0] = false;
			this.CasePrompt.enabled = true;
			this.Key.SetActive(false);
		}
		if (this.CasePrompt.Circle[0].fillAmount == 0f)
		{
			this.KeyPrompt.Yandere.Inventory.CaseKey = false;
			this.Open = true;
			this.CasePrompt.Hide();
			this.CasePrompt.enabled = false;
		}
		if (this.CasePrompt.Yandere.Inventory.LockPick)
		{
			this.CasePrompt.HideButton[2] = false;
			this.CasePrompt.enabled = true;
			if (this.CasePrompt.Circle[2].fillAmount == 0f)
			{
				this.KeyPrompt.Hide();
				this.KeyPrompt.enabled = false;
				this.CasePrompt.Yandere.Inventory.LockPick = false;
				this.CasePrompt.Label[0].text = "     Open";
				this.CasePrompt.HideButton[2] = true;
				this.CasePrompt.HideButton[0] = true;
				this.Open = true;
			}
		}
		else if (!this.CasePrompt.HideButton[2])
		{
			this.CasePrompt.HideButton[2] = true;
		}
		if (this.Open)
		{
			this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * 10f);
			this.Door.eulerAngles = new Vector3(this.Door.eulerAngles.x, this.Door.eulerAngles.y, this.Rotation);
			if (this.Rotation < -179.9f)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002442 RID: 9282
	public PromptScript CasePrompt;

	// Token: 0x04002443 RID: 9283
	public PromptScript KeyPrompt;

	// Token: 0x04002444 RID: 9284
	public Transform Door;

	// Token: 0x04002445 RID: 9285
	public GameObject Key;

	// Token: 0x04002446 RID: 9286
	public float Rotation;

	// Token: 0x04002447 RID: 9287
	public bool Open;
}
