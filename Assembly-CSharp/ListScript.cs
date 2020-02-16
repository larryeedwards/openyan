using System;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class ListScript : MonoBehaviour
{
	// Token: 0x06001D75 RID: 7541 RVA: 0x00114730 File Offset: 0x00112B30
	public void Start()
	{
		if (this.AutoFill)
		{
			for (int i = 1; i < this.List.Length; i++)
			{
				this.List[i] = base.transform.GetChild(i - 1);
			}
		}
	}

	// Token: 0x040024A2 RID: 9378
	public Transform[] List;

	// Token: 0x040024A3 RID: 9379
	public bool AutoFill;
}
