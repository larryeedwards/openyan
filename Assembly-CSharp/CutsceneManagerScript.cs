using System;
using UnityEngine;

// Token: 0x02000383 RID: 899
public class CutsceneManagerScript : MonoBehaviour
{
	// Token: 0x06001880 RID: 6272 RVA: 0x000D7C8C File Offset: 0x000D608C
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				if (this.Scheme == 5)
				{
					this.Phase++;
				}
				else
				{
					this.Phase = 4;
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.Subtitle.text = this.Text[this.Line];
			component.clip = this.Voice[this.Line];
			component.Play();
			this.Phase++;
		}
		else if (this.Phase == 3)
		{
			if (!component.isPlaying || Input.GetButtonDown("A"))
			{
				if (this.Line < 2)
				{
					this.Phase--;
					this.Line++;
				}
				else
				{
					this.Subtitle.text = string.Empty;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 4)
		{
			Debug.Log("We're activating EndOfDay from CutsceneManager.");
			this.EndOfDay.gameObject.SetActive(true);
			this.EndOfDay.Phase = 14;
			if (this.Scheme == 5)
			{
				this.Counselor.LecturePhase = 5;
			}
			else
			{
				this.Counselor.LecturePhase = 1;
			}
			this.Phase++;
		}
		else if (this.Phase == 6)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 7)
		{
			if (this.Scheme != 5 || this.StudentManager.Students[this.StudentManager.RivalID] != null)
			{
			}
			this.PromptBar.ClearButtons();
			this.PromptBar.Show = false;
			this.Portal.Proceed = true;
			base.gameObject.SetActive(false);
			this.Scheme = 0;
		}
	}

	// Token: 0x04001B19 RID: 6937
	public StudentManagerScript StudentManager;

	// Token: 0x04001B1A RID: 6938
	public CounselorScript Counselor;

	// Token: 0x04001B1B RID: 6939
	public PromptBarScript PromptBar;

	// Token: 0x04001B1C RID: 6940
	public EndOfDayScript EndOfDay;

	// Token: 0x04001B1D RID: 6941
	public PortalScript Portal;

	// Token: 0x04001B1E RID: 6942
	public UISprite Darkness;

	// Token: 0x04001B1F RID: 6943
	public UILabel Subtitle;

	// Token: 0x04001B20 RID: 6944
	public AudioClip[] Voice;

	// Token: 0x04001B21 RID: 6945
	public string[] Text;

	// Token: 0x04001B22 RID: 6946
	public int Scheme;

	// Token: 0x04001B23 RID: 6947
	public int Phase = 1;

	// Token: 0x04001B24 RID: 6948
	public int Line = 1;
}
