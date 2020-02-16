using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000163 RID: 355
	public class ExitMaidMinigame : MonoBehaviour
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x000572A9 File Offset: 0x000556A9
		private void OnMouseOver()
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameController.GoToExitScene(true);
			}
		}
	}
}
