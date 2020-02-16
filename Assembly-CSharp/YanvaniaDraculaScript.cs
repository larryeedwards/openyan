using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005A8 RID: 1448
public class YanvaniaDraculaScript : MonoBehaviour
{
	// Token: 0x0600230A RID: 8970 RVA: 0x001B81D0 File Offset: 0x001B65D0
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["succubus_a_damage_l"].speed = 0.1f;
		component["succubus_a_charm_02"].speed = 2.4f;
		component["succubus_a_charm_03"].speed = 4.66666f;
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x001B8228 File Offset: 0x001B6628
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.Yanmont.Health > 0f)
		{
			if (this.Health > 0f)
			{
				if (base.transform.position.z == 0f)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, (this.Yanmont.transform.position.x <= base.transform.position.x) ? 90f : -90f, base.transform.localEulerAngles.z);
				}
				if (this.NewTeleportEffect == null)
				{
					if (base.transform.position.y > 0f)
					{
						this.Distance = Mathf.Abs(this.Yanmont.transform.position.x) - Mathf.Abs(base.transform.position.x);
						if (Mathf.Abs(this.Distance) < 0.61f && this.Yanmont.FlashTimer == 0f)
						{
							this.Yanmont.TakeDamage(5);
						}
						if (this.AttackID == 0)
						{
							this.AttackID = UnityEngine.Random.Range(1, 3);
							this.TeleportTimer = 5f;
							if (this.AttackID == 1)
							{
								component["succubus_a_charm_02"].time = 0f;
								component.CrossFade("succubus_a_charm_02");
							}
							else
							{
								component["succubus_a_charm_03"].time = 0f;
								component.CrossFade("succubus_a_charm_03");
							}
						}
						else
						{
							if (this.AttackID == 1)
							{
								if (component["succubus_a_charm_02"].time > 4f && this.NewAttack == null)
								{
									this.NewAttack = UnityEngine.Object.Instantiate<GameObject>(this.TripleFireball, this.RightHand.position, Quaternion.identity);
									this.NewAttack.GetComponent<YanvaniaTripleFireballScript>().Dracula = base.transform;
								}
								if (component["succubus_a_charm_02"].time >= component["succubus_a_charm_02"].length)
								{
									component.CrossFade("succubus_a_idle_01");
								}
							}
							else
							{
								if (component["succubus_a_charm_03"].time > 4f && this.NewAttack == null)
								{
									this.NewAttack = UnityEngine.Object.Instantiate<GameObject>(this.DoubleFireball, this.RightHand.position, Quaternion.identity);
									this.NewAttack.GetComponent<YanvaniaDoubleFireballScript>().Dracula = base.transform;
								}
								if (component["succubus_a_charm_03"].time >= component["succubus_a_charm_03"].length)
								{
									component.CrossFade("succubus_a_idle_01");
								}
							}
							this.TeleportTimer -= Time.deltaTime;
						}
					}
					else
					{
						this.TeleportTimer -= Time.deltaTime;
					}
					if (this.TeleportTimer < 0f)
					{
						if (base.transform.position.y < 0f)
						{
							base.transform.position = new Vector3(UnityEngine.Random.Range(-38.5f, -31f), base.transform.position.y, base.transform.position.z);
						}
						this.SpawnTeleportEffect();
					}
				}
				else
				{
					if (this.Shrink)
					{
						base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, new Vector3(0f, 2f, 0f), Time.deltaTime * 0.5f);
						if (base.transform.localScale.x == 0f)
						{
							this.NewTeleportEffect = null;
							this.Shrink = false;
							this.Teleport();
						}
					}
					if (this.Grow)
					{
						base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), Time.deltaTime * 1.5f);
						if (base.transform.localScale.x > 1.49f)
						{
							this.NewTeleportEffect = null;
							base.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
							this.Grow = false;
						}
					}
				}
				if (this.FlashTimer > 0f)
				{
					this.FlashTimer -= Time.deltaTime;
					if (!this.Red)
					{
						foreach (Material material in this.MyRenderer.materials)
						{
							material.color = new Color(1f, 0f, 0f, 1f);
						}
						this.Frames++;
						if (this.Frames == 5)
						{
							this.Frames = 0;
							this.Red = true;
						}
					}
					else
					{
						foreach (Material material2 in this.MyRenderer.materials)
						{
							material2.color = new Color(1f, 1f, 1f, 1f);
						}
						this.Frames++;
						if (this.Frames == 5)
						{
							this.Frames = 0;
							this.Red = false;
						}
					}
				}
				else if (this.MyRenderer.materials[0].color != new Color(1f, 1f, 1f, 1f))
				{
					foreach (Material material3 in this.MyRenderer.materials)
					{
						material3.color = new Color(1f, 1f, 1f, 1f);
					}
				}
			}
			else
			{
				this.HealthBarParent.transform.localPosition = new Vector3(this.HealthBarParent.transform.localPosition.x, this.HealthBarParent.transform.localPosition.y - Time.deltaTime * 0.2f, this.HealthBarParent.transform.localPosition.z);
				this.HealthBarParent.transform.localScale = new Vector3(this.HealthBarParent.transform.localScale.x, this.HealthBarParent.transform.localScale.y + Time.deltaTime * 0.2f, this.HealthBarParent.transform.localScale.z);
				this.HealthBarParent.color = new Color(this.HealthBarParent.color.r, this.HealthBarParent.color.g, this.HealthBarParent.color.b, this.HealthBarParent.color.a - Time.deltaTime * 0.2f);
				component.Play("succubus_a_damage_l");
				this.ExplosionTimer += Time.deltaTime;
				this.DeathTimer += Time.deltaTime;
				AudioSource component2 = base.GetComponent<AudioSource>();
				if (this.DeathTimer < 5f)
				{
					if (this.DeathTimer > 1f && !this.FinalLineSpoken)
					{
						this.FinalLineSpoken = true;
						component2.clip = this.FinalLine;
						component2.Play();
					}
					if (this.ExplosionTimer > 0.1f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 2.5f), UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity);
						this.ExplosionTimer = 0f;
					}
				}
				else
				{
					if (!this.Screamed)
					{
						this.Screamed = true;
						component2.clip = this.DeathScream;
						component2.Play();
					}
					if (this.DeathTimer > 5f)
					{
						if (!this.PhotoTaken)
						{
							Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0f, 0.0166666675f);
							if (Time.timeScale == 0f)
							{
								ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/Dracula.png");
								if (this.Frame > 0)
								{
									base.StartCoroutine(this.ApplyScreenshot());
								}
								this.Frame++;
							}
						}
						else if (this.Photograph.mainTexture != null)
						{
							this.Photograph.transform.localEulerAngles = new Vector3(this.Photograph.transform.localEulerAngles.x + Time.deltaTime * 3.6f, this.Photograph.transform.localEulerAngles.y, this.Photograph.transform.localEulerAngles.z - Time.deltaTime * 3.6f);
							this.Photograph.transform.localScale = Vector3.MoveTowards(this.Photograph.transform.localScale, Vector3.zero, Time.deltaTime * 0.2f);
							this.Photograph.color = new Color(this.Photograph.color.r, this.Photograph.color.g - Time.deltaTime * 0.2f, this.Photograph.color.b - Time.deltaTime * 0.2f, this.Photograph.color.a);
							if (this.Photograph.transform.localScale == Vector3.zero)
							{
								this.FinalTimer += Time.deltaTime;
								if (this.FinalTimer > 1f)
								{
									YanvaniaGlobals.DraculaDefeated = true;
									SceneManager.LoadScene("YanvaniaTitleScene");
								}
							}
						}
					}
				}
			}
		}
		else
		{
			component.CrossFade("succubus_a_talk_01");
		}
		this.HealthBar.parent.transform.localPosition = new Vector3(Mathf.Lerp(this.HealthBar.parent.transform.localPosition.x, 1025f, Time.deltaTime * 10f), this.HealthBar.parent.transform.localPosition.y, this.HealthBar.parent.transform.localPosition.z);
		this.HealthBar.localScale = new Vector3(this.HealthBar.localScale.x, Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10f), this.HealthBar.localScale.z);
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			base.transform.position = new Vector3(base.transform.position.x, 6.5f, 0f);
			this.Health = 1f;
			this.TakeDamage();
		}
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x001B8E5C File Offset: 0x001B725C
	public void SpawnTeleportEffect()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (base.transform.position.y > 0f)
		{
			this.NewTeleportEffect = UnityEngine.Object.Instantiate<GameObject>(this.TeleportEffect, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.identity);
			component["DraculaTeleport"].time = component["DraculaTeleport"].length;
			component["DraculaTeleport"].speed = -1f;
			component.Play("DraculaTeleport");
			this.Shrink = true;
		}
		else
		{
			this.Teleport();
			this.NewTeleportEffect = UnityEngine.Object.Instantiate<GameObject>(this.TeleportEffect, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z), Quaternion.identity);
			component["DraculaTeleport"].speed = 0.85f;
			component["DraculaTeleport"].time = 0f;
			component.Play("DraculaTeleport");
			this.Grow = true;
		}
		this.NewTeleportEffect.GetComponent<YanvaniaTeleportEffectScript>().Dracula = this;
		this.TeleportTimer = 1f;
		this.AttackID = 0;
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x001B8FF4 File Offset: 0x001B73F4
	private void Teleport()
	{
		base.transform.position = new Vector3(base.transform.position.x, (base.transform.position.y <= 0f) ? 6.5f : -10f, 0f);
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x001B9058 File Offset: 0x001B7458
	public void TakeDamage()
	{
		this.Health -= 5f + ((float)this.Yanmont.Level * 5f - 5f);
		if (this.Health <= 0f)
		{
			this.YanvaniaCamera.StopMusic = true;
			this.Health = 0f;
		}
		else
		{
			this.FlashTimer = 1f;
			this.Injured = true;
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.Injuries[UnityEngine.Random.Range(0, this.Injuries.Length)];
			component.Play();
		}
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x001B90F8 File Offset: 0x001B74F8
	private IEnumerator ApplyScreenshot()
	{
		this.PhotoTaken = true;
		string path = "file:///" + Application.streamingAssetsPath + "/Dracula.png";
		WWW www = new WWW(path);
		yield return www;
		this.Photograph.mainTexture = www.texture;
		this.MainCamera.SetActive(false);
		this.EndCamera.GetComponent<AudioListener>().enabled = true;
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x04003C07 RID: 15367
	public YanvaniaCameraScript YanvaniaCamera;

	// Token: 0x04003C08 RID: 15368
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003C09 RID: 15369
	public UITexture HealthBarParent;

	// Token: 0x04003C0A RID: 15370
	public UITexture Photograph;

	// Token: 0x04003C0B RID: 15371
	public AudioClip DeathScream;

	// Token: 0x04003C0C RID: 15372
	public AudioClip FinalLine;

	// Token: 0x04003C0D RID: 15373
	public GameObject NewTeleportEffect;

	// Token: 0x04003C0E RID: 15374
	public GameObject NewAttack;

	// Token: 0x04003C0F RID: 15375
	public GameObject DoubleFireball;

	// Token: 0x04003C10 RID: 15376
	public GameObject TripleFireball;

	// Token: 0x04003C11 RID: 15377
	public GameObject MainCamera;

	// Token: 0x04003C12 RID: 15378
	public GameObject EndCamera;

	// Token: 0x04003C13 RID: 15379
	public GameObject TeleportEffect;

	// Token: 0x04003C14 RID: 15380
	public GameObject Explosion;

	// Token: 0x04003C15 RID: 15381
	public GameObject Character;

	// Token: 0x04003C16 RID: 15382
	public Transform HealthBar;

	// Token: 0x04003C17 RID: 15383
	public Transform RightHand;

	// Token: 0x04003C18 RID: 15384
	public Renderer MyRenderer;

	// Token: 0x04003C19 RID: 15385
	public AudioClip[] Injuries;

	// Token: 0x04003C1A RID: 15386
	public float ExplosionTimer;

	// Token: 0x04003C1B RID: 15387
	public float TeleportTimer = 10f;

	// Token: 0x04003C1C RID: 15388
	public float FinalTimer;

	// Token: 0x04003C1D RID: 15389
	public float DeathTimer;

	// Token: 0x04003C1E RID: 15390
	public float FlashTimer;

	// Token: 0x04003C1F RID: 15391
	public float Distance;

	// Token: 0x04003C20 RID: 15392
	public float MaxHealth = 100f;

	// Token: 0x04003C21 RID: 15393
	public float Health = 100f;

	// Token: 0x04003C22 RID: 15394
	public bool FinalLineSpoken;

	// Token: 0x04003C23 RID: 15395
	public bool PhotoTaken;

	// Token: 0x04003C24 RID: 15396
	public bool Screamed;

	// Token: 0x04003C25 RID: 15397
	public bool Injured;

	// Token: 0x04003C26 RID: 15398
	public bool Shrink;

	// Token: 0x04003C27 RID: 15399
	public bool Grow;

	// Token: 0x04003C28 RID: 15400
	public bool Red;

	// Token: 0x04003C29 RID: 15401
	public int AttackID;

	// Token: 0x04003C2A RID: 15402
	public int Frames;

	// Token: 0x04003C2B RID: 15403
	public int Frame;
}
