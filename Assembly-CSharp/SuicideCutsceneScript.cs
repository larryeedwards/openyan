using System;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class SuicideCutsceneScript : MonoBehaviour
{
	// Token: 0x06002147 RID: 8519 RVA: 0x0018B720 File Offset: 0x00189B20
	private void Start()
	{
		this.PointLight.color = new Color(0.1f, 0.1f, 0.1f, 1f);
		this.Door.eulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x0018B770 File Offset: 0x00189B70
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 2f)
		{
			this.Speed += Time.deltaTime;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * this.Speed);
			this.PointLight.color = new Color(0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 0.1f + this.Rotation / -45f * 0.9f, 1f);
			this.Door.eulerAngles = new Vector3(0f, this.Rotation, 0f);
		}
	}

	// Token: 0x0400354A RID: 13642
	public Light PointLight;

	// Token: 0x0400354B RID: 13643
	public Transform Door;

	// Token: 0x0400354C RID: 13644
	public float Timer;

	// Token: 0x0400354D RID: 13645
	public float Rotation;

	// Token: 0x0400354E RID: 13646
	public float Speed;
}
