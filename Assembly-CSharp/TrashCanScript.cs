using System;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class TrashCanScript : MonoBehaviour
{
	// Token: 0x060021C3 RID: 8643 RVA: 0x001990F8 File Offset: 0x001974F8
	private void Update()
	{
		if (!this.Occupied)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (this.Yandere.PickUp != null)
				{
					this.Item = this.Yandere.PickUp.gameObject;
					this.Yandere.MyController.radius = 0.5f;
					this.Yandere.EmptyHands();
				}
				else
				{
					this.Item = this.Yandere.EquippedWeapon.gameObject;
					this.Yandere.DropTimer[this.Yandere.Equipped] = 0.5f;
					this.Yandere.DropWeapon(this.Yandere.Equipped);
					this.Weapon = true;
				}
				this.Item.transform.parent = this.TrashPosition;
				this.Item.GetComponent<Rigidbody>().useGravity = false;
				this.Item.GetComponent<Collider>().enabled = false;
				this.Item.GetComponent<PromptScript>().Hide();
				this.Item.GetComponent<PromptScript>().enabled = false;
				this.Occupied = true;
				this.UpdatePrompt();
			}
		}
		else if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Item.GetComponent<PromptScript>().Circle[3].fillAmount = -1f;
			this.Item.GetComponent<PromptScript>().enabled = true;
			this.Item = null;
			this.Occupied = false;
			this.Weapon = false;
			this.UpdatePrompt();
		}
		if (this.Item != null)
		{
			if (this.Weapon)
			{
				this.Item.transform.localPosition = new Vector3(0f, 0.29f, 0f);
				this.Item.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			else
			{
				this.Item.transform.localPosition = new Vector3(0f, 0f, -0.021f);
				this.Item.transform.localEulerAngles = Vector3.zero;
			}
		}
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x0019936C File Offset: 0x0019776C
	public void UpdatePrompt()
	{
		if (!this.Occupied)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.Label[0].text = "     Insert";
				this.Prompt.HideButton[0] = false;
			}
			else if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.Evidence || this.Yandere.PickUp.Suspicious)
				{
					this.Prompt.Label[0].text = "     Insert";
					this.Prompt.HideButton[0] = false;
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}
		else
		{
			this.Prompt.Label[0].text = "     Remove";
			this.Prompt.HideButton[0] = false;
		}
	}

	// Token: 0x040036CB RID: 14027
	public YandereScript Yandere;

	// Token: 0x040036CC RID: 14028
	public PromptScript Prompt;

	// Token: 0x040036CD RID: 14029
	public Transform TrashPosition;

	// Token: 0x040036CE RID: 14030
	public GameObject Item;

	// Token: 0x040036CF RID: 14031
	public bool Occupied;

	// Token: 0x040036D0 RID: 14032
	public bool Weapon;
}
