using System;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class DoorOpenerScript : MonoBehaviour
{
	// Token: 0x060018E4 RID: 6372 RVA: 0x000E45C4 File Offset: 0x000E29C4
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			this.Student = other.gameObject.GetComponent<StudentScript>();
			if (this.Student != null && !this.Student.Dying && !this.Door.Open && !this.Door.Locked)
			{
				this.Door.Student = this.Student;
				this.Door.OpenDoor();
			}
		}
	}

	// Token: 0x04001CAA RID: 7338
	public StudentScript Student;

	// Token: 0x04001CAB RID: 7339
	public DoorScript Door;
}
