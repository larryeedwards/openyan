using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	public abstract class PostProcessingModel
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000ABC10 File Offset: 0x000AA010
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x000ABC18 File Offset: 0x000AA018
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				if (value)
				{
					this.OnValidate();
				}
			}
		}

		// Token: 0x0600169B RID: 5787
		public abstract void Reset();

		// Token: 0x0600169C RID: 5788 RVA: 0x000ABC2D File Offset: 0x000AA02D
		public virtual void OnValidate()
		{
		}

		// Token: 0x0400143B RID: 5179
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;
	}
}
