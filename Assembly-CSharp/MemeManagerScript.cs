using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class MemeManagerScript : MonoBehaviour
{
	// Token: 0x06001DA1 RID: 7585 RVA: 0x00118F00 File Offset: 0x00117300
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			foreach (GameObject gameObject in this.Memes)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0400252E RID: 9518
	[SerializeField]
	private GameObject[] Memes;
}
