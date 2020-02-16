using System;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
public class RadioScript : MonoBehaviour
{
	// Token: 0x06001EF7 RID: 7927 RVA: 0x00139CB0 File Offset: 0x001380B0
	private void Update()
	{
		if (base.transform.parent == null)
		{
			if (this.CooldownTimer > 0f)
			{
				this.CooldownTimer = Mathf.MoveTowards(this.CooldownTimer, 0f, Time.deltaTime);
				if (this.CooldownTimer == 0f)
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				UISprite uisprite = this.Prompt.Circle[0];
				if (uisprite.fillAmount == 0f)
				{
					uisprite.fillAmount = 1f;
					if (!this.On)
					{
						this.Prompt.Label[0].text = "     Turn Off";
						this.MyRenderer.material.mainTexture = this.OnTexture;
						base.GetComponent<AudioSource>().Play();
						this.RadioNotes.SetActive(true);
						this.On = true;
					}
					else
					{
						this.CooldownTimer = 1f;
						this.TurnOff();
					}
				}
			}
			if (this.On && this.Victim == null && this.AlarmDisc != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity);
				AlarmDiscScript component = gameObject.GetComponent<AlarmDiscScript>();
				component.SourceRadio = this;
				component.NoScream = true;
				component.Radio = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
		if (this.Delinquent)
		{
			this.Proximity = 0;
			this.ID = 1;
			while (this.ID < 6)
			{
				if (this.StudentManager.Students[75 + this.ID] != null && Vector3.Distance(base.transform.position, this.StudentManager.Students[75 + this.ID].transform.position) < 1.1f)
				{
					if (!this.StudentManager.Students[75 + this.ID].Alarmed && !this.StudentManager.Students[75 + this.ID].Threatened && this.StudentManager.Students[75 + this.ID].Alive)
					{
						this.Proximity++;
					}
					else
					{
						this.Proximity = -100;
						this.ID = 5;
						this.MyAudio.Stop();
						this.Jukebox.ClubDip = 0f;
					}
				}
				this.ID++;
			}
			if (this.Proximity > 0)
			{
				if (!base.GetComponent<AudioSource>().isPlaying)
				{
					base.GetComponent<AudioSource>().Play();
				}
				float num = Vector3.Distance(this.Prompt.Yandere.transform.position, base.transform.position);
				if (num < 11f)
				{
					this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (10f - num) * 0.2f * this.Jukebox.Volume, Time.deltaTime);
					if (this.Jukebox.ClubDip < 0f)
					{
						this.Jukebox.ClubDip = 0f;
					}
					if (this.Jukebox.ClubDip > this.Jukebox.Volume)
					{
						this.Jukebox.ClubDip = this.Jukebox.Volume;
					}
				}
			}
			else if (this.MyAudio.isPlaying)
			{
				this.MyAudio.Stop();
				this.Jukebox.ClubDip = 0f;
			}
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x0013A094 File Offset: 0x00138494
	public void TurnOff()
	{
		this.Prompt.Label[0].text = "     Turn On";
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		this.MyRenderer.material.mainTexture = this.OffTexture;
		base.GetComponent<AudioSource>().Stop();
		this.RadioNotes.SetActive(false);
		this.CooldownTimer = 1f;
		this.Victim = null;
		this.On = false;
	}

	// Token: 0x0400290F RID: 10511
	public StudentManagerScript StudentManager;

	// Token: 0x04002910 RID: 10512
	public JukeboxScript Jukebox;

	// Token: 0x04002911 RID: 10513
	public GameObject RadioNotes;

	// Token: 0x04002912 RID: 10514
	public GameObject AlarmDisc;

	// Token: 0x04002913 RID: 10515
	public AudioSource MyAudio;

	// Token: 0x04002914 RID: 10516
	public Renderer MyRenderer;

	// Token: 0x04002915 RID: 10517
	public Texture OffTexture;

	// Token: 0x04002916 RID: 10518
	public Texture OnTexture;

	// Token: 0x04002917 RID: 10519
	public StudentScript Victim;

	// Token: 0x04002918 RID: 10520
	public PromptScript Prompt;

	// Token: 0x04002919 RID: 10521
	public float CooldownTimer;

	// Token: 0x0400291A RID: 10522
	public bool Delinquent;

	// Token: 0x0400291B RID: 10523
	public bool On;

	// Token: 0x0400291C RID: 10524
	public int Proximity;

	// Token: 0x0400291D RID: 10525
	public int ID;
}
