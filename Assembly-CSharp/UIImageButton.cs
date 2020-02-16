using System;
using UnityEngine;

// Token: 0x020001C5 RID: 453
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0006DA9C File Offset: 0x0006BE9C
	// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0006DACC File Offset: 0x0006BECC
	public bool isEnabled
	{
		get
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			return component && component.enabled;
		}
		set
		{
			Collider component = base.gameObject.GetComponent<Collider>();
			if (!component)
			{
				return;
			}
			if (component.enabled != value)
			{
				component.enabled = value;
				this.UpdateImage();
			}
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0006DB0A File Offset: 0x0006BF0A
	private void OnEnable()
	{
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<UISprite>();
		}
		this.UpdateImage();
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0006DB30 File Offset: 0x0006BF30
	private void OnValidate()
	{
		if (this.target != null)
		{
			if (string.IsNullOrEmpty(this.normalSprite))
			{
				this.normalSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.hoverSprite))
			{
				this.hoverSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.pressedSprite))
			{
				this.pressedSprite = this.target.spriteName;
			}
			if (string.IsNullOrEmpty(this.disabledSprite))
			{
				this.disabledSprite = this.target.spriteName;
			}
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0006DBD4 File Offset: 0x0006BFD4
	private void UpdateImage()
	{
		if (this.target != null)
		{
			if (this.isEnabled)
			{
				this.SetSprite((!UICamera.IsHighlighted(base.gameObject)) ? this.normalSprite : this.hoverSprite);
			}
			else
			{
				this.SetSprite(this.disabledSprite);
			}
		}
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0006DC35 File Offset: 0x0006C035
	private void OnHover(bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			this.SetSprite((!isOver) ? this.normalSprite : this.hoverSprite);
		}
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0006DC70 File Offset: 0x0006C070
	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			this.SetSprite(this.pressedSprite);
		}
		else
		{
			this.UpdateImage();
		}
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0006DC90 File Offset: 0x0006C090
	private void SetSprite(string sprite)
	{
		if (this.target.atlas == null || this.target.atlas.GetSprite(sprite) == null)
		{
			return;
		}
		this.target.spriteName = sprite;
		if (this.pixelSnap)
		{
			this.target.MakePixelPerfect();
		}
	}

	// Token: 0x04000C1C RID: 3100
	public UISprite target;

	// Token: 0x04000C1D RID: 3101
	public string normalSprite;

	// Token: 0x04000C1E RID: 3102
	public string hoverSprite;

	// Token: 0x04000C1F RID: 3103
	public string pressedSprite;

	// Token: 0x04000C20 RID: 3104
	public string disabledSprite;

	// Token: 0x04000C21 RID: 3105
	public bool pixelSnap = true;
}
