using System;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public class YanvaniaZombieScript : MonoBehaviour
{
	// Token: 0x06002343 RID: 9027 RVA: 0x001BE0B8 File Offset: 0x001BC4B8
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, (this.Yanmont.transform.position.x <= base.transform.position.x) ? -90f : 90f, base.transform.eulerAngles.z);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		base.transform.position = new Vector3(base.transform.position.x, -0.63f, base.transform.position.z);
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].speed = 2f;
		component.Play("getup1");
		base.GetComponent<AudioSource>().PlayOneShot(this.RisingSound);
		this.MyRenderer.material.mainTexture = this.Textures[UnityEngine.Random.Range(0, 22)];
		this.MyCollider.enabled = false;
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x001BE200 File Offset: 0x001BC600
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Dying)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 1f)
			{
				if (!this.EffectSpawned)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
					component.PlayOneShot(this.SinkingSound);
					this.EffectSpawned = true;
				}
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime, base.transform.position.z);
				if (base.transform.position.y < -0.4f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
		else
		{
			Animation component2 = this.Character.GetComponent<Animation>();
			if (this.Sink)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime * 0.74f, base.transform.position.z);
				if (base.transform.position.y < -0.63f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else if (this.Walk)
			{
				this.WalkTimer += Time.deltaTime;
				if (this.WalkType == 1)
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed1);
					component2.CrossFade("walk1");
				}
				else
				{
					base.transform.Translate(Vector3.forward * Time.deltaTime * this.WalkSpeed2);
					component2.CrossFade("walk2");
				}
				if (this.WalkTimer > 10f)
				{
					this.SinkNow();
				}
			}
			else
			{
				this.Timer += Time.deltaTime;
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.74f, base.transform.position.z);
					if (base.transform.position.y > 0f)
					{
						base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
					}
				}
				if (this.Timer > 0.85f)
				{
					this.Walk = true;
					this.MyCollider.enabled = true;
					this.WalkType = UnityEngine.Random.Range(1, 3);
				}
			}
			if (base.transform.position.x < this.LeftBoundary)
			{
				base.transform.position = new Vector3(this.LeftBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (base.transform.position.x > this.RightBoundary)
			{
				base.transform.position = new Vector3(this.RightBoundary, base.transform.position.y, base.transform.position.z);
				this.SinkNow();
			}
			if (this.HP <= 0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z), Quaternion.identity);
				component2.Play("die");
				component.PlayOneShot(this.DeathSound);
				this.MyCollider.enabled = false;
				this.Yanmont.EXP += 10f;
				this.Dying = true;
			}
		}
		if (this.HitReactTimer < 1f)
		{
			this.MyRenderer.material.color = new Color(1f, this.HitReactTimer, this.HitReactTimer, 1f);
			this.HitReactTimer += Time.deltaTime * 10f;
			if (this.HitReactTimer >= 1f)
			{
				this.MyRenderer.material.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x001BE750 File Offset: 0x001BCB50
	private void SinkNow()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["getup1"].time = component["getup1"].length;
		component["getup1"].speed = -2f;
		component.Play("getup1");
		AudioSource component2 = base.GetComponent<AudioSource>();
		component2.PlayOneShot(this.SinkingSound);
		UnityEngine.Object.Instantiate<GameObject>(this.ZombieEffect, base.transform.position, Quaternion.identity);
		this.MyCollider.enabled = false;
		this.Sink = true;
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x001BE7EC File Offset: 0x001BCBEC
	private void OnTriggerEnter(Collider other)
	{
		if (!this.Dying)
		{
			if (other.gameObject.tag == "Player")
			{
				this.Yanmont.TakeDamage(5);
			}
			if (other.gameObject.name == "Heart" && this.HitReactTimer >= 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
				AudioSource component = base.GetComponent<AudioSource>();
				component.PlayOneShot(this.HitSound);
				this.HitReactTimer = 0f;
				this.HP -= 20 + (this.Yanmont.Level * 5 - 5);
			}
		}
	}

	// Token: 0x04003CE9 RID: 15593
	public GameObject ZombieEffect;

	// Token: 0x04003CEA RID: 15594
	public GameObject BloodEffect;

	// Token: 0x04003CEB RID: 15595
	public GameObject DeathEffect;

	// Token: 0x04003CEC RID: 15596
	public GameObject HitEffect;

	// Token: 0x04003CED RID: 15597
	public GameObject Character;

	// Token: 0x04003CEE RID: 15598
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003CEF RID: 15599
	public int HP;

	// Token: 0x04003CF0 RID: 15600
	public float WalkSpeed1;

	// Token: 0x04003CF1 RID: 15601
	public float WalkSpeed2;

	// Token: 0x04003CF2 RID: 15602
	public float Damage;

	// Token: 0x04003CF3 RID: 15603
	public float HitReactTimer;

	// Token: 0x04003CF4 RID: 15604
	public float DeathTimer;

	// Token: 0x04003CF5 RID: 15605
	public float WalkTimer;

	// Token: 0x04003CF6 RID: 15606
	public float Timer;

	// Token: 0x04003CF7 RID: 15607
	public int HitReactState;

	// Token: 0x04003CF8 RID: 15608
	public int WalkType;

	// Token: 0x04003CF9 RID: 15609
	public float LeftBoundary;

	// Token: 0x04003CFA RID: 15610
	public float RightBoundary;

	// Token: 0x04003CFB RID: 15611
	public bool EffectSpawned;

	// Token: 0x04003CFC RID: 15612
	public bool Dying;

	// Token: 0x04003CFD RID: 15613
	public bool Sink;

	// Token: 0x04003CFE RID: 15614
	public bool Walk;

	// Token: 0x04003CFF RID: 15615
	public Texture[] Textures;

	// Token: 0x04003D00 RID: 15616
	public Renderer MyRenderer;

	// Token: 0x04003D01 RID: 15617
	public Collider MyCollider;

	// Token: 0x04003D02 RID: 15618
	public AudioClip DeathSound;

	// Token: 0x04003D03 RID: 15619
	public AudioClip HitSound;

	// Token: 0x04003D04 RID: 15620
	public AudioClip RisingSound;

	// Token: 0x04003D05 RID: 15621
	public AudioClip SinkingSound;
}
