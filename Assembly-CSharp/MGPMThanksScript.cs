using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000176 RID: 374
public class MGPMThanksScript : MonoBehaviour
{
	// Token: 0x06000BDD RID: 3037 RVA: 0x0005BA4B File Offset: 0x00059E4B
	private void Start()
	{
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x0005BA78 File Offset: 0x00059E78
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
			if (this.Black.material.color.a == 0f)
			{
				this.Jukebox.Play();
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (!this.Jukebox.isPlaying)
			{
				this.Jukebox.clip = this.ThanksMusic;
				this.Jukebox.loop = true;
				this.Jukebox.Play();
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			if (Input.anyKeyDown)
			{
				this.Phase++;
			}
		}
		else
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume = 1f - this.Black.material.color.a;
			if (this.Black.material.color.a == 1f)
			{
				HomeGlobals.MiyukiDefeated = true;
				SceneManager.LoadScene("HomeScene");
			}
		}
	}

	// Token: 0x04000955 RID: 2389
	public AudioClip ThanksMusic;

	// Token: 0x04000956 RID: 2390
	public AudioSource Jukebox;

	// Token: 0x04000957 RID: 2391
	public Renderer Black;

	// Token: 0x04000958 RID: 2392
	public int Phase = 1;
}
