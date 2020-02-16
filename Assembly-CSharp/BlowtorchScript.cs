using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class BlowtorchScript : MonoBehaviour
{
	// Token: 0x06001761 RID: 5985 RVA: 0x000B8791 File Offset: 0x000B6B91
	private void Start()
	{
		this.Flame.localScale = Vector3.zero;
		base.enabled = false;
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000B87AC File Offset: 0x000B6BAC
	private void Update()
	{
		this.Timer = Mathf.MoveTowards(this.Timer, 5f, Time.deltaTime);
		float num = UnityEngine.Random.Range(0.9f, 1f);
		this.Flame.localScale = new Vector3(num, num, num);
		if (this.Timer == 5f)
		{
			this.Flame.localScale = Vector3.zero;
			this.Yandere.Cauterizing = false;
			this.Yandere.CanMove = true;
			base.enabled = false;
			base.GetComponent<AudioSource>().Stop();
			this.Timer = 0f;
		}
	}

	// Token: 0x040016E4 RID: 5860
	public YandereScript Yandere;

	// Token: 0x040016E5 RID: 5861
	public RagdollScript Corpse;

	// Token: 0x040016E6 RID: 5862
	public PickUpScript PickUp;

	// Token: 0x040016E7 RID: 5863
	public PromptScript Prompt;

	// Token: 0x040016E8 RID: 5864
	public Transform Flame;

	// Token: 0x040016E9 RID: 5865
	public float Timer;
}
