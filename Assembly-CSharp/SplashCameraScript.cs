using System;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public class SplashCameraScript : MonoBehaviour
{
	// Token: 0x0600202F RID: 8239 RVA: 0x0014EC0A File Offset: 0x0014D00A
	private void Start()
	{
		this.MyCamera.enabled = false;
		this.MyCamera.rect = new Rect(0f, 0.219f, 0f, 0f);
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x0014EC3C File Offset: 0x0014D03C
	private void Update()
	{
		if (this.Show)
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0.4f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0.71104f, Time.deltaTime * 10f));
			this.Timer += Time.deltaTime;
			if (this.Timer > 15f)
			{
				this.Show = false;
				this.Timer = 0f;
			}
		}
		else
		{
			this.MyCamera.rect = new Rect(this.MyCamera.rect.x, this.MyCamera.rect.y, Mathf.Lerp(this.MyCamera.rect.width, 0f, Time.deltaTime * 10f), Mathf.Lerp(this.MyCamera.rect.height, 0f, Time.deltaTime * 10f));
			if (this.MyCamera.enabled && this.MyCamera.rect.width < 0.1f)
			{
				this.MyCamera.enabled = false;
			}
		}
	}

	// Token: 0x04002CE7 RID: 11495
	public Camera MyCamera;

	// Token: 0x04002CE8 RID: 11496
	public bool Show;

	// Token: 0x04002CE9 RID: 11497
	public float Timer;
}
