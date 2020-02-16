using System;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class MGPMProjectileScript : MonoBehaviour
{
	// Token: 0x06000BD8 RID: 3032 RVA: 0x0005B4AC File Offset: 0x000598AC
	private void Update()
	{
		if (base.gameObject.layer == 8)
		{
			base.transform.Translate(Vector3.up * Time.deltaTime * this.Speed);
		}
		else
		{
			base.transform.Translate(Vector3.forward * Time.deltaTime * this.Speed);
		}
		if (this.Angle == 1)
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * this.Speed * 0.2f);
		}
		else if (this.Angle == -1)
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * this.Speed * -0.2f);
		}
		if (base.transform.localPosition.y > 300f || base.transform.localPosition.y < -300f || base.transform.localPosition.x > 134f || base.transform.localPosition.x < -134f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000945 RID: 2373
	public Transform Sprite;

	// Token: 0x04000946 RID: 2374
	public int Angle;

	// Token: 0x04000947 RID: 2375
	public float Speed;
}
