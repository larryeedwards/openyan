using System;
using System.Collections.Generic;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200028B RID: 651
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public class AmplifyMotionCamera : MonoBehaviour
{
	// Token: 0x17000306 RID: 774
	// (get) Token: 0x060014DF RID: 5343 RVA: 0x000A17AC File Offset: 0x0009FBAC
	public bool Initialized
	{
		get
		{
			return this.m_initialized;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000A17B4 File Offset: 0x0009FBB4
	public bool AutoStep
	{
		get
		{
			return this.m_autoStep;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000A17BC File Offset: 0x0009FBBC
	public bool Overlay
	{
		get
		{
			return this.m_overlay;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000A17C4 File Offset: 0x0009FBC4
	public Camera Camera
	{
		get
		{
			return this.m_camera;
		}
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000A17CC File Offset: 0x0009FBCC
	public void RegisterObject(AmplifyMotionObjectBase obj)
	{
		this.m_affectedObjectsTable.Add(obj);
		this.m_affectedObjectsChanged = true;
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000A17E2 File Offset: 0x0009FBE2
	public void UnregisterObject(AmplifyMotionObjectBase obj)
	{
		this.m_affectedObjectsTable.Remove(obj);
		this.m_affectedObjectsChanged = true;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000A17F8 File Offset: 0x0009FBF8
	private void UpdateAffectedObjects()
	{
		if (this.m_affectedObjects == null || this.m_affectedObjectsTable.Count != this.m_affectedObjects.Length)
		{
			this.m_affectedObjects = new AmplifyMotionObjectBase[this.m_affectedObjectsTable.Count];
		}
		this.m_affectedObjectsTable.CopyTo(this.m_affectedObjects);
		this.m_affectedObjectsChanged = false;
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000A1856 File Offset: 0x0009FC56
	public void LinkTo(AmplifyMotionEffectBase instance, bool overlay)
	{
		this.Instance = instance;
		this.m_camera = base.GetComponent<Camera>();
		this.m_camera.depthTextureMode |= DepthTextureMode.Depth;
		this.InitializeCommandBuffers();
		this.m_overlay = overlay;
		this.m_linked = true;
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000A1892 File Offset: 0x0009FC92
	public void Initialize()
	{
		this.m_step = false;
		this.UpdateMatrices();
		this.m_initialized = true;
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000A18A8 File Offset: 0x0009FCA8
	private void InitializeCommandBuffers()
	{
		this.ShutdownCommandBuffers();
		this.m_renderCB = new CommandBuffer();
		this.m_renderCB.name = "AmplifyMotion.Render";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.m_renderCB);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000A18DE File Offset: 0x0009FCDE
	private void ShutdownCommandBuffers()
	{
		if (this.m_renderCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_renderCB);
			this.m_renderCB.Release();
			this.m_renderCB = null;
		}
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000A1910 File Offset: 0x0009FD10
	private void Awake()
	{
		this.Transform = base.transform;
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000A191E File Offset: 0x0009FD1E
	private void OnEnable()
	{
		AmplifyMotionEffectBase.RegisterCamera(this);
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x000A1926 File Offset: 0x0009FD26
	private void OnDisable()
	{
		this.m_initialized = false;
		this.ShutdownCommandBuffers();
		AmplifyMotionEffectBase.UnregisterCamera(this);
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000A193B File Offset: 0x0009FD3B
	private void OnDestroy()
	{
		if (this.Instance != null)
		{
			this.Instance.RemoveCamera(this.m_camera);
		}
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000A195F File Offset: 0x0009FD5F
	public void StopAutoStep()
	{
		if (this.m_autoStep)
		{
			this.m_autoStep = false;
			this.m_step = true;
		}
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000A197A File Offset: 0x0009FD7A
	public void StartAutoStep()
	{
		this.m_autoStep = true;
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000A1983 File Offset: 0x0009FD83
	public void Step()
	{
		this.m_step = true;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000A198C File Offset: 0x0009FD8C
	private void Update()
	{
		if (!this.m_linked || !this.Instance.isActiveAndEnabled)
		{
			return;
		}
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if ((this.m_camera.depthTextureMode & DepthTextureMode.Depth) == DepthTextureMode.None)
		{
			this.m_camera.depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000A19EC File Offset: 0x0009FDEC
	private void UpdateMatrices()
	{
		if (!this.m_starting)
		{
			this.PrevViewProjMatrix = this.ViewProjMatrix;
			this.PrevViewProjMatrixRT = this.ViewProjMatrixRT;
		}
		Matrix4x4 worldToCameraMatrix = this.m_camera.worldToCameraMatrix;
		Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.m_camera.projectionMatrix, false);
		this.ViewProjMatrix = gpuprojectionMatrix * worldToCameraMatrix;
		this.InvViewProjMatrix = Matrix4x4.Inverse(this.ViewProjMatrix);
		Matrix4x4 gpuprojectionMatrix2 = GL.GetGPUProjectionMatrix(this.m_camera.projectionMatrix, true);
		this.ViewProjMatrixRT = gpuprojectionMatrix2 * worldToCameraMatrix;
		if (this.m_starting)
		{
			this.PrevViewProjMatrix = this.ViewProjMatrix;
			this.PrevViewProjMatrixRT = this.ViewProjMatrixRT;
		}
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000A1A9C File Offset: 0x0009FE9C
	public void FixedUpdateTransform(AmplifyMotionEffectBase inst, CommandBuffer updateCB)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if (this.m_affectedObjectsChanged)
		{
			this.UpdateAffectedObjects();
		}
		for (int i = 0; i < this.m_affectedObjects.Length; i++)
		{
			if (this.m_affectedObjects[i].FixedStep)
			{
				this.m_affectedObjects[i].OnUpdateTransform(inst, this.m_camera, updateCB, this.m_starting);
			}
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000A1B14 File Offset: 0x0009FF14
	public void UpdateTransform(AmplifyMotionEffectBase inst, CommandBuffer updateCB)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		if (Time.frameCount > this.m_prevFrameCount && (this.m_autoStep || this.m_step))
		{
			this.UpdateMatrices();
			if (this.m_affectedObjectsChanged)
			{
				this.UpdateAffectedObjects();
			}
			for (int i = 0; i < this.m_affectedObjects.Length; i++)
			{
				if (!this.m_affectedObjects[i].FixedStep)
				{
					this.m_affectedObjects[i].OnUpdateTransform(inst, this.m_camera, updateCB, this.m_starting);
				}
			}
			this.m_starting = false;
			this.m_step = false;
			this.m_prevFrameCount = Time.frameCount;
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000A1BD0 File Offset: 0x0009FFD0
	public void RenderReprojectionVectors(RenderTexture destination, float scale)
	{
		this.m_renderCB.SetGlobalMatrix("_AM_MATRIX_CURR_REPROJ", this.PrevViewProjMatrix * this.InvViewProjMatrix);
		this.m_renderCB.SetGlobalFloat("_AM_MOTION_SCALE", scale);
		RenderTexture tex = null;
		this.m_renderCB.Blit(new RenderTargetIdentifier(tex), destination, this.Instance.ReprojectionMaterial);
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000A1C34 File Offset: 0x000A0034
	public void PreRenderVectors(RenderTexture motionRT, bool clearColor, float rcpDepthThreshold)
	{
		this.m_renderCB.Clear();
		this.m_renderCB.SetGlobalFloat("_AM_MIN_VELOCITY", this.Instance.MinVelocity);
		this.m_renderCB.SetGlobalFloat("_AM_MAX_VELOCITY", this.Instance.MaxVelocity);
		this.m_renderCB.SetGlobalFloat("_AM_RCP_TOTAL_VELOCITY", 1f / (this.Instance.MaxVelocity - this.Instance.MinVelocity));
		this.m_renderCB.SetGlobalVector("_AM_DEPTH_THRESHOLD", new Vector2(this.Instance.DepthThreshold, rcpDepthThreshold));
		this.m_renderCB.SetRenderTarget(motionRT);
		this.m_renderCB.ClearRenderTarget(true, clearColor, Color.black);
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000A1CF8 File Offset: 0x000A00F8
	public void RenderVectors(float scale, float fixedScale, Quality quality)
	{
		if (!this.m_initialized)
		{
			this.Initialize();
		}
		float nearClipPlane = this.m_camera.nearClipPlane;
		float farClipPlane = this.m_camera.farClipPlane;
		Vector4 value;
		if (AmplifyMotionEffectBase.IsD3D)
		{
			value.x = 1f - farClipPlane / nearClipPlane;
			value.y = farClipPlane / nearClipPlane;
		}
		else
		{
			value.x = (1f - farClipPlane / nearClipPlane) / 2f;
			value.y = (1f + farClipPlane / nearClipPlane) / 2f;
		}
		value.z = value.x / farClipPlane;
		value.w = value.y / farClipPlane;
		this.m_renderCB.SetGlobalVector("_AM_ZBUFFER_PARAMS", value);
		if (this.m_affectedObjectsChanged)
		{
			this.UpdateAffectedObjects();
		}
		for (int i = 0; i < this.m_affectedObjects.Length; i++)
		{
			if ((this.m_camera.cullingMask & 1 << this.m_affectedObjects[i].gameObject.layer) != 0)
			{
				this.m_affectedObjects[i].OnRenderVectors(this.m_camera, this.m_renderCB, (!this.m_affectedObjects[i].FixedStep) ? scale : fixedScale, quality);
			}
		}
	}

	// Token: 0x040011B9 RID: 4537
	internal AmplifyMotionEffectBase Instance;

	// Token: 0x040011BA RID: 4538
	internal Matrix4x4 PrevViewProjMatrix;

	// Token: 0x040011BB RID: 4539
	internal Matrix4x4 ViewProjMatrix;

	// Token: 0x040011BC RID: 4540
	internal Matrix4x4 InvViewProjMatrix;

	// Token: 0x040011BD RID: 4541
	internal Matrix4x4 PrevViewProjMatrixRT;

	// Token: 0x040011BE RID: 4542
	internal Matrix4x4 ViewProjMatrixRT;

	// Token: 0x040011BF RID: 4543
	internal Transform Transform;

	// Token: 0x040011C0 RID: 4544
	private bool m_linked;

	// Token: 0x040011C1 RID: 4545
	private bool m_initialized;

	// Token: 0x040011C2 RID: 4546
	private bool m_starting = true;

	// Token: 0x040011C3 RID: 4547
	private bool m_autoStep = true;

	// Token: 0x040011C4 RID: 4548
	private bool m_step;

	// Token: 0x040011C5 RID: 4549
	private bool m_overlay;

	// Token: 0x040011C6 RID: 4550
	private Camera m_camera;

	// Token: 0x040011C7 RID: 4551
	private int m_prevFrameCount;

	// Token: 0x040011C8 RID: 4552
	private HashSet<AmplifyMotionObjectBase> m_affectedObjectsTable = new HashSet<AmplifyMotionObjectBase>();

	// Token: 0x040011C9 RID: 4553
	private AmplifyMotionObjectBase[] m_affectedObjects;

	// Token: 0x040011CA RID: 4554
	private bool m_affectedObjectsChanged = true;

	// Token: 0x040011CB RID: 4555
	private const CameraEvent m_renderCBEvent = CameraEvent.BeforeImageEffects;

	// Token: 0x040011CC RID: 4556
	private CommandBuffer m_renderCB;
}
