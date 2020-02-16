using System;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class MGPMPickUpScript : MonoBehaviour
{
	// Token: 0x06000BD6 RID: 3030 RVA: 0x0005B440 File Offset: 0x00059840
	private void Update()
	{
		base.transform.Translate(Vector3.up * Time.deltaTime * this.Speed * -1f);
		if (base.transform.localPosition.y < -300f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000944 RID: 2372
	public float Speed;
}
