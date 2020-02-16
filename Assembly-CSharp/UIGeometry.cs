using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class UIGeometry
{
	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x0600109F RID: 4255 RVA: 0x0008A880 File Offset: 0x00088C80
	public bool hasVertices
	{
		get
		{
			return this.verts.Count > 0;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0008A890 File Offset: 0x00088C90
	public bool hasTransformed
	{
		get
		{
			return this.mRtpVerts != null && this.mRtpVerts.Count > 0 && this.mRtpVerts.Count == this.verts.Count;
		}
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0008A8C9 File Offset: 0x00088CC9
	public void Clear()
	{
		this.verts.Clear();
		this.uvs.Clear();
		this.cols.Clear();
		this.mRtpVerts.Clear();
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0008A8F8 File Offset: 0x00088CF8
	public void ApplyTransform(Matrix4x4 widgetToPanel, bool generateNormals = true)
	{
		if (this.verts.Count > 0)
		{
			this.mRtpVerts.Clear();
			int i = 0;
			int count = this.verts.Count;
			while (i < count)
			{
				this.mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(this.verts[i]));
				i++;
			}
			if (generateNormals)
			{
				this.mRtpNormal = widgetToPanel.MultiplyVector(Vector3.back).normalized;
				Vector3 normalized = widgetToPanel.MultiplyVector(Vector3.right).normalized;
				this.mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
			}
		}
		else
		{
			this.mRtpVerts.Clear();
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0008A9C8 File Offset: 0x00088DC8
	public void WriteToBuffers(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2)
	{
		if (this.mRtpVerts != null && this.mRtpVerts.Count > 0)
		{
			if (n == null)
			{
				int i = 0;
				int count = this.mRtpVerts.Count;
				while (i < count)
				{
					v.Add(this.mRtpVerts[i]);
					u.Add(this.uvs[i]);
					c.Add(this.cols[i]);
					i++;
				}
			}
			else
			{
				int j = 0;
				int count2 = this.mRtpVerts.Count;
				while (j < count2)
				{
					v.Add(this.mRtpVerts[j]);
					u.Add(this.uvs[j]);
					c.Add(this.cols[j]);
					n.Add(this.mRtpNormal);
					t.Add(this.mRtpTan);
					j++;
				}
			}
			if (u2 != null)
			{
				Vector4 zero = Vector4.zero;
				int k = 0;
				int count3 = this.verts.Count;
				while (k < count3)
				{
					zero.x = this.verts[k].x;
					zero.y = this.verts[k].y;
					u2.Add(zero);
					k++;
				}
			}
			if (this.onCustomWrite != null)
			{
				this.onCustomWrite(v, u, c, n, t, u2);
			}
		}
	}

	// Token: 0x04000E87 RID: 3719
	public List<Vector3> verts = new List<Vector3>();

	// Token: 0x04000E88 RID: 3720
	public List<Vector2> uvs = new List<Vector2>();

	// Token: 0x04000E89 RID: 3721
	public List<Color> cols = new List<Color>();

	// Token: 0x04000E8A RID: 3722
	public UIGeometry.OnCustomWrite onCustomWrite;

	// Token: 0x04000E8B RID: 3723
	private List<Vector3> mRtpVerts = new List<Vector3>();

	// Token: 0x04000E8C RID: 3724
	private Vector3 mRtpNormal;

	// Token: 0x04000E8D RID: 3725
	private Vector4 mRtpTan;

	// Token: 0x02000221 RID: 545
	// (Invoke) Token: 0x060010A5 RID: 4261
	public delegate void OnCustomWrite(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2);
}
