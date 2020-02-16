using System;
using UnityEngine;

// Token: 0x02000432 RID: 1074
public class IncineratorScript : MonoBehaviour
{
	// Token: 0x06001CF1 RID: 7409 RVA: 0x0010AD5D File Offset: 0x0010915D
	private void Start()
	{
		this.Panel.SetActive(false);
		this.Prompt.enabled = true;
	}

	// Token: 0x06001CF2 RID: 7410 RVA: 0x0010AD78 File Offset: 0x00109178
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Open)
		{
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.RightDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.MoveTowards(this.LeftDoor.transform.localEulerAngles.y, 0f, Time.deltaTime * 360f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y < 36f)
			{
				if (this.RightDoor.transform.localEulerAngles.y > 0f)
				{
					component.clip = this.IncineratorClose;
					component.Play();
				}
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 0f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		else
		{
			if (this.RightDoor.transform.localEulerAngles.y == 0f)
			{
				component.clip = this.IncineratorOpen;
				component.Play();
			}
			this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, Mathf.Lerp(this.RightDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.RightDoor.transform.localEulerAngles.z);
			this.LeftDoor.transform.localEulerAngles = new Vector3(this.LeftDoor.transform.localEulerAngles.x, Mathf.Lerp(this.LeftDoor.transform.localEulerAngles.y, 135f, Time.deltaTime * 10f), this.LeftDoor.transform.localEulerAngles.z);
			if (this.RightDoor.transform.localEulerAngles.y > 134f)
			{
				this.RightDoor.transform.localEulerAngles = new Vector3(this.RightDoor.transform.localEulerAngles.x, 135f, this.RightDoor.transform.localEulerAngles.z);
			}
		}
		if (this.OpenTimer > 0f)
		{
			this.OpenTimer -= Time.deltaTime;
			if (this.OpenTimer <= 1f)
			{
				this.Open = false;
			}
			if (this.OpenTimer <= 0f)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (!this.Smoke.isPlaying)
		{
			this.YandereHoldingEvidence = (this.Yandere.Ragdoll != null);
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.PickUp != null)
				{
					this.YandereHoldingEvidence = (this.Yandere.PickUp.Evidence || this.Yandere.PickUp.Garbage);
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (this.Yandere.EquippedWeapon != null)
				{
					this.YandereHoldingEvidence = this.Yandere.EquippedWeapon.MurderWeapon;
				}
				else
				{
					this.YandereHoldingEvidence = false;
				}
			}
			if (!this.YandereHoldingEvidence)
			{
				if (!this.Prompt.HideButton[3])
				{
					this.Prompt.HideButton[3] = true;
				}
			}
			else if (this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = false;
			}
			if ((this.Yandere.Chased || this.Yandere.Chasers > 0 || !this.YandereHoldingEvidence) && !this.Prompt.HideButton[3])
			{
				this.Prompt.HideButton[3] = true;
			}
			if (this.Ready)
			{
				if (!this.Smoke.isPlaying)
				{
					if (this.Prompt.HideButton[0])
					{
						this.Prompt.HideButton[0] = false;
					}
				}
				else if (!this.Prompt.HideButton[0])
				{
					this.Prompt.HideButton[0] = true;
				}
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			Time.timeScale = 1f;
			if (this.Yandere.Ragdoll != null)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade((!this.Yandere.Carrying) ? "f02_dragIdle_00" : "f02_carryIdleA_00");
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Dumping = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Victims++;
				this.VictimList[this.Victims] = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Open = true;
			}
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.BodyPart != null)
				{
					this.Limbs++;
					this.LimbList[this.Limbs] = this.Yandere.PickUp.GetComponent<BodyPartScript>().StudentID;
				}
				this.Yandere.PickUp.Incinerator = this;
				this.Yandere.PickUp.Dumped = true;
				this.Yandere.PickUp.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
			WeaponScript equippedWeapon = this.Yandere.EquippedWeapon;
			if (equippedWeapon != null)
			{
				this.DestroyedEvidence++;
				this.EvidenceList[this.DestroyedEvidence] = equippedWeapon.WeaponID;
				equippedWeapon.Incinerator = this;
				equippedWeapon.Dumped = true;
				equippedWeapon.Drop();
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.OpenTimer = 2f;
				this.Ready = true;
				this.Open = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Panel.SetActive(true);
			this.Timer = 60f;
			component.clip = this.IncineratorActivate;
			component.Play();
			this.Flames.Play();
			this.Smoke.Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.Police.IncineratedWeapons += this.MurderWeapons;
			this.Yandere.Police.BloodyClothing -= this.BloodyClothing;
			this.Yandere.Police.BloodyWeapons -= this.MurderWeapons;
			this.Yandere.Police.BodyParts -= this.BodyParts;
			this.Yandere.Police.Corpses -= this.Corpses;
			if (this.Yandere.Police.SuicideScene && this.Yandere.Police.Corpses == 1)
			{
				this.Yandere.Police.MurderScene = false;
			}
			if (this.Yandere.Police.Corpses == 0)
			{
				this.Yandere.Police.MurderScene = false;
			}
			this.BloodyClothing = 0;
			this.MurderWeapons = 0;
			this.BodyParts = 0;
			this.Corpses = 0;
			this.ID = 0;
			while (this.ID < 101)
			{
				if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]] != null)
				{
					this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Disposed = true;
					this.ConfirmedDead[this.ID] = this.CorpseList[this.ID];
					if (this.Yandere.StudentManager.Students[this.CorpseList[this.ID]].Ragdoll.Drowned)
					{
						this.Yandere.Police.DrownVictims--;
					}
				}
				this.ID++;
			}
		}
		if (this.Smoke.isPlaying)
		{
			this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			this.FlameSound.volume += Time.deltaTime;
			this.Circle.fillAmount = 1f - this.Timer / 60f;
			if (this.Timer <= 0f)
			{
				this.Prompt.HideButton[0] = true;
				this.Prompt.enabled = true;
				this.Panel.SetActive(false);
				this.Ready = false;
				this.Flames.Stop();
				this.Smoke.Stop();
			}
		}
		else
		{
			this.FlameSound.volume -= Time.deltaTime;
		}
		if (this.Panel.activeInHierarchy)
		{
			float num = (float)Mathf.CeilToInt(this.Timer * 60f);
			float num2 = Mathf.Floor(num / 60f);
			float num3 = (float)Mathf.RoundToInt(num % 60f);
			this.TimeLabel.text = string.Format("{0:00}:{1:00}", num2, num3);
		}
	}

	// Token: 0x06001CF3 RID: 7411 RVA: 0x0010B8A8 File Offset: 0x00109CA8
	public void SetVictimsMissing()
	{
		foreach (int studentID in this.ConfirmedDead)
		{
			StudentGlobals.SetStudentMissing(studentID, true);
		}
	}

	// Token: 0x040022C2 RID: 8898
	public YandereScript Yandere;

	// Token: 0x040022C3 RID: 8899
	public PromptScript Prompt;

	// Token: 0x040022C4 RID: 8900
	public ClockScript Clock;

	// Token: 0x040022C5 RID: 8901
	public AudioClip IncineratorActivate;

	// Token: 0x040022C6 RID: 8902
	public AudioClip IncineratorClose;

	// Token: 0x040022C7 RID: 8903
	public AudioClip IncineratorOpen;

	// Token: 0x040022C8 RID: 8904
	public AudioSource FlameSound;

	// Token: 0x040022C9 RID: 8905
	public ParticleSystem Flames;

	// Token: 0x040022CA RID: 8906
	public ParticleSystem Smoke;

	// Token: 0x040022CB RID: 8907
	public Transform DumpPoint;

	// Token: 0x040022CC RID: 8908
	public Transform RightDoor;

	// Token: 0x040022CD RID: 8909
	public Transform LeftDoor;

	// Token: 0x040022CE RID: 8910
	public GameObject Panel;

	// Token: 0x040022CF RID: 8911
	public UILabel TimeLabel;

	// Token: 0x040022D0 RID: 8912
	public UISprite Circle;

	// Token: 0x040022D1 RID: 8913
	public bool YandereHoldingEvidence;

	// Token: 0x040022D2 RID: 8914
	public bool Ready;

	// Token: 0x040022D3 RID: 8915
	public bool Open;

	// Token: 0x040022D4 RID: 8916
	public int DestroyedEvidence;

	// Token: 0x040022D5 RID: 8917
	public int BloodyClothing;

	// Token: 0x040022D6 RID: 8918
	public int MurderWeapons;

	// Token: 0x040022D7 RID: 8919
	public int BodyParts;

	// Token: 0x040022D8 RID: 8920
	public int Corpses;

	// Token: 0x040022D9 RID: 8921
	public int Victims;

	// Token: 0x040022DA RID: 8922
	public int Limbs;

	// Token: 0x040022DB RID: 8923
	public int ID;

	// Token: 0x040022DC RID: 8924
	public float OpenTimer;

	// Token: 0x040022DD RID: 8925
	public float Timer;

	// Token: 0x040022DE RID: 8926
	public int[] EvidenceList;

	// Token: 0x040022DF RID: 8927
	public int[] CorpseList;

	// Token: 0x040022E0 RID: 8928
	public int[] VictimList;

	// Token: 0x040022E1 RID: 8929
	public int[] LimbList;

	// Token: 0x040022E2 RID: 8930
	public int[] ConfirmedDead;
}
