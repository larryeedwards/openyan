using System;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class AnswerSheetScript : MonoBehaviour
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x000B189B File Offset: 0x000AFC9B
	private void Start()
	{
		this.OriginalMesh = this.MyMesh.mesh;
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000B18D0 File Offset: 0x000AFCD0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.MyMesh.mesh = null;
				this.Phase++;
			}
			else
			{
				SchemeGlobals.SetSchemeStage(5, 8);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.MyMesh.mesh = this.OriginalMesh;
				this.Phase++;
			}
		}
	}

	// Token: 0x0400160B RID: 5643
	public SchemesScript Schemes;

	// Token: 0x0400160C RID: 5644
	public DoorGapScript DoorGap;

	// Token: 0x0400160D RID: 5645
	public PromptScript Prompt;

	// Token: 0x0400160E RID: 5646
	public ClockScript Clock;

	// Token: 0x0400160F RID: 5647
	public Mesh OriginalMesh;

	// Token: 0x04001610 RID: 5648
	public MeshFilter MyMesh;

	// Token: 0x04001611 RID: 5649
	public int Phase = 1;
}
