using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000381 RID: 897
public class CustomizationScript : MonoBehaviour
{
	// Token: 0x0600185F RID: 6239 RVA: 0x000D56B0 File Offset: 0x000D3AB0
	private void Awake()
	{
		this.Data = new CustomizationScript.CustomizationData();
		this.Data.skinColor = new global::RangeInt(3, this.MinSkinColor, this.MaxSkinColor);
		this.Data.hairstyle = new global::RangeInt(1, this.MinHairstyle, this.MaxHairstyle);
		this.Data.hairColor = new global::RangeInt(1, this.MinHairColor, this.MaxHairColor);
		this.Data.eyeColor = new global::RangeInt(1, this.MinEyeColor, this.MaxEyeColor);
		this.Data.eyewear = new global::RangeInt(0, this.MinEyewear, this.MaxEyewear);
		this.Data.facialHair = new global::RangeInt(0, this.MinFacialHair, this.MaxFacialHair);
		this.Data.maleUniform = new global::RangeInt(1, this.MinMaleUniform, this.MaxMaleUniform);
		this.Data.femaleUniform = new global::RangeInt(1, this.MinFemaleUniform, this.MaxFemaleUniform);
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000D57B0 File Offset: 0x000D3BB0
	private void Start()
	{
		Time.timeScale = 1f;
		this.LoveSick = GameGlobals.LoveSick;
		this.ApologyWindow.localPosition = new Vector3(1360f, this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
		this.CustomizePanel.alpha = 0f;
		this.UniformPanel.alpha = 0f;
		this.FinishPanel.alpha = 0f;
		this.GenderPanel.alpha = 0f;
		this.WhitePanel.alpha = 1f;
		this.UpdateFacialHair(this.Data.facialHair.Value);
		this.UpdateHairStyle(this.Data.hairstyle.Value);
		this.UpdateEyes(this.Data.eyeColor.Value);
		this.UpdateSkin(this.Data.skinColor.Value);
		if (this.LoveSick)
		{
			this.LoveSickColorSwap();
			this.WhitePanel.alpha = 0f;
			this.Data.femaleUniform.Value = 5;
			this.Data.maleUniform.Value = 5;
			RenderSettings.fogColor = new Color(0f, 0f, 0f, 1f);
			this.LoveSickCamera.SetActive(true);
			this.Black.color = Color.black;
			this.MyAudio.loop = false;
			this.MyAudio.clip = this.LoveSickIntro;
			this.MyAudio.Play();
		}
		else
		{
			this.Data.femaleUniform.Value = this.MinFemaleUniform;
			this.Data.maleUniform.Value = this.MinMaleUniform;
			RenderSettings.fogColor = new Color(1f, 0.5f, 1f, 1f);
			this.Black.color = new Color(0f, 0f, 0f, 0f);
			this.LoveSickCamera.SetActive(false);
		}
		this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
		this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
		this.Senpai.position = new Vector3(0f, -1f, 2f);
		this.Senpai.gameObject.SetActive(true);
		this.Senpai.GetComponent<Animation>().Play("newWalk_00");
		this.Yandere.position = new Vector3(1f, -1f, 4.5f);
		this.Yandere.gameObject.SetActive(true);
		this.Yandere.GetComponent<Animation>().Play("f02_newWalk_00");
		this.CensorCloud.SetActive(false);
		this.Hearts.SetActive(false);
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001861 RID: 6241 RVA: 0x000D5AB9 File Offset: 0x000D3EB9
	private int MinSkinColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001862 RID: 6242 RVA: 0x000D5ABC File Offset: 0x000D3EBC
	private int MaxSkinColor
	{
		get
		{
			return 5;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001863 RID: 6243 RVA: 0x000D5ABF File Offset: 0x000D3EBF
	private int MinHairstyle
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06001864 RID: 6244 RVA: 0x000D5AC2 File Offset: 0x000D3EC2
	private int MaxHairstyle
	{
		get
		{
			return this.Hairstyles.Length - 1;
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06001865 RID: 6245 RVA: 0x000D5ACE File Offset: 0x000D3ECE
	private int MinHairColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06001866 RID: 6246 RVA: 0x000D5AD1 File Offset: 0x000D3ED1
	private int MaxHairColor
	{
		get
		{
			return CustomizationScript.ColorPairs.Length - 1;
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06001867 RID: 6247 RVA: 0x000D5ADC File Offset: 0x000D3EDC
	private int MinEyeColor
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06001868 RID: 6248 RVA: 0x000D5ADF File Offset: 0x000D3EDF
	private int MaxEyeColor
	{
		get
		{
			return CustomizationScript.ColorPairs.Length - 1;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06001869 RID: 6249 RVA: 0x000D5AEA File Offset: 0x000D3EEA
	private int MinEyewear
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x0600186A RID: 6250 RVA: 0x000D5AED File Offset: 0x000D3EED
	private int MaxEyewear
	{
		get
		{
			return 5;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x0600186B RID: 6251 RVA: 0x000D5AF0 File Offset: 0x000D3EF0
	private int MinFacialHair
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x0600186C RID: 6252 RVA: 0x000D5AF3 File Offset: 0x000D3EF3
	private int MaxFacialHair
	{
		get
		{
			return this.FacialHairstyles.Length - 1;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x0600186D RID: 6253 RVA: 0x000D5AFF File Offset: 0x000D3EFF
	private int MinMaleUniform
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x0600186E RID: 6254 RVA: 0x000D5B02 File Offset: 0x000D3F02
	private int MaxMaleUniform
	{
		get
		{
			return this.MaleUniforms.Length - 1;
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x0600186F RID: 6255 RVA: 0x000D5B0E File Offset: 0x000D3F0E
	private int MinFemaleUniform
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06001870 RID: 6256 RVA: 0x000D5B11 File Offset: 0x000D3F11
	private int MaxFemaleUniform
	{
		get
		{
			return this.FemaleUniforms.Length - 1;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06001871 RID: 6257 RVA: 0x000D5B1D File Offset: 0x000D3F1D
	private float CameraSpeed
	{
		get
		{
			return Time.deltaTime * 10f;
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x000D5B2C File Offset: 0x000D3F2C
	private void Update()
	{
		if (!this.MyAudio.loop && !this.MyAudio.isPlaying)
		{
			this.MyAudio.loop = true;
			this.MyAudio.clip = this.LoveSickLoop;
			this.MyAudio.Play();
		}
		for (int i = 1; i < 3; i++)
		{
			Transform transform = this.Corridor[i];
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * this.ScrollSpeed);
			if (transform.position.z > 36f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 72f);
			}
		}
		if (this.Phase == 1)
		{
			if (this.WhitePanel.alpha == 0f)
			{
				this.GenderPanel.alpha = Mathf.MoveTowards(this.GenderPanel.alpha, 1f, Time.deltaTime * 2f);
				if (this.GenderPanel.alpha == 1f)
				{
					if (Input.GetButtonDown("A"))
					{
						this.Phase++;
					}
					if (Input.GetButtonDown("B"))
					{
						this.Apologize = true;
					}
					if (Input.GetButtonDown("X"))
					{
						this.White.color = new Color(0f, 0f, 0f, 1f);
						this.FadeOut = true;
						this.Phase = 0;
					}
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.GenderPanel.alpha = Mathf.MoveTowards(this.GenderPanel.alpha, 0f, Time.deltaTime * 2f);
			this.Black.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.color.a, 0f, Time.deltaTime * 2f));
			if (this.GenderPanel.alpha == 0f)
			{
				this.Senpai.gameObject.SetActive(true);
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			this.Adjustment += Input.GetAxis("Mouse X") * Time.deltaTime * 10f;
			if (this.Adjustment > 3f)
			{
				this.Adjustment = 3f;
			}
			else if (this.Adjustment < 0f)
			{
				this.Adjustment = 0f;
			}
			this.CustomizePanel.alpha = Mathf.MoveTowards(this.CustomizePanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.CustomizePanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Senpai.localEulerAngles = new Vector3(this.Senpai.localEulerAngles.x, 180f, this.Senpai.localEulerAngles.z);
					this.Phase++;
				}
				bool tappedDown = this.InputManager.TappedDown;
				bool tappedUp = this.InputManager.TappedUp;
				if (tappedDown || tappedUp)
				{
					if (tappedDown)
					{
						this.Selected = ((this.Selected < 6) ? (this.Selected + 1) : 1);
					}
					else if (tappedUp)
					{
						this.Selected = ((this.Selected > 1) ? (this.Selected - 1) : 6);
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 650f - (float)this.Selected * 150f, this.Highlight.localPosition.z);
				}
				if (this.InputManager.TappedRight)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Next;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Next;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Next;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Next;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Next;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Next;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}
				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == 1)
					{
						this.Data.skinColor.Value = this.Data.skinColor.Previous;
						this.UpdateSkin(this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.hairstyle.Value = this.Data.hairstyle.Previous;
						this.UpdateHairStyle(this.Data.hairstyle.Value);
					}
					else if (this.Selected == 3)
					{
						this.Data.hairColor.Value = this.Data.hairColor.Previous;
						this.UpdateColor(this.Data.hairColor.Value);
					}
					else if (this.Selected == 4)
					{
						this.Data.eyeColor.Value = this.Data.eyeColor.Previous;
						this.UpdateEyes(this.Data.eyeColor.Value);
					}
					else if (this.Selected == 5)
					{
						this.Data.eyewear.Value = this.Data.eyewear.Previous;
						this.UpdateEyewear(this.Data.eyewear.Value);
					}
					else if (this.Selected == 6)
					{
						this.Data.facialHair.Value = this.Data.facialHair.Previous;
						this.UpdateFacialHair(this.Data.facialHair.Value);
					}
				}
			}
			this.Rotation = Mathf.Lerp(this.Rotation, 45f - this.Adjustment * 30f, Time.deltaTime * 10f);
			this.AbsoluteRotation = 45f - Mathf.Abs(this.Rotation);
			if (this.Selected == 1)
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f + this.Adjustment, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f - this.AbsoluteRotation * 0.015f, this.CameraSpeed));
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -0.5f + this.Adjustment * 0.33333f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 1.5f - this.AbsoluteRotation * 0.015f * 0.33333f, this.CameraSpeed));
			}
			this.LoveSickCamera.transform.eulerAngles = new Vector3(0f, this.Rotation, 0f);
			base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			base.transform.position = this.LoveSickCamera.transform.position;
		}
		else if (this.Phase == 4)
		{
			this.Adjustment = Mathf.Lerp(this.Adjustment, 0f, Time.deltaTime * 10f);
			this.Rotation = Mathf.Lerp(this.Rotation, 45f, Time.deltaTime * 10f);
			this.AbsoluteRotation = 45f - Mathf.Abs(this.Rotation);
			this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f + this.Adjustment, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f - this.AbsoluteRotation * 0.015f, this.CameraSpeed));
			this.LoveSickCamera.transform.eulerAngles = new Vector3(0f, this.Rotation, 0f);
			base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
			base.transform.position = this.LoveSickCamera.transform.position;
			this.CustomizePanel.alpha = Mathf.MoveTowards(this.CustomizePanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.CustomizePanel.alpha == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 5)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Selected = 1;
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 650f - (float)this.Selected * 150f, this.Highlight.localPosition.z);
					this.Phase = 100;
				}
			}
		}
		else if (this.Phase == 6)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
				this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
				this.CensorCloud.SetActive(false);
				this.Yandere.gameObject.SetActive(true);
				this.Selected = 1;
				this.Phase++;
			}
		}
		else if (this.Phase == 7)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(this.UniformPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.UniformPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Yandere.localEulerAngles = new Vector3(this.Yandere.localEulerAngles.x, 180f, this.Yandere.localEulerAngles.z);
					this.Senpai.localEulerAngles = new Vector3(this.Senpai.localEulerAngles.x, 180f, this.Senpai.localEulerAngles.z);
					this.Phase++;
				}
				if (this.InputManager.TappedDown || this.InputManager.TappedUp)
				{
					this.Selected = ((this.Selected != 1) ? 1 : 2);
					this.UniformHighlight.localPosition = new Vector3(this.UniformHighlight.localPosition.x, 650f - (float)this.Selected * 150f, this.UniformHighlight.localPosition.z);
				}
				if (this.InputManager.TappedRight)
				{
					if (this.Selected == 1)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Next;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Next;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}
				if (this.InputManager.TappedLeft)
				{
					if (this.Selected == 1)
					{
						this.Data.maleUniform.Value = this.Data.maleUniform.Previous;
						this.UpdateMaleUniform(this.Data.maleUniform.Value, this.Data.skinColor.Value);
					}
					else if (this.Selected == 2)
					{
						this.Data.femaleUniform.Value = this.Data.femaleUniform.Previous;
						this.UpdateFemaleUniform(this.Data.femaleUniform.Value);
					}
				}
			}
		}
		else if (this.Phase == 8)
		{
			this.UniformPanel.alpha = Mathf.MoveTowards(this.UniformPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.UniformPanel.alpha == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 9)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 1f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 1f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Phase++;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Selected = 1;
					this.UniformHighlight.localPosition = new Vector3(this.UniformHighlight.localPosition.x, 650f - (float)this.Selected * 150f, this.UniformHighlight.localPosition.z);
					this.Phase = 99;
				}
			}
		}
		else if (this.Phase == 10)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.White.color = new Color(0f, 0f, 0f, 1f);
				this.FadeOut = true;
				this.Phase = 0;
			}
		}
		else if (this.Phase == 99)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.Phase = 7;
			}
		}
		else if (this.Phase == 100)
		{
			this.FinishPanel.alpha = Mathf.MoveTowards(this.FinishPanel.alpha, 0f, Time.deltaTime * 2f);
			if (this.FinishPanel.alpha == 0f)
			{
				this.Phase = 3;
			}
		}
		if (this.Phase > 3 && this.Phase < 100)
		{
			if (this.Phase < 6)
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, -1.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0.5f, this.CameraSpeed));
				base.transform.position = this.LoveSickCamera.transform.position;
			}
			else
			{
				this.LoveSickCamera.transform.position = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.position.x, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.y, 0.5f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.position.z, 0f, this.CameraSpeed));
				this.LoveSickCamera.transform.eulerAngles = new Vector3(Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.x, 15f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.y, 0f, this.CameraSpeed), Mathf.Lerp(this.LoveSickCamera.transform.eulerAngles.z, 0f, this.CameraSpeed));
				base.transform.eulerAngles = this.LoveSickCamera.transform.eulerAngles;
				base.transform.position = this.LoveSickCamera.transform.position;
				this.Yandere.position = new Vector3(Mathf.Lerp(this.Yandere.position.x, 1f, Time.deltaTime * 10f), Mathf.Lerp(this.Yandere.position.y, -1f, Time.deltaTime * 10f), Mathf.Lerp(this.Yandere.position.z, 3.5f, Time.deltaTime * 10f));
			}
		}
		if (this.FadeOut)
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(this.WhitePanel.alpha, 1f, Time.deltaTime);
			base.GetComponent<AudioSource>().volume = 1f - this.WhitePanel.alpha;
			if (this.WhitePanel.alpha == 1f)
			{
				SenpaiGlobals.CustomSenpai = true;
				SenpaiGlobals.SenpaiSkinColor = this.Data.skinColor.Value;
				SenpaiGlobals.SenpaiHairStyle = this.Data.hairstyle.Value;
				SenpaiGlobals.SenpaiHairColor = this.HairColorName;
				SenpaiGlobals.SenpaiEyeColor = this.EyeColorName;
				SenpaiGlobals.SenpaiEyeWear = this.Data.eyewear.Value;
				SenpaiGlobals.SenpaiFacialHair = this.Data.facialHair.Value;
				StudentGlobals.MaleUniform = this.Data.maleUniform.Value;
				StudentGlobals.FemaleUniform = this.Data.femaleUniform.Value;
				SceneManager.LoadScene("NewIntroScene");
			}
		}
		else
		{
			this.WhitePanel.alpha = Mathf.MoveTowards(this.WhitePanel.alpha, 0f, Time.deltaTime);
		}
		if (this.Apologize)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer < 1f)
			{
				this.ApologyWindow.localPosition = new Vector3(Mathf.Lerp(this.ApologyWindow.localPosition.x, 0f, Time.deltaTime * 10f), this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
			}
			else
			{
				this.ApologyWindow.localPosition = new Vector3(Mathf.Abs((this.ApologyWindow.localPosition.x - Time.deltaTime) * 0.01f) * (Time.deltaTime * 1000f), this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
				if (this.ApologyWindow.localPosition.x < -1360f)
				{
					this.ApologyWindow.localPosition = new Vector3(1360f, this.ApologyWindow.localPosition.y, this.ApologyWindow.localPosition.z);
					this.Apologize = false;
					this.Timer = 0f;
				}
			}
		}
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x000D72C7 File Offset: 0x000D56C7
	private void LateUpdate()
	{
		this.YandereHead.LookAt(this.SenpaiHead.position);
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x000D72DF File Offset: 0x000D56DF
	private void UpdateSkin(int skinColor)
	{
		this.UpdateMaleUniform(this.Data.maleUniform.Value, skinColor);
		this.SkinColorLabel.text = "Skin Color " + skinColor.ToString();
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x000D731C File Offset: 0x000D571C
	private void UpdateHairStyle(int hairstyle)
	{
		for (int i = 1; i < this.Hairstyles.Length; i++)
		{
			this.Hairstyles[i].SetActive(false);
		}
		if (hairstyle > 0)
		{
			this.HairRenderer = this.Hairstyles[hairstyle].GetComponent<Renderer>();
			this.Hairstyles[hairstyle].SetActive(true);
		}
		this.HairStyleLabel.text = "Hair Style " + hairstyle.ToString();
		this.UpdateColor(this.Data.hairColor.Value);
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x000D73B0 File Offset: 0x000D57B0
	private void UpdateFacialHair(int facialHair)
	{
		for (int i = 1; i < this.FacialHairstyles.Length; i++)
		{
			this.FacialHairstyles[i].SetActive(false);
		}
		if (facialHair > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[facialHair].GetComponent<Renderer>();
			this.FacialHairstyles[facialHair].SetActive(true);
		}
		this.FacialHairStyleLabel.text = "Facial Hair " + facialHair.ToString();
		this.UpdateColor(this.Data.hairColor.Value);
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x000D7444 File Offset: 0x000D5844
	private void UpdateColor(int hairColor)
	{
		KeyValuePair<Color, string> keyValuePair = CustomizationScript.ColorPairs[hairColor];
		Color key = keyValuePair.Key;
		this.HairColorName = keyValuePair.Value;
		if (this.Data.hairstyle.Value > 0)
		{
			this.HairRenderer = this.Hairstyles[this.Data.hairstyle.Value].GetComponent<Renderer>();
			this.HairRenderer.material.color = key;
		}
		if (this.Data.facialHair.Value > 0)
		{
			this.FacialHairRenderer = this.FacialHairstyles[this.Data.facialHair.Value].GetComponent<Renderer>();
			this.FacialHairRenderer.material.color = key;
			if (this.FacialHairRenderer.materials.Length > 1)
			{
				this.FacialHairRenderer.materials[1].color = key;
			}
		}
		this.HairColorLabel.text = "Hair Color " + hairColor.ToString();
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x000D7554 File Offset: 0x000D5954
	private void UpdateEyes(int eyeColor)
	{
		KeyValuePair<Color, string> keyValuePair = CustomizationScript.ColorPairs[eyeColor];
		Color key = keyValuePair.Key;
		this.EyeColorName = keyValuePair.Value;
		this.EyeR.material.color = key;
		this.EyeL.material.color = key;
		this.EyeColorLabel.text = "Eye Color " + eyeColor.ToString();
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x000D75CC File Offset: 0x000D59CC
	private void UpdateEyewear(int eyewear)
	{
		for (int i = 1; i < this.Eyewears.Length; i++)
		{
			this.Eyewears[i].SetActive(false);
		}
		if (eyewear > 0)
		{
			this.Eyewears[eyewear].SetActive(true);
		}
		this.EyeWearLabel.text = "Eye Wear " + eyewear.ToString();
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x000D7638 File Offset: 0x000D5A38
	private void UpdateMaleUniform(int maleUniform, int skinColor)
	{
		this.SenpaiRenderer.sharedMesh = this.MaleUniforms[maleUniform];
		if (maleUniform == 1)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[2].mainTexture = this.FaceTextures[skinColor];
		}
		else if (maleUniform == 2)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 3)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.MaleUniformTextures[maleUniform];
			this.SenpaiRenderer.materials[1].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.SkinTextures[skinColor];
		}
		else if (maleUniform == 4)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 5)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		else if (maleUniform == 6)
		{
			this.SenpaiRenderer.materials[0].mainTexture = this.FaceTextures[skinColor];
			this.SenpaiRenderer.materials[1].mainTexture = this.SkinTextures[skinColor];
			this.SenpaiRenderer.materials[2].mainTexture = this.MaleUniformTextures[maleUniform];
		}
		this.MaleUniformLabel.text = "Male Uniform " + maleUniform.ToString();
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x000D7894 File Offset: 0x000D5C94
	private void UpdateFemaleUniform(int femaleUniform)
	{
		this.YandereRenderer.sharedMesh = this.FemaleUniforms[femaleUniform];
		this.YandereRenderer.materials[0].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[1].mainTexture = this.FemaleUniformTextures[femaleUniform];
		this.YandereRenderer.materials[2].mainTexture = this.FemaleFace;
		this.YandereRenderer.materials[3].mainTexture = this.FemaleFace;
		this.FemaleUniformLabel.text = "Female Uniform " + femaleUniform.ToString();
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x000D793C File Offset: 0x000D5D3C
	private void LoveSickColorSwap()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in array)
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent != this.Highlight && component.transform.parent != this.UniformHighlight)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UITexture component2 = gameObject.GetComponent<UITexture>();
			if (component2 != null)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			UILabel component3 = gameObject.GetComponent<UILabel>();
			if (component3 != null && component3.color != Color.black)
			{
				component3.color = new Color(1f, 0f, 0f, component3.color.a);
			}
		}
	}

	// Token: 0x04001AD2 RID: 6866
	[SerializeField]
	private CustomizationScript.CustomizationData Data;

	// Token: 0x04001AD3 RID: 6867
	[SerializeField]
	private InputManagerScript InputManager;

	// Token: 0x04001AD4 RID: 6868
	[SerializeField]
	private Renderer FacialHairRenderer;

	// Token: 0x04001AD5 RID: 6869
	[SerializeField]
	private SkinnedMeshRenderer YandereRenderer;

	// Token: 0x04001AD6 RID: 6870
	[SerializeField]
	private SkinnedMeshRenderer SenpaiRenderer;

	// Token: 0x04001AD7 RID: 6871
	[SerializeField]
	private Renderer HairRenderer;

	// Token: 0x04001AD8 RID: 6872
	[SerializeField]
	private AudioSource MyAudio;

	// Token: 0x04001AD9 RID: 6873
	[SerializeField]
	private Renderer EyeR;

	// Token: 0x04001ADA RID: 6874
	[SerializeField]
	private Renderer EyeL;

	// Token: 0x04001ADB RID: 6875
	[SerializeField]
	private Transform UniformHighlight;

	// Token: 0x04001ADC RID: 6876
	[SerializeField]
	private Transform ApologyWindow;

	// Token: 0x04001ADD RID: 6877
	[SerializeField]
	private Transform YandereHead;

	// Token: 0x04001ADE RID: 6878
	[SerializeField]
	private Transform YandereNeck;

	// Token: 0x04001ADF RID: 6879
	[SerializeField]
	private Transform SenpaiHead;

	// Token: 0x04001AE0 RID: 6880
	[SerializeField]
	private Transform Highlight;

	// Token: 0x04001AE1 RID: 6881
	[SerializeField]
	private Transform Yandere;

	// Token: 0x04001AE2 RID: 6882
	[SerializeField]
	private Transform Senpai;

	// Token: 0x04001AE3 RID: 6883
	[SerializeField]
	private Transform[] Corridor;

	// Token: 0x04001AE4 RID: 6884
	[SerializeField]
	private UIPanel CustomizePanel;

	// Token: 0x04001AE5 RID: 6885
	[SerializeField]
	private UIPanel UniformPanel;

	// Token: 0x04001AE6 RID: 6886
	[SerializeField]
	private UIPanel FinishPanel;

	// Token: 0x04001AE7 RID: 6887
	[SerializeField]
	private UIPanel GenderPanel;

	// Token: 0x04001AE8 RID: 6888
	[SerializeField]
	private UIPanel WhitePanel;

	// Token: 0x04001AE9 RID: 6889
	[SerializeField]
	private UILabel FacialHairStyleLabel;

	// Token: 0x04001AEA RID: 6890
	[SerializeField]
	private UILabel FemaleUniformLabel;

	// Token: 0x04001AEB RID: 6891
	[SerializeField]
	private UILabel MaleUniformLabel;

	// Token: 0x04001AEC RID: 6892
	[SerializeField]
	private UILabel SkinColorLabel;

	// Token: 0x04001AED RID: 6893
	[SerializeField]
	private UILabel HairStyleLabel;

	// Token: 0x04001AEE RID: 6894
	[SerializeField]
	private UILabel HairColorLabel;

	// Token: 0x04001AEF RID: 6895
	[SerializeField]
	private UILabel EyeColorLabel;

	// Token: 0x04001AF0 RID: 6896
	[SerializeField]
	private UILabel EyeWearLabel;

	// Token: 0x04001AF1 RID: 6897
	[SerializeField]
	private GameObject LoveSickCamera;

	// Token: 0x04001AF2 RID: 6898
	[SerializeField]
	private GameObject CensorCloud;

	// Token: 0x04001AF3 RID: 6899
	[SerializeField]
	private GameObject BigCloud;

	// Token: 0x04001AF4 RID: 6900
	[SerializeField]
	private GameObject Hearts;

	// Token: 0x04001AF5 RID: 6901
	[SerializeField]
	private GameObject Cloud;

	// Token: 0x04001AF6 RID: 6902
	[SerializeField]
	private UISprite Black;

	// Token: 0x04001AF7 RID: 6903
	[SerializeField]
	private UISprite White;

	// Token: 0x04001AF8 RID: 6904
	[SerializeField]
	private bool Apologize;

	// Token: 0x04001AF9 RID: 6905
	[SerializeField]
	private bool LoveSick;

	// Token: 0x04001AFA RID: 6906
	[SerializeField]
	private bool FadeOut;

	// Token: 0x04001AFB RID: 6907
	[SerializeField]
	private float ScrollSpeed;

	// Token: 0x04001AFC RID: 6908
	[SerializeField]
	private float Timer;

	// Token: 0x04001AFD RID: 6909
	[SerializeField]
	private int Selected = 1;

	// Token: 0x04001AFE RID: 6910
	[SerializeField]
	private int Phase = 1;

	// Token: 0x04001AFF RID: 6911
	[SerializeField]
	private Texture[] FemaleUniformTextures;

	// Token: 0x04001B00 RID: 6912
	[SerializeField]
	private Texture[] MaleUniformTextures;

	// Token: 0x04001B01 RID: 6913
	[SerializeField]
	private Texture[] FaceTextures;

	// Token: 0x04001B02 RID: 6914
	[SerializeField]
	private Texture[] SkinTextures;

	// Token: 0x04001B03 RID: 6915
	[SerializeField]
	private GameObject[] FacialHairstyles;

	// Token: 0x04001B04 RID: 6916
	[SerializeField]
	private GameObject[] Hairstyles;

	// Token: 0x04001B05 RID: 6917
	[SerializeField]
	private GameObject[] Eyewears;

	// Token: 0x04001B06 RID: 6918
	[SerializeField]
	private Mesh[] FemaleUniforms;

	// Token: 0x04001B07 RID: 6919
	[SerializeField]
	private Mesh[] MaleUniforms;

	// Token: 0x04001B08 RID: 6920
	[SerializeField]
	private Texture FemaleFace;

	// Token: 0x04001B09 RID: 6921
	[SerializeField]
	private string HairColorName = string.Empty;

	// Token: 0x04001B0A RID: 6922
	[SerializeField]
	private string EyeColorName = string.Empty;

	// Token: 0x04001B0B RID: 6923
	[SerializeField]
	private AudioClip LoveSickIntro;

	// Token: 0x04001B0C RID: 6924
	[SerializeField]
	private AudioClip LoveSickLoop;

	// Token: 0x04001B0D RID: 6925
	public float AbsoluteRotation;

	// Token: 0x04001B0E RID: 6926
	public float Adjustment;

	// Token: 0x04001B0F RID: 6927
	public float Rotation;

	// Token: 0x04001B10 RID: 6928
	private static readonly KeyValuePair<Color, string>[] ColorPairs = new KeyValuePair<Color, string>[]
	{
		new KeyValuePair<Color, string>(default(Color), string.Empty),
		new KeyValuePair<Color, string>(new Color(0.5f, 0.5f, 0.5f), "Black"),
		new KeyValuePair<Color, string>(new Color(1f, 0f, 0f), "Red"),
		new KeyValuePair<Color, string>(new Color(1f, 1f, 0f), "Yellow"),
		new KeyValuePair<Color, string>(new Color(0f, 1f, 0f), "Green"),
		new KeyValuePair<Color, string>(new Color(0f, 1f, 1f), "Cyan"),
		new KeyValuePair<Color, string>(new Color(0f, 0f, 1f), "Blue"),
		new KeyValuePair<Color, string>(new Color(1f, 0f, 1f), "Purple"),
		new KeyValuePair<Color, string>(new Color(1f, 0.5f, 0f), "Orange"),
		new KeyValuePair<Color, string>(new Color(0.5f, 0.25f, 0f), "Brown"),
		new KeyValuePair<Color, string>(new Color(1f, 1f, 1f), "White")
	};

	// Token: 0x02000382 RID: 898
	private class CustomizationData
	{
		// Token: 0x04001B11 RID: 6929
		public global::RangeInt skinColor;

		// Token: 0x04001B12 RID: 6930
		public global::RangeInt hairstyle;

		// Token: 0x04001B13 RID: 6931
		public global::RangeInt hairColor;

		// Token: 0x04001B14 RID: 6932
		public global::RangeInt eyeColor;

		// Token: 0x04001B15 RID: 6933
		public global::RangeInt eyewear;

		// Token: 0x04001B16 RID: 6934
		public global::RangeInt facialHair;

		// Token: 0x04001B17 RID: 6935
		public global::RangeInt maleUniform;

		// Token: 0x04001B18 RID: 6936
		public global::RangeInt femaleUniform;
	}
}
