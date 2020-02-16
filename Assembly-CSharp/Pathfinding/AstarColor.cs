using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class AstarColor
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00007158 File Offset: 0x00005558
		public AstarColor()
		{
			this._NodeConnection = new Color(1f, 1f, 1f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007225 File Offset: 0x00005625
		public static Color GetAreaColor(uint area)
		{
			if (AstarColor.AreaColors == null || (ulong)area >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)area, 1f);
			}
			return AstarColor.AreaColors[(int)area];
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000725C File Offset: 0x0000565C
		public void OnEnable()
		{
			AstarColor.NodeConnection = this._NodeConnection;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x0400009D RID: 157
		public Color _NodeConnection;

		// Token: 0x0400009E RID: 158
		public Color _UnwalkableNode;

		// Token: 0x0400009F RID: 159
		public Color _BoundsHandles;

		// Token: 0x040000A0 RID: 160
		public Color _ConnectionLowLerp;

		// Token: 0x040000A1 RID: 161
		public Color _ConnectionHighLerp;

		// Token: 0x040000A2 RID: 162
		public Color _MeshEdgeColor;

		// Token: 0x040000A3 RID: 163
		public Color[] _AreaColors;

		// Token: 0x040000A4 RID: 164
		public static Color NodeConnection = new Color(1f, 1f, 1f, 0.9f);

		// Token: 0x040000A5 RID: 165
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x040000A6 RID: 166
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x040000A7 RID: 167
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x040000A8 RID: 168
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x040000A9 RID: 169
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x040000AA RID: 170
		private static Color[] AreaColors;
	}
}
