using System;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class HomeYandereDetectorScript : MonoBehaviour
{
	// Token: 0x06001CDB RID: 7387 RVA: 0x00109683 File Offset: 0x00107A83
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = true;
		}
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x001096A1 File Offset: 0x00107AA1
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			this.YandereDetected = false;
		}
	}

	// Token: 0x04002298 RID: 8856
	public bool YandereDetected;
}
