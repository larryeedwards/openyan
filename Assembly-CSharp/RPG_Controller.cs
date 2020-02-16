using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class RPG_Controller : MonoBehaviour
{
	// Token: 0x060016D8 RID: 5848 RVA: 0x000AF641 File Offset: 0x000ADA41
	private void Awake()
	{
		RPG_Controller.instance = this;
		this.characterController = (base.GetComponent("CharacterController") as CharacterController);
		RPG_Camera.CameraSetup();
		this.MainCamera = Camera.main;
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000AF66F File Offset: 0x000ADA6F
	private void Update()
	{
		if (this.MainCamera == null)
		{
			return;
		}
		if (this.characterController == null)
		{
			Debug.Log("Error: No Character Controller component found! Please add one to the GameObject which has this script attached.");
			return;
		}
		this.GetInput();
		this.StartMotor();
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000AF6AC File Offset: 0x000ADAAC
	private void GetInput()
	{
		float d = 0f;
		float d2 = 0f;
		if (Input.GetButton("Horizontal Strafe"))
		{
			d = ((Input.GetAxis("Horizontal Strafe") >= 0f) ? ((Input.GetAxis("Horizontal Strafe") <= 0f) ? 0f : 1f) : -1f);
		}
		if (Input.GetButton("Vertical"))
		{
			d2 = ((Input.GetAxis("Vertical") >= 0f) ? ((Input.GetAxis("Vertical") <= 0f) ? 0f : 1f) : -1f);
		}
		if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			d2 = 1f;
		}
		this.playerDir = d * Vector3.right + d2 * Vector3.forward;
		if (RPG_Animation.instance != null)
		{
			RPG_Animation.instance.SetCurrentMoveDir(this.playerDir);
		}
		if (this.characterController.isGrounded)
		{
			this.playerDirWorld = base.transform.TransformDirection(this.playerDir);
			if (Mathf.Abs(this.playerDir.x) + Mathf.Abs(this.playerDir.z) > 1f)
			{
				this.playerDirWorld.Normalize();
			}
			this.playerDirWorld *= this.walkSpeed;
			this.playerDirWorld.y = this.fallingThreshold;
			if (Input.GetButtonDown("Jump"))
			{
				this.playerDirWorld.y = this.jumpHeight;
				if (RPG_Animation.instance != null)
				{
					RPG_Animation.instance.Jump();
				}
			}
		}
		this.rotation.y = Input.GetAxis("Horizontal") * this.turnSpeed;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000AF8A0 File Offset: 0x000ADCA0
	private void StartMotor()
	{
		this.playerDirWorld.y = this.playerDirWorld.y - this.gravity * Time.deltaTime;
		this.characterController.Move(this.playerDirWorld * Time.deltaTime);
		base.transform.Rotate(this.rotation);
		if (!Input.GetMouseButton(0))
		{
			RPG_Camera.instance.RotateWithCharacter();
		}
	}

	// Token: 0x04001490 RID: 5264
	public static RPG_Controller instance;

	// Token: 0x04001491 RID: 5265
	public CharacterController characterController;

	// Token: 0x04001492 RID: 5266
	public float walkSpeed = 10f;

	// Token: 0x04001493 RID: 5267
	public float turnSpeed = 2.5f;

	// Token: 0x04001494 RID: 5268
	public float jumpHeight = 10f;

	// Token: 0x04001495 RID: 5269
	public float gravity = 20f;

	// Token: 0x04001496 RID: 5270
	public float fallingThreshold = -6f;

	// Token: 0x04001497 RID: 5271
	private Vector3 playerDir;

	// Token: 0x04001498 RID: 5272
	private Vector3 playerDirWorld;

	// Token: 0x04001499 RID: 5273
	private Vector3 rotation = Vector3.zero;

	// Token: 0x0400149A RID: 5274
	private Camera MainCamera;
}
