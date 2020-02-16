using System;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class HomePantiesScript : MonoBehaviour
{
	// Token: 0x06001CB6 RID: 7350 RVA: 0x00106954 File Offset: 0x00104D54
	private void Update()
	{
		float y = (this.PantyChanger.Selected != this.ID) ? 0f : (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed);
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x040021FE RID: 8702
	public HomePantyChangerScript PantyChanger;

	// Token: 0x040021FF RID: 8703
	public float RotationSpeed;

	// Token: 0x04002200 RID: 8704
	public int ID;
}
