using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200003C RID: 60
	public class Draw
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00011B19 File Offset: 0x0000FF19
		private void SetColor(Color color)
		{
			if (this.gizmos && UnityEngine.Gizmos.color != color)
			{
				UnityEngine.Gizmos.color = color;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011B3C File Offset: 0x0000FF3C
		public void Line(Vector3 a, Vector3 b, Color color)
		{
			this.SetColor(color);
			if (this.gizmos)
			{
				UnityEngine.Gizmos.DrawLine(this.matrix.MultiplyPoint3x4(a), this.matrix.MultiplyPoint3x4(b));
			}
			else
			{
				UnityEngine.Debug.DrawLine(this.matrix.MultiplyPoint3x4(a), this.matrix.MultiplyPoint3x4(b), color);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011B9C File Offset: 0x0000FF9C
		public void CircleXZ(Vector3 center, float radius, Color color, float startAngle = 0f, float endAngle = 6.28318548f)
		{
			int num = 40;
			while (startAngle > endAngle)
			{
				startAngle -= 6.28318548f;
			}
			Vector3 b = new Vector3(Mathf.Cos(startAngle) * radius, 0f, Mathf.Sin(startAngle) * radius);
			for (int i = 0; i <= num; i++)
			{
				Vector3 vector = new Vector3(Mathf.Cos(Mathf.Lerp(startAngle, endAngle, (float)i / (float)num)) * radius, 0f, Mathf.Sin(Mathf.Lerp(startAngle, endAngle, (float)i / (float)num)) * radius);
				this.Line(center + b, center + vector, color);
				b = vector;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011C44 File Offset: 0x00010044
		public void Cylinder(Vector3 position, Vector3 up, float height, float radius, Color color)
		{
			Vector3 normalized = Vector3.Cross(up, Vector3.one).normalized;
			this.matrix = Matrix4x4.TRS(position, Quaternion.LookRotation(normalized, up), new Vector3(radius, height, radius));
			this.CircleXZ(Vector3.zero, 1f, color, 0f, 6.28318548f);
			if (height > 0f)
			{
				this.CircleXZ(Vector3.up, 1f, color, 0f, 6.28318548f);
				this.Line(new Vector3(1f, 0f, 0f), new Vector3(1f, 1f, 0f), color);
				this.Line(new Vector3(-1f, 0f, 0f), new Vector3(-1f, 1f, 0f), color);
				this.Line(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 1f), color);
				this.Line(new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, -1f), color);
			}
			this.matrix = Matrix4x4.identity;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011D90 File Offset: 0x00010190
		public void CrossXZ(Vector3 position, Color color, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - Vector3.right * size, position + Vector3.right * size, color);
			this.Line(position - Vector3.forward * size, position + Vector3.forward * size, color);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011DF8 File Offset: 0x000101F8
		public void Bezier(Vector3 a, Vector3 b, Color color)
		{
			Vector3 vector = b - a;
			if (vector == Vector3.zero)
			{
				return;
			}
			Vector3 rhs = Vector3.Cross(Vector3.up, vector);
			Vector3 vector2 = Vector3.Cross(vector, rhs).normalized;
			vector2 *= vector.magnitude * 0.1f;
			Vector3 p = a + vector2;
			Vector3 p2 = b + vector2;
			Vector3 a2 = a;
			for (int i = 1; i <= 20; i++)
			{
				float t = (float)i / 20f;
				Vector3 vector3 = AstarSplines.CubicBezier(a, p, p2, b, t);
				this.Line(a2, vector3, color);
				a2 = vector3;
			}
		}

		// Token: 0x040001C6 RID: 454
		public static readonly Draw Debug = new Draw
		{
			gizmos = false
		};

		// Token: 0x040001C7 RID: 455
		public static readonly Draw Gizmos = new Draw
		{
			gizmos = true
		};

		// Token: 0x040001C8 RID: 456
		private bool gizmos;

		// Token: 0x040001C9 RID: 457
		private Matrix4x4 matrix = Matrix4x4.identity;
	}
}
