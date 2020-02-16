using System;
using UnityEngine;

// Token: 0x02000546 RID: 1350
public class TaskWindowScript : MonoBehaviour
{
	// Token: 0x06002172 RID: 8562 RVA: 0x0019490F File Offset: 0x00192D0F
	private void Start()
	{
		this.Window.SetActive(false);
		this.UpdateTaskObjects(30);
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x00194928 File Offset: 0x00192D28
	public void UpdateWindow(int ID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[1].text = "Refuse";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
		this.GetPortrait(ID);
		this.StudentID = ID;
		this.GenericCheck();
		if (this.Generic)
		{
			ID = 0;
			this.Generic = false;
		}
		this.TaskDescLabel.transform.parent.gameObject.SetActive(true);
		this.TaskDescLabel.text = this.Descriptions[ID];
		this.Icon.mainTexture = this.Icons[ID];
		this.Window.SetActive(true);
		Time.timeScale = 0.0001f;
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x00194A08 File Offset: 0x00192E08
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (Input.GetButtonDown("A"))
			{
				TaskGlobals.SetTaskStatus(this.StudentID, 1);
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 4;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				this.UpdateTaskObjects(this.StudentID);
				Time.timeScale = 1f;
			}
			else if (Input.GetButtonDown("B"))
			{
				this.Yandere.TargetStudent.TalkTimer = 100f;
				this.Yandere.TargetStudent.Interaction = StudentInteractionType.GivingTask;
				this.Yandere.TargetStudent.TaskPhase = 0;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Window.SetActive(false);
				Time.timeScale = 1f;
			}
		}
		if (this.TaskComplete)
		{
			if (this.TrueTimer == 0f)
			{
				base.GetComponent<AudioSource>().Play();
			}
			this.TrueTimer += Time.deltaTime;
			this.Timer += Time.deltaTime;
			if (this.ID < this.TaskCompleteLetters.Length && this.Timer > 0.05f)
			{
				this.TaskCompleteLetters[this.ID].SetActive(true);
				this.Timer = 0f;
				this.ID++;
			}
			if (this.TaskCompleteLetters[12].transform.localPosition.y < -725f)
			{
				this.ID = 0;
				while (this.ID < this.TaskCompleteLetters.Length)
				{
					this.TaskCompleteLetters[this.ID].GetComponent<GrowShrinkScript>().Return();
					this.ID++;
				}
				this.TaskCheck();
				this.DialogueWheel.End();
				this.TaskComplete = false;
				this.TrueTimer = 0f;
				this.Timer = 0f;
				this.ID = 0;
			}
		}
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x00194C60 File Offset: 0x00193060
	private void TaskCheck()
	{
		if (this.Yandere.TargetStudent.StudentID == 37)
		{
			this.DialogueWheel.Yandere.TargetStudent.Cosmetic.MaleAccessories[1].SetActive(true);
		}
		this.GenericCheck();
		if (this.Generic)
		{
			this.Yandere.Inventory.Book = false;
			this.CheckOutBook.UpdatePrompt();
			this.Generic = false;
		}
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x00194CDC File Offset: 0x001930DC
	private void GetPortrait(int ID)
	{
		string url = string.Concat(new string[]
		{
			"file:///",
			Application.streamingAssetsPath,
			"/Portraits/Student_",
			ID.ToString(),
			".png"
		});
		WWW www = new WWW(url);
		this.Portrait.mainTexture = www.texture;
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x00194D3D File Offset: 0x0019313D
	private void UpdateTaskObjects(int StudentID)
	{
		if (this.StudentID == 30)
		{
			this.SewingMachine.Check = true;
		}
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x00194D58 File Offset: 0x00193158
	public void GenericCheck()
	{
		this.Generic = false;
		if (this.Yandere.TargetStudent.StudentID != 8 && this.Yandere.TargetStudent.StudentID != 11 && this.Yandere.TargetStudent.StudentID != 25 && this.Yandere.TargetStudent.StudentID != 28 && this.Yandere.TargetStudent.StudentID != 30 && this.Yandere.TargetStudent.StudentID != 36 && this.Yandere.TargetStudent.StudentID != 37 && this.Yandere.TargetStudent.StudentID != 38 && this.Yandere.TargetStudent.StudentID != 52 && this.Yandere.TargetStudent.StudentID != 76 && this.Yandere.TargetStudent.StudentID != 77 && this.Yandere.TargetStudent.StudentID != 78 && this.Yandere.TargetStudent.StudentID != 79 && this.Yandere.TargetStudent.StudentID != 80 && this.Yandere.TargetStudent.StudentID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x00194ECC File Offset: 0x001932CC
	public void AltGenericCheck(int TempID)
	{
		this.Generic = false;
		if (TempID != 8 && TempID != 11 && TempID != 25 && TempID != 28 && TempID != 30 && TempID != 36 && TempID != 37 && TempID != 38 && TempID != 52 && TempID != 76 && TempID != 77 && TempID != 78 && TempID != 79 && TempID != 80 && TempID != 81)
		{
			this.Generic = true;
		}
	}

	// Token: 0x040035F0 RID: 13808
	public DialogueWheelScript DialogueWheel;

	// Token: 0x040035F1 RID: 13809
	public SewingMachineScript SewingMachine;

	// Token: 0x040035F2 RID: 13810
	public CheckOutBookScript CheckOutBook;

	// Token: 0x040035F3 RID: 13811
	public TaskManagerScript TaskManager;

	// Token: 0x040035F4 RID: 13812
	public PromptBarScript PromptBar;

	// Token: 0x040035F5 RID: 13813
	public UILabel TaskDescLabel;

	// Token: 0x040035F6 RID: 13814
	public YandereScript Yandere;

	// Token: 0x040035F7 RID: 13815
	public UITexture Portrait;

	// Token: 0x040035F8 RID: 13816
	public UITexture Icon;

	// Token: 0x040035F9 RID: 13817
	public GameObject[] TaskCompleteLetters;

	// Token: 0x040035FA RID: 13818
	public string[] Descriptions;

	// Token: 0x040035FB RID: 13819
	public Texture[] Portraits;

	// Token: 0x040035FC RID: 13820
	public Texture[] Icons;

	// Token: 0x040035FD RID: 13821
	public bool TaskComplete;

	// Token: 0x040035FE RID: 13822
	public bool Generic;

	// Token: 0x040035FF RID: 13823
	public GameObject Window;

	// Token: 0x04003600 RID: 13824
	public int StudentID;

	// Token: 0x04003601 RID: 13825
	public int ID;

	// Token: 0x04003602 RID: 13826
	public float TrueTimer;

	// Token: 0x04003603 RID: 13827
	public float Timer;
}
