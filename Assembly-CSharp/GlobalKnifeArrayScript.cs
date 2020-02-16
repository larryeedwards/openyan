using System;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
public class GlobalKnifeArrayScript : MonoBehaviour
{
	// Token: 0x060019E3 RID: 6627 RVA: 0x000F4394 File Offset: 0x000F2794
	public void ActivateKnives()
	{
		foreach (TimeStopKnifeScript timeStopKnifeScript in this.Knives)
		{
			if (timeStopKnifeScript != null)
			{
				timeStopKnifeScript.Unfreeze = true;
			}
		}
		this.ID = 0;
	}

	// Token: 0x04001F66 RID: 8038
	public TimeStopKnifeScript[] Knives;

	// Token: 0x04001F67 RID: 8039
	public int ID;
}
