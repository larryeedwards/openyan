using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200048B RID: 1163
public class PhoneScript : MonoBehaviour
{
	// Token: 0x06001E3A RID: 7738 RVA: 0x00125C50 File Offset: 0x00124050
	private void Start()
	{
		this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, -135f, this.Buttons.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		if (DateGlobals.Week > 1 && DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
		}
		if (EventGlobals.KidnapConversation)
		{
			this.VoiceClips = this.KidnapClip;
			this.Speaker = this.KidnapSpeaker;
			this.Text = this.KidnapText;
			this.Height = this.KidnapHeight;
			EventGlobals.BefriendConversation = true;
			EventGlobals.KidnapConversation = false;
		}
		else if (EventGlobals.BefriendConversation)
		{
			this.VoiceClips = this.BefriendClip;
			this.Speaker = this.BefriendSpeaker;
			this.Text = this.BefriendText;
			this.Height = this.BefriendHeight;
			EventGlobals.LivingRoom = true;
			EventGlobals.BefriendConversation = false;
		}
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = Color.black;
			this.LoveSickColorSwap();
		}
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x00125DD0 File Offset: 0x001241D0
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (this.Timer > 0f && this.Buttons.gameObject.activeInHierarchy)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					if (!this.Jukebox.isPlaying)
					{
						this.Jukebox.Play();
					}
					if (this.NewMessage == null)
					{
						this.SpawnMessage();
					}
				}
			}
			if (this.NewMessage != null)
			{
				this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, 0f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
				this.AutoTimer += Time.deltaTime;
				if ((this.Auto && this.AutoTimer > this.VoiceClips[this.ID].length + 1f) || Input.GetButtonDown("A"))
				{
					this.AutoTimer = 0f;
					if (this.ID < this.Text.Length - 1)
					{
						this.ID++;
						this.SpawnMessage();
					}
					else
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						this.FadeOut = true;
						if (!this.Buttons.gameObject.activeInHierarchy)
						{
							this.Darkness.color = new Color(0f, 0f, 0f, 1f);
						}
					}
				}
				if (Input.GetButtonDown("X"))
				{
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, -135f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
			base.GetComponent<AudioSource>().volume = 1f - this.Darkness.color.a;
			this.Jukebox.volume = 1f - this.Darkness.color.a;
			if (this.Darkness.color.a >= 1f)
			{
				if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					SceneManager.LoadScene("OsanaWarningScene");
				}
				else if (!EventGlobals.BefriendConversation && !EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("CalendarScene");
				}
				else if (EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("LivingRoomScene");
				}
				else
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
		}
		this.Timer += Time.deltaTime;
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x001261EC File Offset: 0x001245EC
	private void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			this.NewMessage.transform.parent = this.OldMessages;
			this.OldMessages.localPosition = new Vector3(this.OldMessages.localPosition.x, this.OldMessages.localPosition.y + (72f + (float)this.Height[this.ID] * 32f), this.OldMessages.localPosition.z);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.VoiceClips[this.ID];
		component.Play();
		if (this.Speaker[this.ID] == 1)
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.LeftMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(-225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
		}
		else
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.RightMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
			if (this.Speaker == this.KidnapSpeaker && this.Height[this.ID] == 8)
			{
				this.NewMessage.GetComponent<TextMessageScript>().Attachment = true;
			}
		}
		if (this.Height[this.ID] == 9)
		{
			this.Buttons.gameObject.SetActive(false);
			this.Darkness.enabled = true;
			this.Jukebox.Stop();
			this.Timer = -99999f;
		}
		this.AutoLimit = (float)(this.Height[this.ID] + 1);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.Text[this.ID];
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x0012644C File Offset: 0x0012484C
	private void LoveSickColorSwap()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in array)
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UILabel component2 = gameObject.GetComponent<UILabel>();
			if (component2 != null && component2.color != Color.black)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			this.Darkness.color = Color.black;
		}
	}

	// Token: 0x040026D2 RID: 9938
	public GameObject[] RightMessage;

	// Token: 0x040026D3 RID: 9939
	public GameObject[] LeftMessage;

	// Token: 0x040026D4 RID: 9940
	public AudioClip[] VoiceClips;

	// Token: 0x040026D5 RID: 9941
	public GameObject NewMessage;

	// Token: 0x040026D6 RID: 9942
	public AudioSource Jukebox;

	// Token: 0x040026D7 RID: 9943
	public Transform OldMessages;

	// Token: 0x040026D8 RID: 9944
	public Transform Buttons;

	// Token: 0x040026D9 RID: 9945
	public Transform Panel;

	// Token: 0x040026DA RID: 9946
	public Vignetting Vignette;

	// Token: 0x040026DB RID: 9947
	public UISprite Darkness;

	// Token: 0x040026DC RID: 9948
	public UISprite Sprite;

	// Token: 0x040026DD RID: 9949
	public int[] Speaker;

	// Token: 0x040026DE RID: 9950
	public string[] Text;

	// Token: 0x040026DF RID: 9951
	public int[] Height;

	// Token: 0x040026E0 RID: 9952
	public AudioClip[] KidnapClip;

	// Token: 0x040026E1 RID: 9953
	public int[] KidnapSpeaker;

	// Token: 0x040026E2 RID: 9954
	public string[] KidnapText;

	// Token: 0x040026E3 RID: 9955
	public int[] KidnapHeight;

	// Token: 0x040026E4 RID: 9956
	public AudioClip[] BefriendClip;

	// Token: 0x040026E5 RID: 9957
	public int[] BefriendSpeaker;

	// Token: 0x040026E6 RID: 9958
	public string[] BefriendText;

	// Token: 0x040026E7 RID: 9959
	public int[] BefriendHeight;

	// Token: 0x040026E8 RID: 9960
	public bool FadeOut;

	// Token: 0x040026E9 RID: 9961
	public bool Auto;

	// Token: 0x040026EA RID: 9962
	public float AutoLimit;

	// Token: 0x040026EB RID: 9963
	public float AutoTimer;

	// Token: 0x040026EC RID: 9964
	public float Timer;

	// Token: 0x040026ED RID: 9965
	public int ID;
}
