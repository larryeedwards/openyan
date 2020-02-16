using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x06000C58 RID: 3160 RVA: 0x000672F4 File Offset: 0x000656F4
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00067398 File Offset: 0x00065798
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = string.Empty;
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x04000AF8 RID: 2808
	public UITextList textList;

	// Token: 0x04000AF9 RID: 2809
	public bool fillWithDummyData;

	// Token: 0x04000AFA RID: 2810
	private UIInput mInput;
}
