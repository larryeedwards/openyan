using System;
using UnityEngine;

// Token: 0x02000516 RID: 1302
public class SpinScript : MonoBehaviour
{
	// Token: 0x0600202D RID: 8237 RVA: 0x0014EB88 File Offset: 0x0014CF88
	private void Update()
	{
		this.RotationX += this.X * Time.deltaTime;
		this.RotationY += this.Y * Time.deltaTime;
		this.RotationZ += this.Z * Time.deltaTime;
		base.transform.localEulerAngles = new Vector3(this.RotationX, this.RotationY, this.RotationZ);
	}

	// Token: 0x04002CE1 RID: 11489
	public float X;

	// Token: 0x04002CE2 RID: 11490
	public float Y;

	// Token: 0x04002CE3 RID: 11491
	public float Z;

	// Token: 0x04002CE4 RID: 11492
	private float RotationX;

	// Token: 0x04002CE5 RID: 11493
	private float RotationY;

	// Token: 0x04002CE6 RID: 11494
	private float RotationZ;
}
