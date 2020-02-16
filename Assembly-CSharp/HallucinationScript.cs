using System;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class HallucinationScript : MonoBehaviour
{
	// Token: 0x06001C67 RID: 7271 RVA: 0x000FDAE0 File Offset: 0x000FBEE0
	private void Start()
	{
		this.YandereHairRenderer.material = this.Black;
		this.RivalHairRenderer.material = this.Black;
		this.YandereRenderer.materials[0] = this.Black;
		this.YandereRenderer.materials[1] = this.Black;
		this.YandereRenderer.materials[2] = this.Black;
		this.RivalRenderer.materials[0] = this.Black;
		this.RivalRenderer.materials[1] = this.Black;
		this.RivalRenderer.materials[2] = this.Black;
		foreach (Renderer renderer in this.WeaponRenderers)
		{
			if (renderer != null)
			{
				renderer.material = this.Black;
			}
		}
		this.SawRenderer.material = this.Black;
		this.MakeTransparent();
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x000FDBD0 File Offset: 0x000FBFD0
	private void Update()
	{
		if (this.Yandere.Sanity < 33.33333f)
		{
			if (!this.Yandere.Aiming && this.Yandere.CanMove)
			{
				this.Timer += Time.deltaTime;
			}
			if (this.Timer > 6f)
			{
				this.Weapon = UnityEngine.Random.Range(1, 6);
				base.transform.position = this.Yandere.transform.position + this.Yandere.transform.forward;
				base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
				this.YandereAnimation["f02_" + this.WeaponName[this.Weapon] + "LowSanityA_00"].time = 0f;
				this.RivalAnimation["f02_" + this.WeaponName[this.Weapon] + "LowSanityB_00"].time = 0f;
				this.YandereAnimation.Play("f02_" + this.WeaponName[this.Weapon] + "LowSanityA_00");
				this.RivalAnimation.Play("f02_" + this.WeaponName[this.Weapon] + "LowSanityB_00");
				if (this.Weapon == 1)
				{
					this.YandereAnimation.transform.localPosition = new Vector3(0f, 0f, 0f);
				}
				else if (this.Weapon == 5)
				{
					this.YandereAnimation.transform.localPosition = new Vector3(-0.25f, 0f, 0f);
				}
				else
				{
					this.YandereAnimation.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
				}
				foreach (GameObject gameObject in this.Weapons)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
				this.Weapons[this.Weapon].SetActive(true);
				this.Hallucinate = true;
				this.Timer = 0f;
			}
		}
		if (this.Hallucinate)
		{
			if (this.YandereAnimation["f02_" + this.WeaponName[this.Weapon] + "LowSanityA_00"].time < 3f)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.33333f);
			}
			else
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.33333f);
			}
			this.YandereHairRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
			this.RivalHairRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
			this.YandereRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.YandereRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
			this.YandereRenderer.materials[2].color = new Color(0f, 0f, 0f, this.Alpha);
			this.RivalRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
			this.RivalRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
			this.RivalRenderer.materials[2].color = new Color(0f, 0f, 0f, this.Alpha);
			foreach (Renderer renderer in this.WeaponRenderers)
			{
				if (renderer != null)
				{
					renderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
				}
			}
			this.SawRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
			if (this.YandereAnimation["f02_" + this.WeaponName[this.Weapon] + "LowSanityA_00"].time == this.YandereAnimation["f02_" + this.WeaponName[this.Weapon] + "LowSanityA_00"].length || this.Yandere.Aiming)
			{
				this.MakeTransparent();
				this.Hallucinate = false;
			}
		}
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x000FE104 File Offset: 0x000FC504
	private void MakeTransparent()
	{
		this.Alpha = 0f;
		this.YandereHairRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
		this.RivalHairRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
		this.YandereRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
		this.YandereRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
		this.YandereRenderer.materials[2].color = new Color(0f, 0f, 0f, this.Alpha);
		this.RivalRenderer.materials[0].color = new Color(0f, 0f, 0f, this.Alpha);
		this.RivalRenderer.materials[1].color = new Color(0f, 0f, 0f, this.Alpha);
		this.RivalRenderer.materials[2].color = new Color(0f, 0f, 0f, this.Alpha);
		foreach (Renderer renderer in this.WeaponRenderers)
		{
			if (renderer != null)
			{
				renderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
			}
		}
		this.SawRenderer.material.color = new Color(0f, 0f, 0f, this.Alpha);
	}

	// Token: 0x040020A0 RID: 8352
	public SkinnedMeshRenderer YandereHairRenderer;

	// Token: 0x040020A1 RID: 8353
	public SkinnedMeshRenderer YandereRenderer;

	// Token: 0x040020A2 RID: 8354
	public SkinnedMeshRenderer RivalHairRenderer;

	// Token: 0x040020A3 RID: 8355
	public SkinnedMeshRenderer RivalRenderer;

	// Token: 0x040020A4 RID: 8356
	public Animation YandereAnimation;

	// Token: 0x040020A5 RID: 8357
	public Animation RivalAnimation;

	// Token: 0x040020A6 RID: 8358
	public YandereScript Yandere;

	// Token: 0x040020A7 RID: 8359
	public Material Black;

	// Token: 0x040020A8 RID: 8360
	public bool Hallucinate;

	// Token: 0x040020A9 RID: 8361
	public float Alpha;

	// Token: 0x040020AA RID: 8362
	public float Timer;

	// Token: 0x040020AB RID: 8363
	public int Weapon;

	// Token: 0x040020AC RID: 8364
	public Renderer[] WeaponRenderers;

	// Token: 0x040020AD RID: 8365
	public Renderer SawRenderer;

	// Token: 0x040020AE RID: 8366
	public GameObject[] Weapons;

	// Token: 0x040020AF RID: 8367
	public string[] WeaponName;
}
