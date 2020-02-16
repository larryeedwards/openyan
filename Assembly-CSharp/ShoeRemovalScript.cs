using System;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public class ShoeRemovalScript : MonoBehaviour
{
	// Token: 0x06001FF3 RID: 8179 RVA: 0x00148368 File Offset: 0x00146768
	public void Start()
	{
		if (this.Locker == null)
		{
			this.GetHeight(this.Student.StudentID);
			this.Locker = this.Student.StudentManager.Lockers.List[this.Student.StudentID];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.NewPairOfShoes, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.Locker;
			gameObject.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
			gameObject.transform.localPosition = new Vector3(0f, -0.29f + 0.3f * (float)this.Height, (!this.Male) ? 0.05f : 0.04f);
			this.LeftSchoolShoe = gameObject.transform.GetChild(0);
			this.RightSchoolShoe = gameObject.transform.GetChild(1);
			this.RemovalAnim = this.RemoveCasualAnim;
			this.RightCurrentShoe = this.RightCasualShoe;
			this.LeftCurrentShoe = this.LeftCasualShoe;
			this.RightNewShoe = this.RightSchoolShoe;
			this.LeftNewShoe = this.LeftSchoolShoe;
			this.ShoeParent = gameObject.transform;
			this.TargetShoes = this.IndoorShoes;
			this.RightShoePosition = this.RightCurrentShoe.localPosition;
			this.LeftShoePosition = this.LeftCurrentShoe.localPosition;
			this.RightCurrentShoe.localScale = new Vector3(1.111113f, 1f, 1.111113f);
			this.LeftCurrentShoe.localScale = new Vector3(1.111113f, 1f, 1.111113f);
			this.OutdoorShoes = this.Student.Cosmetic.CasualTexture;
			this.IndoorShoes = this.Student.Cosmetic.UniformTexture;
			this.Socks = this.Student.Cosmetic.SocksTexture;
			this.TargetShoes = this.IndoorShoes;
		}
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x00148574 File Offset: 0x00146974
	public void StartChangingShoes()
	{
		if (!this.Student.AoT)
		{
			this.RightCasualShoe.gameObject.SetActive(true);
			this.LeftCasualShoe.gameObject.SetActive(true);
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.Socks;
				this.MyRenderer.materials[1].mainTexture = this.Socks;
			}
			else
			{
				this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.Socks;
			}
		}
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x0014861C File Offset: 0x00146A1C
	private void Update()
	{
		if (!this.Student.DiscCheck && !this.Student.Dying && !this.Student.Alarmed && !this.Student.Splashed && !this.Student.TurnOffRadio)
		{
			if (this.Student.CurrentDestination == null)
			{
				this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
				this.Student.Pathfinding.target = this.Student.CurrentDestination;
			}
			this.Student.MoveTowardsTarget(this.Student.CurrentDestination.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Student.CurrentDestination.rotation, 10f * Time.deltaTime);
			this.Student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			this.Student.CharacterAnimation.CrossFade(this.RemovalAnim);
			if (this.Phase == 1)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 0.833333f)
				{
					this.ShoeParent.parent = this.LeftHand;
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 1.833333f)
				{
					this.ShoeParent.parent = this.Locker;
					this.X = this.ShoeParent.localEulerAngles.x;
					this.Y = this.ShoeParent.localEulerAngles.y;
					this.Z = this.ShoeParent.localEulerAngles.z;
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.X = Mathf.MoveTowards(this.X, 0f, Time.deltaTime * 360f);
				this.Y = Mathf.MoveTowards(this.Y, 186.878f, Time.deltaTime * 360f);
				this.Z = Mathf.MoveTowards(this.Z, 0f, Time.deltaTime * 360f);
				this.ShoeParent.localEulerAngles = new Vector3(this.X, this.Y, this.Z);
				this.ShoeParent.localPosition = Vector3.MoveTowards(this.ShoeParent.localPosition, new Vector3(0.272f, 0f, 0.552f), Time.deltaTime);
				if (this.ShoeParent.localPosition.y == 0f)
				{
					this.ShoeParent.localPosition = new Vector3(0.272f, 0f, 0.552f);
					this.ShoeParent.localEulerAngles = new Vector3(0f, 186.878f, 0f);
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 3.5f)
				{
					this.RightCurrentShoe.parent = null;
					this.RightCurrentShoe.position = new Vector3(this.RightCurrentShoe.position.x, 0.05f, this.RightCurrentShoe.position.z);
					this.RightCurrentShoe.localEulerAngles = new Vector3(0f, this.RightCurrentShoe.localEulerAngles.y, 0f);
					this.Phase++;
				}
			}
			else if (this.Phase == 5)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 4f)
				{
					this.LeftCurrentShoe.parent = null;
					this.LeftCurrentShoe.position = new Vector3(this.LeftCurrentShoe.position.x, 0.05f, this.LeftCurrentShoe.position.z);
					this.LeftCurrentShoe.localEulerAngles = new Vector3(0f, this.LeftCurrentShoe.localEulerAngles.y, 0f);
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 5.5f)
				{
					this.LeftNewShoe.parent = this.LeftFoot;
					this.LeftNewShoe.localPosition = this.LeftShoePosition;
					this.LeftNewShoe.localEulerAngles = Vector3.zero;
					this.Phase++;
				}
			}
			else if (this.Phase == 7)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 6.66666f)
				{
					if (!this.Student.AoT)
					{
						if (!this.Male)
						{
							this.MyRenderer.materials[0].mainTexture = this.TargetShoes;
							this.MyRenderer.materials[1].mainTexture = this.TargetShoes;
						}
						else
						{
							this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.TargetShoes;
						}
					}
					this.RightNewShoe.parent = this.RightFoot;
					this.RightNewShoe.localPosition = this.RightShoePosition;
					this.RightNewShoe.localEulerAngles = Vector3.zero;
					this.RightNewShoe.gameObject.SetActive(false);
					this.LeftNewShoe.gameObject.SetActive(false);
					this.Phase++;
				}
			}
			else if (this.Phase == 8)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 7.666666f)
				{
					this.ShoeParent.transform.position = (this.RightCurrentShoe.position - this.LeftCurrentShoe.position) * 0.5f;
					this.RightCurrentShoe.parent = this.ShoeParent;
					this.LeftCurrentShoe.parent = this.ShoeParent;
					this.ShoeParent.parent = this.RightHand;
					this.Phase++;
				}
			}
			else if (this.Phase == 9)
			{
				if (this.Student.CharacterAnimation[this.RemovalAnim].time >= 8.5f)
				{
					this.ShoeParent.parent = this.Locker;
					this.ShoeParent.localPosition = new Vector3(0f, ((!(this.TargetShoes == this.IndoorShoes)) ? -0.29f : -0.14f) + 0.3f * (float)this.Height, -0.01f);
					this.ShoeParent.localEulerAngles = new Vector3(0f, 180f, 0f);
					this.RightCurrentShoe.localPosition = new Vector3(0.041f, 0.04271515f, 0f);
					this.LeftCurrentShoe.localPosition = new Vector3(-0.041f, 0.04271515f, 0f);
					this.RightCurrentShoe.localEulerAngles = Vector3.zero;
					this.LeftCurrentShoe.localEulerAngles = Vector3.zero;
					this.Phase++;
				}
			}
			else if (this.Phase == 10 && this.Student.CharacterAnimation[this.RemovalAnim].time >= this.Student.CharacterAnimation[this.RemovalAnim].length)
			{
				this.Student.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
				this.Student.Routine = true;
				base.enabled = false;
				if (!this.Student.Indoors)
				{
					if (this.Student.Persona == PersonaType.PhoneAddict || this.Student.Sleuthing)
					{
						this.Student.SmartPhone.SetActive(true);
						if (!this.Student.Sleuthing)
						{
							this.Student.WalkAnim = this.Student.PhoneAnims[1];
						}
					}
					this.Student.Indoors = true;
					this.Student.CanTalk = true;
				}
				else
				{
					if (this.Student.Destinations[this.Student.Phase + 1] != null)
					{
						this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase + 1];
						this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase + 1];
					}
					else
					{
						this.Student.CurrentDestination = this.Student.StudentManager.Hangouts.List[0];
						this.Student.Pathfinding.target = this.Student.StudentManager.Hangouts.List[0];
					}
					this.Student.CanTalk = false;
					this.Student.Leaving = true;
					this.Student.Phase++;
					base.enabled = false;
					this.Phase++;
				}
			}
		}
		else
		{
			this.PutOnShoes();
			this.Student.Routine = false;
		}
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x00149040 File Offset: 0x00147440
	private void LateUpdate()
	{
		if (this.Phase < 7)
		{
			this.RightFoot.localScale = new Vector3(0.9f, 1f, 0.9f);
			this.LeftFoot.localScale = new Vector3(0.9f, 1f, 0.9f);
		}
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x00149098 File Offset: 0x00147498
	public void PutOnShoes()
	{
		this.CloseLocker();
		this.ShoeParent.parent = this.LeftHand;
		this.ShoeParent.parent = this.Locker;
		this.ShoeParent.localPosition = new Vector3(0.272f, 0f, 0.552f);
		this.ShoeParent.localEulerAngles = new Vector3(0f, 186.878f, 0f);
		this.RightCurrentShoe.parent = null;
		this.RightCurrentShoe.position = new Vector3(this.RightCurrentShoe.position.x, 0.05f, this.RightCurrentShoe.position.z);
		this.RightCurrentShoe.localEulerAngles = new Vector3(0f, this.RightCurrentShoe.localEulerAngles.y, 0f);
		this.LeftCurrentShoe.parent = null;
		this.LeftCurrentShoe.position = new Vector3(this.LeftCurrentShoe.position.x, 0.05f, this.LeftCurrentShoe.position.z);
		this.LeftCurrentShoe.localEulerAngles = new Vector3(0f, this.LeftCurrentShoe.localEulerAngles.y, 0f);
		this.LeftNewShoe.parent = this.LeftFoot;
		this.LeftNewShoe.localPosition = this.LeftShoePosition;
		this.LeftNewShoe.localEulerAngles = Vector3.zero;
		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.TargetShoes;
				this.MyRenderer.materials[1].mainTexture = this.TargetShoes;
			}
			else
			{
				this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.TargetShoes;
			}
		}
		this.RightNewShoe.parent = this.RightFoot;
		this.RightNewShoe.localPosition = this.RightShoePosition;
		this.RightNewShoe.localEulerAngles = Vector3.zero;
		this.RightNewShoe.gameObject.SetActive(false);
		this.LeftNewShoe.gameObject.SetActive(false);
		this.ShoeParent.transform.position = (this.RightCurrentShoe.position - this.LeftCurrentShoe.position) * 0.5f;
		this.RightCurrentShoe.parent = this.ShoeParent;
		this.LeftCurrentShoe.parent = this.ShoeParent;
		this.ShoeParent.parent = this.RightHand;
		this.ShoeParent.parent = this.Locker;
		this.ShoeParent.localPosition = new Vector3(0f, ((!(this.TargetShoes == this.IndoorShoes)) ? -0.29f : -0.14f) + 0.3f * (float)this.Height, -0.01f);
		this.ShoeParent.localEulerAngles = new Vector3(0f, 180f, 0f);
		this.RightCurrentShoe.localPosition = new Vector3(0.041f, 0.04271515f, 0f);
		this.LeftCurrentShoe.localPosition = new Vector3(-0.041f, 0.04271515f, 0f);
		this.RightCurrentShoe.localEulerAngles = Vector3.zero;
		this.LeftCurrentShoe.localEulerAngles = Vector3.zero;
		this.Student.Indoors = true;
		this.Student.CanTalk = true;
		base.enabled = false;
		this.Student.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Student.StopPairing();
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x00149470 File Offset: 0x00147870
	public void CloseLocker()
	{
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x00149474 File Offset: 0x00147874
	private void UpdateShoes()
	{
		this.Student.Indoors = true;
		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.IndoorShoes;
				this.MyRenderer.materials[1].mainTexture = this.IndoorShoes;
			}
			else
			{
				this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.IndoorShoes;
			}
		}
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x00149504 File Offset: 0x00147904
	public void LeavingSchool()
	{
		if (this.Locker == null)
		{
			this.Start();
		}
		this.Student.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		this.OutdoorShoes = this.Student.Cosmetic.CasualTexture;
		this.IndoorShoes = this.Student.Cosmetic.UniformTexture;
		this.Socks = this.Student.Cosmetic.SocksTexture;
		this.RemovalAnim = this.RemoveSchoolAnim;
		if (!this.Student.AoT)
		{
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.Socks;
				this.MyRenderer.materials[1].mainTexture = this.Socks;
			}
			else
			{
				this.MyRenderer.materials[this.Student.Cosmetic.UniformID].mainTexture = this.Socks;
			}
		}
		this.Student.CharacterAnimation.CrossFade(this.RemovalAnim);
		this.RightNewShoe.gameObject.SetActive(true);
		this.LeftNewShoe.gameObject.SetActive(true);
		this.RightCurrentShoe = this.RightSchoolShoe;
		this.LeftCurrentShoe = this.LeftSchoolShoe;
		this.RightNewShoe = this.RightCasualShoe;
		this.LeftNewShoe = this.LeftCasualShoe;
		this.TargetShoes = this.OutdoorShoes;
		this.Phase = 1;
		this.RightFoot.localScale = new Vector3(0.9f, 1f, 0.9f);
		this.LeftFoot.localScale = new Vector3(0.9f, 1f, 0.9f);
		this.RightCurrentShoe.localScale = new Vector3(1.111113f, 1f, 1.111113f);
		this.LeftCurrentShoe.localScale = new Vector3(1.111113f, 1f, 1.111113f);
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x001496F8 File Offset: 0x00147AF8
	private void GetHeight(int StudentID)
	{
		this.Height = 5;
		if (this.Student.StudentID == 30 || this.Student.StudentID == 5 || this.Student.StudentID == this.Student.StudentManager.RivalID || this.Student.StudentID == this.Student.StudentManager.SuitorID)
		{
			this.Height = 5;
		}
		this.RemoveCasualAnim = this.RemoveCasualAnim + this.Height.ToString() + "_00";
		this.RemoveSchoolAnim = this.RemoveSchoolAnim + this.Height.ToString() + "_01";
	}

	// Token: 0x04002C13 RID: 11283
	public StudentScript Student;

	// Token: 0x04002C14 RID: 11284
	public Vector3 RightShoePosition;

	// Token: 0x04002C15 RID: 11285
	public Vector3 LeftShoePosition;

	// Token: 0x04002C16 RID: 11286
	public Transform RightCurrentShoe;

	// Token: 0x04002C17 RID: 11287
	public Transform LeftCurrentShoe;

	// Token: 0x04002C18 RID: 11288
	public Transform RightCasualShoe;

	// Token: 0x04002C19 RID: 11289
	public Transform LeftCasualShoe;

	// Token: 0x04002C1A RID: 11290
	public Transform RightSchoolShoe;

	// Token: 0x04002C1B RID: 11291
	public Transform LeftSchoolShoe;

	// Token: 0x04002C1C RID: 11292
	public Transform RightNewShoe;

	// Token: 0x04002C1D RID: 11293
	public Transform LeftNewShoe;

	// Token: 0x04002C1E RID: 11294
	public Transform RightFoot;

	// Token: 0x04002C1F RID: 11295
	public Transform LeftFoot;

	// Token: 0x04002C20 RID: 11296
	public Transform RightHand;

	// Token: 0x04002C21 RID: 11297
	public Transform LeftHand;

	// Token: 0x04002C22 RID: 11298
	public Transform ShoeParent;

	// Token: 0x04002C23 RID: 11299
	public Transform Locker;

	// Token: 0x04002C24 RID: 11300
	public GameObject NewPairOfShoes;

	// Token: 0x04002C25 RID: 11301
	public GameObject Character;

	// Token: 0x04002C26 RID: 11302
	public string[] LockerAnims;

	// Token: 0x04002C27 RID: 11303
	public Texture OutdoorShoes;

	// Token: 0x04002C28 RID: 11304
	public Texture IndoorShoes;

	// Token: 0x04002C29 RID: 11305
	public Texture TargetShoes;

	// Token: 0x04002C2A RID: 11306
	public Texture Socks;

	// Token: 0x04002C2B RID: 11307
	public Renderer MyRenderer;

	// Token: 0x04002C2C RID: 11308
	public bool RemovingCasual = true;

	// Token: 0x04002C2D RID: 11309
	public bool Male;

	// Token: 0x04002C2E RID: 11310
	public int Height;

	// Token: 0x04002C2F RID: 11311
	public int Phase = 1;

	// Token: 0x04002C30 RID: 11312
	public float X;

	// Token: 0x04002C31 RID: 11313
	public float Y;

	// Token: 0x04002C32 RID: 11314
	public float Z;

	// Token: 0x04002C33 RID: 11315
	public string RemoveCasualAnim = string.Empty;

	// Token: 0x04002C34 RID: 11316
	public string RemoveSchoolAnim = string.Empty;

	// Token: 0x04002C35 RID: 11317
	public string RemovalAnim = string.Empty;
}
