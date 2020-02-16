using System;

namespace Pathfinding
{
	// Token: 0x02000059 RID: 89
	public struct AstarWorkItem
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00017886 File Offset: 0x00015C86
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = null;
			this.update = update;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000178A4 File Offset: 0x00015CA4
		public AstarWorkItem(Func<IWorkItemContext, bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = update;
			this.update = null;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000178C2 File Offset: 0x00015CC2
		public AstarWorkItem(Action init, Func<bool, bool> update = null)
		{
			this.init = init;
			this.initWithContext = null;
			this.update = update;
			this.updateWithContext = null;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000178E0 File Offset: 0x00015CE0
		public AstarWorkItem(Action<IWorkItemContext> init, Func<IWorkItemContext, bool, bool> update = null)
		{
			this.init = null;
			this.initWithContext = init;
			this.update = null;
			this.updateWithContext = update;
		}

		// Token: 0x0400023F RID: 575
		public Action init;

		// Token: 0x04000240 RID: 576
		public Action<IWorkItemContext> initWithContext;

		// Token: 0x04000241 RID: 577
		public Func<bool, bool> update;

		// Token: 0x04000242 RID: 578
		public Func<IWorkItemContext, bool, bool> updateWithContext;
	}
}
