using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000306 RID: 774
	public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06001689 RID: 5769
		public abstract CameraEvent GetCameraEvent();

		// Token: 0x0600168A RID: 5770
		public abstract string GetName();

		// Token: 0x0600168B RID: 5771
		public abstract void PopulateCommandBuffer(CommandBuffer cb);
	}
}
