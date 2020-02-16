using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class RiggedAccessoryAttacher : MonoBehaviour
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x000657F8 File Offset: 0x00063BF8
	public void Start()
	{
		this.Initialized = true;
		if (this.PantyID == 99)
		{
			this.PantyID = PlayerGlobals.PantiesEquipped;
		}
		if (this.CookingClub)
		{
			if (this.Student.Male)
			{
				this.accessory = GameObject.Find("MaleCookingApron");
			}
			else
			{
				this.accessory = GameObject.Find("FemaleCookingApron");
			}
		}
		else if (this.ArtClub)
		{
			if (this.Student.Male)
			{
				this.accessory = GameObject.Find("PainterApron");
				this.accessoryMaterials = this.painterMaterials;
			}
			else
			{
				this.accessory = GameObject.Find("PainterApronFemale");
				this.accessoryMaterials = this.painterMaterials;
			}
		}
		else if (this.Gentle)
		{
			this.accessory = GameObject.Find("GentleEyes");
			this.accessoryMaterials = this.defaultMaterials;
		}
		else
		{
			if (this.ID == 1)
			{
				this.accessory = GameObject.Find("LabcoatFemale");
			}
			if (this.ID == 2)
			{
				this.accessory = GameObject.Find("LabcoatMale");
			}
			if (this.ID == 26)
			{
				this.accessory = GameObject.Find("OkaBlazer");
				this.accessoryMaterials = this.okaMaterials;
			}
			if (this.ID == 100)
			{
				this.accessory = this.Panties[this.PantyID];
				this.accessoryMaterials[0] = this.PantyMaterials[this.PantyID];
			}
		}
		this.AttachAccessory();
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0006598E File Offset: 0x00063D8E
	public void AttachAccessory()
	{
		this.AddLimb(this.accessory, this.root, this.accessoryMaterials);
		if (this.ID == 100)
		{
			this.newRenderer.updateWhenOffscreen = true;
		}
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x000659C1 File Offset: 0x00063DC1
	public void RemoveAccessory()
	{
		UnityEngine.Object.Destroy(this.newRenderer);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x000659D0 File Offset: 0x00063DD0
	private void AddLimb(GameObject bonedObj, GameObject rootObj, Material[] bonedObjMaterials)
	{
		SkinnedMeshRenderer[] componentsInChildren = bonedObj.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (SkinnedMeshRenderer thisRenderer in componentsInChildren)
		{
			this.ProcessBonedObject(thisRenderer, rootObj, bonedObjMaterials);
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00065A0C File Offset: 0x00063E0C
	private void ProcessBonedObject(SkinnedMeshRenderer thisRenderer, GameObject rootObj, Material[] thisRendererMaterials)
	{
		GameObject gameObject = new GameObject(thisRenderer.gameObject.name);
		gameObject.transform.parent = rootObj.transform;
		gameObject.layer = rootObj.layer;
		gameObject.AddComponent<SkinnedMeshRenderer>();
		this.newRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
		Transform[] array = new Transform[thisRenderer.bones.Length];
		for (int i = 0; i < thisRenderer.bones.Length; i++)
		{
			array[i] = this.FindChildByName(thisRenderer.bones[i].name, rootObj.transform);
		}
		this.newRenderer.bones = array;
		this.newRenderer.sharedMesh = thisRenderer.sharedMesh;
		if (thisRendererMaterials == null)
		{
			this.newRenderer.materials = thisRenderer.sharedMaterials;
		}
		else
		{
			this.newRenderer.materials = thisRendererMaterials;
		}
		if (this.UpdateBounds)
		{
			this.newRenderer.updateWhenOffscreen = true;
		}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00065AFC File Offset: 0x00063EFC
	private Transform FindChildByName(string thisName, Transform thisGameObj)
	{
		if (thisGameObj.name == thisName)
		{
			return thisGameObj.transform;
		}
		IEnumerator enumerator = thisGameObj.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform thisGameObj2 = (Transform)obj;
				Transform transform = this.FindChildByName(thisName, thisGameObj2);
				if (transform)
				{
					return transform;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	// Token: 0x04000A83 RID: 2691
	public StudentScript Student;

	// Token: 0x04000A84 RID: 2692
	public GameObject root;

	// Token: 0x04000A85 RID: 2693
	public GameObject accessory;

	// Token: 0x04000A86 RID: 2694
	public Material[] accessoryMaterials;

	// Token: 0x04000A87 RID: 2695
	public Material[] okaMaterials;

	// Token: 0x04000A88 RID: 2696
	public Material[] ribaruMaterials;

	// Token: 0x04000A89 RID: 2697
	public Material[] defaultMaterials;

	// Token: 0x04000A8A RID: 2698
	public Material[] painterMaterials;

	// Token: 0x04000A8B RID: 2699
	public Material[] painterMaterialsFlipped;

	// Token: 0x04000A8C RID: 2700
	public GameObject[] Panties;

	// Token: 0x04000A8D RID: 2701
	public Material[] PantyMaterials;

	// Token: 0x04000A8E RID: 2702
	public SkinnedMeshRenderer newRenderer;

	// Token: 0x04000A8F RID: 2703
	public bool UpdateBounds;

	// Token: 0x04000A90 RID: 2704
	public bool Initialized;

	// Token: 0x04000A91 RID: 2705
	public bool CookingClub;

	// Token: 0x04000A92 RID: 2706
	public bool ScienceClub;

	// Token: 0x04000A93 RID: 2707
	public bool ArtClub;

	// Token: 0x04000A94 RID: 2708
	public bool Gentle;

	// Token: 0x04000A95 RID: 2709
	public int PantyID;

	// Token: 0x04000A96 RID: 2710
	public int ID;
}
