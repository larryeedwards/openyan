using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class PickUpScript : MonoBehaviour
{
	// Token: 0x06001E7C RID: 7804 RVA: 0x0012BC48 File Offset: 0x0012A048
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();
		if (!this.CanCollide)
		{
			Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		}
		this.OriginalColor = this.Outline[0].color;
		this.OriginalScale = base.transform.localScale;
		if (this.MyRigidbody == null)
		{
			this.MyRigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.DisableAtStart)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x0012BCF8 File Offset: 0x0012A0F8
	private void LateUpdate()
	{
		if (this.CleaningProduct)
		{
			if (this.Clock.Period == 5)
			{
				this.Suspicious = false;
			}
			else
			{
				this.Suspicious = true;
			}
		}
		if (this.Weight)
		{
			if (this.Period < this.Clock.Period)
			{
				this.Strength = ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus;
			}
			if (this.Strength == 0)
			{
				this.Prompt.Label[3].text = "     Physical Stat Too Low";
				this.Prompt.Circle[3].fillAmount = 1f;
			}
			else
			{
				this.Prompt.Label[3].text = "     Carry";
			}
		}
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Weight)
			{
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Yandere.PickUp != null)
					{
						this.Yandere.CharacterAnimation[this.Yandere.CarryAnims[this.Yandere.PickUp.CarryAnimID]].weight = 0f;
					}
					if (this.Yandere.Armed)
					{
						this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[this.Yandere.EquippedWeapon.AnimID]].weight = 0f;
					}
					this.Yandere.targetRotation = Quaternion.LookRotation(new Vector3(base.transform.position.x, this.Yandere.transform.position.y, base.transform.position.z) - this.Yandere.transform.position);
					this.Yandere.transform.rotation = this.Yandere.targetRotation;
					this.Yandere.EmptyHands();
					base.transform.parent = this.Yandere.transform;
					base.transform.localPosition = new Vector3(0f, 0f, 0.79184f);
					base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Yandere.Character.GetComponent<Animation>().Play("f02_heavyWeightLift_00");
					this.Yandere.HeavyWeight = true;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.MyAnimation.Play("Weight_liftUp_00");
					this.MyRigidbody.isKinematic = true;
					this.BeingLifted = true;
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
		if (this.Yandere.PickUp == this)
		{
			base.transform.localPosition = this.HoldPosition;
			base.transform.localEulerAngles = this.HoldRotation;
			if (this.Garbage && !this.Yandere.StudentManager.IncineratorArea.bounds.Contains(this.Yandere.transform.position))
			{
				this.Drop();
				base.transform.position = new Vector3(-40f, 0f, 24f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				if (this.Clothing)
				{
					this.Yandere.Incinerator.BloodyClothing++;
				}
				else if (this.BodyPart)
				{
					this.Yandere.Incinerator.BodyParts++;
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (this.Yandere.PickUp != this && !this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Weight && this.BeingLifted)
		{
			if (this.Yandere.Lifting)
			{
				if (this.Yandere.StudentManager.Stop)
				{
					this.Drop();
				}
			}
			else
			{
				this.BePickedUp();
			}
		}
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x0012C318 File Offset: 0x0012A718
	public void BePickedUp()
	{
		if (this.Radio && SchemeGlobals.GetSchemeStage(5) == 2)
		{
			SchemeGlobals.SetSchemeStage(5, 3);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 4)
		{
			SchemeGlobals.SetSchemeStage(4, 5);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.OpenBook;
			this.Yandere.LifeNotePen.SetActive(true);
		}
		if (this.MyAnimation != null)
		{
			this.MyAnimation.Stop();
		}
		this.Prompt.Circle[3].fillAmount = 1f;
		this.BeingLifted = false;
		if (this.Yandere.PickUp != null)
		{
			this.Yandere.PickUp.Drop();
		}
		if (this.Yandere.Equipped == 3)
		{
			this.Yandere.Weapon[3].Drop();
		}
		else if (this.Yandere.Equipped > 0)
		{
			this.Yandere.Unequip();
		}
		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}
		if (this.Yandere.Carrying)
		{
			this.Yandere.StopCarrying();
		}
		if (!this.LeftHand)
		{
			base.transform.parent = this.Yandere.ItemParent;
		}
		else
		{
			base.transform.parent = this.Yandere.LeftItemParent;
		}
		if (base.GetComponent<RadioScript>() != null && base.GetComponent<RadioScript>().On)
		{
			base.GetComponent<RadioScript>().TurnOff();
		}
		this.MyCollider.enabled = false;
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		if (!this.Usable)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
		}
		else
		{
			this.Prompt.Carried = true;
		}
		this.Yandere.PickUp = this;
		this.Yandere.CarryAnimID = this.CarryAnimID;
		foreach (OutlineScript outlineScript in this.Outline)
		{
			outlineScript.color = new Color(0f, 0f, 0f, 1f);
		}
		if (this.BodyPart)
		{
			this.Yandere.NearBodies++;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		this.MyRigidbody.isKinematic = true;
		this.KinematicTimer = 0f;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x0012C618 File Offset: 0x0012AA18
	public void Drop()
	{
		if (this.Salty && SchemeGlobals.GetSchemeStage(4) == 5)
		{
			SchemeGlobals.SetSchemeStage(4, 4);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		if (this.TrashCan)
		{
			this.Yandere.MyController.radius = 0.2f;
		}
		if (this.CarryAnimID == 10)
		{
			this.MyRenderer.mesh = this.ClosedBook;
			this.Yandere.LifeNotePen.SetActive(false);
		}
		if (this.Weight)
		{
			this.Yandere.IdleAnim = this.Yandere.OriginalIdleAnim;
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = this.Yandere.OriginalRunAnim;
		}
		if (this.BloodCleaner != null)
		{
			this.BloodCleaner.enabled = true;
			this.BloodCleaner.Pathfinding.enabled = true;
		}
		this.Yandere.PickUp = null;
		if (this.BodyPart)
		{
			base.transform.parent = this.Yandere.LimbParent;
		}
		else
		{
			base.transform.parent = null;
		}
		if (this.LockRotation)
		{
			base.transform.localEulerAngles = new Vector3(0f, base.transform.localEulerAngles.y, 0f);
		}
		if (this.MyRigidbody != null)
		{
			this.MyRigidbody.constraints = this.OriginalConstraints;
			this.MyRigidbody.isKinematic = false;
			this.MyRigidbody.useGravity = true;
		}
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			this.MyCollider.isTrigger = false;
			if (!this.CanCollide)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		this.Prompt.Carried = false;
		foreach (OutlineScript outlineScript in this.Outline)
		{
			outlineScript.color = ((!this.Evidence) ? this.OriginalColor : this.EvidenceColor);
		}
		base.transform.localScale = this.OriginalScale;
		if (this.BodyPart)
		{
			this.Yandere.NearBodies--;
		}
		this.Yandere.StudentManager.UpdateStudents(0);
		if (this.Clothing && this.Evidence)
		{
			base.transform.parent = this.Yandere.Police.BloodParent;
		}
	}

	// Token: 0x0400276E RID: 10094
	public RigidbodyConstraints OriginalConstraints;

	// Token: 0x0400276F RID: 10095
	public BloodCleanerScript BloodCleaner;

	// Token: 0x04002770 RID: 10096
	public IncineratorScript Incinerator;

	// Token: 0x04002771 RID: 10097
	public BodyPartScript BodyPart;

	// Token: 0x04002772 RID: 10098
	public TrashCanScript TrashCan;

	// Token: 0x04002773 RID: 10099
	public OutlineScript[] Outline;

	// Token: 0x04002774 RID: 10100
	public YandereScript Yandere;

	// Token: 0x04002775 RID: 10101
	public Animation MyAnimation;

	// Token: 0x04002776 RID: 10102
	public BucketScript Bucket;

	// Token: 0x04002777 RID: 10103
	public PromptScript Prompt;

	// Token: 0x04002778 RID: 10104
	public ClockScript Clock;

	// Token: 0x04002779 RID: 10105
	public MopScript Mop;

	// Token: 0x0400277A RID: 10106
	public Mesh ClosedBook;

	// Token: 0x0400277B RID: 10107
	public Mesh OpenBook;

	// Token: 0x0400277C RID: 10108
	public Rigidbody MyRigidbody;

	// Token: 0x0400277D RID: 10109
	public Collider MyCollider;

	// Token: 0x0400277E RID: 10110
	public MeshFilter MyRenderer;

	// Token: 0x0400277F RID: 10111
	public Vector3 TrashPosition;

	// Token: 0x04002780 RID: 10112
	public Vector3 TrashRotation;

	// Token: 0x04002781 RID: 10113
	public Vector3 OriginalScale;

	// Token: 0x04002782 RID: 10114
	public Vector3 HoldPosition;

	// Token: 0x04002783 RID: 10115
	public Vector3 HoldRotation;

	// Token: 0x04002784 RID: 10116
	public Color EvidenceColor;

	// Token: 0x04002785 RID: 10117
	public Color OriginalColor;

	// Token: 0x04002786 RID: 10118
	public bool CleaningProduct;

	// Token: 0x04002787 RID: 10119
	public bool DisableAtStart;

	// Token: 0x04002788 RID: 10120
	public bool LockRotation;

	// Token: 0x04002789 RID: 10121
	public bool BeingLifted;

	// Token: 0x0400278A RID: 10122
	public bool CanCollide;

	// Token: 0x0400278B RID: 10123
	public bool Electronic;

	// Token: 0x0400278C RID: 10124
	public bool Flashlight;

	// Token: 0x0400278D RID: 10125
	public bool Suspicious;

	// Token: 0x0400278E RID: 10126
	public bool Blowtorch;

	// Token: 0x0400278F RID: 10127
	public bool Clothing;

	// Token: 0x04002790 RID: 10128
	public bool Evidence;

	// Token: 0x04002791 RID: 10129
	public bool JerryCan;

	// Token: 0x04002792 RID: 10130
	public bool LeftHand;

	// Token: 0x04002793 RID: 10131
	public bool RedPaint;

	// Token: 0x04002794 RID: 10132
	public bool Garbage;

	// Token: 0x04002795 RID: 10133
	public bool Bleach;

	// Token: 0x04002796 RID: 10134
	public bool Dumped;

	// Token: 0x04002797 RID: 10135
	public bool Usable;

	// Token: 0x04002798 RID: 10136
	public bool Weight;

	// Token: 0x04002799 RID: 10137
	public bool Radio;

	// Token: 0x0400279A RID: 10138
	public bool Salty;

	// Token: 0x0400279B RID: 10139
	public int CarryAnimID;

	// Token: 0x0400279C RID: 10140
	public int Strength;

	// Token: 0x0400279D RID: 10141
	public int Period;

	// Token: 0x0400279E RID: 10142
	public int Food;

	// Token: 0x0400279F RID: 10143
	public float KinematicTimer;

	// Token: 0x040027A0 RID: 10144
	public float DumpTimer;

	// Token: 0x040027A1 RID: 10145
	public bool Empty = true;

	// Token: 0x040027A2 RID: 10146
	public GameObject[] FoodPieces;

	// Token: 0x040027A3 RID: 10147
	public WeaponScript StuckBoxCutter;
}
