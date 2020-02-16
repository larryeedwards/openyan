using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000016 RID: 22
	public struct Progress
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000075E5 File Offset: 0x000059E5
		public Progress(float progress, string description)
		{
			this.progress = progress;
			this.description = description;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000075F5 File Offset: 0x000059F5
		public Progress MapTo(float min, float max, string prefix = null)
		{
			return new Progress(Mathf.Lerp(min, max, this.progress), prefix + this.description);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007618 File Offset: 0x00005A18
		public override string ToString()
		{
			return this.progress.ToString("0.0") + " " + this.description;
		}

		// Token: 0x040000BF RID: 191
		public readonly float progress;

		// Token: 0x040000C0 RID: 192
		public readonly string description;
	}
}
