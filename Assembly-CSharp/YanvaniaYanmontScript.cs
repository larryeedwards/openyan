using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005B5 RID: 1461
[RequireComponent(typeof(CharacterController))]
public class YanvaniaYanmontScript : MonoBehaviour
{
	// Token: 0x06002335 RID: 9013 RVA: 0x001BC00C File Offset: 0x001BA40C
	private void Awake()
	{
		Animation component = this.Character.GetComponent<Animation>();
		component["f02_yanvaniaDeath_00"].speed = 0.25f;
		component["f02_yanvaniaAttack_00"].speed = 2f;
		component["f02_yanvaniaCrouchAttack_00"].speed = 2f;
		component["f02_yanvaniaWalk_00"].speed = 0.6666667f;
		component["f02_yanvaniaWhip_Neutral"].speed = 0f;
		component["f02_yanvaniaWhip_Up"].speed = 0f;
		component["f02_yanvaniaWhip_Right"].speed = 0f;
		component["f02_yanvaniaWhip_Down"].speed = 0f;
		component["f02_yanvaniaWhip_Left"].speed = 0f;
		component["f02_yanvaniaCrouchPose_00"].layer = 1;
		component.Play("f02_yanvaniaCrouchPose_00");
		component["f02_yanvaniaCrouchPose_00"].weight = 0f;
		Physics.IgnoreLayerCollision(19, 13, true);
		Physics.IgnoreLayerCollision(19, 19, true);
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x001BC128 File Offset: 0x001BA528
	private void Start()
	{
		this.WhipChain[0].transform.localScale = Vector3.zero;
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaIdle_00");
		this.controller = base.GetComponent<CharacterController>();
		this.myTransform = base.transform;
		this.speed = this.walkSpeed;
		this.rayDistance = this.controller.height * 0.5f + this.controller.radius;
		this.slideLimit = this.controller.slopeLimit - 0.1f;
		this.jumpTimer = this.antiBunnyHopFactor;
		this.originalThreshold = this.fallingDamageThreshold;
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x001BC1D8 File Offset: 0x001BA5D8
	private void FixedUpdate()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (this.CanMove)
		{
			if (!this.Injured)
			{
				if (!this.Cutscene)
				{
					if (this.grounded)
					{
						if (!this.Attacking)
						{
							if (!this.Crouching)
							{
								if (Input.GetAxis("VaniaHorizontal") > 0f)
								{
									this.inputX = 1f;
								}
								else if (Input.GetAxis("VaniaHorizontal") < 0f)
								{
									this.inputX = -1f;
								}
								else
								{
									this.inputX = 0f;
								}
							}
						}
						else if (this.grounded)
						{
							this.fallingDamageThreshold = 100f;
							this.moveDirection.x = 0f;
							this.inputX = 0f;
							this.speed = 0f;
						}
					}
					else if (Input.GetAxis("VaniaHorizontal") != 0f)
					{
						if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.inputX = 1f;
						}
						else if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.inputX = -1f;
						}
						else
						{
							this.inputX = 0f;
						}
					}
					else
					{
						this.inputX = Mathf.MoveTowards(this.inputX, 0f, Time.deltaTime * 10f);
					}
					float num = 0f;
					float num2 = (this.inputX == 0f || num == 0f || !this.limitDiagonalSpeed) ? 1f : 0.707106769f;
					if (!this.Attacking)
					{
						if (Input.GetAxis("VaniaHorizontal") < 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
						else if (Input.GetAxis("VaniaHorizontal") > 0f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
						}
					}
					if (this.grounded)
					{
						if (!this.Attacking && !this.Dangling)
						{
							if (Input.GetAxis("VaniaVertical") < -0.5f)
							{
								this.MyController.center = new Vector3(this.MyController.center.x, 0.5f, this.MyController.center.z);
								this.MyController.height = 1f;
								this.Crouching = true;
								this.IdleTimer = 10f;
								this.inputX = 0f;
							}
							if (this.Crouching)
							{
								component.CrossFade("f02_yanvaniaCrouch_00", 0.1f);
								if (!this.Attacking)
								{
									if (!this.Dangling)
									{
										if (Input.GetAxis("VaniaVertical") > -0.5f)
										{
											component["f02_yanvaniaCrouchPose_00"].weight = 0f;
											this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
											this.MyController.height = 1.5f;
											this.Crouching = false;
										}
									}
									else if (Input.GetAxis("VaniaVertical") > -0.5f && Input.GetButton("X"))
									{
										component["f02_yanvaniaCrouchPose_00"].weight = 0f;
										this.MyController.center = new Vector3(this.MyController.center.x, 0.75f, this.MyController.center.z);
										this.MyController.height = 1.5f;
										this.Crouching = false;
									}
								}
							}
							else if (this.inputX == 0f)
							{
								if (this.IdleTimer > 0f)
								{
									component.CrossFade("f02_yanvaniaIdle_00", 0.1f);
									component["f02_yanvaniaIdle_00"].speed = this.IdleTimer / 10f;
								}
								else
								{
									component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
								}
								this.IdleTimer -= Time.deltaTime;
							}
							else
							{
								this.IdleTimer = 10f;
								component.CrossFade((this.speed != this.walkSpeed) ? "f02_yanvaniaRun_00" : "f02_yanvaniaWalk_00", 0.1f);
							}
						}
						bool flag = false;
						if (Physics.Raycast(this.myTransform.position, -Vector3.up, out this.hit, this.rayDistance))
						{
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						else
						{
							Physics.Raycast(this.contactPoint + Vector3.up, -Vector3.up, out this.hit);
							if (Vector3.Angle(this.hit.normal, Vector3.up) > this.slideLimit)
							{
								flag = true;
							}
						}
						if (this.falling)
						{
							this.falling = false;
							if (this.myTransform.position.y < this.fallStartLevel - this.fallingDamageThreshold)
							{
								this.FallingDamageAlert(this.fallStartLevel - this.myTransform.position.y);
							}
							this.fallingDamageThreshold = this.originalThreshold;
						}
						if (!this.toggleRun)
						{
							this.speed = ((!Input.GetKey(KeyCode.LeftShift)) ? this.walkSpeed : this.runSpeed);
						}
						if ((flag && this.slideWhenOverSlopeLimit) || (this.slideOnTaggedObjects && this.hit.collider.tag == "Slide"))
						{
							Vector3 normal = this.hit.normal;
							this.moveDirection = new Vector3(normal.x, -normal.y, normal.z);
							Vector3.OrthoNormalize(ref normal, ref this.moveDirection);
							this.moveDirection *= this.slideSpeed;
							this.playerControl = false;
						}
						else
						{
							this.moveDirection = new Vector3(this.inputX * num2, -this.antiBumpFactor, num * num2);
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection) * this.speed;
							this.playerControl = true;
						}
						if (!Input.GetButton("VaniaJump"))
						{
							this.jumpTimer++;
						}
						else if (this.jumpTimer >= this.antiBunnyHopFactor && !this.Attacking)
						{
							this.Crouching = false;
							this.fallingDamageThreshold = 0f;
							this.moveDirection.y = this.jumpSpeed;
							this.IdleTimer = 10f;
							this.jumpTimer = 0;
							AudioSource component2 = base.GetComponent<AudioSource>();
							component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
							component2.Play();
						}
					}
					else
					{
						if (!this.Attacking)
						{
							component.CrossFade((base.transform.position.y <= this.PreviousY) ? "f02_yanvaniaFall_00" : "f02_yanvaniaJump_00", 0.4f);
						}
						this.PreviousY = base.transform.position.y;
						if (!this.falling)
						{
							this.falling = true;
							this.fallStartLevel = this.myTransform.position.y;
						}
						if (this.airControl && this.playerControl)
						{
							this.moveDirection.x = this.inputX * this.speed * num2;
							this.moveDirection.z = num * this.speed * num2;
							this.moveDirection = this.myTransform.TransformDirection(this.moveDirection);
						}
					}
				}
				else
				{
					this.moveDirection.x = 0f;
					if (this.grounded)
					{
						if (base.transform.position.x > -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else if (base.transform.position.x < -34f)
						{
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, 90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(-1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							base.transform.position = new Vector3(Mathf.MoveTowards(base.transform.position.x, -34f, Time.deltaTime * this.walkSpeed), base.transform.position.y, base.transform.position.z);
							component.CrossFade("f02_yanvaniaWalk_00");
						}
						else
						{
							component.CrossFade("f02_yanvaniaDramaticIdle_00", 1f);
							this.Character.transform.localEulerAngles = new Vector3(this.Character.transform.localEulerAngles.x, -90f, this.Character.transform.localEulerAngles.z);
							this.Character.transform.localScale = new Vector3(1f, this.Character.transform.localScale.y, this.Character.transform.localScale.z);
							this.WhipChain[0].transform.localScale = Vector3.zero;
							this.fallingDamageThreshold = 100f;
							this.TextBox.SetActive(true);
							this.Attacking = false;
							base.enabled = false;
						}
					}
					Physics.SyncTransforms();
				}
			}
			else
			{
				component.CrossFade("f02_damage_25");
				this.RecoveryTimer += Time.deltaTime;
				if (this.RecoveryTimer > 1f)
				{
					this.RecoveryTimer = 0f;
					this.Injured = false;
				}
			}
			this.moveDirection.y = this.moveDirection.y - this.gravity * Time.deltaTime;
			this.grounded = ((this.controller.Move(this.moveDirection * Time.deltaTime) & CollisionFlags.Below) != CollisionFlags.None);
			if (this.grounded && this.EnterCutscene)
			{
				this.YanvaniaCamera.Cutscene = true;
				this.Cutscene = true;
			}
			if ((this.controller.collisionFlags & CollisionFlags.Above) != CollisionFlags.None && this.moveDirection.y > 0f)
			{
				this.moveDirection.y = 0f;
			}
		}
		else if (this.Health == 0f)
		{
			this.DeathTimer += Time.deltaTime;
			if (this.DeathTimer > 5f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime * 0.2f);
				if (this.Darkness.color.a >= 1f)
				{
					if (this.Darkness.gameObject.activeInHierarchy)
					{
						this.HealthBar.parent.gameObject.SetActive(false);
						this.EXPBar.parent.gameObject.SetActive(false);
						this.Darkness.gameObject.SetActive(false);
						this.BossHealthBar.SetActive(false);
						this.BlackBG.SetActive(true);
					}
					this.TryAgainWindow.transform.localScale = Vector3.Lerp(this.TryAgainWindow.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
		}
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x001BD0E0 File Offset: 0x001BB4E0
	private void Update()
	{
		Animation component = this.Character.GetComponent<Animation>();
		if (!this.Injured && this.CanMove && !this.Cutscene)
		{
			if (this.grounded)
			{
				if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
				{
					this.TapTimer = 0.25f;
					this.Taps++;
				}
				if (this.Taps > 1)
				{
					this.speed = this.runSpeed;
				}
			}
			if (this.inputX == 0f)
			{
				this.speed = this.walkSpeed;
			}
			this.TapTimer -= Time.deltaTime;
			if (this.TapTimer < 0f)
			{
				this.Taps = 0;
			}
			if (Input.GetButtonDown("VaniaAttack") && !this.Attacking)
			{
				AudioSource.PlayClipAtPoint(this.WhipSound, base.transform.position);
				AudioSource component2 = base.GetComponent<AudioSource>();
				component2.clip = this.Voices[UnityEngine.Random.Range(0, this.Voices.Length)];
				component2.Play();
				this.WhipChain[0].transform.localScale = Vector3.zero;
				this.Attacking = true;
				this.IdleTimer = 10f;
				if (this.Crouching)
				{
					component["f02_yanvaniaCrouchAttack_00"].time = 0f;
					component.Play("f02_yanvaniaCrouchAttack_00");
				}
				else
				{
					component["f02_yanvaniaAttack_00"].time = 0f;
					component.Play("f02_yanvaniaAttack_00");
				}
				if (this.grounded)
				{
					this.moveDirection.x = 0f;
					this.inputX = 0f;
					this.speed = 0f;
				}
			}
			if (this.Attacking)
			{
				if (!this.Dangling)
				{
					this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 5f);
					this.StraightenWhip();
				}
				else
				{
					this.LoosenWhip();
					if (Input.GetAxis("VaniaHorizontal") > -0.5f && Input.GetAxis("VaniaHorizontal") < 0.5f && Input.GetAxis("VaniaVertical") > -0.5f && Input.GetAxis("VaniaVertical") < 0.5f)
					{
						component.CrossFade("f02_yanvaniaWhip_Neutral");
						if (this.Crouching)
						{
							component["f02_yanvaniaCrouchPose_00"].weight = 1f;
						}
						this.SpunUp = false;
						this.SpunDown = false;
						this.SpunRight = false;
						this.SpunLeft = false;
					}
					else
					{
						if (Input.GetAxis("VaniaVertical") > 0.5f)
						{
							if (!this.SpunUp)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = -360f;
								this.Rotation = 0f;
								this.SpunUp = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Up", 0.1f);
						}
						else
						{
							this.SpunUp = false;
						}
						if (Input.GetAxis("VaniaVertical") < -0.5f)
						{
							if (!this.SpunDown)
							{
								AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
								this.StraightenWhip();
								this.TargetRotation = 360f;
								this.Rotation = 0f;
								this.SpunDown = true;
							}
							component.CrossFade("f02_yanvaniaWhip_Down", 0.1f);
						}
						else
						{
							this.SpunDown = false;
						}
						if (Input.GetAxis("VaniaHorizontal") > 0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinRight();
							}
							else
							{
								this.SpinLeft();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunRight = false;
						}
						else
						{
							this.SpunLeft = false;
						}
						if (Input.GetAxis("VaniaHorizontal") < -0.5f)
						{
							if (this.Character.transform.localScale.x == 1f)
							{
								this.SpinLeft();
							}
							else
							{
								this.SpinRight();
							}
						}
						else if (this.Character.transform.localScale.x == 1f)
						{
							this.SpunLeft = false;
						}
						else
						{
							this.SpunRight = false;
						}
					}
					this.Rotation = Mathf.MoveTowards(this.Rotation, this.TargetRotation, Time.deltaTime * 3600f * 0.5f);
					this.WhipChain[1].transform.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
					if (!Input.GetButton("VaniaAttack"))
					{
						this.StopAttacking();
					}
				}
			}
			else
			{
				if (this.WhipCollider[1].enabled)
				{
					for (int i = 1; i < this.WhipChain.Length; i++)
					{
						this.SphereCollider[i].enabled = false;
						this.WhipCollider[i].enabled = false;
					}
				}
				this.WhipChain[0].transform.localScale = Vector3.MoveTowards(this.WhipChain[0].transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
			if ((!this.Crouching && component["f02_yanvaniaAttack_00"].time >= component["f02_yanvaniaAttack_00"].length) || (this.Crouching && component["f02_yanvaniaCrouchAttack_00"].time >= component["f02_yanvaniaCrouchAttack_00"].length))
			{
				if (Input.GetButton("VaniaAttack"))
				{
					if (this.Crouching)
					{
						component["f02_yanvaniaCrouchPose_00"].weight = 1f;
					}
					this.Dangling = true;
				}
				else
				{
					this.StopAttacking();
				}
			}
		}
		if (this.FlashTimer > 0f)
		{
			this.FlashTimer -= Time.deltaTime;
			if (!this.Red)
			{
				foreach (Material material in this.MyRenderer.materials)
				{
					material.color = new Color(1f, 0f, 0f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = true;
				}
			}
			else
			{
				foreach (Material material2 in this.MyRenderer.materials)
				{
					material2.color = new Color(1f, 1f, 1f, 1f);
				}
				this.Frames++;
				if (this.Frames == 5)
				{
					this.Frames = 0;
					this.Red = false;
				}
			}
		}
		else
		{
			this.FlashTimer = 0f;
			if (this.MyRenderer.materials[0].color != new Color(1f, 1f, 1f, 1f))
			{
				foreach (Material material3 in this.MyRenderer.materials)
				{
					material3.color = new Color(1f, 1f, 1f, 1f);
				}
			}
		}
		this.HealthBar.localScale = new Vector3(this.HealthBar.localScale.x, Mathf.Lerp(this.HealthBar.localScale.y, this.Health / this.MaxHealth, Time.deltaTime * 10f), this.HealthBar.localScale.z);
		if (this.Health > 0f)
		{
			if (this.EXP >= 100f)
			{
				this.Level++;
				if (this.Level >= 99)
				{
					this.Level = 99;
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.LevelUpEffect, this.LevelLabel.transform.position, Quaternion.identity);
					gameObject.transform.parent = this.LevelLabel.transform;
					this.MaxHealth += 20f;
					this.Health = this.MaxHealth;
					this.EXP -= 100f;
				}
				this.LevelLabel.text = this.Level.ToString();
			}
			this.EXPBar.localScale = new Vector3(this.EXPBar.localScale.x, Mathf.Lerp(this.EXPBar.localScale.y, this.EXP / 100f, Time.deltaTime * 10f), this.EXPBar.localScale.z);
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			base.transform.position = new Vector3(-31.75f, 6.51f, 0f);
			Physics.SyncTransforms();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			this.Level = 5;
			this.LevelLabel.text = this.Level.ToString();
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
			if (Time.timeScale < 0f)
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x001BDBBC File Offset: 0x001BBFBC
	private void LateUpdate()
	{
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x001BDBBE File Offset: 0x001BBFBE
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		this.contactPoint = this.hit.point;
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x001BDBD4 File Offset: 0x001BBFD4
	private void FallingDamageAlert(float fallDistance)
	{
		AudioClipPlayer.Play2D(this.LandSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
		this.Character.GetComponent<Animation>().Play("f02_yanvaniaCrouch_00");
		this.fallingDamageThreshold = this.originalThreshold;
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x001BDC28 File Offset: 0x001BC028
	private void SpinRight()
	{
		if (!this.SpunRight)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = 360f;
			this.Rotation = 0f;
			this.SpunRight = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Right", 0.1f);
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x001BDCA4 File Offset: 0x001BC0A4
	private void SpinLeft()
	{
		if (!this.SpunLeft)
		{
			AudioClipPlayer.Play2D(this.WhipSound, base.transform.position, UnityEngine.Random.Range(0.9f, 1.1f));
			this.StraightenWhip();
			this.TargetRotation = -360f;
			this.Rotation = 0f;
			this.SpunLeft = true;
		}
		this.Character.GetComponent<Animation>().CrossFade("f02_yanvaniaWhip_Left", 0.1f);
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x001BDD20 File Offset: 0x001BC120
	private void StraightenWhip()
	{
		for (int i = 1; i < this.WhipChain.Length; i++)
		{
			this.WhipCollider[i].enabled = true;
			this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
			Transform transform = this.WhipChain[i].transform;
			transform.localPosition = new Vector3(0f, -0.03f, 0f);
			transform.localEulerAngles = Vector3.zero;
		}
		this.WhipChain[1].transform.localPosition = new Vector3(0f, -0.1f, 0f);
		this.WhipTimer = 0f;
		this.Loose = false;
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x001BDDD8 File Offset: 0x001BC1D8
	private void LoosenWhip()
	{
		if (!this.Loose)
		{
			this.WhipTimer += Time.deltaTime;
			if (this.WhipTimer > 0.25f)
			{
				for (int i = 1; i < this.WhipChain.Length; i++)
				{
					this.WhipChain[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
					this.SphereCollider[i].enabled = true;
				}
				this.Loose = true;
			}
		}
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x001BDE58 File Offset: 0x001BC258
	private void StopAttacking()
	{
		this.Character.GetComponent<Animation>()["f02_yanvaniaCrouchPose_00"].weight = 0f;
		this.TargetRotation = 0f;
		this.Rotation = 0f;
		this.Attacking = false;
		this.Dangling = false;
		this.SpunUp = false;
		this.SpunDown = false;
		this.SpunRight = false;
		this.SpunLeft = false;
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x001BDEC4 File Offset: 0x001BC2C4
	public void TakeDamage(int Damage)
	{
		if (this.WhipCollider[1].enabled)
		{
			for (int i = 1; i < this.WhipChain.Length; i++)
			{
				this.SphereCollider[i].enabled = false;
				this.WhipCollider[i].enabled = false;
			}
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Injuries[UnityEngine.Random.Range(0, this.Injuries.Length)];
		component.Play();
		this.WhipChain[0].transform.localScale = Vector3.zero;
		Animation component2 = this.Character.GetComponent<Animation>();
		component2["f02_damage_25"].time = 0f;
		this.fallingDamageThreshold = 100f;
		this.moveDirection.x = 0f;
		this.RecoveryTimer = 0f;
		this.FlashTimer = 2f;
		this.Injured = true;
		this.StopAttacking();
		this.Health -= (float)Damage;
		if (this.Dracula.Health <= 0f)
		{
			this.Health = 1f;
		}
		if (this.Dracula.Health > 0f && this.Health <= 0f)
		{
			if (this.NewBlood == null)
			{
				this.MyController.enabled = false;
				this.YanvaniaCamera.StopMusic = true;
				component.clip = this.DeathSound;
				component.Play();
				this.NewBlood = UnityEngine.Object.Instantiate<GameObject>(this.DeathBlood, base.transform.position, Quaternion.identity);
				this.NewBlood.transform.parent = this.Hips;
				this.NewBlood.transform.localPosition = Vector3.zero;
				component2.CrossFade("f02_yanvaniaDeath_00");
				this.CanMove = false;
			}
			this.Health = 0f;
		}
	}

	// Token: 0x04003C94 RID: 15508
	private GameObject NewBlood;

	// Token: 0x04003C95 RID: 15509
	public YanvaniaCameraScript YanvaniaCamera;

	// Token: 0x04003C96 RID: 15510
	public InputManagerScript InputManager;

	// Token: 0x04003C97 RID: 15511
	public YanvaniaDraculaScript Dracula;

	// Token: 0x04003C98 RID: 15512
	public CharacterController MyController;

	// Token: 0x04003C99 RID: 15513
	public GameObject BossHealthBar;

	// Token: 0x04003C9A RID: 15514
	public GameObject LevelUpEffect;

	// Token: 0x04003C9B RID: 15515
	public GameObject DeathBlood;

	// Token: 0x04003C9C RID: 15516
	public GameObject Character;

	// Token: 0x04003C9D RID: 15517
	public GameObject BlackBG;

	// Token: 0x04003C9E RID: 15518
	public GameObject TextBox;

	// Token: 0x04003C9F RID: 15519
	public Renderer MyRenderer;

	// Token: 0x04003CA0 RID: 15520
	public Transform TryAgainWindow;

	// Token: 0x04003CA1 RID: 15521
	public Transform HealthBar;

	// Token: 0x04003CA2 RID: 15522
	public Transform EXPBar;

	// Token: 0x04003CA3 RID: 15523
	public Transform Hips;

	// Token: 0x04003CA4 RID: 15524
	public Transform TrailStart;

	// Token: 0x04003CA5 RID: 15525
	public Transform TrailEnd;

	// Token: 0x04003CA6 RID: 15526
	public UITexture Photograph;

	// Token: 0x04003CA7 RID: 15527
	public UILabel LevelLabel;

	// Token: 0x04003CA8 RID: 15528
	public UISprite Darkness;

	// Token: 0x04003CA9 RID: 15529
	public Collider[] SphereCollider;

	// Token: 0x04003CAA RID: 15530
	public Collider[] WhipCollider;

	// Token: 0x04003CAB RID: 15531
	public Transform[] WhipChain;

	// Token: 0x04003CAC RID: 15532
	public AudioClip[] Voices;

	// Token: 0x04003CAD RID: 15533
	public AudioClip[] Injuries;

	// Token: 0x04003CAE RID: 15534
	public AudioClip DeathSound;

	// Token: 0x04003CAF RID: 15535
	public AudioClip LandSound;

	// Token: 0x04003CB0 RID: 15536
	public AudioClip WhipSound;

	// Token: 0x04003CB1 RID: 15537
	public bool Attacking;

	// Token: 0x04003CB2 RID: 15538
	public bool Crouching;

	// Token: 0x04003CB3 RID: 15539
	public bool Dangling;

	// Token: 0x04003CB4 RID: 15540
	public bool EnterCutscene;

	// Token: 0x04003CB5 RID: 15541
	public bool Cutscene;

	// Token: 0x04003CB6 RID: 15542
	public bool CanMove;

	// Token: 0x04003CB7 RID: 15543
	public bool Injured;

	// Token: 0x04003CB8 RID: 15544
	public bool Loose;

	// Token: 0x04003CB9 RID: 15545
	public bool Red;

	// Token: 0x04003CBA RID: 15546
	public bool SpunUp;

	// Token: 0x04003CBB RID: 15547
	public bool SpunDown;

	// Token: 0x04003CBC RID: 15548
	public bool SpunRight;

	// Token: 0x04003CBD RID: 15549
	public bool SpunLeft;

	// Token: 0x04003CBE RID: 15550
	public float TargetRotation;

	// Token: 0x04003CBF RID: 15551
	public float Rotation;

	// Token: 0x04003CC0 RID: 15552
	public float RecoveryTimer;

	// Token: 0x04003CC1 RID: 15553
	public float DeathTimer;

	// Token: 0x04003CC2 RID: 15554
	public float FlashTimer;

	// Token: 0x04003CC3 RID: 15555
	public float IdleTimer;

	// Token: 0x04003CC4 RID: 15556
	public float WhipTimer;

	// Token: 0x04003CC5 RID: 15557
	public float TapTimer;

	// Token: 0x04003CC6 RID: 15558
	public float PreviousY;

	// Token: 0x04003CC7 RID: 15559
	public float MaxHealth = 100f;

	// Token: 0x04003CC8 RID: 15560
	public float Health = 100f;

	// Token: 0x04003CC9 RID: 15561
	public float EXP;

	// Token: 0x04003CCA RID: 15562
	public int Frames;

	// Token: 0x04003CCB RID: 15563
	public int Level;

	// Token: 0x04003CCC RID: 15564
	public int Taps;

	// Token: 0x04003CCD RID: 15565
	public float walkSpeed = 6f;

	// Token: 0x04003CCE RID: 15566
	public float runSpeed = 11f;

	// Token: 0x04003CCF RID: 15567
	public bool limitDiagonalSpeed = true;

	// Token: 0x04003CD0 RID: 15568
	public bool toggleRun;

	// Token: 0x04003CD1 RID: 15569
	public float jumpSpeed = 8f;

	// Token: 0x04003CD2 RID: 15570
	public float gravity = 20f;

	// Token: 0x04003CD3 RID: 15571
	public float fallingDamageThreshold = 10f;

	// Token: 0x04003CD4 RID: 15572
	public bool slideWhenOverSlopeLimit;

	// Token: 0x04003CD5 RID: 15573
	public bool slideOnTaggedObjects;

	// Token: 0x04003CD6 RID: 15574
	public float slideSpeed = 12f;

	// Token: 0x04003CD7 RID: 15575
	public bool airControl;

	// Token: 0x04003CD8 RID: 15576
	public float antiBumpFactor = 0.75f;

	// Token: 0x04003CD9 RID: 15577
	public int antiBunnyHopFactor = 1;

	// Token: 0x04003CDA RID: 15578
	private Vector3 moveDirection = Vector3.zero;

	// Token: 0x04003CDB RID: 15579
	public bool grounded;

	// Token: 0x04003CDC RID: 15580
	private CharacterController controller;

	// Token: 0x04003CDD RID: 15581
	private Transform myTransform;

	// Token: 0x04003CDE RID: 15582
	private float speed;

	// Token: 0x04003CDF RID: 15583
	private RaycastHit hit;

	// Token: 0x04003CE0 RID: 15584
	private float fallStartLevel;

	// Token: 0x04003CE1 RID: 15585
	private bool falling;

	// Token: 0x04003CE2 RID: 15586
	private float slideLimit;

	// Token: 0x04003CE3 RID: 15587
	private float rayDistance;

	// Token: 0x04003CE4 RID: 15588
	private Vector3 contactPoint;

	// Token: 0x04003CE5 RID: 15589
	private bool playerControl;

	// Token: 0x04003CE6 RID: 15590
	private int jumpTimer;

	// Token: 0x04003CE7 RID: 15591
	private float originalThreshold;

	// Token: 0x04003CE8 RID: 15592
	public float inputX;
}
