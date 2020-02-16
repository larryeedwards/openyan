using System;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class PromptBarScript : MonoBehaviour
{
	// Token: 0x06001ECF RID: 7887 RVA: 0x001366F0 File Offset: 0x00134AF0
	private void Awake()
	{
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, -627f, base.transform.localPosition.z);
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.ID++;
		}
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x0013677C File Offset: 0x00134B7C
	private void Start()
	{
		this.UpdateButtons();
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00136784 File Offset: 0x00134B84
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Panel.enabled)
			{
				base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -628f, t), base.transform.localPosition.z);
				if (base.transform.localPosition.y < -627f)
				{
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, -628f, base.transform.localPosition.z);
					if (this.Panel != null)
					{
						this.Panel.enabled = false;
					}
				}
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, -528.5f, t), base.transform.localPosition.z);
		}
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x001368E0 File Offset: 0x00134CE0
	public void UpdateButtons()
	{
		if (this.Panel != null)
		{
			this.Panel.enabled = true;
		}
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Button[this.ID].enabled = (this.Label[this.ID].text.Length > 0);
			this.ID++;
		}
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00136964 File Offset: 0x00134D64
	public void ClearButtons()
	{
		this.ID = 0;
		while (this.ID < this.Label.Length)
		{
			this.Label[this.ID].text = string.Empty;
			this.Button[this.ID].enabled = false;
			this.ID++;
		}
	}

	// Token: 0x040028A8 RID: 10408
	public UISprite[] Button;

	// Token: 0x040028A9 RID: 10409
	public UILabel[] Label;

	// Token: 0x040028AA RID: 10410
	public UIPanel Panel;

	// Token: 0x040028AB RID: 10411
	public bool Show;

	// Token: 0x040028AC RID: 10412
	public int ID;
}
