using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
public abstract class UIRect : MonoBehaviour
{
	// Token: 0x170001EB RID: 491
	// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0008336B File Offset: 0x0008176B
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x060010AA RID: 4266 RVA: 0x00083390 File Offset: 0x00081790
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x060010AB RID: 4267 RVA: 0x000833B5 File Offset: 0x000817B5
	public Camera anchorCamera
	{
		get
		{
			if (!this.mAnchorsCached)
			{
				this.ResetAnchors();
			}
			return this.mCam;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x060010AC RID: 4268 RVA: 0x000833D0 File Offset: 0x000817D0
	public bool isFullyAnchored
	{
		get
		{
			return this.leftAnchor.target && this.rightAnchor.target && this.topAnchor.target && this.bottomAnchor.target;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x060010AD RID: 4269 RVA: 0x0008342F File Offset: 0x0008182F
	public virtual bool isAnchoredHorizontally
	{
		get
		{
			return this.leftAnchor.target || this.rightAnchor.target;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x060010AE RID: 4270 RVA: 0x00083459 File Offset: 0x00081859
	public virtual bool isAnchoredVertically
	{
		get
		{
			return this.bottomAnchor.target || this.topAnchor.target;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x060010AF RID: 4271 RVA: 0x00083483 File Offset: 0x00081883
	public virtual bool canBeAnchored
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00083486 File Offset: 0x00081886
	public UIRect parent
	{
		get
		{
			if (!this.mParentFound)
			{
				this.mParentFound = true;
				this.mParent = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
			}
			return this.mParent;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060010B1 RID: 4273 RVA: 0x000834B8 File Offset: 0x000818B8
	public UIRoot root
	{
		get
		{
			if (this.parent != null)
			{
				return this.mParent.root;
			}
			if (!this.mRootSet)
			{
				this.mRootSet = true;
				this.mRoot = NGUITools.FindInParents<UIRoot>(this.cachedTransform);
			}
			return this.mRoot;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0008350C File Offset: 0x0008190C
	public bool isAnchored
	{
		get
		{
			return (this.leftAnchor.target || this.rightAnchor.target || this.topAnchor.target || this.bottomAnchor.target) && this.canBeAnchored;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x060010B3 RID: 4275
	// (set) Token: 0x060010B4 RID: 4276
	public abstract float alpha { get; set; }

	// Token: 0x060010B5 RID: 4277
	public abstract float CalculateFinalAlpha(int frameID);

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x060010B6 RID: 4278
	public abstract Vector3[] localCorners { get; }

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060010B7 RID: 4279
	public abstract Vector3[] worldCorners { get; }

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00083578 File Offset: 0x00081978
	protected float cameraRayDistance
	{
		get
		{
			if (this.anchorCamera == null)
			{
				return 0f;
			}
			if (!this.mCam.orthographic)
			{
				Transform cachedTransform = this.cachedTransform;
				Transform transform = this.mCam.transform;
				Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
				Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
				float result;
				if (plane.Raycast(ray, out result))
				{
					return result;
				}
			}
			return Mathf.Lerp(this.mCam.nearClipPlane, this.mCam.farClipPlane, 0.5f);
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0008362C File Offset: 0x00081A2C
	public virtual void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		if (includeChildren)
		{
			for (int i = 0; i < this.mChildren.size; i++)
			{
				this.mChildren.buffer[i].Invalidate(true);
			}
		}
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00083678 File Offset: 0x00081A78
	public virtual Vector3[] GetSides(Transform relativeTo)
	{
		if (this.anchorCamera != null)
		{
			return this.mCam.GetSides(this.cameraRayDistance, relativeTo);
		}
		Vector3 position = this.cachedTransform.position;
		for (int i = 0; i < 4; i++)
		{
			UIRect.mSides[i] = position;
		}
		if (relativeTo != null)
		{
			for (int j = 0; j < 4; j++)
			{
				UIRect.mSides[j] = relativeTo.InverseTransformPoint(UIRect.mSides[j]);
			}
		}
		return UIRect.mSides;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00083724 File Offset: 0x00081B24
	protected Vector3 GetLocalPos(UIRect.AnchorPoint ac, Transform trans)
	{
		if (ac.targetCam == null)
		{
			this.FindCameraFor(ac);
		}
		if (this.anchorCamera == null || ac.targetCam == null)
		{
			return this.cachedTransform.localPosition;
		}
		Rect rect = ac.targetCam.rect;
		Vector3 vector = ac.targetCam.WorldToViewportPoint(ac.target.position);
		Vector3 vector2 = new Vector3(vector.x * rect.width + rect.x, vector.y * rect.height + rect.y, vector.z);
		vector2 = this.mCam.ViewportToWorldPoint(vector2);
		if (trans != null)
		{
			vector2 = trans.InverseTransformPoint(vector2);
		}
		vector2.x = Mathf.Floor(vector2.x + 0.5f);
		vector2.y = Mathf.Floor(vector2.y + 0.5f);
		return vector2;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0008382A File Offset: 0x00081C2A
	protected virtual void OnEnable()
	{
		this.mUpdateFrame = -1;
		if (this.updateAnchors == UIRect.AnchorUpdate.OnEnable)
		{
			this.mAnchorsCached = false;
			this.mUpdateAnchors = true;
		}
		if (this.mStarted)
		{
			this.OnInit();
		}
		this.mUpdateFrame = -1;
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00083864 File Offset: 0x00081C64
	protected virtual void OnInit()
	{
		this.mChanged = true;
		this.mRootSet = false;
		this.mParentFound = false;
		if (this.parent != null)
		{
			this.mParent.mChildren.Add(this);
		}
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0008389D File Offset: 0x00081C9D
	protected virtual void OnDisable()
	{
		if (this.mParent)
		{
			this.mParent.mChildren.Remove(this);
		}
		this.mParent = null;
		this.mRoot = null;
		this.mRootSet = false;
		this.mParentFound = false;
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x000838DD File Offset: 0x00081CDD
	protected virtual void Awake()
	{
		this.mStarted = false;
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x000838FE File Offset: 0x00081CFE
	protected void Start()
	{
		this.mStarted = true;
		this.OnInit();
		this.OnStart();
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00083914 File Offset: 0x00081D14
	public void Update()
	{
		if (!this.mAnchorsCached)
		{
			this.ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (this.mUpdateFrame != frameCount)
		{
			if (this.updateAnchors == UIRect.AnchorUpdate.OnUpdate || this.mUpdateAnchors)
			{
				this.UpdateAnchorsInternal(frameCount);
			}
			this.OnUpdate();
		}
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00083968 File Offset: 0x00081D68
	protected void UpdateAnchorsInternal(int frame)
	{
		this.mUpdateFrame = frame;
		this.mUpdateAnchors = false;
		bool flag = false;
		if (this.leftAnchor.target)
		{
			flag = true;
			if (this.leftAnchor.rect != null && this.leftAnchor.rect.mUpdateFrame != frame)
			{
				this.leftAnchor.rect.Update();
			}
		}
		if (this.bottomAnchor.target)
		{
			flag = true;
			if (this.bottomAnchor.rect != null && this.bottomAnchor.rect.mUpdateFrame != frame)
			{
				this.bottomAnchor.rect.Update();
			}
		}
		if (this.rightAnchor.target)
		{
			flag = true;
			if (this.rightAnchor.rect != null && this.rightAnchor.rect.mUpdateFrame != frame)
			{
				this.rightAnchor.rect.Update();
			}
		}
		if (this.topAnchor.target)
		{
			flag = true;
			if (this.topAnchor.rect != null && this.topAnchor.rect.mUpdateFrame != frame)
			{
				this.topAnchor.rect.Update();
			}
		}
		if (flag)
		{
			this.OnAnchor();
		}
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00083ADD File Offset: 0x00081EDD
	public void UpdateAnchors()
	{
		if (this.isAnchored)
		{
			this.mUpdateFrame = -1;
			this.mUpdateAnchors = true;
			this.UpdateAnchorsInternal(Time.frameCount);
		}
	}

	// Token: 0x060010C4 RID: 4292
	protected abstract void OnAnchor();

	// Token: 0x060010C5 RID: 4293 RVA: 0x00083B03 File Offset: 0x00081F03
	public void SetAnchor(Transform t)
	{
		this.leftAnchor.target = t;
		this.rightAnchor.target = t;
		this.topAnchor.target = t;
		this.bottomAnchor.target = t;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00083B44 File Offset: 0x00081F44
	public void SetAnchor(GameObject go)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00083BA8 File Offset: 0x00081FA8
	public void SetAnchor(GameObject go, int left, int bottom, int right, int top)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = 0f;
		this.rightAnchor.relative = 1f;
		this.bottomAnchor.relative = 0f;
		this.topAnchor.relative = 1f;
		this.leftAnchor.absolute = left;
		this.rightAnchor.absolute = right;
		this.bottomAnchor.absolute = bottom;
		this.topAnchor.absolute = top;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00083C7C File Offset: 0x0008207C
	public void SetAnchor(GameObject go, float left, float bottom, float right, float top)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = 0;
		this.rightAnchor.absolute = 0;
		this.bottomAnchor.absolute = 0;
		this.topAnchor.absolute = 0;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x00083D40 File Offset: 0x00082140
	public void SetAnchor(GameObject go, float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform target = (!(go != null)) ? null : go.transform;
		this.leftAnchor.target = target;
		this.rightAnchor.target = target;
		this.topAnchor.target = target;
		this.bottomAnchor.target = target;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x00083E08 File Offset: 0x00082208
	public void SetAnchor(float left, int leftOffset, float bottom, int bottomOffset, float right, int rightOffset, float top, int topOffset)
	{
		Transform parent = this.cachedTransform.parent;
		this.leftAnchor.target = parent;
		this.rightAnchor.target = parent;
		this.topAnchor.target = parent;
		this.bottomAnchor.target = parent;
		this.leftAnchor.relative = left;
		this.rightAnchor.relative = right;
		this.bottomAnchor.relative = bottom;
		this.topAnchor.relative = top;
		this.leftAnchor.absolute = leftOffset;
		this.rightAnchor.absolute = rightOffset;
		this.bottomAnchor.absolute = bottomOffset;
		this.topAnchor.absolute = topOffset;
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x00083EC4 File Offset: 0x000822C4
	public void SetScreenRect(int left, int top, int width, int height)
	{
		this.SetAnchor(0f, left, 1f, -top - height, 0f, left + width, 1f, -top);
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x00083EF8 File Offset: 0x000822F8
	public void ResetAnchors()
	{
		this.mAnchorsCached = true;
		this.leftAnchor.rect = ((!this.leftAnchor.target) ? null : this.leftAnchor.target.GetComponent<UIRect>());
		this.bottomAnchor.rect = ((!this.bottomAnchor.target) ? null : this.bottomAnchor.target.GetComponent<UIRect>());
		this.rightAnchor.rect = ((!this.rightAnchor.target) ? null : this.rightAnchor.target.GetComponent<UIRect>());
		this.topAnchor.rect = ((!this.topAnchor.target) ? null : this.topAnchor.target.GetComponent<UIRect>());
		this.mCam = NGUITools.FindCameraForLayer(this.cachedGameObject.layer);
		this.FindCameraFor(this.leftAnchor);
		this.FindCameraFor(this.bottomAnchor);
		this.FindCameraFor(this.rightAnchor);
		this.FindCameraFor(this.topAnchor);
		this.mUpdateAnchors = true;
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00084031 File Offset: 0x00082431
	public void ResetAndUpdateAnchors()
	{
		this.ResetAnchors();
		this.UpdateAnchors();
	}

	// Token: 0x060010CE RID: 4302
	public abstract void SetRect(float x, float y, float width, float height);

	// Token: 0x060010CF RID: 4303 RVA: 0x00084040 File Offset: 0x00082440
	private void FindCameraFor(UIRect.AnchorPoint ap)
	{
		if (ap.target == null || ap.rect != null)
		{
			ap.targetCam = null;
		}
		else
		{
			ap.targetCam = NGUITools.FindCameraForLayer(ap.target.gameObject.layer);
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x00084098 File Offset: 0x00082498
	public virtual void ParentHasChanged()
	{
		this.mParentFound = false;
		UIRect y = NGUITools.FindInParents<UIRect>(this.cachedTransform.parent);
		if (this.mParent != y)
		{
			if (this.mParent)
			{
				this.mParent.mChildren.Remove(this);
			}
			this.mParent = y;
			if (this.mParent)
			{
				this.mParent.mChildren.Add(this);
			}
			this.mRootSet = false;
		}
	}

	// Token: 0x060010D1 RID: 4305
	protected abstract void OnStart();

	// Token: 0x060010D2 RID: 4306 RVA: 0x0008411F File Offset: 0x0008251F
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x04000E8E RID: 3726
	public UIRect.AnchorPoint leftAnchor = new UIRect.AnchorPoint();

	// Token: 0x04000E8F RID: 3727
	public UIRect.AnchorPoint rightAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04000E90 RID: 3728
	public UIRect.AnchorPoint bottomAnchor = new UIRect.AnchorPoint();

	// Token: 0x04000E91 RID: 3729
	public UIRect.AnchorPoint topAnchor = new UIRect.AnchorPoint(1f);

	// Token: 0x04000E92 RID: 3730
	public UIRect.AnchorUpdate updateAnchors = UIRect.AnchorUpdate.OnUpdate;

	// Token: 0x04000E93 RID: 3731
	[NonSerialized]
	protected GameObject mGo;

	// Token: 0x04000E94 RID: 3732
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x04000E95 RID: 3733
	[NonSerialized]
	protected BetterList<UIRect> mChildren = new BetterList<UIRect>();

	// Token: 0x04000E96 RID: 3734
	[NonSerialized]
	protected bool mChanged = true;

	// Token: 0x04000E97 RID: 3735
	[NonSerialized]
	protected bool mParentFound;

	// Token: 0x04000E98 RID: 3736
	[NonSerialized]
	private bool mUpdateAnchors = true;

	// Token: 0x04000E99 RID: 3737
	[NonSerialized]
	private int mUpdateFrame = -1;

	// Token: 0x04000E9A RID: 3738
	[NonSerialized]
	private bool mAnchorsCached;

	// Token: 0x04000E9B RID: 3739
	[NonSerialized]
	private UIRoot mRoot;

	// Token: 0x04000E9C RID: 3740
	[NonSerialized]
	private UIRect mParent;

	// Token: 0x04000E9D RID: 3741
	[NonSerialized]
	private bool mRootSet;

	// Token: 0x04000E9E RID: 3742
	[NonSerialized]
	protected Camera mCam;

	// Token: 0x04000E9F RID: 3743
	protected bool mStarted;

	// Token: 0x04000EA0 RID: 3744
	[NonSerialized]
	public float finalAlpha = 1f;

	// Token: 0x04000EA1 RID: 3745
	protected static Vector3[] mSides = new Vector3[4];

	// Token: 0x02000223 RID: 547
	[Serializable]
	public class AnchorPoint
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x0008412E File Offset: 0x0008252E
		public AnchorPoint()
		{
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00084136 File Offset: 0x00082536
		public AnchorPoint(float relative)
		{
			this.relative = relative;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00084145 File Offset: 0x00082545
		public void Set(float relative, float absolute)
		{
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00084160 File Offset: 0x00082560
		public void Set(Transform target, float relative, float absolute)
		{
			this.target = target;
			this.relative = relative;
			this.absolute = Mathf.FloorToInt(absolute + 0.5f);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00084182 File Offset: 0x00082582
		public void SetToNearest(float abs0, float abs1, float abs2)
		{
			this.SetToNearest(0f, 0.5f, 1f, abs0, abs1, abs2);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0008419C File Offset: 0x0008259C
		public void SetToNearest(float rel0, float rel1, float rel2, float abs0, float abs1, float abs2)
		{
			float num = Mathf.Abs(abs0);
			float num2 = Mathf.Abs(abs1);
			float num3 = Mathf.Abs(abs2);
			if (num < num2 && num < num3)
			{
				this.Set(rel0, abs0);
			}
			else if (num2 < num && num2 < num3)
			{
				this.Set(rel1, abs1);
			}
			else
			{
				this.Set(rel2, abs2);
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00084204 File Offset: 0x00082604
		public void SetHorizontal(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = this.target.position;
				if (parent != null)
				{
					position = parent.InverseTransformPoint(position);
				}
				this.absolute = Mathf.FloorToInt(localPos - position.x + 0.5f);
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000842A8 File Offset: 0x000826A8
		public void SetVertical(Transform parent, float localPos)
		{
			if (this.rect)
			{
				Vector3[] sides = this.rect.GetSides(parent);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, this.relative);
				this.absolute = Mathf.FloorToInt(localPos - num + 0.5f);
			}
			else
			{
				Vector3 position = this.target.position;
				if (parent != null)
				{
					position = parent.InverseTransformPoint(position);
				}
				this.absolute = Mathf.FloorToInt(localPos - position.y + 0.5f);
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0008434C File Offset: 0x0008274C
		public Vector3[] GetSides(Transform relativeTo)
		{
			if (this.target != null)
			{
				if (this.rect != null)
				{
					return this.rect.GetSides(relativeTo);
				}
				Camera component = this.target.GetComponent<Camera>();
				if (component != null)
				{
					return component.GetSides(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x04000EA2 RID: 3746
		public Transform target;

		// Token: 0x04000EA3 RID: 3747
		public float relative;

		// Token: 0x04000EA4 RID: 3748
		public int absolute;

		// Token: 0x04000EA5 RID: 3749
		[NonSerialized]
		public UIRect rect;

		// Token: 0x04000EA6 RID: 3750
		[NonSerialized]
		public Camera targetCam;
	}

	// Token: 0x02000224 RID: 548
	public enum AnchorUpdate
	{
		// Token: 0x04000EA8 RID: 3752
		OnEnable,
		// Token: 0x04000EA9 RID: 3753
		OnUpdate,
		// Token: 0x04000EAA RID: 3754
		OnStart
	}
}
