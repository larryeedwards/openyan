using System;
using UnityEngine;

// Token: 0x020003AF RID: 943
public class EmergencyExitScript : MonoBehaviour
{
	// Token: 0x0600192C RID: 6444 RVA: 0x000E8460 File Offset: 0x000E6860
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, base.transform.position) < 2f)
		{
			this.Open = true;
		}
		else if (this.Timer == 0f)
		{
			this.Student = null;
			this.Open = false;
		}
		if (!this.Open)
		{
			this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 0f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
		}
		else
		{
			this.Pivot.localEulerAngles = new Vector3(this.Pivot.localEulerAngles.x, Mathf.Lerp(this.Pivot.localEulerAngles.y, 90f, Time.deltaTime * 10f), this.Pivot.localEulerAngles.z);
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
		}
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x000E85A5 File Offset: 0x000E69A5
	private void OnTriggerStay(Collider other)
	{
		this.Student = other.gameObject.GetComponent<StudentScript>();
		if (this.Student != null)
		{
			this.Timer = 1f;
			this.Open = true;
		}
	}

	// Token: 0x04001D3E RID: 7486
	public StudentScript Student;

	// Token: 0x04001D3F RID: 7487
	public Transform Yandere;

	// Token: 0x04001D40 RID: 7488
	public Transform Pivot;

	// Token: 0x04001D41 RID: 7489
	public float Timer;

	// Token: 0x04001D42 RID: 7490
	public bool Open;
}
