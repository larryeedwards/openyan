using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0006E9B4 File Offset: 0x0006CDB4
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0006E9EA File Offset: 0x0006CDEA
	private void OnEnable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnEnable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0006EA10 File Offset: 0x0006CE10
	private void OnDisable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnDisable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0006EA38 File Offset: 0x0006CE38
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x0006EAAC File Offset: 0x0006CEAC
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0006EB20 File Offset: 0x0006CF20
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0006EB50 File Offset: 0x0006CF50
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0006EB75 File Offset: 0x0006CF75
	public void Play()
	{
		NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
	}

	// Token: 0x04000C53 RID: 3155
	public AudioClip audioClip;

	// Token: 0x04000C54 RID: 3156
	public UIPlaySound.Trigger trigger;

	// Token: 0x04000C55 RID: 3157
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x04000C56 RID: 3158
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x04000C57 RID: 3159
	private bool mIsOver;

	// Token: 0x020001CD RID: 461
	public enum Trigger
	{
		// Token: 0x04000C59 RID: 3161
		OnClick,
		// Token: 0x04000C5A RID: 3162
		OnMouseOver,
		// Token: 0x04000C5B RID: 3163
		OnMouseOut,
		// Token: 0x04000C5C RID: 3164
		OnPress,
		// Token: 0x04000C5D RID: 3165
		OnRelease,
		// Token: 0x04000C5E RID: 3166
		Custom,
		// Token: 0x04000C5F RID: 3167
		OnEnable,
		// Token: 0x04000C60 RID: 3168
		OnDisable
	}
}
