using System;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class LoveManagerScript : MonoBehaviour
{
	// Token: 0x06001D82 RID: 7554 RVA: 0x00115D9B File Offset: 0x0011419B
	private void Start()
	{
		this.SuitorProgress = DatingGlobals.SuitorProgress;
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x00115DA8 File Offset: 0x001141A8
	private void LateUpdate()
	{
		if (this.Follower != null && this.Follower.Alive && !this.Follower.InCouple)
		{
			this.ID = 0;
			while (this.ID < this.TotalTargets)
			{
				Transform transform = this.Targets[this.ID];
				if (transform != null && this.Follower.transform.position.y > transform.position.y - 2f && this.Follower.transform.position.y < transform.position.y + 2f && Vector3.Distance(this.Follower.transform.position, new Vector3(transform.position.x, this.Follower.transform.position.y, transform.position.z)) < 2.5f)
				{
					float f = Vector3.Angle(this.Follower.transform.forward, this.Follower.transform.position - new Vector3(transform.position.x, this.Follower.transform.position.y, transform.position.z));
					if (Mathf.Abs(f) > this.AngleLimit)
					{
						if (!this.Follower.Gush)
						{
							this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
							this.Follower.GushTarget = transform;
							ParticleSystem.EmissionModule emission = this.Follower.Hearts.emission;
							emission.enabled = true;
							emission.rateOverTime = 5f;
							this.Follower.Hearts.Play();
							this.Follower.Gush = true;
						}
					}
					else
					{
						this.Follower.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
						this.Follower.Hearts.emission.enabled = false;
						this.Follower.Gush = false;
					}
				}
				this.ID++;
			}
		}
		if (this.LeftNote)
		{
			this.Rival = this.StudentManager.Students[this.RivalID];
			this.Suitor = this.StudentManager.Students[this.SuitorID];
			if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
			{
				this.Rival = this.StudentManager.Students[this.StudentManager.RivalID];
				this.Suitor = this.StudentManager.Students[1];
			}
			if (this.Rival != null && this.Suitor != null && this.Rival.Alive && this.Suitor.Alive && !this.Rival.Dying && !this.Suitor.Dying && this.Rival.ConfessPhase == 5 && this.Suitor.ConfessPhase == 3)
			{
				this.WaitingToConfess = true;
				float num = Vector3.Distance(this.Yandere.transform.position, this.MythHill.position);
				if (this.WaitingToConfess && !this.Yandere.Chased && this.Yandere.Chasers == 0 && num > 10f && num < 25f)
				{
					this.BeginConfession();
				}
			}
		}
		if (this.HoldingHands)
		{
			if (this.Rival == null)
			{
				this.Rival = this.StudentManager.Students[this.RivalID];
			}
			if (this.Suitor == null)
			{
				this.Suitor = this.StudentManager.Students[this.SuitorID];
			}
			this.Rival.MyController.Move(base.transform.forward * Time.deltaTime);
			this.Suitor.transform.position = new Vector3(this.Rival.transform.position.x - 0.5f, this.Rival.transform.position.y, this.Rival.transform.position.z);
			if (this.Rival.transform.position.z > -50f)
			{
				this.Suitor.MyController.radius = 0.12f;
				this.Suitor.enabled = true;
				this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 0f);
				this.Suitor.Hearts.emission.enabled = false;
				this.Rival.MyController.radius = 0.12f;
				this.Rival.enabled = true;
				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 0f);
				this.Rival.Hearts.emission.enabled = false;
				this.Suitor.HoldingHands = false;
				this.Rival.HoldingHands = false;
				this.HoldingHands = false;
			}
		}
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x001163BC File Offset: 0x001147BC
	public void CoupleCheck()
	{
		if (this.SuitorProgress == 2)
		{
			this.Rival = this.StudentManager.Students[this.RivalID];
			this.Suitor = this.StudentManager.Students[this.SuitorID];
			if (this.Rival != null && this.Suitor != null)
			{
				this.Suitor.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.Suitor.Character.GetComponent<Animation>().enabled = true;
				this.Rival.Character.GetComponent<Animation>().enabled = true;
				this.Suitor.Character.GetComponent<Animation>().Play("walkHands_00");
				this.Suitor.transform.eulerAngles = Vector3.zero;
				this.Suitor.transform.position = new Vector3(-0.25f, 0f, -90f);
				this.Suitor.Pathfinding.canSearch = false;
				this.Suitor.Pathfinding.canMove = false;
				this.Suitor.MyController.radius = 0f;
				this.Suitor.enabled = false;
				this.Rival.Character.GetComponent<Animation>().Play("f02_walkHands_00");
				this.Rival.transform.eulerAngles = Vector3.zero;
				this.Rival.transform.position = new Vector3(0.25f, 0f, -90f);
				this.Rival.Pathfinding.canSearch = false;
				this.Rival.Pathfinding.canMove = false;
				this.Rival.MyController.radius = 0f;
				this.Rival.enabled = false;
				Physics.SyncTransforms();
				this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
				ParticleSystem.EmissionModule emission = this.Suitor.Hearts.emission;
				emission.enabled = true;
				emission.rateOverTime = 5f;
				this.Suitor.Hearts.Play();
				this.Rival.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
				ParticleSystem.EmissionModule emission2 = this.Rival.Hearts.emission;
				emission2.enabled = true;
				emission2.rateOverTime = 5f;
				this.Rival.Hearts.Play();
				this.Suitor.HoldingHands = true;
				this.Rival.HoldingHands = true;
				this.Suitor.CoupleID = this.SuitorID;
				this.Rival.CoupleID = this.RivalID;
				this.HoldingHands = true;
				Debug.Log("Students are now holding hands.");
			}
		}
	}

	// Token: 0x06001D85 RID: 7557 RVA: 0x001166C4 File Offset: 0x00114AC4
	public void BeginConfession()
	{
		this.Suitor.EmptyHands();
		this.Rival.EmptyHands();
		this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.CanMove = false;
		this.StudentManager.DisableEveryone();
		this.Suitor.gameObject.SetActive(true);
		this.Rival.gameObject.SetActive(true);
		this.Suitor.enabled = false;
		this.Rival.enabled = false;
		if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
		{
			this.ConfessionManager.gameObject.SetActive(true);
		}
		else
		{
			this.ConfessionScene.enabled = true;
		}
		this.Clock.StopTime = true;
		this.LeftNote = false;
	}

	// Token: 0x040024CF RID: 9423
	public ConfessionManagerScript ConfessionManager;

	// Token: 0x040024D0 RID: 9424
	public ConfessionSceneScript ConfessionScene;

	// Token: 0x040024D1 RID: 9425
	public StudentManagerScript StudentManager;

	// Token: 0x040024D2 RID: 9426
	public YandereScript Yandere;

	// Token: 0x040024D3 RID: 9427
	public ClockScript Clock;

	// Token: 0x040024D4 RID: 9428
	public StudentScript Follower;

	// Token: 0x040024D5 RID: 9429
	public StudentScript Suitor;

	// Token: 0x040024D6 RID: 9430
	public StudentScript Rival;

	// Token: 0x040024D7 RID: 9431
	public Transform FriendWaitSpot;

	// Token: 0x040024D8 RID: 9432
	public Transform[] Targets;

	// Token: 0x040024D9 RID: 9433
	public Transform MythHill;

	// Token: 0x040024DA RID: 9434
	public int SuitorProgress;

	// Token: 0x040024DB RID: 9435
	public int TotalTargets;

	// Token: 0x040024DC RID: 9436
	public int Phase = 1;

	// Token: 0x040024DD RID: 9437
	public int ID;

	// Token: 0x040024DE RID: 9438
	public int SuitorID = 28;

	// Token: 0x040024DF RID: 9439
	public int RivalID = 30;

	// Token: 0x040024E0 RID: 9440
	public float AngleLimit;

	// Token: 0x040024E1 RID: 9441
	public bool WaitingToConfess;

	// Token: 0x040024E2 RID: 9442
	public bool HoldingHands;

	// Token: 0x040024E3 RID: 9443
	public bool RivalWaiting;

	// Token: 0x040024E4 RID: 9444
	public bool LeftNote;

	// Token: 0x040024E5 RID: 9445
	public bool Courted;
}
