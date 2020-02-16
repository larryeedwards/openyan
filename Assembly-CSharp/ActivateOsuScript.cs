using System;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class ActivateOsuScript : MonoBehaviour
{
	// Token: 0x06001E15 RID: 7701 RVA: 0x00122D45 File Offset: 0x00121145
	private void Start()
	{
		this.OsuScripts = this.Osu.GetComponents<OsuScript>();
		this.OriginalMouseRotation = this.Mouse.transform.eulerAngles;
		this.OriginalMousePosition = this.Mouse.transform.position;
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x00122D84 File Offset: 0x00121184
	private void Update()
	{
		if (this.Student == null)
		{
			this.Student = this.StudentManager.Students[this.PlayerID];
		}
		else if (!this.Osu.activeInHierarchy)
		{
			if (Vector3.Distance(base.transform.position, this.Student.transform.position) < 0.1f && this.Student.Routine && this.Student.CurrentDestination == this.Student.Destinations[this.Student.Phase] && this.Student.Actions[this.Student.Phase] == StudentActionType.Gaming)
			{
				this.ActivateOsu();
			}
		}
		else
		{
			this.Mouse.transform.eulerAngles = this.OriginalMouseRotation;
			if (!this.Student.Routine || this.Student.CurrentDestination != this.Student.Destinations[this.Student.Phase] || this.Student.Actions[this.Student.Phase] != StudentActionType.Gaming)
			{
				this.DeactivateOsu();
			}
		}
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x00122ED4 File Offset: 0x001212D4
	private void ActivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(true);
		this.Student.SmartPhone.SetActive(false);
		this.Music.SetActive(true);
		this.Mouse.parent = this.Student.RightHand;
		this.Mouse.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x00122F44 File Offset: 0x00121344
	private void DeactivateOsu()
	{
		this.Osu.transform.parent.gameObject.SetActive(false);
		this.Music.SetActive(false);
		foreach (OsuScript osuScript in this.OsuScripts)
		{
			osuScript.Timer = 0f;
		}
		this.Mouse.parent = base.transform.parent;
		this.Mouse.transform.position = this.OriginalMousePosition;
	}

	// Token: 0x04002657 RID: 9815
	public StudentManagerScript StudentManager;

	// Token: 0x04002658 RID: 9816
	public OsuScript[] OsuScripts;

	// Token: 0x04002659 RID: 9817
	public StudentScript Student;

	// Token: 0x0400265A RID: 9818
	public ClockScript Clock;

	// Token: 0x0400265B RID: 9819
	public GameObject Music;

	// Token: 0x0400265C RID: 9820
	public Transform Mouse;

	// Token: 0x0400265D RID: 9821
	public GameObject Osu;

	// Token: 0x0400265E RID: 9822
	public int PlayerID;

	// Token: 0x0400265F RID: 9823
	public Vector3 OriginalMousePosition;

	// Token: 0x04002660 RID: 9824
	public Vector3 OriginalMouseRotation;
}
