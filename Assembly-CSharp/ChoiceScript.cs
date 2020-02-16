using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000364 RID: 868
public class ChoiceScript : MonoBehaviour
{
	// Token: 0x060017D3 RID: 6099 RVA: 0x000BE249 File Offset: 0x000BC649
	private void Start()
	{
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x000BE270 File Offset: 0x000BC670
	private void Update()
	{
		this.Highlight.transform.localPosition = Vector3.Lerp(this.Highlight.transform.localPosition, new Vector3((float)(-360 + 720 * this.Selected), this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z), Time.deltaTime * 10f);
		if (this.Phase == 0)
		{
			this.Darkness.color = new Color(1f, 1f, 1f, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 0f)
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 1)
		{
			if (this.InputManager.TappedLeft)
			{
				this.Darkness.color = new Color(1f, 1f, 1f, 0f);
				this.Selected = 0;
			}
			else if (this.InputManager.TappedRight)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.Selected = 1;
			}
			if (Input.GetButtonDown("A"))
			{
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime * 2f));
			if (this.Darkness.color.a == 1f)
			{
				GameGlobals.LoveSick = (this.Selected == 1);
				SceneManager.LoadScene("TitleScene");
			}
		}
	}

	// Token: 0x040017F6 RID: 6134
	public InputManagerScript InputManager;

	// Token: 0x040017F7 RID: 6135
	public PromptBarScript PromptBar;

	// Token: 0x040017F8 RID: 6136
	public Transform Highlight;

	// Token: 0x040017F9 RID: 6137
	public UISprite Darkness;

	// Token: 0x040017FA RID: 6138
	public int Selected;

	// Token: 0x040017FB RID: 6139
	public int Phase;
}
