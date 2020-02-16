using System;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class MatchboxScript : MonoBehaviour
{
	// Token: 0x06001D97 RID: 7575 RVA: 0x0011832B File Offset: 0x0011672B
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x00118344 File Offset: 0x00116744
	private void Update()
	{
		if (!this.Prompt.PauseScreen.Show)
		{
			if (this.Yandere.PickUp == this.PickUp)
			{
				if (this.Prompt.HideButton[0])
				{
					this.Yandere.Arc.SetActive(true);
					this.Prompt.HideButton[0] = false;
					this.Prompt.HideButton[3] = true;
				}
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.Prompt.Circle[0].fillAmount = 1f;
					if (!this.Yandere.Chased && this.Yandere.Chasers == 0 && this.Yandere.CanMove && !this.Yandere.Flicking)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Match, base.transform.position, Quaternion.identity);
						gameObject.transform.parent = this.Yandere.ItemParent;
						gameObject.transform.localPosition = new Vector3(0.0159f, 0.0043f, 0.0152f);
						gameObject.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
						gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
						this.Yandere.Match = gameObject;
						this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_flickingMatch_00");
						this.Yandere.YandereVision = false;
						this.Yandere.Arc.SetActive(false);
						this.Yandere.Flicking = true;
						this.Yandere.CanMove = false;
						this.Prompt.Hide();
						this.Prompt.enabled = false;
					}
				}
			}
			else if (!this.Prompt.HideButton[0])
			{
				this.Yandere.Arc.SetActive(false);
				this.Prompt.HideButton[0] = true;
				this.Prompt.HideButton[3] = false;
			}
		}
	}

	// Token: 0x04002516 RID: 9494
	public YandereScript Yandere;

	// Token: 0x04002517 RID: 9495
	public PromptScript Prompt;

	// Token: 0x04002518 RID: 9496
	public PickUpScript PickUp;

	// Token: 0x04002519 RID: 9497
	public GameObject Match;
}
