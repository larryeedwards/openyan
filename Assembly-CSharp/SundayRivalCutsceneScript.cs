using System;
using UnityEngine;

// Token: 0x0200053C RID: 1340
public class SundayRivalCutsceneScript : MonoBehaviour
{
	// Token: 0x0600214D RID: 8525 RVA: 0x0018C135 File Offset: 0x0018A535
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Sunday)
		{
			base.gameObject.SetActive(false);
		}
	}
}
