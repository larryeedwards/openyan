using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000169 RID: 361
	public class Timer : Meter
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x00057B83 File Offset: 0x00055F83
		private void Awake()
		{
			this.gameTime = GameController.Instance.activeDifficultyVariables.gameTime;
			this.starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			this.isPaused = true;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00057BAC File Offset: 0x00055FAC
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00057BCE File Offset: 0x00055FCE
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00057BF0 File Offset: 0x00055FF0
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00057BFC File Offset: 0x00055FFC
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			this.gameTime -= Time.deltaTime;
			base.SetFill(this.gameTime / GameController.Instance.activeDifficultyVariables.gameTime);
			this.starter.SetGameTime(this.gameTime);
		}

		// Token: 0x040008BB RID: 2235
		private GameStarter starter;

		// Token: 0x040008BC RID: 2236
		private float gameTime;

		// Token: 0x040008BD RID: 2237
		private bool isPaused;
	}
}
