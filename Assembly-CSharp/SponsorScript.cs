using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000519 RID: 1305
public class SponsorScript : MonoBehaviour
{
	// Token: 0x06002035 RID: 8245 RVA: 0x0014EF4C File Offset: 0x0014D34C
	private void Start()
	{
		Time.timeScale = 1f;
		this.Set[1].SetActive(true);
		this.Set[2].SetActive(false);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x0014EFD0 File Offset: 0x0014D3D0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer < 3.2f)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("TitleScene");
			}
		}
	}

	// Token: 0x04002CF1 RID: 11505
	public GameObject[] Set;

	// Token: 0x04002CF2 RID: 11506
	public UISprite Darkness;

	// Token: 0x04002CF3 RID: 11507
	public float Timer;

	// Token: 0x04002CF4 RID: 11508
	public int ID;
}
