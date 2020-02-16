using System;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class MusicCreditScript : MonoBehaviour
{
	// Token: 0x06001DCB RID: 7627 RVA: 0x0011BF8C File Offset: 0x0011A38C
	private void Start()
	{
		base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x0011BFE0 File Offset: 0x0011A3E0
	private void Update()
	{
		if (this.Slide)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 5f)
			{
				base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			}
			else
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x + Time.deltaTime, base.transform.localPosition.y, base.transform.localPosition.z);
				base.transform.localPosition = new Vector3(base.transform.localPosition.x + Mathf.Abs(base.transform.localPosition.x * 0.01f) * (Time.deltaTime * 1000f), base.transform.localPosition.y, base.transform.localPosition.z);
				if (base.transform.localPosition.x > 400f)
				{
					base.transform.localPosition = new Vector3(400f, base.transform.localPosition.y, base.transform.localPosition.z);
					this.Panel.enabled = false;
					this.Slide = false;
					this.Timer = 0f;
				}
			}
		}
	}

	// Token: 0x04002587 RID: 9607
	public UILabel SongLabel;

	// Token: 0x04002588 RID: 9608
	public UILabel BandLabel;

	// Token: 0x04002589 RID: 9609
	public UIPanel Panel;

	// Token: 0x0400258A RID: 9610
	public bool Slide;

	// Token: 0x0400258B RID: 9611
	public float Timer;
}
