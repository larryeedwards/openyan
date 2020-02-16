using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000CC8 RID: 3272 RVA: 0x0006A88D File Offset: 0x00068C8D
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0006A896 File Offset: 0x00068C96
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0006A8B4 File Offset: 0x00068CB4
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0006A8EB File Offset: 0x00068CEB
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0006A922 File Offset: 0x00068D22
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0006A947 File Offset: 0x00068D47
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0006A965 File Offset: 0x00068D65
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0006A984 File Offset: 0x00068D84
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04000B71 RID: 2929
	public GameObject target;

	// Token: 0x04000B72 RID: 2930
	public string functionName;

	// Token: 0x04000B73 RID: 2931
	public UIButtonMessage.Trigger trigger;

	// Token: 0x04000B74 RID: 2932
	public bool includeChildren;

	// Token: 0x04000B75 RID: 2933
	private bool mStarted;

	// Token: 0x020001AE RID: 430
	public enum Trigger
	{
		// Token: 0x04000B77 RID: 2935
		OnClick,
		// Token: 0x04000B78 RID: 2936
		OnMouseOver,
		// Token: 0x04000B79 RID: 2937
		OnMouseOut,
		// Token: 0x04000B7A RID: 2938
		OnPress,
		// Token: 0x04000B7B RID: 2939
		OnRelease,
		// Token: 0x04000B7C RID: 2940
		OnDoubleClick
	}
}
