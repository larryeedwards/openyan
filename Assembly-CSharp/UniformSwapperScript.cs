using System;
using UnityEngine;

// Token: 0x02000561 RID: 1377
public class UniformSwapperScript : MonoBehaviour
{
	// Token: 0x060021D8 RID: 8664 RVA: 0x0019A954 File Offset: 0x00198D54
	private void Start()
	{
		int maleUniform = StudentGlobals.MaleUniform;
		this.MyRenderer.sharedMesh = this.UniformMeshes[maleUniform];
		Texture mainTexture = this.UniformTextures[maleUniform];
		if (maleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (maleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (maleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (maleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
		this.MyRenderer.materials[this.SkinID].mainTexture = mainTexture;
		this.MyRenderer.materials[this.UniformID].mainTexture = mainTexture;
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x0019AA91 File Offset: 0x00198E91
	private void LateUpdate()
	{
		if (this.LookTarget != null)
		{
			this.Head.LookAt(this.LookTarget);
		}
	}

	// Token: 0x04003747 RID: 14151
	public Texture[] UniformTextures;

	// Token: 0x04003748 RID: 14152
	public Mesh[] UniformMeshes;

	// Token: 0x04003749 RID: 14153
	public Texture FaceTexture;

	// Token: 0x0400374A RID: 14154
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400374B RID: 14155
	public int UniformID;

	// Token: 0x0400374C RID: 14156
	public int FaceID;

	// Token: 0x0400374D RID: 14157
	public int SkinID;

	// Token: 0x0400374E RID: 14158
	public Transform LookTarget;

	// Token: 0x0400374F RID: 14159
	public Transform Head;
}
