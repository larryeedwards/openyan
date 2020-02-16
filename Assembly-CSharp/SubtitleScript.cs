using System;
using UnityEngine;

// Token: 0x02000538 RID: 1336
public class SubtitleScript : MonoBehaviour
{
	// Token: 0x0600213B RID: 8507 RVA: 0x001873D4 File Offset: 0x001857D4
	private void Awake()
	{
		this.Club3Info = this.SubClub3Info;
		this.ClubGreetings[3] = this.ClubGreetings[13];
		this.ClubUnwelcomes[3] = this.ClubUnwelcomes[13];
		this.ClubKicks[3] = this.ClubKicks[13];
		this.ClubJoins[3] = this.ClubJoins[13];
		this.ClubAccepts[3] = this.ClubAccepts[13];
		this.ClubRefuses[3] = this.ClubRefuses[13];
		this.ClubRejoins[3] = this.ClubRejoins[13];
		this.ClubExclusives[3] = this.ClubExclusives[13];
		this.ClubGrudges[3] = this.ClubGrudges[13];
		this.ClubQuits[3] = this.ClubQuits[13];
		this.ClubConfirms[3] = this.ClubConfirms[13];
		this.ClubDenies[3] = this.ClubDenies[13];
		this.ClubFarewells[3] = this.ClubFarewells[13];
		this.ClubActivities[3] = this.ClubActivities[13];
		this.ClubEarlies[3] = this.ClubEarlies[13];
		this.ClubLates[3] = this.ClubLates[13];
		this.ClubYeses[3] = this.ClubYeses[13];
		this.ClubNoes[3] = this.ClubNoes[13];
		this.Club3Clips = this.SubClub3Clips;
		this.ClubGreetingClips[3] = this.ClubGreetingClips[13];
		this.ClubUnwelcomeClips[3] = this.ClubUnwelcomeClips[13];
		this.ClubKickClips[3] = this.ClubKickClips[13];
		this.ClubJoinClips[3] = this.ClubJoinClips[13];
		this.ClubAcceptClips[3] = this.ClubAcceptClips[13];
		this.ClubRefuseClips[3] = this.ClubRefuseClips[13];
		this.ClubRejoinClips[3] = this.ClubRejoinClips[13];
		this.ClubExclusiveClips[3] = this.ClubExclusiveClips[13];
		this.ClubGrudgeClips[3] = this.ClubGrudgeClips[13];
		this.ClubQuitClips[3] = this.ClubQuitClips[13];
		this.ClubConfirmClips[3] = this.ClubConfirmClips[13];
		this.ClubDenyClips[3] = this.ClubDenyClips[13];
		this.ClubFarewellClips[3] = this.ClubFarewellClips[13];
		this.ClubActivityClips[3] = this.ClubActivityClips[13];
		this.ClubEarlyClips[3] = this.ClubEarlyClips[13];
		this.ClubLateClips[3] = this.ClubLateClips[13];
		this.ClubYesClips[3] = this.ClubYesClips[13];
		this.ClubNoClips[3] = this.ClubNoClips[13];
		this.SubtitleClipArrays = new SubtitleTypeAndAudioClipArrayDictionary
		{
			{
				SubtitleType.ClubAccept,
				new AudioClipArrayWrapper(this.ClubAcceptClips)
			},
			{
				SubtitleType.ClubActivity,
				new AudioClipArrayWrapper(this.ClubActivityClips)
			},
			{
				SubtitleType.ClubConfirm,
				new AudioClipArrayWrapper(this.ClubConfirmClips)
			},
			{
				SubtitleType.ClubDeny,
				new AudioClipArrayWrapper(this.ClubDenyClips)
			},
			{
				SubtitleType.ClubEarly,
				new AudioClipArrayWrapper(this.ClubEarlyClips)
			},
			{
				SubtitleType.ClubExclusive,
				new AudioClipArrayWrapper(this.ClubExclusiveClips)
			},
			{
				SubtitleType.ClubFarewell,
				new AudioClipArrayWrapper(this.ClubFarewellClips)
			},
			{
				SubtitleType.ClubGreeting,
				new AudioClipArrayWrapper(this.ClubGreetingClips)
			},
			{
				SubtitleType.ClubGrudge,
				new AudioClipArrayWrapper(this.ClubGrudgeClips)
			},
			{
				SubtitleType.ClubJoin,
				new AudioClipArrayWrapper(this.ClubJoinClips)
			},
			{
				SubtitleType.ClubKick,
				new AudioClipArrayWrapper(this.ClubKickClips)
			},
			{
				SubtitleType.ClubLate,
				new AudioClipArrayWrapper(this.ClubLateClips)
			},
			{
				SubtitleType.ClubNo,
				new AudioClipArrayWrapper(this.ClubNoClips)
			},
			{
				SubtitleType.ClubPlaceholderInfo,
				new AudioClipArrayWrapper(this.Club0Clips)
			},
			{
				SubtitleType.ClubCookingInfo,
				new AudioClipArrayWrapper(this.Club1Clips)
			},
			{
				SubtitleType.ClubDramaInfo,
				new AudioClipArrayWrapper(this.Club2Clips)
			},
			{
				SubtitleType.ClubOccultInfo,
				new AudioClipArrayWrapper(this.Club3Clips)
			},
			{
				SubtitleType.ClubArtInfo,
				new AudioClipArrayWrapper(this.Club4Clips)
			},
			{
				SubtitleType.ClubLightMusicInfo,
				new AudioClipArrayWrapper(this.Club5Clips)
			},
			{
				SubtitleType.ClubMartialArtsInfo,
				new AudioClipArrayWrapper(this.Club6Clips)
			},
			{
				SubtitleType.ClubPhotoInfoLight,
				new AudioClipArrayWrapper(this.Club7ClipsLight)
			},
			{
				SubtitleType.ClubPhotoInfoDark,
				new AudioClipArrayWrapper(this.Club7ClipsDark)
			},
			{
				SubtitleType.ClubScienceInfo,
				new AudioClipArrayWrapper(this.Club8Clips)
			},
			{
				SubtitleType.ClubSportsInfo,
				new AudioClipArrayWrapper(this.Club9Clips)
			},
			{
				SubtitleType.ClubGardenInfo,
				new AudioClipArrayWrapper(this.Club10Clips)
			},
			{
				SubtitleType.ClubGamingInfo,
				new AudioClipArrayWrapper(this.Club11Clips)
			},
			{
				SubtitleType.ClubDelinquentInfo,
				new AudioClipArrayWrapper(this.Club12Clips)
			},
			{
				SubtitleType.ClubQuit,
				new AudioClipArrayWrapper(this.ClubQuitClips)
			},
			{
				SubtitleType.ClubRefuse,
				new AudioClipArrayWrapper(this.ClubRefuseClips)
			},
			{
				SubtitleType.ClubRejoin,
				new AudioClipArrayWrapper(this.ClubRejoinClips)
			},
			{
				SubtitleType.ClubUnwelcome,
				new AudioClipArrayWrapper(this.ClubUnwelcomeClips)
			},
			{
				SubtitleType.ClubYes,
				new AudioClipArrayWrapper(this.ClubYesClips)
			},
			{
				SubtitleType.ClubPractice,
				new AudioClipArrayWrapper(this.ClubPracticeClips)
			},
			{
				SubtitleType.ClubPracticeYes,
				new AudioClipArrayWrapper(this.ClubPracticeYesClips)
			},
			{
				SubtitleType.ClubPracticeNo,
				new AudioClipArrayWrapper(this.ClubPracticeNoClips)
			},
			{
				SubtitleType.DrownReaction,
				new AudioClipArrayWrapper(this.DrownReactionClips)
			},
			{
				SubtitleType.EavesdropReaction,
				new AudioClipArrayWrapper(this.EavesdropClips)
			},
			{
				SubtitleType.RejectFood,
				new AudioClipArrayWrapper(this.FoodRejectionClips)
			},
			{
				SubtitleType.ViolenceReaction,
				new AudioClipArrayWrapper(this.ViolenceClips)
			},
			{
				SubtitleType.EventEavesdropReaction,
				new AudioClipArrayWrapper(this.EventEavesdropClips)
			},
			{
				SubtitleType.RivalEavesdropReaction,
				new AudioClipArrayWrapper(this.RivalEavesdropClips)
			},
			{
				SubtitleType.GrudgeWarning,
				new AudioClipArrayWrapper(this.GrudgeWarningClips)
			},
			{
				SubtitleType.LightSwitchReaction,
				new AudioClipArrayWrapper(this.LightSwitchClips)
			},
			{
				SubtitleType.LostPhone,
				new AudioClipArrayWrapper(this.LostPhoneClips)
			},
			{
				SubtitleType.NoteReaction,
				new AudioClipArrayWrapper(this.NoteReactionClips)
			},
			{
				SubtitleType.NoteReactionMale,
				new AudioClipArrayWrapper(this.NoteReactionMaleClips)
			},
			{
				SubtitleType.PickpocketReaction,
				new AudioClipArrayWrapper(this.PickpocketReactionClips)
			},
			{
				SubtitleType.RivalLostPhone,
				new AudioClipArrayWrapper(this.RivalLostPhoneClips)
			},
			{
				SubtitleType.RivalPickpocketReaction,
				new AudioClipArrayWrapper(this.RivalPickpocketReactionClips)
			},
			{
				SubtitleType.RivalSplashReaction,
				new AudioClipArrayWrapper(this.RivalSplashReactionClips)
			},
			{
				SubtitleType.SenpaiBloodReaction,
				new AudioClipArrayWrapper(this.SenpaiBloodReactionClips)
			},
			{
				SubtitleType.SenpaiInsanityReaction,
				new AudioClipArrayWrapper(this.SenpaiInsanityReactionClips)
			},
			{
				SubtitleType.SenpaiLewdReaction,
				new AudioClipArrayWrapper(this.SenpaiLewdReactionClips)
			},
			{
				SubtitleType.SenpaiMurderReaction,
				new AudioClipArrayWrapper(this.SenpaiMurderReactionClips)
			},
			{
				SubtitleType.SenpaiStalkingReaction,
				new AudioClipArrayWrapper(this.SenpaiStalkingReactionClips)
			},
			{
				SubtitleType.SenpaiWeaponReaction,
				new AudioClipArrayWrapper(this.SenpaiWeaponReactionClips)
			},
			{
				SubtitleType.SenpaiViolenceReaction,
				new AudioClipArrayWrapper(this.SenpaiViolenceReactionClips)
			},
			{
				SubtitleType.SenpaiRivalDeathReaction,
				new AudioClipArrayWrapper(this.SenpaiRivalDeathReactionClips)
			},
			{
				SubtitleType.RaibaruRivalDeathReaction,
				new AudioClipArrayWrapper(this.RaibaruRivalDeathReactionClips)
			},
			{
				SubtitleType.SplashReaction,
				new AudioClipArrayWrapper(this.SplashReactionClips)
			},
			{
				SubtitleType.SplashReactionMale,
				new AudioClipArrayWrapper(this.SplashReactionMaleClips)
			},
			{
				SubtitleType.Task6Line,
				new AudioClipArrayWrapper(this.Task6Clips)
			},
			{
				SubtitleType.Task7Line,
				new AudioClipArrayWrapper(this.Task7Clips)
			},
			{
				SubtitleType.Task8Line,
				new AudioClipArrayWrapper(this.Task8Clips)
			},
			{
				SubtitleType.Task11Line,
				new AudioClipArrayWrapper(this.Task11Clips)
			},
			{
				SubtitleType.Task13Line,
				new AudioClipArrayWrapper(this.Task13Clips)
			},
			{
				SubtitleType.Task14Line,
				new AudioClipArrayWrapper(this.Task14Clips)
			},
			{
				SubtitleType.Task15Line,
				new AudioClipArrayWrapper(this.Task15Clips)
			},
			{
				SubtitleType.Task25Line,
				new AudioClipArrayWrapper(this.Task25Clips)
			},
			{
				SubtitleType.Task28Line,
				new AudioClipArrayWrapper(this.Task28Clips)
			},
			{
				SubtitleType.Task30Line,
				new AudioClipArrayWrapper(this.Task30Clips)
			},
			{
				SubtitleType.Task34Line,
				new AudioClipArrayWrapper(this.Task34Clips)
			},
			{
				SubtitleType.Task36Line,
				new AudioClipArrayWrapper(this.Task36Clips)
			},
			{
				SubtitleType.Task37Line,
				new AudioClipArrayWrapper(this.Task37Clips)
			},
			{
				SubtitleType.Task38Line,
				new AudioClipArrayWrapper(this.Task38Clips)
			},
			{
				SubtitleType.Task52Line,
				new AudioClipArrayWrapper(this.Task52Clips)
			},
			{
				SubtitleType.Task76Line,
				new AudioClipArrayWrapper(this.Task76Clips)
			},
			{
				SubtitleType.Task77Line,
				new AudioClipArrayWrapper(this.Task77Clips)
			},
			{
				SubtitleType.Task78Line,
				new AudioClipArrayWrapper(this.Task78Clips)
			},
			{
				SubtitleType.Task79Line,
				new AudioClipArrayWrapper(this.Task79Clips)
			},
			{
				SubtitleType.Task80Line,
				new AudioClipArrayWrapper(this.Task80Clips)
			},
			{
				SubtitleType.Task81Line,
				new AudioClipArrayWrapper(this.Task81Clips)
			},
			{
				SubtitleType.TaskGenericLineMale,
				new AudioClipArrayWrapper(this.TaskGenericMaleClips)
			},
			{
				SubtitleType.TaskGenericLineFemale,
				new AudioClipArrayWrapper(this.TaskGenericFemaleClips)
			},
			{
				SubtitleType.TaskInquiry,
				new AudioClipArrayWrapper(this.TaskInquiryClips)
			},
			{
				SubtitleType.TeacherAttackReaction,
				new AudioClipArrayWrapper(this.TeacherAttackClips)
			},
			{
				SubtitleType.TeacherBloodHostile,
				new AudioClipArrayWrapper(this.TeacherBloodHostileClips)
			},
			{
				SubtitleType.TeacherBloodReaction,
				new AudioClipArrayWrapper(this.TeacherBloodClips)
			},
			{
				SubtitleType.TeacherCorpseInspection,
				new AudioClipArrayWrapper(this.TeacherInspectClips)
			},
			{
				SubtitleType.TeacherCorpseReaction,
				new AudioClipArrayWrapper(this.TeacherCorpseClips)
			},
			{
				SubtitleType.TeacherInsanityHostile,
				new AudioClipArrayWrapper(this.TeacherInsanityHostileClips)
			},
			{
				SubtitleType.TeacherInsanityReaction,
				new AudioClipArrayWrapper(this.TeacherInsanityClips)
			},
			{
				SubtitleType.TeacherLateReaction,
				new AudioClipArrayWrapper(this.TeacherLateClips)
			},
			{
				SubtitleType.TeacherLewdReaction,
				new AudioClipArrayWrapper(this.TeacherLewdClips)
			},
			{
				SubtitleType.TeacherMurderReaction,
				new AudioClipArrayWrapper(this.TeacherMurderClips)
			},
			{
				SubtitleType.TeacherPoliceReport,
				new AudioClipArrayWrapper(this.TeacherPoliceClips)
			},
			{
				SubtitleType.TeacherPrankReaction,
				new AudioClipArrayWrapper(this.TeacherPrankClips)
			},
			{
				SubtitleType.TeacherReportReaction,
				new AudioClipArrayWrapper(this.TeacherReportClips)
			},
			{
				SubtitleType.TeacherTheftReaction,
				new AudioClipArrayWrapper(this.TeacherTheftClips)
			},
			{
				SubtitleType.TeacherTrespassingReaction,
				new AudioClipArrayWrapper(this.TeacherTrespassClips)
			},
			{
				SubtitleType.TeacherWeaponHostile,
				new AudioClipArrayWrapper(this.TeacherWeaponHostileClips)
			},
			{
				SubtitleType.TeacherWeaponReaction,
				new AudioClipArrayWrapper(this.TeacherWeaponClips)
			},
			{
				SubtitleType.TeacherCoverUpHostile,
				new AudioClipArrayWrapper(this.TeacherCoverUpHostileClips)
			},
			{
				SubtitleType.YandereWhimper,
				new AudioClipArrayWrapper(this.YandereWhimperClips)
			},
			{
				SubtitleType.DelinquentAnnoy,
				new AudioClipArrayWrapper(this.DelinquentAnnoyClips)
			},
			{
				SubtitleType.DelinquentCase,
				new AudioClipArrayWrapper(this.DelinquentCaseClips)
			},
			{
				SubtitleType.DelinquentShove,
				new AudioClipArrayWrapper(this.DelinquentShoveClips)
			},
			{
				SubtitleType.DelinquentReaction,
				new AudioClipArrayWrapper(this.DelinquentReactionClips)
			},
			{
				SubtitleType.DelinquentWeaponReaction,
				new AudioClipArrayWrapper(this.DelinquentWeaponReactionClips)
			},
			{
				SubtitleType.DelinquentThreatened,
				new AudioClipArrayWrapper(this.DelinquentThreatenedClips)
			},
			{
				SubtitleType.DelinquentTaunt,
				new AudioClipArrayWrapper(this.DelinquentTauntClips)
			},
			{
				SubtitleType.DelinquentCalm,
				new AudioClipArrayWrapper(this.DelinquentCalmClips)
			},
			{
				SubtitleType.DelinquentFight,
				new AudioClipArrayWrapper(this.DelinquentFightClips)
			},
			{
				SubtitleType.DelinquentAvenge,
				new AudioClipArrayWrapper(this.DelinquentAvengeClips)
			},
			{
				SubtitleType.DelinquentWin,
				new AudioClipArrayWrapper(this.DelinquentWinClips)
			},
			{
				SubtitleType.DelinquentSurrender,
				new AudioClipArrayWrapper(this.DelinquentSurrenderClips)
			},
			{
				SubtitleType.DelinquentNoSurrender,
				new AudioClipArrayWrapper(this.DelinquentNoSurrenderClips)
			},
			{
				SubtitleType.DelinquentMurderReaction,
				new AudioClipArrayWrapper(this.DelinquentMurderReactionClips)
			},
			{
				SubtitleType.DelinquentCorpseReaction,
				new AudioClipArrayWrapper(this.DelinquentCorpseReactionClips)
			},
			{
				SubtitleType.DelinquentFriendCorpseReaction,
				new AudioClipArrayWrapper(this.DelinquentFriendCorpseReactionClips)
			},
			{
				SubtitleType.DelinquentResume,
				new AudioClipArrayWrapper(this.DelinquentResumeClips)
			},
			{
				SubtitleType.DelinquentFlee,
				new AudioClipArrayWrapper(this.DelinquentFleeClips)
			},
			{
				SubtitleType.DelinquentEnemyFlee,
				new AudioClipArrayWrapper(this.DelinquentEnemyFleeClips)
			},
			{
				SubtitleType.DelinquentFriendFlee,
				new AudioClipArrayWrapper(this.DelinquentFriendFleeClips)
			},
			{
				SubtitleType.DelinquentInjuredFlee,
				new AudioClipArrayWrapper(this.DelinquentInjuredFleeClips)
			},
			{
				SubtitleType.DelinquentCheer,
				new AudioClipArrayWrapper(this.DelinquentCheerClips)
			},
			{
				SubtitleType.DelinquentHmm,
				new AudioClipArrayWrapper(this.DelinquentHmmClips)
			},
			{
				SubtitleType.DelinquentGrudge,
				new AudioClipArrayWrapper(this.DelinquentGrudgeClips)
			},
			{
				SubtitleType.Dismissive,
				new AudioClipArrayWrapper(this.DismissiveClips)
			},
			{
				SubtitleType.EvilDelinquentCorpseReaction,
				new AudioClipArrayWrapper(this.EvilDelinquentCorpseReactionClips)
			},
			{
				SubtitleType.Eulogy,
				new AudioClipArrayWrapper(this.EulogyClips)
			},
			{
				SubtitleType.ObstacleMurderReaction,
				new AudioClipArrayWrapper(this.ObstacleMurderReactionClips)
			},
			{
				SubtitleType.ObstaclePoisonReaction,
				new AudioClipArrayWrapper(this.ObstaclePoisonReactionClips)
			}
		};
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x00188100 File Offset: 0x00186500
	private void Start()
	{
		this.Label.text = string.Empty;
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x00188112 File Offset: 0x00186512
	private string GetRandomString(string[] strings)
	{
		return strings[UnityEngine.Random.Range(0, strings.Length)];
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x00188120 File Offset: 0x00186520
	public void UpdateLabel(SubtitleType subtitleType, int ID, float Duration)
	{
		if (subtitleType == SubtitleType.WeaponAndBloodAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodInsanityReactions);
		}
		else if (subtitleType == SubtitleType.WeaponAndBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodReactions);
		}
		else if (subtitleType == SubtitleType.WeaponAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.WeaponInsanityReactions);
		}
		else if (subtitleType == SubtitleType.BloodAndInsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.BloodInsanityReactions);
		}
		else if (subtitleType == SubtitleType.WeaponReaction)
		{
			if (ID == 1)
			{
				this.Label.text = this.GetRandomString(this.KnifeReactions);
			}
			else if (ID == 2)
			{
				this.Label.text = this.GetRandomString(this.KatanaReactions);
			}
			else if (ID == 3)
			{
				this.Label.text = this.GetRandomString(this.SyringeReactions);
			}
			else if (ID == 7)
			{
				this.Label.text = this.GetRandomString(this.SawReactions);
			}
			else if (ID == 8)
			{
				if (this.StudentID < 31 || this.StudentID > 35)
				{
					this.Label.text = this.RitualReactions[0];
				}
				else
				{
					this.Label.text = this.RitualReactions[1];
				}
			}
			else if (ID == 9)
			{
				this.Label.text = this.GetRandomString(this.BatReactions);
			}
			else if (ID == 10)
			{
				this.Label.text = this.GetRandomString(this.ShovelReactions);
			}
			else if (ID == 11 || ID == 14 || ID == 16 || ID == 17 || ID == 22)
			{
				this.Label.text = this.GetRandomString(this.PropReactions);
			}
			else if (ID == 12)
			{
				this.Label.text = this.GetRandomString(this.DumbbellReactions);
			}
			else if (ID == 13 || ID == 15)
			{
				this.Label.text = this.GetRandomString(this.AxeReactions);
			}
			else if (ID > 17 && ID < 22)
			{
				this.Label.text = this.GetRandomString(this.DelinkWeaponReactions);
			}
			else if (ID == 23)
			{
				this.Label.text = this.GetRandomString(this.ExtinguisherReactions);
			}
			else if (ID == 24)
			{
				this.Label.text = this.GetRandomString(this.WrenchReactions);
			}
			else if (ID == 25)
			{
				this.Label.text = this.GetRandomString(this.GuitarReactions);
			}
		}
		else if (subtitleType == SubtitleType.BloodReaction)
		{
			this.Label.text = this.GetRandomString(this.BloodReactions);
		}
		else if (subtitleType == SubtitleType.BloodPoolReaction)
		{
			this.Label.text = this.BloodPoolReactions[ID];
		}
		else if (subtitleType == SubtitleType.BloodyWeaponReaction)
		{
			this.Label.text = this.BloodyWeaponReactions[ID];
		}
		else if (subtitleType == SubtitleType.LimbReaction)
		{
			this.Label.text = this.LimbReactions[ID];
		}
		else if (subtitleType == SubtitleType.WetBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.WetBloodReactions);
		}
		else if (subtitleType == SubtitleType.InsanityReaction)
		{
			this.Label.text = this.GetRandomString(this.InsanityReactions);
		}
		else if (subtitleType == SubtitleType.LewdReaction)
		{
			this.Label.text = this.GetRandomString(this.LewdReactions);
		}
		else if (subtitleType == SubtitleType.SuspiciousReaction)
		{
			this.Label.text = this.SuspiciousReactions[ID];
		}
		else if (subtitleType == SubtitleType.PoisonReaction)
		{
			this.Label.text = this.PoisonReactions[ID];
		}
		else if (subtitleType == SubtitleType.PrankReaction)
		{
			this.Label.text = this.GetRandomString(this.PrankReactions);
		}
		else if (subtitleType == SubtitleType.InterruptionReaction)
		{
			this.Label.text = this.InterruptReactions[ID];
		}
		else if (subtitleType == SubtitleType.IntrusionReaction)
		{
			this.Label.text = this.GetRandomString(this.IntrusionReactions);
		}
		else if (subtitleType == SubtitleType.TheftReaction)
		{
			this.Label.text = this.GetRandomString(this.TheftReactions);
		}
		else if (subtitleType == SubtitleType.KilledMood)
		{
			this.Label.text = this.GetRandomString(this.KilledMoods);
		}
		else if (subtitleType == SubtitleType.SendToLocker)
		{
			this.Label.text = this.SendToLockers[ID];
		}
		else if (subtitleType == SubtitleType.NoteReaction)
		{
			this.Label.text = this.NoteReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.NoteReactionMale)
		{
			this.Label.text = this.NoteReactionsMale[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.OfferSnack)
		{
			this.Label.text = this.OfferSnacks[ID];
		}
		else if (subtitleType == SubtitleType.AcceptFood)
		{
			this.Label.text = this.GetRandomString(this.FoodAccepts);
		}
		else if (subtitleType == SubtitleType.RejectFood)
		{
			this.Label.text = this.FoodRejects[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.EavesdropReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EavesdropReactions.Length);
			this.Label.text = this.EavesdropReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.ViolenceReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.ViolenceReactions.Length);
			this.Label.text = this.ViolenceReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.EventEavesdropReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EventEavesdropReactions.Length);
			this.Label.text = this.EventEavesdropReactions[this.RandomID];
		}
		else if (subtitleType == SubtitleType.RivalEavesdropReaction)
		{
			Debug.Log("Rival eavesdrop reaction. ID is: " + ID);
			this.Label.text = this.RivalEavesdropReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.PickpocketReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PickpocketReactions.Length);
			this.Label.text = this.PickpocketReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.PickpocketApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PickpocketApologies.Length);
			this.Label.text = this.PickpocketApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.CleaningApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.CleaningApologies.Length);
			this.Label.text = this.CleaningApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.PoisonApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.PoisonApologies.Length);
			this.Label.text = this.PoisonApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.HoldingBloodyClothingApology)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.HoldingBloodyClothingApologies.Length);
			this.Label.text = this.HoldingBloodyClothingApologies[this.RandomID];
		}
		else if (subtitleType == SubtitleType.RivalPickpocketReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.RivalPickpocketReactions.Length);
			this.Label.text = this.RivalPickpocketReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DrownReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DrownReactions.Length);
			this.Label.text = this.DrownReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.HmmReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.HmmReactions.Length);
				this.Label.text = this.HmmReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.HoldingBloodyClothingReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.HoldingBloodyClothingReactions.Length);
				this.Label.text = this.HoldingBloodyClothingReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.ParanoidReaction)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.ParanoidReactions.Length);
				this.Label.text = this.ParanoidReactions[this.RandomID];
			}
		}
		else if (subtitleType == SubtitleType.TeacherWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherWeaponReactions.Length);
			this.Label.text = this.TeacherWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherBloodReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherBloodReactions.Length);
			this.Label.text = this.TeacherBloodReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherInsanityReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherInsanityReactions.Length);
			this.Label.text = this.TeacherInsanityReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherWeaponHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherWeaponHostiles.Length);
			this.Label.text = this.TeacherWeaponHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherBloodHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherBloodHostiles.Length);
			this.Label.text = this.TeacherBloodHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherInsanityHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherInsanityHostiles.Length);
			this.Label.text = this.TeacherInsanityHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherCoverUpHostile)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherCoverUpHostiles.Length);
			this.Label.text = this.TeacherCoverUpHostiles[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherLewdReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherLewdReactions.Length);
			this.Label.text = this.TeacherLewdReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherTrespassingReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherTrespassReactions.Length);
			this.Label.text = this.TeacherTrespassReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherLateReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherLateReactions.Length);
			this.Label.text = this.TeacherLateReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherReportReaction)
		{
			this.Label.text = this.TeacherReportReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherCorpseReactions.Length);
			this.Label.text = this.TeacherCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherCorpseInspection)
		{
			this.Label.text = this.TeacherCorpseInspections[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherPoliceReport)
		{
			this.Label.text = this.TeacherPoliceReports[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherAttackReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherAttackReactions.Length);
			this.Label.text = this.TeacherAttackReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherMurderReaction)
		{
			this.Label.text = this.TeacherMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TeacherPrankReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherPrankReactions.Length);
			this.Label.text = this.TeacherPrankReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.TeacherTheftReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.TeacherTheftReactions.Length);
			this.Label.text = this.TeacherTheftReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentAnnoy)
		{
			this.Label.text = this.DelinquentAnnoys[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.DelinquentCase)
		{
			this.Label.text = this.DelinquentCases[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.DelinquentShove)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentShoves.Length);
			this.Label.text = this.DelinquentShoves[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentReactions.Length);
			this.Label.text = this.DelinquentReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentWeaponReactions.Length);
			this.Label.text = this.DelinquentWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentThreatened)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentThreateneds.Length);
			this.Label.text = this.DelinquentThreateneds[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentTaunt)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentTaunts.Length);
			this.Label.text = this.DelinquentTaunts[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCalm)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCalms.Length);
			this.Label.text = this.DelinquentCalms[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFight)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFights.Length);
			this.Label.text = this.DelinquentFights[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentAvenge)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentAvenges.Length);
			this.Label.text = this.DelinquentAvenges[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentWin)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentWins.Length);
			this.Label.text = this.DelinquentWins[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentSurrender)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentSurrenders.Length);
			this.Label.text = this.DelinquentSurrenders[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentNoSurrender)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentNoSurrenders.Length);
			this.Label.text = this.DelinquentNoSurrenders[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentMurderReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentMurderReactions.Length);
			this.Label.text = this.DelinquentMurderReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCorpseReactions.Length);
			this.Label.text = this.DelinquentCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFriendCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFriendCorpseReactions.Length);
			this.Label.text = this.DelinquentFriendCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentResume)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentResumes.Length);
			this.Label.text = this.DelinquentResumes[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFlees.Length);
			this.Label.text = this.DelinquentFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentEnemyFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentEnemyFlees.Length);
			this.Label.text = this.DelinquentEnemyFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentFriendFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentFriendFlees.Length);
			this.Label.text = this.DelinquentFriendFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentInjuredFlee)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentInjuredFlees.Length);
			this.Label.text = this.DelinquentInjuredFlees[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentCheer)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentCheers.Length);
			this.Label.text = this.DelinquentCheers[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.DelinquentHmm)
		{
			if (this.Label.text == string.Empty)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.DelinquentHmms.Length);
				this.Label.text = this.DelinquentHmms[this.RandomID];
				this.PlayVoice(subtitleType, this.RandomID);
			}
		}
		else if (subtitleType == SubtitleType.DelinquentGrudge)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.DelinquentGrudges.Length);
			this.Label.text = this.DelinquentGrudges[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.Dismissive)
		{
			this.Label.text = this.Dismissives[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.LostPhone)
		{
			this.Label.text = this.LostPhones[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RivalLostPhone)
		{
			this.Label.text = this.RivalLostPhones[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.MurderReaction)
		{
			this.Label.text = this.GetRandomString(this.MurderReactions);
		}
		else if (subtitleType == SubtitleType.CorpseReaction)
		{
			this.Label.text = this.CorpseReactions[ID];
		}
		else if (subtitleType == SubtitleType.CouncilCorpseReaction)
		{
			this.Label.text = this.CouncilCorpseReactions[ID];
		}
		else if (subtitleType == SubtitleType.CouncilToCounselor)
		{
			this.Label.text = this.CouncilToCounselors[ID];
		}
		else if (subtitleType == SubtitleType.LonerMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.LonerMurderReactions);
		}
		else if (subtitleType == SubtitleType.LonerCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.LonerCorpseReactions);
		}
		else if (subtitleType == SubtitleType.PetBloodReport)
		{
			this.Label.text = this.PetBloodReports[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodReaction)
		{
			this.Label.text = this.GetRandomString(this.PetBloodReactions);
		}
		else if (subtitleType == SubtitleType.PetCorpseReport)
		{
			this.Label.text = this.PetCorpseReports[ID];
		}
		else if (subtitleType == SubtitleType.PetCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.PetCorpseReactions);
		}
		else if (subtitleType == SubtitleType.PetLimbReport)
		{
			this.Label.text = this.PetLimbReports[ID];
		}
		else if (subtitleType == SubtitleType.PetLimbReaction)
		{
			this.Label.text = this.GetRandomString(this.PetLimbReactions);
		}
		else if (subtitleType == SubtitleType.PetMurderReport)
		{
			this.Label.text = this.PetMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.PetMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.PetMurderReactions);
		}
		else if (subtitleType == SubtitleType.PetWeaponReport)
		{
			this.Label.text = this.PetWeaponReports[ID];
		}
		else if (subtitleType == SubtitleType.PetWeaponReaction)
		{
			this.Label.text = this.PetWeaponReactions[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodyWeaponReport)
		{
			this.Label.text = this.PetBloodyWeaponReports[ID];
		}
		else if (subtitleType == SubtitleType.PetBloodyWeaponReaction)
		{
			this.Label.text = this.GetRandomString(this.PetBloodyWeaponReactions);
		}
		else if (subtitleType == SubtitleType.EvilCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.EvilCorpseReactions);
		}
		else if (subtitleType == SubtitleType.EvilDelinquentCorpseReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.EvilDelinquentCorpseReactions.Length);
			this.Label.text = this.EvilDelinquentCorpseReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.HeroMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.HeroMurderReactions);
		}
		else if (subtitleType == SubtitleType.CowardMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.CowardMurderReactions);
		}
		else if (subtitleType == SubtitleType.EvilMurderReaction)
		{
			this.Label.text = this.GetRandomString(this.EvilMurderReactions);
		}
		else if (subtitleType == SubtitleType.SocialDeathReaction)
		{
			this.Label.text = this.GetRandomString(this.SocialDeathReactions);
		}
		else if (subtitleType == SubtitleType.LovestruckDeathReaction)
		{
			this.Label.text = this.LovestruckDeathReactions[ID];
		}
		else if (subtitleType == SubtitleType.LovestruckMurderReport)
		{
			this.Label.text = this.LovestruckMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.LovestruckCorpseReport)
		{
			this.Label.text = this.LovestruckCorpseReports[ID];
		}
		else if (subtitleType == SubtitleType.SocialReport)
		{
			this.Label.text = this.GetRandomString(this.SocialReports);
		}
		else if (subtitleType == SubtitleType.SocialFear)
		{
			this.Label.text = this.GetRandomString(this.SocialFears);
		}
		else if (subtitleType == SubtitleType.SocialTerror)
		{
			this.Label.text = this.GetRandomString(this.SocialTerrors);
		}
		else if (subtitleType == SubtitleType.RepeatReaction)
		{
			this.Label.text = this.GetRandomString(this.RepeatReactions);
		}
		else if (subtitleType == SubtitleType.Greeting)
		{
			this.Label.text = this.GetRandomString(this.Greetings);
		}
		else if (subtitleType == SubtitleType.PlayerFarewell)
		{
			this.Label.text = this.GetRandomString(this.PlayerFarewells);
		}
		else if (subtitleType == SubtitleType.StudentFarewell)
		{
			this.Label.text = this.GetRandomString(this.StudentFarewells);
		}
		else if (subtitleType == SubtitleType.InsanityApology)
		{
			this.Label.text = this.GetRandomString(this.InsanityApologies);
		}
		else if (subtitleType == SubtitleType.WeaponAndBloodApology)
		{
			this.Label.text = this.GetRandomString(this.WeaponBloodApologies);
		}
		else if (subtitleType == SubtitleType.WeaponApology)
		{
			this.Label.text = this.GetRandomString(this.WeaponApologies);
		}
		else if (subtitleType == SubtitleType.BloodApology)
		{
			this.Label.text = this.GetRandomString(this.BloodApologies);
		}
		else if (subtitleType == SubtitleType.LewdApology)
		{
			this.Label.text = this.GetRandomString(this.LewdApologies);
		}
		else if (subtitleType == SubtitleType.SuspiciousApology)
		{
			this.Label.text = this.GetRandomString(this.SuspiciousApologies);
		}
		else if (subtitleType == SubtitleType.EavesdropApology)
		{
			this.Label.text = this.GetRandomString(this.EavesdropApologies);
		}
		else if (subtitleType == SubtitleType.ViolenceApology)
		{
			this.Label.text = this.GetRandomString(this.ViolenceApologies);
		}
		else if (subtitleType == SubtitleType.TheftApology)
		{
			this.Label.text = this.GetRandomString(this.TheftApologies);
		}
		else if (subtitleType == SubtitleType.EventApology)
		{
			this.Label.text = this.GetRandomString(this.EventApologies);
		}
		else if (subtitleType == SubtitleType.ClassApology)
		{
			this.Label.text = this.GetRandomString(this.ClassApologies);
		}
		else if (subtitleType == SubtitleType.AccidentApology)
		{
			this.Label.text = this.GetRandomString(this.AccidentApologies);
		}
		else if (subtitleType == SubtitleType.SadApology)
		{
			this.Label.text = this.GetRandomString(this.SadApologies);
		}
		else if (subtitleType == SubtitleType.Dismissive)
		{
			this.Label.text = this.Dismissives[ID];
		}
		else if (subtitleType == SubtitleType.Forgiving)
		{
			this.Label.text = this.GetRandomString(this.Forgivings);
		}
		else if (subtitleType == SubtitleType.ForgivingAccident)
		{
			this.Label.text = this.GetRandomString(this.AccidentForgivings);
		}
		else if (subtitleType == SubtitleType.ForgivingInsanity)
		{
			this.Label.text = this.GetRandomString(this.InsanityForgivings);
		}
		else if (subtitleType == SubtitleType.Impatience)
		{
			this.Label.text = this.Impatiences[ID];
		}
		else if (subtitleType == SubtitleType.PlayerCompliment)
		{
			this.Label.text = this.GetRandomString(this.PlayerCompliments);
		}
		else if (subtitleType == SubtitleType.StudentHighCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentHighCompliments);
		}
		else if (subtitleType == SubtitleType.StudentMidCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentMidCompliments);
		}
		else if (subtitleType == SubtitleType.StudentLowCompliment)
		{
			this.Label.text = this.GetRandomString(this.StudentLowCompliments);
		}
		else if (subtitleType == SubtitleType.PlayerGossip)
		{
			this.Label.text = this.GetRandomString(this.PlayerGossip);
		}
		else if (subtitleType == SubtitleType.StudentGossip)
		{
			this.Label.text = this.GetRandomString(this.StudentGossip);
		}
		else if (subtitleType == SubtitleType.PlayerFollow)
		{
			this.Label.text = this.PlayerFollows[ID];
		}
		else if (subtitleType == SubtitleType.StudentFollow)
		{
			this.Label.text = this.StudentFollows[ID];
		}
		else if (subtitleType == SubtitleType.PlayerLeave)
		{
			this.Label.text = this.PlayerLeaves[ID];
		}
		else if (subtitleType == SubtitleType.StudentLeave)
		{
			this.Label.text = this.StudentLeaves[ID];
		}
		else if (subtitleType == SubtitleType.StudentStay)
		{
			this.Label.text = this.StudentStays[ID];
		}
		else if (subtitleType == SubtitleType.PlayerDistract)
		{
			this.Label.text = this.PlayerDistracts[ID];
		}
		else if (subtitleType == SubtitleType.StudentDistract)
		{
			this.Label.text = this.StudentDistracts[ID];
		}
		else if (subtitleType == SubtitleType.StudentDistractRefuse)
		{
			this.Label.text = this.GetRandomString(this.StudentDistractRefuses);
		}
		else if (subtitleType == SubtitleType.StudentDistractBullyRefuse)
		{
			this.Label.text = this.GetRandomString(this.StudentDistractBullyRefuses);
		}
		else if (subtitleType == SubtitleType.StopFollowApology)
		{
			this.Label.text = this.StopFollowApologies[ID];
		}
		else if (subtitleType == SubtitleType.GrudgeWarning)
		{
			this.Label.text = this.GetRandomString(this.GrudgeWarnings);
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.GrudgeRefusal)
		{
			this.Label.text = this.GetRandomString(this.GrudgeRefusals);
		}
		else if (subtitleType == SubtitleType.CowardGrudge)
		{
			this.Label.text = this.GetRandomString(this.CowardGrudges);
		}
		else if (subtitleType == SubtitleType.EvilGrudge)
		{
			this.Label.text = this.GetRandomString(this.EvilGrudges);
		}
		else if (subtitleType == SubtitleType.PlayerLove)
		{
			this.Label.text = this.PlayerLove[ID];
		}
		else if (subtitleType == SubtitleType.SuitorLove)
		{
			this.Label.text = this.SuitorLove[ID];
		}
		else if (subtitleType == SubtitleType.RivalLove)
		{
			this.Label.text = this.RivalLove[ID];
		}
		else if (subtitleType == SubtitleType.RequestMedicine)
		{
			this.Label.text = this.RequestMedicines[ID];
		}
		else if (subtitleType == SubtitleType.ReturningWeapon)
		{
			this.Label.text = this.ReturningWeapons[ID];
		}
		else if (subtitleType == SubtitleType.Dying)
		{
			this.Label.text = this.GetRandomString(this.Deaths);
		}
		else if (subtitleType == SubtitleType.SenpaiInsanityReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiInsanityReactions.Length);
			this.Label.text = this.SenpaiInsanityReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiWeaponReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiWeaponReactions.Length);
			this.Label.text = this.SenpaiWeaponReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiBloodReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiBloodReactions.Length);
			this.Label.text = this.SenpaiBloodReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiLewdReaction)
		{
			this.Label.text = this.GetRandomString(this.SenpaiLewdReactions);
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiStalkingReaction)
		{
			this.Label.text = this.SenpaiStalkingReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SenpaiMurderReaction)
		{
			this.Label.text = this.SenpaiMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SenpaiCorpseReaction)
		{
			this.Label.text = this.GetRandomString(this.SenpaiCorpseReactions);
		}
		else if (subtitleType == SubtitleType.SenpaiViolenceReaction)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.SenpaiViolenceReactions.Length);
			this.Label.text = this.SenpaiViolenceReactions[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.SenpaiRivalDeathReaction)
		{
			this.Label.text = this.SenpaiRivalDeathReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RaibaruRivalDeathReaction)
		{
			this.Label.text = this.RaibaruRivalDeathReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.YandereWhimper)
		{
			this.RandomID = UnityEngine.Random.Range(0, this.YandereWhimpers.Length);
			this.Label.text = this.YandereWhimpers[this.RandomID];
			this.PlayVoice(subtitleType, this.RandomID);
		}
		else if (subtitleType == SubtitleType.StudentMurderReport)
		{
			this.Label.text = this.StudentMurderReports[ID];
		}
		else if (subtitleType == SubtitleType.SplashReaction)
		{
			this.Label.text = this.SplashReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.SplashReactionMale)
		{
			this.Label.text = this.SplashReactionsMale[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.RivalSplashReaction)
		{
			this.Label.text = this.RivalSplashReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.LightSwitchReaction)
		{
			this.Label.text = this.LightSwitchReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.PhotoAnnoyance)
		{
			while (this.RandomID == this.PreviousRandom)
			{
				this.RandomID = UnityEngine.Random.Range(0, this.PhotoAnnoyances.Length);
			}
			this.PreviousRandom = this.RandomID;
			this.Label.text = this.PhotoAnnoyances[this.RandomID];
		}
		else if (subtitleType == SubtitleType.Task6Line)
		{
			this.Label.text = this.Task6Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task7Line)
		{
			this.Label.text = this.Task7Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task8Line)
		{
			this.Label.text = this.Task8Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task11Line)
		{
			this.Label.text = this.Task11Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task13Line)
		{
			this.Label.text = this.Task13Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task14Line)
		{
			this.Label.text = this.Task14Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task15Line)
		{
			this.Label.text = this.Task15Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task25Line)
		{
			this.Label.text = this.Task25Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task28Line)
		{
			this.Label.text = this.Task28Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task30Line)
		{
			this.Label.text = this.Task30Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task32Line)
		{
			this.Label.text = this.Task32Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task33Line)
		{
			this.Label.text = this.Task33Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task34Line)
		{
			this.Label.text = this.Task34Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task36Line)
		{
			this.Label.text = this.Task36Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task37Line)
		{
			this.Label.text = this.Task37Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task38Line)
		{
			this.Label.text = this.Task38Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task52Line)
		{
			this.Label.text = this.Task52Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task76Line)
		{
			this.Label.text = this.Task76Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task77Line)
		{
			this.Label.text = this.Task77Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task78Line)
		{
			this.Label.text = this.Task78Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task79Line)
		{
			this.Label.text = this.Task79Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task80Line)
		{
			this.Label.text = this.Task80Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.Task81Line)
		{
			this.Label.text = this.Task81Lines[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.TaskGenericLine)
		{
			this.Label.text = "(PLACEHOLDER TASK - WILL BE REPLACED AFTER DEMO)\n" + this.TaskGenericLines[ID];
			if (this.Yandere.GetComponent<YandereScript>().TargetStudent.Male)
			{
				this.PlayVoice(SubtitleType.TaskGenericLineMale, ID);
			}
			else
			{
				this.PlayVoice(SubtitleType.TaskGenericLineFemale, ID);
			}
		}
		else if (subtitleType == SubtitleType.TaskInquiry)
		{
			this.Label.text = this.TaskInquiries[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGreeting)
		{
			this.Label.text = this.ClubGreetings[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubUnwelcome)
		{
			this.Label.text = this.ClubUnwelcomes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubKick)
		{
			this.Label.text = this.ClubKicks[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPractice)
		{
			this.Label.text = this.ClubPractices[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPracticeYes)
		{
			this.Label.text = this.ClubPracticeYeses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPracticeNo)
		{
			this.Label.text = this.ClubPracticeNoes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPlaceholderInfo)
		{
			this.Label.text = this.Club0Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubCookingInfo)
		{
			this.Label.text = this.Club1Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDramaInfo)
		{
			this.Label.text = this.Club2Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubOccultInfo)
		{
			this.Label.text = this.Club3Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubArtInfo)
		{
			this.Label.text = this.Club4Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubLightMusicInfo)
		{
			this.Label.text = this.Club5Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubMartialArtsInfo)
		{
			this.Label.text = this.Club6Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPhotoInfoLight)
		{
			this.Label.text = this.Club7InfoLight[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubPhotoInfoDark)
		{
			this.Label.text = this.Club7InfoDark[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubScienceInfo)
		{
			this.Label.text = this.Club8Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubSportsInfo)
		{
			this.Label.text = this.Club9Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGardenInfo)
		{
			this.Label.text = this.Club10Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGamingInfo)
		{
			this.Label.text = this.Club11Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDelinquentInfo)
		{
			this.Label.text = this.Club12Info[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubJoin)
		{
			this.Label.text = this.ClubJoins[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubAccept)
		{
			this.Label.text = this.ClubAccepts[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubRefuse)
		{
			this.Label.text = this.ClubRefuses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubRejoin)
		{
			this.Label.text = this.ClubRejoins[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubExclusive)
		{
			this.Label.text = this.ClubExclusives[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubGrudge)
		{
			this.Label.text = this.ClubGrudges[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubQuit)
		{
			this.Label.text = this.ClubQuits[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubConfirm)
		{
			this.Label.text = this.ClubConfirms[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubDeny)
		{
			this.Label.text = this.ClubDenies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubFarewell)
		{
			this.Label.text = this.ClubFarewells[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubActivity)
		{
			this.Label.text = this.ClubActivities[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubEarly)
		{
			this.Label.text = this.ClubEarlies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubLate)
		{
			this.Label.text = this.ClubLates[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubYes)
		{
			this.Label.text = this.ClubYeses[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ClubNo)
		{
			this.Label.text = this.ClubNoes[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.InfoNotice)
		{
			this.Label.text = this.InfoNotice;
		}
		else if (subtitleType == SubtitleType.StrictReaction)
		{
			this.Label.text = this.StrictReaction[ID];
		}
		else if (subtitleType == SubtitleType.CasualReaction)
		{
			this.Label.text = this.CasualReaction[ID];
		}
		else if (subtitleType == SubtitleType.GraceReaction)
		{
			this.Label.text = this.GraceReaction[ID];
		}
		else if (subtitleType == SubtitleType.EdgyReaction)
		{
			this.Label.text = this.EdgyReaction[ID];
		}
		else if (subtitleType == SubtitleType.Shoving)
		{
			this.Label.text = this.Shoving[ID];
		}
		else if (subtitleType == SubtitleType.Spraying)
		{
			this.Label.text = this.Spraying[ID];
		}
		else if (subtitleType == SubtitleType.Chasing)
		{
			this.Label.text = this.Chasing[ID];
		}
		else if (subtitleType == SubtitleType.Eulogy)
		{
			this.Label.text = this.Eulogies[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.AskForHelp)
		{
			this.Label.text = this.AskForHelps[ID];
		}
		else if (subtitleType == SubtitleType.GiveHelp)
		{
			this.Label.text = this.GiveHelps[ID];
		}
		else if (subtitleType == SubtitleType.RejectHelp)
		{
			this.Label.text = this.RejectHelps[ID];
		}
		else if (subtitleType == SubtitleType.ObstacleMurderReaction)
		{
			this.Label.text = this.ObstacleMurderReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		else if (subtitleType == SubtitleType.ObstaclePoisonReaction)
		{
			this.Label.text = this.ObstaclePoisonReactions[ID];
			this.PlayVoice(subtitleType, ID);
		}
		this.Timer = Duration;
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x0018B0AC File Offset: 0x001894AC
	private void Update()
	{
		if (this.Timer > 0f)
		{
			this.Timer -= Time.deltaTime;
			if (this.Timer <= 0f)
			{
				this.Jukebox.Dip = 1f;
				this.Label.text = string.Empty;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x0018B118 File Offset: 0x00189518
	private void PlayVoice(SubtitleType subtitleType, int ID)
	{
		if (this.CurrentClip != null)
		{
		}
		this.Jukebox.Dip = 0.5f;
		AudioClipArrayWrapper audioClipArrayWrapper;
		bool flag = this.SubtitleClipArrays.TryGetValue(subtitleType, out audioClipArrayWrapper);
		this.PlayClip(audioClipArrayWrapper[ID], base.transform.position);
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x0018B170 File Offset: 0x00189570
	public float GetClipLength(int StudentID, int TaskPhase)
	{
		if (StudentID == 6)
		{
			return this.Task6Clips[TaskPhase].length + 0.5f;
		}
		if (StudentID == 8)
		{
			return this.Task8Clips[TaskPhase].length;
		}
		if (StudentID == 11)
		{
			return this.Task11Clips[TaskPhase].length;
		}
		if (StudentID == 25)
		{
			return this.Task25Clips[TaskPhase].length;
		}
		if (StudentID == 28)
		{
			return this.Task28Clips[TaskPhase].length;
		}
		if (StudentID == 30)
		{
			return this.Task30Clips[TaskPhase].length;
		}
		if (StudentID == 36)
		{
			return this.Task36Clips[TaskPhase].length;
		}
		if (StudentID == 37)
		{
			return this.Task37Clips[TaskPhase].length;
		}
		if (StudentID == 38)
		{
			return this.Task38Clips[TaskPhase].length;
		}
		if (StudentID == 52)
		{
			return this.Task52Clips[TaskPhase].length;
		}
		if (StudentID == 76)
		{
			return this.Task76Clips[TaskPhase].length;
		}
		if (StudentID == 77)
		{
			return this.Task77Clips[TaskPhase].length;
		}
		if (StudentID == 78)
		{
			return this.Task78Clips[TaskPhase].length;
		}
		if (StudentID == 79)
		{
			return this.Task79Clips[TaskPhase].length;
		}
		if (StudentID == 80)
		{
			return this.Task80Clips[TaskPhase].length;
		}
		if (StudentID == 81)
		{
			return this.Task81Clips[TaskPhase].length;
		}
		if (this.Yandere.GetComponent<YandereScript>().TargetStudent.Male)
		{
			return this.TaskGenericMaleClips[TaskPhase].length;
		}
		return this.TaskGenericFemaleClips[TaskPhase].length;
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x0018B318 File Offset: 0x00189718
	public float GetClubClipLength(ClubType Club, int ClubPhase)
	{
		if (Club == ClubType.Cooking)
		{
			return this.Club1Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Drama)
		{
			return this.Club2Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Occult)
		{
			return this.Club3Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Art)
		{
			return this.Club4Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.LightMusic)
		{
			return this.Club5Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.MartialArts)
		{
			return this.Club6Clips[ClubPhase].length + 0.5f;
		}
		if (Club == ClubType.Photography)
		{
			if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
			{
				return this.Club7ClipsDark[ClubPhase].length + 0.5f;
			}
			return this.Club7ClipsLight[ClubPhase].length + 0.5f;
		}
		else
		{
			if (Club == ClubType.Science)
			{
				return this.Club8Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Sports)
			{
				return this.Club9Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Gardening)
			{
				return this.Club10Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Gaming)
			{
				return this.Club11Clips[ClubPhase].length + 0.5f;
			}
			if (Club == ClubType.Delinquent)
			{
				return this.Club12Clips[ClubPhase].length + 0.5f;
			}
			return 0f;
		}
	}

	// Token: 0x06002143 RID: 8515 RVA: 0x0018B498 File Offset: 0x00189898
	private void PlayClip(AudioClip clip, Vector3 pos)
	{
		if (clip != null)
		{
			GameObject gameObject = new GameObject("TempAudio");
			if (this.Speaker != null)
			{
				gameObject.transform.position = this.Speaker.transform.position + base.transform.up;
				gameObject.transform.parent = this.Speaker.transform;
			}
			else
			{
				gameObject.transform.position = this.Yandere.transform.position + base.transform.up;
				gameObject.transform.parent = this.Yandere.transform;
			}
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = clip;
			audioSource.Play();
			UnityEngine.Object.Destroy(gameObject, clip.length);
			audioSource.rolloffMode = AudioRolloffMode.Linear;
			audioSource.spatialBlend = 1f;
			audioSource.minDistance = 5f;
			audioSource.maxDistance = 15f;
			this.CurrentClip = gameObject;
			audioSource.volume = ((this.Yandere.position.y >= gameObject.transform.position.y - 2f) ? 1f : 0f);
			this.Speaker = null;
		}
	}

	// Token: 0x040033AB RID: 13227
	public JukeboxScript Jukebox;

	// Token: 0x040033AC RID: 13228
	public Transform Yandere;

	// Token: 0x040033AD RID: 13229
	public UILabel Label;

	// Token: 0x040033AE RID: 13230
	public string[] WeaponBloodInsanityReactions;

	// Token: 0x040033AF RID: 13231
	public string[] WeaponBloodReactions;

	// Token: 0x040033B0 RID: 13232
	public string[] WeaponInsanityReactions;

	// Token: 0x040033B1 RID: 13233
	public string[] BloodInsanityReactions;

	// Token: 0x040033B2 RID: 13234
	public string[] BloodReactions;

	// Token: 0x040033B3 RID: 13235
	public string[] BloodPoolReactions;

	// Token: 0x040033B4 RID: 13236
	public string[] BloodyWeaponReactions;

	// Token: 0x040033B5 RID: 13237
	public string[] LimbReactions;

	// Token: 0x040033B6 RID: 13238
	public string[] WetBloodReactions;

	// Token: 0x040033B7 RID: 13239
	public string[] InsanityReactions;

	// Token: 0x040033B8 RID: 13240
	public string[] LewdReactions;

	// Token: 0x040033B9 RID: 13241
	public string[] SuspiciousReactions;

	// Token: 0x040033BA RID: 13242
	public string[] MurderReactions;

	// Token: 0x040033BB RID: 13243
	public string[] CowardMurderReactions;

	// Token: 0x040033BC RID: 13244
	public string[] EvilMurderReactions;

	// Token: 0x040033BD RID: 13245
	public string[] HoldingBloodyClothingReactions;

	// Token: 0x040033BE RID: 13246
	public string[] PetBloodReports;

	// Token: 0x040033BF RID: 13247
	public string[] PetBloodReactions;

	// Token: 0x040033C0 RID: 13248
	public string[] PetCorpseReports;

	// Token: 0x040033C1 RID: 13249
	public string[] PetCorpseReactions;

	// Token: 0x040033C2 RID: 13250
	public string[] PetLimbReports;

	// Token: 0x040033C3 RID: 13251
	public string[] PetLimbReactions;

	// Token: 0x040033C4 RID: 13252
	public string[] PetMurderReports;

	// Token: 0x040033C5 RID: 13253
	public string[] PetMurderReactions;

	// Token: 0x040033C6 RID: 13254
	public string[] PetWeaponReports;

	// Token: 0x040033C7 RID: 13255
	public string[] PetWeaponReactions;

	// Token: 0x040033C8 RID: 13256
	public string[] PetBloodyWeaponReports;

	// Token: 0x040033C9 RID: 13257
	public string[] PetBloodyWeaponReactions;

	// Token: 0x040033CA RID: 13258
	public string[] HeroMurderReactions;

	// Token: 0x040033CB RID: 13259
	public string[] LonerMurderReactions;

	// Token: 0x040033CC RID: 13260
	public string[] LonerCorpseReactions;

	// Token: 0x040033CD RID: 13261
	public string[] EvilCorpseReactions;

	// Token: 0x040033CE RID: 13262
	public string[] EvilDelinquentCorpseReactions;

	// Token: 0x040033CF RID: 13263
	public string[] SocialDeathReactions;

	// Token: 0x040033D0 RID: 13264
	public string[] LovestruckDeathReactions;

	// Token: 0x040033D1 RID: 13265
	public string[] LovestruckMurderReports;

	// Token: 0x040033D2 RID: 13266
	public string[] LovestruckCorpseReports;

	// Token: 0x040033D3 RID: 13267
	public string[] SocialReports;

	// Token: 0x040033D4 RID: 13268
	public string[] SocialFears;

	// Token: 0x040033D5 RID: 13269
	public string[] SocialTerrors;

	// Token: 0x040033D6 RID: 13270
	public string[] RepeatReactions;

	// Token: 0x040033D7 RID: 13271
	public string[] CorpseReactions;

	// Token: 0x040033D8 RID: 13272
	public string[] PoisonReactions;

	// Token: 0x040033D9 RID: 13273
	public string[] PrankReactions;

	// Token: 0x040033DA RID: 13274
	public string[] InterruptReactions;

	// Token: 0x040033DB RID: 13275
	public string[] IntrusionReactions;

	// Token: 0x040033DC RID: 13276
	public string[] NoteReactions;

	// Token: 0x040033DD RID: 13277
	public string[] NoteReactionsMale;

	// Token: 0x040033DE RID: 13278
	public string[] OfferSnacks;

	// Token: 0x040033DF RID: 13279
	public string[] FoodAccepts;

	// Token: 0x040033E0 RID: 13280
	public string[] FoodRejects;

	// Token: 0x040033E1 RID: 13281
	public string[] EavesdropReactions;

	// Token: 0x040033E2 RID: 13282
	public string[] ViolenceReactions;

	// Token: 0x040033E3 RID: 13283
	public string[] EventEavesdropReactions;

	// Token: 0x040033E4 RID: 13284
	public string[] RivalEavesdropReactions;

	// Token: 0x040033E5 RID: 13285
	public string[] PickpocketReactions;

	// Token: 0x040033E6 RID: 13286
	public string[] RivalPickpocketReactions;

	// Token: 0x040033E7 RID: 13287
	public string[] DrownReactions;

	// Token: 0x040033E8 RID: 13288
	public string[] ParanoidReactions;

	// Token: 0x040033E9 RID: 13289
	public string[] TheftReactions;

	// Token: 0x040033EA RID: 13290
	public string[] KilledMoods;

	// Token: 0x040033EB RID: 13291
	public string[] SendToLockers;

	// Token: 0x040033EC RID: 13292
	public string[] KnifeReactions;

	// Token: 0x040033ED RID: 13293
	public string[] SyringeReactions;

	// Token: 0x040033EE RID: 13294
	public string[] KatanaReactions;

	// Token: 0x040033EF RID: 13295
	public string[] SawReactions;

	// Token: 0x040033F0 RID: 13296
	public string[] RitualReactions;

	// Token: 0x040033F1 RID: 13297
	public string[] BatReactions;

	// Token: 0x040033F2 RID: 13298
	public string[] ShovelReactions;

	// Token: 0x040033F3 RID: 13299
	public string[] DumbbellReactions;

	// Token: 0x040033F4 RID: 13300
	public string[] AxeReactions;

	// Token: 0x040033F5 RID: 13301
	public string[] PropReactions;

	// Token: 0x040033F6 RID: 13302
	public string[] DelinkWeaponReactions;

	// Token: 0x040033F7 RID: 13303
	public string[] ExtinguisherReactions;

	// Token: 0x040033F8 RID: 13304
	public string[] WrenchReactions;

	// Token: 0x040033F9 RID: 13305
	public string[] GuitarReactions;

	// Token: 0x040033FA RID: 13306
	public string[] WeaponBloodApologies;

	// Token: 0x040033FB RID: 13307
	public string[] WeaponApologies;

	// Token: 0x040033FC RID: 13308
	public string[] BloodApologies;

	// Token: 0x040033FD RID: 13309
	public string[] InsanityApologies;

	// Token: 0x040033FE RID: 13310
	public string[] LewdApologies;

	// Token: 0x040033FF RID: 13311
	public string[] SuspiciousApologies;

	// Token: 0x04003400 RID: 13312
	public string[] EventApologies;

	// Token: 0x04003401 RID: 13313
	public string[] ClassApologies;

	// Token: 0x04003402 RID: 13314
	public string[] AccidentApologies;

	// Token: 0x04003403 RID: 13315
	public string[] SadApologies;

	// Token: 0x04003404 RID: 13316
	public string[] EavesdropApologies;

	// Token: 0x04003405 RID: 13317
	public string[] ViolenceApologies;

	// Token: 0x04003406 RID: 13318
	public string[] PickpocketApologies;

	// Token: 0x04003407 RID: 13319
	public string[] CleaningApologies;

	// Token: 0x04003408 RID: 13320
	public string[] PoisonApologies;

	// Token: 0x04003409 RID: 13321
	public string[] HoldingBloodyClothingApologies;

	// Token: 0x0400340A RID: 13322
	public string[] TheftApologies;

	// Token: 0x0400340B RID: 13323
	public string[] Greetings;

	// Token: 0x0400340C RID: 13324
	public string[] PlayerFarewells;

	// Token: 0x0400340D RID: 13325
	public string[] StudentFarewells;

	// Token: 0x0400340E RID: 13326
	public string[] Forgivings;

	// Token: 0x0400340F RID: 13327
	public string[] AccidentForgivings;

	// Token: 0x04003410 RID: 13328
	public string[] InsanityForgivings;

	// Token: 0x04003411 RID: 13329
	public string[] PlayerCompliments;

	// Token: 0x04003412 RID: 13330
	public string[] StudentHighCompliments;

	// Token: 0x04003413 RID: 13331
	public string[] StudentMidCompliments;

	// Token: 0x04003414 RID: 13332
	public string[] StudentLowCompliments;

	// Token: 0x04003415 RID: 13333
	public string[] PlayerGossip;

	// Token: 0x04003416 RID: 13334
	public string[] StudentGossip;

	// Token: 0x04003417 RID: 13335
	public string[] PlayerFollows;

	// Token: 0x04003418 RID: 13336
	public string[] StudentFollows;

	// Token: 0x04003419 RID: 13337
	public string[] PlayerLeaves;

	// Token: 0x0400341A RID: 13338
	public string[] StudentLeaves;

	// Token: 0x0400341B RID: 13339
	public string[] StudentStays;

	// Token: 0x0400341C RID: 13340
	public string[] PlayerDistracts;

	// Token: 0x0400341D RID: 13341
	public string[] StudentDistracts;

	// Token: 0x0400341E RID: 13342
	public string[] StudentDistractRefuses;

	// Token: 0x0400341F RID: 13343
	public string[] StudentDistractBullyRefuses;

	// Token: 0x04003420 RID: 13344
	public string[] StopFollowApologies;

	// Token: 0x04003421 RID: 13345
	public string[] GrudgeWarnings;

	// Token: 0x04003422 RID: 13346
	public string[] GrudgeRefusals;

	// Token: 0x04003423 RID: 13347
	public string[] CowardGrudges;

	// Token: 0x04003424 RID: 13348
	public string[] EvilGrudges;

	// Token: 0x04003425 RID: 13349
	public string[] PlayerLove;

	// Token: 0x04003426 RID: 13350
	public string[] SuitorLove;

	// Token: 0x04003427 RID: 13351
	public string[] RivalLove;

	// Token: 0x04003428 RID: 13352
	public string[] RequestMedicines;

	// Token: 0x04003429 RID: 13353
	public string[] ReturningWeapons;

	// Token: 0x0400342A RID: 13354
	public string[] Impatiences;

	// Token: 0x0400342B RID: 13355
	public string[] ImpatientFarewells;

	// Token: 0x0400342C RID: 13356
	public string[] Deaths;

	// Token: 0x0400342D RID: 13357
	public string[] SenpaiInsanityReactions;

	// Token: 0x0400342E RID: 13358
	public string[] SenpaiWeaponReactions;

	// Token: 0x0400342F RID: 13359
	public string[] SenpaiBloodReactions;

	// Token: 0x04003430 RID: 13360
	public string[] SenpaiLewdReactions;

	// Token: 0x04003431 RID: 13361
	public string[] SenpaiStalkingReactions;

	// Token: 0x04003432 RID: 13362
	public string[] SenpaiMurderReactions;

	// Token: 0x04003433 RID: 13363
	public string[] SenpaiCorpseReactions;

	// Token: 0x04003434 RID: 13364
	public string[] SenpaiViolenceReactions;

	// Token: 0x04003435 RID: 13365
	public string[] SenpaiRivalDeathReactions;

	// Token: 0x04003436 RID: 13366
	public string[] RaibaruRivalDeathReactions;

	// Token: 0x04003437 RID: 13367
	public string[] TeacherInsanityReactions;

	// Token: 0x04003438 RID: 13368
	public string[] TeacherWeaponReactions;

	// Token: 0x04003439 RID: 13369
	public string[] TeacherBloodReactions;

	// Token: 0x0400343A RID: 13370
	public string[] TeacherInsanityHostiles;

	// Token: 0x0400343B RID: 13371
	public string[] TeacherWeaponHostiles;

	// Token: 0x0400343C RID: 13372
	public string[] TeacherBloodHostiles;

	// Token: 0x0400343D RID: 13373
	public string[] TeacherCoverUpHostiles;

	// Token: 0x0400343E RID: 13374
	public string[] TeacherLewdReactions;

	// Token: 0x0400343F RID: 13375
	public string[] TeacherTrespassReactions;

	// Token: 0x04003440 RID: 13376
	public string[] TeacherLateReactions;

	// Token: 0x04003441 RID: 13377
	public string[] TeacherReportReactions;

	// Token: 0x04003442 RID: 13378
	public string[] TeacherCorpseReactions;

	// Token: 0x04003443 RID: 13379
	public string[] TeacherCorpseInspections;

	// Token: 0x04003444 RID: 13380
	public string[] TeacherPoliceReports;

	// Token: 0x04003445 RID: 13381
	public string[] TeacherAttackReactions;

	// Token: 0x04003446 RID: 13382
	public string[] TeacherMurderReactions;

	// Token: 0x04003447 RID: 13383
	public string[] TeacherPrankReactions;

	// Token: 0x04003448 RID: 13384
	public string[] TeacherTheftReactions;

	// Token: 0x04003449 RID: 13385
	public string[] DelinquentAnnoys;

	// Token: 0x0400344A RID: 13386
	public string[] DelinquentCases;

	// Token: 0x0400344B RID: 13387
	public string[] DelinquentShoves;

	// Token: 0x0400344C RID: 13388
	public string[] DelinquentReactions;

	// Token: 0x0400344D RID: 13389
	public string[] DelinquentWeaponReactions;

	// Token: 0x0400344E RID: 13390
	public string[] DelinquentThreateneds;

	// Token: 0x0400344F RID: 13391
	public string[] DelinquentTaunts;

	// Token: 0x04003450 RID: 13392
	public string[] DelinquentCalms;

	// Token: 0x04003451 RID: 13393
	public string[] DelinquentFights;

	// Token: 0x04003452 RID: 13394
	public string[] DelinquentAvenges;

	// Token: 0x04003453 RID: 13395
	public string[] DelinquentWins;

	// Token: 0x04003454 RID: 13396
	public string[] DelinquentSurrenders;

	// Token: 0x04003455 RID: 13397
	public string[] DelinquentNoSurrenders;

	// Token: 0x04003456 RID: 13398
	public string[] DelinquentMurderReactions;

	// Token: 0x04003457 RID: 13399
	public string[] DelinquentCorpseReactions;

	// Token: 0x04003458 RID: 13400
	public string[] DelinquentFriendCorpseReactions;

	// Token: 0x04003459 RID: 13401
	public string[] DelinquentResumes;

	// Token: 0x0400345A RID: 13402
	public string[] DelinquentFlees;

	// Token: 0x0400345B RID: 13403
	public string[] DelinquentEnemyFlees;

	// Token: 0x0400345C RID: 13404
	public string[] DelinquentFriendFlees;

	// Token: 0x0400345D RID: 13405
	public string[] DelinquentInjuredFlees;

	// Token: 0x0400345E RID: 13406
	public string[] DelinquentCheers;

	// Token: 0x0400345F RID: 13407
	public string[] DelinquentHmms;

	// Token: 0x04003460 RID: 13408
	public string[] DelinquentGrudges;

	// Token: 0x04003461 RID: 13409
	public string[] Dismissives;

	// Token: 0x04003462 RID: 13410
	public string[] LostPhones;

	// Token: 0x04003463 RID: 13411
	public string[] RivalLostPhones;

	// Token: 0x04003464 RID: 13412
	public string[] StudentMurderReports;

	// Token: 0x04003465 RID: 13413
	public string[] YandereWhimpers;

	// Token: 0x04003466 RID: 13414
	public string[] SplashReactions;

	// Token: 0x04003467 RID: 13415
	public string[] SplashReactionsMale;

	// Token: 0x04003468 RID: 13416
	public string[] RivalSplashReactions;

	// Token: 0x04003469 RID: 13417
	public string[] LightSwitchReactions;

	// Token: 0x0400346A RID: 13418
	public string[] PhotoAnnoyances;

	// Token: 0x0400346B RID: 13419
	public string[] Task6Lines;

	// Token: 0x0400346C RID: 13420
	public string[] Task7Lines;

	// Token: 0x0400346D RID: 13421
	public string[] Task8Lines;

	// Token: 0x0400346E RID: 13422
	public string[] Task11Lines;

	// Token: 0x0400346F RID: 13423
	public string[] Task13Lines;

	// Token: 0x04003470 RID: 13424
	public string[] Task14Lines;

	// Token: 0x04003471 RID: 13425
	public string[] Task15Lines;

	// Token: 0x04003472 RID: 13426
	public string[] Task25Lines;

	// Token: 0x04003473 RID: 13427
	public string[] Task28Lines;

	// Token: 0x04003474 RID: 13428
	public string[] Task30Lines;

	// Token: 0x04003475 RID: 13429
	public string[] Task32Lines;

	// Token: 0x04003476 RID: 13430
	public string[] Task33Lines;

	// Token: 0x04003477 RID: 13431
	public string[] Task34Lines;

	// Token: 0x04003478 RID: 13432
	public string[] Task36Lines;

	// Token: 0x04003479 RID: 13433
	public string[] Task37Lines;

	// Token: 0x0400347A RID: 13434
	public string[] Task38Lines;

	// Token: 0x0400347B RID: 13435
	public string[] Task52Lines;

	// Token: 0x0400347C RID: 13436
	public string[] Task76Lines;

	// Token: 0x0400347D RID: 13437
	public string[] Task77Lines;

	// Token: 0x0400347E RID: 13438
	public string[] Task78Lines;

	// Token: 0x0400347F RID: 13439
	public string[] Task79Lines;

	// Token: 0x04003480 RID: 13440
	public string[] Task80Lines;

	// Token: 0x04003481 RID: 13441
	public string[] Task81Lines;

	// Token: 0x04003482 RID: 13442
	public string[] TaskGenericLines;

	// Token: 0x04003483 RID: 13443
	public string[] TaskInquiries;

	// Token: 0x04003484 RID: 13444
	public string[] Club0Info;

	// Token: 0x04003485 RID: 13445
	public string[] Club1Info;

	// Token: 0x04003486 RID: 13446
	public string[] Club2Info;

	// Token: 0x04003487 RID: 13447
	public string[] Club3Info;

	// Token: 0x04003488 RID: 13448
	public string[] Club4Info;

	// Token: 0x04003489 RID: 13449
	public string[] Club5Info;

	// Token: 0x0400348A RID: 13450
	public string[] Club6Info;

	// Token: 0x0400348B RID: 13451
	public string[] Club7InfoLight;

	// Token: 0x0400348C RID: 13452
	public string[] Club7InfoDark;

	// Token: 0x0400348D RID: 13453
	public string[] Club8Info;

	// Token: 0x0400348E RID: 13454
	public string[] Club9Info;

	// Token: 0x0400348F RID: 13455
	public string[] Club10Info;

	// Token: 0x04003490 RID: 13456
	public string[] Club11Info;

	// Token: 0x04003491 RID: 13457
	public string[] Club12Info;

	// Token: 0x04003492 RID: 13458
	public string[] SubClub3Info;

	// Token: 0x04003493 RID: 13459
	public string[] ClubGreetings;

	// Token: 0x04003494 RID: 13460
	public string[] ClubUnwelcomes;

	// Token: 0x04003495 RID: 13461
	public string[] ClubKicks;

	// Token: 0x04003496 RID: 13462
	public string[] ClubJoins;

	// Token: 0x04003497 RID: 13463
	public string[] ClubAccepts;

	// Token: 0x04003498 RID: 13464
	public string[] ClubRefuses;

	// Token: 0x04003499 RID: 13465
	public string[] ClubRejoins;

	// Token: 0x0400349A RID: 13466
	public string[] ClubExclusives;

	// Token: 0x0400349B RID: 13467
	public string[] ClubGrudges;

	// Token: 0x0400349C RID: 13468
	public string[] ClubQuits;

	// Token: 0x0400349D RID: 13469
	public string[] ClubConfirms;

	// Token: 0x0400349E RID: 13470
	public string[] ClubDenies;

	// Token: 0x0400349F RID: 13471
	public string[] ClubFarewells;

	// Token: 0x040034A0 RID: 13472
	public string[] ClubActivities;

	// Token: 0x040034A1 RID: 13473
	public string[] ClubEarlies;

	// Token: 0x040034A2 RID: 13474
	public string[] ClubLates;

	// Token: 0x040034A3 RID: 13475
	public string[] ClubYeses;

	// Token: 0x040034A4 RID: 13476
	public string[] ClubNoes;

	// Token: 0x040034A5 RID: 13477
	public string[] ClubPractices;

	// Token: 0x040034A6 RID: 13478
	public string[] ClubPracticeYeses;

	// Token: 0x040034A7 RID: 13479
	public string[] ClubPracticeNoes;

	// Token: 0x040034A8 RID: 13480
	public string[] StrictReaction;

	// Token: 0x040034A9 RID: 13481
	public string[] CasualReaction;

	// Token: 0x040034AA RID: 13482
	public string[] GraceReaction;

	// Token: 0x040034AB RID: 13483
	public string[] EdgyReaction;

	// Token: 0x040034AC RID: 13484
	public string[] Spraying;

	// Token: 0x040034AD RID: 13485
	public string[] Shoving;

	// Token: 0x040034AE RID: 13486
	public string[] Chasing;

	// Token: 0x040034AF RID: 13487
	public string[] CouncilCorpseReactions;

	// Token: 0x040034B0 RID: 13488
	public string[] CouncilToCounselors;

	// Token: 0x040034B1 RID: 13489
	public string[] HmmReactions;

	// Token: 0x040034B2 RID: 13490
	public string[] Eulogies;

	// Token: 0x040034B3 RID: 13491
	public string[] AskForHelps;

	// Token: 0x040034B4 RID: 13492
	public string[] GiveHelps;

	// Token: 0x040034B5 RID: 13493
	public string[] RejectHelps;

	// Token: 0x040034B6 RID: 13494
	public string[] ObstacleMurderReactions;

	// Token: 0x040034B7 RID: 13495
	public string[] ObstaclePoisonReactions;

	// Token: 0x040034B8 RID: 13496
	public string InfoNotice;

	// Token: 0x040034B9 RID: 13497
	public int PreviousRandom;

	// Token: 0x040034BA RID: 13498
	public int RandomID;

	// Token: 0x040034BB RID: 13499
	public float Timer;

	// Token: 0x040034BC RID: 13500
	public int StudentID;

	// Token: 0x040034BD RID: 13501
	public AudioClip[] NoteReactionClips;

	// Token: 0x040034BE RID: 13502
	public AudioClip[] NoteReactionMaleClips;

	// Token: 0x040034BF RID: 13503
	public AudioClip[] GrudgeWarningClips;

	// Token: 0x040034C0 RID: 13504
	public AudioClip[] SenpaiInsanityReactionClips;

	// Token: 0x040034C1 RID: 13505
	public AudioClip[] SenpaiWeaponReactionClips;

	// Token: 0x040034C2 RID: 13506
	public AudioClip[] SenpaiBloodReactionClips;

	// Token: 0x040034C3 RID: 13507
	public AudioClip[] SenpaiLewdReactionClips;

	// Token: 0x040034C4 RID: 13508
	public AudioClip[] SenpaiStalkingReactionClips;

	// Token: 0x040034C5 RID: 13509
	public AudioClip[] SenpaiMurderReactionClips;

	// Token: 0x040034C6 RID: 13510
	public AudioClip[] SenpaiViolenceReactionClips;

	// Token: 0x040034C7 RID: 13511
	public AudioClip[] SenpaiRivalDeathReactionClips;

	// Token: 0x040034C8 RID: 13512
	public AudioClip[] RaibaruRivalDeathReactionClips;

	// Token: 0x040034C9 RID: 13513
	public AudioClip[] YandereWhimperClips;

	// Token: 0x040034CA RID: 13514
	public AudioClip[] TeacherWeaponClips;

	// Token: 0x040034CB RID: 13515
	public AudioClip[] TeacherBloodClips;

	// Token: 0x040034CC RID: 13516
	public AudioClip[] TeacherInsanityClips;

	// Token: 0x040034CD RID: 13517
	public AudioClip[] TeacherWeaponHostileClips;

	// Token: 0x040034CE RID: 13518
	public AudioClip[] TeacherBloodHostileClips;

	// Token: 0x040034CF RID: 13519
	public AudioClip[] TeacherInsanityHostileClips;

	// Token: 0x040034D0 RID: 13520
	public AudioClip[] TeacherCoverUpHostileClips;

	// Token: 0x040034D1 RID: 13521
	public AudioClip[] TeacherLewdClips;

	// Token: 0x040034D2 RID: 13522
	public AudioClip[] TeacherTrespassClips;

	// Token: 0x040034D3 RID: 13523
	public AudioClip[] TeacherLateClips;

	// Token: 0x040034D4 RID: 13524
	public AudioClip[] TeacherReportClips;

	// Token: 0x040034D5 RID: 13525
	public AudioClip[] TeacherCorpseClips;

	// Token: 0x040034D6 RID: 13526
	public AudioClip[] TeacherInspectClips;

	// Token: 0x040034D7 RID: 13527
	public AudioClip[] TeacherPoliceClips;

	// Token: 0x040034D8 RID: 13528
	public AudioClip[] TeacherAttackClips;

	// Token: 0x040034D9 RID: 13529
	public AudioClip[] TeacherMurderClips;

	// Token: 0x040034DA RID: 13530
	public AudioClip[] TeacherPrankClips;

	// Token: 0x040034DB RID: 13531
	public AudioClip[] TeacherTheftClips;

	// Token: 0x040034DC RID: 13532
	public AudioClip[] LostPhoneClips;

	// Token: 0x040034DD RID: 13533
	public AudioClip[] RivalLostPhoneClips;

	// Token: 0x040034DE RID: 13534
	public AudioClip[] PickpocketReactionClips;

	// Token: 0x040034DF RID: 13535
	public AudioClip[] RivalPickpocketReactionClips;

	// Token: 0x040034E0 RID: 13536
	public AudioClip[] SplashReactionClips;

	// Token: 0x040034E1 RID: 13537
	public AudioClip[] SplashReactionMaleClips;

	// Token: 0x040034E2 RID: 13538
	public AudioClip[] RivalSplashReactionClips;

	// Token: 0x040034E3 RID: 13539
	public AudioClip[] DrownReactionClips;

	// Token: 0x040034E4 RID: 13540
	public AudioClip[] LightSwitchClips;

	// Token: 0x040034E5 RID: 13541
	public AudioClip[] Task6Clips;

	// Token: 0x040034E6 RID: 13542
	public AudioClip[] Task7Clips;

	// Token: 0x040034E7 RID: 13543
	public AudioClip[] Task8Clips;

	// Token: 0x040034E8 RID: 13544
	public AudioClip[] Task11Clips;

	// Token: 0x040034E9 RID: 13545
	public AudioClip[] Task13Clips;

	// Token: 0x040034EA RID: 13546
	public AudioClip[] Task14Clips;

	// Token: 0x040034EB RID: 13547
	public AudioClip[] Task15Clips;

	// Token: 0x040034EC RID: 13548
	public AudioClip[] Task25Clips;

	// Token: 0x040034ED RID: 13549
	public AudioClip[] Task28Clips;

	// Token: 0x040034EE RID: 13550
	public AudioClip[] Task30Clips;

	// Token: 0x040034EF RID: 13551
	public AudioClip[] Task32Clips;

	// Token: 0x040034F0 RID: 13552
	public AudioClip[] Task33Clips;

	// Token: 0x040034F1 RID: 13553
	public AudioClip[] Task34Clips;

	// Token: 0x040034F2 RID: 13554
	public AudioClip[] Task36Clips;

	// Token: 0x040034F3 RID: 13555
	public AudioClip[] Task37Clips;

	// Token: 0x040034F4 RID: 13556
	public AudioClip[] Task38Clips;

	// Token: 0x040034F5 RID: 13557
	public AudioClip[] Task52Clips;

	// Token: 0x040034F6 RID: 13558
	public AudioClip[] Task76Clips;

	// Token: 0x040034F7 RID: 13559
	public AudioClip[] Task77Clips;

	// Token: 0x040034F8 RID: 13560
	public AudioClip[] Task78Clips;

	// Token: 0x040034F9 RID: 13561
	public AudioClip[] Task79Clips;

	// Token: 0x040034FA RID: 13562
	public AudioClip[] Task80Clips;

	// Token: 0x040034FB RID: 13563
	public AudioClip[] Task81Clips;

	// Token: 0x040034FC RID: 13564
	public AudioClip[] TaskGenericMaleClips;

	// Token: 0x040034FD RID: 13565
	public AudioClip[] TaskGenericFemaleClips;

	// Token: 0x040034FE RID: 13566
	public AudioClip[] TaskInquiryClips;

	// Token: 0x040034FF RID: 13567
	public AudioClip[] Club0Clips;

	// Token: 0x04003500 RID: 13568
	public AudioClip[] Club1Clips;

	// Token: 0x04003501 RID: 13569
	public AudioClip[] Club2Clips;

	// Token: 0x04003502 RID: 13570
	public AudioClip[] Club3Clips;

	// Token: 0x04003503 RID: 13571
	public AudioClip[] Club4Clips;

	// Token: 0x04003504 RID: 13572
	public AudioClip[] Club5Clips;

	// Token: 0x04003505 RID: 13573
	public AudioClip[] Club6Clips;

	// Token: 0x04003506 RID: 13574
	public AudioClip[] Club7ClipsLight;

	// Token: 0x04003507 RID: 13575
	public AudioClip[] Club7ClipsDark;

	// Token: 0x04003508 RID: 13576
	public AudioClip[] Club8Clips;

	// Token: 0x04003509 RID: 13577
	public AudioClip[] Club9Clips;

	// Token: 0x0400350A RID: 13578
	public AudioClip[] Club10Clips;

	// Token: 0x0400350B RID: 13579
	public AudioClip[] Club11Clips;

	// Token: 0x0400350C RID: 13580
	public AudioClip[] Club12Clips;

	// Token: 0x0400350D RID: 13581
	public AudioClip[] SubClub3Clips;

	// Token: 0x0400350E RID: 13582
	public AudioClip[] ClubGreetingClips;

	// Token: 0x0400350F RID: 13583
	public AudioClip[] ClubUnwelcomeClips;

	// Token: 0x04003510 RID: 13584
	public AudioClip[] ClubKickClips;

	// Token: 0x04003511 RID: 13585
	public AudioClip[] ClubJoinClips;

	// Token: 0x04003512 RID: 13586
	public AudioClip[] ClubAcceptClips;

	// Token: 0x04003513 RID: 13587
	public AudioClip[] ClubRefuseClips;

	// Token: 0x04003514 RID: 13588
	public AudioClip[] ClubRejoinClips;

	// Token: 0x04003515 RID: 13589
	public AudioClip[] ClubExclusiveClips;

	// Token: 0x04003516 RID: 13590
	public AudioClip[] ClubGrudgeClips;

	// Token: 0x04003517 RID: 13591
	public AudioClip[] ClubQuitClips;

	// Token: 0x04003518 RID: 13592
	public AudioClip[] ClubConfirmClips;

	// Token: 0x04003519 RID: 13593
	public AudioClip[] ClubDenyClips;

	// Token: 0x0400351A RID: 13594
	public AudioClip[] ClubFarewellClips;

	// Token: 0x0400351B RID: 13595
	public AudioClip[] ClubActivityClips;

	// Token: 0x0400351C RID: 13596
	public AudioClip[] ClubEarlyClips;

	// Token: 0x0400351D RID: 13597
	public AudioClip[] ClubLateClips;

	// Token: 0x0400351E RID: 13598
	public AudioClip[] ClubYesClips;

	// Token: 0x0400351F RID: 13599
	public AudioClip[] ClubNoClips;

	// Token: 0x04003520 RID: 13600
	public AudioClip[] ClubPracticeClips;

	// Token: 0x04003521 RID: 13601
	public AudioClip[] ClubPracticeYesClips;

	// Token: 0x04003522 RID: 13602
	public AudioClip[] ClubPracticeNoClips;

	// Token: 0x04003523 RID: 13603
	public AudioClip[] EavesdropClips;

	// Token: 0x04003524 RID: 13604
	public AudioClip[] FoodRejectionClips;

	// Token: 0x04003525 RID: 13605
	public AudioClip[] ViolenceClips;

	// Token: 0x04003526 RID: 13606
	public AudioClip[] EventEavesdropClips;

	// Token: 0x04003527 RID: 13607
	public AudioClip[] RivalEavesdropClips;

	// Token: 0x04003528 RID: 13608
	public AudioClip[] DelinquentAnnoyClips;

	// Token: 0x04003529 RID: 13609
	public AudioClip[] DelinquentCaseClips;

	// Token: 0x0400352A RID: 13610
	public AudioClip[] DelinquentShoveClips;

	// Token: 0x0400352B RID: 13611
	public AudioClip[] DelinquentReactionClips;

	// Token: 0x0400352C RID: 13612
	public AudioClip[] DelinquentWeaponReactionClips;

	// Token: 0x0400352D RID: 13613
	public AudioClip[] DelinquentThreatenedClips;

	// Token: 0x0400352E RID: 13614
	public AudioClip[] DelinquentTauntClips;

	// Token: 0x0400352F RID: 13615
	public AudioClip[] DelinquentCalmClips;

	// Token: 0x04003530 RID: 13616
	public AudioClip[] DelinquentFightClips;

	// Token: 0x04003531 RID: 13617
	public AudioClip[] DelinquentAvengeClips;

	// Token: 0x04003532 RID: 13618
	public AudioClip[] DelinquentWinClips;

	// Token: 0x04003533 RID: 13619
	public AudioClip[] DelinquentSurrenderClips;

	// Token: 0x04003534 RID: 13620
	public AudioClip[] DelinquentNoSurrenderClips;

	// Token: 0x04003535 RID: 13621
	public AudioClip[] DelinquentMurderReactionClips;

	// Token: 0x04003536 RID: 13622
	public AudioClip[] DelinquentCorpseReactionClips;

	// Token: 0x04003537 RID: 13623
	public AudioClip[] DelinquentFriendCorpseReactionClips;

	// Token: 0x04003538 RID: 13624
	public AudioClip[] DelinquentResumeClips;

	// Token: 0x04003539 RID: 13625
	public AudioClip[] DelinquentFleeClips;

	// Token: 0x0400353A RID: 13626
	public AudioClip[] DelinquentEnemyFleeClips;

	// Token: 0x0400353B RID: 13627
	public AudioClip[] DelinquentFriendFleeClips;

	// Token: 0x0400353C RID: 13628
	public AudioClip[] DelinquentInjuredFleeClips;

	// Token: 0x0400353D RID: 13629
	public AudioClip[] DelinquentCheerClips;

	// Token: 0x0400353E RID: 13630
	public AudioClip[] DelinquentHmmClips;

	// Token: 0x0400353F RID: 13631
	public AudioClip[] DelinquentGrudgeClips;

	// Token: 0x04003540 RID: 13632
	public AudioClip[] DismissiveClips;

	// Token: 0x04003541 RID: 13633
	public AudioClip[] EvilDelinquentCorpseReactionClips;

	// Token: 0x04003542 RID: 13634
	public AudioClip[] EulogyClips;

	// Token: 0x04003543 RID: 13635
	public AudioClip[] ObstacleMurderReactionClips;

	// Token: 0x04003544 RID: 13636
	public AudioClip[] ObstaclePoisonReactionClips;

	// Token: 0x04003545 RID: 13637
	private SubtitleTypeAndAudioClipArrayDictionary SubtitleClipArrays;

	// Token: 0x04003546 RID: 13638
	public GameObject CurrentClip;

	// Token: 0x04003547 RID: 13639
	public StudentScript Speaker;
}
