using System;
using UnityEngine;

// Token: 0x0200034F RID: 847
public class CabinetDoorScript : MonoBehaviour
{
	// Token: 0x0600179C RID: 6044 RVA: 0x000BAF4C File Offset: 0x000B934C
	private void Update()
	{
		if (this.Timer < 2f)
		{
			this.Timer += Time.deltaTime;
			if (this.Open)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0.41775f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			}
		}
	}

	// Token: 0x04001769 RID: 5993
	public PromptScript Prompt;

	// Token: 0x0400176A RID: 5994
	public bool Locked;

	// Token: 0x0400176B RID: 5995
	public bool Open;

	// Token: 0x0400176C RID: 5996
	public float Timer;
}
