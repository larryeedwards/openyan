using System;
using UnityEngine;

// Token: 0x02000587 RID: 1415
public class VibrateScript : MonoBehaviour
{
	// Token: 0x06002249 RID: 8777 RVA: 0x0019BCA9 File Offset: 0x0019A0A9
	private void Start()
	{
		this.Origin = base.transform.localPosition;
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x0019BCBC File Offset: 0x0019A0BC
	private void Update()
	{
		base.transform.localPosition = new Vector3(this.Origin.x + UnityEngine.Random.Range(-5f, 5f), this.Origin.y + UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.z);
	}

	// Token: 0x0400377F RID: 14207
	public Vector3 Origin;
}
