using System;
using UnityEngine;

// Token: 0x0200048C RID: 1164
public class TextMessageScript : MonoBehaviour
{
	// Token: 0x06001E3F RID: 7743 RVA: 0x00126553 File Offset: 0x00124953
	private void Start()
	{
		if (!this.Attachment && this.Image != null)
		{
			this.Image.SetActive(false);
		}
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0012657D File Offset: 0x0012497D
	private void Update()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
	}

	// Token: 0x040026EE RID: 9966
	public UILabel Label;

	// Token: 0x040026EF RID: 9967
	public GameObject Image;

	// Token: 0x040026F0 RID: 9968
	public bool Attachment;
}
