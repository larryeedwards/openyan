using System;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class StandScript : MonoBehaviour
{
	// Token: 0x0600204A RID: 8266 RVA: 0x00150363 File Offset: 0x0014E763
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x00150378 File Offset: 0x0014E778
	private void Update()
	{
		if (!this.Stand.activeInHierarchy)
		{
			if (this.Weapons == 8 && this.Yandere.transform.position.y > 11.9f && Input.GetButtonDown("RB") && !MissionModeGlobals.MissionMode && !this.Yandere.Laughing && this.Yandere.CanMove)
			{
				this.Yandere.Jojo();
			}
		}
		else if (this.Phase == 0)
		{
			if (this.Stand.GetComponent<Animation>()["StandSummon"].time >= 2f && this.Stand.GetComponent<Animation>()["StandSummon"].time <= 2.5f)
			{
				if (!this.SFX)
				{
					AudioSource.PlayClipAtPoint(this.SummonSFX, base.transform.position);
					this.SFX = true;
				}
				UnityEngine.Object.Instantiate<GameObject>(this.SummonEffect, this.SummonTransform.position, Quaternion.identity);
			}
			if (this.Stand.GetComponent<Animation>()["StandSummon"].time >= this.Stand.GetComponent<Animation>()["StandSummon"].length)
			{
				this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
				this.Phase++;
			}
		}
		else
		{
			float axis = Input.GetAxis("Vertical");
			float axis2 = Input.GetAxis("Horizontal");
			if (this.Yandere.CanMove)
			{
				this.Return();
				if (axis != 0f || axis2 != 0f)
				{
					if (this.Yandere.Running)
					{
						this.Stand.GetComponent<Animation>().CrossFade("StandRun");
					}
					else
					{
						this.Stand.GetComponent<Animation>().CrossFade("StandWalk");
					}
				}
				else
				{
					this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
				}
			}
			else if (this.Yandere.RPGCamera.enabled)
			{
				if (this.Yandere.Laughing)
				{
					if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0f, 0.2f, -0.4f)) > 0.01f)
					{
						this.Stand.transform.localPosition = Vector3.Lerp(this.Stand.transform.localPosition, new Vector3(0f, 0.2f, 0.1f), Time.deltaTime * 10f);
						this.Stand.transform.localEulerAngles = new Vector3(Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 22.5f, Time.deltaTime * 10f), this.Stand.transform.localEulerAngles.y, this.Stand.transform.localEulerAngles.z);
					}
					this.Stand.GetComponent<Animation>().CrossFade("StandAttack");
					this.StandPunch.MyCollider.enabled = true;
					this.ReadyForFinisher = true;
				}
				else if (this.ReadyForFinisher)
				{
					if (this.Phase == 1)
					{
						base.GetComponent<AudioSource>().Play();
						this.Finisher = UnityEngine.Random.Range(1, 3);
						this.Stand.GetComponent<Animation>().CrossFade("StandFinisher" + this.Finisher.ToString());
						this.Phase++;
					}
					else if (this.Phase == 2)
					{
						if (this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >= 0.5f)
						{
							this.FalconPunch.MyCollider.enabled = true;
							this.StandPunch.MyCollider.enabled = false;
							this.Phase++;
						}
					}
					else if (this.Phase == 3 && (this.StandPunch.MyCollider.enabled || this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].time >= this.Stand.GetComponent<Animation>()["StandFinisher" + this.Finisher.ToString()].length))
					{
						this.Stand.GetComponent<Animation>().CrossFade("StandIdle");
						this.FalconPunch.MyCollider.enabled = false;
						this.ReadyForFinisher = false;
						this.Yandere.CanMove = true;
						this.Phase = 1;
					}
				}
			}
		}
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x00150894 File Offset: 0x0014EC94
	public void Spawn()
	{
		this.FalconPunch.MyCollider.enabled = false;
		this.StandPunch.MyCollider.enabled = false;
		this.StandCamera.SetActive(true);
		this.MotionBlur.enabled = true;
		this.Stand.SetActive(true);
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x001508E8 File Offset: 0x0014ECE8
	private void Return()
	{
		if (Vector3.Distance(this.Stand.transform.localPosition, new Vector3(0f, 0f, -0.5f)) > 0.01f)
		{
			this.Stand.transform.localPosition = Vector3.Lerp(this.Stand.transform.localPosition, new Vector3(0f, 0f, -0.5f), Time.deltaTime * 10f);
			this.Stand.transform.localEulerAngles = new Vector3(Mathf.Lerp(this.Stand.transform.localEulerAngles.x, 0f, Time.deltaTime * 10f), this.Stand.transform.localEulerAngles.y, this.Stand.transform.localEulerAngles.z);
		}
	}

	// Token: 0x04002D2D RID: 11565
	public AmplifyMotionEffect MotionBlur;

	// Token: 0x04002D2E RID: 11566
	public FalconPunchScript FalconPunch;

	// Token: 0x04002D2F RID: 11567
	public StandPunchScript StandPunch;

	// Token: 0x04002D30 RID: 11568
	public Transform SummonTransform;

	// Token: 0x04002D31 RID: 11569
	public GameObject SummonEffect;

	// Token: 0x04002D32 RID: 11570
	public GameObject StandCamera;

	// Token: 0x04002D33 RID: 11571
	public YandereScript Yandere;

	// Token: 0x04002D34 RID: 11572
	public GameObject Stand;

	// Token: 0x04002D35 RID: 11573
	public Transform[] Hands;

	// Token: 0x04002D36 RID: 11574
	public int FinishPhase;

	// Token: 0x04002D37 RID: 11575
	public int Finisher;

	// Token: 0x04002D38 RID: 11576
	public int Weapons;

	// Token: 0x04002D39 RID: 11577
	public int Phase;

	// Token: 0x04002D3A RID: 11578
	public AudioClip SummonSFX;

	// Token: 0x04002D3B RID: 11579
	public bool ReadyForFinisher;

	// Token: 0x04002D3C RID: 11580
	public bool SFX;
}
