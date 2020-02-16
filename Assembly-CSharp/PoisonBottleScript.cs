using System;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class PoisonBottleScript : MonoBehaviour
{
	// Token: 0x06001E88 RID: 7816 RVA: 0x0012CE78 File Offset: 0x0012B278
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Theft)
			{
				this.Prompt.Yandere.TheftTimer = 0.1f;
			}
			if (this.ID == 1)
			{
				this.Prompt.Yandere.Inventory.EmeticPoison = true;
			}
			else if (this.ID == 2)
			{
				this.Prompt.Yandere.Inventory.LethalPoison = true;
			}
			else if (this.ID == 3)
			{
				this.Prompt.Yandere.Inventory.RatPoison = true;
			}
			else if (this.ID == 4)
			{
				this.Prompt.Yandere.Inventory.HeadachePoison = true;
			}
			else if (this.ID == 5)
			{
				this.Prompt.Yandere.Inventory.Tranquilizer = true;
			}
			else if (this.ID == 6)
			{
				this.Prompt.Yandere.Inventory.Sedative = true;
			}
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040027B2 RID: 10162
	public PromptScript Prompt;

	// Token: 0x040027B3 RID: 10163
	public bool Theft;

	// Token: 0x040027B4 RID: 10164
	public int ID;
}
