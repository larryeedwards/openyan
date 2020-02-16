using System;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class InputDeviceScript : MonoBehaviour
{
	// Token: 0x06001CF8 RID: 7416 RVA: 0x0010BAA0 File Offset: 0x00109EA0
	private void Update()
	{
		this.MouseDelta = Input.mousePosition - this.MousePrevious;
		this.MousePrevious = Input.mousePosition;
		InputDeviceType type = this.Type;
		if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || this.MouseDelta != Vector3.zero)
		{
			this.Type = InputDeviceType.MouseAndKeyboard;
		}
		if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9) || Input.GetKey(KeyCode.Joystick1Button10) || Input.GetKey(KeyCode.Joystick1Button11) || Input.GetKey(KeyCode.Joystick1Button12) || Input.GetKey(KeyCode.Joystick1Button13) || Input.GetKey(KeyCode.Joystick1Button14) || Input.GetKey(KeyCode.Joystick1Button15) || Input.GetKey(KeyCode.Joystick1Button16) || Input.GetKey(KeyCode.Joystick1Button17) || Input.GetKey(KeyCode.Joystick1Button18) || Input.GetKey(KeyCode.Joystick1Button19) || Input.GetAxis("DpadX") > 0.5f || Input.GetAxis("DpadX") < -0.5f || Input.GetAxis("DpadY") > 0.5f || Input.GetAxis("DpadY") < -0.5f)
		{
			this.Type = InputDeviceType.Gamepad;
		}
		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && (Input.GetAxis("Vertical") == 1f || Input.GetAxis("Vertical") == -1f || Input.GetAxis("Horizontal") == 1f || Input.GetAxis("Horizontal") == -1f))
		{
			this.Type = InputDeviceType.Gamepad;
		}
		if (this.Type != type)
		{
			PromptSwapScript[] array = Resources.FindObjectsOfTypeAll<PromptSwapScript>();
			foreach (PromptSwapScript promptSwapScript in array)
			{
				promptSwapScript.UpdateSpriteType(this.Type);
			}
		}
		this.Horizontal = Input.GetAxis("Horizontal");
		this.Vertical = Input.GetAxis("Vertical");
	}

	// Token: 0x040022F1 RID: 8945
	public InputDeviceType Type = InputDeviceType.Gamepad;

	// Token: 0x040022F2 RID: 8946
	public Vector3 MousePrevious;

	// Token: 0x040022F3 RID: 8947
	public Vector3 MouseDelta;

	// Token: 0x040022F4 RID: 8948
	public float Horizontal;

	// Token: 0x040022F5 RID: 8949
	public float Vertical;
}
