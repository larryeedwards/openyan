using System;
using UnityEngine;

// Token: 0x020005AA RID: 1450
public class YanvaniaJarScript : MonoBehaviour
{
	// Token: 0x06002315 RID: 8981 RVA: 0x001B980C File Offset: 0x001B7C0C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 19 && !this.Destroyed)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position + Vector3.up * 0.5f, Quaternion.identity);
			this.Destroyed = true;
			AudioClipPlayer.Play2D(this.Break, base.transform.position);
			for (int i = 1; i < 11; i++)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.Shard, base.transform.position + Vector3.up * UnityEngine.Random.Range(0f, 1f) + Vector3.right * UnityEngine.Random.Range(-0.5f, 0.5f), Quaternion.identity);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003C37 RID: 15415
	public GameObject Explosion;

	// Token: 0x04003C38 RID: 15416
	public bool Destroyed;

	// Token: 0x04003C39 RID: 15417
	public AudioClip Break;

	// Token: 0x04003C3A RID: 15418
	public GameObject Shard;
}
