using System;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class HideColliderScript : MonoBehaviour
{
	// Token: 0x06001C82 RID: 7298 RVA: 0x00101C34 File Offset: 0x00100034
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			GameObject gameObject = other.gameObject.transform.root.gameObject;
			if (!gameObject.GetComponent<StudentScript>().Alive)
			{
				this.Corpse = gameObject.GetComponent<RagdollScript>();
				if (!this.Corpse.Hidden)
				{
					this.Corpse.HideCollider = this.MyCollider;
					this.Corpse.Police.HiddenCorpses++;
					this.Corpse.Hidden = true;
				}
			}
		}
	}

	// Token: 0x0400213E RID: 8510
	public RagdollScript Corpse;

	// Token: 0x0400213F RID: 8511
	public Collider MyCollider;
}
