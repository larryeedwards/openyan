using System;
using UnityEngine;

// Token: 0x020005CF RID: 1487
public class RoseSpawnerScript : MonoBehaviour
{
	// Token: 0x0600238A RID: 9098 RVA: 0x001C19A3 File Offset: 0x001BFDA3
	private void Start()
	{
		this.SpawnRose();
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x001C19AB File Offset: 0x001BFDAB
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.1f)
		{
			this.SpawnRose();
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x001C19D8 File Offset: 0x001BFDD8
	private void SpawnRose()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Rose, base.transform.position, Quaternion.identity);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.ForwardForce);
		gameObject.GetComponent<Rigidbody>().AddForce(base.transform.up * this.UpwardForce);
		gameObject.transform.localEulerAngles = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		base.transform.localPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), base.transform.localPosition.y, base.transform.localPosition.z);
		base.transform.LookAt(this.DramaGirl);
		this.Timer = 0f;
	}

	// Token: 0x04003D84 RID: 15748
	public Transform DramaGirl;

	// Token: 0x04003D85 RID: 15749
	public Transform Target;

	// Token: 0x04003D86 RID: 15750
	public GameObject Rose;

	// Token: 0x04003D87 RID: 15751
	public float Timer;

	// Token: 0x04003D88 RID: 15752
	public float ForwardForce;

	// Token: 0x04003D89 RID: 15753
	public float UpwardForce;
}
