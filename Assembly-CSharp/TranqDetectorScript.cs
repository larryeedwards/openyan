using System;
using UnityEngine;

// Token: 0x02000559 RID: 1369
public class TranqDetectorScript : MonoBehaviour
{
	// Token: 0x060021BD RID: 8637 RVA: 0x00198C65 File Offset: 0x00197065
	private void Start()
	{
		this.Checklist.alpha = 0f;
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x00198C78 File Offset: 0x00197078
	private void Update()
	{
		if (!this.StopChecking)
		{
			if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
			{
				if (SchoolGlobals.KidnapVictim > 0)
				{
					this.KidnappingLabel.text = "There is no room for another prisoner in your basement.";
				}
				else
				{
					if (this.Yandere.Inventory.Tranquilizer || this.Yandere.Inventory.Sedative)
					{
						this.TranquilizerIcon.spriteName = "Yes";
					}
					else
					{
						this.TranquilizerIcon.spriteName = "No";
					}
					if (this.Yandere.Followers != 1)
					{
						this.FollowerIcon.spriteName = "No";
					}
					else if (this.Yandere.Follower.Male)
					{
						this.KidnappingLabel.text = "You cannot kidnap male students at this point in time.";
						this.FollowerIcon.spriteName = "No";
					}
					else
					{
						this.KidnappingLabel.text = "Kidnapping Checklist";
						this.FollowerIcon.spriteName = "Yes";
					}
					this.BiologyIcon.spriteName = ((ClassGlobals.BiologyGrade + ClassGlobals.BiologyBonus == 0) ? "No" : "Yes");
					if (!this.Yandere.Armed)
					{
						this.SyringeIcon.spriteName = "No";
					}
					else if (this.Yandere.EquippedWeapon.WeaponID != 3)
					{
						this.SyringeIcon.spriteName = "No";
					}
					else
					{
						this.SyringeIcon.spriteName = "Yes";
					}
					if (this.Door.Open || this.Door.Timer < 1f)
					{
						this.DoorIcon.spriteName = "No";
					}
					else
					{
						this.DoorIcon.spriteName = "Yes";
					}
				}
				this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 1f, Time.deltaTime);
			}
			else
			{
				this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
			}
		}
		else
		{
			this.Checklist.alpha = Mathf.MoveTowards(this.Checklist.alpha, 0f, Time.deltaTime);
			if (this.Checklist.alpha == 0f)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x00198F10 File Offset: 0x00197310
	public void TranqCheck()
	{
		if (!this.StopChecking && this.KidnappingLabel.text == "Kidnapping Checklist" && this.TranquilizerIcon.spriteName == "Yes" && this.FollowerIcon.spriteName == "Yes" && this.BiologyIcon.spriteName == "Yes" && this.SyringeIcon.spriteName == "Yes" && this.DoorIcon.spriteName == "Yes")
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.TranqClips[UnityEngine.Random.Range(0, this.TranqClips.Length)];
			component.Play();
			this.Door.Prompt.Hide();
			this.Door.Prompt.enabled = false;
			this.Door.enabled = false;
			this.Yandere.Inventory.Tranquilizer = false;
			if (!this.Yandere.Follower.Male)
			{
				this.Yandere.CanTranq = true;
			}
			this.Yandere.EquippedWeapon.Type = WeaponType.Syringe;
			this.Yandere.AttackManager.Stealth = true;
			this.StopChecking = true;
		}
	}

	// Token: 0x040036BE RID: 14014
	public YandereScript Yandere;

	// Token: 0x040036BF RID: 14015
	public DoorScript Door;

	// Token: 0x040036C0 RID: 14016
	public UIPanel Checklist;

	// Token: 0x040036C1 RID: 14017
	public Collider MyCollider;

	// Token: 0x040036C2 RID: 14018
	public UILabel KidnappingLabel;

	// Token: 0x040036C3 RID: 14019
	public UISprite TranquilizerIcon;

	// Token: 0x040036C4 RID: 14020
	public UISprite FollowerIcon;

	// Token: 0x040036C5 RID: 14021
	public UISprite BiologyIcon;

	// Token: 0x040036C6 RID: 14022
	public UISprite SyringeIcon;

	// Token: 0x040036C7 RID: 14023
	public UISprite DoorIcon;

	// Token: 0x040036C8 RID: 14024
	public bool StopChecking;

	// Token: 0x040036C9 RID: 14025
	public AudioClip[] TranqClips;
}
