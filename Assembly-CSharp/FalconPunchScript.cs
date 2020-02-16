using System;
using UnityEngine;

// Token: 0x020003CE RID: 974
public class FalconPunchScript : MonoBehaviour
{
	// Token: 0x06001991 RID: 6545 RVA: 0x000EE956 File Offset: 0x000ECD56
	private void Start()
	{
		if (this.Mecha)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed * 10f);
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000EE990 File Offset: 0x000ECD90
	private void Update()
	{
		if (!this.IgnoreTime)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.TimeLimit)
			{
				this.MyCollider.enabled = false;
			}
		}
		if (this.Shipgirl)
		{
			this.MyRigidbody.AddForce(base.transform.forward * this.Speed);
		}
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x000EEA04 File Offset: 0x000ECE04
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("A punch collided with something.");
		if (other.gameObject.layer == 9)
		{
			Debug.Log("A punch collided with something on the Characters layer.");
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null)
			{
				Debug.Log("A punch collided with a student.");
				if (component.StudentID > 1)
				{
					Debug.Log("A punch collided with a student and killed them.");
					UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, component.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
					component.DeathType = DeathType.EasterEgg;
					component.BecomeRagdoll();
					Rigidbody rigidbody = component.Ragdoll.AllRigidbodies[0];
					rigidbody.isKinematic = false;
					Vector3 a = rigidbody.transform.position - component.Yandere.transform.position;
					if (this.Falcon)
					{
						rigidbody.AddForce(a * this.Strength);
					}
					else if (this.Bancho)
					{
						rigidbody.AddForce(a.x * this.Strength, 5000f, a.z * this.Strength);
					}
					else
					{
						rigidbody.AddForce(a.x * this.Strength, 10000f, a.z * this.Strength);
					}
				}
			}
		}
		if (this.Destructive && other.gameObject.layer != 2 && other.gameObject.layer != 8 && other.gameObject.layer != 9 && other.gameObject.layer != 13 && other.gameObject.layer != 17)
		{
			GameObject gameObject = null;
			StudentScript component2 = other.gameObject.transform.root.GetComponent<StudentScript>();
			if (component2 != null)
			{
				if (component2.StudentID <= 1)
				{
					gameObject = component2.gameObject;
				}
			}
			else
			{
				gameObject = other.gameObject;
			}
			if (gameObject != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.FalconExplosion, base.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
				UnityEngine.Object.Destroy(gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04001E74 RID: 7796
	public GameObject FalconExplosion;

	// Token: 0x04001E75 RID: 7797
	public Rigidbody MyRigidbody;

	// Token: 0x04001E76 RID: 7798
	public Collider MyCollider;

	// Token: 0x04001E77 RID: 7799
	public float Strength = 100f;

	// Token: 0x04001E78 RID: 7800
	public float Speed = 100f;

	// Token: 0x04001E79 RID: 7801
	public bool Destructive;

	// Token: 0x04001E7A RID: 7802
	public bool IgnoreTime;

	// Token: 0x04001E7B RID: 7803
	public bool Shipgirl;

	// Token: 0x04001E7C RID: 7804
	public bool Bancho;

	// Token: 0x04001E7D RID: 7805
	public bool Falcon;

	// Token: 0x04001E7E RID: 7806
	public bool Mecha;

	// Token: 0x04001E7F RID: 7807
	public float TimeLimit = 0.5f;

	// Token: 0x04001E80 RID: 7808
	public float Timer;
}
