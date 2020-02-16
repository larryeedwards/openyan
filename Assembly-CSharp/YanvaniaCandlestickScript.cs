using System;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class YanvaniaCandlestickScript : MonoBehaviour
{
	// Token: 0x06002303 RID: 8963 RVA: 0x001B7BF0 File Offset: 0x001B5FF0
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DestroyedCandlestick, base.transform.position, Quaternion.identity);
			gameObject.transform.localScale = base.transform.localScale;
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BF6 RID: 15350
	public GameObject DestroyedCandlestick;

	// Token: 0x04003BF7 RID: 15351
	public bool Destroyed;

	// Token: 0x04003BF8 RID: 15352
	public AudioClip Break;
}
