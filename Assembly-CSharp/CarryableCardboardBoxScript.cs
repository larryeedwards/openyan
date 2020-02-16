using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class CarryableCardboardBoxScript : MonoBehaviour
{
	// Token: 0x060017B7 RID: 6071 RVA: 0x000BCE24 File Offset: 0x000BB224
	private void Update()
	{
		if (!this.Closed)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Insert Box Cutter";
				this.MyRenderer.mesh = this.ClosedMesh;
				this.Prompt.HideButton[0] = true;
				this.Closed = true;
			}
		}
		else if (this.MyCutter == null)
		{
			this.Prompt.HideButton[0] = true;
			if (this.Prompt.Yandere.Armed)
			{
				if (this.Prompt.Yandere.EquippedWeapon.WeaponID == 5 && !this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
				{
					this.Prompt.HideButton[0] = false;
					if (this.Prompt.Circle[0].fillAmount == 0f)
					{
						this.MyCutter = this.Prompt.Yandere.EquippedWeapon;
						Physics.IgnoreCollision(base.GetComponent<Collider>(), this.MyCutter.MyCollider);
						this.Prompt.Yandere.DropTimer[this.Prompt.Yandere.Equipped] = 0.5f;
						this.Prompt.Yandere.DropWeapon(this.Prompt.Yandere.Equipped);
						this.MyCutter.MyRigidbody.useGravity = false;
						this.MyCutter.MyRigidbody.isKinematic = true;
						this.MyCutter.MyCollider.isTrigger = true;
						this.MyCutter.transform.parent = base.transform;
						this.MyCutter.transform.localPosition = new Vector3(0f, 0.24f, 0f);
						this.MyCutter.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
						this.MyCutter.Prompt.Hide();
						this.MyCutter.Prompt.enabled = false;
						this.MyCutter.enabled = false;
						this.MyCutter.gameObject.SetActive(true);
						this.Prompt.HideButton[0] = true;
						this.Prompt.HideButton[3] = false;
						this.PickUp.StuckBoxCutter = this.MyCutter;
						this.PickUp.enabled = true;
					}
				}
				else
				{
					this.Prompt.HideButton[0] = true;
				}
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}
		else if (this.MyCutter.transform.parent != base.transform)
		{
			this.MyCutter = null;
		}
	}

	// Token: 0x040017B4 RID: 6068
	public WeaponScript MyCutter;

	// Token: 0x040017B5 RID: 6069
	public PickUpScript PickUp;

	// Token: 0x040017B6 RID: 6070
	public PromptScript Prompt;

	// Token: 0x040017B7 RID: 6071
	public MeshFilter MyRenderer;

	// Token: 0x040017B8 RID: 6072
	public Mesh ClosedMesh;

	// Token: 0x040017B9 RID: 6073
	public bool Closed;
}
