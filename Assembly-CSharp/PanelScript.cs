using System;
using UnityEngine;

// Token: 0x02000484 RID: 1156
public class PanelScript : MonoBehaviour
{
	// Token: 0x06001E26 RID: 7718 RVA: 0x001233DC File Offset: 0x001217DC
	private void Update()
	{
		if (this.Player.position.z > this.StairsZ || this.Player.position.z < -this.StairsZ)
		{
			this.Floor = "Stairs";
		}
		else if (this.Player.position.y < this.Floor1Height)
		{
			this.Floor = "First Floor";
		}
		else if (this.Player.position.y > this.Floor1Height && this.Player.position.y < this.Floor2Height)
		{
			this.Floor = "Second Floor";
		}
		else if (this.Player.position.y > this.Floor2Height && this.Player.position.y < this.Floor3Height)
		{
			this.Floor = "Third Floor";
		}
		else
		{
			this.Floor = "Rooftop";
		}
		if (this.Player.position.z < this.PracticeBuildingZ)
		{
			this.BuildingLabel.text = "Practice Building, " + this.Floor;
		}
		else
		{
			this.BuildingLabel.text = "Classroom Building, " + this.Floor;
		}
		this.DoorBox.Show = false;
	}

	// Token: 0x04002679 RID: 9849
	public UILabel BuildingLabel;

	// Token: 0x0400267A RID: 9850
	public DoorBoxScript DoorBox;

	// Token: 0x0400267B RID: 9851
	public Transform Player;

	// Token: 0x0400267C RID: 9852
	public string Floor = string.Empty;

	// Token: 0x0400267D RID: 9853
	public float PracticeBuildingZ;

	// Token: 0x0400267E RID: 9854
	public float StairsZ;

	// Token: 0x0400267F RID: 9855
	public float Floor1Height;

	// Token: 0x04002680 RID: 9856
	public float Floor2Height;

	// Token: 0x04002681 RID: 9857
	public float Floor3Height;
}
