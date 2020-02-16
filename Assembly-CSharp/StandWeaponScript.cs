using System;
using UnityEngine;

// Token: 0x02000521 RID: 1313
public class StandWeaponScript : MonoBehaviour
{
	// Token: 0x0600204F RID: 8271 RVA: 0x001509E8 File Offset: 0x0014EDE8
	private void Update()
	{
		if (this.Prompt.enabled)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.MoveToStand();
			}
		}
		else
		{
			base.transform.Rotate(Vector3.forward * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.right * (Time.deltaTime * this.RotationSpeed));
			base.transform.Rotate(Vector3.up * (Time.deltaTime * this.RotationSpeed));
		}
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x00150A90 File Offset: 0x0014EE90
	private void MoveToStand()
	{
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Prompt.MyCollider.enabled = false;
		this.Stand.Weapons++;
		base.transform.parent = this.Stand.Hands[this.Stand.Weapons];
		base.transform.localPosition = new Vector3(-0.277f, 0f, 0f);
	}

	// Token: 0x04002D3D RID: 11581
	public PromptScript Prompt;

	// Token: 0x04002D3E RID: 11582
	public StandScript Stand;

	// Token: 0x04002D3F RID: 11583
	public float RotationSpeed;
}
