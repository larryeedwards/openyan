using System;
using UnityEngine;

// Token: 0x0200050E RID: 1294
public class SkinnedMeshUpdater : MonoBehaviour
{
	// Token: 0x06002015 RID: 8213 RVA: 0x0014DB0F File Offset: 0x0014BF0F
	public void Start()
	{
		this.GlassesCheck();
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0014DB18 File Offset: 0x0014BF18
	public void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.TransformEffect, this.Prompt.Yandere.Hips.position, Quaternion.identity);
			this.Prompt.Yandere.CharacterAnimation.Play(this.Prompt.Yandere.IdleAnim);
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.Egg = true;
			this.BreastR.name = "RightBreast";
			this.BreastL.name = "LeftBreast";
			this.Timer = 1f;
			this.ID++;
			if (this.ID == this.Characters.Length)
			{
				this.ID = 1;
			}
			this.Prompt.Yandere.Hairstyle = 120 + this.ID;
			this.Prompt.Yandere.UpdateHair();
			this.GlassesCheck();
			this.UpdateSkin();
		}
		if (this.Timer > 0f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f)
			{
				this.Prompt.Yandere.CanMove = true;
			}
		}
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0014DC84 File Offset: 0x0014C084
	public void UpdateSkin()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Characters[this.ID], Vector3.zero, Quaternion.identity);
		this.TempRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		this.UpdateMeshRenderer(this.TempRenderer);
		UnityEngine.Object.Destroy(gameObject);
		this.MyRenderer.materials[0].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[1].mainTexture = this.Bodies[this.ID];
		this.MyRenderer.materials[2].mainTexture = this.Faces[this.ID];
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x0014DD2C File Offset: 0x0014C12C
	private void UpdateMeshRenderer(SkinnedMeshRenderer newMeshRenderer)
	{
		SkinnedMeshUpdater.<UpdateMeshRenderer>c__AnonStorey0 <UpdateMeshRenderer>c__AnonStorey = new SkinnedMeshUpdater.<UpdateMeshRenderer>c__AnonStorey0();
		<UpdateMeshRenderer>c__AnonStorey.newMeshRenderer = newMeshRenderer;
		SkinnedMeshRenderer myRenderer = this.Prompt.Yandere.MyRenderer;
		myRenderer.sharedMesh = <UpdateMeshRenderer>c__AnonStorey.newMeshRenderer.sharedMesh;
		Transform[] componentsInChildren = this.Prompt.Yandere.transform.GetComponentsInChildren<Transform>(true);
		Transform[] array = new Transform[<UpdateMeshRenderer>c__AnonStorey.newMeshRenderer.bones.Length];
		int boneOrder;
		for (boneOrder = 0; boneOrder < <UpdateMeshRenderer>c__AnonStorey.newMeshRenderer.bones.Length; boneOrder++)
		{
			array[boneOrder] = Array.Find<Transform>(componentsInChildren, (Transform c) => c.name == <UpdateMeshRenderer>c__AnonStorey.newMeshRenderer.bones[boneOrder].name);
		}
		myRenderer.bones = array;
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x0014DDFC File Offset: 0x0014C1FC
	private void GlassesCheck()
	{
		this.FumiGlasses.SetActive(false);
		this.NinaGlasses.SetActive(false);
		if (this.ID == 7)
		{
			this.FumiGlasses.SetActive(true);
		}
		else if (this.ID == 8)
		{
			this.NinaGlasses.SetActive(true);
		}
	}

	// Token: 0x04002CAE RID: 11438
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04002CAF RID: 11439
	public GameObject TransformEffect;

	// Token: 0x04002CB0 RID: 11440
	public GameObject[] Characters;

	// Token: 0x04002CB1 RID: 11441
	public PromptScript Prompt;

	// Token: 0x04002CB2 RID: 11442
	public GameObject BreastR;

	// Token: 0x04002CB3 RID: 11443
	public GameObject BreastL;

	// Token: 0x04002CB4 RID: 11444
	public GameObject FumiGlasses;

	// Token: 0x04002CB5 RID: 11445
	public GameObject NinaGlasses;

	// Token: 0x04002CB6 RID: 11446
	private SkinnedMeshRenderer TempRenderer;

	// Token: 0x04002CB7 RID: 11447
	public Texture[] Bodies;

	// Token: 0x04002CB8 RID: 11448
	public Texture[] Faces;

	// Token: 0x04002CB9 RID: 11449
	public float Timer;

	// Token: 0x04002CBA RID: 11450
	public int ID;
}
