using System;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public class TitleSaveFilesScript : MonoBehaviour
{
	// Token: 0x0600219C RID: 8604 RVA: 0x00197584 File Offset: 0x00195984
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x001975D4 File Offset: 0x001959D4
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			if (!this.ConfirmationWindow.activeInHierarchy)
			{
				if (this.InputManager.TappedDown)
				{
					this.ID++;
					if (this.ID > 3)
					{
						this.ID = 1;
					}
					this.UpdateHighlight();
				}
				if (this.InputManager.TappedUp)
				{
					this.ID--;
					if (this.ID < 1)
					{
						this.ID = 3;
					}
					this.UpdateHighlight();
				}
			}
			if (base.transform.localPosition.x < 50f)
			{
				if (!this.ConfirmationWindow.activeInHierarchy)
				{
					if (Input.GetButtonDown("A"))
					{
						GameGlobals.Profile = this.ID;
						Globals.DeleteAll();
						GameGlobals.Profile = this.ID;
						this.Menu.FadeOut = true;
						this.Menu.Fading = true;
					}
					else if (Input.GetButtonDown("X"))
					{
						this.ConfirmationWindow.SetActive(true);
					}
				}
				else if (Input.GetButtonDown("A"))
				{
					PlayerPrefs.SetInt("ProfileCreated_" + this.ID, 0);
					this.ConfirmationWindow.SetActive(false);
					this.SaveDatas[this.ID].Start();
				}
				else if (Input.GetButtonDown("B"))
				{
					this.ConfirmationWindow.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x0019782A File Offset: 0x00195C2A
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(0f, 700f - 350f * (float)this.ID, 0f);
	}

	// Token: 0x0400367C RID: 13948
	public InputManagerScript InputManager;

	// Token: 0x0400367D RID: 13949
	public TitleSaveDataScript[] SaveDatas;

	// Token: 0x0400367E RID: 13950
	public GameObject ConfirmationWindow;

	// Token: 0x0400367F RID: 13951
	public TitleMenuScript Menu;

	// Token: 0x04003680 RID: 13952
	public Transform Highlight;

	// Token: 0x04003681 RID: 13953
	public bool Show;

	// Token: 0x04003682 RID: 13954
	public int ID = 1;
}
