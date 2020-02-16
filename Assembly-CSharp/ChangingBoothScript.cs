using System;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class ChangingBoothScript : MonoBehaviour
{
	// Token: 0x060017C2 RID: 6082 RVA: 0x000BD579 File Offset: 0x000BB979
	private void Start()
	{
		this.CheckYandereClub();
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000BD584 File Offset: 0x000BB984
	private void Update()
	{
		if (!this.Occupied && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
			this.YandereChanging = true;
			this.Occupied = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Occupied)
		{
			if (this.OccupyTimer == 0f)
			{
				if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
				{
					this.MyAudioSource.clip = this.CurtainSound;
					this.MyAudioSource.Play();
				}
			}
			else if (this.OccupyTimer > 1f && this.Phase == 0)
			{
				if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
				{
					this.MyAudioSource.clip = this.ClothSound;
					this.MyAudioSource.Play();
				}
				this.Phase++;
			}
			this.OccupyTimer += Time.deltaTime;
			if (this.YandereChanging)
			{
				if (this.OccupyTimer < 2f)
				{
					this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
					this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					this.Yandere.MoveTowardsTarget(base.transform.position);
				}
				else if (this.OccupyTimer < 3f)
				{
					this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
					this.Curtains.SetBlendShapeWeight(0, this.Weight);
					if (this.Phase < 2)
					{
						this.MyAudioSource.clip = this.CurtainSound;
						this.MyAudioSource.Play();
						if (!this.Yandere.ClubAttire)
						{
							this.Yandere.PreviousSchoolwear = this.Yandere.Schoolwear;
						}
						this.Yandere.ChangeClubwear();
						this.Phase++;
					}
					this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, base.transform.rotation, 10f * Time.deltaTime);
					this.Yandere.MoveTowardsTarget(this.ExitSpot.position);
				}
				else
				{
					this.YandereChanging = false;
					this.Yandere.CanMove = true;
					this.Prompt.enabled = true;
					this.Occupied = false;
					this.OccupyTimer = 0f;
					this.Phase = 0;
				}
			}
			else if (this.OccupyTimer < 2f)
			{
				this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
				this.Curtains.SetBlendShapeWeight(0, this.Weight);
			}
			else if (this.OccupyTimer < 3f)
			{
				this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
				this.Curtains.SetBlendShapeWeight(0, this.Weight);
				if (this.Phase < 2)
				{
					if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f)
					{
						this.MyAudioSource.clip = this.CurtainSound;
						this.MyAudioSource.Play();
					}
					this.Student.ChangeClubwear();
					this.Phase++;
				}
			}
			else
			{
				this.Student.WalkAnim = this.Student.OriginalWalkAnim;
				this.Occupied = false;
				this.OccupyTimer = 0f;
				this.Student = null;
				this.Phase = 0;
				this.CheckYandereClub();
			}
		}
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000BDAA4 File Offset: 0x000BBEA4
	public void CheckYandereClub()
	{
		if (ClubGlobals.Club != this.ClubID)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		else if (this.Yandere.Bloodiness == 0f && !this.CannotChange && this.Yandere.Schoolwear > 0)
		{
			if (!this.Occupied)
			{
				this.Prompt.enabled = true;
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x040017C6 RID: 6086
	public YandereScript Yandere;

	// Token: 0x040017C7 RID: 6087
	public StudentScript Student;

	// Token: 0x040017C8 RID: 6088
	public PromptScript Prompt;

	// Token: 0x040017C9 RID: 6089
	public SkinnedMeshRenderer Curtains;

	// Token: 0x040017CA RID: 6090
	public Transform ExitSpot;

	// Token: 0x040017CB RID: 6091
	public Transform[] WaitSpots;

	// Token: 0x040017CC RID: 6092
	public bool YandereChanging;

	// Token: 0x040017CD RID: 6093
	public bool CannotChange;

	// Token: 0x040017CE RID: 6094
	public bool Occupied;

	// Token: 0x040017CF RID: 6095
	public AudioSource MyAudioSource;

	// Token: 0x040017D0 RID: 6096
	public AudioClip CurtainSound;

	// Token: 0x040017D1 RID: 6097
	public AudioClip ClothSound;

	// Token: 0x040017D2 RID: 6098
	public float OccupyTimer;

	// Token: 0x040017D3 RID: 6099
	public float Weight;

	// Token: 0x040017D4 RID: 6100
	public ClubType ClubID;

	// Token: 0x040017D5 RID: 6101
	public int Phase;
}
