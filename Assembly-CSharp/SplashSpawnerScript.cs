using System;
using UnityEngine;

// Token: 0x02000518 RID: 1304
public class SplashSpawnerScript : MonoBehaviour
{
	// Token: 0x06002032 RID: 8242 RVA: 0x0014EDE0 File Offset: 0x0014D1E0
	private void Update()
	{
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			this.FootUp = false;
			if (this.Bloody)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodSplash, new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(-90f, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				this.Bloody = false;
			}
		}
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0014EF1E File Offset: 0x0014D31E
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.Bloody = true;
		}
	}

	// Token: 0x04002CEA RID: 11498
	public GameObject BloodSplash;

	// Token: 0x04002CEB RID: 11499
	public Transform Yandere;

	// Token: 0x04002CEC RID: 11500
	public bool Bloody;

	// Token: 0x04002CED RID: 11501
	public bool FootUp;

	// Token: 0x04002CEE RID: 11502
	public float DownThreshold;

	// Token: 0x04002CEF RID: 11503
	public float UpThreshold;

	// Token: 0x04002CF0 RID: 11504
	public float Height;
}
