using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
[RequireComponent(typeof(UIPopupList))]
[AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000C95 RID: 3221 RVA: 0x00068CDB File Offset: 0x000670DB
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.Refresh();
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x00068CEF File Offset: 0x000670EF
	private void Start()
	{
		EventDelegate.Add(this.mList.onChange, delegate()
		{
			Localization.language = UIPopupList.current.value;
		});
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x00068D20 File Offset: 0x00067120
	public void Refresh()
	{
		if (this.mList != null && Localization.knownLanguages != null)
		{
			this.mList.Clear();
			int i = 0;
			int num = Localization.knownLanguages.Length;
			while (i < num)
			{
				this.mList.items.Add(Localization.knownLanguages[i]);
				i++;
			}
			this.mList.value = Localization.language;
		}
	}

	// Token: 0x04000B37 RID: 2871
	private UIPopupList mList;
}
