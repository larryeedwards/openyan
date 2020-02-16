using System;
using UnityEngine;

// Token: 0x02000406 RID: 1030
public class GloveScript : MonoBehaviour
{
	// Token: 0x06001C50 RID: 7248 RVA: 0x000FCE10 File Offset: 0x000FB210
	private void Start()
	{
		YandereScript component = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		Physics.IgnoreCollision(component.GetComponent<Collider>(), this.MyCollider);
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x000FCE7C File Offset: 0x000FB27C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			base.transform.parent = this.Prompt.Yandere.transform;
			base.transform.localPosition = new Vector3(0f, 1f, 0.25f);
			this.Prompt.Yandere.Gloves = this;
			this.Prompt.Yandere.WearGloves();
			base.gameObject.SetActive(false);
		}
		this.Prompt.HideButton[0] = (this.Prompt.Yandere.Schoolwear != 1 || this.Prompt.Yandere.ClubAttire);
	}

	// Token: 0x04002065 RID: 8293
	public PromptScript Prompt;

	// Token: 0x04002066 RID: 8294
	public PickUpScript PickUp;

	// Token: 0x04002067 RID: 8295
	public Collider MyCollider;

	// Token: 0x04002068 RID: 8296
	public Projector Blood;
}
