using System;
using UnityEngine;

// Token: 0x020003E4 RID: 996
public class GenericBentoScript : MonoBehaviour
{
	// Token: 0x060019D6 RID: 6614 RVA: 0x000F3658 File Offset: 0x000F1A58
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.EmeticPoison)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = false;
				this.Prompt.Yandere.PoisonType = 1;
			}
			else
			{
				this.Prompt.Yandere.Inventory.RatPoison = false;
				this.Prompt.Yandere.PoisonType = 3;
			}
			this.Emetic = true;
			this.ShutOff();
		}
		else if (this.Prompt.Circle[1].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.Sedative)
			{
				this.Prompt.Yandere.Inventory.Sedative = false;
			}
			else
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = false;
			}
			this.Prompt.Yandere.PoisonType = 4;
			this.Tranquil = true;
			this.ShutOff();
		}
		else if (this.Prompt.Circle[2].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Inventory.LethalPoison)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			else
			{
				this.Prompt.Yandere.Inventory.ChemicalPoison = false;
				this.Prompt.Yandere.PoisonType = 2;
			}
			this.Lethal = true;
			this.ShutOff();
		}
		else if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.HeadachePoison = false;
			this.Prompt.Yandere.PoisonType = 5;
			this.Headache = true;
			this.ShutOff();
		}
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x000F3878 File Offset: 0x000F1C78
	private void ShutOff()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
		this.PoisonSpot = gameObject.transform;
		this.PoisonSpot.position = new Vector3(this.PoisonSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PoisonSpot.position.z);
		this.PoisonSpot.LookAt(this.Prompt.Yandere.transform.position);
		this.PoisonSpot.Translate(Vector3.forward * 0.25f);
		this.Prompt.Yandere.CharacterAnimation["f02_poisoning_00"].speed = 2f;
		this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_poisoning_00");
		this.Prompt.Yandere.StudentManager.UpdateAllBentos();
		this.Prompt.Yandere.TargetBento = this;
		this.Prompt.Yandere.Poisoning = true;
		this.Prompt.Yandere.CanMove = false;
		this.Tampered = true;
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x000F39E8 File Offset: 0x000F1DE8
	public void UpdatePrompts()
	{
		this.Prompt.HideButton[0] = true;
		this.Prompt.HideButton[1] = true;
		this.Prompt.HideButton[2] = true;
		this.Prompt.HideButton[3] = true;
		if (this.Prompt.Yandere.Inventory.EmeticPoison || this.Prompt.Yandere.Inventory.RatPoison)
		{
			this.Prompt.HideButton[0] = false;
		}
		if (this.Prompt.Yandere.Inventory.Tranquilizer || this.Prompt.Yandere.Inventory.Sedative)
		{
			this.Prompt.HideButton[1] = false;
		}
		if (this.Prompt.Yandere.Inventory.LethalPoison || this.Prompt.Yandere.Inventory.ChemicalPoison)
		{
			this.Prompt.HideButton[2] = false;
		}
		if (this.Prompt.Yandere.Inventory.HeadachePoison)
		{
			this.Prompt.HideButton[3] = false;
		}
	}

	// Token: 0x04001F4F RID: 8015
	public GameObject EmptyGameObject;

	// Token: 0x04001F50 RID: 8016
	public Transform PoisonSpot;

	// Token: 0x04001F51 RID: 8017
	public PromptScript Prompt;

	// Token: 0x04001F52 RID: 8018
	public bool Emetic;

	// Token: 0x04001F53 RID: 8019
	public bool Tranquil;

	// Token: 0x04001F54 RID: 8020
	public bool Headache;

	// Token: 0x04001F55 RID: 8021
	public bool Lethal;

	// Token: 0x04001F56 RID: 8022
	public bool Tampered;

	// Token: 0x04001F57 RID: 8023
	public int StudentID;
}
