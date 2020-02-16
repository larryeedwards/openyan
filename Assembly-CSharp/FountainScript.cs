using System;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class FountainScript : MonoBehaviour
{
	// Token: 0x060019B3 RID: 6579 RVA: 0x000F08BE File Offset: 0x000EECBE
	private void Start()
	{
		this.SpraySFX.volume = 0.1f;
		this.DropsSFX.volume = 0.1f;
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x000F08E0 File Offset: 0x000EECE0
	private void Update()
	{
		if (this.StartTimer < 1f)
		{
			this.StartTimer += Time.deltaTime;
			if (this.StartTimer > 1f)
			{
				this.SpraySFX.gameObject.SetActive(true);
				this.DropsSFX.gameObject.SetActive(true);
			}
		}
		if (this.Drowning)
		{
			if (this.Timer == 0f && this.EventSubtitle.transform.localScale.x < 1f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "Hey, what are you -";
				base.GetComponent<AudioSource>().Play();
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 3f && this.EventSubtitle.transform.localScale.x > 0f)
			{
				this.EventSubtitle.transform.localScale = Vector3.zero;
				this.EventSubtitle.text = string.Empty;
				this.Splashes.Play();
			}
			if (this.Timer > 9f)
			{
				this.Drowning = false;
				this.Splashes.Stop();
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x04001EDA RID: 7898
	public ParticleSystem Splashes;

	// Token: 0x04001EDB RID: 7899
	public UILabel EventSubtitle;

	// Token: 0x04001EDC RID: 7900
	public Collider[] Colliders;

	// Token: 0x04001EDD RID: 7901
	public bool Drowning;

	// Token: 0x04001EDE RID: 7902
	public AudioSource SpraySFX;

	// Token: 0x04001EDF RID: 7903
	public AudioSource DropsSFX;

	// Token: 0x04001EE0 RID: 7904
	public float StartTimer;

	// Token: 0x04001EE1 RID: 7905
	public float Timer;
}
