using System;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public class NuzzleScript : MonoBehaviour
{
	// Token: 0x06001E03 RID: 7683 RVA: 0x00121DC4 File Offset: 0x001201C4
	private void Start()
	{
		this.OriginalRotation = base.transform.localEulerAngles;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00121DD8 File Offset: 0x001201D8
	private void Update()
	{
		if (!this.Down)
		{
			this.Rotate += Time.deltaTime * this.Speed;
			if (this.Rotate > this.Limit)
			{
				this.Down = true;
			}
		}
		else
		{
			this.Rotate -= Time.deltaTime * this.Speed;
			if (this.Rotate < -1f * this.Limit)
			{
				this.Down = false;
			}
		}
		base.transform.localEulerAngles = this.OriginalRotation + new Vector3(this.Rotate, 0f, 0f);
	}

	// Token: 0x0400262F RID: 9775
	public Vector3 OriginalRotation;

	// Token: 0x04002630 RID: 9776
	public float Rotate;

	// Token: 0x04002631 RID: 9777
	public float Limit;

	// Token: 0x04002632 RID: 9778
	public float Speed;

	// Token: 0x04002633 RID: 9779
	private bool Down;
}
