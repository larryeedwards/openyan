using System;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class IntroCircleScript : MonoBehaviour
{
	// Token: 0x06001D00 RID: 7424 RVA: 0x0010C618 File Offset: 0x0010AA18
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < this.StartTime.Length && this.Timer > this.StartTime[this.ID])
		{
			this.CurrentTime = this.Duration[this.ID];
			this.LastTime = this.Duration[this.ID];
			this.Label.text = this.Text[this.ID];
			this.ID++;
		}
		if (this.CurrentTime > 0f)
		{
			this.CurrentTime -= Time.deltaTime;
		}
		if (this.Timer > 1f)
		{
			this.Sprite.fillAmount = this.CurrentTime / this.LastTime;
			if (this.Sprite.fillAmount == 0f)
			{
				this.Label.text = string.Empty;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentTime -= 5f;
			this.Timer += 5f;
		}
	}

	// Token: 0x0400231C RID: 8988
	public UISprite Sprite;

	// Token: 0x0400231D RID: 8989
	public UILabel Label;

	// Token: 0x0400231E RID: 8990
	public float[] StartTime;

	// Token: 0x0400231F RID: 8991
	public float[] Duration;

	// Token: 0x04002320 RID: 8992
	public string[] Text;

	// Token: 0x04002321 RID: 8993
	public float CurrentTime;

	// Token: 0x04002322 RID: 8994
	public float LastTime;

	// Token: 0x04002323 RID: 8995
	public float Timer;

	// Token: 0x04002324 RID: 8996
	public int ID;
}
