using System;
using UnityEngine;

// Token: 0x02000426 RID: 1062
public class HomeSenpaiShrineScript : MonoBehaviour
{
	// Token: 0x06001CC4 RID: 7364 RVA: 0x00108744 File Offset: 0x00106B44
	private void Start()
	{
		this.UpdateText(this.GetCurrentIndex());
		for (int i = 1; i < 11; i++)
		{
			if (PlayerGlobals.GetShrineCollectible(i))
			{
				this.Collectibles[i].SetActive(true);
			}
		}
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x00108789 File Offset: 0x00106B89
	private bool InUpperHalf()
	{
		return this.Y < 2;
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x00108794 File Offset: 0x00106B94
	private int GetCurrentIndex()
	{
		if (this.InUpperHalf())
		{
			return this.Y;
		}
		return 2 + (this.X + (this.Y - 2) * this.Columns);
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x001087C0 File Offset: 0x00106BC0
	private void Update()
	{
		if (!this.HomeYandere.CanMove && !this.PauseScreen.Show)
		{
			if (this.HomeCamera.ID == 6)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 135f, Time.deltaTime * 10f);
				this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
				this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, -this.Rotation, this.LeftDoor.localEulerAngles.z);
				if (this.InputManager.TappedUp)
				{
					this.Y = ((this.Y <= 0) ? (this.Rows - 1) : (this.Y - 1));
				}
				if (this.InputManager.TappedDown)
				{
					this.Y = ((this.Y >= this.Rows - 1) ? 0 : (this.Y + 1));
				}
				if (this.InputManager.TappedRight && !this.InUpperHalf())
				{
					this.X = ((this.X >= this.Columns - 1) ? 0 : (this.X + 1));
				}
				if (this.InputManager.TappedLeft && !this.InUpperHalf())
				{
					this.X = ((this.X <= 0) ? (this.Columns - 1) : (this.X - 1));
				}
				if (this.InUpperHalf())
				{
					this.X = 1;
				}
				int currentIndex = this.GetCurrentIndex();
				this.HomeCamera.Destination = this.Destinations[currentIndex];
				this.HomeCamera.Target = this.Targets[currentIndex];
				bool flag = this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft;
				if (flag)
				{
					this.UpdateText(currentIndex - 1);
				}
				if (Input.GetButtonDown("B"))
				{
					this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
					this.HomeCamera.Target = this.HomeCamera.Targets[0];
					this.HomeYandere.CanMove = true;
					this.HomeYandere.gameObject.SetActive(true);
					this.HomeWindow.Show = false;
				}
			}
		}
		else
		{
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
			this.RightDoor.localEulerAngles = new Vector3(this.RightDoor.localEulerAngles.x, this.Rotation, this.RightDoor.localEulerAngles.z);
			this.LeftDoor.localEulerAngles = new Vector3(this.LeftDoor.localEulerAngles.x, this.Rotation, this.LeftDoor.localEulerAngles.z);
		}
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x00108B28 File Offset: 0x00106F28
	private void UpdateText(int newIndex)
	{
		if (newIndex == -1)
		{
			newIndex = 10;
		}
		if (newIndex == 0)
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
		}
		else if (PlayerGlobals.GetShrineCollectible(newIndex))
		{
			this.NameLabel.text = this.Names[newIndex];
			this.DescLabel.text = this.Descs[newIndex];
		}
		else
		{
			this.NameLabel.text = "???";
			this.DescLabel.text = "I'd like to find something that Senpai touched and keep it here...";
		}
	}

	// Token: 0x04002264 RID: 8804
	public InputManagerScript InputManager;

	// Token: 0x04002265 RID: 8805
	public PauseScreenScript PauseScreen;

	// Token: 0x04002266 RID: 8806
	public HomeYandereScript HomeYandere;

	// Token: 0x04002267 RID: 8807
	public HomeCameraScript HomeCamera;

	// Token: 0x04002268 RID: 8808
	public HomeWindowScript HomeWindow;

	// Token: 0x04002269 RID: 8809
	public GameObject[] Collectibles;

	// Token: 0x0400226A RID: 8810
	public Transform[] Destinations;

	// Token: 0x0400226B RID: 8811
	public Transform[] Targets;

	// Token: 0x0400226C RID: 8812
	public Transform RightDoor;

	// Token: 0x0400226D RID: 8813
	public Transform LeftDoor;

	// Token: 0x0400226E RID: 8814
	public UILabel NameLabel;

	// Token: 0x0400226F RID: 8815
	public UILabel DescLabel;

	// Token: 0x04002270 RID: 8816
	public string[] Names;

	// Token: 0x04002271 RID: 8817
	public string[] Descs;

	// Token: 0x04002272 RID: 8818
	public float Rotation;

	// Token: 0x04002273 RID: 8819
	private int Rows = 5;

	// Token: 0x04002274 RID: 8820
	private int Columns = 3;

	// Token: 0x04002275 RID: 8821
	private int X = 1;

	// Token: 0x04002276 RID: 8822
	private int Y = 3;
}
