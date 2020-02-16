using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class ARMiyukiScript : MonoBehaviour
{
	// Token: 0x0600171B RID: 5915 RVA: 0x000B40A2 File Offset: 0x000B24A2
	private void Start()
	{
		if (this.Enemy == null)
		{
			this.Enemy = this.MyStudent.StudentManager.MiyukiCat;
		}
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000B40CC File Offset: 0x000B24CC
	private void Update()
	{
		if (!this.Student && this.Yandere.AR)
		{
			base.transform.LookAt(this.Enemy.position);
			if (Input.GetButtonDown("X"))
			{
				this.Shoot();
			}
		}
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000B4120 File Offset: 0x000B2520
	public void Shoot()
	{
		if (this.Enemy == null)
		{
			this.Enemy = this.MyStudent.StudentManager.MiyukiCat;
		}
		base.transform.LookAt(this.Enemy.position);
		UnityEngine.Object.Instantiate<GameObject>(this.Bullet, this.BulletSpawnPoint.position, base.transform.rotation);
	}

	// Token: 0x0400166F RID: 5743
	public Transform BulletSpawnPoint;

	// Token: 0x04001670 RID: 5744
	public StudentScript MyStudent;

	// Token: 0x04001671 RID: 5745
	public YandereScript Yandere;

	// Token: 0x04001672 RID: 5746
	public GameObject Bullet;

	// Token: 0x04001673 RID: 5747
	public Transform Enemy;

	// Token: 0x04001674 RID: 5748
	public GameObject MagicalGirl;

	// Token: 0x04001675 RID: 5749
	public bool Student;
}
