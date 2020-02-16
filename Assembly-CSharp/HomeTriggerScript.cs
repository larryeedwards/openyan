using System;
using UnityEngine;

// Token: 0x02000428 RID: 1064
public class HomeTriggerScript : MonoBehaviour
{
	// Token: 0x06001CCC RID: 7372 RVA: 0x00108CC8 File Offset: 0x001070C8
	private void Start()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x00108D23 File Offset: 0x00107123
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = this.ID;
			this.FadeIn = true;
		}
	}

	// Token: 0x06001CCE RID: 7374 RVA: 0x00108D52 File Offset: 0x00107152
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.HomeCamera.ID = 0;
			this.FadeIn = false;
		}
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00108D7C File Offset: 0x0010717C
	private void Update()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, (!this.FadeIn) ? 0f : 1f, Time.deltaTime * 10f));
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x00108E10 File Offset: 0x00107210
	public void Disable()
	{
		this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 0f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400227B RID: 8827
	public HomeCameraScript HomeCamera;

	// Token: 0x0400227C RID: 8828
	public UILabel Label;

	// Token: 0x0400227D RID: 8829
	public bool FadeIn;

	// Token: 0x0400227E RID: 8830
	public int ID;
}
