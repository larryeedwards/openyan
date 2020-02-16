using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000304 RID: 772
	public abstract class PostProcessingComponentBase
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x000A63B9 File Offset: 0x000A47B9
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600167F RID: 5759
		public abstract bool active { get; }

		// Token: 0x06001680 RID: 5760 RVA: 0x000A63BC File Offset: 0x000A47BC
		public virtual void OnEnable()
		{
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000A63BE File Offset: 0x000A47BE
		public virtual void OnDisable()
		{
		}

		// Token: 0x06001682 RID: 5762
		public abstract PostProcessingModel GetModel();

		// Token: 0x04001434 RID: 5172
		public PostProcessingContext context;
	}
}
