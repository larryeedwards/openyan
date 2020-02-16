using System;

namespace Pathfinding
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public abstract class MonoModifier : VersionedMonoBehaviour, IPathModifier
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x000439E6 File Offset: 0x00041DE6
		protected virtual void OnEnable()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00043A11 File Offset: 0x00041E11
		protected virtual void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060008E5 RID: 2277
		public abstract int Order { get; }

		// Token: 0x060008E6 RID: 2278 RVA: 0x00043A30 File Offset: 0x00041E30
		public virtual void PreProcess(Path path)
		{
		}

		// Token: 0x060008E7 RID: 2279
		public abstract void Apply(Path path);

		// Token: 0x04000607 RID: 1543
		[NonSerialized]
		public Seeker seeker;
	}
}
