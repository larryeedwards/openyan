using System;
using UnityEngine;

// Token: 0x02000514 RID: 1300
public class SpeedrunMenuScript : MonoBehaviour
{
	// Token: 0x06002026 RID: 8230 RVA: 0x0014EA78 File Offset: 0x0014CE78
	private void Start()
	{
		this.YandereAnim["f02_nierRun_00"].speed = 1.5f;
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x0014EA94 File Offset: 0x0014CE94
	private void Update()
	{
	}

	// Token: 0x04002CDD RID: 11485
	public Animation YandereAnim;
}
