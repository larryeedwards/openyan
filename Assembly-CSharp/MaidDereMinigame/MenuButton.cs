using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200015E RID: 350
	[RequireComponent(typeof(SpriteRenderer))]
	public class MenuButton : MonoBehaviour
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x00056F76 File Offset: 0x00055376
		public void Init()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00056F84 File Offset: 0x00055384
		private void OnMouseEnter()
		{
			this.menu.SetActiveMenuButton(this.index);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00056F98 File Offset: 0x00055398
		public void DoClick()
		{
			switch (this.buttonType)
			{
			case MenuButton.ButtonType.Start:
				this.menu.flipBook.FlipToPage(2);
				break;
			case MenuButton.ButtonType.Controls:
				this.menu.flipBook.FlipToPage(3);
				break;
			case MenuButton.ButtonType.Credits:
				this.menu.flipBook.FlipToPage(4);
				break;
			case MenuButton.ButtonType.Exit:
				this.menu.StopInputs();
				GameController.GoToExitScene(true);
				break;
			case MenuButton.ButtonType.Easy:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.easyVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				break;
			case MenuButton.ButtonType.Medium:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.mediumVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				break;
			case MenuButton.ButtonType.Hard:
				this.menu.StopInputs();
				GameController.Instance.activeDifficultyVariables = GameController.Instance.hardVariables;
				GameController.Instance.LoadScene(this.targetScene);
				SFXController.PlaySound(SFXController.Sounds.MenuConfirm);
				break;
			}
		}

		// Token: 0x04000887 RID: 2183
		public MenuButton.ButtonType buttonType;

		// Token: 0x04000888 RID: 2184
		public SceneObject targetScene;

		// Token: 0x04000889 RID: 2185
		[HideInInspector]
		public int index;

		// Token: 0x0400088A RID: 2186
		[HideInInspector]
		public Menu menu;

		// Token: 0x0400088B RID: 2187
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x0200015F RID: 351
		public enum ButtonType
		{
			// Token: 0x0400088D RID: 2189
			Start,
			// Token: 0x0400088E RID: 2190
			Controls,
			// Token: 0x0400088F RID: 2191
			Credits,
			// Token: 0x04000890 RID: 2192
			Exit,
			// Token: 0x04000891 RID: 2193
			Easy,
			// Token: 0x04000892 RID: 2194
			Medium,
			// Token: 0x04000893 RID: 2195
			Hard
		}
	}
}
