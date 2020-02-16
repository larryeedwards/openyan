using System;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class PromptSwapScript : MonoBehaviour
{
	// Token: 0x06001EE2 RID: 7906 RVA: 0x0013845F File Offset: 0x0013685F
	private void Awake()
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x00138480 File Offset: 0x00136880
	public void UpdateSpriteType(InputDeviceType deviceType)
	{
		if (this.InputDevice == null)
		{
			this.InputDevice = UnityEngine.Object.FindObjectOfType<InputDeviceScript>();
		}
		if (deviceType == InputDeviceType.Gamepad)
		{
			this.MySprite.spriteName = this.GamepadName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = string.Empty;
			}
		}
		else
		{
			this.MySprite.spriteName = this.KeyboardName;
			if (this.MyLetter != null)
			{
				this.MyLetter.text = this.KeyboardLetter;
			}
		}
	}

	// Token: 0x040028EB RID: 10475
	public InputDeviceScript InputDevice;

	// Token: 0x040028EC RID: 10476
	public UISprite MySprite;

	// Token: 0x040028ED RID: 10477
	public UILabel MyLetter;

	// Token: 0x040028EE RID: 10478
	public string KeyboardLetter = string.Empty;

	// Token: 0x040028EF RID: 10479
	public string KeyboardName = string.Empty;

	// Token: 0x040028F0 RID: 10480
	public string GamepadName = string.Empty;
}
