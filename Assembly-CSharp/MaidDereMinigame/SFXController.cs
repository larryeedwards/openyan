using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000155 RID: 341
	public class SFXController : MonoBehaviour
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0005673D File Offset: 0x00054B3D
		public static SFXController Instance
		{
			get
			{
				if (SFXController.instance == null)
				{
					SFXController.instance = UnityEngine.Object.FindObjectOfType<SFXController>();
				}
				return SFXController.instance;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0005675E File Offset: 0x00054B5E
		private void Awake()
		{
			if (SFXController.Instance != this)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0005678C File Offset: 0x00054B8C
		public static void PlaySound(SFXController.Sounds sound)
		{
			SoundEmitter emitter = SFXController.Instance.GetEmitter(sound);
			AudioSource source = emitter.GetSource();
			if (!source.isPlaying || emitter.interupt)
			{
				source.clip = SFXController.Instance.GetRandomClip(emitter);
				source.Play();
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000567DC File Offset: 0x00054BDC
		private SoundEmitter GetEmitter(SFXController.Sounds sound)
		{
			foreach (SoundEmitter soundEmitter in this.emitters)
			{
				if (soundEmitter.sound == sound)
				{
					return soundEmitter;
				}
			}
			Debug.Log(string.Format("There is no sound emitter created for {0}", sound), this);
			return null;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0005685C File Offset: 0x00054C5C
		private AudioClip GetRandomClip(SoundEmitter emitter)
		{
			int index = UnityEngine.Random.Range(0, emitter.clips.Count);
			return emitter.clips[index];
		}

		// Token: 0x04000861 RID: 2145
		private static SFXController instance;

		// Token: 0x04000862 RID: 2146
		[Reorderable]
		public SoundEmitters emitters;

		// Token: 0x02000156 RID: 342
		public enum Sounds
		{
			// Token: 0x04000864 RID: 2148
			Countdown,
			// Token: 0x04000865 RID: 2149
			MenuBack,
			// Token: 0x04000866 RID: 2150
			MenuConfirm,
			// Token: 0x04000867 RID: 2151
			ClockTick,
			// Token: 0x04000868 RID: 2152
			DoorBell,
			// Token: 0x04000869 RID: 2153
			GameFail,
			// Token: 0x0400086A RID: 2154
			GameSuccess,
			// Token: 0x0400086B RID: 2155
			Plate,
			// Token: 0x0400086C RID: 2156
			PageTurn,
			// Token: 0x0400086D RID: 2157
			MenuSelect,
			// Token: 0x0400086E RID: 2158
			MaleCustomerGreet,
			// Token: 0x0400086F RID: 2159
			MaleCustomerThank,
			// Token: 0x04000870 RID: 2160
			MaleCustomerLeave,
			// Token: 0x04000871 RID: 2161
			FemaleCustomerGreet,
			// Token: 0x04000872 RID: 2162
			FemaleCustomerThank,
			// Token: 0x04000873 RID: 2163
			FemaleCustomerLeave,
			// Token: 0x04000874 RID: 2164
			MenuOpen
		}
	}
}
