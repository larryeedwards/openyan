using System;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class SabotageVendingMachineScript : MonoBehaviour
{
	// Token: 0x06001F59 RID: 8025 RVA: 0x0014059F File Offset: 0x0013E99F
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x001405B8 File Offset: 0x0013E9B8
	private void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					if (SchemeGlobals.GetSchemeStage(4) == 2)
					{
						SchemeGlobals.SetSchemeStage(4, 3);
						this.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
					if (this.Yandere.StudentManager.Students[11] != null && DateGlobals.Weekday == DayOfWeek.Thursday)
					{
						this.Yandere.StudentManager.Students[11].Hungry = true;
						this.Yandere.StudentManager.Students[11].Fed = false;
					}
					UnityEngine.Object.Instantiate<GameObject>(this.SabotageSparks, new Vector3(-2.5f, 5.3605f, -32.982f), Quaternion.identity);
					this.VendingMachine.Sabotaged = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					base.enabled = false;
				}
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04002A7F RID: 10879
	public VendingMachineScript VendingMachine;

	// Token: 0x04002A80 RID: 10880
	public GameObject SabotageSparks;

	// Token: 0x04002A81 RID: 10881
	public YandereScript Yandere;

	// Token: 0x04002A82 RID: 10882
	public PromptScript Prompt;
}
