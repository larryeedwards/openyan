using System;
using UnityEngine;

// Token: 0x020005AB RID: 1451
public class YanvaniaJarShardScript : MonoBehaviour
{
	// Token: 0x06002317 RID: 8983 RVA: 0x001B9904 File Offset: 0x001B7D04
	private void Start()
	{
		this.Rotation = UnityEngine.Random.Range(-360f, 360f);
		base.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(0f, 100f), UnityEngine.Random.Range(-100f, 100f));
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x001B9960 File Offset: 0x001B7D60
	private void Update()
	{
		this.MyRotation += this.Rotation;
		base.transform.eulerAngles = new Vector3(this.MyRotation, this.MyRotation, this.MyRotation);
		if (base.transform.position.y < 6.5f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003C3B RID: 15419
	public float MyRotation;

	// Token: 0x04003C3C RID: 15420
	public float Rotation;
}
