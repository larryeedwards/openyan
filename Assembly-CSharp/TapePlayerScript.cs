using System;
using UnityEngine;

// Token: 0x02000541 RID: 1345
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x06002162 RID: 8546 RVA: 0x00193669 File Offset: 0x00191A69
	private void Start()
	{
		this.Tape.SetActive(false);
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x00193678 File Offset: 0x00191A78
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.HeartCamera.enabled = false;
			this.Yandere.RPGCamera.enabled = false;
			this.TapePlayerMenu.TimeBar.gameObject.SetActive(true);
			this.TapePlayerMenu.List.gameObject.SetActive(true);
			this.TapePlayerCamera.enabled = true;
			this.TapePlayerMenu.UpdateLabels();
			this.TapePlayerMenu.Show = true;
			this.NoteWindow.SetActive(false);
			this.Yandere.CanMove = false;
			this.Yandere.HUD.alpha = 0f;
			Time.timeScale = 0.0001f;
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[1].text = "EXIT";
			this.PromptBar.Label[4].text = "CHOOSE";
			this.PromptBar.Label[5].text = "CATEGORY";
			this.TapePlayerMenu.CheckSelection();
			this.PromptBar.Show = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spin)
		{
			Transform transform = this.Rolls[0];
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.0166666675f * (360f * this.SpinSpeed), transform.localEulerAngles.z);
			Transform transform2 = this.Rolls[1];
			transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x, transform2.localEulerAngles.y + 0.0166666675f * (360f * this.SpinSpeed), transform2.localEulerAngles.z);
		}
		if (this.FastForward)
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 6.25f, 1.66666663f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 2f;
		}
		else
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 0f, 1.66666663f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 1f;
		}
		if (this.Rewind)
		{
			this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 6.25f, 1.66666663f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
			this.SpinSpeed = -2f;
		}
		else
		{
			this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 0f, 1.66666663f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
		}
	}

	// Token: 0x040035C6 RID: 13766
	public TapePlayerMenuScript TapePlayerMenu;

	// Token: 0x040035C7 RID: 13767
	public PromptBarScript PromptBar;

	// Token: 0x040035C8 RID: 13768
	public YandereScript Yandere;

	// Token: 0x040035C9 RID: 13769
	public PromptScript Prompt;

	// Token: 0x040035CA RID: 13770
	public Transform RWButton;

	// Token: 0x040035CB RID: 13771
	public Transform FFButton;

	// Token: 0x040035CC RID: 13772
	public Camera TapePlayerCamera;

	// Token: 0x040035CD RID: 13773
	public Transform[] Rolls;

	// Token: 0x040035CE RID: 13774
	public GameObject NoteWindow;

	// Token: 0x040035CF RID: 13775
	public GameObject Tape;

	// Token: 0x040035D0 RID: 13776
	public bool FastForward;

	// Token: 0x040035D1 RID: 13777
	public bool Rewind;

	// Token: 0x040035D2 RID: 13778
	public bool Spin;

	// Token: 0x040035D3 RID: 13779
	public float SpinSpeed;
}
