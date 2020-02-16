using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public class PhonePromptBarScript : MonoBehaviour
{
	// Token: 0x06001E4D RID: 7757 RVA: 0x00127AA8 File Offset: 0x00125EA8
	private void Start()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, 630f, base.transform.localPosition.z);
		this.Panel.enabled = false;
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x00127AFC File Offset: 0x00125EFC
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 631f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < 630f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, 631f, base.transform.localPosition.z);
					this.Panel.enabled = false;
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 530f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x04002716 RID: 10006
	public UIPanel Panel;

	// Token: 0x04002717 RID: 10007
	public bool Show;

	// Token: 0x04002718 RID: 10008
	public UILabel Label;
}
