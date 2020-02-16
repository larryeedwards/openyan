using System;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class DayNightController : MonoBehaviour
{
	// Token: 0x0600189A RID: 6298 RVA: 0x000DAC0C File Offset: 0x000D900C
	private void Initialize()
	{
		this.quarterDay = this.dayCycleLength * 0.25f;
		this.dawnTime = 0f;
		this.dayTime = this.dawnTime + this.quarterDay;
		this.duskTime = this.dayTime + this.quarterDay;
		this.nightTime = this.duskTime + this.quarterDay;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			this.lightIntensity = component.intensity;
		}
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000DAC90 File Offset: 0x000D9090
	private void Reset()
	{
		this.dayCycleLength = 120f;
		this.hoursPerDay = 24f;
		this.dawnTimeOffset = 3f;
		this.fullDark = new Color(0.1254902f, 0.109803922f, 0.180392161f);
		this.fullLight = new Color(0.992156863f, 0.972549f, 0.8745098f);
		this.dawnDuskFog = new Color(0.521568656f, 0.4862745f, 0.4f);
		this.dayFog = new Color(0.7058824f, 0.8156863f, 0.819607854f);
		this.nightFog = new Color(0.0470588244f, 0.05882353f, 0.356862754f);
		Skybox[] array = Resources.FindObjectsOfTypeAll<Skybox>();
		foreach (Skybox skybox in array)
		{
			if (skybox.name == "DawnDusk Skybox")
			{
				this.dawnDuskSkybox = skybox.material;
			}
			else if (skybox.name == "StarryNight Skybox")
			{
				this.nightSkybox = skybox.material;
			}
			else if (skybox.name == "Sunny2 Skybox")
			{
				this.daySkybox = skybox.material;
			}
		}
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000DADCD File Offset: 0x000D91CD
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000DADD8 File Offset: 0x000D91D8
	private void Update()
	{
		if (this.currentCycleTime > this.nightTime && this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			this.SetNight();
		}
		else if (this.currentCycleTime > this.duskTime && this.currentPhase == DayNightController.DayPhase.Day)
		{
			this.SetDusk();
		}
		else if (this.currentCycleTime > this.dayTime && this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			this.SetDay();
		}
		else if (this.currentCycleTime > this.dawnTime && this.currentCycleTime < this.dayTime && this.currentPhase == DayNightController.DayPhase.Night)
		{
			this.SetDawn();
		}
		this.UpdateWorldTime();
		this.UpdateDaylight();
		this.UpdateFog();
		this.currentCycleTime += Time.deltaTime;
		this.currentCycleTime %= this.dayCycleLength;
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000DAEC8 File Offset: 0x000D92C8
	public void SetDawn()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = true;
		}
		this.currentPhase = DayNightController.DayPhase.Dawn;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x000DAF04 File Offset: 0x000D9304
	public void SetDay()
	{
		RenderSettings.skybox = this.daySkybox;
		RenderSettings.ambientLight = this.fullLight;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.intensity = this.lightIntensity;
		}
		this.currentPhase = DayNightController.DayPhase.Day;
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000DAF4D File Offset: 0x000D934D
	public void SetDusk()
	{
		RenderSettings.skybox = this.dawnDuskSkybox;
		this.currentPhase = DayNightController.DayPhase.Dusk;
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000DAF64 File Offset: 0x000D9364
	public void SetNight()
	{
		RenderSettings.skybox = this.nightSkybox;
		RenderSettings.ambientLight = this.fullDark;
		Light component = base.GetComponent<Light>();
		if (component != null)
		{
			component.enabled = false;
		}
		this.currentPhase = DayNightController.DayPhase.Night;
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000DAFA8 File Offset: 0x000D93A8
	private void UpdateDaylight()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullDark, this.fullLight, num / this.quarterDay);
			Light component = base.GetComponent<Light>();
			if (component != null)
			{
				component.intensity = this.lightIntensity * (num / this.quarterDay);
			}
		}
		else if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num2 = this.currentCycleTime - this.duskTime;
			RenderSettings.ambientLight = Color.Lerp(this.fullLight, this.fullDark, num2 / this.quarterDay);
			Light component2 = base.GetComponent<Light>();
			if (component2 != null)
			{
				component2.intensity = this.lightIntensity * ((this.quarterDay - num2) / this.quarterDay);
			}
		}
		base.transform.Rotate(Vector3.up * (Time.deltaTime / this.dayCycleLength * 360f), Space.Self);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000DB0AC File Offset: 0x000D94AC
	private void UpdateFog()
	{
		if (this.currentPhase == DayNightController.DayPhase.Dawn)
		{
			float num = this.currentCycleTime - this.dawnTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.dayFog, num / this.quarterDay);
		}
		else if (this.currentPhase == DayNightController.DayPhase.Day)
		{
			float num2 = this.currentCycleTime - this.dayTime;
			RenderSettings.fogColor = Color.Lerp(this.dayFog, this.dawnDuskFog, num2 / this.quarterDay);
		}
		else if (this.currentPhase == DayNightController.DayPhase.Dusk)
		{
			float num3 = this.currentCycleTime - this.duskTime;
			RenderSettings.fogColor = Color.Lerp(this.dawnDuskFog, this.nightFog, num3 / this.quarterDay);
		}
		else if (this.currentPhase == DayNightController.DayPhase.Night)
		{
			float num4 = this.currentCycleTime - this.nightTime;
			RenderSettings.fogColor = Color.Lerp(this.nightFog, this.dawnDuskFog, num4 / this.quarterDay);
		}
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000DB1A7 File Offset: 0x000D95A7
	private void UpdateWorldTime()
	{
		this.worldTimeHour = (int)((Mathf.Ceil(this.currentCycleTime / this.dayCycleLength * this.hoursPerDay) + this.dawnTimeOffset) % this.hoursPerDay) + 1;
	}

	// Token: 0x04001B96 RID: 7062
	public float dayCycleLength;

	// Token: 0x04001B97 RID: 7063
	public float currentCycleTime;

	// Token: 0x04001B98 RID: 7064
	public DayNightController.DayPhase currentPhase;

	// Token: 0x04001B99 RID: 7065
	public float hoursPerDay;

	// Token: 0x04001B9A RID: 7066
	public float dawnTimeOffset;

	// Token: 0x04001B9B RID: 7067
	public int worldTimeHour;

	// Token: 0x04001B9C RID: 7068
	public Color fullLight;

	// Token: 0x04001B9D RID: 7069
	public Color fullDark;

	// Token: 0x04001B9E RID: 7070
	public Material dawnDuskSkybox;

	// Token: 0x04001B9F RID: 7071
	public Color dawnDuskFog;

	// Token: 0x04001BA0 RID: 7072
	public Material daySkybox;

	// Token: 0x04001BA1 RID: 7073
	public Color dayFog;

	// Token: 0x04001BA2 RID: 7074
	public Material nightSkybox;

	// Token: 0x04001BA3 RID: 7075
	public Color nightFog;

	// Token: 0x04001BA4 RID: 7076
	private float dawnTime;

	// Token: 0x04001BA5 RID: 7077
	private float dayTime;

	// Token: 0x04001BA6 RID: 7078
	private float duskTime;

	// Token: 0x04001BA7 RID: 7079
	private float nightTime;

	// Token: 0x04001BA8 RID: 7080
	private float quarterDay;

	// Token: 0x04001BA9 RID: 7081
	private float lightIntensity;

	// Token: 0x02000389 RID: 905
	public enum DayPhase
	{
		// Token: 0x04001BAB RID: 7083
		Night,
		// Token: 0x04001BAC RID: 7084
		Dawn,
		// Token: 0x04001BAD RID: 7085
		Day,
		// Token: 0x04001BAE RID: 7086
		Dusk
	}
}
