using System;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class JukeboxScript : MonoBehaviour
{
	// Token: 0x06001D4A RID: 7498 RVA: 0x0011183C File Offset: 0x0010FC3C
	public void Start()
	{
		if (this.BGM == 0)
		{
			this.BGM = UnityEngine.Random.Range(0, 8);
		}
		else
		{
			this.BGM++;
			if (this.BGM > 8)
			{
				this.BGM = 1;
			}
		}
		if (this.BGM == 1)
		{
			this.FullSanities = this.OriginalFull;
			this.HalfSanities = this.OriginalHalf;
			this.NoSanities = this.OriginalNo;
		}
		else if (this.BGM == 2)
		{
			this.FullSanities = this.AlternateFull;
			this.HalfSanities = this.AlternateHalf;
			this.NoSanities = this.AlternateNo;
		}
		else if (this.BGM == 3)
		{
			this.FullSanities = this.ThirdFull;
			this.HalfSanities = this.ThirdHalf;
			this.NoSanities = this.ThirdNo;
		}
		else if (this.BGM == 4)
		{
			this.FullSanities = this.FourthFull;
			this.HalfSanities = this.FourthHalf;
			this.NoSanities = this.FourthNo;
		}
		else if (this.BGM == 5)
		{
			this.FullSanities = this.FifthFull;
			this.HalfSanities = this.FifthHalf;
			this.NoSanities = this.FifthNo;
		}
		else if (this.BGM == 6)
		{
			this.FullSanities = this.SixthFull;
			this.HalfSanities = this.SixthHalf;
			this.NoSanities = this.SixthNo;
		}
		else if (this.BGM == 7)
		{
			this.FullSanities = this.SeventhFull;
			this.HalfSanities = this.SeventhHalf;
			this.NoSanities = this.SeventhNo;
		}
		else if (this.BGM == 8)
		{
			this.FullSanities = this.EighthFull;
			this.HalfSanities = this.EighthHalf;
			this.NoSanities = this.EighthNo;
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		int num;
		if (SchoolAtmosphere.Type == SchoolAtmosphereType.High)
		{
			num = 3;
		}
		else if (SchoolAtmosphere.Type == SchoolAtmosphereType.Medium)
		{
			num = 2;
		}
		else
		{
			num = 1;
		}
		this.FullSanity.clip = this.FullSanities[num];
		this.HalfSanity.clip = this.HalfSanities[num];
		this.NoSanity.clip = this.NoSanities[num];
		this.Volume = 0.25f;
		this.FullSanity.volume = 0f;
		this.Hitman.time = 26f;
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x00111AD0 File Offset: 0x0010FED0
	private void Update()
	{
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.EasterEggMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
		{
			this.StartStopMusic();
		}
		if (!this.Egg)
		{
			if (!this.Yandere.Police.Clock.SchoolBell.isPlaying && !this.Yandere.StudentManager.MemorialScene.enabled)
			{
				if (!this.StartMusic)
				{
					this.FullSanity.Play();
					this.HalfSanity.Play();
					this.NoSanity.Play();
					this.StartMusic = true;
				}
				if (this.Yandere.Sanity >= 66.6666641f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
				}
				else if (this.Yandere.Sanity >= 33.3333321f)
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
				}
				else
				{
					this.FullSanity.volume = Mathf.MoveTowards(this.FullSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.HalfSanity.volume = Mathf.MoveTowards(this.HalfSanity.volume, 0f, 0.0166666675f * this.FadeSpeed);
					this.NoSanity.volume = Mathf.MoveTowards(this.NoSanity.volume, this.Volume * this.Dip - this.ClubDip, 0.0166666675f * this.FadeSpeed);
				}
			}
		}
		else
		{
			this.AttackOnTitan.volume = Mathf.MoveTowards(this.AttackOnTitan.volume, this.Volume * this.Dip, 0.166666672f);
			this.Megalovania.volume = Mathf.MoveTowards(this.Megalovania.volume, this.Volume * this.Dip, 0.166666672f);
			this.MissionMode.volume = Mathf.MoveTowards(this.MissionMode.volume, this.Volume * this.Dip, 0.166666672f);
			this.Skeletons.volume = Mathf.MoveTowards(this.Skeletons.volume, this.Volume * this.Dip, 0.166666672f);
			this.Vaporwave.volume = Mathf.MoveTowards(this.Vaporwave.volume, this.Volume * this.Dip, 0.166666672f);
			this.AzurLane.volume = Mathf.MoveTowards(this.AzurLane.volume, this.Volume * this.Dip, 0.166666672f);
			this.LifeNote.volume = Mathf.MoveTowards(this.LifeNote.volume, this.Volume * this.Dip, 0.166666672f);
			this.Berserk.volume = Mathf.MoveTowards(this.Berserk.volume, this.Volume * this.Dip, 0.166666672f);
			this.Metroid.volume = Mathf.MoveTowards(this.Metroid.volume, this.Volume * this.Dip, 0.166666672f);
			this.Nuclear.volume = Mathf.MoveTowards(this.Nuclear.volume, this.Volume * this.Dip, 0.166666672f);
			this.Slender.volume = Mathf.MoveTowards(this.Slender.volume, this.Volume * this.Dip, 0.166666672f);
			this.Sukeban.volume = Mathf.MoveTowards(this.Sukeban.volume, this.Volume * this.Dip, 0.166666672f);
			this.Custom.volume = Mathf.MoveTowards(this.Custom.volume, this.Volume * this.Dip, 0.166666672f);
			this.Hatred.volume = Mathf.MoveTowards(this.Hatred.volume, this.Volume * this.Dip, 0.166666672f);
			this.Hitman.volume = Mathf.MoveTowards(this.Hitman.volume, this.Volume * this.Dip, 0.166666672f);
			this.Touhou.volume = Mathf.MoveTowards(this.Touhou.volume, this.Volume * this.Dip, 0.166666672f);
			this.Falcon.volume = Mathf.MoveTowards(this.Falcon.volume, this.Volume * this.Dip, 0.166666672f);
			this.Miyuki.volume = Mathf.MoveTowards(this.Miyuki.volume, this.Volume * this.Dip, 0.166666672f);
			this.Demon.volume = Mathf.MoveTowards(this.Demon.volume, this.Volume * this.Dip, 0.166666672f);
			this.Ebola.volume = Mathf.MoveTowards(this.Ebola.volume, this.Volume * this.Dip, 0.166666672f);
			this.Ninja.volume = Mathf.MoveTowards(this.Ninja.volume, this.Volume * this.Dip, 0.166666672f);
			this.Punch.volume = Mathf.MoveTowards(this.Punch.volume, this.Volume * this.Dip, 0.166666672f);
			this.Galo.volume = Mathf.MoveTowards(this.Galo.volume, this.Volume * this.Dip, 0.166666672f);
			this.Jojo.volume = Mathf.MoveTowards(this.Jojo.volume, this.Volume * this.Dip, 0.166666672f);
			this.Lied.volume = Mathf.MoveTowards(this.Lied.volume, this.Volume * this.Dip, 0.166666672f);
			this.Nier.volume = Mathf.MoveTowards(this.Nier.volume, this.Volume * this.Dip, 0.166666672f);
			this.Sith.volume = Mathf.MoveTowards(this.Sith.volume, this.Volume * this.Dip, 0.166666672f);
			this.DK.volume = Mathf.MoveTowards(this.DK.volume, this.Volume * this.Dip, 0.166666672f);
			this.Horror.volume = Mathf.MoveTowards(this.Horror.volume, this.Volume * this.Dip, 0.166666672f);
		}
		if (!this.Yandere.PauseScreen.Show && !this.Yandere.Noticed && this.Yandere.CanMove && this.Yandere.EasterEggMenu.activeInHierarchy && !this.Egg)
		{
			if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				this.Egg = true;
				this.KillVolume();
				this.AttackOnTitan.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.P))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nuclear.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.H))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hatred.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.B))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sukeban.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
			{
				this.Egg = true;
				this.KillVolume();
				this.Slender.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.G))
			{
				this.Egg = true;
				this.KillVolume();
				this.Galo.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.L))
			{
				this.Egg = true;
				this.KillVolume();
				this.Hitman.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				this.Egg = true;
				this.KillVolume();
				this.Skeletons.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.K))
			{
				this.Egg = true;
				this.KillVolume();
				this.DK.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				this.Egg = true;
				this.KillVolume();
				this.Touhou.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F))
			{
				this.Egg = true;
				this.KillVolume();
				this.Falcon.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				this.Egg = true;
				this.KillVolume();
				this.Punch.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.U))
			{
				this.Egg = true;
				this.KillVolume();
				this.Megalovania.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				this.Egg = true;
				this.KillVolume();
				this.Metroid.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Y))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ninja.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.F5) || Input.GetKeyDown(KeyCode.W))
			{
				this.Egg = true;
				this.KillVolume();
				this.Ebola.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				this.Egg = true;
				this.KillVolume();
				this.Demon.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				this.Egg = true;
				this.KillVolume();
				this.Sith.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F2))
			{
				this.Egg = true;
				this.KillVolume();
				this.Horror.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F3))
			{
				this.Egg = true;
				this.KillVolume();
				this.LifeNote.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F6))
			{
				this.Egg = true;
				this.KillVolume();
				this.Lied.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F7))
			{
				this.Egg = true;
				this.KillVolume();
				this.Berserk.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.F8))
			{
				this.Egg = true;
				this.KillVolume();
				this.Nier.enabled = true;
			}
			else if (Input.GetKeyDown(KeyCode.V))
			{
				this.Egg = true;
				this.KillVolume();
				this.Vaporwave.enabled = true;
			}
		}
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x00112750 File Offset: 0x00110B50
	public void StartStopMusic()
	{
		if (this.Custom.isPlaying)
		{
			this.Egg = false;
			this.Custom.Stop();
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
		}
		else if (this.Volume == 0f)
		{
			this.FadeSpeed = 1f;
			this.StartMusic = false;
			this.Volume = this.LastVolume;
			this.Start();
		}
		else
		{
			this.LastVolume = this.Volume;
			this.FadeSpeed = 10f;
			this.Volume = 0f;
		}
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00112803 File Offset: 0x00110C03
	public void Shipgirl()
	{
		this.Egg = true;
		this.KillVolume();
		this.AzurLane.enabled = true;
	}

	// Token: 0x06001D4E RID: 7502 RVA: 0x0011281E File Offset: 0x00110C1E
	public void MiyukiMusic()
	{
		this.Egg = true;
		this.KillVolume();
		this.Miyuki.enabled = true;
	}

	// Token: 0x06001D4F RID: 7503 RVA: 0x00112839 File Offset: 0x00110C39
	public void KillVolume()
	{
		this.FullSanity.volume = 0f;
		this.HalfSanity.volume = 0f;
		this.NoSanity.volume = 0f;
		this.Volume = 0.5f;
	}

	// Token: 0x06001D50 RID: 7504 RVA: 0x00112878 File Offset: 0x00110C78
	public void GameOver()
	{
		this.AttackOnTitan.Stop();
		this.Megalovania.Stop();
		this.MissionMode.Stop();
		this.Skeletons.Stop();
		this.Vaporwave.Stop();
		this.AzurLane.Stop();
		this.LifeNote.Stop();
		this.Berserk.Stop();
		this.Metroid.Stop();
		this.Nuclear.Stop();
		this.Sukeban.Stop();
		this.Custom.Stop();
		this.Slender.Stop();
		this.Hatred.Stop();
		this.Hitman.Stop();
		this.Horror.Stop();
		this.Touhou.Stop();
		this.Falcon.Stop();
		this.Miyuki.Stop();
		this.Ebola.Stop();
		this.Punch.Stop();
		this.Ninja.Stop();
		this.Jojo.Stop();
		this.Galo.Stop();
		this.Lied.Stop();
		this.Nier.Stop();
		this.Sith.Stop();
		this.DK.Stop();
		this.Confession.Stop();
		this.FullSanity.Stop();
		this.HalfSanity.Stop();
		this.NoSanity.Stop();
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x001129E5 File Offset: 0x00110DE5
	public void PlayJojo()
	{
		this.Egg = true;
		this.KillVolume();
		this.Jojo.enabled = true;
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x00112A00 File Offset: 0x00110E00
	public void PlayCustom()
	{
		this.Egg = true;
		this.KillVolume();
		this.Custom.enabled = true;
		this.Custom.Play();
	}

	// Token: 0x040023FA RID: 9210
	public YandereScript Yandere;

	// Token: 0x040023FB RID: 9211
	public AudioSource SFX;

	// Token: 0x040023FC RID: 9212
	public AudioSource AttackOnTitan;

	// Token: 0x040023FD RID: 9213
	public AudioSource Megalovania;

	// Token: 0x040023FE RID: 9214
	public AudioSource MissionMode;

	// Token: 0x040023FF RID: 9215
	public AudioSource Skeletons;

	// Token: 0x04002400 RID: 9216
	public AudioSource Vaporwave;

	// Token: 0x04002401 RID: 9217
	public AudioSource AzurLane;

	// Token: 0x04002402 RID: 9218
	public AudioSource LifeNote;

	// Token: 0x04002403 RID: 9219
	public AudioSource Berserk;

	// Token: 0x04002404 RID: 9220
	public AudioSource Metroid;

	// Token: 0x04002405 RID: 9221
	public AudioSource Nuclear;

	// Token: 0x04002406 RID: 9222
	public AudioSource Slender;

	// Token: 0x04002407 RID: 9223
	public AudioSource Sukeban;

	// Token: 0x04002408 RID: 9224
	public AudioSource Custom;

	// Token: 0x04002409 RID: 9225
	public AudioSource Hatred;

	// Token: 0x0400240A RID: 9226
	public AudioSource Hitman;

	// Token: 0x0400240B RID: 9227
	public AudioSource Horror;

	// Token: 0x0400240C RID: 9228
	public AudioSource Touhou;

	// Token: 0x0400240D RID: 9229
	public AudioSource Falcon;

	// Token: 0x0400240E RID: 9230
	public AudioSource Miyuki;

	// Token: 0x0400240F RID: 9231
	public AudioSource Ebola;

	// Token: 0x04002410 RID: 9232
	public AudioSource Demon;

	// Token: 0x04002411 RID: 9233
	public AudioSource Ninja;

	// Token: 0x04002412 RID: 9234
	public AudioSource Punch;

	// Token: 0x04002413 RID: 9235
	public AudioSource Galo;

	// Token: 0x04002414 RID: 9236
	public AudioSource Jojo;

	// Token: 0x04002415 RID: 9237
	public AudioSource Lied;

	// Token: 0x04002416 RID: 9238
	public AudioSource Nier;

	// Token: 0x04002417 RID: 9239
	public AudioSource Sith;

	// Token: 0x04002418 RID: 9240
	public AudioSource DK;

	// Token: 0x04002419 RID: 9241
	public AudioSource Confession;

	// Token: 0x0400241A RID: 9242
	public AudioSource FullSanity;

	// Token: 0x0400241B RID: 9243
	public AudioSource HalfSanity;

	// Token: 0x0400241C RID: 9244
	public AudioSource NoSanity;

	// Token: 0x0400241D RID: 9245
	public AudioSource Chase;

	// Token: 0x0400241E RID: 9246
	public float LastVolume;

	// Token: 0x0400241F RID: 9247
	public float FadeSpeed;

	// Token: 0x04002420 RID: 9248
	public float ClubDip;

	// Token: 0x04002421 RID: 9249
	public float Volume;

	// Token: 0x04002422 RID: 9250
	public int Track;

	// Token: 0x04002423 RID: 9251
	public int BGM;

	// Token: 0x04002424 RID: 9252
	public float Dip = 1f;

	// Token: 0x04002425 RID: 9253
	public bool StartMusic;

	// Token: 0x04002426 RID: 9254
	public bool Egg;

	// Token: 0x04002427 RID: 9255
	public AudioClip[] FullSanities;

	// Token: 0x04002428 RID: 9256
	public AudioClip[] HalfSanities;

	// Token: 0x04002429 RID: 9257
	public AudioClip[] NoSanities;

	// Token: 0x0400242A RID: 9258
	public AudioClip[] OriginalFull;

	// Token: 0x0400242B RID: 9259
	public AudioClip[] OriginalHalf;

	// Token: 0x0400242C RID: 9260
	public AudioClip[] OriginalNo;

	// Token: 0x0400242D RID: 9261
	public AudioClip[] AlternateFull;

	// Token: 0x0400242E RID: 9262
	public AudioClip[] AlternateHalf;

	// Token: 0x0400242F RID: 9263
	public AudioClip[] AlternateNo;

	// Token: 0x04002430 RID: 9264
	public AudioClip[] ThirdFull;

	// Token: 0x04002431 RID: 9265
	public AudioClip[] ThirdHalf;

	// Token: 0x04002432 RID: 9266
	public AudioClip[] ThirdNo;

	// Token: 0x04002433 RID: 9267
	public AudioClip[] FourthFull;

	// Token: 0x04002434 RID: 9268
	public AudioClip[] FourthHalf;

	// Token: 0x04002435 RID: 9269
	public AudioClip[] FourthNo;

	// Token: 0x04002436 RID: 9270
	public AudioClip[] FifthFull;

	// Token: 0x04002437 RID: 9271
	public AudioClip[] FifthHalf;

	// Token: 0x04002438 RID: 9272
	public AudioClip[] FifthNo;

	// Token: 0x04002439 RID: 9273
	public AudioClip[] SixthFull;

	// Token: 0x0400243A RID: 9274
	public AudioClip[] SixthHalf;

	// Token: 0x0400243B RID: 9275
	public AudioClip[] SixthNo;

	// Token: 0x0400243C RID: 9276
	public AudioClip[] SeventhFull;

	// Token: 0x0400243D RID: 9277
	public AudioClip[] SeventhHalf;

	// Token: 0x0400243E RID: 9278
	public AudioClip[] SeventhNo;

	// Token: 0x0400243F RID: 9279
	public AudioClip[] EighthFull;

	// Token: 0x04002440 RID: 9280
	public AudioClip[] EighthHalf;

	// Token: 0x04002441 RID: 9281
	public AudioClip[] EighthNo;
}
