using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000B8 RID: 184
	public interface IMovementPlane
	{
		// Token: 0x060007B2 RID: 1970
		Vector2 ToPlane(Vector3 p);

		// Token: 0x060007B3 RID: 1971
		Vector2 ToPlane(Vector3 p, out float elevation);

		// Token: 0x060007B4 RID: 1972
		Vector3 ToWorld(Vector2 p, float elevation = 0f);
	}
}
