using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000588 RID: 1416
public class VibrationScript : MonoBehaviour
{
	// Token: 0x0600224C RID: 8780 RVA: 0x0019BD2A File Offset: 0x0019A12A
	private void Start()
	{
		GamePad.SetVibration(PlayerIndex.One, this.Strength1, this.Strength2);
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x0019BD3E File Offset: 0x0019A13E
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.TimeLimit)
		{
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
			base.enabled = false;
		}
	}

	// Token: 0x04003780 RID: 14208
	public float Strength1;

	// Token: 0x04003781 RID: 14209
	public float Strength2;

	// Token: 0x04003782 RID: 14210
	public float TimeLimit;

	// Token: 0x04003783 RID: 14211
	public float Timer;
}
