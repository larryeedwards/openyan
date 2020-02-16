using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class StreetCivilianScript : MonoBehaviour
{
	// Token: 0x0600205C RID: 8284 RVA: 0x00151ADC File Offset: 0x0014FEDC
	private void Start()
	{
		this.Pathfinding.target = this.Destinations[0];
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x00151AF4 File Offset: 0x0014FEF4
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Destinations[this.ID].position) < 0.55f)
		{
			this.MoveTowardsTarget(this.Destinations[this.ID].position);
			this.MyAnimation.CrossFade("f02_idle_00");
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.Timer += Time.deltaTime;
			if (this.Timer > 13.5f)
			{
				this.MyAnimation.CrossFade("f02_newWalk_00");
				this.ID++;
				if (this.ID == this.Destinations.Length)
				{
					this.ID = 0;
				}
				this.Pathfinding.target = this.Destinations[this.ID];
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x00151C04 File Offset: 0x00150004
	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		float sqrMagnitude = a.sqrMagnitude;
		if (sqrMagnitude > 1E-06f)
		{
			this.MyController.Move(a * (Time.deltaTime * 1f / Time.timeScale));
		}
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Destinations[this.ID].rotation, 10f * Time.deltaTime);
	}

	// Token: 0x04002D56 RID: 11606
	public CharacterController MyController;

	// Token: 0x04002D57 RID: 11607
	public Animation MyAnimation;

	// Token: 0x04002D58 RID: 11608
	public AIPath Pathfinding;

	// Token: 0x04002D59 RID: 11609
	public Transform[] Destinations;

	// Token: 0x04002D5A RID: 11610
	public float Timer;

	// Token: 0x04002D5B RID: 11611
	public int ID;
}
