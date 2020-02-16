using System;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class VendingMachineScript : MonoBehaviour
{
	// Token: 0x06002246 RID: 8774 RVA: 0x0019BAB0 File Offset: 0x00199EB0
	private void Start()
	{
		if (this.SnackMachine)
		{
			this.Prompt.Text[0] = "Buy Snack for $" + this.Price + ".00";
		}
		else
		{
			this.Prompt.Text[0] = "Buy Drink for $" + this.Price + ".00";
		}
		this.Prompt.Label[0].text = "     " + this.Prompt.Text[0];
	}

	// Token: 0x06002247 RID: 8775 RVA: 0x0019BB44 File Offset: 0x00199F44
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Inventory.Money >= (float)this.Price)
			{
				if (!this.Sabotaged)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Cans[UnityEngine.Random.Range(0, this.Cans.Length)], this.CanSpawn.position, this.CanSpawn.rotation);
					gameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				}
				if (this.SnackMachine && SchemeGlobals.GetSchemeStage(4) == 3)
				{
					SchemeGlobals.SetSchemeStage(4, 4);
					this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
				}
				this.Prompt.Yandere.Inventory.Money -= (float)this.Price;
				this.Prompt.Yandere.Inventory.UpdateMoney();
			}
			else
			{
				this.Prompt.Yandere.NotificationManager.CustomText = "Not enough money!";
				this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			}
		}
	}

	// Token: 0x04003779 RID: 14201
	public PromptScript Prompt;

	// Token: 0x0400377A RID: 14202
	public Transform CanSpawn;

	// Token: 0x0400377B RID: 14203
	public GameObject[] Cans;

	// Token: 0x0400377C RID: 14204
	public bool SnackMachine;

	// Token: 0x0400377D RID: 14205
	public bool Sabotaged;

	// Token: 0x0400377E RID: 14206
	public int Price;
}
