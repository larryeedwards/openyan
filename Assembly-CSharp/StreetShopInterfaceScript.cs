using System;
using UnityEngine;
using UnityEngine.PostProcessing;

// Token: 0x02000528 RID: 1320
public class StreetShopInterfaceScript : MonoBehaviour
{
	// Token: 0x06002064 RID: 8292 RVA: 0x00152378 File Offset: 0x00150778
	private void Start()
	{
		this.Shopkeeper.transform.localPosition = new Vector3(1485f, 0f, 0f);
		this.Interface.localPosition = new Vector3(-815.5f, 0f, 0f);
		this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
		this.UpdateFakeID();
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x001523F0 File Offset: 0x001507F0
	private void Update()
	{
		if (this.Show)
		{
			this.Shopkeeper.transform.localPosition = Vector3.Lerp(this.Shopkeeper.transform.localPosition, new Vector3((float)this.ShopkeeperPosition, 0f, 0f), Time.deltaTime * 10f);
			this.Interface.localPosition = Vector3.Lerp(this.Interface.localPosition, new Vector3(100f, 0f, 0f), Time.deltaTime * 10f);
			this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0f, Time.deltaTime * 10f);
			if (Input.GetButtonUp("B"))
			{
				this.Yandere.RPGCamera.enabled = true;
				this.PromptBar.Show = false;
				this.Yandere.CanMove = true;
				this.Show = false;
			}
			if (this.Timer > 0.5f && Input.GetButtonUp("A") && this.Icons[this.Selected].spriteName != "Yes")
			{
				this.CheckStore();
				this.UpdateIcons();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected > this.Limit)
				{
					this.Selected = 1;
				}
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected < 1)
				{
					this.Selected = this.Limit;
				}
				this.UpdateHighlight();
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 0.5f)
			{
				this.SpeechBubbleParent.localScale = Vector3.Lerp(this.SpeechBubbleParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
			if (this.SpeechPhase == 0)
			{
				this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[1];
				this.SpeechPhase++;
			}
			else if (this.Timer > 10f)
			{
				if (this.SpeechPhase == 1)
				{
					this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[2];
					this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[2];
					this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
					this.SpeechPhase++;
				}
				else if (this.SpeechPhase == 2 && this.Timer > 10.1f)
				{
					int num = UnityEngine.Random.Range(2, 4);
					this.Shopkeeper.mainTexture = this.ShopkeeperPortraits[num];
					this.Timer = 10f;
				}
			}
		}
		else
		{
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.Shopkeeper.transform.localPosition = Vector3.Lerp(this.Shopkeeper.transform.localPosition, new Vector3(1604f, 0f, 0f), Time.deltaTime * 10f);
			this.Interface.localPosition = Vector3.Lerp(this.Interface.localPosition, new Vector3(-815.5f, 0f, 0f), Time.deltaTime * 10f);
			if (this.ShowMaid)
			{
				this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0f, Time.deltaTime * 10f);
				this.MaidWindow.localScale = Vector3.Lerp(this.MaidWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				if (Input.GetButtonDown("A"))
				{
					this.StreetManager.FadeOut = true;
					this.StreetManager.GoToCafe = true;
				}
				else if (Input.GetButtonDown("B"))
				{
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.CanMove = true;
					this.ShowMaid = false;
				}
			}
			else
			{
				this.BlurAmount = Mathf.Lerp(this.BlurAmount, 0.6f, Time.deltaTime * 10f);
				this.MaidWindow.localScale = Vector3.Lerp(this.MaidWindow.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
			}
		}
		this.AdjustBlur();
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x001528C4 File Offset: 0x00150CC4
	private void AdjustBlur()
	{
		DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
		settings.focusDistance = this.BlurAmount;
		this.Profile.depthOfField.settings = settings;
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x00152900 File Offset: 0x00150D00
	public void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-50f, (float)(50 - 50 * this.Selected), 0f);
	}

	// Token: 0x06002068 RID: 8296 RVA: 0x0015292C File Offset: 0x00150D2C
	public void CheckStore()
	{
		if (this.AdultProducts[this.Selected] && !PlayerGlobals.FakeID)
		{
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[3];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
		}
		else if (PlayerGlobals.Money < this.Costs[this.Selected])
		{
			this.StreetManager.Clock.MoneyFail();
			this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[4];
			this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
			this.SpeechPhase = 0;
			this.Timer = 1f;
		}
		else
		{
			switch (this.CurrentStore)
			{
			case ShopType.Nonfunctional:
				this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[6];
				this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
				this.SpeechPhase = 0;
				this.Timer = 1f;
				break;
			case ShopType.Manga:
				this.PurchaseEffect();
				switch (this.Selected)
				{
				case 1:
					CollectibleGlobals.SetMangaCollected(6, true);
					break;
				case 2:
					CollectibleGlobals.SetMangaCollected(7, true);
					break;
				case 3:
					CollectibleGlobals.SetMangaCollected(8, true);
					break;
				case 4:
					CollectibleGlobals.SetMangaCollected(9, true);
					break;
				case 5:
					CollectibleGlobals.SetMangaCollected(10, true);
					break;
				case 6:
					CollectibleGlobals.SetMangaCollected(1, true);
					break;
				case 7:
					CollectibleGlobals.SetMangaCollected(2, true);
					break;
				case 8:
					CollectibleGlobals.SetMangaCollected(3, true);
					break;
				case 9:
					CollectibleGlobals.SetMangaCollected(4, true);
					break;
				case 10:
					CollectibleGlobals.SetMangaCollected(5, true);
					break;
				}
				break;
			case ShopType.Salon:
				this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[6];
				this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
				this.SpeechPhase = 0;
				this.Timer = 1f;
				break;
			case ShopType.Gift:
				this.PurchaseEffect();
				if (this.Selected < 6)
				{
					CollectibleGlobals.SenpaiGifts++;
				}
				else
				{
					CollectibleGlobals.MatchmakingGifts++;
				}
				CollectibleGlobals.SetGiftPurchased(this.Selected, true);
				break;
			}
		}
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x00152BCC File Offset: 0x00150FCC
	public void PurchaseEffect()
	{
		this.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[5];
		this.SpeechBubbleParent.localScale = new Vector3(0f, 0f, 0f);
		this.SpeechPhase = 0;
		this.Timer = 1f;
		PlayerGlobals.Money -= this.Costs[this.Selected];
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
		this.StreetManager.Clock.UpdateMoneyLabel();
		this.MyAudio.Play();
	}

	// Token: 0x0600206A RID: 8298 RVA: 0x00152C77 File Offset: 0x00151077
	public void UpdateFakeID()
	{
		this.FakeIDBox.SetActive(PlayerGlobals.FakeID);
	}

	// Token: 0x0600206B RID: 8299 RVA: 0x00152C8C File Offset: 0x0015108C
	public void UpdateIcons()
	{
		for (int i = 1; i < 11; i++)
		{
			this.Icons[i].spriteName = string.Empty;
			this.Icons[i].gameObject.SetActive(false);
			this.ProductsLabel[i].color = new Color(1f, 1f, 1f, 1f);
		}
		for (int i = 1; i < 11; i++)
		{
			if (this.AdultProducts[i])
			{
				this.Icons[i].spriteName = "18+";
			}
		}
		ShopType currentStore = this.CurrentStore;
		if (currentStore != ShopType.Manga)
		{
			if (currentStore == ShopType.Gift)
			{
				for (int i = 1; i < 11; i++)
				{
					if (CollectibleGlobals.GetGiftPurchased(i))
					{
						this.Icons[i].spriteName = "Yes";
						this.PricesLabel[i].text = "Owned";
					}
				}
			}
		}
		else
		{
			if (CollectibleGlobals.GetMangaCollected(1))
			{
				this.Icons[6].spriteName = "Yes";
				this.PricesLabel[6].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(2))
			{
				this.Icons[7].spriteName = "Yes";
				this.PricesLabel[7].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(3))
			{
				this.Icons[8].spriteName = "Yes";
				this.PricesLabel[8].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(4))
			{
				this.Icons[9].spriteName = "Yes";
				this.PricesLabel[9].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(5))
			{
				this.Icons[10].spriteName = "Yes";
				this.PricesLabel[10].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(6))
			{
				this.Icons[1].spriteName = "Yes";
				this.PricesLabel[1].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(7))
			{
				this.Icons[2].spriteName = "Yes";
				this.PricesLabel[2].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(8))
			{
				this.Icons[3].spriteName = "Yes";
				this.PricesLabel[3].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(9))
			{
				this.Icons[4].spriteName = "Yes";
				this.PricesLabel[4].text = "Owned";
			}
			if (CollectibleGlobals.GetMangaCollected(10))
			{
				this.Icons[5].spriteName = "Yes";
				this.PricesLabel[5].text = "Owned";
			}
		}
		for (int i = 1; i < 11; i++)
		{
			if (this.Icons[i].spriteName != string.Empty)
			{
				this.Icons[i].gameObject.SetActive(true);
				if (this.Icons[i].spriteName == "Yes")
				{
					this.ProductsLabel[i].color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
		}
	}

	// Token: 0x04002D81 RID: 11649
	public StreetManagerScript StreetManager;

	// Token: 0x04002D82 RID: 11650
	public InputManagerScript InputManager;

	// Token: 0x04002D83 RID: 11651
	public PostProcessingProfile Profile;

	// Token: 0x04002D84 RID: 11652
	public StalkerYandereScript Yandere;

	// Token: 0x04002D85 RID: 11653
	public PromptBarScript PromptBar;

	// Token: 0x04002D86 RID: 11654
	public UILabel SpeechBubbleLabel;

	// Token: 0x04002D87 RID: 11655
	public UILabel StoreNameLabel;

	// Token: 0x04002D88 RID: 11656
	public UILabel MoneyLabel;

	// Token: 0x04002D89 RID: 11657
	public Texture[] ShopkeeperPortraits;

	// Token: 0x04002D8A RID: 11658
	public string[] ShopkeeperSpeeches;

	// Token: 0x04002D8B RID: 11659
	public UILabel[] ProductsLabel;

	// Token: 0x04002D8C RID: 11660
	public UILabel[] PricesLabel;

	// Token: 0x04002D8D RID: 11661
	public UISprite[] Icons;

	// Token: 0x04002D8E RID: 11662
	public bool[] AdultProducts;

	// Token: 0x04002D8F RID: 11663
	public float[] Costs;

	// Token: 0x04002D90 RID: 11664
	public UITexture Shopkeeper;

	// Token: 0x04002D91 RID: 11665
	public Transform SpeechBubbleParent;

	// Token: 0x04002D92 RID: 11666
	public Transform MaidWindow;

	// Token: 0x04002D93 RID: 11667
	public Transform Highlight;

	// Token: 0x04002D94 RID: 11668
	public Transform Interface;

	// Token: 0x04002D95 RID: 11669
	public GameObject FakeIDBox;

	// Token: 0x04002D96 RID: 11670
	public AudioSource MyAudio;

	// Token: 0x04002D97 RID: 11671
	public int ShopkeeperPosition;

	// Token: 0x04002D98 RID: 11672
	public int SpeechPhase;

	// Token: 0x04002D99 RID: 11673
	public int Selected;

	// Token: 0x04002D9A RID: 11674
	public int Limit;

	// Token: 0x04002D9B RID: 11675
	public float BlurAmount;

	// Token: 0x04002D9C RID: 11676
	public float Timer;

	// Token: 0x04002D9D RID: 11677
	public bool ShowMaid;

	// Token: 0x04002D9E RID: 11678
	public bool Show;

	// Token: 0x04002D9F RID: 11679
	public ShopType CurrentStore;
}
