using System;
using UnityEngine;

// Token: 0x02000550 RID: 1360
public class TitleSaveDataScript : MonoBehaviour
{
	// Token: 0x0600219A RID: 8602 RVA: 0x00197324 File Offset: 0x00195724
	public void Start()
	{
		if (PlayerPrefs.GetInt("ProfileCreated_" + this.ID) == 1)
		{
			GameGlobals.Profile = this.ID;
			this.EmptyFile.SetActive(false);
			this.Data.SetActive(true);
			this.Kills.text = "Kills: " + PlayerGlobals.Kills;
			this.Mood.text = "Mood: " + Mathf.RoundToInt(SchoolGlobals.SchoolAtmosphere * 100f);
			this.Alerts.text = "Alerts: " + PlayerGlobals.Alerts;
			this.Week.text = "Week: " + 1;
			this.Day.text = "Day: " + DateGlobals.Weekday;
			this.Rival.text = "Rival: Osana";
			this.Rep.text = "Rep: " + PlayerGlobals.Reputation;
			this.Club.text = "Club: " + ClubGlobals.Club;
			this.Friends.text = "Friends: " + PlayerGlobals.Friends;
			if (PlayerGlobals.Kills == 0)
			{
				this.Blood.mainTexture = null;
			}
			else if (PlayerGlobals.Kills > 0)
			{
				this.Blood.mainTexture = this.Bloods[1];
			}
			else if (PlayerGlobals.Kills > 5)
			{
				this.Blood.mainTexture = this.Bloods[2];
			}
			else if (PlayerGlobals.Kills > 10)
			{
				this.Blood.mainTexture = this.Bloods[3];
			}
			else if (PlayerGlobals.Kills > 15)
			{
				this.Blood.mainTexture = this.Bloods[4];
			}
			else if (PlayerGlobals.Kills > 20)
			{
				this.Blood.mainTexture = this.Bloods[5];
			}
		}
		else
		{
			this.EmptyFile.SetActive(true);
			this.Data.SetActive(false);
			this.Blood.enabled = false;
		}
	}

	// Token: 0x0400366E RID: 13934
	public GameObject EmptyFile;

	// Token: 0x0400366F RID: 13935
	public GameObject Data;

	// Token: 0x04003670 RID: 13936
	public Texture[] Bloods;

	// Token: 0x04003671 RID: 13937
	public UITexture Blood;

	// Token: 0x04003672 RID: 13938
	public UILabel Kills;

	// Token: 0x04003673 RID: 13939
	public UILabel Mood;

	// Token: 0x04003674 RID: 13940
	public UILabel Alerts;

	// Token: 0x04003675 RID: 13941
	public UILabel Week;

	// Token: 0x04003676 RID: 13942
	public UILabel Day;

	// Token: 0x04003677 RID: 13943
	public UILabel Rival;

	// Token: 0x04003678 RID: 13944
	public UILabel Rep;

	// Token: 0x04003679 RID: 13945
	public UILabel Club;

	// Token: 0x0400367A RID: 13946
	public UILabel Friends;

	// Token: 0x0400367B RID: 13947
	public int ID;
}
