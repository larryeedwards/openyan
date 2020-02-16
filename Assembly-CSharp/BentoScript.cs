using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class BentoScript : MonoBehaviour
{
	// Token: 0x06001744 RID: 5956 RVA: 0x000B7618 File Offset: 0x000B5A18
	private void Start()
	{
		if (this.Prompt.Yandere != null)
		{
			this.Yandere = this.Prompt.Yandere;
		}
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x000B7644 File Offset: 0x000B5A44
	private void Update()
	{
		if (this.Yandere == null)
		{
			if (this.Prompt.Yandere != null)
			{
				this.Yandere = this.Prompt.Yandere;
			}
		}
		else if (this.Yandere.Inventory.EmeticPoison || this.Yandere.Inventory.RatPoison || this.Yandere.Inventory.LethalPoison)
		{
			this.Prompt.enabled = true;
			if (!this.Yandere.Inventory.EmeticPoison && !this.Yandere.Inventory.RatPoison)
			{
				this.Prompt.HideButton[0] = true;
			}
			else
			{
				this.Prompt.HideButton[0] = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				if (this.Yandere.Inventory.EmeticPoison)
				{
					this.Yandere.Inventory.EmeticPoison = false;
					this.Yandere.PoisonType = 1;
				}
				else
				{
					this.Yandere.Inventory.RatPoison = false;
					this.Yandere.PoisonType = 3;
				}
				this.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
				this.Yandere.PoisonSpot = this.PoisonSpot;
				this.Yandere.Poisoning = true;
				this.Yandere.CanMove = false;
				base.enabled = false;
				this.Poison = 1;
				if (this.ID != 1)
				{
					this.StudentManager.Students[this.ID].Emetic = true;
				}
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
			if (this.ID == 11 || this.ID == 6)
			{
				this.Prompt.HideButton[1] = !this.Prompt.Yandere.Inventory.LethalPoison;
				if (this.Prompt.Circle[1].fillAmount == 0f)
				{
					this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
					this.Prompt.Yandere.Inventory.LethalPoison = false;
					this.StudentManager.Students[this.ID].Lethal = true;
					this.Prompt.Yandere.PoisonSpot = this.PoisonSpot;
					this.Prompt.Yandere.Poisoning = true;
					this.Prompt.Yandere.CanMove = false;
					this.Prompt.Yandere.PoisonType = 2;
					base.enabled = false;
					this.Poison = 2;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else
		{
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x040016B9 RID: 5817
	public StudentManagerScript StudentManager;

	// Token: 0x040016BA RID: 5818
	public YandereScript Yandere;

	// Token: 0x040016BB RID: 5819
	public Transform PoisonSpot;

	// Token: 0x040016BC RID: 5820
	public PromptScript Prompt;

	// Token: 0x040016BD RID: 5821
	public int Poison;

	// Token: 0x040016BE RID: 5822
	public int ID;
}
