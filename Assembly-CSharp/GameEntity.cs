using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class GameEntity : MonoBehaviour
{
	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00052A4B File Offset: 0x00050E4B
	// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00052A53 File Offset: 0x00050E53
	public Vector3 speed { get; private set; }

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00052A5C File Offset: 0x00050E5C
	// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00052A64 File Offset: 0x00050E64
	public float absSpeed { get; private set; }

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00052A6D File Offset: 0x00050E6D
	// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00052A75 File Offset: 0x00050E75
	public float sqrtSpeed { get; private set; }

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00052A7E File Offset: 0x00050E7E
	// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00052A86 File Offset: 0x00050E86
	public float totalMass { get; private set; }

	// Token: 0x06000ABB RID: 2747 RVA: 0x00052A90 File Offset: 0x00050E90
	protected virtual void Awake()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.S_centerOfMass = this.rb.centerOfMass;
		this.last_position = base.transform.position;
		this.totalMass = GameEntity.GetTotalMass(base.transform);
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00052ADC File Offset: 0x00050EDC
	private static float GetTotalMass(Transform t)
	{
		float num = 0f;
		Rigidbody component = t.GetComponent<Rigidbody>();
		if (component != null)
		{
			num += component.mass;
		}
		for (int i = 0; i < t.childCount; i++)
		{
			num += GameEntity.GetTotalMass(t.GetChild(i));
		}
		return num;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x00052B34 File Offset: 0x00050F34
	protected virtual void FixedUpdate()
	{
		this.speed = (base.transform.position - this.last_position) / Time.deltaTime;
		this.last_position = base.transform.position;
		this.absSpeed = ((this.speed.x >= 0f) ? ((this.speed.x + this.speed.y >= 0f) ? ((this.speed.y + this.speed.z >= 0f) ? this.speed.z : (-this.speed.z)) : (-this.speed.y)) : (-this.speed.x));
		if (this.absSpeed < 0f)
		{
			this.absSpeed = -this.absSpeed;
		}
		this.sqrtSpeed = Mathf.Sqrt(this.absSpeed);
	}

	// Token: 0x040007A4 RID: 1956
	protected Rigidbody rb;

	// Token: 0x040007A5 RID: 1957
	public Vector3 centerOfMassOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040007A6 RID: 1958
	private Vector3 S_centerOfMass;

	// Token: 0x040007AA RID: 1962
	private Vector3 last_position;
}
