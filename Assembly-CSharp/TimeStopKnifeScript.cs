using System;
using UnityEngine;

// Token: 0x0200054D RID: 1357
public class TimeStopKnifeScript : MonoBehaviour
{
	// Token: 0x0600218B RID: 8587 RVA: 0x001959EC File Offset: 0x00193DEC
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x00195A10 File Offset: 0x00193E10
	private void Update()
	{
		if (!this.Unfreeze)
		{
			this.Speed = Mathf.MoveTowards(this.Speed, 0f, Time.deltaTime);
			if (base.transform.localScale.x < 0.99f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
		}
		else
		{
			this.Speed = 10f;
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		base.transform.Translate(Vector3.forward * this.Speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x00195B00 File Offset: 0x00193F00
	private void OnTriggerEnter(Collider other)
	{
		if (this.Unfreeze && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (component.Male)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.MaleScream, base.transform.position, Quaternion.identity);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.FemaleScream, base.transform.position, Quaternion.identity);
				}
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
			}
		}
	}

	// Token: 0x04003630 RID: 13872
	public GameObject FemaleScream;

	// Token: 0x04003631 RID: 13873
	public GameObject MaleScream;

	// Token: 0x04003632 RID: 13874
	public bool Unfreeze;

	// Token: 0x04003633 RID: 13875
	public float Speed = 0.1f;

	// Token: 0x04003634 RID: 13876
	private float Timer;
}
