using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class ContainerScript : MonoBehaviour
{
	// Token: 0x06001823 RID: 6179 RVA: 0x000C8438 File Offset: 0x000C6838
	public void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000C84B0 File Offset: 0x000C68B0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Open = !this.Open;
			this.UpdatePrompts();
		}
		if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			if (this.Prompt.Yandere.Armed)
			{
				this.Weapon = this.Prompt.Yandere.EquippedWeapon.gameObject.GetComponent<WeaponScript>();
				this.Prompt.Yandere.EmptyHands();
				this.Weapon.transform.parent = this.WeaponSpot;
				this.Weapon.transform.localPosition = Vector3.zero;
				this.Weapon.transform.localEulerAngles = Vector3.zero;
				this.Weapon.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.Weapon.MyCollider.enabled = false;
				this.Weapon.Prompt.Hide();
				this.Weapon.Prompt.enabled = false;
			}
			else
			{
				this.BodyPart = this.Prompt.Yandere.PickUp;
				this.Prompt.Yandere.EmptyHands();
				this.BodyPart.transform.parent = this.BodyPartPositions[this.BodyPart.GetComponent<BodyPartScript>().Type];
				this.BodyPart.transform.localPosition = Vector3.zero;
				this.BodyPart.transform.localEulerAngles = Vector3.zero;
				this.BodyPart.gameObject.GetComponent<Rigidbody>().useGravity = false;
				this.BodyPart.MyCollider.enabled = false;
				this.BodyParts[this.BodyPart.GetComponent<BodyPartScript>().Type] = this.BodyPart;
			}
			this.Contents++;
			this.UpdatePrompts();
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (!this.Open)
			{
				base.transform.parent = this.Prompt.Yandere.Backpack;
				base.transform.localPosition = Vector3.zero;
				base.transform.localEulerAngles = Vector3.zero;
				this.Prompt.Yandere.Container = this;
				this.Prompt.Yandere.WeaponMenu.UpdateSprites();
				this.Prompt.Yandere.ObstacleDetector.gameObject.SetActive(true);
				this.Prompt.MyCollider.enabled = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				Rigidbody component = base.GetComponent<Rigidbody>();
				component.isKinematic = true;
				component.useGravity = false;
			}
			else
			{
				if (this.Weapon != null)
				{
					this.Weapon.Prompt.Circle[3].fillAmount = -1f;
					this.Weapon.Prompt.enabled = true;
					this.Weapon = null;
				}
				else
				{
					this.BodyPart = null;
					this.ID = 1;
					while (this.BodyPart == null)
					{
						this.BodyPart = this.BodyParts[this.ID];
						this.BodyParts[this.ID] = null;
						this.ID++;
					}
					this.BodyPart.Prompt.Circle[3].fillAmount = -1f;
				}
				this.Contents--;
				this.UpdatePrompts();
			}
		}
		this.Lid.localEulerAngles = new Vector3(this.Lid.localEulerAngles.x, this.Lid.localEulerAngles.y, Mathf.Lerp(this.Lid.localEulerAngles.z, (!this.Open) ? 0f : 90f, Time.deltaTime * 10f));
		if (this.Weapon != null)
		{
			this.Weapon.transform.localPosition = Vector3.zero;
			this.Weapon.transform.localEulerAngles = Vector3.zero;
		}
		this.ID = 1;
		while (this.ID < this.BodyParts.Length)
		{
			if (this.BodyParts[this.ID] != null)
			{
				this.BodyParts[this.ID].transform.localPosition = Vector3.zero;
				this.BodyParts[this.ID].transform.localEulerAngles = Vector3.zero;
			}
			this.ID++;
		}
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000C89D0 File Offset: 0x000C6DD0
	public void Drop()
	{
		base.transform.parent = null;
		base.transform.position = this.Prompt.Yandere.ObstacleDetector.transform.position + new Vector3(0f, 0.5f, 0f);
		base.transform.eulerAngles = this.Prompt.Yandere.ObstacleDetector.transform.eulerAngles;
		this.Prompt.Yandere.Container = null;
		this.Prompt.MyCollider.enabled = true;
		this.Prompt.enabled = true;
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = false;
		component.useGravity = true;
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000C8A90 File Offset: 0x000C6E90
	public void UpdatePrompts()
	{
		if (this.Open)
		{
			this.Prompt.Label[0].text = "     Close";
			if (this.Contents > 0)
			{
				this.Prompt.Label[3].text = "     Remove";
				this.Prompt.HideButton[3] = false;
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
			if (this.Prompt.Yandere.Armed)
			{
				if (!this.Prompt.Yandere.EquippedWeapon.Concealable)
				{
					if (this.Weapon == null)
					{
						this.Prompt.Label[1].text = "     Insert";
						this.Prompt.HideButton[1] = false;
					}
					else
					{
						this.Prompt.HideButton[1] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			else if (this.Prompt.Yandere.PickUp != null)
			{
				if (this.Prompt.Yandere.PickUp.BodyPart != null)
				{
					if (this.BodyParts[this.Prompt.Yandere.PickUp.gameObject.GetComponent<BodyPartScript>().Type] == null)
					{
						this.Prompt.Label[1].text = "     Insert";
						this.Prompt.HideButton[1] = false;
					}
					else
					{
						this.Prompt.HideButton[1] = true;
					}
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[1] = true;
			}
		}
		else if (this.Prompt.Label[0] != null)
		{
			this.Prompt.Label[0].text = "     Open";
			this.Prompt.HideButton[1] = true;
			this.Prompt.Label[3].text = "     Wear";
			this.Prompt.HideButton[3] = false;
		}
	}

	// Token: 0x04001920 RID: 6432
	public Transform[] BodyPartPositions;

	// Token: 0x04001921 RID: 6433
	public Transform WeaponSpot;

	// Token: 0x04001922 RID: 6434
	public Transform Lid;

	// Token: 0x04001923 RID: 6435
	public Collider GardenArea;

	// Token: 0x04001924 RID: 6436
	public Collider NEStairs;

	// Token: 0x04001925 RID: 6437
	public Collider NWStairs;

	// Token: 0x04001926 RID: 6438
	public Collider SEStairs;

	// Token: 0x04001927 RID: 6439
	public Collider SWStairs;

	// Token: 0x04001928 RID: 6440
	public PickUpScript[] BodyParts;

	// Token: 0x04001929 RID: 6441
	public PickUpScript BodyPart;

	// Token: 0x0400192A RID: 6442
	public WeaponScript Weapon;

	// Token: 0x0400192B RID: 6443
	public PromptScript Prompt;

	// Token: 0x0400192C RID: 6444
	public string SpriteName = string.Empty;

	// Token: 0x0400192D RID: 6445
	public bool CanDrop;

	// Token: 0x0400192E RID: 6446
	public bool Open;

	// Token: 0x0400192F RID: 6447
	public int Contents;

	// Token: 0x04001930 RID: 6448
	public int ID;
}
