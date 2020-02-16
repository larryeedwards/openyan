using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class Orbital : MonoBehaviour
{
	// Token: 0x06000AD4 RID: 2772 RVA: 0x0005361C File Offset: 0x00051A1C
	private void Awake()
	{
		this.direction = new Vector3(0f, 0f, (this.target.position - base.transform.position).magnitude);
		base.transform.SetParent(this.target);
		this.lastPosition = Input.mousePosition;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00053680 File Offset: 0x00051A80
	private void Update()
	{
		Vector3 vector = Input.mousePosition - this.lastPosition;
		if (Input.GetMouseButton(0))
		{
			this.movement += new Vector3(vector.x * 0.1f, vector.y * 0.05f, 0f);
		}
		this.movement.z = this.movement.z + Input.GetAxis("Mouse ScrollWheel") * -2.5f;
		this.rotation += this.movement;
		this.rotation.x = this.rotation.x % 360f;
		this.rotation.y = Mathf.Clamp(this.rotation.y, -80f, -10f);
		this.direction.z = Mathf.Clamp(this.movement.z + this.direction.z, 15f, 100f);
		base.transform.position = this.target.position + Quaternion.Euler(180f - this.rotation.y, this.rotation.x, 0f) * this.direction;
		base.transform.LookAt(this.target.position);
		this.lastPosition = Input.mousePosition;
		this.movement *= 0.9f;
	}

	// Token: 0x040007BA RID: 1978
	public Transform target;

	// Token: 0x040007BB RID: 1979
	private Vector3 lastPosition;

	// Token: 0x040007BC RID: 1980
	private Vector3 direction;

	// Token: 0x040007BD RID: 1981
	private float distance;

	// Token: 0x040007BE RID: 1982
	private Vector3 movement;

	// Token: 0x040007BF RID: 1983
	private Vector3 rotation;
}
