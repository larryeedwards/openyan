using System;
using ArchimedsLab;
using UnityEngine;

// Token: 0x02000133 RID: 307
[RequireComponent(typeof(MeshFilter))]
public class FloatingGameEntityRealist : GameEntity
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x00052D90 File Offset: 0x00051190
	protected override void Awake()
	{
		base.Awake();
		Mesh m = (!(this.buoyancyMesh == null)) ? this.buoyancyMesh : base.GetComponent<MeshFilter>().mesh;
		WaterCutter.CookCache(m, ref this._triangles, ref this.worldBuffer, ref this.wetTris, ref this.dryTris);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00052DEC File Offset: 0x000511EC
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.rb.IsSleeping())
		{
			return;
		}
		WaterCutter.CookMesh(base.transform.position, base.transform.rotation, ref this._triangles, ref this.worldBuffer);
		WaterCutter.SplitMesh(this.worldBuffer, ref this.wetTris, ref this.dryTris, out this.nbrWet, out this.nbrDry, this.realist);
		Archimeds.ComputeAllForces(this.wetTris, this.dryTris, this.nbrWet, this.nbrDry, base.speed, this.rb);
		if (this.Stable)
		{
			base.transform.localEulerAngles = new Vector3(100f, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x00052EC4 File Offset: 0x000512C4
	private void LateUpdate()
	{
		if (this.Stable)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f);
			if (base.transform.position.y > 0.5f)
			{
				base.transform.position = new Vector3(base.transform.position.x, 1f, base.transform.position.z);
			}
			else if (base.transform.position.y < -0.5f)
			{
				base.transform.position = new Vector3(base.transform.position.x, -1f, base.transform.position.z);
			}
		}
	}

	// Token: 0x04000793 RID: 1939
	public Mesh buoyancyMesh;

	// Token: 0x04000794 RID: 1940
	private tri[] _triangles;

	// Token: 0x04000795 RID: 1941
	private tri[] worldBuffer;

	// Token: 0x04000796 RID: 1942
	private tri[] wetTris;

	// Token: 0x04000797 RID: 1943
	private tri[] dryTris;

	// Token: 0x04000798 RID: 1944
	private uint nbrWet;

	// Token: 0x04000799 RID: 1945
	private uint nbrDry;

	// Token: 0x0400079A RID: 1946
	private WaterSurface.GetWaterHeight realist = (Vector3 pos) => (OceanAdvanced.GetWaterHeight(pos + new Vector3(-0.1f, 0f, -0.1f)) + OceanAdvanced.GetWaterHeight(pos + new Vector3(0.1f, 0f, -0.1f)) + OceanAdvanced.GetWaterHeight(pos + new Vector3(0f, 0f, 0.1f))) / 3f;

	// Token: 0x0400079B RID: 1947
	public bool Stable;
}
