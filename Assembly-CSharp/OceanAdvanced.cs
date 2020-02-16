using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class OceanAdvanced : MonoBehaviour
{
	// Token: 0x06000ABF RID: 2751 RVA: 0x0005315C File Offset: 0x0005155C
	private void Awake()
	{
		Vector4[] array = new Vector4[5];
		Vector4[] array2 = new Vector4[5];
		for (int i = 0; i < 5; i++)
		{
			array[i] = new Vector4(OceanAdvanced.waves[i].frequency, OceanAdvanced.waves[i].amplitude, OceanAdvanced.waves[i].phase, OceanAdvanced.waves[i].sharpness);
			array2[i] = new Vector4(OceanAdvanced.waves[i].direction.x, OceanAdvanced.waves[i].direction.y, 0f, 0f);
		}
		this.ocean.SetVectorArray("waves_p", array);
		this.ocean.SetVectorArray("waves_d", array2);
		for (int j = 0; j < 64; j++)
		{
			this.interactions[j].w = 500f;
		}
		this.ocean.SetVectorArray("interactions", this.interactions);
		this.ocean.SetVector("world_light_dir", -this.sun.transform.forward);
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x000532A0 File Offset: 0x000516A0
	private void FixedUpdate()
	{
		this.ocean.SetVector("world_light_dir", -this.sun.transform.forward);
		this.ocean.SetVector("sun_color", new Vector4(this.sun.color.r, this.sun.color.g, this.sun.color.b, 0f));
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0005332C File Offset: 0x0005172C
	public void RegisterInteraction(Vector3 pos, float strength)
	{
		this.interactions[this.interaction_id].x = pos.x;
		this.interactions[this.interaction_id].y = pos.z;
		this.interactions[this.interaction_id].z = strength;
		this.interactions[this.interaction_id].w = Time.time;
		this.ocean.SetVectorArray("interactions", this.interactions);
		this.interaction_id = (this.interaction_id + 1) % 64;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x000533CC File Offset: 0x000517CC
	public static float GetWaterHeight(Vector3 p)
	{
		float num = 0f;
		for (int i = 0; i < 5; i++)
		{
			num += OceanAdvanced.waves[i].amplitude * Mathf.Sin(Vector2.Dot(OceanAdvanced.waves[i].direction, new Vector2(p.x, p.z)) * OceanAdvanced.waves[i].frequency + Time.time * OceanAdvanced.waves[i].phase);
		}
		return num - 1f;
	}

	// Token: 0x040007AC RID: 1964
	public Material ocean;

	// Token: 0x040007AD RID: 1965
	public Light sun;

	// Token: 0x040007AE RID: 1966
	private int interaction_id;

	// Token: 0x040007AF RID: 1967
	private Vector4[] interactions = new Vector4[64];

	// Token: 0x040007B0 RID: 1968
	private const int NB_WAVE = 5;

	// Token: 0x040007B1 RID: 1969
	private const int NB_INTERACTIONS = 64;

	// Token: 0x040007B2 RID: 1970
	private static OceanAdvanced.Wave[] waves = new OceanAdvanced.Wave[]
	{
		new OceanAdvanced.Wave(99f, 1f, 1.4f, 0.9f, new Vector2(1f, 0.2f)),
		new OceanAdvanced.Wave(60f, 1.2f, 0.8f, 0.5f, new Vector2(1f, 3f)),
		new OceanAdvanced.Wave(20f, 3.5f, 0.4f, 0.8f, new Vector2(2f, 4f)),
		new OceanAdvanced.Wave(30f, 2f, 0.4f, 0.4f, new Vector2(-1f, 0f)),
		new OceanAdvanced.Wave(10f, 3f, 0.05f, 0.9f, new Vector2(-1f, 1.2f))
	};

	// Token: 0x02000137 RID: 311
	private class Wave
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00053544 File Offset: 0x00051944
		public Wave(float waveLength, float speed, float amplitude, float sharpness, Vector2 direction)
		{
			this.waveLength = waveLength;
			this.speed = speed;
			this.amplitude = amplitude;
			this.sharpness = sharpness;
			this.direction = direction.normalized;
			this.frequency = 6.28318548f / waveLength;
			this.phase = this.frequency * speed;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0005359C File Offset: 0x0005199C
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000535A4 File Offset: 0x000519A4
		public float waveLength { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000535AD File Offset: 0x000519AD
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000535B5 File Offset: 0x000519B5
		public float speed { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000535BE File Offset: 0x000519BE
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000535C6 File Offset: 0x000519C6
		public float amplitude { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000535CF File Offset: 0x000519CF
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x000535D7 File Offset: 0x000519D7
		public float sharpness { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000535E0 File Offset: 0x000519E0
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x000535E8 File Offset: 0x000519E8
		public float frequency { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000535F1 File Offset: 0x000519F1
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x000535F9 File Offset: 0x000519F9
		public float phase { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00053602 File Offset: 0x00051A02
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0005360A File Offset: 0x00051A0A
		public Vector2 direction { get; private set; }
	}
}
