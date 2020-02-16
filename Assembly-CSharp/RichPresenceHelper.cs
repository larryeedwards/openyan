using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004C0 RID: 1216
public class RichPresenceHelper : MonoBehaviour
{
	// Token: 0x06001F21 RID: 7969 RVA: 0x0013E08C File Offset: 0x0013C48C
	private void Start()
	{
		this.CompileDictionaries();
		this._discordController = base.GetComponent<DiscordController>();
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this._discordController.enabled = false;
		this._discordController.presence.state = this.GetSceneDescription();
		this._discordController.enabled = true;
		DiscordRpc.UpdatePresence(ref this._discordController.presence);
		base.InvokeRepeating("UpdatePresence", 0f, 10f);
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x0013E109 File Offset: 0x0013C509
	private void OnLevelWasLoaded(int level)
	{
		if (level == 11)
		{
			this._clockScript = UnityEngine.Object.FindObjectOfType<ClockScript>();
		}
		this.UpdatePresence();
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x0013E124 File Offset: 0x0013C524
	private void UpdatePresence()
	{
		this._discordController.presence.state = this.GetSceneDescription();
		DiscordRpc.UpdatePresence(ref this._discordController.presence);
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x0013E14C File Offset: 0x0013C54C
	private void CompileDictionaries()
	{
		this._weekdays.Add(1, "Monday");
		this._weekdays.Add(2, "Tuesday");
		this._weekdays.Add(3, "Wednesday");
		this._weekdays.Add(4, "Thursday");
		this._weekdays.Add(5, "Friday");
		this._periods.Add(1, "Before Class");
		this._periods.Add(2, "Class Time");
		this._periods.Add(3, "Lunch Time");
		this._periods.Add(4, "Class Time");
		this._periods.Add(5, "Cleaning Time");
		this._periods.Add(6, "After School");
		this._sceneDescriptions.Add("WelcomeScene", "Launching the game!");
		this._sceneDescriptions.Add("SponsorScene", "Checking out the sponsors!");
		this._sceneDescriptions.Add("TitleScene", "At the title screen!");
		this._sceneDescriptions.Add("SenpaiScene", "Customizing Senpai!");
		this._sceneDescriptions.Add("IntroScene", "Watching the Intro!");
		this._sceneDescriptions.Add("NewIntroScene", "Watching the Intro!");
		this._sceneDescriptions.Add("PhoneScene", "Texting with Info-Chan!");
		this._sceneDescriptions.Add("CalendarScene", "Checking out the Calendar!");
		this._sceneDescriptions.Add("HomeScene", "Chilling at home!");
		this._sceneDescriptions.Add("LoadingScene", "Now Loading!");
		this._sceneDescriptions.Add("SchoolScene", "At School");
		this._sceneDescriptions.Add("YanvaniaTitleScene", "Beginning Yanvania: Senpai of the Night!");
		this._sceneDescriptions.Add("YanvaniaScene", "Playing Yanvania: Senpai of the Night!");
		this._sceneDescriptions.Add("LivingRoomScene", "Chatting with Kokona!");
		this._sceneDescriptions.Add("MissionModeScene", "Accepting a mission!");
		this._sceneDescriptions.Add("VeryFunScene", "??????????");
		this._sceneDescriptions.Add("CreditsScene", "Viewing the credits!");
		this._sceneDescriptions.Add("MiyukiTitleScene", "Beginning Magical Girl Pretty Miyuki!");
		this._sceneDescriptions.Add("MiyukiGameplayScene", "Playing Magical Girl Pretty Miyuki!");
		this._sceneDescriptions.Add("MiyukiThanksScene", "Finishing Magical Girl Pretty Miyuki!");
		this._sceneDescriptions.Add("RhythmMinigameScene", "Jamming out with the Light Music Club!");
		this._sceneDescriptions.Add("LifeNoteScene", "Watching an episode of Life Note!");
		this._sceneDescriptions.Add("YancordScene", "Chatting over Yancord!");
		this._sceneDescriptions.Add("MaidMenuScene", "Getting ready to be cute at a maid cafe!");
		this._sceneDescriptions.Add("MaidGameScene", "Being a cute maid! MOE MOE KYUN!");
		this._sceneDescriptions.Add("StreetScene", "Chilling in town!");
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x0013E438 File Offset: 0x0013C838
	private string GetSceneDescription()
	{
		string name = SceneManager.GetActiveScene().name;
		if (name != null)
		{
			if (name == "SchoolScene")
			{
				string text = (!MissionModeGlobals.MissionMode) ? string.Empty : ", Mission Mode";
				return string.Format("{0}, {1}, {2}, {3}{4}", new object[]
				{
					this._sceneDescriptions["SchoolScene"],
					this._clockScript.TimeLabel.text,
					this._periods[this._clockScript.Period],
					this._weekdays[this._clockScript.Weekday],
					text
				});
			}
		}
		if (this._sceneDescriptions.ContainsKey(name))
		{
			return this._sceneDescriptions[name];
		}
		return "No description available yet.";
	}

	// Token: 0x040029A3 RID: 10659
	private DiscordController _discordController;

	// Token: 0x040029A4 RID: 10660
	private ClockScript _clockScript;

	// Token: 0x040029A5 RID: 10661
	private Dictionary<int, string> _weekdays = new Dictionary<int, string>();

	// Token: 0x040029A6 RID: 10662
	private Dictionary<int, string> _periods = new Dictionary<int, string>();

	// Token: 0x040029A7 RID: 10663
	private Dictionary<string, string> _sceneDescriptions = new Dictionary<string, string>();
}
