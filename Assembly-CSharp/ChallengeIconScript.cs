using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class ChallengeIconScript : MonoBehaviour
{
	// Token: 0x060017A2 RID: 6050 RVA: 0x000BBD30 File Offset: 0x000BA130
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.R = 1f;
			this.G = 0f;
			this.B = 0f;
		}
		else
		{
			this.R = 1f;
			this.G = 1f;
			this.B = 1f;
		}
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000BBD90 File Offset: 0x000BA190
	private void Update()
	{
		if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
		{
			if (this.Icon != null)
			{
				this.LargeIcon.mainTexture = this.Icon.mainTexture;
			}
			this.Dark -= Time.deltaTime * 10f;
			if (this.Dark < 0f)
			{
				this.Dark = 0f;
			}
		}
		else
		{
			this.Dark += Time.deltaTime * 10f;
			if (this.Dark > 1f)
			{
				this.Dark = 1f;
			}
		}
		this.IconFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.NameFrame.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		this.Name.color = new Color(this.Dark * this.R, this.Dark * this.G, this.Dark * this.B, 1f);
		if (GameGlobals.LoveSick)
		{
			if (base.transform.position.x > -0.125f && base.transform.position.x < 0.125f)
			{
				this.IconFrame.color = Color.white;
				this.NameFrame.color = Color.white;
				this.Name.color = Color.white;
			}
			else
			{
				this.IconFrame.color = new Color(this.R, this.G, this.B, 1f);
				this.NameFrame.color = new Color(this.R, this.G, this.B, 1f);
				this.Name.color = new Color(this.R, this.G, this.B, 1f);
			}
		}
	}

	// Token: 0x04001783 RID: 6019
	public UITexture LargeIcon;

	// Token: 0x04001784 RID: 6020
	public UISprite IconFrame;

	// Token: 0x04001785 RID: 6021
	public UISprite NameFrame;

	// Token: 0x04001786 RID: 6022
	public UITexture Icon;

	// Token: 0x04001787 RID: 6023
	public UILabel Name;

	// Token: 0x04001788 RID: 6024
	public float Dark;

	// Token: 0x04001789 RID: 6025
	private float R;

	// Token: 0x0400178A RID: 6026
	private float G;

	// Token: 0x0400178B RID: 6027
	private float B;
}
