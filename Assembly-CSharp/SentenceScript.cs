using System;
using UnityEngine;

// Token: 0x02000502 RID: 1282
public class SentenceScript : MonoBehaviour
{
	// Token: 0x06001FDD RID: 8157 RVA: 0x0014679C File Offset: 0x00144B9C
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.Sentence.text = this.Words[this.ID];
			this.ID++;
		}
	}

	// Token: 0x04002BCA RID: 11210
	public UILabel Sentence;

	// Token: 0x04002BCB RID: 11211
	public string[] Words;

	// Token: 0x04002BCC RID: 11212
	public int ID;
}
