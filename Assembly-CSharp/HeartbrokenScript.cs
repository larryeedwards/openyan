using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000412 RID: 1042
public class HeartbrokenScript : MonoBehaviour
{
	// Token: 0x06001C79 RID: 7289 RVA: 0x000FF7DC File Offset: 0x000FDBDC
	private void Start()
	{
		if (this.Yandere.Bloodiness > 0f && !this.Yandere.RedPaint && !this.Yandere.Unmasked)
		{
			this.Arrested = true;
		}
		if (this.Confessed)
		{
			this.Letters[0].text = "S";
			this.Letters[1].text = "E";
			this.Letters[2].text = "N";
			this.Letters[3].text = "P";
			this.Letters[4].text = "A";
			this.Letters[5].text = "I";
			this.Letters[6].text = string.Empty;
			this.Letters[7].text = "L";
			this.Letters[8].text = "O";
			this.Letters[9].text = "S";
			this.Letters[10].text = "T";
			this.LetterID = 0;
			this.StopID = 11;
		}
		else if (this.Yandere.Attacked)
		{
			if (!this.Headmaster)
			{
				this.Letters[0].text = string.Empty;
				this.Letters[1].text = "C";
				this.Letters[2].text = "O";
				this.Letters[3].text = "M";
				this.Letters[4].text = "A";
				this.Letters[5].text = "T";
				this.Letters[6].text = "O";
				this.Letters[7].text = "S";
				this.Letters[8].text = "E";
				this.Letters[9].text = string.Empty;
				this.Letters[10].text = string.Empty;
				this.Letters[3].fontSize = 250;
				this.LetterID = 1;
				this.StopID = 9;
			}
			else
			{
				this.Letters[0].text = "?";
				this.Letters[1].text = "?";
				this.Letters[2].text = "?";
				this.Letters[3].text = "?";
				this.Letters[4].text = "?";
				this.Letters[5].text = "?";
				this.Letters[6].text = "?";
				this.Letters[7].text = "?";
				this.Letters[8].text = "?";
				this.Letters[9].text = "?";
				this.Letters[10].text = string.Empty;
				this.LetterID = 0;
				this.StopID = 10;
			}
			foreach (UILabel uilabel in this.Letters)
			{
				uilabel.transform.localPosition = new Vector3(uilabel.transform.localPosition.x + 100f, uilabel.transform.localPosition.y, uilabel.transform.localPosition.z);
			}
			this.SNAP.SetActive(false);
			this.Cursor.Options = 3;
		}
		else if (this.Yandere.Lost || this.ShoulderCamera.LookDown || this.ShoulderCamera.Counter || this.ShoulderCamera.ObstacleCounter)
		{
			this.Letters[0].text = "A";
			this.Letters[1].text = "P";
			this.Letters[2].text = "P";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "H";
			this.Letters[6].text = "E";
			this.Letters[7].text = "N";
			this.Letters[8].text = "D";
			this.Letters[9].text = "E";
			this.Letters[10].text = "D";
			this.LetterID = 0;
			this.StopID = 11;
		}
		else if (this.Exposed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = string.Empty;
			this.Letters[2].text = "E";
			this.Letters[3].text = "X";
			this.Letters[4].text = "P";
			this.Letters[5].text = "O";
			this.Letters[6].text = "S";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel2 in this.Letters)
			{
				uilabel2.transform.localPosition = new Vector3(uilabel2.transform.localPosition.x + 100f, uilabel2.transform.localPosition.y, uilabel2.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
		}
		else if (this.Arrested)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "A";
			this.Letters[2].text = "R";
			this.Letters[3].text = "R";
			this.Letters[4].text = "E";
			this.Letters[5].text = "S";
			this.Letters[6].text = "T";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel3 in this.Letters)
			{
				uilabel3.transform.localPosition = new Vector3(uilabel3.transform.localPosition.x + 100f, uilabel3.transform.localPosition.y, uilabel3.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
		}
		else if (this.Counselor.Expelled || this.Yandere.Sprayed)
		{
			this.Letters[0].text = string.Empty;
			this.Letters[1].text = "E";
			this.Letters[2].text = "X";
			this.Letters[3].text = "P";
			this.Letters[4].text = "E";
			this.Letters[5].text = "L";
			this.Letters[6].text = "L";
			this.Letters[7].text = "E";
			this.Letters[8].text = "D";
			this.Letters[9].text = string.Empty;
			this.Letters[10].text = string.Empty;
			foreach (UILabel uilabel4 in this.Letters)
			{
				uilabel4.transform.localPosition = new Vector3(uilabel4.transform.localPosition.x + 100f, uilabel4.transform.localPosition.y, uilabel4.transform.localPosition.z);
			}
			this.LetterID = 1;
			this.StopID = 9;
		}
		else
		{
			this.LetterID = 0;
			this.StopID = 11;
		}
		this.ID = 0;
		while (this.ID < this.Letters.Length)
		{
			UILabel uilabel5 = this.Letters[this.ID];
			uilabel5.transform.localScale = new Vector3(10f, 10f, 1f);
			uilabel5.color = new Color(uilabel5.color.r, uilabel5.color.g, uilabel5.color.b, 0f);
			this.Origins[this.ID] = uilabel5.transform.localPosition;
			this.ID++;
		}
		this.ID = 0;
		while (this.ID < this.Options.Length)
		{
			UILabel uilabel6 = this.Options[this.ID];
			uilabel6.color = new Color(uilabel6.color.r, uilabel6.color.g, uilabel6.color.b, 0f);
			this.ID++;
		}
		this.ID = 0;
		this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 0f);
		if (this.Noticed)
		{
			this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, 0f);
			this.Ground.color = new Color(this.Ground.color.r, this.Ground.color.g, this.Ground.color.b, 0f);
		}
		else
		{
			base.transform.parent.transform.position = new Vector3(base.transform.parent.transform.position.x, 100f, base.transform.parent.transform.position.z);
		}
		this.Clock.StopTime = true;
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x00100398 File Offset: 0x000FE798
	private void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			base.gameObject.GetComponent<AudioSource>().Stop();
		}
		this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
		if (this.VibrationTimer == 0f)
		{
			GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
		}
		if (this.Noticed)
		{
			this.Ground.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			this.Ground.transform.position = new Vector3(this.Ground.transform.position.x, this.Yandere.transform.position.y, this.Ground.transform.position.z);
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 3f)
		{
			if (this.Phase == 1)
			{
				if (this.Noticed)
				{
					this.UpdateSubtitle();
				}
				this.Phase += ((this.Subtitle.color.a <= 0f) ? 2 : 1);
			}
			else if (this.Phase == 2)
			{
				this.AudioTimer += Time.deltaTime;
				if (this.AudioTimer > this.Subtitle.GetComponent<AudioSource>().clip.length)
				{
					this.Phase++;
				}
			}
		}
		if (this.Background.color.a < 1f)
		{
			this.Background.color = new Color(this.Background.color.r, this.Background.color.g, this.Background.color.b, this.Background.color.a + Time.deltaTime);
			this.Ground.color = new Color(this.Ground.color.r, this.Ground.color.g, this.Ground.color.b, this.Ground.color.a + Time.deltaTime);
			if (this.Background.color.a >= 1f)
			{
				this.MainCamera.enabled = false;
			}
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.LetterID < this.StopID)
		{
			UILabel uilabel = this.Letters[this.LetterID];
			uilabel.transform.localScale = Vector3.MoveTowards(uilabel.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 100f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, uilabel.color.a + Time.deltaTime * 10f);
			if (uilabel.transform.localScale == new Vector3(1f, 1f, 1f))
			{
				component.PlayOneShot(this.Slam);
				GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
				this.VibrationTimer = 0.1f;
				this.LetterID++;
				if (this.LetterID == this.StopID)
				{
					this.ID = 0;
				}
			}
		}
		else if (this.Phase == 3)
		{
			if (this.Options[0].color.a == 0f)
			{
				this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 0f);
				component.Play();
			}
			if (this.ID < this.Options.Length)
			{
				UILabel uilabel2 = this.Options[this.ID];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, uilabel2.color.a + Time.deltaTime * 2f);
				if (uilabel2.color.a >= 1f)
				{
					this.ID++;
				}
			}
		}
		if (!this.Freeze)
		{
			this.ShakeID = 0;
			while (this.ShakeID < this.Letters.Length)
			{
				UILabel uilabel3 = this.Letters[this.ShakeID];
				Vector3 vector = this.Origins[this.ShakeID];
				uilabel3.transform.localPosition = new Vector3(vector.x + UnityEngine.Random.Range(-5f, 5f), vector.y + UnityEngine.Random.Range(-5f, 5f), uilabel3.transform.localPosition.z);
				this.ShakeID++;
			}
		}
		this.GrowID = 0;
		while (this.GrowID < 4)
		{
			UILabel uilabel4 = this.Options[this.GrowID];
			uilabel4.transform.localScale = Vector3.Lerp(uilabel4.transform.localScale, (this.Cursor.Selected - 1 == this.GrowID) ? new Vector3(1f, 1f, 1f) : new Vector3(0.5f, 0.5f, 0.5f), Time.deltaTime * 10f);
			this.GrowID++;
		}
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x00100A14 File Offset: 0x000FEE14
	private void UpdateSubtitle()
	{
		StudentScript component = this.Yandere.Senpai.GetComponent<StudentScript>();
		if (!component.Teacher && this.Yandere.Noticed)
		{
			this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 1f);
			GameOverType gameOverCause = component.GameOverCause;
			int num = 0;
			if (gameOverCause == GameOverType.Stalking)
			{
				num = 4;
			}
			else if (gameOverCause == GameOverType.Insanity)
			{
				num = 3;
			}
			else if (gameOverCause == GameOverType.Weapon)
			{
				num = 2;
			}
			else if (gameOverCause == GameOverType.Murder)
			{
				num = 5;
			}
			else if (gameOverCause == GameOverType.Blood)
			{
				num = 1;
			}
			else if (gameOverCause == GameOverType.Lewd)
			{
				num = 6;
			}
			this.Subtitle.text = this.NoticedLines[num];
			this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[num];
			this.Subtitle.GetComponent<AudioSource>().Play();
		}
		else if (this.Headmaster)
		{
			this.Subtitle.color = new Color(this.Subtitle.color.r, this.Subtitle.color.g, this.Subtitle.color.b, 1f);
			this.Subtitle.text = this.NoticedLines[7];
			this.Subtitle.GetComponent<AudioSource>().clip = this.NoticedClips[7];
			this.Subtitle.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x00100BDC File Offset: 0x000FEFDC
	public void Darken()
	{
		for (int i = 0; i < this.Letters.Length; i++)
		{
			if (this.Letters[i].color.a > 1f)
			{
				this.Letters[i].color = new Color(1f, 0f, 0f, 1f);
			}
			this.Letters[i].color = new Color(1f, 0f, 0f, this.Letters[i].color.a - 0.05882353f);
		}
		for (int i = 0; i < 3; i++)
		{
			if (this.Options[i].color.a > 1f)
			{
				this.Options[i].color = new Color(this.Options[i].color.r, this.Options[i].color.g, this.Options[i].color.b, 1f);
			}
			this.Options[i].color = new Color(this.Options[i].color.r, this.Options[i].color.g, this.Options[i].color.b, this.Options[i].color.a - 0.05882353f);
		}
	}

	// Token: 0x040020FB RID: 8443
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x040020FC RID: 8444
	public HeartbrokenCursorScript Cursor;

	// Token: 0x040020FD RID: 8445
	public CounselorScript Counselor;

	// Token: 0x040020FE RID: 8446
	public YandereScript Yandere;

	// Token: 0x040020FF RID: 8447
	public ClockScript Clock;

	// Token: 0x04002100 RID: 8448
	public AudioListener Listener;

	// Token: 0x04002101 RID: 8449
	public AudioClip[] NoticedClips;

	// Token: 0x04002102 RID: 8450
	public string[] NoticedLines;

	// Token: 0x04002103 RID: 8451
	public UILabel[] Letters;

	// Token: 0x04002104 RID: 8452
	public UILabel[] Options;

	// Token: 0x04002105 RID: 8453
	public Vector3[] Origins;

	// Token: 0x04002106 RID: 8454
	public UISprite Background;

	// Token: 0x04002107 RID: 8455
	public UISprite Ground;

	// Token: 0x04002108 RID: 8456
	public Camera MainCamera;

	// Token: 0x04002109 RID: 8457
	public UILabel Subtitle;

	// Token: 0x0400210A RID: 8458
	public GameObject SNAP;

	// Token: 0x0400210B RID: 8459
	public AudioClip Slam;

	// Token: 0x0400210C RID: 8460
	public bool Headmaster;

	// Token: 0x0400210D RID: 8461
	public bool Confessed;

	// Token: 0x0400210E RID: 8462
	public bool Arrested;

	// Token: 0x0400210F RID: 8463
	public bool Exposed;

	// Token: 0x04002110 RID: 8464
	public bool Noticed = true;

	// Token: 0x04002111 RID: 8465
	public bool Freeze;

	// Token: 0x04002112 RID: 8466
	public float VibrationTimer;

	// Token: 0x04002113 RID: 8467
	public float AudioTimer;

	// Token: 0x04002114 RID: 8468
	public float Timer;

	// Token: 0x04002115 RID: 8469
	public int Phase = 1;

	// Token: 0x04002116 RID: 8470
	public int LetterID;

	// Token: 0x04002117 RID: 8471
	public int ShakeID;

	// Token: 0x04002118 RID: 8472
	public int GrowID;

	// Token: 0x04002119 RID: 8473
	public int StopID;

	// Token: 0x0400211A RID: 8474
	public int ID;
}
