using System;
using UnityEngine;

// Token: 0x0200040D RID: 1037
public class HairBladeScript : MonoBehaviour
{
	// Token: 0x06001C64 RID: 7268 RVA: 0x000FD9FA File Offset: 0x000FBDFA
	private void Update()
	{
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x000FD9FC File Offset: 0x000FBDFC
	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = other.gameObject.transform.root.gameObject;
		if (gameObject.GetComponent<StudentScript>() != null)
		{
			this.Student = gameObject.GetComponent<StudentScript>();
			if (this.Student.StudentID != 1 && this.Student.Alive)
			{
				this.Student.DeathType = DeathType.EasterEgg;
				UnityEngine.Object.Instantiate<GameObject>((!this.Student.Male) ? this.FemaleBloodyScream : this.MaleBloodyScream, this.Student.transform.position + Vector3.up, Quaternion.identity);
				this.Student.BecomeRagdoll();
				this.Student.Ragdoll.Dismember();
				base.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x0400209A RID: 8346
	public GameObject FemaleBloodyScream;

	// Token: 0x0400209B RID: 8347
	public GameObject MaleBloodyScream;

	// Token: 0x0400209C RID: 8348
	public Vector3 PreviousPosition;

	// Token: 0x0400209D RID: 8349
	public Collider MyCollider;

	// Token: 0x0400209E RID: 8350
	public float Timer;

	// Token: 0x0400209F RID: 8351
	public StudentScript Student;
}
