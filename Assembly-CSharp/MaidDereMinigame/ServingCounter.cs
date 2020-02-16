using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000149 RID: 329
	public class ServingCounter : MonoBehaviour
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x00055204 File Offset: 0x00053604
		private void Awake()
		{
			this.plates = new List<FoodInstance>();
			this.interactionIndicator.gameObject.SetActive(false);
			this.interactionIndicatorStartingPos = this.interactionIndicator.transform.position;
			this.platePositions = new List<Transform>();
			this.kitchenModeHide.gameObject.SetActive(false);
			FoodMenu.Instance.gameObject.SetActive(false);
			for (int i = 0; i < this.maxPlates; i++)
			{
				Transform transform = new GameObject("Position " + i).transform;
				transform.parent = base.transform;
				transform.position = new Vector3(this.xPosStart - this.plateSeparation * (float)i, this.yPos, 0f);
				this.platePositions.Add(transform);
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000552DF File Offset: 0x000536DF
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00055301 File Offset: 0x00053701
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.SetPause));
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00055324 File Offset: 0x00053724
		private void Update()
		{
			switch (this.state)
			{
			case ServingCounter.KitchenState.None:
				if (this.isPaused)
				{
					return;
				}
				if (this.interactionRange && Input.GetButtonDown("A"))
				{
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					this.selectedIndex = ((this.plates.Count != 0) ? 0 : 2);
					this.kitchenModeHide.gameObject.SetActive(true);
					this.SetMask(this.selectedIndex);
					SFXController.PlaySound(SFXController.Sounds.MenuOpen);
					if (this.plates.Count == 0 && YandereController.Instance.heldItem == null)
					{
						this.interactionIndicator.transform.position = Chef.Instance.transform.position + Vector3.up * 0.8f;
						InteractionMenu.SetAButton(InteractionMenu.AButtonText.PlaceOrder);
						this.state = ServingCounter.KitchenState.Chef;
						FoodMenu.Instance.gameObject.SetActive(true);
					}
					GameController.SetPause(true);
					InteractionMenu.SetBButton(true);
				}
				break;
			case ServingCounter.KitchenState.SelectingInteraction:
			{
				int num = this.selectedIndex;
				if (num != 0)
				{
					if (num != 1)
					{
						if (num == 2)
						{
							this.interactionIndicator.transform.position = Chef.Instance.transform.position + Vector3.up * 0.8f;
							InteractionMenu.SetAButton(InteractionMenu.AButtonText.PlaceOrder);
							this.SetMask(this.selectedIndex);
							if (Input.GetButtonDown("A"))
							{
								this.state = ServingCounter.KitchenState.Chef;
								InteractionMenu.SetAButton(InteractionMenu.AButtonText.PlaceOrder);
								FoodMenu.Instance.gameObject.SetActive(true);
								SFXController.PlaySound(SFXController.Sounds.MenuOpen);
							}
						}
					}
					else
					{
						this.interactionIndicator.transform.position = this.trash.transform.position + Vector3.up * 0.5f;
						InteractionMenu.SetAButton(InteractionMenu.AButtonText.TossPlate);
						this.SetMask(this.selectedIndex);
						if (Input.GetButtonDown("A"))
						{
							this.state = ServingCounter.KitchenState.Trash;
							SFXController.PlaySound(SFXController.Sounds.MenuOpen);
						}
					}
				}
				else
				{
					this.interactionIndicator.transform.position = this.interactionIndicatorStartingPos;
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.ChoosePlate);
					this.SetMask(this.selectedIndex);
					if (Input.GetButtonDown("A"))
					{
						this.state = ServingCounter.KitchenState.Plates;
						this.selectedIndex = 0;
						InteractionMenu.SetAButton(InteractionMenu.AButtonText.GrabPlate);
						SFXController.PlaySound(SFXController.Sounds.MenuOpen);
					}
				}
				if (Input.GetButtonDown("B"))
				{
					InteractionMenu.SetBButton(false);
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.KitchenMenu);
					this.state = ServingCounter.KitchenState.None;
					GameController.SetPause(false);
					this.kitchenModeHide.gameObject.SetActive(false);
					this.interactionIndicator.transform.position = this.interactionIndicatorStartingPos;
					SFXController.PlaySound(SFXController.Sounds.MenuBack);
				}
				if (YandereController.rightButton)
				{
					this.selectedIndex = (this.selectedIndex + 1) % 3;
					if (this.selectedIndex == 0 && this.plates.Count == 0)
					{
						this.selectedIndex = (this.selectedIndex + 1) % 3;
					}
					if (this.selectedIndex == 1 && YandereController.Instance.heldItem == null)
					{
						this.selectedIndex = (this.selectedIndex + 1) % 3;
					}
					SFXController.PlaySound(SFXController.Sounds.MenuSelect);
				}
				if (YandereController.leftButton)
				{
					if (this.selectedIndex == 0)
					{
						this.selectedIndex = 2;
					}
					else
					{
						this.selectedIndex--;
					}
					if (this.selectedIndex == 1 && YandereController.Instance.heldItem == null)
					{
						this.selectedIndex--;
					}
					if (this.selectedIndex == 0 && this.plates.Count == 0)
					{
						this.selectedIndex = 2;
					}
					SFXController.PlaySound(SFXController.Sounds.MenuSelect);
				}
				break;
			}
			case ServingCounter.KitchenState.Plates:
				this.interactionIndicator.gameObject.SetActive(true);
				this.interactionIndicator.transform.position = this.plates[this.selectedIndex].transform.position + Vector3.up * 0.25f;
				this.SetMask(3);
				this.plateMask.transform.position = this.plates[this.selectedIndex].transform.position + Vector3.up * 0.05f;
				if (YandereController.rightButton)
				{
					if (this.selectedIndex == 0)
					{
						this.selectedIndex = this.plates.Count - 1;
					}
					else
					{
						this.selectedIndex--;
					}
					SFXController.PlaySound(SFXController.Sounds.MenuSelect);
				}
				else if (YandereController.leftButton)
				{
					this.selectedIndex = (this.selectedIndex + 1) % this.plates.Count;
					SFXController.PlaySound(SFXController.Sounds.MenuSelect);
				}
				if (Input.GetButtonDown("A") && YandereController.Instance.heldItem == null)
				{
					YandereController.Instance.PickUpTray(this.plates[this.selectedIndex].food);
					this.RemovePlate(this.selectedIndex);
					this.selectedIndex = 2;
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					SFXController.PlaySound(SFXController.Sounds.MenuOpen);
				}
				if (Input.GetButtonDown("B"))
				{
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					SFXController.PlaySound(SFXController.Sounds.MenuBack);
				}
				break;
			case ServingCounter.KitchenState.Chef:
				if (Input.GetButtonDown("B"))
				{
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					FoodMenu.Instance.gameObject.SetActive(false);
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					SFXController.PlaySound(SFXController.Sounds.MenuBack);
				}
				if (Input.GetButtonDown("A"))
				{
					this.state = ServingCounter.KitchenState.SelectingInteraction;
					Chef.AddToQueue(FoodMenu.Instance.GetActiveFood());
					FoodMenu.Instance.gameObject.SetActive(false);
					SFXController.PlaySound(SFXController.Sounds.MenuOpen);
				}
				break;
			case ServingCounter.KitchenState.Trash:
				YandereController.Instance.DropTray();
				this.state = ServingCounter.KitchenState.SelectingInteraction;
				this.selectedIndex = 2;
				break;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00055924 File Offset: 0x00053D24
		public void SetMask(int position)
		{
			this.counterMask.gameObject.SetActive(position == 0);
			this.trashMask.gameObject.SetActive(position == 1);
			this.chefMask.gameObject.SetActive(position == 2);
			this.plateMask.gameObject.SetActive(position == 3);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00055984 File Offset: 0x00053D84
		public void AddPlate(Food food)
		{
			if (this.plates.Count >= this.maxPlates)
			{
				this.RemovePlate(this.maxPlates - 1);
				this.selectedIndex--;
			}
			for (int i = 0; i < this.plates.Count; i++)
			{
				FoodInstance foodInstance = this.plates[i];
				foodInstance.transform.parent = this.platePositions[i + 1];
				foodInstance.transform.localPosition = Vector3.zero;
			}
			SFXController.PlaySound(SFXController.Sounds.Plate);
			FoodInstance foodInstance2 = UnityEngine.Object.Instantiate<FoodInstance>(this.platePrefab);
			foodInstance2.transform.parent = this.platePositions[0];
			foodInstance2.transform.localPosition = Vector3.zero;
			foodInstance2.food = food;
			this.plates.Insert(0, foodInstance2);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00055A64 File Offset: 0x00053E64
		public void RemovePlate(int index)
		{
			FoodInstance foodInstance = this.plates[index];
			this.plates.RemoveAt(index);
			UnityEngine.Object.Destroy(foodInstance.gameObject);
			for (int i = index; i < this.plates.Count; i++)
			{
				FoodInstance foodInstance2 = this.plates[i];
				foodInstance2.transform.parent = this.platePositions[i];
				foodInstance2.transform.localPosition = Vector3.zero;
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00055AE5 File Offset: 0x00053EE5
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00055AEE File Offset: 0x00053EEE
		private void OnTriggerEnter2D(Collider2D collision)
		{
			this.interactionIndicator.gameObject.SetActive(true);
			this.interactionIndicator.transform.position = this.interactionIndicatorStartingPos;
			this.interactionRange = true;
			InteractionMenu.SetAButton(InteractionMenu.AButtonText.KitchenMenu);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00055B24 File Offset: 0x00053F24
		private void OnTriggerExit2D(Collider2D collision)
		{
			this.interactionIndicator.gameObject.SetActive(false);
			this.interactionRange = false;
			InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
		}

		// Token: 0x04000816 RID: 2070
		public FoodInstance platePrefab;

		// Token: 0x04000817 RID: 2071
		public GameObject trash;

		// Token: 0x04000818 RID: 2072
		public SpriteRenderer interactionIndicator;

		// Token: 0x04000819 RID: 2073
		public SpriteRenderer kitchenModeHide;

		// Token: 0x0400081A RID: 2074
		public SpriteMask chefMask;

		// Token: 0x0400081B RID: 2075
		public SpriteMask trashMask;

		// Token: 0x0400081C RID: 2076
		public SpriteMask counterMask;

		// Token: 0x0400081D RID: 2077
		public SpriteMask plateMask;

		// Token: 0x0400081E RID: 2078
		public int maxPlates = 7;

		// Token: 0x0400081F RID: 2079
		public float plateSeparation = 0.214f;

		// Token: 0x04000820 RID: 2080
		public float yPos = -1.328f;

		// Token: 0x04000821 RID: 2081
		public float xPosStart = 2.812f;

		// Token: 0x04000822 RID: 2082
		private ServingCounter.KitchenState state;

		// Token: 0x04000823 RID: 2083
		private List<FoodInstance> plates;

		// Token: 0x04000824 RID: 2084
		private List<Transform> platePositions;

		// Token: 0x04000825 RID: 2085
		private Vector3 interactionIndicatorStartingPos;

		// Token: 0x04000826 RID: 2086
		private int selectedIndex;

		// Token: 0x04000827 RID: 2087
		private bool interactionRange;

		// Token: 0x04000828 RID: 2088
		private bool interacting;

		// Token: 0x04000829 RID: 2089
		private bool isPaused;

		// Token: 0x0200014A RID: 330
		public enum KitchenState
		{
			// Token: 0x0400082B RID: 2091
			None,
			// Token: 0x0400082C RID: 2092
			SelectingInteraction,
			// Token: 0x0400082D RID: 2093
			Plates,
			// Token: 0x0400082E RID: 2094
			Chef,
			// Token: 0x0400082F RID: 2095
			Trash
		}
	}
}
