using System;
using UnityEngine;

// Token: 0x02000594 RID: 1428
public class WoodChipperScript : MonoBehaviour
{
	// Token: 0x06002282 RID: 8834 RVA: 0x001A2650 File Offset: 0x001A0A50
	private void Update()
	{
		if (this.Yandere.PickUp != null)
		{
			if (this.Yandere.PickUp.Bucket != null)
			{
				if (!this.Yandere.PickUp.Bucket.Full)
				{
					this.BucketPrompt.HideButton[0] = false;
					if (this.BucketPrompt.Circle[0].fillAmount == 0f)
					{
						this.Bucket = this.Yandere.PickUp;
						this.Yandere.EmptyHands();
						this.Bucket.transform.eulerAngles = this.BucketPoint.eulerAngles;
						this.Bucket.transform.position = this.BucketPoint.position;
						this.Bucket.GetComponent<Rigidbody>().useGravity = false;
						this.Bucket.MyCollider.enabled = false;
					}
				}
				else
				{
					this.BucketPrompt.HideButton[0] = true;
				}
			}
			else
			{
				this.BucketPrompt.HideButton[0] = true;
			}
		}
		else
		{
			this.BucketPrompt.HideButton[0] = true;
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Open)
		{
			this.Rotation = Mathf.MoveTowards(this.Rotation, 0f, Time.deltaTime * 360f);
			if (this.Rotation > -36f)
			{
				if (this.Rotation < 0f)
				{
					component.clip = this.CloseAudio;
					component.Play();
				}
				this.Rotation = 0f;
			}
			this.Lid.transform.localEulerAngles = new Vector3(this.Rotation, this.Lid.transform.localEulerAngles.y, this.Lid.transform.localEulerAngles.z);
		}
		else
		{
			if (this.Lid.transform.localEulerAngles.x == 0f)
			{
				component.clip = this.OpenAudio;
				component.Play();
			}
			this.Rotation = Mathf.MoveTowards(this.Rotation, -90f, Time.deltaTime * 360f);
			this.Lid.transform.localEulerAngles = new Vector3(this.Rotation, this.Lid.transform.localEulerAngles.y, this.Lid.transform.localEulerAngles.z);
		}
		if (!this.BloodSpray.isPlaying)
		{
			if (!this.Occupied)
			{
				if (this.Yandere.Ragdoll == null)
				{
					this.Prompt.HideButton[3] = true;
				}
				else
				{
					this.Prompt.HideButton[3] = false;
				}
			}
			else if (this.Bucket == null)
			{
				this.Prompt.HideButton[0] = true;
			}
			else if (this.Bucket.Bucket.Full)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			Time.timeScale = 1f;
			if (this.Yandere.Ragdoll != null)
			{
				if (!this.Yandere.Carrying)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_dragIdle_00");
				}
				else
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_carryIdleA_00");
				}
				this.Yandere.YandereVision = false;
				this.Yandere.Chipping = true;
				this.Yandere.CanMove = false;
				this.Victims++;
				this.VictimList[this.Victims] = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Open = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			component.clip = this.ShredAudio;
			component.Play();
			this.Prompt.HideButton[3] = false;
			this.Prompt.HideButton[0] = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.Police.Corpses--;
			if (this.Yandere.Police.SuicideScene && this.Yandere.Police.Corpses == 1)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.StudentManager.Students[this.VictimID].Drowned)
			{
				this.Yandere.Police.DrownVictims--;
			}
			this.Shredding = true;
			this.Yandere.StudentManager.Students[this.VictimID].Ragdoll.Disposed = true;
		}
		if (this.Shredding)
		{
			if (this.Bucket != null)
			{
				this.Bucket.Bucket.UpdateAppearance = true;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer >= 10f)
			{
				this.Prompt.enabled = true;
				this.Shredding = false;
				this.Occupied = false;
				this.Timer = 0f;
			}
			else if (this.Timer >= 9f)
			{
				if (this.Bucket != null)
				{
					this.Bucket.MyCollider.enabled = true;
					this.Bucket.Bucket.FillSpeed = 1f;
					this.Bucket = null;
					this.BloodSpray.Stop();
				}
			}
			else if (this.Timer >= 0.33333f && !this.Bucket.Bucket.Full)
			{
				this.BloodSpray.GetComponent<AudioSource>().Play();
				this.BloodSpray.Play();
				this.Bucket.Bucket.Bloodiness = 100f;
				this.Bucket.Bucket.FillSpeed = 0.05f;
				this.Bucket.Bucket.Full = true;
			}
		}
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x001A2D04 File Offset: 0x001A1104
	public void SetVictimsMissing()
	{
		foreach (int studentID in this.VictimList)
		{
			StudentGlobals.SetStudentMissing(studentID, true);
		}
	}

	// Token: 0x0400386D RID: 14445
	public ParticleSystem BloodSpray;

	// Token: 0x0400386E RID: 14446
	public PromptScript BucketPrompt;

	// Token: 0x0400386F RID: 14447
	public YandereScript Yandere;

	// Token: 0x04003870 RID: 14448
	public PickUpScript Bucket;

	// Token: 0x04003871 RID: 14449
	public PromptScript Prompt;

	// Token: 0x04003872 RID: 14450
	public AudioClip CloseAudio;

	// Token: 0x04003873 RID: 14451
	public AudioClip ShredAudio;

	// Token: 0x04003874 RID: 14452
	public AudioClip OpenAudio;

	// Token: 0x04003875 RID: 14453
	public Transform BucketPoint;

	// Token: 0x04003876 RID: 14454
	public Transform DumpPoint;

	// Token: 0x04003877 RID: 14455
	public Transform Lid;

	// Token: 0x04003878 RID: 14456
	public float Rotation;

	// Token: 0x04003879 RID: 14457
	public float Timer;

	// Token: 0x0400387A RID: 14458
	public bool Shredding;

	// Token: 0x0400387B RID: 14459
	public bool Occupied;

	// Token: 0x0400387C RID: 14460
	public bool Open;

	// Token: 0x0400387D RID: 14461
	public int VictimID;

	// Token: 0x0400387E RID: 14462
	public int Victims;

	// Token: 0x0400387F RID: 14463
	public int ID;

	// Token: 0x04003880 RID: 14464
	public int[] VictimList;
}
