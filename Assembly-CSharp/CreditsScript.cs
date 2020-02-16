using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200037D RID: 893
public class CreditsScript : MonoBehaviour
{
	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06001854 RID: 6228 RVA: 0x000D4F03 File Offset: 0x000D3303
	private bool ShouldStopCredits
	{
		get
		{
			return this.ID == this.JSON.Credits.Length;
		}
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000D4F1A File Offset: 0x000D331A
	private GameObject SpawnLabel(int size)
	{
		return UnityEngine.Object.Instantiate<GameObject>((size != 1) ? this.BigCreditsLabel : this.SmallCreditsLabel, this.SpawnPoint.position, Quaternion.identity);
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000D4F4C File Offset: 0x000D334C
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Begin)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Begin = true;
				component.Play();
				this.Timer = 0f;
			}
		}
		else
		{
			if (!this.ShouldStopCredits)
			{
				if (this.Timer == 0f)
				{
					CreditJson creditJson = this.JSON.Credits[this.ID];
					GameObject gameObject = this.SpawnLabel(creditJson.Size);
					this.TimerLimit = (float)creditJson.Size * this.SpeedUpFactor;
					gameObject.transform.parent = this.Panel;
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject.GetComponent<UILabel>().text = creditJson.Name;
					this.ID++;
				}
				this.Timer += Time.deltaTime * this.Speed;
				if (this.Timer >= this.TimerLimit)
				{
					this.Timer = 0f;
				}
			}
			if (Input.GetButtonDown("B") || !component.isPlaying)
			{
				this.FadeOut = true;
			}
		}
		if (this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			component.volume -= Time.deltaTime;
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("TitleScene");
			}
		}
		bool keyDown = Input.GetKeyDown(KeyCode.Minus);
		bool keyDown2 = Input.GetKeyDown(KeyCode.Equals);
		if (keyDown)
		{
			Time.timeScale -= 1f;
		}
		else if (keyDown2)
		{
			Time.timeScale += 1f;
		}
		if (keyDown || keyDown2)
		{
			component.pitch = Time.timeScale;
		}
	}

	// Token: 0x04001ABB RID: 6843
	[SerializeField]
	private JsonScript JSON;

	// Token: 0x04001ABC RID: 6844
	[SerializeField]
	private Transform SpawnPoint;

	// Token: 0x04001ABD RID: 6845
	[SerializeField]
	private Transform Panel;

	// Token: 0x04001ABE RID: 6846
	[SerializeField]
	private GameObject SmallCreditsLabel;

	// Token: 0x04001ABF RID: 6847
	[SerializeField]
	private GameObject BigCreditsLabel;

	// Token: 0x04001AC0 RID: 6848
	[SerializeField]
	private UISprite Darkness;

	// Token: 0x04001AC1 RID: 6849
	[SerializeField]
	private int ID;

	// Token: 0x04001AC2 RID: 6850
	[SerializeField]
	private float SpeedUpFactor;

	// Token: 0x04001AC3 RID: 6851
	[SerializeField]
	private float TimerLimit;

	// Token: 0x04001AC4 RID: 6852
	[SerializeField]
	private float FadeTimer;

	// Token: 0x04001AC5 RID: 6853
	[SerializeField]
	private float Speed = 1f;

	// Token: 0x04001AC6 RID: 6854
	[SerializeField]
	private float Timer;

	// Token: 0x04001AC7 RID: 6855
	[SerializeField]
	private bool FadeOut;

	// Token: 0x04001AC8 RID: 6856
	[SerializeField]
	private bool Begin;

	// Token: 0x04001AC9 RID: 6857
	private const int SmallTextSize = 1;

	// Token: 0x04001ACA RID: 6858
	private const int BigTextSize = 2;
}
