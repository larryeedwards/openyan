using System;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public class SmartphoneScript : MonoBehaviour
{
	// Token: 0x06002020 RID: 8224 RVA: 0x0014E854 File Offset: 0x0014CC54
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.CrushingPhone = true;
			this.Prompt.Yandere.PhoneToCrush = this;
			this.Prompt.Yandere.CanMove = false;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
			this.PhoneCrushingSpot = gameObject.transform;
			this.PhoneCrushingSpot.position = new Vector3(this.PhoneCrushingSpot.position.x, this.Prompt.Yandere.transform.position.y, this.PhoneCrushingSpot.position.z);
			this.PhoneCrushingSpot.LookAt(this.Prompt.Yandere.transform.position);
			this.PhoneCrushingSpot.Translate(Vector3.forward * 0.5f);
		}
	}

	// Token: 0x04002CD2 RID: 11474
	public Transform PhoneCrushingSpot;

	// Token: 0x04002CD3 RID: 11475
	public GameObject EmptyGameObject;

	// Token: 0x04002CD4 RID: 11476
	public Texture SmashedTexture;

	// Token: 0x04002CD5 RID: 11477
	public GameObject PhoneSmash;

	// Token: 0x04002CD6 RID: 11478
	public Renderer MyRenderer;

	// Token: 0x04002CD7 RID: 11479
	public PromptScript Prompt;

	// Token: 0x04002CD8 RID: 11480
	public MeshFilter MyMesh;

	// Token: 0x04002CD9 RID: 11481
	public Mesh SmashedMesh;
}
