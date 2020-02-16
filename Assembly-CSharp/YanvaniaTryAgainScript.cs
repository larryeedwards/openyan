using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005B2 RID: 1458
public class YanvaniaTryAgainScript : MonoBehaviour
{
	// Token: 0x0600232D RID: 9005 RVA: 0x001BB7FC File Offset: 0x001B9BFC
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x001BB810 File Offset: 0x001B9C10
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (base.transform.localScale.x > 0.9f)
			{
				if (this.InputManager.TappedLeft)
				{
					this.Selected = 1;
				}
				if (this.InputManager.TappedRight)
				{
					this.Selected = 2;
				}
				if (this.Selected == 1)
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, -1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, 100f, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
					this.Highlight.localScale = new Vector3(Mathf.Lerp(this.Highlight.localScale.x, 1f, Time.deltaTime * 10f), this.Highlight.localScale.y, this.Highlight.localScale.z);
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
					gameObject.transform.parent = this.Highlight;
					gameObject.transform.localPosition = Vector3.zero;
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					SceneManager.LoadScene("YanvaniaTitleScene");
				}
			}
		}
	}

	// Token: 0x04003C7F RID: 15487
	public InputManagerScript InputManager;

	// Token: 0x04003C80 RID: 15488
	public GameObject ButtonEffect;

	// Token: 0x04003C81 RID: 15489
	public Transform Highlight;

	// Token: 0x04003C82 RID: 15490
	public UISprite Darkness;

	// Token: 0x04003C83 RID: 15491
	public bool FadeOut;

	// Token: 0x04003C84 RID: 15492
	public int Selected = 1;
}
