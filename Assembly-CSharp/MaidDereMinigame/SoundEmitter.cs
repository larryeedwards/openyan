using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class SoundEmitter
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00056898 File Offset: 0x00054C98
		public AudioSource GetSource()
		{
			for (int i = 0; i < this.sources.Count; i++)
			{
				if (!this.sources[i].isPlaying)
				{
					return this.sources[i];
				}
			}
			return this.sources[0];
		}

		// Token: 0x04000875 RID: 2165
		public SFXController.Sounds sound;

		// Token: 0x04000876 RID: 2166
		public bool interupt;

		// Token: 0x04000877 RID: 2167
		[Reorderable]
		public AudioSources sources;

		// Token: 0x04000878 RID: 2168
		[Reorderable]
		public AudioClips clips;
	}
}
