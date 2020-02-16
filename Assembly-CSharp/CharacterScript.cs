using System;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class CharacterScript : MonoBehaviour
{
	// Token: 0x060017C6 RID: 6086 RVA: 0x000BDB68 File Offset: 0x000BBF68
	private void SetAnimations()
	{
		Animation component = base.GetComponent<Animation>();
		component["f02_yanderePose_00"].layer = 1;
		component["f02_yanderePose_00"].weight = 0f;
		component.Play("f02_yanderePose_00");
		component["f02_shy_00"].layer = 2;
		component["f02_shy_00"].weight = 0f;
		component.Play("f02_shy_00");
		component["f02_fist_00"].layer = 3;
		component["f02_fist_00"].weight = 0f;
		component.Play("f02_fist_00");
		component["f02_mopping_00"].layer = 4;
		component["f02_mopping_00"].weight = 0f;
		component["f02_mopping_00"].speed = 2f;
		component.Play("f02_mopping_00");
		component["f02_carry_00"].layer = 5;
		component["f02_carry_00"].weight = 0f;
		component.Play("f02_carry_00");
		component["f02_mopCarry_00"].layer = 6;
		component["f02_mopCarry_00"].weight = 0f;
		component.Play("f02_mopCarry_00");
		component["f02_bucketCarry_00"].layer = 7;
		component["f02_bucketCarry_00"].weight = 0f;
		component.Play("f02_bucketCarry_00");
		component["f02_cameraPose_00"].layer = 8;
		component["f02_cameraPose_00"].weight = 0f;
		component.Play("f02_cameraPose_00");
		component["f02_dipping_00"].speed = 2f;
		component["f02_cameraPose_00"].weight = 0f;
		component["f02_shy_00"].weight = 0f;
	}

	// Token: 0x040017D6 RID: 6102
	public Transform RightBreast;

	// Token: 0x040017D7 RID: 6103
	public Transform LeftBreast;

	// Token: 0x040017D8 RID: 6104
	public Transform ItemParent;

	// Token: 0x040017D9 RID: 6105
	public Transform PelvisRoot;

	// Token: 0x040017DA RID: 6106
	public Transform RightEye;

	// Token: 0x040017DB RID: 6107
	public Transform LeftEye;

	// Token: 0x040017DC RID: 6108
	public Transform Head;

	// Token: 0x040017DD RID: 6109
	public Transform[] Spine;

	// Token: 0x040017DE RID: 6110
	public Transform[] Arm;

	// Token: 0x040017DF RID: 6111
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040017E0 RID: 6112
	public Renderer RightYandereEye;

	// Token: 0x040017E1 RID: 6113
	public Renderer LeftYandereEye;
}
