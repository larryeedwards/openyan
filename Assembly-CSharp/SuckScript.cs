using System;
using UnityEngine;

// Token: 0x02000539 RID: 1337
public class SuckScript : MonoBehaviour
{
	// Token: 0x06002145 RID: 8517 RVA: 0x0018B5F8 File Offset: 0x001899F8
	private void Update()
	{
		this.Strength += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f, Time.deltaTime * this.Strength);
		if (Vector3.Distance(base.transform.position, this.Student.Yandere.Hips.position + base.transform.up * 0.25f) < 1f)
		{
			base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, Vector3.zero, Time.deltaTime);
			if (base.transform.localScale == Vector3.zero)
			{
				base.transform.parent.parent.parent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04003548 RID: 13640
	public StudentScript Student;

	// Token: 0x04003549 RID: 13641
	public float Strength;
}
