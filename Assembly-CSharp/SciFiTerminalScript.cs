using System;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public class SciFiTerminalScript : MonoBehaviour
{
	// Token: 0x06001FCB RID: 8139 RVA: 0x001460D0 File Offset: 0x001444D0
	private void Start()
	{
		if (this.Student.StudentID != 65)
		{
			base.enabled = false;
		}
		else
		{
			this.RobotArms = this.Student.StudentManager.RobotArms;
		}
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x00146108 File Offset: 0x00144508
	private void Update()
	{
		if (this.RobotArms != null)
		{
			if ((double)Vector3.Distance(this.RobotArms.TerminalTarget.position, base.transform.position) < 0.3 || (double)Vector3.Distance(this.RobotArms.TerminalTarget.position, this.OtherFinger.position) < 0.3)
			{
				if (!this.Updated)
				{
					this.Updated = true;
					if (!this.RobotArms.On[0])
					{
						this.RobotArms.ActivateArms();
					}
					else
					{
						this.RobotArms.ToggleWork();
					}
				}
			}
			else
			{
				this.Updated = false;
			}
		}
	}

	// Token: 0x04002BB3 RID: 11187
	public StudentScript Student;

	// Token: 0x04002BB4 RID: 11188
	public RobotArmScript RobotArms;

	// Token: 0x04002BB5 RID: 11189
	public Transform OtherFinger;

	// Token: 0x04002BB6 RID: 11190
	public bool Updated;
}
