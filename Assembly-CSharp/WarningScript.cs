using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200058B RID: 1419
public class WarningScript : MonoBehaviour
{
	// Token: 0x0600225A RID: 8794 RVA: 0x0019DFC4 File Offset: 0x0019C3C4
	private void Start()
	{
		this.WarningLabel.gameObject.SetActive(false);
		this.Label.text = string.Empty;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x0019E040 File Offset: 0x0019C440
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (this.Timer == 0f)
				{
					this.WarningLabel.gameObject.SetActive(true);
					component.Play();
				}
				this.Timer += Time.deltaTime;
				if (this.ID < this.Triggers.Length && this.Timer > this.Triggers[this.ID])
				{
					this.Label.text = this.Text[this.ID];
					this.ID++;
				}
			}
		}
		else
		{
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (Input.anyKey)
		{
			this.FadeOut = true;
		}
	}

	// Token: 0x040037D7 RID: 14295
	public float[] Triggers;

	// Token: 0x040037D8 RID: 14296
	public string[] Text;

	// Token: 0x040037D9 RID: 14297
	public UILabel WarningLabel;

	// Token: 0x040037DA RID: 14298
	public UISprite Darkness;

	// Token: 0x040037DB RID: 14299
	public UILabel Label;

	// Token: 0x040037DC RID: 14300
	public bool FadeOut;

	// Token: 0x040037DD RID: 14301
	public float Timer;

	// Token: 0x040037DE RID: 14302
	public int ID;
}
