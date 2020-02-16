using System;
using System.Collections;
using System.Collections.Generic;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000292 RID: 658
[AddComponentMenu("")]
public class AmplifyMotionObjectBase : MonoBehaviour
{
	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06001511 RID: 5393 RVA: 0x000A1E89 File Offset: 0x000A0289
	internal bool FixedStep
	{
		get
		{
			return this.m_fixedStep;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06001512 RID: 5394 RVA: 0x000A1E91 File Offset: 0x000A0291
	internal int ObjectId
	{
		get
		{
			return this.m_objectId;
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06001513 RID: 5395 RVA: 0x000A1E99 File Offset: 0x000A0299
	public ObjectType Type
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000A1EA4 File Offset: 0x000A02A4
	internal void RegisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		if ((component.cullingMask & 1 << base.gameObject.layer) != 0 && !this.m_states.ContainsKey(component))
		{
			MotionState value;
			switch (this.m_type)
			{
			case ObjectType.Solid:
				value = new SolidState(camera, this);
				break;
			case ObjectType.Skinned:
				value = new SkinnedState(camera, this);
				break;
			case ObjectType.Cloth:
				value = new ClothState(camera, this);
				break;
			case ObjectType.Particle:
				value = new ParticleState(camera, this);
				break;
			default:
				throw new Exception("[AmplifyMotion] Invalid object type.");
			}
			camera.RegisterObject(this);
			this.m_states.Add(component, value);
		}
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000A1F60 File Offset: 0x000A0360
	internal void UnregisterCamera(AmplifyMotionCamera camera)
	{
		Camera component = camera.GetComponent<Camera>();
		MotionState motionState;
		if (this.m_states.TryGetValue(component, out motionState))
		{
			camera.UnregisterObject(this);
			if (this.m_states.TryGetValue(component, out motionState))
			{
				motionState.Shutdown();
			}
			this.m_states.Remove(component);
		}
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x000A1FB4 File Offset: 0x000A03B4
	private bool InitializeType()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (AmplifyMotionEffectBase.CanRegister(base.gameObject, false))
		{
			ParticleSystem component2 = base.GetComponent<ParticleSystem>();
			if (component2 != null)
			{
				this.m_type = ObjectType.Particle;
				AmplifyMotionEffectBase.RegisterObject(this);
			}
			else if (component != null)
			{
				if (component.GetType() == typeof(MeshRenderer))
				{
					this.m_type = ObjectType.Solid;
				}
				else if (component.GetType() == typeof(SkinnedMeshRenderer))
				{
					if (base.GetComponent<Cloth>() != null)
					{
						this.m_type = ObjectType.Cloth;
					}
					else
					{
						this.m_type = ObjectType.Skinned;
					}
				}
				AmplifyMotionEffectBase.RegisterObject(this);
			}
		}
		return component != null;
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000A2074 File Offset: 0x000A0474
	private void OnEnable()
	{
		bool flag = this.InitializeType();
		if (flag)
		{
			if (this.m_type == ObjectType.Cloth)
			{
				this.m_fixedStep = false;
			}
			else if (this.m_type == ObjectType.Solid)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				if (component != null && component.interpolation == RigidbodyInterpolation.None && !component.isKinematic)
				{
					this.m_fixedStep = true;
				}
			}
		}
		if (this.m_applyToChildren)
		{
			IEnumerator enumerator = base.gameObject.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					AmplifyMotionEffectBase.RegisterRecursivelyS(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (!flag)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000A2160 File Offset: 0x000A0560
	private void OnDisable()
	{
		AmplifyMotionEffectBase.UnregisterObject(this);
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000A2168 File Offset: 0x000A0568
	private void TryInitializeStates()
	{
		foreach (KeyValuePair<Camera, MotionState> keyValuePair in this.m_states)
		{
			MotionState value = keyValuePair.Value;
			if (value.Owner.Initialized && !value.Error && !value.Initialized)
			{
				value.Initialize();
			}
		}
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000A21CE File Offset: 0x000A05CE
	private void Start()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000A21F7 File Offset: 0x000A05F7
	private void Update()
	{
		if (AmplifyMotionEffectBase.Instance != null)
		{
			this.TryInitializeStates();
		}
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000A2210 File Offset: 0x000A0610
	private static void RecursiveResetMotionAtFrame(Transform transform, AmplifyMotionObjectBase obj, int frame)
	{
		if (obj != null)
		{
			obj.m_resetAtFrame = frame;
		}
		IEnumerator enumerator = transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj2 = enumerator.Current;
				Transform transform2 = (Transform)obj2;
				AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(transform2, transform2.GetComponent<AmplifyMotionObjectBase>(), frame);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000A228C File Offset: 0x000A068C
	public void ResetMotionNow()
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000A229F File Offset: 0x000A069F
	public void ResetMotionAtFrame(int frame)
	{
		AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, frame);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000A22AE File Offset: 0x000A06AE
	private void CheckTeleportReset(AmplifyMotionEffectBase inst)
	{
		if (Vector3.SqrMagnitude(base.transform.position - this.m_lastPosition) > inst.MinResetDeltaDistSqr)
		{
			AmplifyMotionObjectBase.RecursiveResetMotionAtFrame(base.transform, this, Time.frameCount + inst.ResetFrameDelay);
		}
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000A22F0 File Offset: 0x000A06F0
	internal void OnUpdateTransform(AmplifyMotionEffectBase inst, Camera camera, CommandBuffer updateCB, bool starting)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			this.CheckTeleportReset(inst);
			bool flag = this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame;
			motionState.UpdateTransform(updateCB, starting || flag);
		}
		this.m_lastPosition = base.transform.position;
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000A2368 File Offset: 0x000A0768
	internal void OnRenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
	{
		MotionState motionState;
		if (this.m_states.TryGetValue(camera, out motionState) && !motionState.Error)
		{
			motionState.RenderVectors(camera, renderCB, scale, quality);
			if (this.m_resetAtFrame > 0 && Time.frameCount >= this.m_resetAtFrame)
			{
				this.m_resetAtFrame = -1;
			}
		}
	}

	// Token: 0x040011EA RID: 4586
	internal static bool ApplyToChildren = true;

	// Token: 0x040011EB RID: 4587
	[SerializeField]
	private bool m_applyToChildren = AmplifyMotionObjectBase.ApplyToChildren;

	// Token: 0x040011EC RID: 4588
	private ObjectType m_type;

	// Token: 0x040011ED RID: 4589
	private Dictionary<Camera, MotionState> m_states = new Dictionary<Camera, MotionState>();

	// Token: 0x040011EE RID: 4590
	private bool m_fixedStep;

	// Token: 0x040011EF RID: 4591
	private int m_objectId;

	// Token: 0x040011F0 RID: 4592
	private Vector3 m_lastPosition = Vector3.zero;

	// Token: 0x040011F1 RID: 4593
	private int m_resetAtFrame = -1;

	// Token: 0x02000293 RID: 659
	public enum MinMaxCurveState
	{
		// Token: 0x040011F3 RID: 4595
		Scalar,
		// Token: 0x040011F4 RID: 4596
		Curve,
		// Token: 0x040011F5 RID: 4597
		TwoCurves,
		// Token: 0x040011F6 RID: 4598
		TwoScalars
	}
}
