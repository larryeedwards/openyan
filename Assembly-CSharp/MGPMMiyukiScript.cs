using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class MGPMMiyukiScript : MonoBehaviour
{
	// Token: 0x06000BD1 RID: 3025 RVA: 0x0005A486 File Offset: 0x00058886
	private void Start()
	{
		Time.timeScale = 1f;
		if (!GameGlobals.EasyMode)
		{
			this.MagicBar.parent.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0005A4B4 File Offset: 0x000588B4
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == 3)
			{
				this.Frame = 0;
				if (this.RightPhase == 1)
				{
					this.RightPhase = 2;
				}
				else if (this.RightPhase == 3)
				{
					this.RightPhase = 0;
				}
				if (this.LeftPhase == 1)
				{
					this.LeftPhase = 2;
				}
				else if (this.LeftPhase == 3)
				{
					this.LeftPhase = 0;
				}
			}
			if (this.RightPhase == 0 && this.LeftPhase == 0)
			{
				this.MyRenderer.material.mainTexture = this.ForwardSprite[this.Frame];
			}
			else if (this.RightPhase == 1)
			{
				this.MyRenderer.material.mainTexture = this.TurnRightSprite[this.Frame];
			}
			else if (this.RightPhase == 2)
			{
				this.MyRenderer.material.mainTexture = this.RightSprite[this.Frame];
			}
			else if (this.RightPhase == 3)
			{
				this.MyRenderer.material.mainTexture = this.ReverseRightSprite[this.Frame];
			}
			else if (this.LeftPhase == 1)
			{
				this.MyRenderer.material.mainTexture = this.TurnLeftSprite[this.Frame];
			}
			else if (this.LeftPhase == 2)
			{
				this.MyRenderer.material.mainTexture = this.LeftSprite[this.Frame];
			}
			else if (this.LeftPhase == 3)
			{
				this.MyRenderer.material.mainTexture = this.ReverseLeftSprite[this.Frame];
			}
		}
		float num;
		if (Input.GetButton("LB"))
		{
			num = this.Speed * 0.5f;
		}
		else
		{
			num = this.Speed;
		}
		if (this.Gameplay)
		{
			if (Input.GetKey("right") || this.InputManager.DPadRight || Input.GetAxis("Horizontal") > 0.5f)
			{
				if (this.RightPhase < 1)
				{
					this.RightPhase = 1;
					this.LeftPhase = 0;
					this.Frame = 0;
				}
				this.PositionX += num * Time.deltaTime;
			}
			else if (this.RightPhase == 1 || this.RightPhase == 2)
			{
				this.RightPhase = 3;
				this.Frame = 0;
			}
			if (Input.GetKey("left") || this.InputManager.DPadLeft || Input.GetAxis("Horizontal") < -0.5f)
			{
				if (this.LeftPhase < 1)
				{
					this.RightPhase = 0;
					this.LeftPhase = 1;
					this.Frame = 0;
				}
				this.PositionX -= num * Time.deltaTime;
			}
			else if (this.LeftPhase == 1 || this.LeftPhase == 2)
			{
				this.LeftPhase = 3;
				this.Frame = 0;
			}
			if (Input.GetKey("up") || this.InputManager.DPadUp || Input.GetAxis("Vertical") > 0.5f)
			{
				this.PositionY += num * Time.deltaTime;
			}
			if (Input.GetKey("down") || this.InputManager.DPadDown || Input.GetAxis("Vertical") < -0.5f)
			{
				this.PositionY -= num * Time.deltaTime;
			}
			if (this.PositionX > 108f)
			{
				this.PositionX = 108f;
			}
			if (this.PositionX < -110f)
			{
				this.PositionX = -110f;
			}
			if (this.PositionY > 224f)
			{
				this.PositionY = 224f;
			}
			if (this.PositionY < -224f)
			{
				this.PositionY = -224f;
			}
			base.transform.localPosition = new Vector3(this.PositionX, this.PositionY, 0f);
			if (Input.GetKey("z") || Input.GetKey("y") || Input.GetButton("A"))
			{
				if (this.ShootTimer == 0f)
				{
					if (this.MagicLevel == 0)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else if (this.MagicLevel == 1)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.1f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.1f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else if (this.MagicLevel == 2)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.4f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject.GetComponent<MGPMProjectileScript>().Angle = 1;
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.4f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject.GetComponent<MGPMProjectileScript>().Angle = -1;
					}
					this.ShootTimer = 0f;
				}
				this.ShootTimer += Time.deltaTime;
				if (this.ShootTimer >= 0.075f)
				{
					this.ShootTimer = 0f;
				}
			}
			if (Input.GetKeyUp("z") || Input.GetKeyUp("y") || Input.GetButtonUp("A"))
			{
				this.ShootTimer = 0f;
			}
			if (Input.GetKeyDown("r"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if (this.Invincibility > 0f)
		{
			this.Invincibility = Mathf.MoveTowards(this.Invincibility, 0f, Time.deltaTime);
			if (this.Invincibility == 0f)
			{
				this.MyRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 0f));
			}
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0005B180 File Offset: 0x00059580
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 9)
		{
			if (this.Invincibility == 0f)
			{
				this.Health--;
				if (GameGlobals.EasyMode)
				{
					this.MyRenderer.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f, 1f));
					this.Invincibility = 1f;
				}
				if (this.Health > 0)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
					gameObject.transform.parent = base.transform.parent;
					gameObject.transform.localScale = new Vector3(64f, 64f, 1f);
					AudioSource.PlayClipAtPoint(this.DamageSound, base.transform.position);
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DeathExplosion, base.transform.position, Quaternion.identity);
					gameObject.transform.parent = base.transform.parent;
					gameObject.transform.localScale = new Vector3(128f, 128f, 1f);
					AudioSource.PlayClipAtPoint(this.DeathSound, base.transform.position);
					this.GameplayManager.BeginGameOver();
					base.gameObject.SetActive(false);
				}
			}
			this.UpdateHearts();
		}
		else if (collision.gameObject.layer == 15)
		{
			AudioSource.PlayClipAtPoint(this.PickUpSound, base.transform.position);
			this.GameplayManager.Score += 10;
			this.Magic += 1f;
			if (this.Magic == 20f)
			{
				this.MagicLevel++;
				if (this.MagicLevel > 3 && this.Health < 3)
				{
					this.Health++;
					this.UpdateHearts();
				}
				this.Magic = 0f;
			}
			this.MagicBar.localScale = new Vector3(this.Magic / 20f, 1f, 1f);
			UnityEngine.Object.Destroy(collision.gameObject);
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0005B3D8 File Offset: 0x000597D8
	private void UpdateHearts()
	{
		this.Hearts[1].SetActive(false);
		this.Hearts[2].SetActive(false);
		this.Hearts[3].SetActive(false);
		for (int i = 1; i < this.Health + 1; i++)
		{
			this.Hearts[i].SetActive(true);
		}
	}

	// Token: 0x04000923 RID: 2339
	public MGPMManagerScript GameplayManager;

	// Token: 0x04000924 RID: 2340
	public InputManagerScript InputManager;

	// Token: 0x04000925 RID: 2341
	public AudioClip DamageSound;

	// Token: 0x04000926 RID: 2342
	public AudioClip PickUpSound;

	// Token: 0x04000927 RID: 2343
	public AudioClip DeathSound;

	// Token: 0x04000928 RID: 2344
	public GameObject Projectile;

	// Token: 0x04000929 RID: 2345
	public GameObject DeathExplosion;

	// Token: 0x0400092A RID: 2346
	public GameObject Explosion;

	// Token: 0x0400092B RID: 2347
	public Transform SpawnPoint;

	// Token: 0x0400092C RID: 2348
	public Transform MagicBar;

	// Token: 0x0400092D RID: 2349
	public Renderer MyRenderer;

	// Token: 0x0400092E RID: 2350
	public Texture[] ForwardSprite;

	// Token: 0x0400092F RID: 2351
	public Texture[] ReverseRightSprite;

	// Token: 0x04000930 RID: 2352
	public Texture[] TurnRightSprite;

	// Token: 0x04000931 RID: 2353
	public Texture[] RightSprite;

	// Token: 0x04000932 RID: 2354
	public Texture[] ReverseLeftSprite;

	// Token: 0x04000933 RID: 2355
	public Texture[] TurnLeftSprite;

	// Token: 0x04000934 RID: 2356
	public Texture[] LeftSprite;

	// Token: 0x04000935 RID: 2357
	public GameObject[] Hearts;

	// Token: 0x04000936 RID: 2358
	public int MagicLevel;

	// Token: 0x04000937 RID: 2359
	public int Frame;

	// Token: 0x04000938 RID: 2360
	public int RightPhase;

	// Token: 0x04000939 RID: 2361
	public int LeftPhase;

	// Token: 0x0400093A RID: 2362
	public int Health;

	// Token: 0x0400093B RID: 2363
	public float Invincibility;

	// Token: 0x0400093C RID: 2364
	public float ShootTimer;

	// Token: 0x0400093D RID: 2365
	public float Magic;

	// Token: 0x0400093E RID: 2366
	public float Speed;

	// Token: 0x0400093F RID: 2367
	public float Timer;

	// Token: 0x04000940 RID: 2368
	public float FPS;

	// Token: 0x04000941 RID: 2369
	public float PositionX;

	// Token: 0x04000942 RID: 2370
	public float PositionY;

	// Token: 0x04000943 RID: 2371
	public bool Gameplay;
}
