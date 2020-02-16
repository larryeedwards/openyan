using System;
using UnityEngine;

// Token: 0x020005A4 RID: 1444
public class YanvaniaCandlestickHeadScript : MonoBehaviour
{
	// Token: 0x06002300 RID: 8960 RVA: 0x001B7AFC File Offset: 0x001B5EFC
	private void Start()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.AddForce(base.transform.up * 100f);
		component.AddForce(base.transform.right * 100f);
		this.Value = UnityEngine.Random.Range(-1f, 1f);
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x001B7B5C File Offset: 0x001B5F5C
	private void Update()
	{
		this.Rotation += new Vector3(this.Value, this.Value, this.Value);
		base.transform.localEulerAngles = this.Rotation;
		if (base.transform.localPosition.y < 0.23f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Fire, base.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BF3 RID: 15347
	public GameObject Fire;

	// Token: 0x04003BF4 RID: 15348
	public Vector3 Rotation;

	// Token: 0x04003BF5 RID: 15349
	public float Value;
}
