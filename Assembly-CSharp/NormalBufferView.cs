using System;
using UnityEngine;

// Token: 0x020005BA RID: 1466
public class NormalBufferView : MonoBehaviour
{
	// Token: 0x0600234E RID: 9038 RVA: 0x001BF593 File Offset: 0x001BD993
	public void ApplyNormalView()
	{
		this.camera.SetReplacementShader(this.normalShader, "RenderType");
	}

	// Token: 0x0600234F RID: 9039 RVA: 0x001BF5AB File Offset: 0x001BD9AB
	public void DisableNormalView()
	{
		this.camera.ResetReplacementShader();
	}

	// Token: 0x04003D1F RID: 15647
	[SerializeField]
	private Camera camera;

	// Token: 0x04003D20 RID: 15648
	[SerializeField]
	private Shader normalShader;
}
