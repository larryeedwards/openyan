﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000592 RID: 1426
public class WelcomeScript : MonoBehaviour
{
	// Token: 0x0600227C RID: 8828 RVA: 0x001A1CD0 File Offset: 0x001A00D0
	private void Start()
	{
		Time.timeScale = 1f;
		this.BeginLabel.color = new Color(this.BeginLabel.color.r, this.BeginLabel.color.g, this.BeginLabel.color.b, 0f);
		this.AltBeginLabel.color = this.BeginLabel.color;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 2f);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (ApplicationGlobals.VersionNumber != this.VersionNumber)
		{
			ApplicationGlobals.VersionNumber = this.VersionNumber;
		}
		if (!Application.CanStreamedLevelBeLoaded("FunScene"))
		{
			Application.Quit();
		}
		if (File.Exists(Application.streamingAssetsPath + "/Fun.txt"))
		{
			this.Text = File.ReadAllText(Application.streamingAssetsPath + "/Fun.txt");
		}
		if (this.Text == "0" || this.Text == "1" || this.Text == "2" || this.Text == "3" || this.Text == "4" || this.Text == "5" || this.Text == "6" || this.Text == "7" || this.Text == "8" || this.Text == "9" || this.Text == "10" || this.Text == "69" || this.Text == "666")
		{
			SceneManager.LoadScene("VeryFunScene");
		}
		this.ID = 0;
		while (this.ID < 100)
		{
			if (this.ID != 10 && (this.JSON.Students[this.ID].Hairstyle == "21" || this.JSON.Students[this.ID].Persona == PersonaType.Protective))
			{
				Debug.Log("Player is cheating!");
				if (Application.CanStreamedLevelBeLoaded("FunScene"))
				{
					SceneManager.LoadScene("FunScene");
				}
			}
			this.ID++;
		}
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x001A1FC0 File Offset: 0x001A03C0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
		}
		if (!this.Continue)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a <= 0f)
			{
				if (Input.GetKeyDown(KeyCode.W))
				{
				}
				if (Input.anyKeyDown)
				{
					this.Timer = 5f;
				}
				this.Timer += Time.deltaTime;
				if (this.Timer > 5f)
				{
					this.BeginLabel.color = new Color(this.BeginLabel.color.r, this.BeginLabel.color.g, this.BeginLabel.color.b, this.BeginLabel.color.a + Time.deltaTime);
					this.AltBeginLabel.color = this.BeginLabel.color;
					if (this.BeginLabel.color.a >= 1f && Input.anyKeyDown)
					{
						this.Darkness.color = new Color(1f, 1f, 1f, 0f);
						this.Continue = true;
					}
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (!this.FlashRed)
		{
			this.ID = 0;
			while (this.ID < 3)
			{
				this.ID++;
				this.FlashingLabels[this.ID].color = new Color(this.FlashingLabels[this.ID].color.r + Time.deltaTime * 10f, this.FlashingLabels[this.ID].color.g, this.FlashingLabels[this.ID].color.b, this.FlashingLabels[this.ID].color.a);
				if (this.FlashingLabels[this.ID].color.r > 1f)
				{
					this.FlashRed = true;
				}
			}
		}
		else
		{
			this.ID = 0;
			while (this.ID < 3)
			{
				this.ID++;
				this.FlashingLabels[this.ID].color = new Color(this.FlashingLabels[this.ID].color.r - Time.deltaTime * 10f, this.FlashingLabels[this.ID].color.g, this.FlashingLabels[this.ID].color.b, this.FlashingLabels[this.ID].color.a);
				if (this.FlashingLabels[this.ID].color.r < 0f)
				{
					this.FlashRed = false;
				}
			}
		}
	}

	// Token: 0x0400385C RID: 14428
	[SerializeField]
	private JsonScript JSON;

	// Token: 0x0400385D RID: 14429
	[SerializeField]
	private GameObject WelcomePanel;

	// Token: 0x0400385E RID: 14430
	[SerializeField]
	private UILabel[] FlashingLabels;

	// Token: 0x0400385F RID: 14431
	[SerializeField]
	private UILabel AltBeginLabel;

	// Token: 0x04003860 RID: 14432
	[SerializeField]
	private UILabel BeginLabel;

	// Token: 0x04003861 RID: 14433
	[SerializeField]
	private UISprite Darkness;

	// Token: 0x04003862 RID: 14434
	[SerializeField]
	private bool Continue;

	// Token: 0x04003863 RID: 14435
	[SerializeField]
	private bool FlashRed;

	// Token: 0x04003864 RID: 14436
	[SerializeField]
	private float VersionNumber;

	// Token: 0x04003865 RID: 14437
	[SerializeField]
	private float Timer;

	// Token: 0x04003866 RID: 14438
	private string Text;

	// Token: 0x04003867 RID: 14439
	private int ID;
}
