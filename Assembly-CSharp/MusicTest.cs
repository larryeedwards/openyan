using System;
using UnityEngine;

// Token: 0x0200046D RID: 1133
public class MusicTest : MonoBehaviour
{
	// Token: 0x06001DD2 RID: 7634 RVA: 0x0011C4E0 File Offset: 0x0011A8E0
	private void Start()
	{
		int num = this.freqData.Length;
		int num2 = 0;
		for (int i = 0; i < this.freqData.Length; i++)
		{
			num /= 2;
			if (num == 0)
			{
				break;
			}
			num2++;
		}
		this.band = new float[num2 + 1];
		this.g = new GameObject[num2 + 1];
		for (int j = 0; j < this.band.Length; j++)
		{
			this.band[j] = 0f;
			this.g[j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			this.g[j].transform.position = new Vector3((float)j, 0f, 0f);
		}
		base.InvokeRepeating("check", 0f, 0.0333333351f);
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x0011C5AC File Offset: 0x0011A9AC
	private void check()
	{
		base.GetComponent<AudioSource>().GetSpectrumData(this.freqData, 0, FFTWindow.Rectangular);
		int num = 0;
		int num2 = 2;
		for (int i = 0; i < this.freqData.Length; i++)
		{
			float num3 = this.freqData[i];
			float num4 = this.band[num];
			this.band[num] = ((num3 <= num4) ? num4 : num3);
			if (i > num2 - 3)
			{
				num++;
				num2 *= 2;
				Transform transform = this.g[num].transform;
				transform.position = new Vector3(transform.position.x, this.band[num] * 32f, transform.position.z);
				this.band[num] = 0f;
			}
		}
	}

	// Token: 0x04002596 RID: 9622
	public float[] freqData;

	// Token: 0x04002597 RID: 9623
	public AudioSource MainSong;

	// Token: 0x04002598 RID: 9624
	public float[] band;

	// Token: 0x04002599 RID: 9625
	public GameObject[] g;
}
