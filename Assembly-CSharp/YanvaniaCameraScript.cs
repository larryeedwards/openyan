using System;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class YanvaniaCameraScript : MonoBehaviour
{
	// Token: 0x060022FD RID: 8957 RVA: 0x001B78EE File Offset: 0x001B5CEE
	private void Start()
	{
		base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f);
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x001B7924 File Offset: 0x001B5D24
	private void FixedUpdate()
	{
		this.TargetZoom += Input.GetAxis("Mouse ScrollWheel");
		if (this.TargetZoom < 0f)
		{
			this.TargetZoom = 0f;
		}
		if (this.TargetZoom > 3.85f)
		{
			this.TargetZoom = 3.85f;
		}
		this.Zoom = Mathf.Lerp(this.Zoom, this.TargetZoom, Time.deltaTime);
		if (!this.Cutscene)
		{
			base.transform.position = this.Yanmont.transform.position + new Vector3(0f, 1.5f, -5.85f + this.Zoom);
			if (base.transform.position.x > 47.9f)
			{
				base.transform.position = new Vector3(47.9f, base.transform.position.y, base.transform.position.z);
			}
		}
		else
		{
			if (this.StopMusic)
			{
				AudioSource component = this.Jukebox.GetComponent<AudioSource>();
				component.volume -= Time.deltaTime * ((this.Yanmont.Health <= 0f) ? 0.025f : 0.2f);
				if (component.volume <= 0f)
				{
					this.StopMusic = false;
				}
			}
			base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34.675f, Time.deltaTime * this.Yanmont.walkSpeed), 8f, -5.85f + this.Zoom);
		}
	}

	// Token: 0x04003BED RID: 15341
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003BEE RID: 15342
	public GameObject Jukebox;

	// Token: 0x04003BEF RID: 15343
	public bool Cutscene;

	// Token: 0x04003BF0 RID: 15344
	public bool StopMusic = true;

	// Token: 0x04003BF1 RID: 15345
	public float TargetZoom;

	// Token: 0x04003BF2 RID: 15346
	public float Zoom;
}
