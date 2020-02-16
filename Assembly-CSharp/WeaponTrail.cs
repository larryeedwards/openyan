using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000590 RID: 1424
public class WeaponTrail : MonoBehaviour
{
	// Token: 0x170004C0 RID: 1216
	// (set) Token: 0x06002276 RID: 8822 RVA: 0x001A13C8 File Offset: 0x0019F7C8
	public bool Emit
	{
		set
		{
			this._emit = value;
		}
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x001A13D4 File Offset: 0x0019F7D4
	public void Start()
	{
		this._lastPosition = base.transform.position;
		this._o = new GameObject("Trail");
		this._o.transform.parent = null;
		this._o.transform.position = Vector3.zero;
		this._o.transform.rotation = Quaternion.identity;
		this._o.transform.localScale = Vector3.one;
		this._o.AddComponent<MeshFilter>();
		this._o.AddComponent<MeshRenderer>();
		this._o.GetComponent<Renderer>().material = this._material;
		this._trailMesh = new Mesh();
		this._trailMesh.name = base.name + "TrailMesh";
		this._o.GetComponent<MeshFilter>().mesh = this._trailMesh;
	}

	// Token: 0x06002278 RID: 8824 RVA: 0x001A14BC File Offset: 0x0019F8BC
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(this._o);
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x001A14CC File Offset: 0x0019F8CC
	private void Update()
	{
		if (this._emit && this._emitTime != 0f)
		{
			this._emitTime -= Time.deltaTime;
			if (this._emitTime == 0f)
			{
				this._emitTime = -1f;
			}
			if (this._emitTime < 0f)
			{
				this._emit = false;
			}
		}
		if (!this._emit && this._points.Count == 0 && this._autoDestruct)
		{
			UnityEngine.Object.Destroy(this._o);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!Camera.main)
		{
			return;
		}
		float magnitude = (this._lastPosition - base.transform.position).magnitude;
		if (this._emit)
		{
			if (magnitude > this._minVertexDistance)
			{
				bool flag = false;
				if (this._points.Count < 3)
				{
					flag = true;
				}
				else
				{
					Vector3 from = this._points[this._points.Count - 2].tipPosition - this._points[this._points.Count - 3].tipPosition;
					Vector3 to = this._points[this._points.Count - 1].tipPosition - this._points[this._points.Count - 2].tipPosition;
					if (Vector3.Angle(from, to) > this._maxAngle || magnitude > this._maxVertexDistance)
					{
						flag = true;
					}
				}
				if (flag)
				{
					WeaponTrail.Point point = new WeaponTrail.Point();
					point.basePosition = this._base.position;
					point.tipPosition = this._tip.position;
					point.timeCreated = Time.time;
					this._points.Add(point);
					this._lastPosition = base.transform.position;
				}
				else
				{
					this._points[this._points.Count - 1].basePosition = this._base.position;
					this._points[this._points.Count - 1].tipPosition = this._tip.position;
				}
			}
			else if (this._points.Count > 0)
			{
				this._points[this._points.Count - 1].basePosition = this._base.position;
				this._points[this._points.Count - 1].tipPosition = this._tip.position;
			}
		}
		if (!this._emit && this._lastFrameEmit && this._points.Count > 0)
		{
			this._points[this._points.Count - 1].lineBreak = true;
		}
		this._lastFrameEmit = this._emit;
		List<WeaponTrail.Point> list = new List<WeaponTrail.Point>();
		foreach (WeaponTrail.Point point2 in this._points)
		{
			if (Time.time - point2.timeCreated > this._lifeTime)
			{
				list.Add(point2);
			}
		}
		foreach (WeaponTrail.Point item in list)
		{
			this._points.Remove(item);
		}
		List<WeaponTrail.Point> points = this._points;
		if (points.Count > 1)
		{
			Vector3[] array = new Vector3[points.Count * 2];
			Vector2[] array2 = new Vector2[points.Count * 2];
			int[] array3 = new int[(points.Count - 1) * 6];
			Color[] array4 = new Color[points.Count * 2];
			for (int i = 0; i < points.Count; i++)
			{
				WeaponTrail.Point point3 = points[i];
				float num = (Time.time - point3.timeCreated) / this._lifeTime;
				Color color = Color.Lerp(Color.white, Color.clear, num);
				if (this._colors != null && this._colors.Length > 0)
				{
					float num2 = num * (float)(this._colors.Length - 1);
					float num3 = Mathf.Floor(num2);
					float num4 = Mathf.Clamp(Mathf.Ceil(num2), 1f, (float)this._colors.Length - 1f);
					float t = Mathf.InverseLerp(num3, num4, num2);
					if (num3 >= (float)this._colors.Length)
					{
						num3 = (float)this._colors.Length - 1f;
					}
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					if (num4 >= (float)this._colors.Length)
					{
						num4 = (float)this._colors.Length - 1f;
					}
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					color = Color.Lerp(this._colors[(int)num3], this._colors[(int)num4], t);
				}
				float num5 = 0f;
				if (this._sizes != null && this._sizes.Length > 0)
				{
					float num6 = num * (float)(this._sizes.Length - 1);
					float num7 = Mathf.Floor(num6);
					float num8 = Mathf.Clamp(Mathf.Ceil(num6), 1f, (float)this._sizes.Length - 1f);
					float t2 = Mathf.InverseLerp(num7, num8, num6);
					if (num7 >= (float)this._sizes.Length)
					{
						num7 = (float)this._sizes.Length - 1f;
					}
					if (num7 < 0f)
					{
						num7 = 0f;
					}
					if (num8 >= (float)this._sizes.Length)
					{
						num8 = (float)this._sizes.Length - 1f;
					}
					if (num8 < 0f)
					{
						num8 = 0f;
					}
					num5 = Mathf.Lerp(this._sizes[(int)num7], this._sizes[(int)num8], t2);
				}
				Vector3 a = point3.tipPosition - point3.basePosition;
				array[i * 2] = point3.basePosition - a * (num5 * 0.5f);
				array[i * 2 + 1] = point3.tipPosition + a * (num5 * 0.5f);
				array4[i * 2] = (array4[i * 2 + 1] = color);
				float x = (float)i / (float)points.Count;
				array2[i * 2] = new Vector2(x, 0f);
				array2[i * 2 + 1] = new Vector2(x, 1f);
				if (i > 0)
				{
					array3[(i - 1) * 6] = i * 2 - 2;
					array3[(i - 1) * 6 + 1] = i * 2 - 1;
					array3[(i - 1) * 6 + 2] = i * 2;
					array3[(i - 1) * 6 + 3] = i * 2 + 1;
					array3[(i - 1) * 6 + 4] = i * 2;
					array3[(i - 1) * 6 + 5] = i * 2 - 1;
				}
			}
			this._trailMesh.Clear();
			this._trailMesh.vertices = array;
			this._trailMesh.colors = array4;
			this._trailMesh.uv = array2;
			this._trailMesh.triangles = array3;
		}
		else
		{
			this._trailMesh.Clear();
		}
	}

	// Token: 0x04003845 RID: 14405
	[SerializeField]
	private bool _emit = true;

	// Token: 0x04003846 RID: 14406
	[SerializeField]
	private float _emitTime;

	// Token: 0x04003847 RID: 14407
	[SerializeField]
	private Material _material;

	// Token: 0x04003848 RID: 14408
	[SerializeField]
	private float _lifeTime = 1f;

	// Token: 0x04003849 RID: 14409
	[SerializeField]
	private Color[] _colors;

	// Token: 0x0400384A RID: 14410
	[SerializeField]
	private float[] _sizes;

	// Token: 0x0400384B RID: 14411
	[SerializeField]
	private float _minVertexDistance = 0.1f;

	// Token: 0x0400384C RID: 14412
	[SerializeField]
	private float _maxVertexDistance = 10f;

	// Token: 0x0400384D RID: 14413
	[SerializeField]
	private float _maxAngle = 3f;

	// Token: 0x0400384E RID: 14414
	[SerializeField]
	private bool _autoDestruct;

	// Token: 0x0400384F RID: 14415
	[SerializeField]
	private Transform _base;

	// Token: 0x04003850 RID: 14416
	[SerializeField]
	private Transform _tip;

	// Token: 0x04003851 RID: 14417
	private List<WeaponTrail.Point> _points = new List<WeaponTrail.Point>();

	// Token: 0x04003852 RID: 14418
	private GameObject _o;

	// Token: 0x04003853 RID: 14419
	private Mesh _trailMesh;

	// Token: 0x04003854 RID: 14420
	private Vector3 _lastPosition;

	// Token: 0x04003855 RID: 14421
	private Vector3 _lastCameraPosition1;

	// Token: 0x04003856 RID: 14422
	private Vector3 _lastCameraPosition2;

	// Token: 0x04003857 RID: 14423
	private bool _lastFrameEmit = true;

	// Token: 0x02000591 RID: 1425
	public class Point
	{
		// Token: 0x04003858 RID: 14424
		public float timeCreated;

		// Token: 0x04003859 RID: 14425
		public Vector3 basePosition;

		// Token: 0x0400385A RID: 14426
		public Vector3 tipPosition;

		// Token: 0x0400385B RID: 14427
		public bool lineBreak;
	}
}
