using System;
using UnityEngine;

// Token: 0x0200041A RID: 1050
public class HomeCorkboardScript : MonoBehaviour
{
	// Token: 0x06001C96 RID: 7318 RVA: 0x00102E28 File Offset: 0x00101228
	private void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.Loaded)
			{
				this.PhotoGallery.LoadingScreen.SetActive(false);
				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotoGallery.enabled = true;
				this.PhotoGallery.gameObject.SetActive(true);
				this.Loaded = true;
			}
			if (!this.PhotoGallery.Adjusting && !this.PhotoGallery.Viewing && !this.PhotoGallery.LoadingScreen.activeInHierarchy && Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeCamera.CorkboardLabel.SetActive(true);
				this.PhotoGallery.PromptBar.Show = false;
				this.PhotoGallery.enabled = false;
				this.HomeYandere.CanMove = true;
				this.HomeYandere.gameObject.SetActive(true);
				this.HomeWindow.Show = false;
				base.enabled = false;
				this.Loaded = false;
				this.PhotoGallery.SaveAllPhotographs();
				this.PhotoGallery.SaveAllStrings();
			}
		}
	}

	// Token: 0x0400217F RID: 8575
	public InputManagerScript InputManager;

	// Token: 0x04002180 RID: 8576
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04002181 RID: 8577
	public HomeYandereScript HomeYandere;

	// Token: 0x04002182 RID: 8578
	public HomeCameraScript HomeCamera;

	// Token: 0x04002183 RID: 8579
	public HomeWindowScript HomeWindow;

	// Token: 0x04002184 RID: 8580
	public bool Loaded;
}
