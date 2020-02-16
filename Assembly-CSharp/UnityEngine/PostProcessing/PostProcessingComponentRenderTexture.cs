using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000307 RID: 775
	public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x0600168D RID: 5773 RVA: 0x000A69D3 File Offset: 0x000A4DD3
		public virtual void Prepare(Material material)
		{
		}
	}
}
