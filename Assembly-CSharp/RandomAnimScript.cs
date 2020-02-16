using System;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
public class RandomAnimScript : MonoBehaviour
{
	// Token: 0x06001F09 RID: 7945 RVA: 0x0013C688 File Offset: 0x0013AA88
	private void Start()
	{
		this.PickRandomAnim();
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x0013C6A4 File Offset: 0x0013AAA4
	private void Update()
	{
		AnimationState animationState = base.GetComponent<Animation>()[this.CurrentAnim];
		if (animationState.time >= animationState.length)
		{
			this.PickRandomAnim();
		}
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x0013C6DA File Offset: 0x0013AADA
	private void PickRandomAnim()
	{
		this.CurrentAnim = this.AnimationNames[UnityEngine.Random.Range(0, this.AnimationNames.Length)];
		base.GetComponent<Animation>().CrossFade(this.CurrentAnim);
	}

	// Token: 0x04002977 RID: 10615
	public string[] AnimationNames;

	// Token: 0x04002978 RID: 10616
	public string CurrentAnim = string.Empty;
}
