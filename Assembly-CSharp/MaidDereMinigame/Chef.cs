using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200013E RID: 318
	[RequireComponent(typeof(Animator))]
	public class Chef : MonoBehaviour
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00053C71 File Offset: 0x00052071
		public static Chef Instance
		{
			get
			{
				if (Chef.instance == null)
				{
					Chef.instance = UnityEngine.Object.FindObjectOfType<Chef>();
				}
				return Chef.instance;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00053C92 File Offset: 0x00052092
		private void Awake()
		{
			this.cookQueue = new Foods();
			this.animator = base.GetComponent<Animator>();
			this.cookMeter.gameObject.SetActive(false);
			this.isPaused = true;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00053CC3 File Offset: 0x000520C3
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00053CE5 File Offset: 0x000520E5
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00053D07 File Offset: 0x00052107
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			this.animator.speed = (float)((!this.isPaused) ? 1 : 0);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00053D2E File Offset: 0x0005212E
		public static void AddToQueue(Food foodItem)
		{
			Chef.Instance.cookQueue.Add(foodItem);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00053D40 File Offset: 0x00052140
		public static Food GrabFromQueue()
		{
			Food result = Chef.Instance.cookQueue[0];
			Chef.Instance.cookQueue.RemoveAt(0);
			return result;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00053D70 File Offset: 0x00052170
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			Chef.ChefState chefState = this.state;
			if (chefState != Chef.ChefState.Queueing)
			{
				if (chefState == Chef.ChefState.Cooking)
				{
					if (this.timeToFinishDish <= 0f)
					{
						this.state = Chef.ChefState.Delivering;
						this.animator.SetTrigger("PlateCooked");
						this.cookMeter.gameObject.SetActive(false);
					}
					else
					{
						this.timeToFinishDish -= Time.deltaTime;
						this.cookMeter.SetFill(1f - this.timeToFinishDish / (this.currentPlate.cookTimeMultiplier * this.cookTime));
					}
				}
			}
			else if (this.cookQueue.Count > 0)
			{
				this.currentPlate = Chef.GrabFromQueue();
				this.timeToFinishDish = this.currentPlate.cookTimeMultiplier * this.cookTime;
				this.state = Chef.ChefState.Cooking;
				this.cookMeter.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00053E71 File Offset: 0x00052271
		public void Deliver()
		{
			UnityEngine.Object.FindObjectOfType<ServingCounter>().AddPlate(this.currentPlate);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00053E83 File Offset: 0x00052283
		public void Queue()
		{
			this.state = Chef.ChefState.Queueing;
		}

		// Token: 0x040007D3 RID: 2003
		private static Chef instance;

		// Token: 0x040007D4 RID: 2004
		[Reorderable]
		public Foods cookQueue;

		// Token: 0x040007D5 RID: 2005
		public FoodMenu foodMenu;

		// Token: 0x040007D6 RID: 2006
		public Meter cookMeter;

		// Token: 0x040007D7 RID: 2007
		public float cookTime = 3f;

		// Token: 0x040007D8 RID: 2008
		private Chef.ChefState state;

		// Token: 0x040007D9 RID: 2009
		private Food currentPlate;

		// Token: 0x040007DA RID: 2010
		private Animator animator;

		// Token: 0x040007DB RID: 2011
		private float timeToFinishDish;

		// Token: 0x040007DC RID: 2012
		private bool isPaused;

		// Token: 0x0200013F RID: 319
		public enum ChefState
		{
			// Token: 0x040007DE RID: 2014
			Queueing,
			// Token: 0x040007DF RID: 2015
			Cooking,
			// Token: 0x040007E0 RID: 2016
			Delivering
		}
	}
}
