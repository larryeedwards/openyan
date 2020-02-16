using System;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class CensorBloodScript : MonoBehaviour
{
	// Token: 0x060017BC RID: 6076 RVA: 0x000BD384 File Offset: 0x000BB784
	private void Start()
	{
		if (GameGlobals.CensorBlood)
		{
			this.MyParticles.main.startColor = new Color(1f, 1f, 1f, 1f);
			this.MyParticles.colorOverLifetime.enabled = false;
			this.MyParticles.GetComponent<Renderer>().material.mainTexture = this.Flower;
		}
	}

	// Token: 0x040017BC RID: 6076
	public ParticleSystem MyParticles;

	// Token: 0x040017BD RID: 6077
	public Texture Flower;
}
