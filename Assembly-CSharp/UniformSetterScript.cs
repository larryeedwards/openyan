using System;
using UnityEngine;

// Token: 0x02000560 RID: 1376
public class UniformSetterScript : MonoBehaviour
{
	// Token: 0x060021D4 RID: 8660 RVA: 0x0019A6B4 File Offset: 0x00198AB4
	public void Start()
	{
		if (this.MyRenderer == null)
		{
			this.MyRenderer = base.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();
		}
		if (this.Male)
		{
			this.SetMaleUniform();
		}
		else
		{
			this.SetFemaleUniform();
		}
		if (this.AttachHair)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Hair[this.HairID], base.transform.position, base.transform.rotation);
			this.Head = base.transform.Find("Character/PelvisRoot/Hips/Spine/Spine1/Spine2/Spine3/Neck/Head").transform;
			gameObject.transform.parent = this.Head;
		}
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x0019A774 File Offset: 0x00198B74
	public void SetMaleUniform()
	{
		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2 || StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4 || StudentGlobals.MaleUniform == 5 || StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		this.MyRenderer.materials[this.FaceID].mainTexture = this.SenpaiFace;
		this.MyRenderer.materials[this.SkinID].mainTexture = this.SenpaiSkin;
		this.MyRenderer.materials[this.UniformID].mainTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x0019A880 File Offset: 0x00198C80
	public void SetFemaleUniform()
	{
		this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
		if (this.StudentID == 0)
		{
			this.MyRenderer.materials[2].mainTexture = this.RyobaFace;
		}
		else if (this.StudentID == 1)
		{
			this.MyRenderer.materials[2].mainTexture = this.AyanoFace;
		}
		else
		{
			this.MyRenderer.materials[2].mainTexture = this.OsanaFace;
		}
	}

	// Token: 0x04003734 RID: 14132
	public Texture[] FemaleUniformTextures;

	// Token: 0x04003735 RID: 14133
	public Texture[] MaleUniformTextures;

	// Token: 0x04003736 RID: 14134
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003737 RID: 14135
	public Mesh[] FemaleUniforms;

	// Token: 0x04003738 RID: 14136
	public Mesh[] MaleUniforms;

	// Token: 0x04003739 RID: 14137
	public Texture SenpaiFace;

	// Token: 0x0400373A RID: 14138
	public Texture SenpaiSkin;

	// Token: 0x0400373B RID: 14139
	public Texture RyobaFace;

	// Token: 0x0400373C RID: 14140
	public Texture AyanoFace;

	// Token: 0x0400373D RID: 14141
	public Texture OsanaFace;

	// Token: 0x0400373E RID: 14142
	public int FaceID;

	// Token: 0x0400373F RID: 14143
	public int SkinID;

	// Token: 0x04003740 RID: 14144
	public int UniformID;

	// Token: 0x04003741 RID: 14145
	public int StudentID;

	// Token: 0x04003742 RID: 14146
	public bool AttachHair;

	// Token: 0x04003743 RID: 14147
	public bool Male;

	// Token: 0x04003744 RID: 14148
	public Transform Head;

	// Token: 0x04003745 RID: 14149
	public GameObject[] Hair;

	// Token: 0x04003746 RID: 14150
	public int HairID;
}
