using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000543 RID: 1347
public class TaskListScript : MonoBehaviour
{
	// Token: 0x06002168 RID: 8552 RVA: 0x00193BD0 File Offset: 0x00191FD0
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			if (this.ID == 1)
			{
				this.ListPosition--;
				if (this.ListPosition < 0)
				{
					this.ListPosition = 84;
					this.ID = 16;
				}
			}
			else
			{
				this.ID--;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (this.InputManager.TappedDown)
		{
			if (this.ID == 16)
			{
				this.ListPosition++;
				if (this.ListPosition > 84)
				{
					this.ListPosition = 0;
					this.ID = 1;
				}
			}
			else
			{
				this.ID++;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (Input.GetButtonDown("B"))
		{
			this.PauseScreen.PromptBar.ClearButtons();
			this.PauseScreen.PromptBar.Label[0].text = "Accept";
			this.PauseScreen.PromptBar.Label[1].text = "Back";
			this.PauseScreen.PromptBar.Label[4].text = "Choose";
			this.PauseScreen.PromptBar.Label[5].text = "Choose";
			this.PauseScreen.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			this.MainMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x00193D84 File Offset: 0x00192184
	public void UpdateTaskList()
	{
		for (int i = 1; i < this.TaskNameLabels.Length; i++)
		{
			if (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 0)
			{
				this.TaskNameLabels[i].text = "Undiscovered Task #" + (i + this.ListPosition);
			}
			else
			{
				this.TaskNameLabels[i].text = this.JSON.Students[i + this.ListPosition].Name + "'s Task";
			}
			this.Checkmarks[i].enabled = (TaskGlobals.GetTaskStatus(i + this.ListPosition) == 3);
		}
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x00193E34 File Offset: 0x00192234
	public IEnumerator UpdateTaskInfo()
	{
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		if (TaskGlobals.GetTaskStatus(this.ID + this.ListPosition) == 0)
		{
			this.StudentIcon.mainTexture = this.Silhouette;
			this.TaskIcon.mainTexture = this.QuestionMark;
			this.TaskDesc.text = "This task has not been discovered yet.";
		}
		else
		{
			string path = string.Concat(new string[]
			{
				"file:///",
				Application.streamingAssetsPath,
				"/Portraits/Student_",
				(this.ID + this.ListPosition).ToString(),
				".png"
			});
			WWW www = new WWW(path);
			yield return www;
			this.StudentIcon.mainTexture = www.texture;
			this.TaskWindow.AltGenericCheck(this.ID + this.ListPosition);
			if (this.TaskWindow.Generic)
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[0];
				this.TaskDesc.text = this.TaskWindow.Descriptions[0];
			}
			else
			{
				this.TaskIcon.mainTexture = this.TaskWindow.Icons[this.ID + this.ListPosition];
				this.TaskDesc.text = this.TaskWindow.Descriptions[this.ID + this.ListPosition];
			}
		}
		yield break;
	}

	// Token: 0x040035DA RID: 13786
	public InputManagerScript InputManager;

	// Token: 0x040035DB RID: 13787
	public PauseScreenScript PauseScreen;

	// Token: 0x040035DC RID: 13788
	public TaskWindowScript TaskWindow;

	// Token: 0x040035DD RID: 13789
	public JsonScript JSON;

	// Token: 0x040035DE RID: 13790
	public GameObject MainMenu;

	// Token: 0x040035DF RID: 13791
	public UITexture StudentIcon;

	// Token: 0x040035E0 RID: 13792
	public UITexture TaskIcon;

	// Token: 0x040035E1 RID: 13793
	public UILabel TaskDesc;

	// Token: 0x040035E2 RID: 13794
	public Texture QuestionMark;

	// Token: 0x040035E3 RID: 13795
	public Transform Highlight;

	// Token: 0x040035E4 RID: 13796
	public Texture Silhouette;

	// Token: 0x040035E5 RID: 13797
	public UILabel[] TaskNameLabels;

	// Token: 0x040035E6 RID: 13798
	public UISprite[] Checkmarks;

	// Token: 0x040035E7 RID: 13799
	public int ListPosition;

	// Token: 0x040035E8 RID: 13800
	public int ID = 1;
}
