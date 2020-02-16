using System;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class DelinquentManagerScript : MonoBehaviour
{
	// Token: 0x060018AD RID: 6317 RVA: 0x000DD8E0 File Offset: 0x000DBCE0
	private void Start()
	{
		this.Delinquents.SetActive(false);
		this.TimerMax = 15f;
		this.Timer = 15f;
		this.Phase++;
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x000DD914 File Offset: 0x000DBD14
	private void Update()
	{
		this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
		if (this.Attacker != null && !this.Attacker.Attacking && this.Attacker.ExpressedSurprise && this.Attacker.Run && !this.Aggro)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.Attacker.AggroClips[UnityEngine.Random.Range(0, this.Attacker.AggroClips.Length)];
			component.Play();
			this.Aggro = true;
		}
		if (this.Panel.activeInHierarchy && this.Clock.HourTime > this.NextTime[this.Phase])
		{
			if (this.Phase == 3 && this.Clock.HourTime > 7.25f)
			{
				this.TimerMax = 75f;
				this.Timer = 75f;
				this.Phase++;
			}
			else if (this.Phase == 5 && this.Clock.HourTime > 8.5f)
			{
				this.TimerMax = 285f;
				this.Timer = 285f;
				this.Phase++;
			}
			else if (this.Phase == 7 && this.Clock.HourTime > 13.25f)
			{
				this.TimerMax = 15f;
				this.Timer = 15f;
				this.Phase++;
			}
			else if (this.Phase == 9 && this.Clock.HourTime > 13.5f)
			{
				this.TimerMax = 135f;
				this.Timer = 135f;
				this.Phase++;
			}
			if (this.Attacker == null)
			{
				this.Timer -= Time.deltaTime * (this.Clock.TimeSpeed / 60f);
			}
			this.Circle.fillAmount = 1f - this.Timer / this.TimerMax;
			if (this.Timer <= 0f)
			{
				this.Delinquents.SetActive(!this.Delinquents.activeInHierarchy);
				if (this.Phase < 8)
				{
					this.Phase++;
				}
				else
				{
					this.Delinquents.SetActive(false);
					this.Panel.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x000DDBC8 File Offset: 0x000DBFC8
	public void CheckTime()
	{
		if (this.Clock.HourTime < 13f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 6;
		}
		else if (this.Clock.HourTime < 15.5f)
		{
			this.Delinquents.SetActive(false);
			this.TimerMax = 15f;
			this.Timer = 15f;
			this.Phase = 8;
		}
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x000DDC56 File Offset: 0x000DC056
	public void EasterEgg()
	{
		this.RapBeat.SetActive(true);
		this.Mirror.Limit++;
	}

	// Token: 0x04001BD7 RID: 7127
	public GameObject Delinquents;

	// Token: 0x04001BD8 RID: 7128
	public GameObject RapBeat;

	// Token: 0x04001BD9 RID: 7129
	public GameObject Panel;

	// Token: 0x04001BDA RID: 7130
	public float[] NextTime;

	// Token: 0x04001BDB RID: 7131
	public DelinquentScript Attacker;

	// Token: 0x04001BDC RID: 7132
	public MirrorScript Mirror;

	// Token: 0x04001BDD RID: 7133
	public UILabel TimeLabel;

	// Token: 0x04001BDE RID: 7134
	public ClockScript Clock;

	// Token: 0x04001BDF RID: 7135
	public UISprite Circle;

	// Token: 0x04001BE0 RID: 7136
	public float SpeechTimer;

	// Token: 0x04001BE1 RID: 7137
	public float TimerMax;

	// Token: 0x04001BE2 RID: 7138
	public float Timer;

	// Token: 0x04001BE3 RID: 7139
	public bool Aggro;

	// Token: 0x04001BE4 RID: 7140
	public int Phase = 1;
}
