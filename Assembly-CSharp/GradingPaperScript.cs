using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class GradingPaperScript : MonoBehaviour
{
	// Token: 0x06001C53 RID: 7251 RVA: 0x000FCF5C File Offset: 0x000FB35C
	private void Start()
	{
		this.OriginalPosition = this.Chair.position;
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x000FCF70 File Offset: 0x000FB370
	private void Update()
	{
		if (!this.Writing)
		{
			if (Vector3.Distance(this.Chair.position, this.OriginalPosition) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.OriginalPosition, Time.deltaTime * 10f);
			}
		}
		else if (this.Character != null)
		{
			if (Vector3.Distance(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f) > 0.01f)
			{
				this.Chair.position = Vector3.Lerp(this.Chair.position, this.Character.transform.position + this.Character.transform.forward * 0.1f, Time.deltaTime * 10f);
			}
			if (this.Phase == 1)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime1)
				{
					this.Teacher.CharacterAnimation["f02_deskWrite"].speed = this.Speed;
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition1;
					this.Paper.localEulerAngles = this.PickUpRotation1;
					this.Paper.localScale = new Vector3(0.9090909f, 0.9090909f, 0.9090909f);
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime1)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localPosition = this.SetDownPosition1;
					this.Paper.localEulerAngles = this.SetDownRotation1;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.PickUpTime2)
				{
					this.Paper.parent = this.LeftHand;
					this.Paper.localPosition = this.PickUpPosition2;
					this.Paper.localEulerAngles = this.PickUpRotation2;
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Teacher.CharacterAnimation["f02_deskWrite"].time > this.SetDownTime2)
				{
					this.Paper.parent = this.Character.transform;
					this.Paper.localScale = Vector3.zero;
					this.Phase++;
				}
			}
			else if (this.Phase == 5 && this.Teacher.CharacterAnimation["f02_deskWrite"].time >= this.Teacher.CharacterAnimation["f02_deskWrite"].length)
			{
				this.Teacher.CharacterAnimation["f02_deskWrite"].time = 0f;
				this.Teacher.CharacterAnimation.Play("f02_deskWrite");
				this.Phase = 1;
			}
			if (this.Teacher.Actions[this.Teacher.Phase] != StudentActionType.GradePapers || !this.Teacher.Routine || this.Teacher.Stop)
			{
				this.Paper.localScale = Vector3.zero;
				this.Teacher.Obstacle.enabled = false;
				this.Teacher.Pen.SetActive(false);
				this.Writing = false;
				this.Phase = 1;
			}
		}
	}

	// Token: 0x04002069 RID: 8297
	public StudentScript Teacher;

	// Token: 0x0400206A RID: 8298
	public GameObject Character;

	// Token: 0x0400206B RID: 8299
	public Transform LeftHand;

	// Token: 0x0400206C RID: 8300
	public Transform Chair;

	// Token: 0x0400206D RID: 8301
	public Transform Paper;

	// Token: 0x0400206E RID: 8302
	public float PickUpTime1;

	// Token: 0x0400206F RID: 8303
	public float SetDownTime1;

	// Token: 0x04002070 RID: 8304
	public float PickUpTime2;

	// Token: 0x04002071 RID: 8305
	public float SetDownTime2;

	// Token: 0x04002072 RID: 8306
	public Vector3 OriginalPosition;

	// Token: 0x04002073 RID: 8307
	public Vector3 PickUpPosition1;

	// Token: 0x04002074 RID: 8308
	public Vector3 SetDownPosition1;

	// Token: 0x04002075 RID: 8309
	public Vector3 PickUpPosition2;

	// Token: 0x04002076 RID: 8310
	public Vector3 PickUpRotation1;

	// Token: 0x04002077 RID: 8311
	public Vector3 SetDownRotation1;

	// Token: 0x04002078 RID: 8312
	public Vector3 PickUpRotation2;

	// Token: 0x04002079 RID: 8313
	public int Phase = 1;

	// Token: 0x0400207A RID: 8314
	public float Speed = 1f;

	// Token: 0x0400207B RID: 8315
	public bool Writing;
}
