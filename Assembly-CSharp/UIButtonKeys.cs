using System;
using UnityEngine;

// Token: 0x020001AC RID: 428
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x06000CC5 RID: 3269 RVA: 0x0006A727 File Offset: 0x00068B27
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0006A738 File Offset: 0x00068B38
	public void Upgrade()
	{
		if (this.onClick == null && this.selectOnClick != null)
		{
			this.onClick = this.selectOnClick.gameObject;
			this.selectOnClick = null;
			NGUITools.SetDirty(this);
		}
		if (this.onLeft == null && this.selectOnLeft != null)
		{
			this.onLeft = this.selectOnLeft.gameObject;
			this.selectOnLeft = null;
			NGUITools.SetDirty(this);
		}
		if (this.onRight == null && this.selectOnRight != null)
		{
			this.onRight = this.selectOnRight.gameObject;
			this.selectOnRight = null;
			NGUITools.SetDirty(this);
		}
		if (this.onUp == null && this.selectOnUp != null)
		{
			this.onUp = this.selectOnUp.gameObject;
			this.selectOnUp = null;
			NGUITools.SetDirty(this);
		}
		if (this.onDown == null && this.selectOnDown != null)
		{
			this.onDown = this.selectOnDown.gameObject;
			this.selectOnDown = null;
			NGUITools.SetDirty(this);
		}
	}

	// Token: 0x04000B6C RID: 2924
	public UIButtonKeys selectOnClick;

	// Token: 0x04000B6D RID: 2925
	public UIButtonKeys selectOnUp;

	// Token: 0x04000B6E RID: 2926
	public UIButtonKeys selectOnDown;

	// Token: 0x04000B6F RID: 2927
	public UIButtonKeys selectOnLeft;

	// Token: 0x04000B70 RID: 2928
	public UIButtonKeys selectOnRight;
}
