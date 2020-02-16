﻿using System;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class ConvoManagerScript : MonoBehaviour
{
	// Token: 0x06001828 RID: 6184 RVA: 0x000C8CCC File Offset: 0x000C70CC
	public void CheckMe(int StudentID)
	{
		if (StudentID == 2)
		{
			if (this.SM.Students[3].Routine && (double)Vector3.Distance(this.SM.Students[2].transform.position, this.SM.Students[3].transform.position) < 1.4)
			{
				this.SM.Students[2].Alone = false;
			}
			else
			{
				this.SM.Students[2].Alone = true;
			}
		}
		else if (StudentID == 3)
		{
			if (this.SM.Students[2].Routine && (double)Vector3.Distance(this.SM.Students[3].transform.position, this.SM.Students[2].transform.position) < 1.4)
			{
				this.SM.Students[3].Alone = false;
			}
			else
			{
				this.SM.Students[3].Alone = true;
			}
		}
		else if (StudentID == 11)
		{
			if (this.SM.Students[10] != null)
			{
				if (this.SM.Students[10].Routine && Vector3.Distance(this.SM.Students[11].transform.position, this.SM.Students[10].transform.position) < 1.1f)
				{
					this.SM.Students[11].Alone = false;
				}
				else
				{
					this.SM.Students[11].Alone = true;
				}
			}
			else
			{
				this.SM.Students[11].Alone = true;
			}
		}
		else if (StudentID > 20 && StudentID < 26)
		{
			this.NearbyStudents = 0;
			this.ID = 21;
			while (this.ID < 26)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
				if (this.ID == StudentID)
				{
					this.SM.Students[StudentID].Alone = true;
				}
			}
		}
		else if (StudentID > 25 && StudentID < 31)
		{
			this.ID = 26;
			while (this.ID < 31)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 35 && StudentID < 41)
		{
			this.NearbyStudents = 0;
			this.ID = 36;
			while (this.ID < 41)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
				if (this.ID == StudentID)
				{
					this.SM.Students[StudentID].Alone = true;
				}
			}
		}
		else if (StudentID > 45 && StudentID < 51)
		{
			this.ID = 46;
			while (this.ID < 51)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 30 && StudentID < 36)
		{
			this.ID = 31;
			while (this.ID < 36)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID == 11)
		{
			if (this.SM.Students[6] != null)
			{
				if (this.SM.Students[6].Routine && (double)Vector3.Distance(this.SM.Students[11].transform.position, this.SM.Students[6].transform.position) < 1.4)
				{
					this.SM.Students[11].Alone = false;
				}
				else
				{
					this.SM.Students[11].Alone = true;
				}
			}
			else
			{
				this.SM.Students[11].Alone = true;
			}
		}
		else if (StudentID == 6)
		{
			if (this.SM.Students[11].Routine && (double)Vector3.Distance(this.SM.Students[6].transform.position, this.SM.Students[11].transform.position) < 1.4)
			{
				this.SM.Students[6].Alone = false;
			}
			else
			{
				this.SM.Students[6].Alone = true;
			}
		}
		else if (StudentID > 55 && StudentID < 61)
		{
			this.ID = 56;
			while (this.ID < 61)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.66666f)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 60 && StudentID < 66)
		{
			this.ID = 61;
			while (this.ID < 66)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.66666f)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 65 && StudentID < 71)
		{
			this.ID = 66;
			while (this.ID < 71)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.66666f)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 75 && StudentID < 81)
		{
			this.ID = 76;
			while (this.ID < 81)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if ((double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].TrueAlone = false;
							if (this.SM.Students[this.ID].Routine)
							{
								this.SM.Students[StudentID].Alone = false;
								break;
							}
							this.SM.Students[StudentID].Alone = true;
						}
						else
						{
							this.SM.Students[StudentID].TrueAlone = true;
							this.SM.Students[StudentID].Alone = true;
						}
					}
					else
					{
						this.SM.Students[StudentID].TrueAlone = true;
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
		else if (StudentID > 80 && StudentID < 86)
		{
			this.ID = 81;
			while (this.ID < 86)
			{
				if (this.ID != StudentID)
				{
					if (this.SM.Students[this.ID] != null)
					{
						if (this.SM.Students[this.ID].Routine && (double)Vector3.Distance(this.SM.Students[this.ID].transform.position, this.SM.Students[StudentID].transform.position) < 2.5)
						{
							this.SM.Students[StudentID].Alone = false;
							break;
						}
						this.SM.Students[StudentID].Alone = true;
					}
					else
					{
						this.SM.Students[StudentID].Alone = true;
					}
				}
				this.ID++;
			}
		}
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x000C9B14 File Offset: 0x000C7F14
	public void MartialArtsCheck()
	{
		this.CheckTimer += Time.deltaTime;
		if ((this.CheckTimer > 1f || this.Confirmed) && this.SM.Students[47] != null && this.SM.Students[49] != null && this.SM.Students[47].Routine && this.SM.Students[49].Routine && this.SM.Students[47].DistanceToDestination < 0.1f && this.SM.Students[49].DistanceToDestination < 0.1f)
		{
			this.Confirmed = true;
			this.CombatAnimID++;
			if (this.CombatAnimID > 2)
			{
				this.CombatAnimID = 1;
			}
			this.SM.Students[47].ClubAnim = this.MaleCombatAnims[this.CombatAnimID];
			this.SM.Students[49].ClubAnim = this.FemaleCombatAnims[this.CombatAnimID];
			this.SM.Students[47].GetNewAnimation = false;
			this.SM.Students[49].GetNewAnimation = false;
			this.Cycles++;
			if (this.Cycles == 5)
			{
				this.SM.UpdateMartialArts();
				this.Cycles = 0;
			}
		}
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000C9CA8 File Offset: 0x000C80A8
	public void LateUpdate()
	{
		this.CheckTimer = Mathf.MoveTowards(this.CheckTimer, 0f, Time.deltaTime);
		if (this.Confirmed)
		{
			if (this.SM.Students[47].Routine && this.SM.Students[49].Routine)
			{
				if (this.SM.Students[47].DistanceToPlayer < 1.5f || this.SM.Students[49].DistanceToPlayer < 1.5f || this.SM.Students[47].Talking || this.SM.Students[49].Talking || this.SM.Students[47].Distracted || this.SM.Students[49].Distracted || this.SM.Students[47].TurnOffRadio || this.SM.Students[49].TurnOffRadio)
				{
					if (this.SM.Students[47].DistanceToPlayer < 1.5f || this.SM.Students[49].DistanceToPlayer < 1.5f)
					{
						this.SM.Students[47].Subtitle.UpdateLabel(SubtitleType.IntrusionReaction, 2, 5f);
					}
					this.SM.Students[47].ClubAnim = "idle_20";
					this.SM.Students[49].ClubAnim = "f02_idle_20";
					this.Confirmed = false;
				}
			}
			else
			{
				this.SM.Students[47].ClubAnim = "idle_20";
				this.SM.Students[49].ClubAnim = "f02_idle_20";
				this.Confirmed = false;
			}
		}
	}

	// Token: 0x04001931 RID: 6449
	public StudentManagerScript SM;

	// Token: 0x04001932 RID: 6450
	public int NearbyStudents;

	// Token: 0x04001933 RID: 6451
	public int ID;

	// Token: 0x04001934 RID: 6452
	public string[] FemaleCombatAnims;

	// Token: 0x04001935 RID: 6453
	public string[] MaleCombatAnims;

	// Token: 0x04001936 RID: 6454
	public int CombatAnimID;

	// Token: 0x04001937 RID: 6455
	public float CheckTimer;

	// Token: 0x04001938 RID: 6456
	public bool Confirmed;

	// Token: 0x04001939 RID: 6457
	public int Cycles;
}
