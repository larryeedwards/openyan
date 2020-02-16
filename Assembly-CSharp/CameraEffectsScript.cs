using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000354 RID: 852
public class CameraEffectsScript : MonoBehaviour
{
	// Token: 0x060017A9 RID: 6057 RVA: 0x000BC6FC File Offset: 0x000BAAFC
	private void Start()
	{
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 0f);
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 0f);
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x000BC7A8 File Offset: 0x000BABA8
	private void Update()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
			if (this.VibrationTimer == 0f)
			{
				GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
				this.VibrationCheck = false;
			}
		}
		if (this.Streaks.color.a > 0f)
		{
			this.AlarmBloom.bloomIntensity -= Time.deltaTime;
			this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, this.Streaks.color.a - Time.deltaTime);
			if (this.Streaks.color.a <= 0f)
			{
				this.AlarmBloom.enabled = false;
			}
		}
		if (this.MurderStreaks.color.a > 0f)
		{
			this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, this.MurderStreaks.color.a - Time.deltaTime);
		}
		this.EffectStrength = 1f - this.Yandere.Sanity * 0.01f;
		this.Vignette.intensity = Mathf.Lerp(this.Vignette.intensity, this.EffectStrength * 5f, Time.deltaTime);
		this.Vignette.blur = Mathf.Lerp(this.Vignette.blur, this.EffectStrength, Time.deltaTime);
		this.Vignette.chromaticAberration = Mathf.Lerp(this.Vignette.chromaticAberration, this.EffectStrength * 5f, Time.deltaTime);
	}

	// Token: 0x060017AB RID: 6059 RVA: 0x000BC9E4 File Offset: 0x000BADE4
	public void Alarm()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.AlarmBloom.bloomIntensity = 1f;
		this.Streaks.color = new Color(this.Streaks.color.r, this.Streaks.color.g, this.Streaks.color.b, 1f);
		this.AlarmBloom.enabled = true;
		this.Yandere.Jukebox.SFX.PlayOneShot(this.Noticed);
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000BCA98 File Offset: 0x000BAE98
	public void MurderWitnessed()
	{
		GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
		this.VibrationCheck = true;
		this.VibrationTimer = 0.1f;
		this.MurderStreaks.color = new Color(this.MurderStreaks.color.r, this.MurderStreaks.color.g, this.MurderStreaks.color.b, 1f);
		this.Yandere.Jukebox.SFX.PlayOneShot((!this.Yandere.Noticed) ? this.MurderNoticed : this.SenpaiNoticed);
	}

	// Token: 0x060017AD RID: 6061 RVA: 0x000BCB4B File Offset: 0x000BAF4B
	public void DisableCamera()
	{
		if (!this.OneCamera)
		{
			this.OneCamera = true;
		}
		else
		{
			this.OneCamera = false;
		}
	}

	// Token: 0x0400179E RID: 6046
	public YandereScript Yandere;

	// Token: 0x0400179F RID: 6047
	public Vignetting Vignette;

	// Token: 0x040017A0 RID: 6048
	public UITexture MurderStreaks;

	// Token: 0x040017A1 RID: 6049
	public UITexture Streaks;

	// Token: 0x040017A2 RID: 6050
	public Bloom AlarmBloom;

	// Token: 0x040017A3 RID: 6051
	public float EffectStrength;

	// Token: 0x040017A4 RID: 6052
	public float VibrationTimer;

	// Token: 0x040017A5 RID: 6053
	public Bloom QualityBloom;

	// Token: 0x040017A6 RID: 6054
	public Vignetting QualityVignetting;

	// Token: 0x040017A7 RID: 6055
	public AntialiasingAsPostEffect QualityAntialiasingAsPostEffect;

	// Token: 0x040017A8 RID: 6056
	public bool VibrationCheck;

	// Token: 0x040017A9 RID: 6057
	public bool OneCamera;

	// Token: 0x040017AA RID: 6058
	public AudioClip MurderNoticed;

	// Token: 0x040017AB RID: 6059
	public AudioClip SenpaiNoticed;

	// Token: 0x040017AC RID: 6060
	public AudioClip Noticed;
}
