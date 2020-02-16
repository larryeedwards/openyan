using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000142 RID: 322
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class AIController : AIMover
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x000541EC File Offset: 0x000525EC
		public void Init()
		{
			this.animator = base.GetComponent<Animator>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.throbObject.SetActive(false);
			this.targetChair = Chair.RandomChair;
			this.collider2d = base.GetComponent<Collider2D>();
			this.collider2d.enabled = false;
			if (this.targetChair == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.happinessMeter.gameObject.SetActive(false);
			this.speechBubble.gameObject.SetActive(false);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00054280 File Offset: 0x00052680
		private void Start()
		{
			this.leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
			this.moveSpeed = GameController.Instance.activeDifficultyVariables.customerMoveSpeed;
			this.timeToOrder = GameController.Instance.activeDifficultyVariables.timeSpentOrdering;
			this.eatTime = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;
			this.patienceDegradation = GameController.Instance.activeDifficultyVariables.customerPatienceDegradation;
			this.timeToEat = GameController.Instance.activeDifficultyVariables.timeSpentEatingFood;
			SFXController.PlaySound(SFXController.Sounds.DoorBell);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0005430C File Offset: 0x0005270C
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0005432E File Offset: 0x0005272E
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00054350 File Offset: 0x00052750
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			base.GetComponent<Animator>().speed = (float)((!this.isPaused) ? 1 : 0);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00054378 File Offset: 0x00052778
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			switch (this.state)
			{
			case AIController.AIState.Entering:
				if (Mathf.Abs(base.transform.position.x - this.targetChair.transform.position.x) <= this.distanceThreshold)
				{
					this.SitDown();
					this.happiness = 100f;
					this.happinessMeter.SetFill(this.happiness / 100f);
					this.state = AIController.AIState.Menu;
				}
				break;
			case AIController.AIState.Menu:
				if (this.happiness <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
					GameController.AddAngryCustomer();
				}
				else
				{
					this.ReduceHappiness();
				}
				break;
			case AIController.AIState.Ordering:
				if (this.orderTime <= 0f)
				{
					this.state = AIController.AIState.Waiting;
					this.speechBubble.GetComponent<Animator>().SetTrigger("BubbleDrop");
					this.animator.SetTrigger("DoneOrdering");
				}
				else
				{
					this.orderTime -= Time.deltaTime;
				}
				break;
			case AIController.AIState.Waiting:
				if (this.happiness <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
					GameController.AddAngryCustomer();
				}
				else
				{
					this.ReduceHappiness();
				}
				break;
			case AIController.AIState.Eating:
				if (this.eatTime <= 0f)
				{
					this.StandUp();
					this.state = AIController.AIState.Leaving;
				}
				else
				{
					this.eatTime -= Time.deltaTime;
				}
				break;
			case AIController.AIState.Leaving:
				if (Mathf.Abs(base.transform.position.x - this.leaveTarget.position.x) <= this.distanceThreshold)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					this.leaveTarget.GetComponent<CustomerSpawner>().OpenDoor();
				}
				break;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00054578 File Offset: 0x00052978
		public override ControlInput GetInput()
		{
			ControlInput result = default(ControlInput);
			if (this.isPaused)
			{
				return result;
			}
			AIController.AIState aistate = this.state;
			if (aistate != AIController.AIState.Entering)
			{
				if (aistate == AIController.AIState.Leaving)
				{
					if (this.leaveTarget.position.x > base.transform.position.x)
					{
						result.horizontal = 1f;
						this.SetFlip(false);
					}
					else
					{
						result.horizontal = -1f;
						this.SetFlip(true);
					}
				}
			}
			else if (this.targetChair.transform.position.x > base.transform.position.x)
			{
				result.horizontal = 1f;
				this.SetFlip(false);
			}
			else
			{
				result.horizontal = -1f;
				this.SetFlip(true);
			}
			return result;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00054674 File Offset: 0x00052A74
		public void TakeOrder()
		{
			this.state = AIController.AIState.Ordering;
			this.happiness = 100f;
			this.happinessMeter.SetFill(this.happiness / 100f);
			this.orderTime = this.timeToOrder;
			this.animator.SetTrigger("OrderTaken");
			this.animator.SetFloat("Happiness", this.happiness);
			this.desiredFood = FoodMenu.Instance.GetRandomFood();
			this.speechBubble.gameObject.SetActive(true);
			this.speechBubble.food = this.desiredFood;
			if (this.Male)
			{
				SFXController.PlaySound(SFXController.Sounds.MaleCustomerGreet);
			}
			else
			{
				SFXController.PlaySound(SFXController.Sounds.FemaleCustomerGreet);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0005472C File Offset: 0x00052B2C
		public void DeliverFood(Food deliveredFood)
		{
			if (deliveredFood.name == this.desiredFood.name)
			{
				this.state = AIController.AIState.Eating;
				this.animator.SetTrigger("ServedFood");
				this.eatTime = this.timeToEat;
				GameController.AddTip(GameController.Instance.activeDifficultyVariables.baseTip * this.happiness);
				if (this.happiness <= 50f)
				{
					this.happiness = 50f;
					this.animator.SetFloat("Happiness", this.happiness);
				}
				if (this.Male)
				{
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerThank);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerThank);
				}
			}
			else
			{
				this.state = AIController.AIState.Leaving;
				this.happiness = 0f;
				this.animator.SetFloat("Happiness", this.happiness);
				GameController.AddAngryCustomer();
				this.StandUp();
				if (this.Male)
				{
					SFXController.PlaySound(SFXController.Sounds.MaleCustomerLeave);
				}
				else
				{
					SFXController.PlaySound(SFXController.Sounds.FemaleCustomerLeave);
				}
			}
			this.happinessMeter.gameObject.SetActive(false);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00054848 File Offset: 0x00052C48
		private void SitDown()
		{
			base.transform.position = new Vector3(this.targetChair.transform.position.x, base.transform.position.y, base.transform.position.z);
			this.animator.SetTrigger("SitDown");
			this.SetFlip(this.targetChair.transform.localScale.x <= 0f);
			this.SetSortingLayer(true);
			this.collider2d.enabled = true;
			this.happinessMeter.gameObject.SetActive(true);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00054908 File Offset: 0x00052D08
		private void StandUp()
		{
			this.animator.SetTrigger("StandUp");
			this.SetSortingLayer(false);
			this.targetChair.available = true;
			this.collider2d.enabled = false;
			this.happinessMeter.gameObject.SetActive(false);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00054958 File Offset: 0x00052D58
		private void ReduceHappiness()
		{
			this.happiness -= Time.deltaTime * this.patienceDegradation;
			this.animator.SetFloat("Happiness", this.happiness);
			this.happinessMeter.SetFill(this.happiness / 100f);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000549AB File Offset: 0x00052DAB
		private void SetFlip(bool flip)
		{
			this.spriteRenderer.flipX = flip;
			base.GetComponentInChildren<CharacterHairPlacer>().hairInstance.flipX = flip;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000549CC File Offset: 0x00052DCC
		public void SetSortingLayer(bool back)
		{
			this.spriteRenderer.sortingLayerName = ((!back) ? "Default" : "CustomerSitting");
			base.GetComponent<CharacterHairPlacer>().hairInstance.sortingLayerName = ((!back) ? "Default" : "CustomerSitting");
			this.throbObject.GetComponent<SpriteRenderer>().sortingLayerName = ((!back) ? "Default" : "CustomerSitting");
		}

		// Token: 0x040007E9 RID: 2025
		public GameObject throbObject;

		// Token: 0x040007EA RID: 2026
		public Meter happinessMeter;

		// Token: 0x040007EB RID: 2027
		public Bubble speechBubble;

		// Token: 0x040007EC RID: 2028
		public float distanceThreshold = 0.5f;

		// Token: 0x040007ED RID: 2029
		private Food desiredFood;

		// Token: 0x040007EE RID: 2030
		private Collider2D collider2d;

		// Token: 0x040007EF RID: 2031
		private Chair targetChair;

		// Token: 0x040007F0 RID: 2032
		[HideInInspector]
		public Transform leaveTarget;

		// Token: 0x040007F1 RID: 2033
		[HideInInspector]
		public AIController.AIState state;

		// Token: 0x040007F2 RID: 2034
		private Animator animator;

		// Token: 0x040007F3 RID: 2035
		private SpriteRenderer spriteRenderer;

		// Token: 0x040007F4 RID: 2036
		private float patienceDegradation = 2f;

		// Token: 0x040007F5 RID: 2037
		private float timeToOrder = 0.5f;

		// Token: 0x040007F6 RID: 2038
		private float timeToEat;

		// Token: 0x040007F7 RID: 2039
		private float happiness = 50f;

		// Token: 0x040007F8 RID: 2040
		private float orderTime;

		// Token: 0x040007F9 RID: 2041
		private float eatTime;

		// Token: 0x040007FA RID: 2042
		private int normalSortingLayer;

		// Token: 0x040007FB RID: 2043
		private bool isPaused;

		// Token: 0x040007FC RID: 2044
		public bool Male;

		// Token: 0x02000143 RID: 323
		public enum AIState
		{
			// Token: 0x040007FE RID: 2046
			Entering,
			// Token: 0x040007FF RID: 2047
			Menu,
			// Token: 0x04000800 RID: 2048
			Ordering,
			// Token: 0x04000801 RID: 2049
			Waiting,
			// Token: 0x04000802 RID: 2050
			Eating,
			// Token: 0x04000803 RID: 2051
			Leaving
		}
	}
}
