using System;
using UnityEngine;

// Token: 0x020005CC RID: 1484
public class FoldingChairScript : MonoBehaviour
{
	// Token: 0x06002382 RID: 9090 RVA: 0x001C185C File Offset: 0x001BFC5C
	private void Start()
	{
		int num = UnityEngine.Random.Range(0, this.Student.Length);
		UnityEngine.Object.Instantiate<GameObject>(this.Student[num], base.transform.position - new Vector3(0f, 0.4f, 0f), base.transform.rotation);
	}

	// Token: 0x04003D81 RID: 15745
	public GameObject[] Student;
}
