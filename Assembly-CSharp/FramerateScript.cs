using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class FramerateScript : MonoBehaviour
{
	// Token: 0x060019B6 RID: 6582 RVA: 0x000F0A71 File Offset: 0x000EEE71
	private void Start()
	{
		this.fpsText = base.GetComponent<GUIText>();
		this.timeleft = this.updateInterval;
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000F0A8C File Offset: 0x000EEE8C
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if (this.timeleft <= 0f)
		{
			this.FPS = this.accum / (float)this.frames;
			int num = Mathf.Clamp((int)this.FPS, 0, Application.targetFrameRate);
			if (num > 0)
			{
				this.fpsText.text = "FPS: " + num.ToString();
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}

	// Token: 0x04001EE2 RID: 7906
	public float updateInterval = 0.5f;

	// Token: 0x04001EE3 RID: 7907
	private GUIText fpsText;

	// Token: 0x04001EE4 RID: 7908
	private float accum;

	// Token: 0x04001EE5 RID: 7909
	private int frames;

	// Token: 0x04001EE6 RID: 7910
	private float timeleft;

	// Token: 0x04001EE7 RID: 7911
	public float FPS;
}
