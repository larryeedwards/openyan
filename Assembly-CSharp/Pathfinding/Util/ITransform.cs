using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000B9 RID: 185
	public interface ITransform
	{
		// Token: 0x060007B5 RID: 1973
		Vector3 Transform(Vector3 position);

		// Token: 0x060007B6 RID: 1974
		Vector3 InverseTransform(Vector3 position);
	}
}
