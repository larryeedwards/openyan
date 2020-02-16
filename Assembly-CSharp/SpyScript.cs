using System;
using UnityEngine;

// Token: 0x0200051A RID: 1306
public class SpyScript : MonoBehaviour
{
	// Token: 0x06002038 RID: 8248 RVA: 0x0014F110 File Offset: 0x0014D510
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spying_00");
			this.Yandere.CanMove = false;
			this.Phase++;
		}
		if (this.Phase == 1)
		{
			Quaternion b = Quaternion.LookRotation(this.SpyTarget.transform.position - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, b, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.SpySpot.position);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				if (this.Yandere.Inventory.DirectionalMic)
				{
					this.PromptBar.Label[0].text = "Record";
					this.CanRecord = true;
				}
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Yandere.MainCamera.enabled = false;
				this.SpyCamera.SetActive(true);
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.CanRecord && Input.GetButtonDown("A"))
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_spyRecord_00");
				this.Yandere.Microphone.SetActive(true);
				this.Recording = true;
			}
			if (Input.GetButtonDown("B"))
			{
				this.End();
			}
		}
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x0014F30C File Offset: 0x0014D70C
	public void End()
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Yandere.Microphone.SetActive(false);
		this.Yandere.MainCamera.enabled = true;
		this.Yandere.CanMove = true;
		this.SpyCamera.SetActive(false);
		this.Timer = 0f;
		this.Phase = 0;
	}

	// Token: 0x04002CF5 RID: 11509
	public PromptBarScript PromptBar;

	// Token: 0x04002CF6 RID: 11510
	public YandereScript Yandere;

	// Token: 0x04002CF7 RID: 11511
	public PromptScript Prompt;

	// Token: 0x04002CF8 RID: 11512
	public GameObject SpyCamera;

	// Token: 0x04002CF9 RID: 11513
	public Transform SpyTarget;

	// Token: 0x04002CFA RID: 11514
	public Transform SpySpot;

	// Token: 0x04002CFB RID: 11515
	public float Timer;

	// Token: 0x04002CFC RID: 11516
	public bool CanRecord;

	// Token: 0x04002CFD RID: 11517
	public bool Recording;

	// Token: 0x04002CFE RID: 11518
	public int Phase;
}
