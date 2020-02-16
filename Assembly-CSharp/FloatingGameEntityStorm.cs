using System;
using ArchimedsLab;
using UnityEngine;

// Token: 0x02000134 RID: 308
[RequireComponent(typeof(MeshFilter))]
public class FloatingGameEntityStorm : GameEntity
{
	// Token: 0x06000AB0 RID: 2736 RVA: 0x0005304C File Offset: 0x0005144C
	protected override void Awake()
	{
		base.Awake();
		Mesh m = (!(this.buoyancyMesh == null)) ? this.buoyancyMesh : base.GetComponent<MeshFilter>().mesh;
		WaterCutter.CookCache(m, ref this._triangles, ref this.worldBuffer, ref this.wetTris, ref this.dryTris);
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x000530A8 File Offset: 0x000514A8
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.rb.IsSleeping())
		{
			return;
		}
		WaterCutter.CookMesh(base.transform.position, base.transform.rotation, ref this._triangles, ref this.worldBuffer);
		WaterCutter.SplitMesh(this.worldBuffer, ref this.wetTris, ref this.dryTris, out this.nbrWet, out this.nbrDry, WaterSurface.simpleWater);
		Archimeds.ComputeAllForces(this.wetTris, this.dryTris, this.nbrWet, this.nbrDry, base.speed, this.rb);
	}

	// Token: 0x0400079D RID: 1949
	public Mesh buoyancyMesh;

	// Token: 0x0400079E RID: 1950
	private tri[] _triangles;

	// Token: 0x0400079F RID: 1951
	private tri[] worldBuffer;

	// Token: 0x040007A0 RID: 1952
	private tri[] wetTris;

	// Token: 0x040007A1 RID: 1953
	private tri[] dryTris;

	// Token: 0x040007A2 RID: 1954
	private uint nbrWet;

	// Token: 0x040007A3 RID: 1955
	private uint nbrDry;
}
