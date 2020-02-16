using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000305 RID: 773
	public abstract class PostProcessingComponent<T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000A63C8 File Offset: 0x000A47C8
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x000A63D0 File Offset: 0x000A47D0
		public T model { get; internal set; }

		// Token: 0x06001686 RID: 5766 RVA: 0x000A63D9 File Offset: 0x000A47D9
		public virtual void Init(PostProcessingContext pcontext, T pmodel)
		{
			this.context = pcontext;
			this.model = pmodel;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000A63E9 File Offset: 0x000A47E9
		public override PostProcessingModel GetModel()
		{
			return this.model;
		}
	}
}
