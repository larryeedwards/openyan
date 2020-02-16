using System;
using UnityEngine;

// Token: 0x02000529 RID: 1321
public class StreetShopScript : MonoBehaviour
{
	// Token: 0x0600206D RID: 8301 RVA: 0x00153002 File Offset: 0x00151402
	private void Start()
	{
		this.MyLabel.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x00153028 File Offset: 0x00151428
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 1f)
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 10f);
		}
		else
		{
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 10f);
		}
		this.MyLabel.color = new Color(1f, 0.75f, 1f, this.Alpha);
		if (this.Alpha == 1f && Input.GetButtonDown("A"))
		{
			if (this.Exit)
			{
				this.StreetManager.FadeOut = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.CanMove = false;
			}
			else if (this.MaidCafe)
			{
				this.StreetShopInterface.ShowMaid = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.RPGCamera.enabled = false;
				this.Yandere.CanMove = false;
			}
			else if (!this.Binoculars)
			{
				if (!this.StreetShopInterface.Show)
				{
					this.StreetShopInterface.CurrentStore = this.StoreType;
					this.StreetShopInterface.Show = true;
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Purchase";
					this.PromptBar.Label[1].text = "Exit";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
					this.Yandere.CanMove = false;
					this.UpdateShopInterface();
				}
			}
			else if (PlayerGlobals.Money >= 0.25f)
			{
				this.MyAudio.clip = this.InsertCoin;
				PlayerGlobals.Money -= 0.25f;
				this.HomeClock.UpdateMoneyLabel();
				this.BinocularCamera.gameObject.SetActive(true);
				this.BinocularRenderer.enabled = false;
				this.BinocularOverlay.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.Yandere.MyAnimation.CrossFade(this.Yandere.IdleAnim);
				this.Yandere.transform.position = new Vector3(5f, 0f, 3f);
				this.Yandere.CanMove = false;
				this.MyAudio.Play();
			}
			else
			{
				this.HomeClock.MoneyFail();
			}
		}
		if (this.Binoculars && this.BinocularCamera.gameObject.activeInHierarchy)
		{
			if (this.InputDevice.Type == InputDeviceType.MouseAndKeyboard)
			{
				this.RotationX -= Input.GetAxis("Mouse Y") * (this.BinocularCamera.fieldOfView / 60f);
				this.RotationY += Input.GetAxis("Mouse X") * (this.BinocularCamera.fieldOfView / 60f);
			}
			else
			{
				this.RotationX -= Input.GetAxis("Mouse Y") * (this.BinocularCamera.fieldOfView / 60f);
				this.RotationY += Input.GetAxis("Mouse X") * (this.BinocularCamera.fieldOfView / 60f);
			}
			this.BinocularCamera.transform.eulerAngles = new Vector3(this.RotationX, this.RotationY + 90f, 0f);
			if (this.RotationX > 45f)
			{
				this.RotationX = 45f;
			}
			if (this.RotationX < -45f)
			{
				this.RotationX = -45f;
			}
			if (this.RotationY > 90f)
			{
				this.RotationY = 90f;
			}
			if (this.RotationY < -90f)
			{
				this.RotationY = -90f;
			}
			this.Zoom -= Input.GetAxis("Mouse ScrollWheel") * 10f;
			this.Zoom -= Input.GetAxis("Vertical") * 0.1f;
			if (this.Zoom > 60f)
			{
				this.Zoom = 60f;
			}
			else if (this.Zoom < 1f)
			{
				this.Zoom = 1f;
			}
			this.BinocularCamera.fieldOfView = Mathf.Lerp(this.BinocularCamera.fieldOfView, this.Zoom, Time.deltaTime * 10f);
			this.StreetManager.CurrentlyActiveJukebox.volume = this.BinocularCamera.fieldOfView / 60f * 0.5f;
			if (Input.GetButtonUp("B"))
			{
				this.BinocularCamera.gameObject.SetActive(false);
				this.BinocularRenderer.enabled = true;
				this.BinocularOverlay.SetActive(false);
				this.RotationX = 0f;
				this.RotationY = 0f;
				this.Zoom = 60f;
				this.StreetManager.CurrentlyActiveJukebox.volume = 0.5f;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Yandere.CanMove = true;
			}
		}
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x0015360C File Offset: 0x00151A0C
	private void UpdateShopInterface()
	{
		this.Yandere.MainCamera.GetComponent<RPG_Camera>().enabled = false;
		this.StreetShopInterface.StoreNameLabel.text = this.StoreName;
		this.StreetShopInterface.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
		this.StreetShopInterface.Shopkeeper.mainTexture = this.ShopkeeperPortraits[1];
		this.StreetShopInterface.SpeechBubbleLabel.text = this.ShopkeeperSpeeches[1];
		this.StreetShopInterface.ShopkeeperPortraits = this.ShopkeeperPortraits;
		this.StreetShopInterface.ShopkeeperSpeeches = this.ShopkeeperSpeeches;
		this.StreetShopInterface.ShopkeeperPosition = this.ShopkeeperPosition;
		this.StreetShopInterface.AdultProducts = this.AdultProducts;
		this.StreetShopInterface.SpeechPhase = 0;
		this.StreetShopInterface.Costs = this.Costs;
		this.StreetShopInterface.Limit = this.Limit;
		this.StreetShopInterface.Selected = 1;
		this.StreetShopInterface.Timer = 0f;
		this.StreetShopInterface.UpdateHighlight();
		for (int i = 1; i < 11; i++)
		{
			this.StreetShopInterface.ProductsLabel[i].text = this.Products[i];
			this.StreetShopInterface.PricesLabel[i].text = "$" + this.Costs[i];
			if (this.StreetShopInterface.PricesLabel[i].text == "$0")
			{
				this.StreetShopInterface.PricesLabel[i].text = string.Empty;
			}
			if (this.StoreType == ShopType.Salon)
			{
				this.StreetShopInterface.PricesLabel[i].text = "Free";
			}
		}
		this.StreetShopInterface.UpdateIcons();
	}

	// Token: 0x04002DA0 RID: 11680
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002DA1 RID: 11681
	public StreetManagerScript StreetManager;

	// Token: 0x04002DA2 RID: 11682
	public InputDeviceScript InputDevice;

	// Token: 0x04002DA3 RID: 11683
	public StalkerYandereScript Yandere;

	// Token: 0x04002DA4 RID: 11684
	public PromptBarScript PromptBar;

	// Token: 0x04002DA5 RID: 11685
	public HomeClockScript HomeClock;

	// Token: 0x04002DA6 RID: 11686
	public GameObject BinocularOverlay;

	// Token: 0x04002DA7 RID: 11687
	public Renderer BinocularRenderer;

	// Token: 0x04002DA8 RID: 11688
	public Camera BinocularCamera;

	// Token: 0x04002DA9 RID: 11689
	public AudioSource MyAudio;

	// Token: 0x04002DAA RID: 11690
	public AudioClip InsertCoin;

	// Token: 0x04002DAB RID: 11691
	public AudioClip Fail;

	// Token: 0x04002DAC RID: 11692
	public UILabel MyLabel;

	// Token: 0x04002DAD RID: 11693
	public Texture[] ShopkeeperPortraits;

	// Token: 0x04002DAE RID: 11694
	public string[] ShopkeeperSpeeches;

	// Token: 0x04002DAF RID: 11695
	public bool[] AdultProducts;

	// Token: 0x04002DB0 RID: 11696
	public string[] Products;

	// Token: 0x04002DB1 RID: 11697
	public float[] Costs;

	// Token: 0x04002DB2 RID: 11698
	public float RotationX;

	// Token: 0x04002DB3 RID: 11699
	public float RotationY;

	// Token: 0x04002DB4 RID: 11700
	public float Alpha;

	// Token: 0x04002DB5 RID: 11701
	public float Zoom;

	// Token: 0x04002DB6 RID: 11702
	public int ShopkeeperPosition = 500;

	// Token: 0x04002DB7 RID: 11703
	public int Limit;

	// Token: 0x04002DB8 RID: 11704
	public bool Binoculars;

	// Token: 0x04002DB9 RID: 11705
	public bool MaidCafe;

	// Token: 0x04002DBA RID: 11706
	public bool Exit;

	// Token: 0x04002DBB RID: 11707
	public string StoreName;

	// Token: 0x04002DBC RID: 11708
	public ShopType StoreType;
}
