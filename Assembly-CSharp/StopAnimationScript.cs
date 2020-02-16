using System;
using UnityEngine;

// Token: 0x02000524 RID: 1316
public class StopAnimationScript : MonoBehaviour
{
	// Token: 0x06002059 RID: 8281 RVA: 0x00151A0E File Offset: 0x0014FE0E
	private void Start()
	{
		this.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Anim = base.GetComponent<Animation>();
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x00151A34 File Offset: 0x0014FE34
	private void Update()
	{
		if (this.StudentManager.DisableFarAnims)
		{
			if (Vector3.Distance(this.Yandere.position, base.transform.position) > 15f)
			{
				if (this.Anim.enabled)
				{
					this.Anim.enabled = false;
				}
			}
			else if (!this.Anim.enabled)
			{
				this.Anim.enabled = true;
			}
		}
		else if (!this.Anim.enabled)
		{
			this.Anim.enabled = true;
		}
	}

	// Token: 0x04002D53 RID: 11603
	public StudentManagerScript StudentManager;

	// Token: 0x04002D54 RID: 11604
	public Transform Yandere;

	// Token: 0x04002D55 RID: 11605
	private Animation Anim;
}
