using System;
using UnityEngine;

// Token: 0x02000436 RID: 1078
public class InputManagerScript : MonoBehaviour
{
	// Token: 0x06001CFA RID: 7418 RVA: 0x0010BD8C File Offset: 0x0010A18C
	private void Update()
	{
		this.TappedUp = false;
		this.TappedDown = false;
		this.TappedRight = false;
		this.TappedLeft = false;
		if (Input.GetAxis("DpadY") > 0.5f)
		{
			this.TappedUp = !this.DPadUp;
			this.DPadUp = true;
		}
		else if (Input.GetAxis("DpadY") < -0.5f)
		{
			this.TappedDown = !this.DPadDown;
			this.DPadDown = true;
		}
		else
		{
			this.DPadUp = false;
			this.DPadDown = false;
		}
		if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
		{
			if (Input.GetAxis("Vertical") > 0.5f)
			{
				this.TappedUp = !this.StickUp;
				this.StickUp = !this.TappedDown;
			}
			else if (Input.GetAxis("Vertical") < -0.5f)
			{
				this.TappedDown = !this.StickDown;
				this.StickDown = !this.TappedUp;
			}
			else
			{
				this.StickUp = false;
				this.StickDown = false;
			}
		}
		if (Input.GetAxis("DpadX") > 0.5f)
		{
			this.TappedRight = !this.DPadRight;
			this.DPadRight = true;
		}
		else if (Input.GetAxis("DpadX") < -0.5f)
		{
			this.TappedLeft = !this.DPadLeft;
			this.DPadLeft = true;
		}
		else
		{
			this.DPadRight = false;
			this.DPadLeft = false;
		}
		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			if (Input.GetAxis("Horizontal") > 0.5f)
			{
				this.TappedRight = !this.StickRight;
				this.StickRight = true;
			}
			else if (Input.GetAxis("Horizontal") < -0.5f)
			{
				this.TappedLeft = !this.StickLeft;
				this.StickLeft = true;
			}
			else
			{
				this.StickRight = false;
				this.StickLeft = false;
			}
		}
		if (Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("DpadX") < 0.5f && Input.GetAxis("DpadX") > -0.5f)
		{
			this.TappedRight = false;
			this.TappedLeft = false;
		}
		if (Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("DpadY") < 0.5f && Input.GetAxis("DpadY") > -0.5f)
		{
			this.TappedUp = false;
			this.TappedDown = false;
		}
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.TappedUp = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			this.TappedDown = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.TappedLeft = true;
			this.NoStick();
		}
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.TappedRight = true;
			this.NoStick();
		}
	}

	// Token: 0x06001CFB RID: 7419 RVA: 0x0010C101 File Offset: 0x0010A501
	private void NoStick()
	{
		this.StickUp = false;
		this.StickDown = false;
		this.StickLeft = false;
		this.StickRight = false;
	}

	// Token: 0x040022F6 RID: 8950
	public bool TappedUp;

	// Token: 0x040022F7 RID: 8951
	public bool TappedDown;

	// Token: 0x040022F8 RID: 8952
	public bool TappedRight;

	// Token: 0x040022F9 RID: 8953
	public bool TappedLeft;

	// Token: 0x040022FA RID: 8954
	public bool DPadUp;

	// Token: 0x040022FB RID: 8955
	public bool StickUp;

	// Token: 0x040022FC RID: 8956
	public bool DPadDown;

	// Token: 0x040022FD RID: 8957
	public bool StickDown;

	// Token: 0x040022FE RID: 8958
	public bool DPadRight;

	// Token: 0x040022FF RID: 8959
	public bool StickRight;

	// Token: 0x04002300 RID: 8960
	public bool DPadLeft;

	// Token: 0x04002301 RID: 8961
	public bool StickLeft;
}
