using System;
using UnityEngine;

// Token: 0x020003CC RID: 972
public class FakeStudentScript : MonoBehaviour
{
	// Token: 0x0600198C RID: 6540 RVA: 0x000EE439 File Offset: 0x000EC839
	private void Start()
	{
		this.targetRotation = base.transform.rotation;
		this.Student.Club = this.Club;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x000EE460 File Offset: 0x000EC860
	private void Update()
	{
		if (!this.Student.Talking)
		{
			if (this.LeaderAnim != string.Empty)
			{
				base.GetComponent<Animation>().CrossFade(this.LeaderAnim);
			}
			if (this.Rotate)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RotationTimer += Time.deltaTime;
				if (this.RotationTimer > 1f)
				{
					this.RotationTimer = 0f;
					this.Rotate = false;
				}
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Yandere.TargetStudent = this.Student;
			this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Student.Club, 4f);
			this.DialogueWheel.ClubLeader = true;
			this.StudentManager.DisablePrompts();
			this.DialogueWheel.HideShadows();
			this.DialogueWheel.Show = true;
			this.DialogueWheel.Panel.enabled = true;
			this.Student.Talking = true;
			this.Student.TalkTimer = 0f;
			this.Yandere.ShoulderCamera.OverShoulder = true;
			this.Yandere.WeaponMenu.KeyboardShow = false;
			this.Yandere.Obscurance.enabled = false;
			this.Yandere.WeaponMenu.Show = false;
			this.Yandere.YandereVision = false;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = true;
			this.RotationTimer = 0f;
			this.Rotate = true;
		}
	}

	// Token: 0x04001E58 RID: 7768
	public StudentManagerScript StudentManager;

	// Token: 0x04001E59 RID: 7769
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04001E5A RID: 7770
	public SubtitleScript Subtitle;

	// Token: 0x04001E5B RID: 7771
	public YandereScript Yandere;

	// Token: 0x04001E5C RID: 7772
	public StudentScript Student;

	// Token: 0x04001E5D RID: 7773
	public PromptScript Prompt;

	// Token: 0x04001E5E RID: 7774
	public Quaternion targetRotation;

	// Token: 0x04001E5F RID: 7775
	public float RotationTimer;

	// Token: 0x04001E60 RID: 7776
	public bool Rotate;

	// Token: 0x04001E61 RID: 7777
	public ClubType Club;

	// Token: 0x04001E62 RID: 7778
	public string LeaderAnim;
}
