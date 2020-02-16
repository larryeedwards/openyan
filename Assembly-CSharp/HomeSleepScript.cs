using System;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class HomeSleepScript : MonoBehaviour
{
	// Token: 0x06001CCA RID: 7370 RVA: 0x00108BD0 File Offset: 0x00106FD0
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.HomeDarkness.FadeOut)
		{
			if (Input.GetButtonDown("A"))
			{
				this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
				this.HomeDarkness.Cyberstalking = true;
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

	// Token: 0x04002277 RID: 8823
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002278 RID: 8824
	public HomeYandereScript HomeYandere;

	// Token: 0x04002279 RID: 8825
	public HomeCameraScript HomeCamera;

	// Token: 0x0400227A RID: 8826
	public HomeWindowScript HomeWindow;
}
