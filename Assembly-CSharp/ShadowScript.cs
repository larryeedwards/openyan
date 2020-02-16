using System;
using UnityEngine;

// Token: 0x02000506 RID: 1286
public class ShadowScript : MonoBehaviour
{
	// Token: 0x06001FEE RID: 8174 RVA: 0x00148240 File Offset: 0x00146640
	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = this.Foot.position;
		position.x = position2.x;
		position.z = position2.z;
		base.transform.position = position;
	}

	// Token: 0x04002C0F RID: 11279
	public Transform Foot;
}
