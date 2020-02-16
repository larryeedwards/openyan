using System;
using UnityEngine;

// Token: 0x0200051D RID: 1309
public class StalkerYandereScript : MonoBehaviour
{
	// Token: 0x06002041 RID: 8257 RVA: 0x0014FA30 File Offset: 0x0014DE30
	private void Update()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (Input.GetKeyDown("=") && Time.timeScale < 10f)
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown("-") && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			if (this.Jukebox != null)
			{
				if (this.Jukebox.isPlaying)
				{
					this.Jukebox.Stop();
				}
				else
				{
					this.Jukebox.Play();
				}
			}
		}
		if (this.CanMove)
		{
			if (this.CameraTarget != null)
			{
				this.CameraTarget.localPosition = new Vector3(0f, 1f + (this.RPGCamera.distanceMax - this.RPGCamera.distance) * 0.2f, 0f);
			}
			this.UpdateMovement();
		}
		else if (this.CameraTarget != null && this.Climbing)
		{
			if (this.ClimbPhase == 1)
			{
				if (this.MyAnimation["f02_climbTrellis_00"].time < this.MyAnimation["f02_climbTrellis_00"].length - 1f)
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, this.Hips.position + new Vector3(0f, 0.103729f, 0.003539f), Time.deltaTime);
				}
				else
				{
					this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(-9.5f, 5f, -2.5f), Time.deltaTime);
				}
				this.MoveTowardsTarget(this.TrellisClimbSpot.position);
				this.SpinTowardsTarget(this.TrellisClimbSpot.rotation);
				if (this.MyAnimation["f02_climbTrellis_00"].time > 7.5f)
				{
					this.RPGCamera.transform.position = this.EntryPOV.position;
					this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
					this.RPGCamera.enabled = false;
					RenderSettings.ambientIntensity = 8f;
					this.ClimbPhase++;
				}
			}
			else
			{
				this.RPGCamera.transform.position = this.EntryPOV.position;
				this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
				if (this.MyAnimation["f02_climbTrellis_00"].time > 11f)
				{
					base.transform.position = Vector3.MoveTowards(base.transform.position, this.TrellisClimbSpot.position + new Vector3(0.4f, 0f, 0f), Time.deltaTime * 0.5f);
				}
			}
			if (this.MyAnimation["f02_climbTrellis_00"].time > this.MyAnimation["f02_climbTrellis_00"].length)
			{
				this.MyAnimation.Play(this.IdleAnim);
				base.transform.position = new Vector3(-9.1f, 4f, -2.5f);
				this.CameraTarget.position = base.transform.position + new Vector3(0f, 1f, 0f);
				this.RPGCamera.enabled = true;
				this.Climbing = false;
				this.CanMove = true;
			}
		}
		if (this.Street && base.transform.position.x < -16f)
		{
			base.transform.position = new Vector3(-16f, 0f, base.transform.position.z);
		}
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x0014FE7C File Offset: 0x0014E27C
	private void UpdateMovement()
	{
		if (!OptionGlobals.ToggleRun)
		{
			this.Running = false;
			if (Input.GetButton("LB"))
			{
				this.Running = true;
			}
		}
		else if (Input.GetButtonDown("LB"))
		{
			this.Running = !this.Running;
		}
		this.MyController.Move(Physics.gravity * Time.deltaTime);
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 a = this.MainCamera.transform.TransformDirection(Vector3.forward);
		a.y = 0f;
		a = a.normalized;
		Vector3 a2 = new Vector3(a.z, 0f, -a.x);
		Vector3 vector = axis2 * a2 + axis * a;
		Quaternion b = Quaternion.identity;
		if (vector != Vector3.zero)
		{
			b = Quaternion.LookRotation(vector);
		}
		if (vector != Vector3.zero)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
		}
		else
		{
			b = new Quaternion(0f, 0f, 0f, 0f);
		}
		if (!this.Street)
		{
			if (this.Stance.Current == StanceType.Standing)
			{
				if (Input.GetButtonDown("RS"))
				{
					this.Stance.Current = StanceType.Crouching;
				}
			}
			else if (Input.GetButtonDown("RS"))
			{
				this.Stance.Current = StanceType.Standing;
			}
		}
		if (axis != 0f || axis2 != 0f)
		{
			if (this.Running)
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchRunAnim);
					this.MyController.Move(base.transform.forward * this.CrouchRunSpeed * Time.deltaTime);
				}
				else
				{
					this.MyAnimation.CrossFade(this.RunAnim);
					this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
				}
			}
			else if (this.Stance.Current == StanceType.Crouching)
			{
				this.MyAnimation.CrossFade(this.CrouchWalkAnim);
				this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
			}
			else
			{
				this.MyAnimation.CrossFade(this.WalkAnim);
				this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
			}
		}
		else if (this.Stance.Current == StanceType.Crouching)
		{
			this.MyAnimation.CrossFade(this.CrouchIdleAnim);
		}
		else
		{
			this.MyAnimation.CrossFade(this.IdleAnim);
		}
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x001501AC File Offset: 0x0014E5AC
	private void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x001501E8 File Offset: 0x0014E5E8
	private void SpinTowardsTarget(Quaternion target)
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target, Time.deltaTime * 10f);
	}

	// Token: 0x04002D0F RID: 11535
	public CharacterController MyController;

	// Token: 0x04002D10 RID: 11536
	public Transform TrellisClimbSpot;

	// Token: 0x04002D11 RID: 11537
	public Transform CameraTarget;

	// Token: 0x04002D12 RID: 11538
	public Transform EntryPOV;

	// Token: 0x04002D13 RID: 11539
	public Transform Hips;

	// Token: 0x04002D14 RID: 11540
	public RPG_Camera RPGCamera;

	// Token: 0x04002D15 RID: 11541
	public Animation MyAnimation;

	// Token: 0x04002D16 RID: 11542
	public AudioSource Jukebox;

	// Token: 0x04002D17 RID: 11543
	public Camera MainCamera;

	// Token: 0x04002D18 RID: 11544
	public bool Climbing;

	// Token: 0x04002D19 RID: 11545
	public bool Running;

	// Token: 0x04002D1A RID: 11546
	public bool CanMove;

	// Token: 0x04002D1B RID: 11547
	public bool Street;

	// Token: 0x04002D1C RID: 11548
	public Stance Stance = new Stance(StanceType.Standing);

	// Token: 0x04002D1D RID: 11549
	public string IdleAnim;

	// Token: 0x04002D1E RID: 11550
	public string WalkAnim;

	// Token: 0x04002D1F RID: 11551
	public string RunAnim;

	// Token: 0x04002D20 RID: 11552
	public string CrouchIdleAnim;

	// Token: 0x04002D21 RID: 11553
	public string CrouchWalkAnim;

	// Token: 0x04002D22 RID: 11554
	public string CrouchRunAnim;

	// Token: 0x04002D23 RID: 11555
	public float WalkSpeed;

	// Token: 0x04002D24 RID: 11556
	public float RunSpeed;

	// Token: 0x04002D25 RID: 11557
	public float CrouchWalkSpeed;

	// Token: 0x04002D26 RID: 11558
	public float CrouchRunSpeed;

	// Token: 0x04002D27 RID: 11559
	public int ClimbPhase;

	// Token: 0x04002D28 RID: 11560
	public int Frame;
}
