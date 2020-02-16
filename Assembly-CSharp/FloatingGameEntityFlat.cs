using System;
using ArchimedsLab;
using UnityEngine;

// Token: 0x02000132 RID: 306
[RequireComponent(typeof(MeshFilter))]
public class FloatingGameEntityFlat : GameEntity
{
	// Token: 0x06000AA8 RID: 2728 RVA: 0x00052C6C File Offset: 0x0005106C
	protected override void Awake()
	{
		base.Awake();
		Mesh m = (!(this.buoyancyMesh == null)) ? this.buoyancyMesh : base.GetComponent<MeshFilter>().mesh;
		WaterCutter.CookCache(m, ref this._triangles, ref this.worldBuffer, ref this.wetTris, ref this.dryTris);
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x00052CC8 File Offset: 0x000510C8
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.rb.IsSleeping())
		{
			return;
		}
		WaterCutter.CookMesh(base.transform.position, base.transform.rotation, ref this._triangles, ref this.worldBuffer);
		WaterCutter.SplitMesh(this.worldBuffer, ref this.wetTris, ref this.dryTris, out this.nbrWet, out this.nbrDry, WaterSurface.flatWater);
		Archimeds.ComputeAllForces(this.wetTris, this.dryTris, this.nbrWet, this.nbrDry, base.speed, this.rb);
	}

	// Token: 0x0400078C RID: 1932
	public Mesh buoyancyMesh;

	// Token: 0x0400078D RID: 1933
	private tri[] _triangles;

	// Token: 0x0400078E RID: 1934
	private tri[] worldBuffer;

	// Token: 0x0400078F RID: 1935
	private tri[] wetTris;

	// Token: 0x04000790 RID: 1936
	private tri[] dryTris;

	// Token: 0x04000791 RID: 1937
	private uint nbrWet;

	// Token: 0x04000792 RID: 1938
	private uint nbrDry;
}
