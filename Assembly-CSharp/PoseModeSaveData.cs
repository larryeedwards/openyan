using System;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
[Serializable]
public class PoseModeSaveData
{
	// Token: 0x06001F89 RID: 8073 RVA: 0x00142068 File Offset: 0x00140468
	public static PoseModeSaveData ReadFromGlobals()
	{
		return new PoseModeSaveData
		{
			posePosition = PoseModeGlobals.PosePosition,
			poseRotation = PoseModeGlobals.PoseRotation,
			poseScale = PoseModeGlobals.PoseScale
		};
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x0014209D File Offset: 0x0014049D
	public static void WriteToGlobals(PoseModeSaveData data)
	{
		PoseModeGlobals.PosePosition = data.posePosition;
		PoseModeGlobals.PoseRotation = data.poseRotation;
		PoseModeGlobals.PoseScale = data.poseScale;
	}

	// Token: 0x04002AE8 RID: 10984
	public Vector3 posePosition = default(Vector3);

	// Token: 0x04002AE9 RID: 10985
	public Vector3 poseRotation = default(Vector3);

	// Token: 0x04002AEA RID: 10986
	public Vector3 poseScale = default(Vector3);
}
