using System;
using UnityEngine;

// Token: 0x02000477 RID: 1143
public class NurseScript : MonoBehaviour
{
	// Token: 0x06001E00 RID: 7680 RVA: 0x00121D3C File Offset: 0x0012013C
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_noBlink_00"].layer = 1;
		component.Blend("f02_noBlink_00");
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00121D74 File Offset: 0x00120174
	private void LateUpdate()
	{
		this.SkirtCenter.localEulerAngles = new Vector3(-15f, this.SkirtCenter.localEulerAngles.y, this.SkirtCenter.localEulerAngles.z);
	}

	// Token: 0x0400262D RID: 9773
	public GameObject Character;

	// Token: 0x0400262E RID: 9774
	public Transform SkirtCenter;
}
