using System;

namespace Pathfinding
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060008DD RID: 2269
		public abstract int Order { get; }

		// Token: 0x060008DE RID: 2270 RVA: 0x00045184 File Offset: 0x00043584
		public void Awake(Seeker seeker)
		{
			this.seeker = seeker;
			if (seeker != null)
			{
				seeker.RegisterModifier(this);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000451A0 File Offset: 0x000435A0
		public void OnDestroy(Seeker seeker)
		{
			if (seeker != null)
			{
				seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000451B5 File Offset: 0x000435B5
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x060008E1 RID: 2273
		public abstract void Apply(Path path);

		// Token: 0x04000606 RID: 1542
		[NonSerialized]
		public Seeker seeker;
	}
}
