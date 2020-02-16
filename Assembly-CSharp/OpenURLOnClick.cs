using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class OpenURLOnClick : MonoBehaviour
{
	// Token: 0x06000C73 RID: 3187 RVA: 0x0006820C File Offset: 0x0006660C
	private void OnClick()
	{
		UILabel component = base.GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.lastWorldPosition);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
