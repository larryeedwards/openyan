using System;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class Proxies : MonoBehaviour
{
	// Token: 0x06000ADB RID: 2779 RVA: 0x00053970 File Offset: 0x00051D70
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.GetComponent<MeshRenderer>());
		UnityEngine.Object.Destroy(base.GetComponent<MeshFilter>());
		UnityEngine.Object.Destroy(this);
	}
}
