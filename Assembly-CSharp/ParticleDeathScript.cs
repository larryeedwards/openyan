using System;
using UnityEngine;

// Token: 0x02000485 RID: 1157
public class ParticleDeathScript : MonoBehaviour
{
	// Token: 0x06001E28 RID: 7720 RVA: 0x00123578 File Offset: 0x00121978
	private void LateUpdate()
	{
		if (this.Particles.isPlaying && this.Particles.particleCount == 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002682 RID: 9858
	public ParticleSystem Particles;
}
