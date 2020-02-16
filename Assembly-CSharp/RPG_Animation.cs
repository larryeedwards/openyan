using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class RPG_Animation : MonoBehaviour
{
	// Token: 0x060016B7 RID: 5815 RVA: 0x000AE36B File Offset: 0x000AC76B
	private void Awake()
	{
		RPG_Animation.instance = this;
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000AE373 File Offset: 0x000AC773
	private void Update()
	{
		this.SetCurrentState();
		this.StartAnimation();
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000AE384 File Offset: 0x000AC784
	public void SetCurrentMoveDir(Vector3 playerDir)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (playerDir.z > 0f)
		{
			flag = true;
		}
		if (playerDir.z < 0f)
		{
			flag2 = true;
		}
		if (playerDir.x < 0f)
		{
			flag3 = true;
		}
		if (playerDir.x > 0f)
		{
			flag4 = true;
		}
		if (flag)
		{
			if (flag3)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeForwardLeft;
			}
			else if (flag4)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeForwardRight;
			}
			else
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.Forward;
			}
		}
		else if (flag2)
		{
			if (flag3)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeBackLeft;
			}
			else if (flag4)
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeBackRight;
			}
			else
			{
				this.currentMoveDir = RPG_Animation.CharacterMoveDirection.Backward;
			}
		}
		else if (flag3)
		{
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeLeft;
		}
		else if (flag4)
		{
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.StrafeRight;
		}
		else
		{
			this.currentMoveDir = RPG_Animation.CharacterMoveDirection.None;
		}
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000AE47C File Offset: 0x000AC87C
	public void SetCurrentState()
	{
		if (RPG_Controller.instance.characterController.isGrounded)
		{
			switch (this.currentMoveDir)
			{
			case RPG_Animation.CharacterMoveDirection.None:
				this.currentState = RPG_Animation.CharacterState.Idle;
				break;
			case RPG_Animation.CharacterMoveDirection.Forward:
				this.currentState = RPG_Animation.CharacterState.Walk;
				break;
			case RPG_Animation.CharacterMoveDirection.Backward:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeLeft:
				this.currentState = RPG_Animation.CharacterState.StrafeLeft;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeRight:
				this.currentState = RPG_Animation.CharacterState.StrafeRight;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeForwardLeft:
				this.currentState = RPG_Animation.CharacterState.Walk;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeForwardRight:
				this.currentState = RPG_Animation.CharacterState.Walk;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeBackLeft:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				break;
			case RPG_Animation.CharacterMoveDirection.StrafeBackRight:
				this.currentState = RPG_Animation.CharacterState.WalkBack;
				break;
			}
		}
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x000AE540 File Offset: 0x000AC940
	public void StartAnimation()
	{
		switch (this.currentState)
		{
		case RPG_Animation.CharacterState.Idle:
			this.Idle();
			break;
		case RPG_Animation.CharacterState.Walk:
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeForwardLeft)
			{
				this.StrafeForwardLeft();
			}
			else if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeForwardRight)
			{
				this.StrafeForwardRight();
			}
			else
			{
				this.Walk();
			}
			break;
		case RPG_Animation.CharacterState.WalkBack:
			if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeBackLeft)
			{
				this.StrafeBackLeft();
			}
			else if (this.currentMoveDir == RPG_Animation.CharacterMoveDirection.StrafeBackRight)
			{
				this.StrafeBackRight();
			}
			else
			{
				this.WalkBack();
			}
			break;
		case RPG_Animation.CharacterState.StrafeLeft:
			this.StrafeLeft();
			break;
		case RPG_Animation.CharacterState.StrafeRight:
			this.StrafeRight();
			break;
		}
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x000AE606 File Offset: 0x000ACA06
	private void Idle()
	{
		base.GetComponent<Animation>().CrossFade("idle");
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000AE618 File Offset: 0x000ACA18
	private void Walk()
	{
		base.GetComponent<Animation>().CrossFade("walk");
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x000AE62A File Offset: 0x000ACA2A
	private void StrafeForwardLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardleft");
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000AE63C File Offset: 0x000ACA3C
	private void StrafeForwardRight()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardright");
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000AE64E File Offset: 0x000ACA4E
	private void WalkBack()
	{
		base.GetComponent<Animation>().CrossFade("walkback");
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x000AE660 File Offset: 0x000ACA60
	private void StrafeBackLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafebackleft");
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x000AE672 File Offset: 0x000ACA72
	private void StrafeBackRight()
	{
		base.GetComponent<Animation>().CrossFade("strafebackright");
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000AE684 File Offset: 0x000ACA84
	private void StrafeLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeleft");
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000AE696 File Offset: 0x000ACA96
	private void StrafeRight()
	{
		base.GetComponent<Animation>().CrossFade("straferight");
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000AE6A8 File Offset: 0x000ACAA8
	public void Jump()
	{
		this.currentState = RPG_Animation.CharacterState.Jump;
		if (base.GetComponent<Animation>().IsPlaying("jump"))
		{
			base.GetComponent<Animation>().Stop("jump");
		}
		base.GetComponent<Animation>().CrossFade("jump");
	}

	// Token: 0x04001458 RID: 5208
	public static RPG_Animation instance;

	// Token: 0x04001459 RID: 5209
	public RPG_Animation.CharacterMoveDirection currentMoveDir;

	// Token: 0x0400145A RID: 5210
	public RPG_Animation.CharacterState currentState;

	// Token: 0x02000312 RID: 786
	public enum CharacterMoveDirection
	{
		// Token: 0x0400145C RID: 5212
		None,
		// Token: 0x0400145D RID: 5213
		Forward,
		// Token: 0x0400145E RID: 5214
		Backward,
		// Token: 0x0400145F RID: 5215
		StrafeLeft,
		// Token: 0x04001460 RID: 5216
		StrafeRight,
		// Token: 0x04001461 RID: 5217
		StrafeForwardLeft,
		// Token: 0x04001462 RID: 5218
		StrafeForwardRight,
		// Token: 0x04001463 RID: 5219
		StrafeBackLeft,
		// Token: 0x04001464 RID: 5220
		StrafeBackRight
	}

	// Token: 0x02000313 RID: 787
	public enum CharacterState
	{
		// Token: 0x04001466 RID: 5222
		Idle,
		// Token: 0x04001467 RID: 5223
		Walk,
		// Token: 0x04001468 RID: 5224
		WalkBack,
		// Token: 0x04001469 RID: 5225
		StrafeLeft,
		// Token: 0x0400146A RID: 5226
		StrafeRight,
		// Token: 0x0400146B RID: 5227
		Jump
	}
}
