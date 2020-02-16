using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class CheeseScript : MonoBehaviour
{
	// Token: 0x060017CF RID: 6095 RVA: 0x000BDED0 File Offset: 0x000BC2D0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Subtitle.text = "Knowing the mouse might one day leave its hole and get the cheese...It fills you with determination.";
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.GlowingEye.SetActive(true);
			this.Timer = 5f;
		}
		if (this.Timer > 0f)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Prompt.enabled = true;
				this.Subtitle.text = string.Empty;
			}
		}
	}

	// Token: 0x040017E9 RID: 6121
	public GameObject GlowingEye;

	// Token: 0x040017EA RID: 6122
	public PromptScript Prompt;

	// Token: 0x040017EB RID: 6123
	public UILabel Subtitle;

	// Token: 0x040017EC RID: 6124
	public float Timer;
}
