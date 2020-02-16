using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class GrandfatherScript : MonoBehaviour
{
	// Token: 0x06001C56 RID: 7254 RVA: 0x000FD3A4 File Offset: 0x000FB7A4
	private void Update()
	{
		if (!this.Flip)
		{
			if ((double)this.Force < 0.1)
			{
				this.Force += Time.deltaTime * 0.1f * this.Speed;
			}
		}
		else if ((double)this.Force > -0.1)
		{
			this.Force -= Time.deltaTime * 0.1f * this.Speed;
		}
		this.Rotation += this.Force;
		if (this.Rotation > 1f)
		{
			this.Flip = true;
		}
		else if (this.Rotation < -1f)
		{
			this.Flip = false;
		}
		if (this.Rotation > 5f)
		{
			this.Rotation = 5f;
		}
		else if (this.Rotation < -5f)
		{
			this.Rotation = -5f;
		}
		this.Pendulum.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		this.MinuteHand.localEulerAngles = new Vector3(this.MinuteHand.localEulerAngles.x, this.MinuteHand.localEulerAngles.y, this.Clock.Minute * 6f);
		this.HourHand.localEulerAngles = new Vector3(this.HourHand.localEulerAngles.x, this.HourHand.localEulerAngles.y, this.Clock.Hour * 30f);
	}

	// Token: 0x0400207C RID: 8316
	public ClockScript Clock;

	// Token: 0x0400207D RID: 8317
	public Transform MinuteHand;

	// Token: 0x0400207E RID: 8318
	public Transform HourHand;

	// Token: 0x0400207F RID: 8319
	public Transform Pendulum;

	// Token: 0x04002080 RID: 8320
	public float Rotation;

	// Token: 0x04002081 RID: 8321
	public float Force;

	// Token: 0x04002082 RID: 8322
	public float Speed;

	// Token: 0x04002083 RID: 8323
	public bool Flip;
}
