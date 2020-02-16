using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class LocationScript : MonoBehaviour
{
	// Token: 0x06001D7D RID: 7549 RVA: 0x001159EC File Offset: 0x00113DEC
	private void Start()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
		this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, 0f);
	}

	// Token: 0x06001D7E RID: 7550 RVA: 0x00115A98 File Offset: 0x00113E98
	private void Update()
	{
		if (this.Show)
		{
			this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, this.BG.color.a + Time.deltaTime * 10f);
			if (this.BG.color.a > 1f)
			{
				this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, 1f);
			}
			this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, this.BG.color.a);
		}
		else
		{
			this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, this.BG.color.a - Time.deltaTime * 10f);
			if (this.BG.color.a < 0f)
			{
				this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, 0f);
			}
			this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, this.BG.color.a);
		}
	}

	// Token: 0x040024CB RID: 9419
	public UILabel Label;

	// Token: 0x040024CC RID: 9420
	public UISprite BG;

	// Token: 0x040024CD RID: 9421
	public bool Show;
}
