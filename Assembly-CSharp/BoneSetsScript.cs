using System;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class BoneSetsScript : MonoBehaviour
{
	// Token: 0x06001769 RID: 5993 RVA: 0x000B8AA6 File Offset: 0x000B6EA6
	private void Start()
	{
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000B8AA8 File Offset: 0x000B6EA8
	private void Update()
	{
		if (this.Head != null)
		{
			this.RightArm.localPosition = this.RightArmPosition;
			this.RightArm.localEulerAngles = this.RightArmRotation;
			this.LeftArm.localPosition = this.LeftArmPosition;
			this.LeftArm.localEulerAngles = this.LeftArmRotation;
			this.RightLeg.localPosition = this.RightLegPosition;
			this.RightLeg.localEulerAngles = this.RightLegRotation;
			this.LeftLeg.localPosition = this.LeftLegPosition;
			this.LeftLeg.localEulerAngles = this.LeftLegRotation;
			this.Head.localPosition = this.HeadPosition;
		}
		base.enabled = false;
	}

	// Token: 0x040016F7 RID: 5879
	public Transform[] BoneSet1;

	// Token: 0x040016F8 RID: 5880
	public Transform[] BoneSet2;

	// Token: 0x040016F9 RID: 5881
	public Transform[] BoneSet3;

	// Token: 0x040016FA RID: 5882
	public Transform[] BoneSet4;

	// Token: 0x040016FB RID: 5883
	public Transform[] BoneSet5;

	// Token: 0x040016FC RID: 5884
	public Transform[] BoneSet6;

	// Token: 0x040016FD RID: 5885
	public Transform[] BoneSet7;

	// Token: 0x040016FE RID: 5886
	public Transform[] BoneSet8;

	// Token: 0x040016FF RID: 5887
	public Transform[] BoneSet9;

	// Token: 0x04001700 RID: 5888
	public Vector3[] BoneSet1Pos;

	// Token: 0x04001701 RID: 5889
	public Vector3[] BoneSet2Pos;

	// Token: 0x04001702 RID: 5890
	public Vector3[] BoneSet3Pos;

	// Token: 0x04001703 RID: 5891
	public Vector3[] BoneSet4Pos;

	// Token: 0x04001704 RID: 5892
	public Vector3[] BoneSet5Pos;

	// Token: 0x04001705 RID: 5893
	public Vector3[] BoneSet6Pos;

	// Token: 0x04001706 RID: 5894
	public Vector3[] BoneSet7Pos;

	// Token: 0x04001707 RID: 5895
	public Vector3[] BoneSet8Pos;

	// Token: 0x04001708 RID: 5896
	public Vector3[] BoneSet9Pos;

	// Token: 0x04001709 RID: 5897
	public float Timer;

	// Token: 0x0400170A RID: 5898
	public Transform RightArm;

	// Token: 0x0400170B RID: 5899
	public Transform LeftArm;

	// Token: 0x0400170C RID: 5900
	public Transform RightLeg;

	// Token: 0x0400170D RID: 5901
	public Transform LeftLeg;

	// Token: 0x0400170E RID: 5902
	public Transform Head;

	// Token: 0x0400170F RID: 5903
	public Vector3 RightArmPosition;

	// Token: 0x04001710 RID: 5904
	public Vector3 RightArmRotation;

	// Token: 0x04001711 RID: 5905
	public Vector3 LeftArmPosition;

	// Token: 0x04001712 RID: 5906
	public Vector3 LeftArmRotation;

	// Token: 0x04001713 RID: 5907
	public Vector3 RightLegPosition;

	// Token: 0x04001714 RID: 5908
	public Vector3 RightLegRotation;

	// Token: 0x04001715 RID: 5909
	public Vector3 LeftLegPosition;

	// Token: 0x04001716 RID: 5910
	public Vector3 LeftLegRotation;

	// Token: 0x04001717 RID: 5911
	public Vector3 HeadPosition;
}
