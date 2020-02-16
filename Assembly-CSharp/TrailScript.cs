using System;
using UnityEngine;

// Token: 0x02000557 RID: 1367
public class TrailScript : MonoBehaviour
{
	// Token: 0x060021B8 RID: 8632 RVA: 0x00198930 File Offset: 0x00196D30
	private void Start()
	{
		GameObject gameObject = GameObject.Find("YandereChan");
		Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), base.GetComponent<Collider>());
		UnityEngine.Object.Destroy(this);
	}
}
