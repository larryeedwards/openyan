using System;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class AudioSoftwareScript : MonoBehaviour
{
	// Token: 0x0600173C RID: 5948 RVA: 0x000B6D0A File Offset: 0x000B510A
	private void Start()
	{
		this.Screen.SetActive(false);
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000B6D18 File Offset: 0x000B5118
	private void Update()
	{
		if (this.ConversationRecorded && this.Yandere.Inventory.RivalPhone)
		{
			if (!this.Prompt.enabled)
			{
				this.Prompt.enabled = true;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.enabled = false;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_playingGames_00");
			this.Yandere.MyController.radius = 0.1f;
			this.Yandere.CanMove = false;
			base.GetComponent<AudioSource>().Play();
			this.ChairCollider.enabled = false;
			this.Screen.SetActive(true);
			this.Editing = true;
		}
		if (this.Editing)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Screen.transform.position.x, this.Yandere.transform.position.y, this.Screen.transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(this.SitSpot.position);
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.EventSubtitle.text = "Okay, how 'bout that boy from class 3-2? What do you think of him?";
			}
			if (this.Timer > 7f)
			{
				this.EventSubtitle.text = "He's just my childhood friend.";
			}
			if (this.Timer > 9f)
			{
				this.EventSubtitle.text = "Is he your boyfriend?";
			}
			if (this.Timer > 11f)
			{
				this.EventSubtitle.text = "What? HIM? Ugh, no way! That guy's a total creep! I wouldn't date him if he was the last man alive on earth! He can go jump off a cliff for all I care!";
			}
			if (this.Timer > 22f)
			{
				this.Yandere.MyController.radius = 0.2f;
				this.Yandere.CanMove = true;
				this.ChairCollider.enabled = false;
				this.EventSubtitle.text = string.Empty;
				this.Screen.SetActive(false);
				this.AudioDoctored = true;
				this.Editing = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
	}

	// Token: 0x0400169C RID: 5788
	public YandereScript Yandere;

	// Token: 0x0400169D RID: 5789
	public PromptScript Prompt;

	// Token: 0x0400169E RID: 5790
	public Quaternion targetRotation;

	// Token: 0x0400169F RID: 5791
	public Collider ChairCollider;

	// Token: 0x040016A0 RID: 5792
	public UILabel EventSubtitle;

	// Token: 0x040016A1 RID: 5793
	public GameObject Screen;

	// Token: 0x040016A2 RID: 5794
	public Transform SitSpot;

	// Token: 0x040016A3 RID: 5795
	public bool ConversationRecorded;

	// Token: 0x040016A4 RID: 5796
	public bool AudioDoctored;

	// Token: 0x040016A5 RID: 5797
	public bool Editing;

	// Token: 0x040016A6 RID: 5798
	public float Timer;
}
