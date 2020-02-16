using System;
using UnityEngine;

// Token: 0x02000531 RID: 1329
public class StudentPortraitScript : MonoBehaviour
{
	// Token: 0x060020CB RID: 8395 RVA: 0x0015D9DB File Offset: 0x0015BDDB
	private void Start()
	{
		this.DeathShadow.SetActive(false);
		this.PrisonBars.SetActive(false);
		this.Panties.SetActive(false);
		this.Friend.SetActive(false);
	}

	// Token: 0x04002F5E RID: 12126
	public GameObject DeathShadow;

	// Token: 0x04002F5F RID: 12127
	public GameObject PrisonBars;

	// Token: 0x04002F60 RID: 12128
	public GameObject Panties;

	// Token: 0x04002F61 RID: 12129
	public GameObject Friend;

	// Token: 0x04002F62 RID: 12130
	public UITexture Portrait;
}
