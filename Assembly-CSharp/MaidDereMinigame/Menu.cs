using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200015D RID: 349
	public class Menu : MonoBehaviour
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00056D00 File Offset: 0x00055100
		private void Start()
		{
			for (int i = 0; i < this.mainMenuButtons.Count; i++)
			{
				int index = i;
				this.mainMenuButtons[i].Init();
				this.mainMenuButtons[i].index = index;
				this.mainMenuButtons[i].spriteRenderer.enabled = false;
				this.mainMenuButtons[i].menu = this;
			}
			this.flipBook = FlipBook.Instance;
			this.SetActiveMenuButton(0);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00056D8C File Offset: 0x0005518C
		private void Update()
		{
			if (this.cancelInputs)
			{
				return;
			}
			if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("A")) && this.activeMenuButton != null)
			{
				this.activeMenuButton.DoClick();
			}
			float num = Input.GetAxisRaw("Vertical") * -1f;
			if (Input.GetKeyDown("up") || Input.GetAxis("DpadY") > 0.5f)
			{
				num = -1f;
			}
			else if (Input.GetKeyDown("down") || Input.GetAxis("DpadY") < -0.5f)
			{
				num = 1f;
			}
			if (num != 0f && this.PreviousFrameVertical == 0f)
			{
				SFXController.PlaySound(SFXController.Sounds.MenuSelect);
			}
			this.PreviousFrameVertical = num;
			if (num != 0f)
			{
				if (!this.prevVertical)
				{
					this.prevVertical = true;
					if (num < 0f)
					{
						int num2 = this.mainMenuButtons.IndexOf(this.activeMenuButton);
						if (num2 == 0)
						{
							this.SetActiveMenuButton(this.mainMenuButtons.Count - 1);
						}
						else
						{
							this.SetActiveMenuButton(num2 - 1);
						}
					}
					else
					{
						int num3 = this.mainMenuButtons.IndexOf(this.activeMenuButton);
						this.SetActiveMenuButton((num3 + 1) % this.mainMenuButtons.Count);
					}
				}
			}
			else
			{
				this.prevVertical = false;
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00056F08 File Offset: 0x00055308
		public void SetActiveMenuButton(int index)
		{
			if (this.activeMenuButton != null)
			{
				this.activeMenuButton.spriteRenderer.enabled = false;
			}
			this.activeMenuButton = this.mainMenuButtons[index];
			this.activeMenuButton.spriteRenderer.enabled = true;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00056F5A File Offset: 0x0005535A
		public void StopInputs()
		{
			this.cancelInputs = true;
			this.flipBook.StopInputs();
		}

		// Token: 0x04000881 RID: 2177
		public List<MenuButton> mainMenuButtons;

		// Token: 0x04000882 RID: 2178
		[HideInInspector]
		public FlipBook flipBook;

		// Token: 0x04000883 RID: 2179
		private MenuButton activeMenuButton;

		// Token: 0x04000884 RID: 2180
		private bool prevVertical;

		// Token: 0x04000885 RID: 2181
		private bool cancelInputs;

		// Token: 0x04000886 RID: 2182
		private float PreviousFrameVertical;
	}
}
