using System;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class JiggleBone : MonoBehaviour
{
	// Token: 0x06001D16 RID: 7446 RVA: 0x00110C90 File Offset: 0x0010F090
	private void Awake()
	{
		Vector3 vector = base.transform.position + base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		this.dynamicPos = vector;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x00110CD4 File Offset: 0x0010F0D4
	private void LateUpdate()
	{
		base.transform.rotation = default(Quaternion);
		Vector3 dir = base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		Vector3 vector = base.transform.TransformDirection(new Vector3(0f, 1f, 0f));
		Vector3 vector2 = base.transform.position + base.transform.TransformDirection(this.boneAxis * this.targetDistance);
		this.force.x = (vector2.x - this.dynamicPos.x) * this.bStiffness;
		this.acc.x = this.force.x / this.bMass;
		this.vel.x = this.vel.x + this.acc.x * (1f - this.bDamping);
		this.force.y = (vector2.y - this.dynamicPos.y) * this.bStiffness;
		this.force.y = this.force.y - this.bGravity / 10f;
		this.acc.y = this.force.y / this.bMass;
		this.vel.y = this.vel.y + this.acc.y * (1f - this.bDamping);
		this.force.z = (vector2.z - this.dynamicPos.z) * this.bStiffness;
		this.acc.z = this.force.z / this.bMass;
		this.vel.z = this.vel.z + this.acc.z * (1f - this.bDamping);
		this.dynamicPos += this.vel + this.force;
		base.transform.LookAt(this.dynamicPos, vector);
		if (this.SquashAndStretch)
		{
			float magnitude = (this.dynamicPos - vector2).magnitude;
			float num = 1f + ((this.boneAxis.x != 0f) ? (magnitude * this.frontStretch) : (-magnitude * this.sideStretch));
			float num2 = 1f + ((this.boneAxis.y != 0f) ? (magnitude * this.frontStretch) : (-magnitude * this.sideStretch));
			float num3 = 1f + ((this.boneAxis.z != 0f) ? (magnitude * this.frontStretch) : (-magnitude * this.sideStretch));
		}
		if (this.debugMode)
		{
			Debug.DrawRay(base.transform.position, dir, Color.blue);
			Debug.DrawRay(base.transform.position, vector, Color.green);
			Debug.DrawRay(vector2, Vector3.up * 0.2f, Color.yellow);
			Debug.DrawRay(this.dynamicPos, Vector3.up * 0.2f, Color.red);
		}
	}

	// Token: 0x040023D4 RID: 9172
	public bool debugMode = true;

	// Token: 0x040023D5 RID: 9173
	private Vector3 dynamicPos = default(Vector3);

	// Token: 0x040023D6 RID: 9174
	public Vector3 boneAxis = new Vector3(0f, 0f, 1f);

	// Token: 0x040023D7 RID: 9175
	public float targetDistance = 2f;

	// Token: 0x040023D8 RID: 9176
	public float bStiffness = 0.1f;

	// Token: 0x040023D9 RID: 9177
	public float bMass = 0.9f;

	// Token: 0x040023DA RID: 9178
	public float bDamping = 0.75f;

	// Token: 0x040023DB RID: 9179
	public float bGravity = 0.75f;

	// Token: 0x040023DC RID: 9180
	private Vector3 force = default(Vector3);

	// Token: 0x040023DD RID: 9181
	private Vector3 acc = default(Vector3);

	// Token: 0x040023DE RID: 9182
	private Vector3 vel = default(Vector3);

	// Token: 0x040023DF RID: 9183
	public bool SquashAndStretch = true;

	// Token: 0x040023E0 RID: 9184
	public float sideStretch = 0.15f;

	// Token: 0x040023E1 RID: 9185
	public float frontStretch = 0.2f;
}
