using System;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class DemonArmScript : MonoBehaviour
{
	// Token: 0x060018BD RID: 6333 RVA: 0x000DF14C File Offset: 0x000DD54C
	private void Start()
	{
		this.MyAnimation = base.GetComponent<Animation>();
		if (!this.Rising)
		{
			this.MyAnimation[this.IdleAnim].speed = this.AnimSpeed * 0.5f;
		}
		this.MyAnimation[this.AttackAnim].speed = 0f;
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x000DF1B0 File Offset: 0x000DD5B0
	private void Update()
	{
		if (!this.Rising)
		{
			if (!this.Attacking)
			{
				this.MyAnimation.CrossFade(this.IdleAnim);
			}
			else
			{
				this.AnimTime += 0.0166666675f;
				this.MyAnimation[this.AttackAnim].time = this.AnimTime;
				if (!this.Attacked)
				{
					if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.15f)
					{
						this.ClawCollider.enabled = true;
						this.Attacked = true;
					}
				}
				else
				{
					if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length * 0.4f)
					{
						this.ClawCollider.enabled = false;
					}
					if (this.MyAnimation[this.AttackAnim].time >= this.MyAnimation[this.AttackAnim].length)
					{
						this.MyAnimation.CrossFade(this.IdleAnim);
						this.ClawCollider.enabled = false;
						this.Attacking = false;
						this.Attacked = false;
						this.AnimTime = 0f;
					}
				}
			}
		}
		else if (this.MyAnimation[this.AttackAnim].time > this.MyAnimation[this.AttackAnim].length)
		{
			this.Rising = false;
		}
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x000DF358 File Offset: 0x000DD758
	private void OnTriggerEnter(Collider other)
	{
		StudentScript component = other.gameObject.GetComponent<StudentScript>();
		if (component != null && component.StudentID > 1)
		{
			AudioSource component2 = base.GetComponent<AudioSource>();
			component2.clip = this.Whoosh;
			component2.pitch = UnityEngine.Random.Range(-0.9f, 1.1f);
			component2.Play();
			base.GetComponent<Animation>().CrossFade(this.AttackAnim);
			this.Attacking = true;
		}
	}

	// Token: 0x04001C2C RID: 7212
	public GameObject DismembermentCollider;

	// Token: 0x04001C2D RID: 7213
	public Animation MyAnimation;

	// Token: 0x04001C2E RID: 7214
	public Collider ClawCollider;

	// Token: 0x04001C2F RID: 7215
	public bool Attacking;

	// Token: 0x04001C30 RID: 7216
	public bool Attacked;

	// Token: 0x04001C31 RID: 7217
	public bool Rising = true;

	// Token: 0x04001C32 RID: 7218
	public string IdleAnim = "DemonArmIdle";

	// Token: 0x04001C33 RID: 7219
	public string AttackAnim = "DemonArmAttack";

	// Token: 0x04001C34 RID: 7220
	public AudioClip Whoosh;

	// Token: 0x04001C35 RID: 7221
	public float AnimSpeed = 1f;

	// Token: 0x04001C36 RID: 7222
	public float AnimTime;
}
