using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class GasterBeamScript : MonoBehaviour
{
	// Token: 0x060019C7 RID: 6599 RVA: 0x000F23DD File Offset: 0x000F07DD
	private void Start()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x000F240C File Offset: 0x000F080C
	private void Update()
	{
		if (this.LoveLoveBeam)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(100f, this.Target, this.Target), Time.deltaTime * 10f);
			if (base.transform.localScale.x > 99.99f)
			{
				this.Target = 0f;
				if (base.transform.localScale.y < 0.1f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000F24B0 File Offset: 0x000F08B0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
				Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
				rigidbody.isKinematic = false;
				rigidbody.AddForce((rigidbody.transform.root.position - base.transform.root.position) * this.Strength);
				rigidbody.AddForce(Vector3.up * 1000f);
			}
		}
	}

	// Token: 0x04001F24 RID: 7972
	public float Strength = 1000f;

	// Token: 0x04001F25 RID: 7973
	public float Target = 2f;

	// Token: 0x04001F26 RID: 7974
	public bool LoveLoveBeam;
}
