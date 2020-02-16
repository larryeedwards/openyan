using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class SplashGenerator : MonoBehaviour
{
	// Token: 0x06000AD7 RID: 2775 RVA: 0x00053814 File Offset: 0x00051C14
	private void Awake()
	{
		this.ocean = UnityEngine.Object.FindObjectOfType<OceanAdvanced>();
		this.speed = 0f;
		this.last_position = base.transform.position;
		base.InvokeRepeating("CheckSplash", 0.1f, 0.2f);
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00053854 File Offset: 0x00051C54
	private void CheckSplash()
	{
		this.speed = (base.transform.position - this.last_position).magnitude / 0.5f;
		if (this.speed < 3f)
		{
			return;
		}
		Vector3 vector = base.transform.position + base.transform.rotation * this.offset;
		float waterHeight = OceanAdvanced.GetWaterHeight(vector);
		if (vector.y < waterHeight && this.last_position.y > waterHeight && this.speed > 2f)
		{
			this.ocean.RegisterInteraction(vector, Mathf.Clamp01(this.speed / 15f) * 0.5f);
		}
		this.last_position = base.transform.position;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0005392C File Offset: 0x00051D2C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(base.transform.position + base.transform.rotation * this.offset, 1f);
	}

	// Token: 0x040007C0 RID: 1984
	public Vector3 offset;

	// Token: 0x040007C1 RID: 1985
	private OceanAdvanced ocean;

	// Token: 0x040007C2 RID: 1986
	private Vector3 last_position;

	// Token: 0x040007C3 RID: 1987
	private float speed;
}
