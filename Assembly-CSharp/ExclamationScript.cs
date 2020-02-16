using System;
using UnityEngine;

// Token: 0x020003C9 RID: 969
public class ExclamationScript : MonoBehaviour
{
	// Token: 0x06001984 RID: 6532 RVA: 0x000EDA80 File Offset: 0x000EBE80
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0f));
		this.MainCamera = Camera.main;
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x000EDAD8 File Offset: 0x000EBED8
	private void Update()
	{
		this.Timer -= Time.deltaTime;
		if (this.Timer > 0f)
		{
			base.transform.LookAt(this.MainCamera.transform);
			if (this.Timer > 1.5f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				this.Alpha = Mathf.Lerp(this.Alpha, 0.5f, Time.deltaTime * 10f);
				this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
			}
			else
			{
				if (base.transform.localScale.x > 0.1f)
				{
					base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
				}
				else
				{
					base.transform.localScale = Vector3.zero;
				}
				this.Alpha = Mathf.Lerp(this.Alpha, 0f, Time.deltaTime * 10f);
				this.Graphic.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, this.Alpha));
			}
		}
	}

	// Token: 0x04001E3C RID: 7740
	public Renderer Graphic;

	// Token: 0x04001E3D RID: 7741
	public float Alpha;

	// Token: 0x04001E3E RID: 7742
	public float Timer;

	// Token: 0x04001E3F RID: 7743
	public Camera MainCamera;
}
