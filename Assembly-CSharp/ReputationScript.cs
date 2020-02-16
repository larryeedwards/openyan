using System;
using UnityEngine;

// Token: 0x020004BE RID: 1214
public class ReputationScript : MonoBehaviour
{
	// Token: 0x06001F1A RID: 7962 RVA: 0x0013D82D File Offset: 0x0013BC2D
	private void Start()
	{
		if (MissionModeGlobals.MissionMode)
		{
			this.MissionMode = true;
		}
		this.Reputation = PlayerGlobals.Reputation;
		this.Bully();
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x0013D854 File Offset: 0x0013BC54
	private void Update()
	{
		if (this.Phase == 1)
		{
			if (this.Clock.PresentTime / 60f > 8.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.Clock.PresentTime / 60f > 13.5f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 3 && this.Clock.PresentTime / 60f > 18f)
		{
			this.Phase++;
		}
		if (this.PendingRep < 0f)
		{
			this.StudentManager.TutorialWindow.ShowRepMessage = true;
		}
		if (this.CheckedRep < this.Phase && !this.StudentManager.Yandere.Struggling && !this.StudentManager.Yandere.DelinquentFighting && !this.StudentManager.Yandere.Pickpocketing && !this.StudentManager.Yandere.Noticed && !this.ArmDetector.SummonDemon)
		{
			this.UpdateRep();
			if (this.Reputation <= -100f)
			{
				this.Portal.EndDay();
			}
		}
		if (!this.MissionMode)
		{
			this.CurrentRepMarker.localPosition = new Vector3(Mathf.Lerp(this.CurrentRepMarker.localPosition.x, -830f + this.Reputation * 1.5f, Time.deltaTime * 10f), this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, this.CurrentRepMarker.transform.localPosition.x + this.PendingRep * 1.5f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		else
		{
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, -980f + this.PendingRep * -3f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x < -980f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-980f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x < -980f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-980f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x > -680f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-680f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x > -680f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-680f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (!this.MissionMode)
		{
			if (this.PendingRep > 0f)
			{
				this.PendingRepLabel.text = "+" + this.PendingRep.ToString();
			}
			else if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = this.PendingRep.ToString();
			}
			else
			{
				this.PendingRepLabel.text = string.Empty;
			}
		}
		else if (this.PendingRep < 0f)
		{
			this.PendingRepLabel.text = (-this.PendingRep).ToString();
		}
		else
		{
			this.PendingRepLabel.text = string.Empty;
		}
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x0013DD69 File Offset: 0x0013C169
	private void Bully()
	{
		this.FlowerVase.SetActive(false);
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x0013DD78 File Offset: 0x0013C178
	public void UpdateRep()
	{
		this.Reputation += this.PendingRep;
		this.PendingRep = 0f;
		this.CheckedRep++;
		if (ClubGlobals.Club == ClubType.Delinquent && this.Reputation > -33.33333f)
		{
			this.Reputation = -33.33333f;
		}
		this.StudentManager.WipePendingRep();
	}

	// Token: 0x04002993 RID: 10643
	public StudentManagerScript StudentManager;

	// Token: 0x04002994 RID: 10644
	public ArmDetectorScript ArmDetector;

	// Token: 0x04002995 RID: 10645
	public PortalScript Portal;

	// Token: 0x04002996 RID: 10646
	public Transform CurrentRepMarker;

	// Token: 0x04002997 RID: 10647
	public Transform PendingRepMarker;

	// Token: 0x04002998 RID: 10648
	public UILabel PendingRepLabel;

	// Token: 0x04002999 RID: 10649
	public ClockScript Clock;

	// Token: 0x0400299A RID: 10650
	public float Reputation;

	// Token: 0x0400299B RID: 10651
	public float PendingRep;

	// Token: 0x0400299C RID: 10652
	public int CheckedRep = 1;

	// Token: 0x0400299D RID: 10653
	public int Phase;

	// Token: 0x0400299E RID: 10654
	public bool MissionMode;

	// Token: 0x0400299F RID: 10655
	public GameObject FlowerVase;

	// Token: 0x040029A0 RID: 10656
	public GameObject Grafitti;
}
