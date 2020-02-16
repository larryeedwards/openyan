using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class WitnessCameraScript : MonoBehaviour
{
	// Token: 0x0600227F RID: 8831 RVA: 0x001A23EF File Offset: 0x001A07EF
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0f, 0f, 0f);
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x001A2424 File Offset: 0x001A0824
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.25f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.444444448f, Time.deltaTime * 10f));
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + Time.deltaTime * 0.09f);
			this.WitnessTimer += Time.deltaTime;
			if (this.WitnessTimer > 5f)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
			}
			if (this.Yandere.Struggling)
			{
				this.WitnessTimer = 0f;
				this.Show = false;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
				base.transform.parent = null;
			}
		}
	}

	// Token: 0x04003868 RID: 14440
	public YandereScript Yandere;

	// Token: 0x04003869 RID: 14441
	public Transform WitnessPOV;

	// Token: 0x0400386A RID: 14442
	public float WitnessTimer;

	// Token: 0x0400386B RID: 14443
	public Camera MyCamera;

	// Token: 0x0400386C RID: 14444
	public bool Show;
}
