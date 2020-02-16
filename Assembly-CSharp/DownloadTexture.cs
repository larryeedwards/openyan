using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000193 RID: 403
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x06000C5B RID: 3163 RVA: 0x00067414 File Offset: 0x00065814
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture component = base.GetComponent<UITexture>();
			component.mainTexture = this.mTex;
			if (this.pixelPerfect)
			{
				component.MakePixelPerfect();
			}
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0006742F File Offset: 0x0006582F
	private void OnDestroy()
	{
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	// Token: 0x04000AFB RID: 2811
	public string url = "http://www.yourwebsite.com/logo.png";

	// Token: 0x04000AFC RID: 2812
	public bool pixelPerfect = true;

	// Token: 0x04000AFD RID: 2813
	private Texture2D mTex;
}
