﻿using System;
using UnityEngine;

// Token: 0x02000589 RID: 1417
public class VoidGoddessScript : MonoBehaviour
{
	// Token: 0x0600224F RID: 8783 RVA: 0x0019BD84 File Offset: 0x0019A184
	private void Start()
	{
		this.Legs[1]["SpiderLegWiggle"].speed = 1f;
		this.Legs[2]["SpiderLegWiggle"].speed = 0.95f;
		this.Legs[3]["SpiderLegWiggle"].speed = 0.9f;
		this.Legs[4]["SpiderLegWiggle"].speed = 0.85f;
		this.Legs[5]["SpiderLegWiggle"].speed = 0.8f;
		this.Legs[6]["SpiderLegWiggle"].speed = 0.75f;
		this.Legs[7]["SpiderLegWiggle"].speed = 0.7f;
		this.Legs[8]["SpiderLegWiggle"].speed = 0.65f;
		this.ID = 1;
		while (this.ID < 101)
		{
			this.NewPortrait = UnityEngine.Object.Instantiate<GameObject>(this.Portrait, base.transform.position, Quaternion.identity);
			this.NewPortrait.transform.parent = this.Window;
			this.NewPortrait.transform.localScale = new Vector3(1f, 1f, 1f);
			this.NewPortrait.transform.localPosition = new Vector3((float)(-450 + this.Column * 100), (float)(450 - this.Row * 100), 0f);
			this.Portraits[this.ID] = this.NewPortrait.GetComponent<UITexture>();
			if (this.ID < 98)
			{
				string url = string.Concat(new string[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Portraits/Student_",
					this.ID.ToString(),
					".png"
				});
				WWW www = new WWW(url);
				this.NewPortrait.GetComponent<UITexture>().mainTexture = www.texture;
			}
			else if (this.ID == 98)
			{
				this.NewPortrait.GetComponent<UITexture>().mainTexture = this.Counselor;
			}
			else if (this.ID == 99)
			{
				this.NewPortrait.GetComponent<UITexture>().mainTexture = this.Headmaster;
			}
			else if (this.ID == 100)
			{
				this.NewPortrait.GetComponent<UITexture>().mainTexture = this.Infochan;
			}
			this.Column++;
			if (this.Column == 10)
			{
				this.Column = 0;
				this.Row++;
			}
			this.ID++;
		}
		this.Window.parent.gameObject.SetActive(false);
		this.Selected = 1;
		this.Column = 0;
		this.Row = 0;
		this.UpdatePortraits();
	}

	// Token: 0x06002250 RID: 8784 RVA: 0x0019C088 File Offset: 0x0019A488
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Goddess.activeInHierarchy)
			{
				this.Prompt.Label[0].text = "     Pass Judgement";
				this.Prompt.Label[1].text = "     Dismiss";
				this.Prompt.Label[2].text = "     Follow";
				this.Prompt.HideButton[1] = false;
				this.Prompt.HideButton[2] = false;
				this.Prompt.OffsetX[0] = -1f;
				this.Goddess.SetActive(true);
			}
			else
			{
				this.Window.parent.gameObject.SetActive(true);
				this.Prompt.Yandere.CanMove = false;
				this.PassingJudgement = true;
			}
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			this.Prompt.Label[0].text = "     Summon An Ancient Evil";
			this.Prompt.Label[1].text = string.Empty;
			this.Prompt.Label[2].text = string.Empty;
			this.Prompt.HideButton[1] = true;
			this.Prompt.HideButton[2] = true;
			this.Prompt.OffsetX[0] = 0f;
			base.transform.position = new Vector3(-9.5f, 1f, -75f);
			this.Goddess.SetActive(false);
			this.Follow = false;
		}
		if (this.Prompt.Circle[2].fillAmount == 0f)
		{
			this.Prompt.Circle[2].fillAmount = 1f;
			this.Follow = !this.Follow;
		}
		if (this.Follow && Vector3.Distance(this.Prompt.Yandere.transform.position + new Vector3(0f, 1f, 0f), base.transform.position) > 1f)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.Prompt.Yandere.transform.position + new Vector3(0f, 1f, 0f), Time.deltaTime);
		}
		if (this.PassingJudgement)
		{
			if (this.InputManager.TappedUp)
			{
				this.Row--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Row++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedLeft)
			{
				this.Column--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedRight)
			{
				this.Column++;
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				this.StudentManager.DisableStudent(this.Selected);
				this.UpdatePortraits();
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (!this.Disabled)
				{
					this.StudentManager.DisableEveryone();
					this.Disabled = true;
				}
				else
				{
					this.ID = 1;
					while (this.ID < 101)
					{
						this.StudentManager.DisableStudent(this.ID);
						this.ID++;
					}
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				this.ID = 1;
				while (this.ID < 11)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				this.ID = 11;
				while (this.ID < 21)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.ID = 21;
				while (this.ID < 26)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				this.ID = 26;
				while (this.ID < 31)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.ID = 31;
				while (this.ID < 36)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				this.ID = 36;
				while (this.ID < 41)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				this.ID = 41;
				while (this.ID < 46)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				this.ID = 46;
				while (this.ID < 51)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				this.ID = 51;
				while (this.ID < 56)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("-"))
			{
				this.ID = 56;
				while (this.ID < 61)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("="))
			{
				this.ID = 61;
				while (this.ID < 66)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("r"))
			{
				this.ID = 66;
				while (this.ID < 71)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("t"))
			{
				this.ID = 71;
				while (this.ID < 76)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("y"))
			{
				this.ID = 76;
				while (this.ID < 81)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("u"))
			{
				this.ID = 81;
				while (this.ID < 86)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("i"))
			{
				this.ID = 86;
				while (this.ID < 90)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("o"))
			{
				this.ID = 90;
				while (this.ID < 98)
				{
					this.StudentManager.DisableStudent(this.ID);
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("p"))
			{
				this.ID = 1;
				while (this.ID < 101)
				{
					if (this.ID != 21 && this.ID != 26 && this.ID != 31 && this.ID != 36 && this.ID != 41 && this.ID != 46 && this.ID != 51 && this.ID != 56 && this.ID != 61 && this.ID != 66 && this.ID != 71)
					{
						this.StudentManager.DisableStudent(this.ID);
					}
					this.ID++;
				}
				this.UpdatePortraits();
			}
			else if (Input.GetKeyDown("space"))
			{
				this.ID = 2;
				while (this.ID < 101)
				{
					if (this.ID != 1 && this.ID != 2 && this.ID != 3 && this.ID != 6 && this.ID != 10 && this.ID != 11 && this.ID != 39 && this.ID != 41 && this.ID != 42 && this.ID != 43 && this.ID != 44 && this.ID != 45 && this.ID != 81 && this.ID != 82 && this.ID != 83 && this.ID != 84 && this.ID != 85)
					{
						this.StudentManager.DisableStudent(this.ID);
					}
					this.ID++;
				}
				if (this.StudentManager.Students[7].gameObject.activeInHierarchy)
				{
					this.StudentManager.DisableStudent(7);
				}
				this.UpdatePortraits();
			}
			if (Input.GetButtonDown("B"))
			{
				this.Window.parent.gameObject.SetActive(false);
				this.Prompt.Yandere.CanMove = true;
				this.Prompt.Yandere.Shoved = false;
				this.PassingJudgement = false;
			}
			if (Input.GetKeyDown(KeyCode.Z))
			{
				this.StudentManager.Students[this.Selected].transform.position = this.Prompt.Yandere.transform.position + this.Prompt.Yandere.transform.forward;
				Physics.SyncTransforms();
			}
			if (Input.GetKeyDown(KeyCode.X))
			{
				CounselorGlobals.DeleteAll();
				StudentGlobals.SetStudentExpelled(76, false);
				StudentGlobals.SetStudentExpelled(77, false);
				StudentGlobals.SetStudentExpelled(78, false);
				StudentGlobals.SetStudentExpelled(79, false);
				StudentGlobals.SetStudentExpelled(80, false);
			}
		}
	}

	// Token: 0x06002251 RID: 8785 RVA: 0x0019CCFC File Offset: 0x0019B0FC
	private void UpdateHighlight()
	{
		if (this.Row < 0)
		{
			this.Row = 9;
		}
		else if (this.Row > 9)
		{
			this.Row = 0;
		}
		if (this.Column < 0)
		{
			this.Column = 9;
		}
		else if (this.Column > 9)
		{
			this.Column = 0;
		}
		this.Highlight.localPosition = new Vector3((float)(-450 + 100 * this.Column), (float)(450 - 100 * this.Row), this.Highlight.localPosition.z);
		this.Selected = 1 + this.Row * 10 + this.Column;
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x0019CDC0 File Offset: 0x0019B1C0
	private void UpdatePortraits()
	{
		this.ID = 1;
		while (this.ID < 98)
		{
			if (this.StudentManager.Students[this.ID] != null)
			{
				if (this.StudentManager.Students[this.ID].gameObject.activeInHierarchy)
				{
					this.Portraits[this.ID].color = new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					this.Portraits[this.ID].color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
			else
			{
				this.Portraits[this.ID].color = new Color(1f, 1f, 1f, 0.5f);
			}
			this.ID++;
		}
	}

	// Token: 0x04003784 RID: 14212
	public StudentManagerScript StudentManager;

	// Token: 0x04003785 RID: 14213
	public InputManagerScript InputManager;

	// Token: 0x04003786 RID: 14214
	public PromptScript Prompt;

	// Token: 0x04003787 RID: 14215
	public GameObject BloodyUniform;

	// Token: 0x04003788 RID: 14216
	public GameObject SeveredLimb;

	// Token: 0x04003789 RID: 14217
	public GameObject NewPortrait;

	// Token: 0x0400378A RID: 14218
	public GameObject BloodPool;

	// Token: 0x0400378B RID: 14219
	public GameObject Portrait;

	// Token: 0x0400378C RID: 14220
	public GameObject Goddess;

	// Token: 0x0400378D RID: 14221
	public Transform BloodParent;

	// Token: 0x0400378E RID: 14222
	public Transform Highlight;

	// Token: 0x0400378F RID: 14223
	public Transform Window;

	// Token: 0x04003790 RID: 14224
	public Transform Head;

	// Token: 0x04003791 RID: 14225
	public UITexture[] Portraits;

	// Token: 0x04003792 RID: 14226
	public Animation[] Legs;

	// Token: 0x04003793 RID: 14227
	public bool PassingJudgement;

	// Token: 0x04003794 RID: 14228
	public bool Disabled;

	// Token: 0x04003795 RID: 14229
	public bool Follow;

	// Token: 0x04003796 RID: 14230
	public int Selected;

	// Token: 0x04003797 RID: 14231
	public int Column;

	// Token: 0x04003798 RID: 14232
	public int Row;

	// Token: 0x04003799 RID: 14233
	public int ID;

	// Token: 0x0400379A RID: 14234
	public Texture Headmaster;

	// Token: 0x0400379B RID: 14235
	public Texture Counselor;

	// Token: 0x0400379C RID: 14236
	public Texture Infochan;
}
