using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class FallCheckerScript : MonoBehaviour
{
	// Token: 0x06001995 RID: 6549 RVA: 0x000EEC70 File Offset: 0x000ED070
	private void OnTriggerEnter(Collider other)
	{
		if (this.Ragdoll == null && other.gameObject.layer == 11)
		{
			this.Ragdoll = other.transform.root.gameObject.GetComponent<RagdollScript>();
			this.Ragdoll.Prompt.Hide();
			this.Ragdoll.Prompt.enabled = false;
			this.Ragdoll.Prompt.MyCollider.enabled = false;
			this.Ragdoll.BloodPoolSpawner.enabled = false;
			this.Ragdoll.HideCollider = this.MyCollider;
			this.Ragdoll.Police.HiddenCorpses++;
			this.Ragdoll.Hidden = true;
			this.Dumpster.Corpse = this.Ragdoll.gameObject;
			this.Dumpster.Victim = this.Ragdoll.Student;
		}
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x000EED64 File Offset: 0x000ED164
	private void Update()
	{
		if (this.Ragdoll != null)
		{
			if (this.Ragdoll.Prompt.transform.localPosition.y > -10.5f)
			{
				this.Ragdoll.Prompt.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
				this.Ragdoll.AllColliders[2].transform.localEulerAngles = Vector3.zero;
				this.Ragdoll.AllColliders[7].transform.localEulerAngles = new Vector3(0f, 0f, -80f);
				this.Ragdoll.Prompt.transform.position = new Vector3(this.Dumpster.transform.position.x, this.Ragdoll.Prompt.transform.position.y, this.Dumpster.transform.position.z);
			}
			else
			{
				base.GetComponent<AudioSource>().Play();
				this.Dumpster.Slide = true;
				this.Ragdoll = null;
			}
		}
	}

	// Token: 0x04001E81 RID: 7809
	public DumpsterLidScript Dumpster;

	// Token: 0x04001E82 RID: 7810
	public RagdollScript Ragdoll;

	// Token: 0x04001E83 RID: 7811
	public Collider MyCollider;
}
