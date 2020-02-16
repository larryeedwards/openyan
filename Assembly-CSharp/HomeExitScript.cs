using System;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class HomeExitScript : MonoBehaviour
{
	// Token: 0x06001CA2 RID: 7330 RVA: 0x001038A8 File Offset: 0x00101CA8
	private void Start()
	{
		UILabel uilabel = this.Labels[2];
		uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		if (HomeGlobals.Night)
		{
			UILabel uilabel2 = this.Labels[1];
			uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 0.5f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
		}
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x0010398C File Offset: 0x00101D8C
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (this.InputManager.TappedDown)
			{
				this.ID++;
				if (this.ID > 3)
				{
					this.ID = 1;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (this.InputManager.TappedUp)
			{
				this.ID--;
				if (this.ID < 1)
				{
					this.ID = 3;
				}
				this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 50f - (float)this.ID * 50f, this.Highlight.localPosition.z);
			}
			if (Input.GetButtonDown("A") && this.Labels[this.ID].color.a == 1f)
			{
				if (this.ID == 1)
				{
					if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
					{
						this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
					}
					else
					{
						this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
					}
				}
				else if (this.ID == 2)
				{
					this.HomeDarkness.Sprite.color = new Color(1f, 1f, 1f, 0f);
				}
				else
				{
					this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				}
				this.HomeDarkness.FadeOut = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002195 RID: 8597
	public InputManagerScript InputManager;

	// Token: 0x04002196 RID: 8598
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002197 RID: 8599
	public HomeYandereScript HomeYandere;

	// Token: 0x04002198 RID: 8600
	public HomeCameraScript HomeCamera;

	// Token: 0x04002199 RID: 8601
	public HomeWindowScript HomeWindow;

	// Token: 0x0400219A RID: 8602
	public Transform Highlight;

	// Token: 0x0400219B RID: 8603
	public UILabel[] Labels;

	// Token: 0x0400219C RID: 8604
	public int ID = 1;
}
