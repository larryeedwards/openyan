using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class GhostScript : MonoBehaviour
{
	// Token: 0x060019DC RID: 6620 RVA: 0x000F3B94 File Offset: 0x000F1F94
	private void Update()
	{
		if (Time.timeScale > 0.0001f)
		{
			if (this.Frame > 0)
			{
				base.GetComponent<Animation>().enabled = false;
				base.gameObject.SetActive(false);
				this.Frame = 0;
			}
			this.Frame++;
		}
	}

	// Token: 0x060019DD RID: 6621 RVA: 0x000F3BE9 File Offset: 0x000F1FE9
	public void Look()
	{
		this.Neck.LookAt(this.SmartphoneCamera.position);
	}

	// Token: 0x04001F5A RID: 8026
	public Transform SmartphoneCamera;

	// Token: 0x04001F5B RID: 8027
	public Transform Neck;

	// Token: 0x04001F5C RID: 8028
	public Transform GhostEyeLocation;

	// Token: 0x04001F5D RID: 8029
	public Transform GhostEye;

	// Token: 0x04001F5E RID: 8030
	public int Frame;

	// Token: 0x04001F5F RID: 8031
	public bool Move;
}
