using System;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class MusicRatingScript : MonoBehaviour
{
	// Token: 0x06000BED RID: 3053 RVA: 0x0005E0A7 File Offset: 0x0005C4A7
	private void Start()
	{
		if (this.SFX != null)
		{
			this.SFX.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		}
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0005E0D4 File Offset: 0x0005C4D4
	private void Update()
	{
		base.transform.localPosition += new Vector3(0f, this.Speed * Time.deltaTime, 0f);
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, new Vector3(0.2f, 0.1f, 0.1f), Time.deltaTime);
		this.Timer += Time.deltaTime * 5f;
		if (this.Timer > 1f)
		{
			this.MyRenderer.material.color = new Color(1f, 1f, 1f, 2f - this.Timer);
			if (this.MyRenderer.material.color.a <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x040009AD RID: 2477
	public Renderer MyRenderer;

	// Token: 0x040009AE RID: 2478
	public AudioSource SFX;

	// Token: 0x040009AF RID: 2479
	public float Speed;

	// Token: 0x040009B0 RID: 2480
	public float Timer;
}
