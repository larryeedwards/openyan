using System;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class MaskScript : MonoBehaviour
{
	// Token: 0x06001D93 RID: 7571 RVA: 0x001180F4 File Offset: 0x001164F4
	private void Start()
	{
		if (GameGlobals.MasksBanned)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			this.MyFilter.mesh = this.Meshes[this.ID];
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
		base.enabled = false;
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00118158 File Offset: 0x00116558
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.StudentManager.CanAnyoneSeeYandere();
			if (!this.StudentManager.YandereVisible && !this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				component.useGravity = false;
				component.isKinematic = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Prompt.MyCollider.enabled = false;
				base.transform.parent = this.Yandere.Head;
				base.transform.localPosition = new Vector3(0f, 0.033333f, 0.1f);
				base.transform.localEulerAngles = Vector3.zero;
				this.Yandere.Mask = this;
				this.ClubManager.UpdateMasks();
				this.StudentManager.UpdateStudents(0);
			}
			else
			{
				this.Yandere.NotificationManager.CustomText = "Not now. Too suspicious.";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			}
		}
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x001182A4 File Offset: 0x001166A4
	public void Drop()
	{
		this.Prompt.MyCollider.isTrigger = false;
		this.Prompt.MyCollider.enabled = true;
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.useGravity = true;
		component.isKinematic = false;
		this.Prompt.enabled = true;
		base.transform.parent = null;
		this.Yandere.Mask = null;
		this.ClubManager.UpdateMasks();
		this.StudentManager.UpdateStudents(0);
	}

	// Token: 0x0400250B RID: 9483
	public StudentManagerScript StudentManager;

	// Token: 0x0400250C RID: 9484
	public ClubManagerScript ClubManager;

	// Token: 0x0400250D RID: 9485
	public YandereScript Yandere;

	// Token: 0x0400250E RID: 9486
	public PromptScript Prompt;

	// Token: 0x0400250F RID: 9487
	public PickUpScript PickUp;

	// Token: 0x04002510 RID: 9488
	public Projector Blood;

	// Token: 0x04002511 RID: 9489
	public Renderer MyRenderer;

	// Token: 0x04002512 RID: 9490
	public MeshFilter MyFilter;

	// Token: 0x04002513 RID: 9491
	public Texture[] Textures;

	// Token: 0x04002514 RID: 9492
	public Mesh[] Meshes;

	// Token: 0x04002515 RID: 9493
	public int ID;
}
