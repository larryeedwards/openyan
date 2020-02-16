using System;
using UnityEngine;

// Token: 0x020003D6 RID: 982
public class FollowSkirtScript : MonoBehaviour
{
	// Token: 0x060019A7 RID: 6567 RVA: 0x000EFF64 File Offset: 0x000EE364
	private void LateUpdate()
	{
		this.SkirtHips.position = this.TargetSkirtHips.position;
		for (int i = 0; i < 3; i++)
		{
			this.SkirtFront[i].position = this.TargetSkirtFront[i].position;
			this.SkirtFront[i].rotation = this.TargetSkirtFront[i].rotation;
			this.SkirtBack[i].position = this.TargetSkirtBack[i].position;
			this.SkirtBack[i].rotation = this.TargetSkirtBack[i].rotation;
			this.SkirtRight[i].position = this.TargetSkirtRight[i].position;
			this.SkirtRight[i].rotation = this.TargetSkirtRight[i].rotation;
			this.SkirtLeft[i].position = this.TargetSkirtLeft[i].position;
			this.SkirtLeft[i].rotation = this.TargetSkirtLeft[i].rotation;
		}
	}

	// Token: 0x04001EB2 RID: 7858
	public Transform[] TargetSkirtFront;

	// Token: 0x04001EB3 RID: 7859
	public Transform[] TargetSkirtBack;

	// Token: 0x04001EB4 RID: 7860
	public Transform[] TargetSkirtRight;

	// Token: 0x04001EB5 RID: 7861
	public Transform[] TargetSkirtLeft;

	// Token: 0x04001EB6 RID: 7862
	public Transform[] SkirtFront;

	// Token: 0x04001EB7 RID: 7863
	public Transform[] SkirtBack;

	// Token: 0x04001EB8 RID: 7864
	public Transform[] SkirtRight;

	// Token: 0x04001EB9 RID: 7865
	public Transform[] SkirtLeft;

	// Token: 0x04001EBA RID: 7866
	public Transform TargetSkirtHips;

	// Token: 0x04001EBB RID: 7867
	public Transform SkirtHips;
}
