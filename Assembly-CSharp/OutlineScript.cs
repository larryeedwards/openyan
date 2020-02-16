using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x02000481 RID: 1153
public class OutlineScript : MonoBehaviour
{
	// Token: 0x06001E1F RID: 7711 RVA: 0x00123203 File Offset: 0x00121603
	public void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x00123233 File Offset: 0x00121633
	private void Update()
	{
		this.h.ConstantOnImmediate(this.color);
	}

	// Token: 0x04002672 RID: 9842
	public YandereScript Yandere;

	// Token: 0x04002673 RID: 9843
	public Highlighter h;

	// Token: 0x04002674 RID: 9844
	public Color color = new Color(1f, 1f, 1f, 1f);
}
