using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class BloodPoolScript : MonoBehaviour
{
	// Token: 0x06001750 RID: 5968 RVA: 0x000B7D80 File Offset: 0x000B6180
	private void Start()
	{
		if (PlayerGlobals.PantiesEquipped == 7 && this.Blood)
		{
			this.TargetSize *= 0.5f;
		}
		if (GameGlobals.CensorBlood)
		{
			this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			this.MyRenderer.material.mainTexture = this.Flower;
		}
		base.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		Vector3 position = base.transform.position;
		if (position.x > 125f || position.x < -125f || position.z > 200f || position.z < -100f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (Application.loadedLevelName == "IntroScene" || Application.loadedLevelName == "NewIntroScene")
		{
			this.MyRenderer.material.SetColor("_TintColor", new Color(0.1f, 0.1f, 0.1f));
		}
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000B7ECC File Offset: 0x000B62CC
	private void Update()
	{
		if (this.Grow)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(this.TargetSize, this.TargetSize, this.TargetSize), Time.deltaTime);
			if (base.transform.localScale.x > this.TargetSize * 0.99f)
			{
				this.Grow = false;
			}
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000B7F46 File Offset: 0x000B6346
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodSpawner")
		{
			this.Grow = true;
		}
	}

	// Token: 0x040016CD RID: 5837
	public float TargetSize;

	// Token: 0x040016CE RID: 5838
	public bool Blood = true;

	// Token: 0x040016CF RID: 5839
	public bool Grow;

	// Token: 0x040016D0 RID: 5840
	public Renderer MyRenderer;

	// Token: 0x040016D1 RID: 5841
	public Texture Flower;
}
