using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public class PhotoGalleryScript : MonoBehaviour
{
	// Token: 0x06001E50 RID: 7760 RVA: 0x00127C68 File Offset: 0x00126068
	private void Start()
	{
		if (this.Cursor != null)
		{
			this.Cursor.gameObject.SetActive(false);
			base.enabled = false;
		}
		if (this.Corkboard)
		{
			base.StartCoroutine(this.GetPhotos());
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00127CB6 File Offset: 0x001260B6
	private int CurrentIndex
	{
		get
		{
			return this.Column + (this.Row - 1) * 5;
		}
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00127CC9 File Offset: 0x001260C9
	private float LerpSpeed
	{
		get
		{
			return Time.unscaledDeltaTime * 10f;
		}
	}

	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x06001E53 RID: 7763 RVA: 0x00127CD6 File Offset: 0x001260D6
	private float HighlightX
	{
		get
		{
			return -450f + 150f * (float)this.Column;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00127CEB File Offset: 0x001260EB
	private float HighlightY
	{
		get
		{
			return 225f - 75f * (float)this.Row;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x06001E55 RID: 7765 RVA: 0x00127D00 File Offset: 0x00126100
	// (set) Token: 0x06001E56 RID: 7766 RVA: 0x00127D3C File Offset: 0x0012613C
	private float MovingPhotoXPercent
	{
		get
		{
			float num = -this.MaxPhotoX;
			float maxPhotoX = this.MaxPhotoX;
			return (this.MovingPhotograph.transform.localPosition.x - num) / (maxPhotoX - num);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(-this.MaxPhotoX + 2f * (this.MaxPhotoX * Mathf.Clamp01(value)), this.MovingPhotograph.transform.localPosition.y, this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06001E57 RID: 7767 RVA: 0x00127DAC File Offset: 0x001261AC
	// (set) Token: 0x06001E58 RID: 7768 RVA: 0x00127DE8 File Offset: 0x001261E8
	private float MovingPhotoYPercent
	{
		get
		{
			float num = -this.MaxPhotoY;
			float maxPhotoY = this.MaxPhotoY;
			return (this.MovingPhotograph.transform.localPosition.y - num) / (maxPhotoY - num);
		}
		set
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(this.MovingPhotograph.transform.localPosition.x, -this.MaxPhotoY + 2f * (this.MaxPhotoY * Mathf.Clamp01(value)), this.MovingPhotograph.transform.localPosition.z);
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00127E58 File Offset: 0x00126258
	// (set) Token: 0x06001E5A RID: 7770 RVA: 0x00127E80 File Offset: 0x00126280
	private float MovingPhotoRotation
	{
		get
		{
			return this.MovingPhotograph.transform.localEulerAngles.z;
		}
		set
		{
			this.MovingPhotograph.transform.localEulerAngles = new Vector3(this.MovingPhotograph.transform.localEulerAngles.x, this.MovingPhotograph.transform.localEulerAngles.y, value);
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x06001E5B RID: 7771 RVA: 0x00127ED4 File Offset: 0x001262D4
	// (set) Token: 0x06001E5C RID: 7772 RVA: 0x00127F0C File Offset: 0x0012630C
	private float CursorXPercent
	{
		get
		{
			float num = -4788f;
			float num2 = 4788f;
			return (this.Cursor.transform.localPosition.x - num) / (num2 - num);
		}
		set
		{
			this.Cursor.transform.localPosition = new Vector3(-4788f + 2f * (4788f * Mathf.Clamp01(value)), this.Cursor.transform.localPosition.y, this.Cursor.transform.localPosition.z);
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00127F78 File Offset: 0x00126378
	// (set) Token: 0x06001E5E RID: 7774 RVA: 0x00127FB0 File Offset: 0x001263B0
	private float CursorYPercent
	{
		get
		{
			float num = -3122f;
			float num2 = 3122f;
			return (this.Cursor.transform.localPosition.y - num) / (num2 - num);
		}
		set
		{
			this.Cursor.transform.localPosition = new Vector3(this.Cursor.transform.localPosition.x, -3122f + 2f * (3122f * Mathf.Clamp01(value)), this.Cursor.transform.localPosition.z);
		}
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x0012801C File Offset: 0x0012641C
	private void UpdatePhotoSelection()
	{
		if (Input.GetButtonDown("A"))
		{
			if (!this.NamingBully)
			{
				UITexture uitexture = this.Photographs[this.CurrentIndex];
				if (uitexture.mainTexture != this.NoPhoto)
				{
					this.ViewPhoto.mainTexture = uitexture.mainTexture;
					this.ViewPhoto.transform.position = uitexture.transform.position;
					this.ViewPhoto.transform.localScale = uitexture.transform.localScale;
					this.Destination.position = uitexture.transform.position;
					this.Viewing = true;
					if (!this.Corkboard)
					{
						for (int i = 1; i < 26; i++)
						{
							this.Hearts[i].gameObject.SetActive(false);
						}
					}
					this.CanAdjust = false;
				}
				this.UpdateButtonPrompts();
			}
			else
			{
				UITexture uitexture2 = this.Photographs[this.CurrentIndex];
				if (uitexture2.mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
				{
					this.Yandere.Police.EndOfDay.FragileTarget = PlayerGlobals.GetBullyPhoto(this.CurrentIndex);
					this.Yandere.StudentManager.FragileOfferHelp.Continue();
					this.PauseScreen.MainMenu.SetActive(true);
					this.Yandere.RPGCamera.enabled = true;
					base.gameObject.SetActive(false);
					this.PauseScreen.Show = false;
					this.PromptBar.Show = false;
					this.NamingBully = false;
					Time.timeScale = 1f;
				}
			}
		}
		if (!this.NamingBully && Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("X"))
		{
			this.ViewPhoto.mainTexture = null;
			int currentIndex = this.CurrentIndex;
			if (this.Photographs[currentIndex].mainTexture != this.NoPhoto)
			{
				this.Photographs[currentIndex].mainTexture = this.NoPhoto;
				PlayerGlobals.SetPhoto(currentIndex, false);
				PlayerGlobals.SetSenpaiPhoto(currentIndex, false);
				TaskGlobals.SetGuitarPhoto(currentIndex, false);
				TaskGlobals.SetKittenPhoto(currentIndex, false);
				this.Hearts[currentIndex].gameObject.SetActive(false);
				this.TaskManager.UpdateTaskStatus();
			}
			this.UpdateButtonPrompts();
		}
		if (this.Corkboard)
		{
			if (Input.GetButtonDown("Y"))
			{
				this.CanAdjust = false;
				this.Cursor.gameObject.SetActive(true);
				this.Adjusting = true;
				this.UpdateButtonPrompts();
			}
		}
		else if (Input.GetButtonDown("Y") && PlayerGlobals.GetSenpaiPhoto(this.CurrentIndex))
		{
			int currentIndex2 = this.CurrentIndex;
			PlayerGlobals.SetSenpaiPhoto(currentIndex2, false);
			this.Hearts[currentIndex2].gameObject.SetActive(false);
			this.CanAdjust = false;
			this.Yandere.Sanity += 20f;
			this.UpdateButtonPrompts();
			AudioSource.PlayClipAtPoint(this.Sighs[UnityEngine.Random.Range(0, this.Sighs.Length)], this.Yandere.Head.position);
		}
		if (this.InputManager.TappedRight)
		{
			this.Column = ((this.Column >= 5) ? 1 : (this.Column + 1));
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column = ((this.Column <= 1) ? 5 : (this.Column - 1));
		}
		if (this.InputManager.TappedUp)
		{
			this.Row = ((this.Row <= 1) ? 5 : (this.Row - 1));
		}
		if (this.InputManager.TappedDown)
		{
			this.Row = ((this.Row >= 5) ? 1 : (this.Row + 1));
		}
		bool flag = this.InputManager.TappedRight || this.InputManager.TappedLeft;
		bool flag2 = this.InputManager.TappedUp || this.InputManager.TappedDown;
		if (flag || flag2)
		{
			this.Highlight.transform.localPosition = new Vector3(this.HighlightX, this.HighlightY, this.Highlight.transform.localPosition.z);
			this.UpdateButtonPrompts();
		}
		this.ViewPhoto.transform.localScale = Vector3.Lerp(this.ViewPhoto.transform.localScale, new Vector3(1f, 1f, 1f), this.LerpSpeed);
		this.ViewPhoto.transform.position = Vector3.Lerp(this.ViewPhoto.transform.position, this.Destination.position, this.LerpSpeed);
		if (this.Corkboard)
		{
			this.Gallery.transform.localPosition = new Vector3(this.Gallery.transform.localPosition.x, Mathf.Lerp(this.Gallery.transform.localPosition.y, 0f, Time.deltaTime * 10f), this.Gallery.transform.localPosition.z);
		}
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x00128640 File Offset: 0x00126A40
	private void UpdatePhotoViewing()
	{
		this.ViewPhoto.transform.localScale = Vector3.Lerp(this.ViewPhoto.transform.localScale, (!this.Corkboard) ? new Vector3(6.5f, 6.5f, 6.5f) : new Vector3(5.8f, 5.8f, 5.8f), this.LerpSpeed);
		this.ViewPhoto.transform.localPosition = Vector3.Lerp(this.ViewPhoto.transform.localPosition, Vector3.zero, this.LerpSpeed);
		bool flag = this.Corkboard && Input.GetButtonDown("A");
		if (flag)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Photograph, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.CorkboardPanel;
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.GetComponent<UITexture>().mainTexture = this.Photographs[this.CurrentIndex].mainTexture;
			this.MovingPhotograph = gameObject;
			this.CanAdjust = false;
			this.Adjusting = true;
			this.Viewing = false;
			this.Moving = true;
			this.CorkboardPhotographs[this.Photos] = gameObject.GetComponent<HomeCorkboardPhotoScript>();
			this.CorkboardPhotographs[this.Photos].ID = this.CurrentIndex;
			this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
			this.Photos++;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("B"))
		{
			this.Viewing = false;
			if (this.Corkboard)
			{
				this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
				this.CanAdjust = true;
			}
			else
			{
				for (int i = 1; i < 26; i++)
				{
					if (PlayerGlobals.GetSenpaiPhoto(i))
					{
						this.Hearts[i].gameObject.SetActive(true);
						this.CanAdjust = true;
					}
				}
			}
			this.UpdateButtonPrompts();
		}
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x001288CC File Offset: 0x00126CCC
	private void UpdateCorkboardPhoto()
	{
		if (Input.GetMouseButton(1))
		{
			this.MovingPhotoRotation += this.MouseDelta.x;
		}
		else
		{
			this.MovingPhotograph.transform.localPosition = new Vector3(this.MovingPhotograph.transform.localPosition.x + this.MouseDelta.x * 8.66666f, this.MovingPhotograph.transform.localPosition.y + this.MouseDelta.y * 8.66666f, 0f);
		}
		if (Input.GetButton("LB"))
		{
			this.MovingPhotoRotation += Time.deltaTime * 100f;
		}
		if (Input.GetButton("RB"))
		{
			this.MovingPhotoRotation -= Time.deltaTime * 100f;
		}
		if (Input.GetButton("Y"))
		{
			this.MovingPhotograph.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
			if (this.MovingPhotograph.transform.localScale.x > 2f)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(2f, 2f, 2f);
			}
		}
		if (Input.GetButton("X"))
		{
			this.MovingPhotograph.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
			if (this.MovingPhotograph.transform.localScale.x < 1f)
			{
				this.MovingPhotograph.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		Vector2 vector = new Vector2(this.MovingPhotograph.transform.localPosition.x, this.MovingPhotograph.transform.localPosition.y);
		Vector2 vector2 = new Vector2(Input.GetAxis("Horizontal") * 86.66666f * this.SpeedLimit, Input.GetAxis("Vertical") * 86.66666f * this.SpeedLimit);
		this.MovingPhotograph.transform.localPosition = new Vector3(Mathf.Clamp(vector.x + vector2.x, -this.MaxPhotoX, this.MaxPhotoX), Mathf.Clamp(vector.y + vector2.y, -this.MaxPhotoY, this.MaxPhotoY), this.MovingPhotograph.transform.localPosition.z);
		if (Input.GetButtonDown("A"))
		{
			this.Cursor.transform.localPosition = this.MovingPhotograph.transform.localPosition;
			this.Cursor.gameObject.SetActive(true);
			this.Moving = false;
			this.UpdateButtonPrompts();
			this.PhotoID++;
		}
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x00128C00 File Offset: 0x00127000
	private void UpdateString()
	{
		this.MouseDelta.x = this.MouseDelta.x + Input.GetAxis("Horizontal") * 8.66666f * this.SpeedLimit;
		this.MouseDelta.y = this.MouseDelta.y + Input.GetAxis("Vertical") * 8.66666f * this.SpeedLimit;
		Transform transform;
		if (this.StringPhase == 0)
		{
			transform = this.String.Origin;
			this.String.Target.position = this.String.Origin.position;
		}
		else
		{
			transform = this.String.Target;
		}
		transform.localPosition = new Vector3(transform.localPosition.x - this.MouseDelta.x * Time.deltaTime * 0.33333f, transform.localPosition.y + this.MouseDelta.y * Time.deltaTime * 0.33333f, 0f);
		if (transform.localPosition.x > 0.971f)
		{
			transform.localPosition = new Vector3(0.971f, transform.localPosition.y, transform.localPosition.z);
		}
		else if (transform.localPosition.x < -0.971f)
		{
			transform.localPosition = new Vector3(-0.971f, transform.localPosition.y, transform.localPosition.z);
		}
		if (transform.localPosition.y > 0.637f)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, 0.637f, transform.localPosition.z);
		}
		else if (transform.localPosition.y < -0.637f)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, -0.637f, transform.localPosition.z);
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.StringPhase == 0)
			{
				this.StringPhase++;
			}
			else if (this.StringPhase == 1)
			{
				this.Cursor.transform.localPosition = transform.localPosition;
				this.Cursor.gameObject.SetActive(true);
				this.MovingString = false;
				this.StringPhase = 0;
				this.UpdateButtonPrompts();
			}
		}
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x00128EA0 File Offset: 0x001272A0
	private void UpdateCorkboardCursor()
	{
		Vector2 vector = new Vector2(this.Cursor.transform.localPosition.x, this.Cursor.transform.localPosition.y);
		Vector2 vector2 = new Vector2(this.MouseDelta.x * 8.66666f + Input.GetAxis("Horizontal") * 86.66666f * this.SpeedLimit, this.MouseDelta.y * 8.66666f + Input.GetAxis("Vertical") * 86.66666f * this.SpeedLimit);
		this.Cursor.transform.localPosition = new Vector3(Mathf.Clamp(vector.x + vector2.x, -4788f, 4788f), Mathf.Clamp(vector.y + vector2.y, -3122f, 3122f), this.Cursor.transform.localPosition.z);
		if (Input.GetButtonDown("A") && this.Cursor.Photograph != null)
		{
			this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
			this.MovingPhotograph = this.Cursor.Photograph;
			this.Cursor.gameObject.SetActive(false);
			this.Moving = true;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("Y"))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StringSet, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.StringParent;
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.String = gameObject.GetComponent<StringScript>();
			this.Cursor.gameObject.SetActive(false);
			this.MovingString = true;
			this.CorkboardStrings[this.Strings] = this.String.GetComponent<StringScript>();
			this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
			this.Strings++;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.Cursor.Photograph != null)
			{
				this.Cursor.Photograph = null;
			}
			this.Cursor.transform.localPosition = new Vector3(0f, 0f, this.Cursor.transform.localPosition.z);
			this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
			this.CanAdjust = true;
			this.Cursor.gameObject.SetActive(false);
			this.Adjusting = false;
			this.UpdateButtonPrompts();
		}
		if (Input.GetButtonDown("X"))
		{
			if (this.Cursor.Photograph != null)
			{
				this.Cursor.Highlight.transform.position = new Vector3(this.Cursor.Highlight.transform.position.x, 100f, this.Cursor.Highlight.transform.position.z);
				this.Shuffle(this.Cursor.Photograph.GetComponent<HomeCorkboardPhotoScript>().ArrayID);
				UnityEngine.Object.Destroy(this.Cursor.Photograph);
				this.Photos--;
				this.Cursor.Photograph = null;
				this.UpdateButtonPrompts();
			}
			if (this.Cursor.Tack != null)
			{
				this.Cursor.CircleHighlight.transform.position = new Vector3(this.Cursor.CircleHighlight.transform.position.x, 100f, this.Cursor.CircleHighlight.transform.position.z);
				this.ShuffleStrings(this.Cursor.Tack.transform.parent.GetComponent<StringScript>().ArrayID);
				UnityEngine.Object.Destroy(this.Cursor.Tack.transform.parent.gameObject);
				this.Strings--;
				this.Cursor.Tack = null;
				this.UpdateButtonPrompts();
			}
		}
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x001293C8 File Offset: 0x001277C8
	private void Update()
	{
		if (this.GotPhotos && this.Corkboard && !this.SpawnedPhotos)
		{
			this.SpawnPhotographs();
			this.SpawnStrings();
			base.enabled = false;
			base.gameObject.SetActive(false);
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = string.Empty;
			this.PromptBar.Label[5].text = string.Empty;
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = false;
		}
		if (!this.Adjusting)
		{
			if (!this.Viewing)
			{
				this.UpdatePhotoSelection();
			}
			else
			{
				this.UpdatePhotoViewing();
			}
		}
		else
		{
			if (this.Corkboard)
			{
				this.Gallery.transform.localPosition = new Vector3(this.Gallery.transform.localPosition.x, Mathf.Lerp(this.Gallery.transform.localPosition.y, 1000f, Time.deltaTime * 10f), this.Gallery.transform.localPosition.z);
			}
			this.MouseDelta = new Vector2(Input.mousePosition.x - this.PreviousPosition.x, Input.mousePosition.y - this.PreviousPosition.y);
			if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
			{
				this.SpeedLimit = 0.1f;
			}
			else
			{
				this.SpeedLimit = 1f;
			}
			if (this.Moving)
			{
				this.UpdateCorkboardPhoto();
			}
			else if (this.MovingString)
			{
				this.UpdateString();
			}
			else
			{
				this.UpdateCorkboardCursor();
			}
		}
		this.PreviousPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x00129624 File Offset: 0x00127A24
	public IEnumerator GetPhotos()
	{
		if (!this.Corkboard)
		{
			for (int i = 1; i < 26; i++)
			{
				this.Hearts[i].gameObject.SetActive(false);
			}
		}
		for (int ID = 1; ID < 26; ID++)
		{
			if (PlayerGlobals.GetPhoto(ID))
			{
				string path = string.Concat(new object[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Photographs/Photo_",
					ID,
					".png"
				});
				WWW www = new WWW(path);
				yield return www;
				if (www.error == null)
				{
					this.Photographs[ID].mainTexture = www.texture;
					if (!this.Corkboard && PlayerGlobals.GetSenpaiPhoto(ID))
					{
						this.Hearts[ID].gameObject.SetActive(true);
					}
				}
				else
				{
					Debug.Log(string.Concat(new object[]
					{
						"Could not retrieve Photo ",
						ID,
						". Maybe it was deleted from Streaming Assets? Setting Photo ",
						ID,
						" to ''false''."
					}));
					PlayerGlobals.SetPhoto(ID, false);
				}
			}
		}
		this.LoadingScreen.SetActive(false);
		if (!this.Corkboard)
		{
			this.PauseScreen.Sideways = true;
		}
		this.UpdateButtonPrompts();
		base.enabled = true;
		base.gameObject.SetActive(true);
		this.GotPhotos = true;
		yield break;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x00129640 File Offset: 0x00127A40
	public void UpdateButtonPrompts()
	{
		if (this.NamingBully)
		{
			UITexture uitexture = this.Photographs[this.CurrentIndex];
			if (uitexture.mainTexture != this.NoPhoto && PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
			{
				if (PlayerGlobals.GetBullyPhoto(this.CurrentIndex) > 0)
				{
					this.PromptBar.Label[0].text = "Name Bully";
				}
				else
				{
					this.PromptBar.Label[0].text = string.Empty;
				}
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (this.Moving || this.MovingString)
		{
			this.PromptBar.Label[0].text = "Place";
			this.PromptBar.Label[1].text = string.Empty;
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
			if (!this.MovingString)
			{
				this.PromptBar.Label[2].text = "Resize";
				this.PromptBar.Label[3].text = "Resize";
			}
		}
		else if (this.Adjusting)
		{
			if (this.Cursor.Photograph != null)
			{
				this.PromptBar.Label[0].text = "Adjust";
				this.PromptBar.Label[1].text = string.Empty;
				this.PromptBar.Label[2].text = "Remove";
				this.PromptBar.Label[3].text = string.Empty;
			}
			else if (this.Cursor.Tack != null)
			{
				this.PromptBar.Label[2].text = "Remove";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}
			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[3].text = "Place Pin";
			this.PromptBar.Label[4].text = "Move";
			this.PromptBar.Label[5].text = "Move";
		}
		else if (!this.Viewing)
		{
			int currentIndex = this.CurrentIndex;
			if (this.Photographs[currentIndex].mainTexture != this.NoPhoto)
			{
				this.PromptBar.Label[0].text = "View";
				this.PromptBar.Label[2].text = "Delete";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.Label[2].text = string.Empty;
			}
			if (!this.Corkboard)
			{
				this.PromptBar.Label[3].text = ((!PlayerGlobals.GetSenpaiPhoto(currentIndex)) ? string.Empty : "Use");
			}
			else
			{
				this.PromptBar.Label[3].text = "Corkboard";
			}
			this.PromptBar.Label[1].text = "Back";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.Label[5].text = "Choose";
		}
		else
		{
			if (this.Corkboard)
			{
				this.PromptBar.Label[0].text = "Place";
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			this.PromptBar.Label[2].text = string.Empty;
			this.PromptBar.Label[3].text = string.Empty;
			this.PromptBar.Label[4].text = string.Empty;
			this.PromptBar.Label[5].text = string.Empty;
		}
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x00129B64 File Offset: 0x00127F64
	private void Shuffle(int Start)
	{
		for (int i = Start; i < this.CorkboardPhotographs.Length - 1; i++)
		{
			if (this.CorkboardPhotographs[i] != null)
			{
				this.CorkboardPhotographs[i].ArrayID--;
				this.CorkboardPhotographs[i] = this.CorkboardPhotographs[i + 1];
			}
		}
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x00129BC8 File Offset: 0x00127FC8
	private void ShuffleStrings(int Start)
	{
		for (int i = Start; i < this.CorkboardPhotographs.Length - 1; i++)
		{
			if (this.CorkboardStrings[i] != null)
			{
				this.CorkboardStrings[i].ArrayID--;
				this.CorkboardStrings[i] = this.CorkboardStrings[i + 1];
			}
		}
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x00129C2C File Offset: 0x0012802C
	public void SaveAllPhotographs()
	{
		for (int i = 0; i < 100; i++)
		{
			if (this.CorkboardPhotographs[i] != null)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_Exists"
				}), 1);
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}), this.CorkboardPhotographs[i].ID);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionX"
				}), this.CorkboardPhotographs[i].transform.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionY"
				}), this.CorkboardPhotographs[i].transform.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionZ"
				}), this.CorkboardPhotographs[i].transform.localPosition.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationX"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationY"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationZ"
				}), this.CorkboardPhotographs[i].transform.localEulerAngles.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleX"
				}), this.CorkboardPhotographs[i].transform.localScale.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleY"
				}), this.CorkboardPhotographs[i].transform.localScale.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleZ"
				}), this.CorkboardPhotographs[i].transform.localScale.z);
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x0012A04C File Offset: 0x0012844C
	public void SpawnPhotographs()
	{
		for (int i = 0; i < 100; i++)
		{
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardPhoto_",
				i,
				"_Exists"
			})) == 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Photograph, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = this.CorkboardPanel;
				gameObject.transform.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PositionZ"
				})));
				gameObject.transform.localEulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_RotationZ"
				})));
				gameObject.transform.localScale = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_ScaleZ"
				})));
				gameObject.GetComponent<UITexture>().mainTexture = this.Photographs[PlayerPrefs.GetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}))].mainTexture;
				this.CorkboardPhotographs[this.Photos] = gameObject.GetComponent<HomeCorkboardPhotoScript>();
				this.CorkboardPhotographs[this.Photos].ID = PlayerPrefs.GetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardPhoto_",
					i,
					"_PhotoID"
				}));
				this.CorkboardPhotographs[this.Photos].ArrayID = this.Photos;
				this.Photos++;
			}
		}
		this.SpawnedPhotos = true;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x0012A420 File Offset: 0x00128820
	public void SaveAllStrings()
	{
		Debug.Log("Saved strings.");
		for (int i = 0; i < 100; i++)
		{
			if (this.CorkboardStrings[i] != null)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 1);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionX"
				}), this.CorkboardStrings[i].Origin.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionY"
				}), this.CorkboardStrings[i].Origin.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionZ"
				}), this.CorkboardStrings[i].Origin.localPosition.z);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionX"
				}), this.CorkboardStrings[i].Target.localPosition.x);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionY"
				}), this.CorkboardStrings[i].Target.localPosition.y);
				PlayerPrefs.SetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionZ"
				}), this.CorkboardStrings[i].Target.localPosition.z);
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x0012A6F4 File Offset: 0x00128AF4
	public void SpawnStrings()
	{
		for (int i = 0; i < 100; i++)
		{
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_CorkboardString_",
				i,
				"_Exists"
			})) == 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StringSet, base.transform.position, Quaternion.identity);
				gameObject.transform.parent = this.StringParent;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				this.String = gameObject.GetComponent<StringScript>();
				this.String.Origin.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_PositionZ"
				})));
				this.String.Target.localPosition = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionX"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionY"
				})), PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString2_",
					i,
					"_PositionZ"
				})));
				this.CorkboardStrings[this.Strings] = this.String.GetComponent<StringScript>();
				this.CorkboardStrings[this.Strings].ArrayID = this.Strings;
				this.Strings++;
			}
			else
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					GameGlobals.Profile,
					"_CorkboardString_",
					i,
					"_Exists"
				}), 0);
			}
		}
	}

	// Token: 0x04002719 RID: 10009
	public HomeCorkboardPhotoScript[] CorkboardPhotographs;

	// Token: 0x0400271A RID: 10010
	public StringScript[] CorkboardStrings;

	// Token: 0x0400271B RID: 10011
	public int PhotoID;

	// Token: 0x0400271C RID: 10012
	public StringScript String;

	// Token: 0x0400271D RID: 10013
	public InputManagerScript InputManager;

	// Token: 0x0400271E RID: 10014
	public PauseScreenScript PauseScreen;

	// Token: 0x0400271F RID: 10015
	public TaskManagerScript TaskManager;

	// Token: 0x04002720 RID: 10016
	public InputDeviceScript InputDevice;

	// Token: 0x04002721 RID: 10017
	public PromptBarScript PromptBar;

	// Token: 0x04002722 RID: 10018
	public HomeCursorScript Cursor;

	// Token: 0x04002723 RID: 10019
	public YandereScript Yandere;

	// Token: 0x04002724 RID: 10020
	public GameObject MovingPhotograph;

	// Token: 0x04002725 RID: 10021
	public GameObject LoadingScreen;

	// Token: 0x04002726 RID: 10022
	public GameObject Photograph;

	// Token: 0x04002727 RID: 10023
	public GameObject StringSet;

	// Token: 0x04002728 RID: 10024
	public Transform CorkboardPanel;

	// Token: 0x04002729 RID: 10025
	public Transform Destination;

	// Token: 0x0400272A RID: 10026
	public Transform Highlight;

	// Token: 0x0400272B RID: 10027
	public Transform Gallery;

	// Token: 0x0400272C RID: 10028
	public Transform StringParent;

	// Token: 0x0400272D RID: 10029
	public UITexture[] Photographs;

	// Token: 0x0400272E RID: 10030
	public UISprite[] Hearts;

	// Token: 0x0400272F RID: 10031
	public AudioClip[] Sighs;

	// Token: 0x04002730 RID: 10032
	public UITexture ViewPhoto;

	// Token: 0x04002731 RID: 10033
	public Texture NoPhoto;

	// Token: 0x04002732 RID: 10034
	public Vector2 PreviousPosition;

	// Token: 0x04002733 RID: 10035
	public Vector2 MouseDelta;

	// Token: 0x04002734 RID: 10036
	public bool DoNotRaisePromptBar;

	// Token: 0x04002735 RID: 10037
	public bool SpawnedPhotos;

	// Token: 0x04002736 RID: 10038
	public bool MovingString;

	// Token: 0x04002737 RID: 10039
	public bool NamingBully;

	// Token: 0x04002738 RID: 10040
	public bool Adjusting;

	// Token: 0x04002739 RID: 10041
	public bool CanAdjust;

	// Token: 0x0400273A RID: 10042
	public bool Corkboard;

	// Token: 0x0400273B RID: 10043
	public bool GotPhotos;

	// Token: 0x0400273C RID: 10044
	public bool Viewing;

	// Token: 0x0400273D RID: 10045
	public bool Moving;

	// Token: 0x0400273E RID: 10046
	public bool Reset;

	// Token: 0x0400273F RID: 10047
	public int StringPhase;

	// Token: 0x04002740 RID: 10048
	public int Strings;

	// Token: 0x04002741 RID: 10049
	public int Photos;

	// Token: 0x04002742 RID: 10050
	public int Column;

	// Token: 0x04002743 RID: 10051
	public int Row;

	// Token: 0x04002744 RID: 10052
	public float MaxPhotoX = 4150f;

	// Token: 0x04002745 RID: 10053
	public float MaxPhotoY = 2500f;

	// Token: 0x04002746 RID: 10054
	private const float MaxCursorX = 4788f;

	// Token: 0x04002747 RID: 10055
	private const float MaxCursorY = 3122f;

	// Token: 0x04002748 RID: 10056
	private const float CorkboardAspectRatio = 1.53363228f;

	// Token: 0x04002749 RID: 10057
	public float SpeedLimit;
}
