using System;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class PodScript : MonoBehaviour
{
	// Token: 0x06001E85 RID: 7813 RVA: 0x0012CD40 File Offset: 0x0012B140
	private void Start()
	{
		this.Timer = 1f;
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x0012CD50 File Offset: 0x0012B150
	private void LateUpdate()
	{
		this.PodTarget.transform.parent.eulerAngles = new Vector3(0f, this.AimTarget.parent.eulerAngles.y, 0f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.PodTarget.position, Time.deltaTime * 100f);
		base.transform.rotation = this.AimTarget.parent.rotation;
		base.transform.eulerAngles += new Vector3(-15f, 7.5f, 0f);
		if (Input.GetButton("RB"))
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > this.FireRate)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, base.transform.rotation);
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x040027AC RID: 10156
	public GameObject Projectile;

	// Token: 0x040027AD RID: 10157
	public Transform SpawnPoint;

	// Token: 0x040027AE RID: 10158
	public Transform PodTarget;

	// Token: 0x040027AF RID: 10159
	public Transform AimTarget;

	// Token: 0x040027B0 RID: 10160
	public float FireRate;

	// Token: 0x040027B1 RID: 10161
	public float Timer;
}
