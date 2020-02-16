using System;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class RuntimeAnimRetarget : MonoBehaviour
{
	// Token: 0x06001F56 RID: 8022 RVA: 0x001404F8 File Offset: 0x0013E8F8
	private void Start()
	{
		Debug.Log(this.Source.name);
		this.SourceSkelNodes = this.Source.GetComponentsInChildren<Component>();
		this.TargetSkelNodes = this.Target.GetComponentsInChildren<Component>();
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0014052C File Offset: 0x0013E92C
	private void LateUpdate()
	{
		this.TargetSkelNodes[1].transform.localPosition = this.SourceSkelNodes[1].transform.localPosition;
		for (int i = 0; i < 154; i++)
		{
			this.TargetSkelNodes[i].transform.localRotation = this.SourceSkelNodes[i].transform.localRotation;
		}
	}

	// Token: 0x04002A7B RID: 10875
	public GameObject Source;

	// Token: 0x04002A7C RID: 10876
	public GameObject Target;

	// Token: 0x04002A7D RID: 10877
	private Component[] SourceSkelNodes;

	// Token: 0x04002A7E RID: 10878
	private Component[] TargetSkelNodes;
}
