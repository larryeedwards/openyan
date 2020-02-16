using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class DemonScript : MonoBehaviour
{
	// Token: 0x060018C3 RID: 6339 RVA: 0x000DF750 File Offset: 0x000DDB50
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			this.Communing = true;
		}
		if (this.DemonID == 1)
		{
			if ((double)Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 2.5)
			{
				if (!this.Open)
				{
					AudioSource.PlayClipAtPoint(this.MouthOpen, base.transform.position);
				}
				this.Open = true;
			}
			else
			{
				if (this.Open)
				{
					AudioSource.PlayClipAtPoint(this.MouthClose, base.transform.position);
				}
				this.Open = false;
			}
			if (this.Open)
			{
				this.Value = Mathf.Lerp(this.Value, 100f, Time.deltaTime * 10f);
			}
			else
			{
				this.Value = Mathf.Lerp(this.Value, 0f, Time.deltaTime * 10f);
			}
			this.Face.SetBlendShapeWeight(0, this.Value);
		}
		if (this.Communing)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.Phase == 1)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
				if (this.Darkness.color.a == 1f)
				{
					this.DemonSubtitle.transform.localPosition = Vector3.zero;
					this.DemonSubtitle.text = this.Lines[this.ID];
					this.DemonSubtitle.color = this.MyColor;
					this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, 0f);
					this.Phase++;
					if (this.Clips[this.ID] != null)
					{
						component.clip = this.Clips[this.ID];
						component.Play();
					}
				}
			}
			else if (this.Phase == 2)
			{
				Debug.Log("Demon Phase is 2.");
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-this.Intensity, this.Intensity), UnityEngine.Random.Range(-this.Intensity, this.Intensity), UnityEngine.Random.Range(-this.Intensity, this.Intensity));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 1f, Time.deltaTime));
				this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, Mathf.MoveTowards(this.Button.color.a, 1f, Time.deltaTime));
				if (this.DemonSubtitle.color.a > 0.9f && Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.DemonSubtitle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-this.Intensity, this.Intensity), UnityEngine.Random.Range(-this.Intensity, this.Intensity), UnityEngine.Random.Range(-this.Intensity, this.Intensity));
				this.DemonSubtitle.color = new Color(this.DemonSubtitle.color.r, this.DemonSubtitle.color.g, this.DemonSubtitle.color.b, Mathf.MoveTowards(this.DemonSubtitle.color.a, 0f, Time.deltaTime));
				if (this.DemonSubtitle.color.a == 0f)
				{
					this.ID++;
					if (this.ID < this.Lines.Length)
					{
						this.Phase--;
						this.DemonSubtitle.text = this.Lines[this.ID];
						if (this.Clips[this.ID] != null)
						{
							component.clip = this.Clips[this.ID];
							component.Play();
						}
					}
					else
					{
						this.Phase++;
					}
				}
			}
			else
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, Mathf.MoveTowards(this.Button.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					this.Yandere.CanMove = true;
					this.Communing = false;
					this.Phase = 1;
					this.ID = 0;
					SchoolGlobals.SetDemonActive(this.DemonID, true);
					StudentGlobals.FemaleUniform = 1;
					StudentGlobals.MaleUniform = 1;
					GameGlobals.Paranormal = true;
				}
			}
		}
	}

	// Token: 0x04001C42 RID: 7234
	public SkinnedMeshRenderer Face;

	// Token: 0x04001C43 RID: 7235
	public YandereScript Yandere;

	// Token: 0x04001C44 RID: 7236
	public PromptScript Prompt;

	// Token: 0x04001C45 RID: 7237
	public UILabel DemonSubtitle;

	// Token: 0x04001C46 RID: 7238
	public UISprite Darkness;

	// Token: 0x04001C47 RID: 7239
	public UISprite Button;

	// Token: 0x04001C48 RID: 7240
	public AudioClip MouthOpen;

	// Token: 0x04001C49 RID: 7241
	public AudioClip MouthClose;

	// Token: 0x04001C4A RID: 7242
	public AudioClip[] Clips;

	// Token: 0x04001C4B RID: 7243
	public string[] Lines;

	// Token: 0x04001C4C RID: 7244
	public bool Communing;

	// Token: 0x04001C4D RID: 7245
	public bool Open;

	// Token: 0x04001C4E RID: 7246
	public float Intensity = 1f;

	// Token: 0x04001C4F RID: 7247
	public float Value;

	// Token: 0x04001C50 RID: 7248
	public Color MyColor;

	// Token: 0x04001C51 RID: 7249
	public int DemonID;

	// Token: 0x04001C52 RID: 7250
	public int Phase = 1;

	// Token: 0x04001C53 RID: 7251
	public int ID;
}
