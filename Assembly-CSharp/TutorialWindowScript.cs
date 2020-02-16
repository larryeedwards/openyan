using System;
using UnityEngine;

// Token: 0x0200055F RID: 1375
public class TutorialWindowScript : MonoBehaviour
{
	// Token: 0x060021D0 RID: 8656 RVA: 0x00199A04 File Offset: 0x00197E04
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
		if (OptionGlobals.TutorialsOff)
		{
			base.enabled = false;
		}
		else
		{
			this.IgnoreClothing = TutorialGlobals.IgnoreClothing;
			this.IgnoreCouncil = TutorialGlobals.IgnoreCouncil;
			this.IgnoreTeacher = TutorialGlobals.IgnoreTeacher;
			this.IgnoreLocker = TutorialGlobals.IgnoreLocker;
			this.IgnorePolice = TutorialGlobals.IgnorePolice;
			this.IgnoreSanity = TutorialGlobals.IgnoreSanity;
			this.IgnoreSenpai = TutorialGlobals.IgnoreSenpai;
			this.IgnoreVision = TutorialGlobals.IgnoreVision;
			this.IgnoreWeapon = TutorialGlobals.IgnoreWeapon;
			this.IgnoreBlood = TutorialGlobals.IgnoreBlood;
			this.IgnoreClass = TutorialGlobals.IgnoreClass;
			this.IgnorePhoto = TutorialGlobals.IgnorePhoto;
			this.IgnoreClub = TutorialGlobals.IgnoreClub;
			this.IgnoreInfo = TutorialGlobals.IgnoreInfo;
			this.IgnorePool = TutorialGlobals.IgnorePool;
			this.IgnoreRep = TutorialGlobals.IgnoreRep;
		}
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x00199AF8 File Offset: 0x00197EF8
	private void Update()
	{
		if (this.Show)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1.2925f, 1.2925f, 1.2925f), Time.unscaledDeltaTime * 10f);
			if (base.transform.localScale.x > 1f)
			{
				if (Input.GetButtonDown("B"))
				{
					OptionGlobals.TutorialsOff = true;
					this.TitleLabel.text = "Tutorials Disabled";
					this.TutorialLabel.text = this.DisabledString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.DisabledTexture;
					this.ShadowLabel.text = this.TutorialLabel.text;
				}
				else if (Input.GetButtonDown("A"))
				{
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.Blur.enabled = false;
					Time.timeScale = 1f;
					this.Show = false;
					this.Hide = true;
				}
			}
		}
		else if (this.Hide)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			if (base.transform.localScale.x < 0.1f)
			{
				base.transform.localScale = new Vector3(0f, 0f, 0f);
				this.Hide = false;
				if (OptionGlobals.TutorialsOff)
				{
					base.enabled = false;
				}
			}
		}
		if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Yandere.Aiming && !this.Yandere.PauseScreen.Show && !this.Yandere.CinematicCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				if (!this.IgnoreClothing && this.ShowClothingMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClothing = true;
					this.IgnoreClothing = true;
					this.TitleLabel.text = "No Spare Clothing";
					this.TutorialLabel.text = this.ClothingString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClothingTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreCouncil && this.ShowCouncilMessage && !this.Show)
				{
					TutorialGlobals.IgnoreCouncil = true;
					this.IgnoreCouncil = true;
					this.TitleLabel.text = "Student Council";
					this.TutorialLabel.text = this.CouncilString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.CouncilTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreTeacher && this.ShowTeacherMessage && !this.Show)
				{
					TutorialGlobals.IgnoreTeacher = true;
					this.IgnoreTeacher = true;
					this.TitleLabel.text = "Teachers";
					this.TutorialLabel.text = this.TeacherString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.TeacherTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreLocker && this.ShowLockerMessage && !this.Show)
				{
					TutorialGlobals.IgnoreLocker = true;
					this.IgnoreLocker = true;
					this.TitleLabel.text = "Notes In Lockers";
					this.TutorialLabel.text = this.LockerString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.LockerTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePolice && this.ShowPoliceMessage && !this.Show)
				{
					TutorialGlobals.IgnorePolice = true;
					this.IgnorePolice = true;
					this.TitleLabel.text = "Police";
					this.TutorialLabel.text = this.PoliceString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoliceTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSanity && this.ShowSanityMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSanity = true;
					this.IgnoreSanity = true;
					this.TitleLabel.text = "Restoring Sanity";
					this.TutorialLabel.text = this.SanityString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SanityTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreSenpai && this.ShowSenpaiMessage && !this.Show)
				{
					TutorialGlobals.IgnoreSenpai = true;
					this.IgnoreSenpai = true;
					this.TitleLabel.text = "Your Senpai";
					this.TutorialLabel.text = this.SenpaiString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.SenpaiTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreVision)
				{
					if (this.Yandere.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.Yandere.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position))
					{
						this.ShowVisionMessage = true;
					}
					if (this.ShowVisionMessage && !this.Show)
					{
						TutorialGlobals.IgnoreVision = true;
						this.IgnoreVision = true;
						this.TitleLabel.text = "Yandere Vision";
						this.TutorialLabel.text = this.VisionString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.VisionTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreWeapon)
				{
					if (this.Yandere.Armed)
					{
						this.ShowWeaponMessage = true;
					}
					if (this.ShowWeaponMessage && !this.Show)
					{
						TutorialGlobals.IgnoreWeapon = true;
						this.IgnoreWeapon = true;
						this.TitleLabel.text = "Weapons";
						this.TutorialLabel.text = this.WeaponString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.WeaponTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreBlood && this.ShowBloodMessage && !this.Show)
				{
					TutorialGlobals.IgnoreBlood = true;
					this.IgnoreBlood = true;
					this.TitleLabel.text = "Bloody Clothing";
					this.TutorialLabel.text = this.BloodString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.BloodTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreClass && this.ShowClassMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClass = true;
					this.IgnoreClass = true;
					this.TitleLabel.text = "Attending Class";
					this.TutorialLabel.text = this.ClassString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClassTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePhoto)
				{
					if (this.Yandere.transform.position.z > -50f)
					{
						this.ShowPhotoMessage = true;
					}
					if (this.ShowPhotoMessage && !this.Show)
					{
						TutorialGlobals.IgnorePhoto = true;
						this.IgnorePhoto = true;
						this.TitleLabel.text = "Taking Photographs";
						this.TutorialLabel.text = this.PhotoString;
						this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
						this.TutorialImage.mainTexture = this.PhotoTexture;
						this.SummonWindow();
					}
				}
				if (!this.IgnoreClub && this.ShowClubMessage && !this.Show)
				{
					TutorialGlobals.IgnoreClub = true;
					this.IgnoreClub = true;
					this.TitleLabel.text = "Joining Clubs";
					this.TutorialLabel.text = this.ClubString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.ClubTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreInfo && this.ShowInfoMessage && !this.Show)
				{
					TutorialGlobals.IgnoreInfo = true;
					this.IgnoreInfo = true;
					this.TitleLabel.text = "Info-chan's Services";
					this.TutorialLabel.text = this.InfoString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.InfoTexture;
					this.SummonWindow();
				}
				if (!this.IgnorePool && this.ShowPoolMessage && !this.Show)
				{
					TutorialGlobals.IgnorePool = true;
					this.IgnorePool = true;
					this.TitleLabel.text = "Cleaning Blood";
					this.TutorialLabel.text = this.PoolString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.PoolTexture;
					this.SummonWindow();
				}
				if (!this.IgnoreRep && this.ShowRepMessage && !this.Show)
				{
					TutorialGlobals.IgnoreRep = true;
					this.IgnoreRep = true;
					this.TitleLabel.text = "Reputation";
					this.TutorialLabel.text = this.RepString;
					this.TutorialLabel.text = this.TutorialLabel.text.Replace('@', '\n');
					this.TutorialImage.mainTexture = this.RepTexture;
					this.SummonWindow();
				}
			}
		}
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x0019A648 File Offset: 0x00198A48
	private void SummonWindow()
	{
		this.ShadowLabel.text = this.TutorialLabel.text;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Blur.enabled = true;
		Time.timeScale = 0f;
		this.Show = true;
		this.Timer = 0f;
	}

	// Token: 0x040036EA RID: 14058
	public YandereScript Yandere;

	// Token: 0x040036EB RID: 14059
	public bool ShowClothingMessage;

	// Token: 0x040036EC RID: 14060
	public bool ShowCouncilMessage;

	// Token: 0x040036ED RID: 14061
	public bool ShowTeacherMessage;

	// Token: 0x040036EE RID: 14062
	public bool ShowLockerMessage;

	// Token: 0x040036EF RID: 14063
	public bool ShowPoliceMessage;

	// Token: 0x040036F0 RID: 14064
	public bool ShowSanityMessage;

	// Token: 0x040036F1 RID: 14065
	public bool ShowSenpaiMessage;

	// Token: 0x040036F2 RID: 14066
	public bool ShowVisionMessage;

	// Token: 0x040036F3 RID: 14067
	public bool ShowWeaponMessage;

	// Token: 0x040036F4 RID: 14068
	public bool ShowBloodMessage;

	// Token: 0x040036F5 RID: 14069
	public bool ShowClassMessage;

	// Token: 0x040036F6 RID: 14070
	public bool ShowPhotoMessage;

	// Token: 0x040036F7 RID: 14071
	public bool ShowClubMessage;

	// Token: 0x040036F8 RID: 14072
	public bool ShowInfoMessage;

	// Token: 0x040036F9 RID: 14073
	public bool ShowPoolMessage;

	// Token: 0x040036FA RID: 14074
	public bool ShowRepMessage;

	// Token: 0x040036FB RID: 14075
	public bool IgnoreClothing;

	// Token: 0x040036FC RID: 14076
	public bool IgnoreCouncil;

	// Token: 0x040036FD RID: 14077
	public bool IgnoreTeacher;

	// Token: 0x040036FE RID: 14078
	public bool IgnoreLocker;

	// Token: 0x040036FF RID: 14079
	public bool IgnorePolice;

	// Token: 0x04003700 RID: 14080
	public bool IgnoreSanity;

	// Token: 0x04003701 RID: 14081
	public bool IgnoreSenpai;

	// Token: 0x04003702 RID: 14082
	public bool IgnoreVision;

	// Token: 0x04003703 RID: 14083
	public bool IgnoreWeapon;

	// Token: 0x04003704 RID: 14084
	public bool IgnoreBlood;

	// Token: 0x04003705 RID: 14085
	public bool IgnoreClass;

	// Token: 0x04003706 RID: 14086
	public bool IgnorePhoto;

	// Token: 0x04003707 RID: 14087
	public bool IgnoreClub;

	// Token: 0x04003708 RID: 14088
	public bool IgnoreInfo;

	// Token: 0x04003709 RID: 14089
	public bool IgnorePool;

	// Token: 0x0400370A RID: 14090
	public bool IgnoreRep;

	// Token: 0x0400370B RID: 14091
	public bool Hide;

	// Token: 0x0400370C RID: 14092
	public bool Show;

	// Token: 0x0400370D RID: 14093
	public UILabel TutorialLabel;

	// Token: 0x0400370E RID: 14094
	public UILabel ShadowLabel;

	// Token: 0x0400370F RID: 14095
	public UILabel TitleLabel;

	// Token: 0x04003710 RID: 14096
	public UITexture TutorialImage;

	// Token: 0x04003711 RID: 14097
	public string DisabledString;

	// Token: 0x04003712 RID: 14098
	public Texture DisabledTexture;

	// Token: 0x04003713 RID: 14099
	public string ClothingString;

	// Token: 0x04003714 RID: 14100
	public Texture ClothingTexture;

	// Token: 0x04003715 RID: 14101
	public string CouncilString;

	// Token: 0x04003716 RID: 14102
	public Texture CouncilTexture;

	// Token: 0x04003717 RID: 14103
	public string TeacherString;

	// Token: 0x04003718 RID: 14104
	public Texture TeacherTexture;

	// Token: 0x04003719 RID: 14105
	public string LockerString;

	// Token: 0x0400371A RID: 14106
	public Texture LockerTexture;

	// Token: 0x0400371B RID: 14107
	public string PoliceString;

	// Token: 0x0400371C RID: 14108
	public Texture PoliceTexture;

	// Token: 0x0400371D RID: 14109
	public string SanityString;

	// Token: 0x0400371E RID: 14110
	public Texture SanityTexture;

	// Token: 0x0400371F RID: 14111
	public string SenpaiString;

	// Token: 0x04003720 RID: 14112
	public Texture SenpaiTexture;

	// Token: 0x04003721 RID: 14113
	public string VisionString;

	// Token: 0x04003722 RID: 14114
	public Texture VisionTexture;

	// Token: 0x04003723 RID: 14115
	public string WeaponString;

	// Token: 0x04003724 RID: 14116
	public Texture WeaponTexture;

	// Token: 0x04003725 RID: 14117
	public string BloodString;

	// Token: 0x04003726 RID: 14118
	public Texture BloodTexture;

	// Token: 0x04003727 RID: 14119
	public string ClassString;

	// Token: 0x04003728 RID: 14120
	public Texture ClassTexture;

	// Token: 0x04003729 RID: 14121
	public string PhotoString;

	// Token: 0x0400372A RID: 14122
	public Texture PhotoTexture;

	// Token: 0x0400372B RID: 14123
	public string ClubString;

	// Token: 0x0400372C RID: 14124
	public Texture ClubTexture;

	// Token: 0x0400372D RID: 14125
	public string InfoString;

	// Token: 0x0400372E RID: 14126
	public Texture InfoTexture;

	// Token: 0x0400372F RID: 14127
	public string PoolString;

	// Token: 0x04003730 RID: 14128
	public Texture PoolTexture;

	// Token: 0x04003731 RID: 14129
	public string RepString;

	// Token: 0x04003732 RID: 14130
	public Texture RepTexture;

	// Token: 0x04003733 RID: 14131
	public float Timer;
}
