using System;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public class YanvaniaWineScript : MonoBehaviour
{
	// Token: 0x06002330 RID: 9008 RVA: 0x001BBB38 File Offset: 0x001B9F38
	private void Update()
	{
		if (base.transform.parent == null)
		{
			this.Rotation += Time.deltaTime * 360f;
			base.transform.localEulerAngles = new Vector3(this.Rotation, this.Rotation, this.Rotation);
			if (base.transform.position.y < 6.5f)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Shards, new Vector3(base.transform.position.x, 6.5f, base.transform.position.z), Quaternion.identity);
				gameObject.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
				AudioSource.PlayClipAtPoint(base.GetComponent<AudioSource>().clip, base.transform.position);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04003C85 RID: 15493
	public GameObject Shards;

	// Token: 0x04003C86 RID: 15494
	public float Rotation;
}
