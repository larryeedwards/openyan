using System;
using UnityEngine;

// Token: 0x02000515 RID: 1301
public class SpeedrunTimerScript : MonoBehaviour
{
	// Token: 0x06002029 RID: 8233 RVA: 0x0014EA9E File Offset: 0x0014CE9E
	private void Start()
	{
		this.Label.enabled = false;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0014EAAC File Offset: 0x0014CEAC
	private void Update()
	{
		if (!this.Police.FadeOut)
		{
			this.Timer += Time.deltaTime;
			if (this.Label.enabled)
			{
				this.Label.text = string.Empty + this.FormatTime(this.Timer);
			}
			if (Input.GetKeyDown(KeyCode.Delete))
			{
				this.Label.enabled = !this.Label.enabled;
			}
		}
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x0014EB34 File Offset: 0x0014CF34
	private string FormatTime(float time)
	{
		int num = (int)time;
		int num2 = num / 60;
		int num3 = num % 60;
		float num4 = time * 1000f;
		num4 %= 1000f;
		return string.Format("{0:00}:{1:00}:{2:000}", num2, num3, num4);
	}

	// Token: 0x04002CDE RID: 11486
	public PoliceScript Police;

	// Token: 0x04002CDF RID: 11487
	public UILabel Label;

	// Token: 0x04002CE0 RID: 11488
	public float Timer;
}
