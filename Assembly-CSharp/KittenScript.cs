using System;
using UnityEngine;

// Token: 0x02000447 RID: 1095
public class KittenScript : MonoBehaviour
{
	// Token: 0x06001D57 RID: 7511 RVA: 0x00112C7A File Offset: 0x0011107A
	private void Start()
	{
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x00112C7C File Offset: 0x0011107C
	private void Update()
	{
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x00112C7E File Offset: 0x0011107E
	private void PickRandomAnim()
	{
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x00112C80 File Offset: 0x00111080
	private void LateUpdate()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (!this.Yandere.Aiming)
			{
				Vector3 b = (this.Yandere.Head.transform.position.x >= base.transform.position.x) ? (base.transform.position + base.transform.forward + base.transform.up * 0.139854f) : this.Yandere.Head.transform.position;
				this.Target.position = Vector3.Lerp(this.Target.position, b, Time.deltaTime * 5f);
				this.Head.transform.LookAt(this.Target);
			}
			else
			{
				this.Head.transform.LookAt(this.Yandere.transform.position + Vector3.up * this.Head.position.y);
			}
		}
	}

	// Token: 0x04002448 RID: 9288
	public YandereScript Yandere;

	// Token: 0x04002449 RID: 9289
	public GameObject Character;

	// Token: 0x0400244A RID: 9290
	public string[] AnimationNames;

	// Token: 0x0400244B RID: 9291
	public Transform Target;

	// Token: 0x0400244C RID: 9292
	public Transform Head;

	// Token: 0x0400244D RID: 9293
	public string CurrentAnim = string.Empty;

	// Token: 0x0400244E RID: 9294
	public string IdleAnim = string.Empty;

	// Token: 0x0400244F RID: 9295
	public bool Wait;

	// Token: 0x04002450 RID: 9296
	public float Timer;
}
