using System;
using UnityEngine;

// Token: 0x0200039A RID: 922
public class DoorBoxScript : MonoBehaviour
{
	// Token: 0x060018DF RID: 6367 RVA: 0x000E42A8 File Offset: 0x000E26A8
	private void Update()
	{
		float y = Mathf.Lerp(base.transform.localPosition.y, (!this.Show) ? -630f : -530f, Time.deltaTime * 10f);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, y, base.transform.localPosition.z);
	}

	// Token: 0x04001CA2 RID: 7330
	public UILabel Label;

	// Token: 0x04001CA3 RID: 7331
	public bool Show;
}
