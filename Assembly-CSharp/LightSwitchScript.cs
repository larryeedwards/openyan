﻿using System;
using UnityEngine;

// Token: 0x0200044E RID: 1102
public class LightSwitchScript : MonoBehaviour
{
	// Token: 0x06001D6E RID: 7534 RVA: 0x0011400E File Offset: 0x0011240E
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x00114028 File Offset: 0x00112428
	private void Update()
	{
		if (this.Flicker)
		{
			this.FlickerTimer += Time.deltaTime;
			if (this.FlickerTimer > 0.1f)
			{
				this.FlickerTimer = 0f;
				this.BathroomLight.SetActive(!this.BathroomLight.activeInHierarchy);
			}
		}
		if (!this.Panel.useGravity)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.HideButton[3] = (this.Yandere.EquippedWeapon.WeaponID != 6);
			}
			else
			{
				this.Prompt.HideButton[3] = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.BathroomLight.activeInHierarchy)
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.BathroomLight.SetActive(false);
				component.clip = this.Flick[1];
				component.Play();
				if (this.ToiletEvent.EventActive && (this.ToiletEvent.EventPhase == 2 || this.ToiletEvent.EventPhase == 3))
				{
					this.ReactionID = UnityEngine.Random.Range(1, 4);
					AudioClipPlayer.Play(this.ReactionClips[this.ReactionID], this.ToiletEvent.EventStudent.transform.position, 5f, 10f, out this.ToiletEvent.VoiceClip);
					this.ToiletEvent.EventSubtitle.text = this.ReactionTexts[this.ReactionID];
					this.SubtitleTimer += Time.deltaTime;
				}
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.BathroomLight.SetActive(true);
				component.clip = this.Flick[0];
				component.Play();
			}
		}
		if (this.SubtitleTimer > 0f)
		{
			this.SubtitleTimer += Time.deltaTime;
			if (this.SubtitleTimer > 3f)
			{
				this.ToiletEvent.EventSubtitle.text = string.Empty;
				this.SubtitleTimer = 0f;
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.HideButton[3] = true;
			this.Wires.localScale = new Vector3(this.Wires.localScale.x, this.Wires.localScale.y, 1f);
			this.Panel.useGravity = true;
			this.Panel.AddForce(0f, 0f, 10f);
		}
	}

	// Token: 0x04002489 RID: 9353
	public ToiletEventScript ToiletEvent;

	// Token: 0x0400248A RID: 9354
	public YandereScript Yandere;

	// Token: 0x0400248B RID: 9355
	public PromptScript Prompt;

	// Token: 0x0400248C RID: 9356
	public Transform ElectrocutionSpot;

	// Token: 0x0400248D RID: 9357
	public GameObject BathroomLight;

	// Token: 0x0400248E RID: 9358
	public GameObject Electricity;

	// Token: 0x0400248F RID: 9359
	public Rigidbody Panel;

	// Token: 0x04002490 RID: 9360
	public Transform Wires;

	// Token: 0x04002491 RID: 9361
	public AudioClip[] ReactionClips;

	// Token: 0x04002492 RID: 9362
	public string[] ReactionTexts;

	// Token: 0x04002493 RID: 9363
	public AudioClip[] Flick;

	// Token: 0x04002494 RID: 9364
	public float SubtitleTimer;

	// Token: 0x04002495 RID: 9365
	public float FlickerTimer;

	// Token: 0x04002496 RID: 9366
	public int ReactionID;

	// Token: 0x04002497 RID: 9367
	public bool Flicker;
}
