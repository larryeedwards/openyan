using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class RPG_Camera : MonoBehaviour
{
	// Token: 0x060016C9 RID: 5833 RVA: 0x000AE78F File Offset: 0x000ACB8F
	private void Awake()
	{
		RPG_Camera.instance = this;
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000AE798 File Offset: 0x000ACB98
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		RPG_Camera.MainCamera = base.GetComponent<Camera>();
		this.distance = Mathf.Clamp(this.distance, 0.05f, this.distanceMax);
		this.desiredDistance = this.distance;
		RPG_Camera.halfFieldOfView = RPG_Camera.MainCamera.fieldOfView / 2f * 0.0174532924f;
		RPG_Camera.planeAspect = RPG_Camera.MainCamera.aspect;
		RPG_Camera.halfPlaneHeight = RPG_Camera.MainCamera.nearClipPlane * Mathf.Tan(RPG_Camera.halfFieldOfView);
		RPG_Camera.halfPlaneWidth = RPG_Camera.halfPlaneHeight * RPG_Camera.planeAspect;
		this.UpdateRotation();
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000AE840 File Offset: 0x000ACC40
	public void UpdateRotation()
	{
		this.mouseX = this.cameraPivot.transform.eulerAngles.y;
		this.mouseY = 15f;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000AE878 File Offset: 0x000ACC78
	public static void CameraSetup()
	{
		GameObject gameObject;
		if (RPG_Camera.MainCamera != null)
		{
			gameObject = RPG_Camera.MainCamera.gameObject;
		}
		else
		{
			gameObject = new GameObject("Main Camera");
			gameObject.AddComponent<Camera>();
			gameObject.tag = "MainCamera";
		}
		if (!gameObject.GetComponent("RPG_Camera"))
		{
			gameObject.AddComponent<RPG_Camera>();
		}
		RPG_Camera rpg_Camera = gameObject.GetComponent("RPG_Camera") as RPG_Camera;
		GameObject gameObject2 = GameObject.Find("cameraPivot");
		rpg_Camera.cameraPivot = gameObject2.transform;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000AE908 File Offset: 0x000ACD08
	private void LateUpdate()
	{
		if (Time.deltaTime > 0f)
		{
			if (this.cameraPivot == null)
			{
				this.cameraPivot = GameObject.Find("CameraPivot").transform;
				return;
			}
			this.GetInput();
			this.GetDesiredPosition();
			this.PositionUpdate();
			this.CharacterFade();
		}
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000AE964 File Offset: 0x000ACD64
	public void GetInput()
	{
		if ((double)this.distance > 0.1)
		{
			this.camBottom = Physics.Linecast(base.transform.position, base.transform.position - Vector3.up * this.camBottomDistance);
		}
		bool flag = this.camBottom && base.transform.position.y - this.cameraPivot.transform.position.y <= 0f;
		this.mouseX += Input.GetAxis("Mouse X") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
		if (flag)
		{
			if (Input.GetAxis("Mouse Y") < 0f)
			{
				if (!OptionGlobals.InvertAxis)
				{
					this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
				}
				else
				{
					this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
				}
			}
			else if (!OptionGlobals.InvertAxis)
			{
				this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
			}
			else
			{
				this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
			}
		}
		else if (!OptionGlobals.InvertAxis)
		{
			this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
		}
		else
		{
			this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * (float)OptionGlobals.Sensitivity * 10f;
		}
		this.mouseY = this.ClampAngle(this.mouseY, -89.5f, 89.5f);
		this.mouseXSmooth = Mathf.SmoothDamp(this.mouseXSmooth, this.mouseX, ref this.mouseXVel, this.mouseSmoothingFactor);
		this.mouseYSmooth = Mathf.SmoothDamp(this.mouseYSmooth, this.mouseY, ref this.mouseYVel, this.mouseSmoothingFactor);
		if (flag)
		{
			this.mouseYMin = this.mouseY;
		}
		else
		{
			this.mouseYMin = -89.5f;
		}
		this.mouseYSmooth = this.ClampAngle(this.mouseYSmooth, this.mouseYMin, this.mouseYMax);
		this.desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * this.mouseScroll;
		if (this.desiredDistance > this.distanceMax)
		{
			this.desiredDistance = this.distanceMax;
		}
		if (this.desiredDistance < this.distanceMin)
		{
			this.desiredDistance = this.distanceMin;
		}
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x000AED38 File Offset: 0x000AD138
	public void GetDesiredPosition()
	{
		this.distance = this.desiredDistance;
		this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
		this.constraint = false;
		float num = this.CheckCameraClipPlane(this.cameraPivot.position, this.desiredPosition);
		if (num != -1f)
		{
			this.distance = num;
			this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
			this.constraint = true;
		}
		if (RPG_Camera.MainCamera == null)
		{
			RPG_Camera.MainCamera = base.GetComponent<Camera>();
		}
		this.distance -= RPG_Camera.MainCamera.nearClipPlane;
		if (this.lastDistance < this.distance || !this.constraint)
		{
			this.distance = Mathf.SmoothDamp(this.lastDistance, this.distance, ref this.distanceVel, this.camDistanceSpeed);
		}
		if ((double)this.distance < 0.05)
		{
			this.distance = 0.05f;
		}
		this.lastDistance = this.distance;
		this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x000AEE80 File Offset: 0x000AD280
	public void PositionUpdate()
	{
		base.transform.position = this.desiredPosition;
		if ((double)this.distance > 0.05)
		{
			base.transform.LookAt(this.cameraPivot);
		}
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000AEEBC File Offset: 0x000AD2BC
	private void CharacterFade()
	{
		if (RPG_Animation.instance == null)
		{
			return;
		}
		if (this.distance < this.firstPersonThreshold)
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = false;
		}
		else if (this.distance < this.characterFadeThreshold)
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = true;
			float num = 1f - (this.characterFadeThreshold - this.distance) / (this.characterFadeThreshold - this.firstPersonThreshold);
			if (RPG_Animation.instance.GetComponent<Renderer>().material.color.a != num)
			{
				RPG_Animation.instance.GetComponent<Renderer>().material.color = new Color(RPG_Animation.instance.GetComponent<Renderer>().material.color.r, RPG_Animation.instance.GetComponent<Renderer>().material.color.g, RPG_Animation.instance.GetComponent<Renderer>().material.color.b, num);
			}
		}
		else
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = true;
			if (RPG_Animation.instance.GetComponent<Renderer>().material.color.a != 1f)
			{
				RPG_Animation.instance.GetComponent<Renderer>().material.color = new Color(RPG_Animation.instance.GetComponent<Renderer>().material.color.r, RPG_Animation.instance.GetComponent<Renderer>().material.color.g, RPG_Animation.instance.GetComponent<Renderer>().material.color.b, 1f);
			}
		}
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000AF088 File Offset: 0x000AD488
	private Vector3 GetCameraPosition(float xAxis, float yAxis, float distance)
	{
		Vector3 point = new Vector3(0f, 0f, -distance);
		Quaternion rotation = Quaternion.Euler(xAxis, yAxis, 0f);
		return this.cameraPivot.position + rotation * point;
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000AF0CC File Offset: 0x000AD4CC
	private float CheckCameraClipPlane(Vector3 from, Vector3 to)
	{
		float num = -1f;
		RPG_Camera.ClipPlaneVertexes clipPlaneAt = RPG_Camera.GetClipPlaneAt(to);
		int layerMask = 257;
		if (RPG_Camera.MainCamera != null)
		{
			RaycastHit raycastHit;
			if (Physics.Linecast(from, to, out raycastHit, layerMask))
			{
				num = raycastHit.distance - RPG_Camera.MainCamera.nearClipPlane;
			}
			if (Physics.Linecast(from - base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.UpperLeft, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point + base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from + base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.UpperRight, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point - base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from - base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.LowerLeft, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point + base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from + base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.LowerRight, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point - base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
		}
		return num;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x000AF3AC File Offset: 0x000AD7AC
	private float ClampAngle(float angle, float min, float max)
	{
		while (angle < -360f || angle > 360f)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000AF404 File Offset: 0x000AD804
	public static RPG_Camera.ClipPlaneVertexes GetClipPlaneAt(Vector3 pos)
	{
		RPG_Camera.ClipPlaneVertexes result = default(RPG_Camera.ClipPlaneVertexes);
		if (RPG_Camera.MainCamera == null)
		{
			return result;
		}
		Transform transform = RPG_Camera.MainCamera.transform;
		float nearClipPlane = RPG_Camera.MainCamera.nearClipPlane;
		result.UpperLeft = pos - transform.right * RPG_Camera.halfPlaneWidth;
		result.UpperLeft += transform.up * RPG_Camera.halfPlaneHeight;
		result.UpperLeft += transform.forward * nearClipPlane;
		result.UpperRight = pos + transform.right * RPG_Camera.halfPlaneWidth;
		result.UpperRight += transform.up * RPG_Camera.halfPlaneHeight;
		result.UpperRight += transform.forward * nearClipPlane;
		result.LowerLeft = pos - transform.right * RPG_Camera.halfPlaneWidth;
		result.LowerLeft -= transform.up * RPG_Camera.halfPlaneHeight;
		result.LowerLeft += transform.forward * nearClipPlane;
		result.LowerRight = pos + transform.right * RPG_Camera.halfPlaneWidth;
		result.LowerRight -= transform.up * RPG_Camera.halfPlaneHeight;
		result.LowerRight += transform.forward * nearClipPlane;
		return result;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000AF5B8 File Offset: 0x000AD9B8
	public void RotateWithCharacter()
	{
		float num = Input.GetAxis("Horizontal") * RPG_Controller.instance.turnSpeed;
		this.mouseX += num;
	}

	// Token: 0x0400146D RID: 5229
	public static RPG_Camera instance;

	// Token: 0x0400146E RID: 5230
	public static Camera MainCamera;

	// Token: 0x0400146F RID: 5231
	public Transform cameraPivot;

	// Token: 0x04001470 RID: 5232
	public float distance = 5f;

	// Token: 0x04001471 RID: 5233
	public float distanceMax = 30f;

	// Token: 0x04001472 RID: 5234
	public float distanceMin = 2f;

	// Token: 0x04001473 RID: 5235
	public float mouseSpeed = 8f;

	// Token: 0x04001474 RID: 5236
	public float mouseScroll = 15f;

	// Token: 0x04001475 RID: 5237
	public float mouseSmoothingFactor = 0.08f;

	// Token: 0x04001476 RID: 5238
	public float camDistanceSpeed = 0.7f;

	// Token: 0x04001477 RID: 5239
	public float camBottomDistance = 1f;

	// Token: 0x04001478 RID: 5240
	public float firstPersonThreshold = 0.8f;

	// Token: 0x04001479 RID: 5241
	public float characterFadeThreshold = 1.8f;

	// Token: 0x0400147A RID: 5242
	public Vector3 desiredPosition;

	// Token: 0x0400147B RID: 5243
	public float desiredDistance;

	// Token: 0x0400147C RID: 5244
	private float lastDistance;

	// Token: 0x0400147D RID: 5245
	public float mouseX;

	// Token: 0x0400147E RID: 5246
	public float mouseXSmooth;

	// Token: 0x0400147F RID: 5247
	private float mouseXVel;

	// Token: 0x04001480 RID: 5248
	public float mouseY;

	// Token: 0x04001481 RID: 5249
	public float mouseYSmooth;

	// Token: 0x04001482 RID: 5250
	private float mouseYVel;

	// Token: 0x04001483 RID: 5251
	private float mouseYMin = -89.5f;

	// Token: 0x04001484 RID: 5252
	private float mouseYMax = 89.5f;

	// Token: 0x04001485 RID: 5253
	private float distanceVel;

	// Token: 0x04001486 RID: 5254
	private bool camBottom;

	// Token: 0x04001487 RID: 5255
	private bool constraint;

	// Token: 0x04001488 RID: 5256
	private static float halfFieldOfView;

	// Token: 0x04001489 RID: 5257
	private static float planeAspect;

	// Token: 0x0400148A RID: 5258
	private static float halfPlaneHeight;

	// Token: 0x0400148B RID: 5259
	private static float halfPlaneWidth;

	// Token: 0x02000316 RID: 790
	public struct ClipPlaneVertexes
	{
		// Token: 0x0400148C RID: 5260
		public Vector3 UpperLeft;

		// Token: 0x0400148D RID: 5261
		public Vector3 UpperRight;

		// Token: 0x0400148E RID: 5262
		public Vector3 LowerLeft;

		// Token: 0x0400148F RID: 5263
		public Vector3 LowerRight;
	}
}
