using System;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public class EyeTestScript : MonoBehaviour
{
	// Token: 0x06002357 RID: 9047 RVA: 0x001BF760 File Offset: 0x001BDB60
	private void Start()
	{
		this.MyAnimation["moodyEyes_00"].layer = 1;
		this.MyAnimation.Play("moodyEyes_00");
		this.MyAnimation["moodyEyes_00"].weight = 1f;
		this.MyAnimation.Play("moodyEyes_00");
	}

	// Token: 0x04003D25 RID: 15653
	public Animation MyAnimation;
}
