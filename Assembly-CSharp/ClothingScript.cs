using System;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class ClothingScript : MonoBehaviour
{
	// Token: 0x060017F1 RID: 6129 RVA: 0x000C13CC File Offset: 0x000BF7CC
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000C13E4 File Offset: 0x000BF7E4
	private void Update()
	{
		if (this.CanPickUp)
		{
			if (this.Yandere.Bloodiness == 0f)
			{
				this.CanPickUp = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Yandere.Bloodiness > 0f)
		{
			this.CanPickUp = true;
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Bloodiness = 0f;
			UnityEngine.Object.Instantiate<GameObject>(this.FoldedUniform, base.transform.position + Vector3.up, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001858 RID: 6232
	public YandereScript Yandere;

	// Token: 0x04001859 RID: 6233
	public PromptScript Prompt;

	// Token: 0x0400185A RID: 6234
	public GameObject FoldedUniform;

	// Token: 0x0400185B RID: 6235
	public bool CanPickUp;
}
