using System;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class RooftopCorpseDisposalScript : MonoBehaviour
{
	// Token: 0x06001F4B RID: 8011 RVA: 0x0013FF59 File Offset: 0x0013E359
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x0013FF70 File Offset: 0x0013E370
	private void Update()
	{
		if (this.MyCollider.bounds.Contains(this.Yandere.transform.position))
		{
			if (this.Yandere.Ragdoll != null)
			{
				if (!this.Yandere.Dropping)
				{
					this.Prompt.enabled = true;
					this.Prompt.transform.position = new Vector3(this.Yandere.transform.position.x, this.Yandere.transform.position.y + 1.66666f, this.Yandere.transform.position.z);
					if (this.Prompt.Circle[0].fillAmount == 0f)
					{
						this.DropSpot.position = new Vector3(this.DropSpot.position.x, this.DropSpot.position.y, this.Yandere.transform.position.z);
						this.Yandere.Character.GetComponent<Animation>().CrossFade((!this.Yandere.Carrying) ? "f02_dragIdle_00" : "f02_carryIdleA_00");
						this.Yandere.DropSpot = this.DropSpot;
						this.Yandere.Dropping = true;
						this.Yandere.CanMove = false;
						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04002A6A RID: 10858
	public YandereScript Yandere;

	// Token: 0x04002A6B RID: 10859
	public PromptScript Prompt;

	// Token: 0x04002A6C RID: 10860
	public Collider MyCollider;

	// Token: 0x04002A6D RID: 10861
	public Transform DropSpot;
}
