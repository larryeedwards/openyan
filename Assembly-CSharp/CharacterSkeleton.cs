using System;
using UnityEngine;

// Token: 0x020003B3 RID: 947
[Serializable]
public class CharacterSkeleton
{
	// Token: 0x17000394 RID: 916
	// (get) Token: 0x06001938 RID: 6456 RVA: 0x000EC570 File Offset: 0x000EA970
	public Transform Head
	{
		get
		{
			return this.head;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06001939 RID: 6457 RVA: 0x000EC578 File Offset: 0x000EA978
	public Transform Neck
	{
		get
		{
			return this.neck;
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x0600193A RID: 6458 RVA: 0x000EC580 File Offset: 0x000EA980
	public Transform Chest
	{
		get
		{
			return this.chest;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x0600193B RID: 6459 RVA: 0x000EC588 File Offset: 0x000EA988
	public Transform Stomach
	{
		get
		{
			return this.stomach;
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x0600193C RID: 6460 RVA: 0x000EC590 File Offset: 0x000EA990
	public Transform Pelvis
	{
		get
		{
			return this.pelvis;
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x0600193D RID: 6461 RVA: 0x000EC598 File Offset: 0x000EA998
	public Transform RightShoulder
	{
		get
		{
			return this.rightShoulder;
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x0600193E RID: 6462 RVA: 0x000EC5A0 File Offset: 0x000EA9A0
	public Transform LeftShoulder
	{
		get
		{
			return this.leftShoulder;
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x0600193F RID: 6463 RVA: 0x000EC5A8 File Offset: 0x000EA9A8
	public Transform RightUpperArm
	{
		get
		{
			return this.rightUpperArm;
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x06001940 RID: 6464 RVA: 0x000EC5B0 File Offset: 0x000EA9B0
	public Transform LeftUpperArm
	{
		get
		{
			return this.leftUpperArm;
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001941 RID: 6465 RVA: 0x000EC5B8 File Offset: 0x000EA9B8
	public Transform RightElbow
	{
		get
		{
			return this.rightElbow;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001942 RID: 6466 RVA: 0x000EC5C0 File Offset: 0x000EA9C0
	public Transform LeftElbow
	{
		get
		{
			return this.leftElbow;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001943 RID: 6467 RVA: 0x000EC5C8 File Offset: 0x000EA9C8
	public Transform RightLowerArm
	{
		get
		{
			return this.rightLowerArm;
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001944 RID: 6468 RVA: 0x000EC5D0 File Offset: 0x000EA9D0
	public Transform LeftLowerArm
	{
		get
		{
			return this.leftLowerArm;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001945 RID: 6469 RVA: 0x000EC5D8 File Offset: 0x000EA9D8
	public Transform RightPalm
	{
		get
		{
			return this.rightPalm;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001946 RID: 6470 RVA: 0x000EC5E0 File Offset: 0x000EA9E0
	public Transform LeftPalm
	{
		get
		{
			return this.leftPalm;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001947 RID: 6471 RVA: 0x000EC5E8 File Offset: 0x000EA9E8
	public Transform RightUpperLeg
	{
		get
		{
			return this.rightUpperLeg;
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001948 RID: 6472 RVA: 0x000EC5F0 File Offset: 0x000EA9F0
	public Transform LeftUpperLeg
	{
		get
		{
			return this.leftUpperLeg;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001949 RID: 6473 RVA: 0x000EC5F8 File Offset: 0x000EA9F8
	public Transform RightKnee
	{
		get
		{
			return this.rightKnee;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x0600194A RID: 6474 RVA: 0x000EC600 File Offset: 0x000EAA00
	public Transform LeftKnee
	{
		get
		{
			return this.leftKnee;
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x0600194B RID: 6475 RVA: 0x000EC608 File Offset: 0x000EAA08
	public Transform RightLowerLeg
	{
		get
		{
			return this.rightLowerLeg;
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x0600194C RID: 6476 RVA: 0x000EC610 File Offset: 0x000EAA10
	public Transform LeftLowerLeg
	{
		get
		{
			return this.leftLowerLeg;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x0600194D RID: 6477 RVA: 0x000EC618 File Offset: 0x000EAA18
	public Transform RightFoot
	{
		get
		{
			return this.rightFoot;
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x0600194E RID: 6478 RVA: 0x000EC620 File Offset: 0x000EAA20
	public Transform LeftFoot
	{
		get
		{
			return this.leftFoot;
		}
	}

	// Token: 0x04001DA1 RID: 7585
	[SerializeField]
	private Transform head;

	// Token: 0x04001DA2 RID: 7586
	[SerializeField]
	private Transform neck;

	// Token: 0x04001DA3 RID: 7587
	[SerializeField]
	private Transform chest;

	// Token: 0x04001DA4 RID: 7588
	[SerializeField]
	private Transform stomach;

	// Token: 0x04001DA5 RID: 7589
	[SerializeField]
	private Transform pelvis;

	// Token: 0x04001DA6 RID: 7590
	[SerializeField]
	private Transform rightShoulder;

	// Token: 0x04001DA7 RID: 7591
	[SerializeField]
	private Transform leftShoulder;

	// Token: 0x04001DA8 RID: 7592
	[SerializeField]
	private Transform rightUpperArm;

	// Token: 0x04001DA9 RID: 7593
	[SerializeField]
	private Transform leftUpperArm;

	// Token: 0x04001DAA RID: 7594
	[SerializeField]
	private Transform rightElbow;

	// Token: 0x04001DAB RID: 7595
	[SerializeField]
	private Transform leftElbow;

	// Token: 0x04001DAC RID: 7596
	[SerializeField]
	private Transform rightLowerArm;

	// Token: 0x04001DAD RID: 7597
	[SerializeField]
	private Transform leftLowerArm;

	// Token: 0x04001DAE RID: 7598
	[SerializeField]
	private Transform rightPalm;

	// Token: 0x04001DAF RID: 7599
	[SerializeField]
	private Transform leftPalm;

	// Token: 0x04001DB0 RID: 7600
	[SerializeField]
	private Transform rightUpperLeg;

	// Token: 0x04001DB1 RID: 7601
	[SerializeField]
	private Transform leftUpperLeg;

	// Token: 0x04001DB2 RID: 7602
	[SerializeField]
	private Transform rightKnee;

	// Token: 0x04001DB3 RID: 7603
	[SerializeField]
	private Transform leftKnee;

	// Token: 0x04001DB4 RID: 7604
	[SerializeField]
	private Transform rightLowerLeg;

	// Token: 0x04001DB5 RID: 7605
	[SerializeField]
	private Transform leftLowerLeg;

	// Token: 0x04001DB6 RID: 7606
	[SerializeField]
	private Transform rightFoot;

	// Token: 0x04001DB7 RID: 7607
	[SerializeField]
	private Transform leftFoot;
}
