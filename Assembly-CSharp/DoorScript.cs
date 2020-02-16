using System;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class DoorScript : MonoBehaviour
{
	// Token: 0x17000393 RID: 915
	// (get) Token: 0x060018E6 RID: 6374 RVA: 0x000E46B4 File Offset: 0x000E2AB4
	private bool Double
	{
		get
		{
			return this.Doors.Length == 2;
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x000E46C4 File Offset: 0x000E2AC4
	private void Start()
	{
		this.TrapSwing = 12.15f;
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.StudentManager = this.Yandere.StudentManager;
		this.StudentManager.Doors[this.StudentManager.DoorID] = this;
		this.StudentManager.DoorID++;
		this.DoorID = this.StudentManager.DoorID;
		if (this.StudentManager.EastBathroomArea.bounds.Contains(base.transform.position) || this.StudentManager.WestBathroomArea.bounds.Contains(base.transform.position))
		{
			this.RoomName = "Toilet Stall";
		}
		if (this.Swinging)
		{
			this.OriginX[0] = this.Doors[0].transform.localPosition.z;
			if (this.OriginX.Length > 1)
			{
				this.OriginX[1] = this.Doors[1].transform.localPosition.z;
			}
			this.TimeLimit = 1f;
		}
		if (this.Labels.Length > 0)
		{
			this.Labels[0].text = this.RoomName;
			this.Labels[1].text = this.RoomName;
			this.UpdatePlate();
		}
		if (this.Club != ClubType.None && ClubGlobals.GetClubClosed(this.Club))
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
		if (this.DisableSelf)
		{
			base.enabled = false;
		}
		this.Prompt.Student = false;
		this.Prompt.Door = true;
		this.DoorColliders = new Collider[2];
		this.DoorColliders[0] = this.Doors[0].gameObject.GetComponent<BoxCollider>();
		if (this.DoorColliders[0] == null)
		{
			this.DoorColliders[0] = this.Doors[0].GetChild(0).gameObject.GetComponent<BoxCollider>();
		}
		if (this.Doors.Length > 1)
		{
			this.DoorColliders[1] = this.Doors[1].GetComponent<BoxCollider>();
		}
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000E491C File Offset: 0x000E2D1C
	private void Update()
	{
		if (this.Prompt.DistanceSqr <= 1f)
		{
			if (Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 2f)
			{
				if (!this.Near)
				{
					this.TopicCheck();
					this.Yandere.Location.Label.text = this.RoomName;
					this.Yandere.Location.Show = true;
					this.Near = true;
				}
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.Prompt.Circle[0].fillAmount = 1f;
					if (!this.Open)
					{
						this.OpenDoor();
					}
					else
					{
						this.CloseDoor();
					}
				}
				if (this.Double && this.Swinging && this.Prompt.Circle[1].fillAmount == 0f)
				{
					if (SchemeGlobals.GetSchemeStage(1) == 2)
					{
						SchemeGlobals.SetSchemeStage(1, 3);
						this.Yandere.PauseScreen.Schemes.UpdateInstructions();
					}
					this.Bucket = this.Yandere.PickUp.Bucket;
					this.Yandere.EmptyHands();
					this.Bucket.transform.parent = base.transform;
					this.Bucket.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Bucket.Trap = true;
					this.Bucket.Prompt.Hide();
					this.Bucket.Prompt.enabled = false;
					this.CheckDirection();
					if (this.North)
					{
						this.Bucket.transform.localPosition = new Vector3(0f, 2.25f, 0.2975f);
					}
					else
					{
						this.Bucket.transform.localPosition = new Vector3(0f, 2.25f, -0.2975f);
					}
					this.Bucket.GetComponent<Rigidbody>().isKinematic = true;
					this.Bucket.GetComponent<Rigidbody>().useGravity = false;
					if (this.Open)
					{
						this.DoorColliders[0].isTrigger = true;
						this.DoorColliders[1].isTrigger = true;
					}
					this.Prompt.HideButton[1] = true;
					this.CanSetBucket = false;
					this.BucketSet = true;
					this.Open = false;
					this.Timer = 0f;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
			}
		}
		else if (this.Near)
		{
			this.Yandere.Location.Show = false;
			this.Near = false;
		}
		if (this.Timer < this.TimeLimit)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer >= this.TimeLimit)
			{
				this.DoorColliders[0].isTrigger = false;
				if (this.DoorColliders[1] != null)
				{
					this.DoorColliders[1].isTrigger = false;
				}
				if (this.Portal != null)
				{
					this.Portal.open = this.Open;
				}
			}
			if (this.BucketSet)
			{
				for (int i = 0; i < this.Doors.Length; i++)
				{
					Transform transform = this.Doors[i];
					transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, this.OriginX[i] + ((!this.North) ? this.ShiftNorth : this.ShiftSouth), Time.deltaTime * 3.6f));
					this.Rotation = Mathf.Lerp(this.Rotation, (!this.North) ? this.TrapSwing : (-this.TrapSwing), Time.deltaTime * 3.6f);
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (i != 0) ? (-this.Rotation) : this.Rotation, transform.localEulerAngles.z);
				}
			}
			else if (!this.Open)
			{
				for (int j = 0; j < this.Doors.Length; j++)
				{
					Transform transform2 = this.Doors[j];
					if (!this.Swinging)
					{
						transform2.localPosition = new Vector3(Mathf.Lerp(transform2.localPosition.x, this.ClosedPositions[j], Time.deltaTime * 3.6f), transform2.localPosition.y, transform2.localPosition.z);
					}
					else
					{
						this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 3.6f);
						transform2.localPosition = new Vector3(transform2.localPosition.x, transform2.localPosition.y, Mathf.Lerp(transform2.localPosition.z, this.OriginX[j], Time.deltaTime * 3.6f));
						transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x, (j != 0) ? (-this.Rotation) : this.Rotation, transform2.localEulerAngles.z);
					}
				}
			}
			else
			{
				for (int k = 0; k < this.Doors.Length; k++)
				{
					Transform transform3 = this.Doors[k];
					if (!this.Swinging)
					{
						transform3.localPosition = new Vector3(Mathf.Lerp(transform3.localPosition.x, this.OpenPositions[k], Time.deltaTime * 3.6f), transform3.localPosition.y, transform3.localPosition.z);
					}
					else
					{
						transform3.localPosition = new Vector3(transform3.localPosition.x, transform3.localPosition.y, Mathf.Lerp(transform3.localPosition.z, this.OriginX[k] + ((!this.North) ? this.ShiftSouth : this.ShiftNorth), Time.deltaTime * 3.6f));
						this.Rotation = Mathf.Lerp(this.Rotation, (!this.North) ? (-this.Swing) : this.Swing, Time.deltaTime * 3.6f);
						transform3.localEulerAngles = new Vector3(transform3.localEulerAngles.x, (k != 0) ? (-this.Rotation) : this.Rotation, transform3.localEulerAngles.z);
					}
				}
			}
		}
		else if (this.Locked)
		{
			if (this.Prompt.Circle[0].fillAmount < 1f)
			{
				this.Prompt.Label[0].text = "     Locked";
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			if (this.Yandere.Inventory.LockPick)
			{
				this.Prompt.HideButton[2] = false;
				if (this.Prompt.Circle[2].fillAmount == 0f)
				{
					this.Prompt.Yandere.Inventory.LockPick = false;
					this.Prompt.HideButton[2] = true;
					this.Locked = false;
				}
			}
			else if (!this.Prompt.HideButton[2])
			{
				this.Prompt.HideButton[2] = true;
			}
		}
		if (!this.NoTrap && this.Swinging && this.Double)
		{
			if (this.Yandere.PickUp != null)
			{
				if (this.Yandere.PickUp.Bucket != null)
				{
					if (this.Yandere.PickUp.GetComponent<BucketScript>().Full)
					{
						this.Prompt.HideButton[1] = false;
						this.CanSetBucket = true;
					}
					else if (this.CanSetBucket)
					{
						this.Prompt.HideButton[1] = true;
						this.CanSetBucket = false;
					}
				}
				else if (this.CanSetBucket)
				{
					this.Prompt.HideButton[1] = true;
					this.CanSetBucket = false;
				}
			}
			else if (this.CanSetBucket)
			{
				this.Prompt.HideButton[1] = true;
				this.CanSetBucket = false;
			}
		}
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000E523C File Offset: 0x000E363C
	public void OpenDoor()
	{
		if (this.Portal != null)
		{
			this.Portal.open = true;
		}
		this.Open = true;
		this.Timer = 0f;
		this.UpdateLabel();
		if (this.HidingSpot)
		{
			UnityEngine.Object.Destroy(this.HideCollider.GetComponent<BoxCollider>());
		}
		this.CheckDirection();
		if (this.BucketSet)
		{
			this.Bucket.GetComponent<Rigidbody>().isKinematic = false;
			this.Bucket.GetComponent<Rigidbody>().useGravity = true;
			this.Bucket.UpdateAppearance = true;
			this.Bucket.Prompt.enabled = true;
			this.Bucket.Full = false;
			this.Bucket.Fly = true;
			this.Prompt.enabled = true;
			this.BucketSet = false;
		}
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000E5314 File Offset: 0x000E3714
	private void LockDoor()
	{
		this.Open = false;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x000E5334 File Offset: 0x000E3734
	private void CheckDirection()
	{
		this.North = false;
		this.RelativeCharacter = ((!(this.Student != null)) ? this.Yandere.transform : this.Student.transform);
		if (this.Facing == "North")
		{
			if (this.RelativeCharacter.position.z < base.transform.position.z)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "South")
		{
			if (this.RelativeCharacter.position.z > base.transform.position.z)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "East")
		{
			if (this.RelativeCharacter.position.x < base.transform.position.x)
			{
				this.North = true;
			}
		}
		else if (this.Facing == "West" && this.RelativeCharacter.position.x > base.transform.position.x)
		{
			this.North = true;
		}
		this.Student = null;
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000E54B0 File Offset: 0x000E38B0
	public void CloseDoor()
	{
		this.Open = false;
		this.Timer = 0f;
		this.UpdateLabel();
		this.DoorColliders[0].isTrigger = true;
		if (this.DoorColliders[1] != null)
		{
			this.DoorColliders[1].isTrigger = true;
		}
		if (this.HidingSpot)
		{
			this.HideCollider.gameObject.AddComponent<BoxCollider>();
			BoxCollider component = this.HideCollider.GetComponent<BoxCollider>();
			component.size = new Vector3(component.size.x, component.size.y, 2f);
			component.isTrigger = true;
			this.HideCollider.MyCollider = component;
		}
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000E556B File Offset: 0x000E396B
	private void UpdateLabel()
	{
		if (this.Open)
		{
			this.Prompt.Label[0].text = "     Close";
		}
		else
		{
			this.Prompt.Label[0].text = "     Open";
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000E55AC File Offset: 0x000E39AC
	private void UpdatePlate()
	{
		switch (this.RoomID)
		{
		case 1:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.75f);
			break;
		case 2:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.5f);
			break;
		case 3:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.25f);
			break;
		case 4:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0f);
			break;
		case 5:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.75f);
			break;
		case 6:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.5f);
			break;
		case 7:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.25f);
			break;
		case 8:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0f);
			break;
		case 9:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.75f);
			break;
		case 10:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
			break;
		case 11:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.25f);
			break;
		case 12:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0f);
			break;
		case 13:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.75f);
			break;
		case 14:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.5f);
			break;
		case 15:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.25f);
			break;
		case 16:
			this.Sign.material.mainTexture = this.Plates[1];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0f);
			break;
		case 17:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.75f);
			break;
		case 18:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.5f);
			break;
		case 19:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.25f);
			break;
		case 20:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0f);
			break;
		case 21:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.75f);
			break;
		case 22:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.5f);
			break;
		case 23:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0.25f);
			break;
		case 24:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.25f, 0f);
			break;
		case 25:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.75f);
			break;
		case 26:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.5f);
			break;
		case 27:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0.25f);
			break;
		case 28:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.5f, 0f);
			break;
		case 29:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.75f);
			break;
		case 30:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.5f);
			break;
		case 31:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0.25f);
			break;
		case 32:
			this.Sign.material.mainTexture = this.Plates[2];
			this.Sign.material.mainTextureOffset = new Vector2(0.75f, 0f);
			break;
		case 33:
			this.Sign.material.mainTexture = this.Plates[3];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.75f);
			break;
		case 34:
			this.Sign.material.mainTexture = this.Plates[3];
			this.Sign.material.mainTextureOffset = new Vector2(0f, 0.5f);
			break;
		}
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x000E5E54 File Offset: 0x000E4254
	private void TopicCheck()
	{
		if (this.RoomID > 25 && this.RoomID < 37)
		{
			this.StudentManager.TutorialWindow.ShowClubMessage = true;
		}
		switch (this.RoomID)
		{
		case 3:
			if (!ConversationGlobals.GetTopicDiscovered(22))
			{
				ConversationGlobals.SetTopicDiscovered(22, true);
				this.Yandere.NotificationManager.TopicName = "School";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 13:
			if (!ConversationGlobals.GetTopicDiscovered(18))
			{
				ConversationGlobals.SetTopicDiscovered(18, true);
				this.Yandere.NotificationManager.TopicName = "Reading";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 22:
			if (!ConversationGlobals.GetTopicDiscovered(11))
			{
				ConversationGlobals.SetTopicDiscovered(11, true);
				ConversationGlobals.SetTopicDiscovered(12, true);
				ConversationGlobals.SetTopicDiscovered(13, true);
				ConversationGlobals.SetTopicDiscovered(14, true);
				this.Yandere.NotificationManager.TopicName = "Video Games";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				this.Yandere.NotificationManager.TopicName = "Anime";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				this.Yandere.NotificationManager.TopicName = "Cosplay";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				this.Yandere.NotificationManager.TopicName = "Memes";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 26:
			if (!ConversationGlobals.GetTopicDiscovered(1))
			{
				ConversationGlobals.SetTopicDiscovered(1, true);
				this.Yandere.NotificationManager.TopicName = "Cooking";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 27:
			if (!ConversationGlobals.GetTopicDiscovered(2))
			{
				ConversationGlobals.SetTopicDiscovered(2, true);
				this.Yandere.NotificationManager.TopicName = "Drama";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 28:
			if (!ConversationGlobals.GetTopicDiscovered(3))
			{
				ConversationGlobals.SetTopicDiscovered(3, true);
				this.Yandere.NotificationManager.TopicName = "Occult";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 29:
			if (!ConversationGlobals.GetTopicDiscovered(4))
			{
				ConversationGlobals.SetTopicDiscovered(4, true);
				this.Yandere.NotificationManager.TopicName = "Art";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 30:
			if (!ConversationGlobals.GetTopicDiscovered(5))
			{
				ConversationGlobals.SetTopicDiscovered(5, true);
				this.Yandere.NotificationManager.TopicName = "Music";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 31:
			if (!ConversationGlobals.GetTopicDiscovered(6))
			{
				ConversationGlobals.SetTopicDiscovered(6, true);
				this.Yandere.NotificationManager.TopicName = "Martial Arts";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 32:
			if (!ConversationGlobals.GetTopicDiscovered(7))
			{
				ConversationGlobals.SetTopicDiscovered(7, true);
				this.Yandere.NotificationManager.TopicName = "Photography";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 34:
			if (!ConversationGlobals.GetTopicDiscovered(8))
			{
				ConversationGlobals.SetTopicDiscovered(8, true);
				this.Yandere.NotificationManager.TopicName = "Science";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 35:
			if (!ConversationGlobals.GetTopicDiscovered(9))
			{
				ConversationGlobals.SetTopicDiscovered(9, true);
				this.Yandere.NotificationManager.TopicName = "Sports";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		case 36:
			if (!ConversationGlobals.GetTopicDiscovered(10))
			{
				ConversationGlobals.SetTopicDiscovered(10, true);
				this.Yandere.NotificationManager.TopicName = "Gardening";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			if (!ConversationGlobals.GetTopicDiscovered(24))
			{
				ConversationGlobals.SetTopicDiscovered(24, true);
				this.Yandere.NotificationManager.TopicName = "Nature";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
			}
			break;
		}
	}

	// Token: 0x04001CAC RID: 7340
	[SerializeField]
	private Transform RelativeCharacter;

	// Token: 0x04001CAD RID: 7341
	[SerializeField]
	private HideColliderScript HideCollider;

	// Token: 0x04001CAE RID: 7342
	public StudentScript Student;

	// Token: 0x04001CAF RID: 7343
	[SerializeField]
	private YandereScript Yandere;

	// Token: 0x04001CB0 RID: 7344
	[SerializeField]
	private BucketScript Bucket;

	// Token: 0x04001CB1 RID: 7345
	public PromptScript Prompt;

	// Token: 0x04001CB2 RID: 7346
	[SerializeField]
	private Collider[] DoorColliders;

	// Token: 0x04001CB3 RID: 7347
	[SerializeField]
	private float[] ClosedPositions;

	// Token: 0x04001CB4 RID: 7348
	[SerializeField]
	private float[] OpenPositions;

	// Token: 0x04001CB5 RID: 7349
	[SerializeField]
	private Transform[] Doors;

	// Token: 0x04001CB6 RID: 7350
	[SerializeField]
	private Texture[] Plates;

	// Token: 0x04001CB7 RID: 7351
	[SerializeField]
	private UILabel[] Labels;

	// Token: 0x04001CB8 RID: 7352
	[SerializeField]
	private float[] OriginX;

	// Token: 0x04001CB9 RID: 7353
	[SerializeField]
	private bool CanSetBucket;

	// Token: 0x04001CBA RID: 7354
	[SerializeField]
	private bool HidingSpot;

	// Token: 0x04001CBB RID: 7355
	[SerializeField]
	private bool BucketSet;

	// Token: 0x04001CBC RID: 7356
	[SerializeField]
	private bool Swinging;

	// Token: 0x04001CBD RID: 7357
	public bool Locked;

	// Token: 0x04001CBE RID: 7358
	[SerializeField]
	private bool NoTrap;

	// Token: 0x04001CBF RID: 7359
	[SerializeField]
	private bool North;

	// Token: 0x04001CC0 RID: 7360
	public bool Open;

	// Token: 0x04001CC1 RID: 7361
	[SerializeField]
	private bool Near;

	// Token: 0x04001CC2 RID: 7362
	[SerializeField]
	private float ShiftNorth = -0.1f;

	// Token: 0x04001CC3 RID: 7363
	[SerializeField]
	private float ShiftSouth = 0.1f;

	// Token: 0x04001CC4 RID: 7364
	[SerializeField]
	private float Rotation;

	// Token: 0x04001CC5 RID: 7365
	public float TimeLimit = 2f;

	// Token: 0x04001CC6 RID: 7366
	public float Timer;

	// Token: 0x04001CC7 RID: 7367
	[SerializeField]
	private float TrapSwing = 12.15f;

	// Token: 0x04001CC8 RID: 7368
	[SerializeField]
	private float Swing = 150f;

	// Token: 0x04001CC9 RID: 7369
	[SerializeField]
	private Renderer Sign;

	// Token: 0x04001CCA RID: 7370
	[SerializeField]
	private string RoomName = string.Empty;

	// Token: 0x04001CCB RID: 7371
	[SerializeField]
	private string Facing = string.Empty;

	// Token: 0x04001CCC RID: 7372
	[SerializeField]
	private int RoomID;

	// Token: 0x04001CCD RID: 7373
	[SerializeField]
	private ClubType Club;

	// Token: 0x04001CCE RID: 7374
	[SerializeField]
	private bool DisableSelf;

	// Token: 0x04001CCF RID: 7375
	private StudentManagerScript StudentManager;

	// Token: 0x04001CD0 RID: 7376
	public OcclusionPortal Portal;

	// Token: 0x04001CD1 RID: 7377
	public int DoorID;
}
