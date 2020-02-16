using System;
using UnityEngine;

// Token: 0x0200044C RID: 1100
[RequireComponent(typeof(Camera))]
public class Letterboxing : MonoBehaviour
{
	// Token: 0x06001D69 RID: 7529 RVA: 0x001138A4 File Offset: 0x00111CA4
	private void Start()
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 1f - num / 1.77777779f;
		base.GetComponent<Camera>().rect = new Rect(0f, num2 / 2f, 1f, 1f - num2);
	}

	// Token: 0x04002471 RID: 9329
	private const float KEEP_ASPECT = 1.77777779f;
}
