using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000131 RID: 305
public class ScrollingImage : MonoBehaviour
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x000529CC File Offset: 0x00050DCC
	private void Update()
	{
		this.scroll += Time.deltaTime * this.scrollSpeed;
		if (this.image != null)
		{
			this.image.uvRect = new Rect(this.scroll, this.scroll, 1f, 1f);
		}
	}

	// Token: 0x04000789 RID: 1929
	[SerializeField]
	private RawImage image;

	// Token: 0x0400078A RID: 1930
	[SerializeField]
	private float scrollSpeed;

	// Token: 0x0400078B RID: 1931
	private float scroll;
}
