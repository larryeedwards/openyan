using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000146 RID: 326
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Animator))]
	public class YandereController : AIMover
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00054A4B File Offset: 0x00052E4B
		public static YandereController Instance
		{
			get
			{
				if (YandereController.instance == null)
				{
					YandereController.instance = UnityEngine.Object.FindObjectOfType<YandereController>();
				}
				return YandereController.instance;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00054A6C File Offset: 0x00052E6C
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.animator = base.GetComponent<Animator>();
			this.plateTransform.gameObject.SetActive(false);
			this.moveSpeed = GameController.Instance.activeDifficultyVariables.playerMoveSpeed;
			this.isPaused = true;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00054ABE File Offset: 0x00052EBE
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00054AE0 File Offset: 0x00052EE0
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00054B04 File Offset: 0x00052F04
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
			if (this.isPaused)
			{
				this.animator.SetBool("Moving", false);
			}
			this.animator.speed = (float)((!this.isPaused) ? 1 : 0);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00054B54 File Offset: 0x00052F54
		private void Update()
		{
			YandereController.rightButton = false;
			YandereController.leftButton = false;
			if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetKey("right") || Input.GetAxis("DpadX") > 0.5f)
			{
				if (!this.rightButtonPast)
				{
					this.rightButtonPast = true;
					YandereController.rightButton = true;
				}
			}
			else if (Input.GetAxisRaw("Horizontal") < 0f || Input.GetKey("left") || Input.GetAxis("DpadX") < -0.5f)
			{
				if (!this.leftButtonPast)
				{
					this.leftButtonPast = true;
					YandereController.leftButton = true;
				}
			}
			else
			{
				this.leftButtonPast = false;
				this.rightButtonPast = false;
			}
			if (base.transform.position.x < this.leftBounds.position.x)
			{
				base.transform.position = new Vector3(this.leftBounds.position.x, base.transform.position.y, base.transform.position.z);
			}
			if (base.transform.position.x > this.rightBounds.position.x)
			{
				base.transform.position = new Vector3(this.rightBounds.position.x, base.transform.position.y, base.transform.position.z);
			}
			if (Input.GetButtonDown("A") && this.aiTarget != null)
			{
				if (this.aiTarget.state == AIController.AIState.Menu)
				{
					this.aiTarget.TakeOrder();
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
				}
				else if (this.aiTarget.state == AIController.AIState.Waiting && this.heldItem != null)
				{
					this.aiTarget.DeliverFood(this.heldItem);
					SFXController.PlaySound(SFXController.Sounds.Plate);
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
					this.DropTray();
				}
			}
			if (this.aiTarget != null)
			{
				this.interactionIndicator.gameObject.SetActive(true);
				this.interactionIndicator.position = new Vector3(this.aiTarget.transform.position.x, this.aiTarget.transform.position.y + 0.6f, this.aiTarget.transform.position.z);
			}
			else
			{
				this.interactionIndicator.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00054E38 File Offset: 0x00053238
		public override ControlInput GetInput()
		{
			if (this.isPaused)
			{
				this.animator.SetBool("Moving", false);
				return default(ControlInput);
			}
			float horizontal = 0f;
			if (this.rightButtonPast)
			{
				horizontal = 1f;
			}
			else if (this.leftButtonPast)
			{
				horizontal = -1f;
			}
			ControlInput result = default(ControlInput);
			result.horizontal = horizontal;
			if (result.horizontal != 0f)
			{
				if (result.horizontal < 0f)
				{
					this.spriteRenderer.flipX = true;
				}
				else if (result.horizontal > 0f)
				{
					this.spriteRenderer.flipX = false;
				}
				this.animator.SetBool("Moving", true);
			}
			else
			{
				this.animator.SetBool("Moving", false);
			}
			return result;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00054F20 File Offset: 0x00053320
		public void PickUpTray(Food plate)
		{
			this.animator.SetTrigger("GetTray");
			this.heldItem = plate;
			this.plateTransform.gameObject.SetActive(false);
			this.plateTransform.GetComponent<SpriteRenderer>().sprite = this.heldItem.smallSprite;
			this.plateTransform.gameObject.SetActive(true);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00054F81 File Offset: 0x00053381
		public void DropTray()
		{
			this.plateTransform.gameObject.SetActive(false);
			this.animator.SetTrigger("DropTray");
			this.heldItem = null;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00054FAC File Offset: 0x000533AC
		private void OnTriggerEnter2D(Collider2D collision)
		{
			AIController component = collision.GetComponent<AIController>();
			if (component != null)
			{
				if (component.state == AIController.AIState.Menu)
				{
					this.aiTarget = component;
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.TakeOrder);
				}
				if (component.state == AIController.AIState.Waiting && this.heldItem != null)
				{
					this.aiTarget = component;
					InteractionMenu.SetAButton(InteractionMenu.AButtonText.GiveFood);
				}
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00055010 File Offset: 0x00053410
		private void OnTriggerExit2D(Collider2D collision)
		{
			AIController component = collision.GetComponent<AIController>();
			if (component != null && component == this.aiTarget)
			{
				this.aiTarget = null;
				InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0005504E File Offset: 0x0005344E
		public void SetPause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00055058 File Offset: 0x00053458
		public void PositionTray(string point)
		{
			string[] array = point.Split(new char[]
			{
				','
			});
			float num;
			float.TryParse(array[0], out num);
			float y;
			float.TryParse(array[1], out y);
			this.plateTransform.localPosition = new Vector3((!this.spriteRenderer.flipX) ? num : (-num), y, 0f);
		}

		// Token: 0x04000806 RID: 2054
		private static YandereController instance;

		// Token: 0x04000807 RID: 2055
		public static bool leftButton;

		// Token: 0x04000808 RID: 2056
		public static bool rightButton;

		// Token: 0x04000809 RID: 2057
		public Transform leftBounds;

		// Token: 0x0400080A RID: 2058
		public Transform rightBounds;

		// Token: 0x0400080B RID: 2059
		public Transform interactionIndicator;

		// Token: 0x0400080C RID: 2060
		public Transform plateTransform;

		// Token: 0x0400080D RID: 2061
		public Food heldItem;

		// Token: 0x0400080E RID: 2062
		private SpriteRenderer spriteRenderer;

		// Token: 0x0400080F RID: 2063
		private Animator animator;

		// Token: 0x04000810 RID: 2064
		private AIController aiTarget;

		// Token: 0x04000811 RID: 2065
		public bool leftButtonPast;

		// Token: 0x04000812 RID: 2066
		public bool rightButtonPast;

		// Token: 0x04000813 RID: 2067
		private bool isPaused;
	}
}
