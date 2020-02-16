using System;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class HomePantyChangerScript : MonoBehaviour
{
	// Token: 0x06001CB8 RID: 7352 RVA: 0x001069E4 File Offset: 0x00104DE4
	private void Start()
	{
		for (int i = 0; i < this.TotalPanties; i++)
		{
			this.NewPanties = UnityEngine.Object.Instantiate<GameObject>(this.PantyModels[i], new Vector3(base.transform.position.x, base.transform.position.y - 0.85f, base.transform.position.z - 1f), Quaternion.identity);
			this.NewPanties.transform.parent = this.PantyParent;
			this.NewPanties.GetComponent<HomePantiesScript>().PantyChanger = this;
			this.NewPanties.GetComponent<HomePantiesScript>().ID = i;
			this.PantyParent.transform.localEulerAngles = new Vector3(this.PantyParent.transform.localEulerAngles.x, this.PantyParent.transform.localEulerAngles.y + 360f / (float)this.TotalPanties, this.PantyParent.transform.localEulerAngles.z);
		}
		this.PantyParent.transform.localEulerAngles = new Vector3(this.PantyParent.transform.localEulerAngles.x, 0f, this.PantyParent.transform.localEulerAngles.z);
		this.PantyParent.transform.localPosition = new Vector3(this.PantyParent.transform.localPosition.x, this.PantyParent.transform.localPosition.y, 1.8f);
		this.UpdatePantyLabels();
		this.PantyParent.transform.localScale = Vector3.zero;
		this.PantyParent.gameObject.SetActive(false);
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x00106BD4 File Offset: 0x00104FD4
	private void Update()
	{
		if (this.HomeWindow.Show)
		{
			this.PantyParent.localScale = Vector3.Lerp(this.PantyParent.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.PantyParent.gameObject.SetActive(true);
			if (this.InputManager.TappedRight)
			{
				this.DestinationReached = false;
				this.TargetRotation += 360f / (float)this.TotalPanties;
				this.Selected++;
				if (this.Selected > this.TotalPanties - 1)
				{
					this.Selected = 0;
				}
				this.UpdatePantyLabels();
			}
			if (this.InputManager.TappedLeft)
			{
				this.DestinationReached = false;
				this.TargetRotation -= 360f / (float)this.TotalPanties;
				this.Selected--;
				if (this.Selected < 0)
				{
					this.Selected = this.TotalPanties - 1;
				}
				this.UpdatePantyLabels();
			}
			this.Rotation = Mathf.Lerp(this.Rotation, this.TargetRotation, Time.deltaTime * 10f);
			this.PantyParent.localEulerAngles = new Vector3(this.PantyParent.localEulerAngles.x, this.Rotation, this.PantyParent.localEulerAngles.z);
			if (Input.GetButtonDown("A"))
			{
				PlayerGlobals.PantiesEquipped = this.Selected;
				this.UpdatePantyLabels();
				Debug.Log("Yandere-chan should now be equipped with Panties #" + PlayerGlobals.PantiesEquipped);
			}
			if (Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeYandere.CanMove = true;
				this.HomeWindow.Show = false;
			}
		}
		else
		{
			this.PantyParent.localScale = Vector3.Lerp(this.PantyParent.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.PantyParent.localScale.x < 0.01f)
			{
				this.PantyParent.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x00106E48 File Offset: 0x00105248
	private void UpdatePantyLabels()
	{
		this.PantyNameLabel.text = this.PantyNames[this.Selected];
		this.PantyDescLabel.text = this.PantyDescs[this.Selected];
		this.PantyBuffLabel.text = this.PantyBuffs[this.Selected];
		this.ButtonLabel.text = ((this.Selected != PlayerGlobals.PantiesEquipped) ? "Wear" : "Equipped");
	}

	// Token: 0x04002201 RID: 8705
	public InputManagerScript InputManager;

	// Token: 0x04002202 RID: 8706
	public HomeYandereScript HomeYandere;

	// Token: 0x04002203 RID: 8707
	public HomeCameraScript HomeCamera;

	// Token: 0x04002204 RID: 8708
	public HomeWindowScript HomeWindow;

	// Token: 0x04002205 RID: 8709
	private GameObject NewPanties;

	// Token: 0x04002206 RID: 8710
	public UILabel PantyNameLabel;

	// Token: 0x04002207 RID: 8711
	public UILabel PantyDescLabel;

	// Token: 0x04002208 RID: 8712
	public UILabel PantyBuffLabel;

	// Token: 0x04002209 RID: 8713
	public UILabel ButtonLabel;

	// Token: 0x0400220A RID: 8714
	public Transform PantyParent;

	// Token: 0x0400220B RID: 8715
	public bool DestinationReached;

	// Token: 0x0400220C RID: 8716
	public float TargetRotation;

	// Token: 0x0400220D RID: 8717
	public float Rotation;

	// Token: 0x0400220E RID: 8718
	public int TotalPanties;

	// Token: 0x0400220F RID: 8719
	public int Selected;

	// Token: 0x04002210 RID: 8720
	public GameObject[] PantyModels;

	// Token: 0x04002211 RID: 8721
	public string[] PantyNames;

	// Token: 0x04002212 RID: 8722
	public string[] PantyDescs;

	// Token: 0x04002213 RID: 8723
	public string[] PantyBuffs;
}
