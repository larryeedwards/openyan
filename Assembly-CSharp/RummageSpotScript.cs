using System;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class RummageSpotScript : MonoBehaviour
{
	// Token: 0x06001F52 RID: 8018 RVA: 0x00140224 File Offset: 0x0013E624
	private void Start()
	{
		if (this.ID == 1)
		{
			if (GameGlobals.AnswerSheetUnavailable)
			{
				Debug.Log("The answer sheet is no longer available, due to events on a previous day.");
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Friday && this.Clock.HourTime > 13.5f)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x001402BC File Offset: 0x0013E6BC
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.EmptyHands();
				this.Yandere.CharacterAnimation.CrossFade("f02_rummage_00");
				this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(true);
				this.Yandere.RummageSpot = this;
				this.Yandere.Rummaging = true;
				this.Yandere.CanMove = false;
				component.Play();
			}
		}
		if (this.Yandere.Rummaging)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position, Quaternion.identity);
			gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
			gameObject.transform.localScale = new Vector3(750f, gameObject.transform.localScale.y, 750f);
		}
		if (this.Yandere.Noticed)
		{
			component.Stop();
		}
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x00140408 File Offset: 0x0013E808
	public void GetReward()
	{
		if (this.ID == 1)
		{
			if (this.Phase == 1)
			{
				SchemeGlobals.SetSchemeStage(5, 5);
				this.Schemes.UpdateInstructions();
				this.Yandere.Inventory.AnswerSheet = true;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.DoorGap.Prompt.enabled = true;
				this.Phase++;
			}
			else if (this.Phase == 2)
			{
				SchemeGlobals.SetSchemeStage(5, 8);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.AnswerSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.gameObject.SetActive(false);
				this.Phase++;
			}
		}
	}

	// Token: 0x04002A72 RID: 10866
	public GameObject AlarmDisc;

	// Token: 0x04002A73 RID: 10867
	public DoorGapScript DoorGap;

	// Token: 0x04002A74 RID: 10868
	public SchemesScript Schemes;

	// Token: 0x04002A75 RID: 10869
	public YandereScript Yandere;

	// Token: 0x04002A76 RID: 10870
	public PromptScript Prompt;

	// Token: 0x04002A77 RID: 10871
	public ClockScript Clock;

	// Token: 0x04002A78 RID: 10872
	public Transform Target;

	// Token: 0x04002A79 RID: 10873
	public int Phase;

	// Token: 0x04002A7A RID: 10874
	public int ID;
}
