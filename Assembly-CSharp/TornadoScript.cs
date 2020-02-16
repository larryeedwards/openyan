using System;
using UnityEngine;

// Token: 0x02000556 RID: 1366
public class TornadoScript : MonoBehaviour
{
	// Token: 0x060021B5 RID: 8629 RVA: 0x001987B0 File Offset: 0x00196BB0
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
			this.MyCollider.enabled = true;
		}
	}

	// Token: 0x060021B6 RID: 8630 RVA: 0x0019883C File Offset: 0x00196C3C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				this.Scream = UnityEngine.Object.Instantiate<GameObject>((!component.Male) ? this.FemaleBloodyScream : this.MaleBloodyScream, component.transform.position + Vector3.up, Quaternion.identity);
				this.Scream.transform.parent = component.HipCollider.transform;
				this.Scream.transform.localPosition = Vector3.zero;
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce(Vector3.up * this.Strength);
			}
		}
	}

	// Token: 0x040036AE RID: 13998
	public GameObject FemaleBloodyScream;

	// Token: 0x040036AF RID: 13999
	public GameObject MaleBloodyScream;

	// Token: 0x040036B0 RID: 14000
	public GameObject Scream;

	// Token: 0x040036B1 RID: 14001
	public Collider MyCollider;

	// Token: 0x040036B2 RID: 14002
	public float Strength = 10000f;

	// Token: 0x040036B3 RID: 14003
	public float Timer;
}
