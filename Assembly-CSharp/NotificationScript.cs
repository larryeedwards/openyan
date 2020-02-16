using System;
using UnityEngine;

// Token: 0x02000476 RID: 1142
public class NotificationScript : MonoBehaviour
{
	// Token: 0x06001DFD RID: 7677 RVA: 0x00121BE8 File Offset: 0x0011FFE8
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.Icon[0].color = new Color(1f, 1f, 1f, 1f);
			this.Icon[1].color = new Color(1f, 1f, 1f, 1f);
			this.Label.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x00121C70 File Offset: 0x00120070
	private void Update()
	{
		if (!this.Display)
		{
			this.Panel.alpha -= Time.deltaTime * ((this.NotificationManager.NotificationsSpawned <= this.ID + 2) ? 1f : 3f);
			if (this.Panel.alpha <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 4f)
			{
				this.Display = false;
			}
			if (this.NotificationManager.NotificationsSpawned > this.ID + 2)
			{
				this.Display = false;
			}
		}
	}

	// Token: 0x04002626 RID: 9766
	public NotificationManagerScript NotificationManager;

	// Token: 0x04002627 RID: 9767
	public UISprite[] Icon;

	// Token: 0x04002628 RID: 9768
	public UIPanel Panel;

	// Token: 0x04002629 RID: 9769
	public UILabel Label;

	// Token: 0x0400262A RID: 9770
	public bool Display;

	// Token: 0x0400262B RID: 9771
	public float Timer;

	// Token: 0x0400262C RID: 9772
	public int ID;
}
