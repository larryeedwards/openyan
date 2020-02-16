using System;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class WakeGenerator : MonoBehaviour
{
	// Token: 0x06000ADD RID: 2781 RVA: 0x00053996 File Offset: 0x00051D96
	private void Awake()
	{
		this.ocean = UnityEngine.Object.FindObjectOfType<OceanAdvanced>();
		this.speed = 0f;
		this.last_position = base.transform.position;
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x000539C0 File Offset: 0x00051DC0
	private void Update()
	{
		this.speed = (base.transform.position - this.last_position).magnitude / Time.deltaTime;
		this.last_position = base.transform.position;
		if (Time.time % 0.2f < 0.01f)
		{
			Vector3 vector = base.transform.position + base.transform.rotation * this.offset;
			if (OceanAdvanced.GetWaterHeight(vector) > vector.y)
			{
				this.ocean.RegisterInteraction(vector, Mathf.Clamp01(this.speed / 15f) * 0.5f);
			}
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00053A79 File Offset: 0x00051E79
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(base.transform.position + base.transform.rotation * this.offset, 0.5f);
	}

	// Token: 0x040007C4 RID: 1988
	public Vector3 offset;

	// Token: 0x040007C5 RID: 1989
	private OceanAdvanced ocean;

	// Token: 0x040007C6 RID: 1990
	private Vector3 last_position;

	// Token: 0x040007C7 RID: 1991
	private float speed;
}
