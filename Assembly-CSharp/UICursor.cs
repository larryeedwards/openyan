using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000C1A RID: 3098 RVA: 0x00065D71 File Offset: 0x00064171
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00065D79 File Offset: 0x00064179
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00065D84 File Offset: 0x00064184
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mSprite != null)
		{
			this.mAtlas = this.mSprite.atlas;
			this.mSpriteName = this.mSprite.spriteName;
			if (this.mSprite.depth < 100)
			{
				this.mSprite.depth = 100;
			}
		}
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00065E24 File Offset: 0x00064224
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.orthographic)
			{
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			this.mTrans.localPosition = mousePosition;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00065F4C File Offset: 0x0006434C
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00065F8C File Offset: 0x0006438C
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.instance != null && UICursor.instance.mSprite)
		{
			UICursor.instance.mSprite.atlas = atlas;
			UICursor.instance.mSprite.spriteName = sprite;
			UICursor.instance.mSprite.MakePixelPerfect();
			UICursor.instance.Update();
		}
	}

	// Token: 0x04000A9A RID: 2714
	public static UICursor instance;

	// Token: 0x04000A9B RID: 2715
	public Camera uiCamera;

	// Token: 0x04000A9C RID: 2716
	private Transform mTrans;

	// Token: 0x04000A9D RID: 2717
	private UISprite mSprite;

	// Token: 0x04000A9E RID: 2718
	private UIAtlas mAtlas;

	// Token: 0x04000A9F RID: 2719
	private string mSpriteName;
}
