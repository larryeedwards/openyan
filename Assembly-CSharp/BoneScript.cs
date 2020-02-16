using System;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class BoneScript : MonoBehaviour
{
	// Token: 0x06001765 RID: 5989 RVA: 0x000B885C File Offset: 0x000B6C5C
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 360f), base.transform.eulerAngles.z);
		this.Origin = base.transform.position.y;
		base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000B88E4 File Offset: 0x000B6CE4
	private void Update()
	{
		if (!this.Drop)
		{
			if (base.transform.position.y < this.Origin + 2f - 0.0001f)
			{
				base.transform.position = new Vector3(base.transform.position.x, Mathf.Lerp(base.transform.position.y, this.Origin + 2f, Time.deltaTime * 10f), base.transform.position.z);
			}
			else
			{
				this.Drop = true;
			}
		}
		else
		{
			this.Height -= Time.deltaTime;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + this.Height, base.transform.position.z);
			if (base.transform.position.y < this.Origin - 2.155f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000B8A34 File Offset: 0x000B6E34
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
				rigidbody.AddForce(base.transform.up);
			}
		}
	}

	// Token: 0x040016F4 RID: 5876
	public float Height;

	// Token: 0x040016F5 RID: 5877
	public float Origin;

	// Token: 0x040016F6 RID: 5878
	public bool Drop;
}
