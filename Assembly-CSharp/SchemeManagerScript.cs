using System;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class SchemeManagerScript : MonoBehaviour
{
	// Token: 0x06001FBD RID: 8125 RVA: 0x00145420 File Offset: 0x00143820
	private void Update()
	{
		if (this.Clock.HourTime > 15.5f)
		{
			SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, 100);
			this.Clock.Yandere.NotificationManager.CustomText = "Scheme failed! You were too slow.";
			this.Clock.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			this.Schemes.UpdateInstructions();
			base.enabled = false;
		}
		if (this.ClockCheck && this.Clock.HourTime > 8.25f)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				if (SchemeGlobals.GetSchemeStage(5) == 1)
				{
					Debug.Log("It's past 8:15 AM, so we're advancing to Stage 2 of Scheme 5.");
					SchemeGlobals.SetSchemeStage(5, 2);
					this.Schemes.UpdateInstructions();
					this.ClockCheck = false;
				}
			}
		}
	}

	// Token: 0x04002B84 RID: 11140
	public SchemesScript Schemes;

	// Token: 0x04002B85 RID: 11141
	public ClockScript Clock;

	// Token: 0x04002B86 RID: 11142
	public bool ClockCheck;

	// Token: 0x04002B87 RID: 11143
	public float Timer;
}
