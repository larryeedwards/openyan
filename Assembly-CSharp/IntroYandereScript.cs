using System;
using UnityEngine;

// Token: 0x0200043B RID: 1083
public class IntroYandereScript : MonoBehaviour
{
	// Token: 0x06001D0B RID: 7435 RVA: 0x0010FE7C File Offset: 0x0010E27C
	private void LateUpdate()
	{
		this.Hips.localEulerAngles = new Vector3(this.Hips.localEulerAngles.x + this.X, this.Hips.localEulerAngles.y, this.Hips.localEulerAngles.z);
		this.Spine.localEulerAngles = new Vector3(this.Spine.localEulerAngles.x + this.X, this.Spine.localEulerAngles.y, this.Spine.localEulerAngles.z);
		this.Spine1.localEulerAngles = new Vector3(this.Spine1.localEulerAngles.x + this.X, this.Spine1.localEulerAngles.y, this.Spine1.localEulerAngles.z);
		this.Spine2.localEulerAngles = new Vector3(this.Spine2.localEulerAngles.x + this.X, this.Spine2.localEulerAngles.y, this.Spine2.localEulerAngles.z);
		this.Spine3.localEulerAngles = new Vector3(this.Spine3.localEulerAngles.x + this.X, this.Spine3.localEulerAngles.y, this.Spine3.localEulerAngles.z);
		this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x + this.X, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
		this.Head.localEulerAngles = new Vector3(this.Head.localEulerAngles.x + this.X, this.Head.localEulerAngles.y, this.Head.localEulerAngles.z);
		this.RightUpLeg.localEulerAngles = new Vector3(this.RightUpLeg.localEulerAngles.x - this.X, this.RightUpLeg.localEulerAngles.y, this.RightUpLeg.localEulerAngles.z);
		this.RightLeg.localEulerAngles = new Vector3(this.RightLeg.localEulerAngles.x - this.X, this.RightLeg.localEulerAngles.y, this.RightLeg.localEulerAngles.z);
		this.RightFoot.localEulerAngles = new Vector3(this.RightFoot.localEulerAngles.x - this.X, this.RightFoot.localEulerAngles.y, this.RightFoot.localEulerAngles.z);
		this.LeftUpLeg.localEulerAngles = new Vector3(this.LeftUpLeg.localEulerAngles.x - this.X, this.LeftUpLeg.localEulerAngles.y, this.LeftUpLeg.localEulerAngles.z);
		this.LeftLeg.localEulerAngles = new Vector3(this.LeftLeg.localEulerAngles.x - this.X, this.LeftLeg.localEulerAngles.y, this.LeftLeg.localEulerAngles.z);
		this.LeftFoot.localEulerAngles = new Vector3(this.LeftFoot.localEulerAngles.x - this.X, this.LeftFoot.localEulerAngles.y, this.LeftFoot.localEulerAngles.z);
	}

	// Token: 0x04002382 RID: 9090
	public Transform Hips;

	// Token: 0x04002383 RID: 9091
	public Transform Spine;

	// Token: 0x04002384 RID: 9092
	public Transform Spine1;

	// Token: 0x04002385 RID: 9093
	public Transform Spine2;

	// Token: 0x04002386 RID: 9094
	public Transform Spine3;

	// Token: 0x04002387 RID: 9095
	public Transform Neck;

	// Token: 0x04002388 RID: 9096
	public Transform Head;

	// Token: 0x04002389 RID: 9097
	public Transform RightUpLeg;

	// Token: 0x0400238A RID: 9098
	public Transform RightLeg;

	// Token: 0x0400238B RID: 9099
	public Transform RightFoot;

	// Token: 0x0400238C RID: 9100
	public Transform LeftUpLeg;

	// Token: 0x0400238D RID: 9101
	public Transform LeftLeg;

	// Token: 0x0400238E RID: 9102
	public Transform LeftFoot;

	// Token: 0x0400238F RID: 9103
	public float X;
}
