using System;
using UnityEngine;

// Token: 0x020005B4 RID: 1460
public class YanvaniaWitchScript : MonoBehaviour
{
	// Token: 0x06002332 RID: 9010 RVA: 0x001BBC58 File Offset: 0x001BA058
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.AttackTimer < 10f)
		{
			this.AttackTimer += Time.deltaTime;
			if (this.AttackTimer > 0.8f && !this.CastSpell)
			{
				this.CastSpell = true;
				UnityEngine.Object.Instantiate<GameObject>(this.BlackHole, base.transform.position + Vector3.up * 3f + Vector3.right * 6f, Quaternion.identity);
				UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.position + Vector3.right * 1.15f, Quaternion.identity);
			}
			if (component["Staff Spell Ground"].time >= component["Staff Spell Ground"].length)
			{
				component.CrossFade("Staff Stance");
				this.Casting = false;
			}
		}
		else if (Vector3.Distance(base.transform.position, this.Yanmont.transform.position) < 5f)
		{
			this.AttackTimer = 0f;
			this.Casting = true;
			this.CastSpell = false;
			component["Staff Spell Ground"].time = 0f;
			component.CrossFade("Staff Spell Ground");
		}
		if (!this.Casting && component["Receive Damage"].time >= component["Receive Damage"].length)
		{
			component.CrossFade("Staff Stance");
		}
		this.HitReactTimer += Time.deltaTime * 10f;
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x001BBE1C File Offset: 0x001BA21C
	private void OnTriggerEnter(Collider other)
	{
		if (this.HP > 0f)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart")
			{
				Animation component = this.Character.GetComponent<Animation>();
				if (!this.Casting)
				{
					component["Receive Damage"].time = 0f;
					component.Play("Receive Damage");
				}
				if (this.HitReactTimer >= 1f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
					this.HitReactTimer = 0f;
					this.HP -= 5f + ((float)this.Yanmont.Level * 5f - 5f);
					AudioSource component2 = base.GetComponent<AudioSource>();
					if (this.HP <= 0f)
					{
						component2.PlayOneShot(this.DeathScream);
						component.Play("Die 2");
						this.Yanmont.EXP += 100f;
						base.enabled = false;
						UnityEngine.Object.Destroy(this.Wall);
					}
					else
					{
						component2.PlayOneShot(this.HitSound);
					}
				}
			}
		}
	}

	// Token: 0x04003C87 RID: 15495
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003C88 RID: 15496
	public GameObject GroundImpact;

	// Token: 0x04003C89 RID: 15497
	public GameObject BlackHole;

	// Token: 0x04003C8A RID: 15498
	public GameObject Character;

	// Token: 0x04003C8B RID: 15499
	public GameObject HitEffect;

	// Token: 0x04003C8C RID: 15500
	public GameObject Wall;

	// Token: 0x04003C8D RID: 15501
	public AudioClip DeathScream;

	// Token: 0x04003C8E RID: 15502
	public AudioClip HitSound;

	// Token: 0x04003C8F RID: 15503
	public float HitReactTimer;

	// Token: 0x04003C90 RID: 15504
	public float AttackTimer = 10f;

	// Token: 0x04003C91 RID: 15505
	public float HP = 100f;

	// Token: 0x04003C92 RID: 15506
	public bool CastSpell;

	// Token: 0x04003C93 RID: 15507
	public bool Casting;
}
