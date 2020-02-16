using System;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public class PortraitScript : MonoBehaviour
{
	// Token: 0x06001EA5 RID: 7845 RVA: 0x0013045B File Offset: 0x0012E85B
	private void Start()
	{
		this.StudentObject[1].SetActive(false);
		this.StudentObject[2].SetActive(false);
		this.Selected = 1;
		this.UpdateHair();
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x00130488 File Offset: 0x0012E888
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.StudentObject[0].SetActive(true);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(false);
			this.Selected = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(true);
			this.StudentObject[2].SetActive(false);
			this.Selected = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.StudentObject[0].SetActive(false);
			this.StudentObject[1].SetActive(false);
			this.StudentObject[2].SetActive(true);
			this.Selected = 3;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.CurrentHair++;
			if (this.CurrentHair > this.HairSet1.Length - 1)
			{
				this.CurrentHair = 0;
			}
			this.UpdateHair();
		}
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x00130594 File Offset: 0x0012E994
	private void UpdateHair()
	{
		Texture mainTexture = this.HairSet2[this.CurrentHair];
		this.Renderer1.materials[0].mainTexture = mainTexture;
		this.Renderer1.materials[3].mainTexture = mainTexture;
		this.Renderer2.materials[2].mainTexture = mainTexture;
		this.Renderer2.materials[3].mainTexture = mainTexture;
		this.Renderer3.materials[0].mainTexture = mainTexture;
		this.Renderer3.materials[1].mainTexture = mainTexture;
	}

	// Token: 0x0400282B RID: 10283
	public GameObject[] StudentObject;

	// Token: 0x0400282C RID: 10284
	public Renderer Renderer1;

	// Token: 0x0400282D RID: 10285
	public Renderer Renderer2;

	// Token: 0x0400282E RID: 10286
	public Renderer Renderer3;

	// Token: 0x0400282F RID: 10287
	public Texture[] HairSet1;

	// Token: 0x04002830 RID: 10288
	public Texture[] HairSet2;

	// Token: 0x04002831 RID: 10289
	public Texture[] HairSet3;

	// Token: 0x04002832 RID: 10290
	public int Selected;

	// Token: 0x04002833 RID: 10291
	public int CurrentHair;
}
