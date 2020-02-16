using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000147 RID: 327
	public class Chair : MonoBehaviour
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x000550CC File Offset: 0x000534CC
		public static Chairs AllChairs
		{
			get
			{
				if (Chair.chairs == null || Chair.chairs.Count == 0)
				{
					Chair.chairs = new Chairs();
					foreach (Chair item in UnityEngine.Object.FindObjectsOfType<Chair>())
					{
						Chair.chairs.Add(item);
					}
				}
				return Chair.chairs;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0005512C File Offset: 0x0005352C
		public static Chair RandomChair
		{
			get
			{
				Chairs chairs = new Chairs();
				foreach (Chair chair in Chair.AllChairs)
				{
					if (chair.available)
					{
						chairs.Add(chair);
					}
				}
				if (chairs.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, chairs.Count);
					chairs[index].available = false;
					return chairs[index];
				}
				return null;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000551C4 File Offset: 0x000535C4
		private void OnDestroy()
		{
			Chair.chairs = null;
		}

		// Token: 0x04000814 RID: 2068
		private static Chairs chairs;

		// Token: 0x04000815 RID: 2069
		public bool available = true;
	}
}
