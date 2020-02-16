using System;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class MatchScript : MonoBehaviour
{
	// Token: 0x06001D9A RID: 7578 RVA: 0x00118580 File Offset: 0x00116980
	private void Update()
	{
		if (base.GetComponent<Rigidbody>().useGravity)
		{
			base.transform.Rotate(Vector3.right * (Time.deltaTime * 360f));
			if (this.Timer > 0f && this.MyCollider.isTrigger)
			{
				this.MyCollider.isTrigger = false;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z - Time.deltaTime);
				if (base.transform.localScale.z < 0f)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400251A RID: 9498
	public float Timer;

	// Token: 0x0400251B RID: 9499
	public Collider MyCollider;
}
