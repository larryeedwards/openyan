using System;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class DramaticPanUpScript : MonoBehaviour
{
	// Token: 0x060018F1 RID: 6385 RVA: 0x000E63A0 File Offset: 0x000E47A0
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Pan = true;
		}
		if (this.Pan)
		{
			this.Power += Time.deltaTime * 0.5f;
			this.Height = Mathf.Lerp(this.Height, 1.4f, this.Power * Time.deltaTime);
			base.transform.localPosition = new Vector3(0f, this.Height, 1f);
		}
	}

	// Token: 0x04001CD2 RID: 7378
	public bool Pan;

	// Token: 0x04001CD3 RID: 7379
	public float Height;

	// Token: 0x04001CD4 RID: 7380
	public float Power;
}
