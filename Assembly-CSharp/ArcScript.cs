using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
public class ArcScript : MonoBehaviour
{
	// Token: 0x0600170B RID: 5899 RVA: 0x000B2674 File Offset: 0x000B0A74
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 1f)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArcTrail, base.transform.position, base.transform.rotation);
			gameObject.GetComponent<Rigidbody>().AddRelativeForce(ArcScript.NEW_ARC_RELATIVE_FORCE);
			this.Timer = 0f;
		}
	}

	// Token: 0x04001644 RID: 5700
	private static readonly Vector3 NEW_ARC_RELATIVE_FORCE = Vector3.forward * 250f;

	// Token: 0x04001645 RID: 5701
	public GameObject ArcTrail;

	// Token: 0x04001646 RID: 5702
	public float Timer;
}
