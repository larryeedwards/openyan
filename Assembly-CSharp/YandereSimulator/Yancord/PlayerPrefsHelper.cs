using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020005C5 RID: 1477
	public static class PlayerPrefsHelper
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x001C04BD File Offset: 0x001BE8BD
		public static void SetBool(string name, bool flag)
		{
			PlayerPrefs.SetInt(name, (!flag) ? 0 : 1);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x001C04D2 File Offset: 0x001BE8D2
		public static bool GetBool(string name)
		{
			return PlayerPrefs.GetInt(name) == 1;
		}
	}
}
