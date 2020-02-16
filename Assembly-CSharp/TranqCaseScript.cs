using System;
using UnityEngine;

// Token: 0x02000558 RID: 1368
public class TranqCaseScript : MonoBehaviour
{
	// Token: 0x060021BA RID: 8634 RVA: 0x00198967 File Offset: 0x00196D67
	private void Start()
	{
		this.Prompt.enabled = false;
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x00198978 File Offset: 0x00196D78
	private void Update()
	{
		if (this.Yandere.transform.position.x > base.transform.position.x && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.Dragging)
			{
				if (this.Yandere.Ragdoll.GetComponent<RagdollScript>().Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				this.Yandere.Ragdoll.GetComponent<RagdollScript>().TranqCase = this;
				this.VictimClubType = this.Yandere.Ragdoll.GetComponent<RagdollScript>().Student.Club;
				this.VictimID = this.Yandere.Ragdoll.GetComponent<RagdollScript>().StudentID;
				this.Door.Prompt.enabled = true;
				this.Door.enabled = true;
				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}
		if (this.Animate)
		{
			if (this.Open)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 105f, Time.deltaTime * 10f);
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				if (this.Rotation < 1f)
				{
					this.Animate = false;
					this.Rotation = 0f;
				}
			}
			this.Hinge.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		}
	}

	// Token: 0x040036B4 RID: 14004
	public YandereScript Yandere;

	// Token: 0x040036B5 RID: 14005
	public PromptScript Prompt;

	// Token: 0x040036B6 RID: 14006
	public DoorScript Door;

	// Token: 0x040036B7 RID: 14007
	public Transform Hinge;

	// Token: 0x040036B8 RID: 14008
	public bool Occupied;

	// Token: 0x040036B9 RID: 14009
	public bool Open;

	// Token: 0x040036BA RID: 14010
	public int VictimID;

	// Token: 0x040036BB RID: 14011
	public ClubType VictimClubType;

	// Token: 0x040036BC RID: 14012
	public float Rotation;

	// Token: 0x040036BD RID: 14013
	public bool Animate;
}
