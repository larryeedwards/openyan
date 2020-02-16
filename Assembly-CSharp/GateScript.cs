using System;
using UnityEngine;

// Token: 0x020003E1 RID: 993
public class GateScript : MonoBehaviour
{
	// Token: 0x060019CB RID: 6603 RVA: 0x000F255C File Offset: 0x000F095C
	private void Update()
	{
		if (!this.ManuallyAdjusted)
		{
			if (this.Clock.PresentTime / 60f > 8f && this.Clock.PresentTime / 60f < 15.5f)
			{
				if (!this.Closed)
				{
					this.PlayAudio();
					this.Closed = true;
					if (this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = false;
					}
				}
			}
			else if (this.Closed)
			{
				this.PlayAudio();
				this.Closed = false;
				if (!this.EmergencyDoor.enabled)
				{
					this.EmergencyDoor.enabled = true;
				}
			}
		}
		if (this.StudentManager.Students[97] != null)
		{
			if (this.StudentManager.Students[97].CurrentAction == StudentActionType.AtLocker && this.StudentManager.Students[97].Routine && this.StudentManager.Students[97].Alive)
			{
				if (Vector3.Distance(this.StudentManager.Students[97].transform.position, this.StudentManager.Podiums.List[0].position) < 0.1f)
				{
					if (this.ManuallyAdjusted)
					{
						this.ManuallyAdjusted = false;
					}
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
				else
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.PlayAudio();
			this.EmergencyDoor.enabled = !this.EmergencyDoor.enabled;
			this.ManuallyAdjusted = true;
			this.Closed = !this.Closed;
			if (this.StudentManager.Students[97] != null && this.StudentManager.Students[97].Investigating)
			{
				this.StudentManager.Students[97].StopInvestigating();
			}
		}
		if (!this.Closed)
		{
			if (this.RightGate.localPosition.x != 7f)
			{
				this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 7f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
				this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -7f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
				if (!this.AudioPlayed && this.RightGate.localPosition.x == 7f)
				{
					this.RightGateAudio.clip = this.StopOpen;
					this.LeftGateAudio.clip = this.StopOpen;
					this.RightGateAudio.Play();
					this.LeftGateAudio.Play();
					this.RightGateLoop.Stop();
					this.LeftGateLoop.Stop();
					this.AudioPlayed = true;
				}
			}
		}
		else if (this.RightGate.localPosition.x != 2.325f)
		{
			if (this.RightGate.localPosition.x < 2.4f)
			{
				this.Crushing = true;
			}
			this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 2.325f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
			this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -2.325f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
			if (!this.AudioPlayed && this.RightGate.localPosition.x == 2.325f)
			{
				this.RightGateAudio.clip = this.StopOpen;
				this.LeftGateAudio.clip = this.StopOpen;
				this.RightGateAudio.Play();
				this.LeftGateAudio.Play();
				this.RightGateLoop.Stop();
				this.LeftGateLoop.Stop();
				this.AudioPlayed = true;
				this.Crushing = false;
			}
		}
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x000F2A9C File Offset: 0x000F0E9C
	public void PlayAudio()
	{
		this.RightGateAudio.clip = this.Start;
		this.LeftGateAudio.clip = this.Start;
		this.RightGateAudio.Play();
		this.LeftGateAudio.Play();
		this.RightGateLoop.Play();
		this.LeftGateLoop.Play();
		this.AudioPlayed = false;
	}

	// Token: 0x04001F27 RID: 7975
	public StudentManagerScript StudentManager;

	// Token: 0x04001F28 RID: 7976
	public PromptScript Prompt;

	// Token: 0x04001F29 RID: 7977
	public ClockScript Clock;

	// Token: 0x04001F2A RID: 7978
	public Collider EmergencyDoor;

	// Token: 0x04001F2B RID: 7979
	public Collider GateCollider;

	// Token: 0x04001F2C RID: 7980
	public Transform RightGate;

	// Token: 0x04001F2D RID: 7981
	public Transform LeftGate;

	// Token: 0x04001F2E RID: 7982
	public bool ManuallyAdjusted;

	// Token: 0x04001F2F RID: 7983
	public bool AudioPlayed;

	// Token: 0x04001F30 RID: 7984
	public bool UpdateGates;

	// Token: 0x04001F31 RID: 7985
	public bool Crushing;

	// Token: 0x04001F32 RID: 7986
	public bool Closed;

	// Token: 0x04001F33 RID: 7987
	public AudioSource RightGateAudio;

	// Token: 0x04001F34 RID: 7988
	public AudioSource LeftGateAudio;

	// Token: 0x04001F35 RID: 7989
	public AudioSource RightGateLoop;

	// Token: 0x04001F36 RID: 7990
	public AudioSource LeftGateLoop;

	// Token: 0x04001F37 RID: 7991
	public AudioClip Start;

	// Token: 0x04001F38 RID: 7992
	public AudioClip StopOpen;

	// Token: 0x04001F39 RID: 7993
	public AudioClip StopClose;
}
