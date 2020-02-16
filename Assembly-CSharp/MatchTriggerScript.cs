using System;
using UnityEngine;

// Token: 0x0200045D RID: 1117
public class MatchTriggerScript : MonoBehaviour
{
	// Token: 0x06001D9C RID: 7580 RVA: 0x0011868C File Offset: 0x00116A8C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student == null)
			{
				GameObject gameObject = other.gameObject.transform.root.gameObject;
				this.Student = gameObject.GetComponent<StudentScript>();
			}
			if (this.Student != null && (this.Student.Gas || this.Fireball))
			{
				this.Student.Combust();
				if (this.PickUp != null && this.PickUp.Yandere.PickUp != null && this.PickUp.Yandere.PickUp == this.PickUp)
				{
					this.PickUp.Yandere.TargetStudent = this.Student;
					this.PickUp.Yandere.MurderousActionTimer = 1f;
				}
				if (this.Fireball)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400251C RID: 9500
	public PickUpScript PickUp;

	// Token: 0x0400251D RID: 9501
	public StudentScript Student;

	// Token: 0x0400251E RID: 9502
	public bool Fireball;
}
