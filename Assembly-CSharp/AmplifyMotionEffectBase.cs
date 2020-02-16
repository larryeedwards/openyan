using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

// Token: 0x0200028A RID: 650
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class AmplifyMotionEffectBase : MonoBehaviour
{
	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001496 RID: 5270 RVA: 0x0009F9E9 File Offset: 0x0009DDE9
	// (set) Token: 0x06001497 RID: 5271 RVA: 0x0009F9F1 File Offset: 0x0009DDF1
	[Obsolete("workerThreads is deprecated, please use WorkerThreads instead.")]
	public int workerThreads
	{
		get
		{
			return this.WorkerThreads;
		}
		set
		{
			this.WorkerThreads = value;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001498 RID: 5272 RVA: 0x0009F9FA File Offset: 0x0009DDFA
	internal Material ReprojectionMaterial
	{
		get
		{
			return this.m_reprojectionMaterial;
		}
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06001499 RID: 5273 RVA: 0x0009FA02 File Offset: 0x0009DE02
	internal Material SolidVectorsMaterial
	{
		get
		{
			return this.m_solidVectorsMaterial;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x0600149A RID: 5274 RVA: 0x0009FA0A File Offset: 0x0009DE0A
	internal Material SkinnedVectorsMaterial
	{
		get
		{
			return this.m_skinnedVectorsMaterial;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x0600149B RID: 5275 RVA: 0x0009FA12 File Offset: 0x0009DE12
	internal Material ClothVectorsMaterial
	{
		get
		{
			return this.m_clothVectorsMaterial;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x0600149C RID: 5276 RVA: 0x0009FA1A File Offset: 0x0009DE1A
	internal RenderTexture MotionRenderTexture
	{
		get
		{
			return this.m_motionRT;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x0600149D RID: 5277 RVA: 0x0009FA22 File Offset: 0x0009DE22
	public Dictionary<Camera, AmplifyMotionCamera> LinkedCameras
	{
		get
		{
			return this.m_linkedCameras;
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x0600149E RID: 5278 RVA: 0x0009FA2A File Offset: 0x0009DE2A
	internal float MotionScaleNorm
	{
		get
		{
			return this.m_motionScaleNorm;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x0600149F RID: 5279 RVA: 0x0009FA32 File Offset: 0x0009DE32
	internal float FixedMotionScaleNorm
	{
		get
		{
			return this.m_fixedMotionScaleNorm;
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0009FA3A File Offset: 0x0009DE3A
	public AmplifyMotionCamera BaseCamera
	{
		get
		{
			return this.m_baseCamera;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0009FA42 File Offset: 0x0009DE42
	internal WorkerThreadPool WorkerPool
	{
		get
		{
			return this.m_workerThreadPool;
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0009FA4A File Offset: 0x0009DE4A
	public static bool IsD3D
	{
		get
		{
			return AmplifyMotionEffectBase.m_isD3D;
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0009FA51 File Offset: 0x0009DE51
	public bool CanUseGPU
	{
		get
		{
			return this.m_canUseGPU;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0009FA59 File Offset: 0x0009DE59
	public static bool IgnoreMotionScaleWarning
	{
		get
		{
			return AmplifyMotionEffectBase.m_ignoreMotionScaleWarning;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0009FA60 File Offset: 0x0009DE60
	public static AmplifyMotionEffectBase FirstInstance
	{
		get
		{
			return AmplifyMotionEffectBase.m_firstInstance;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0009FA67 File Offset: 0x0009DE67
	public static AmplifyMotionEffectBase Instance
	{
		get
		{
			return AmplifyMotionEffectBase.m_firstInstance;
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0009FA70 File Offset: 0x0009DE70
	private void Awake()
	{
		if (AmplifyMotionEffectBase.m_firstInstance == null)
		{
			AmplifyMotionEffectBase.m_firstInstance = this;
		}
		AmplifyMotionEffectBase.m_isD3D = SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D");
		this.m_globalObjectId = 1;
		this.m_width = (this.m_height = 0);
		if (this.ForceCPUOnly)
		{
			this.m_canUseGPU = false;
		}
		else
		{
			bool flag = SystemInfo.graphicsShaderLevel >= 30;
			bool flag2 = SystemInfo.SupportsTextureFormat(TextureFormat.RHalf);
			bool flag3 = SystemInfo.SupportsTextureFormat(TextureFormat.RGHalf);
			bool flag4 = SystemInfo.SupportsTextureFormat(TextureFormat.RGBAHalf);
			bool flag5 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat);
			this.m_canUseGPU = (flag && flag2 && flag3 && flag4 && flag5);
		}
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0009FB28 File Offset: 0x0009DF28
	internal void ResetObjectId()
	{
		this.m_globalObjectId = 1;
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0009FB31 File Offset: 0x0009DF31
	internal int GenerateObjectId(GameObject obj)
	{
		if (obj.isStatic)
		{
			return 0;
		}
		this.m_globalObjectId++;
		if (this.m_globalObjectId > 254)
		{
			this.m_globalObjectId = 1;
		}
		return this.m_globalObjectId;
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x0009FB6B File Offset: 0x0009DF6B
	private void SafeDestroyMaterial(ref Material mat)
	{
		if (mat != null)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x0009FB84 File Offset: 0x0009DF84
	private bool CheckMaterialAndShader(Material material, string name)
	{
		bool result = true;
		if (material == null || material.shader == null)
		{
			Debug.LogWarning("[AmplifyMotion] Error creating " + name + " material");
			result = false;
		}
		else if (!material.shader.isSupported)
		{
			Debug.LogWarning("[AmplifyMotion] " + name + " shader not supported on this platform");
			result = false;
		}
		return result;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x0009FBF4 File Offset: 0x0009DFF4
	private void DestroyMaterials()
	{
		this.SafeDestroyMaterial(ref this.m_blurMaterial);
		this.SafeDestroyMaterial(ref this.m_solidVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_skinnedVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_clothVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_reprojectionMaterial);
		this.SafeDestroyMaterial(ref this.m_combineMaterial);
		this.SafeDestroyMaterial(ref this.m_dilationMaterial);
		this.SafeDestroyMaterial(ref this.m_depthMaterial);
		this.SafeDestroyMaterial(ref this.m_debugMaterial);
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x0009FC70 File Offset: 0x0009E070
	private bool CreateMaterials()
	{
		this.DestroyMaterials();
		int num = (SystemInfo.graphicsShaderLevel < 30) ? 2 : 3;
		string name = "Hidden/Amplify Motion/MotionBlurSM" + num;
		string name2 = "Hidden/Amplify Motion/SolidVectors";
		string name3 = "Hidden/Amplify Motion/SkinnedVectors";
		string name4 = "Hidden/Amplify Motion/ClothVectors";
		string name5 = "Hidden/Amplify Motion/ReprojectionVectors";
		string name6 = "Hidden/Amplify Motion/Combine";
		string name7 = "Hidden/Amplify Motion/Dilation";
		string name8 = "Hidden/Amplify Motion/Depth";
		string name9 = "Hidden/Amplify Motion/Debug";
		try
		{
			this.m_blurMaterial = new Material(Shader.Find(name))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_solidVectorsMaterial = new Material(Shader.Find(name2))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_skinnedVectorsMaterial = new Material(Shader.Find(name3))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_clothVectorsMaterial = new Material(Shader.Find(name4))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_reprojectionMaterial = new Material(Shader.Find(name5))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_combineMaterial = new Material(Shader.Find(name6))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_dilationMaterial = new Material(Shader.Find(name7))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_depthMaterial = new Material(Shader.Find(name8))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_debugMaterial = new Material(Shader.Find(name9))
			{
				hideFlags = HideFlags.DontSave
			};
		}
		catch (Exception)
		{
		}
		bool flag = this.CheckMaterialAndShader(this.m_blurMaterial, name);
		flag = (flag && this.CheckMaterialAndShader(this.m_solidVectorsMaterial, name2));
		flag = (flag && this.CheckMaterialAndShader(this.m_skinnedVectorsMaterial, name3));
		flag = (flag && this.CheckMaterialAndShader(this.m_clothVectorsMaterial, name4));
		flag = (flag && this.CheckMaterialAndShader(this.m_reprojectionMaterial, name5));
		flag = (flag && this.CheckMaterialAndShader(this.m_combineMaterial, name6));
		flag = (flag && this.CheckMaterialAndShader(this.m_dilationMaterial, name7));
		flag = (flag && this.CheckMaterialAndShader(this.m_depthMaterial, name8));
		return flag && this.CheckMaterialAndShader(this.m_debugMaterial, name9);
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x0009FEF8 File Offset: 0x0009E2F8
	private RenderTexture CreateRenderTexture(string name, int depth, RenderTextureFormat fmt, RenderTextureReadWrite rw, FilterMode fm)
	{
		RenderTexture renderTexture = new RenderTexture(this.m_width, this.m_height, depth, fmt, rw);
		renderTexture.hideFlags = HideFlags.DontSave;
		renderTexture.name = name;
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture.filterMode = fm;
		renderTexture.Create();
		return renderTexture;
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x0009FF41 File Offset: 0x0009E341
	private void SafeDestroyRenderTexture(ref RenderTexture rt)
	{
		if (rt != null)
		{
			RenderTexture.active = null;
			rt.Release();
			UnityEngine.Object.DestroyImmediate(rt);
			rt = null;
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x0009FF67 File Offset: 0x0009E367
	private void SafeDestroyTexture(ref Texture tex)
	{
		if (tex != null)
		{
			UnityEngine.Object.DestroyImmediate(tex);
			tex = null;
		}
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x0009FF80 File Offset: 0x0009E380
	private void DestroyRenderTextures()
	{
		RenderTexture.active = null;
		this.SafeDestroyRenderTexture(ref this.m_motionRT);
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x0009FF94 File Offset: 0x0009E394
	private void UpdateRenderTextures(bool qualityChanged)
	{
		int num = Mathf.Max(Mathf.FloorToInt((float)this.m_camera.pixelWidth + 0.5f), 1);
		int num2 = Mathf.Max(Mathf.FloorToInt((float)this.m_camera.pixelHeight + 0.5f), 1);
		if (this.QualityLevel == Quality.Mobile)
		{
			num /= 2;
			num2 /= 2;
		}
		if (this.m_width != num || this.m_height != num2)
		{
			this.m_width = num;
			this.m_height = num2;
			this.DestroyRenderTextures();
		}
		if (this.m_motionRT == null)
		{
			this.m_motionRT = this.CreateRenderTexture("AM-MotionVectors", 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Point);
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000A0043 File Offset: 0x0009E443
	public bool CheckSupport()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("[AmplifyMotion] Initialization failed. This plugin requires support for Image Effects and Render Textures.");
			return false;
		}
		return true;
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000A005C File Offset: 0x0009E45C
	private void InitializeThreadPool()
	{
		if (this.WorkerThreads <= 0)
		{
			this.WorkerThreads = Mathf.Max(Environment.ProcessorCount / 2, 1);
		}
		this.m_workerThreadPool = new WorkerThreadPool();
		this.m_workerThreadPool.InitializeAsyncUpdateThreads(this.WorkerThreads, this.SystemThreadPool);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000A00AA File Offset: 0x0009E4AA
	private void ShutdownThreadPool()
	{
		if (this.m_workerThreadPool != null)
		{
			this.m_workerThreadPool.FinalizeAsyncUpdateThreads();
			this.m_workerThreadPool = null;
		}
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000A00CC File Offset: 0x0009E4CC
	private void InitializeCommandBuffers()
	{
		this.ShutdownCommandBuffers();
		this.m_updateCB = new CommandBuffer();
		this.m_updateCB.name = "AmplifyMotion.Update";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_updateCB);
		this.m_fixedUpdateCB = new CommandBuffer();
		this.m_fixedUpdateCB.name = "AmplifyMotion.FixedUpdate";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_fixedUpdateCB);
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x000A013C File Offset: 0x0009E53C
	private void ShutdownCommandBuffers()
	{
		if (this.m_updateCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_updateCB);
			this.m_updateCB.Release();
			this.m_updateCB = null;
		}
		if (this.m_fixedUpdateCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_fixedUpdateCB);
			this.m_fixedUpdateCB.Release();
			this.m_fixedUpdateCB = null;
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000A01AC File Offset: 0x0009E5AC
	private void OnEnable()
	{
		this.m_camera = base.GetComponent<Camera>();
		if (!this.CheckSupport())
		{
			base.enabled = false;
			return;
		}
		this.InitializeThreadPool();
		this.m_starting = true;
		if (!this.CreateMaterials())
		{
			Debug.LogError("[AmplifyMotion] Failed loading or compiling necessary shaders. Please try reinstalling Amplify Motion or contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (this.AutoRegisterObjs)
		{
			this.UpdateActiveObjects();
		}
		this.InitializeCameras();
		this.InitializeCommandBuffers();
		this.UpdateRenderTextures(true);
		this.m_linkedCameras.TryGetValue(this.m_camera, out this.m_baseCamera);
		if (this.m_baseCamera == null)
		{
			Debug.LogError("[AmplifyMotion] Failed setting up Base Camera. Please contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (this.m_currentPostProcess != null)
		{
			this.m_currentPostProcess.enabled = true;
		}
		this.m_qualityLevel = this.QualityLevel;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000A028A File Offset: 0x0009E68A
	private void OnDisable()
	{
		if (this.m_currentPostProcess != null)
		{
			this.m_currentPostProcess.enabled = false;
		}
		this.ShutdownCommandBuffers();
		this.ShutdownThreadPool();
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000A02B5 File Offset: 0x0009E6B5
	private void Start()
	{
		this.UpdatePostProcess();
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000A02BD File Offset: 0x0009E6BD
	internal void RemoveCamera(Camera reference)
	{
		this.m_linkedCameras.Remove(reference);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000A02CC File Offset: 0x0009E6CC
	private void OnDestroy()
	{
		AmplifyMotionCamera[] array = this.m_linkedCameras.Values.ToArray<AmplifyMotionCamera>();
		foreach (AmplifyMotionCamera amplifyMotionCamera in array)
		{
			if (amplifyMotionCamera != null && amplifyMotionCamera.gameObject != base.gameObject)
			{
				Camera component = amplifyMotionCamera.GetComponent<Camera>();
				if (component != null)
				{
					component.targetTexture = null;
				}
				UnityEngine.Object.DestroyImmediate(amplifyMotionCamera);
			}
		}
		this.DestroyRenderTextures();
		this.DestroyMaterials();
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000A0358 File Offset: 0x0009E758
	private GameObject RecursiveFindCamera(GameObject obj, string auxCameraName)
	{
		GameObject gameObject = null;
		if (obj.name == auxCameraName)
		{
			gameObject = obj;
		}
		else
		{
			IEnumerator enumerator = obj.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					Transform transform = (Transform)obj2;
					gameObject = this.RecursiveFindCamera(transform.gameObject, auxCameraName);
					if (gameObject != null)
					{
						break;
					}
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
		return gameObject;
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000A03F0 File Offset: 0x0009E7F0
	private void InitializeCameras()
	{
		List<Camera> list = new List<Camera>(this.OverlayCameras.Length);
		for (int i = 0; i < this.OverlayCameras.Length; i++)
		{
			if (this.OverlayCameras[i] != null)
			{
				list.Add(this.OverlayCameras[i]);
			}
		}
		Camera[] array = new Camera[list.Count + 1];
		array[0] = this.m_camera;
		for (int j = 0; j < list.Count; j++)
		{
			array[j + 1] = list[j];
		}
		this.m_linkedCameras.Clear();
		for (int k = 0; k < array.Length; k++)
		{
			Camera camera = array[k];
			if (!this.m_linkedCameras.ContainsKey(camera))
			{
				AmplifyMotionCamera amplifyMotionCamera = camera.gameObject.GetComponent<AmplifyMotionCamera>();
				if (amplifyMotionCamera != null)
				{
					amplifyMotionCamera.enabled = false;
					amplifyMotionCamera.enabled = true;
				}
				else
				{
					amplifyMotionCamera = camera.gameObject.AddComponent<AmplifyMotionCamera>();
				}
				amplifyMotionCamera.LinkTo(this, k > 0);
				this.m_linkedCameras.Add(camera, amplifyMotionCamera);
				this.m_linkedCamerasChanged = true;
			}
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000A051B File Offset: 0x0009E91B
	public void UpdateActiveCameras()
	{
		this.InitializeCameras();
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000A0524 File Offset: 0x0009E924
	internal static void RegisterCamera(AmplifyMotionCamera cam)
	{
		if (!AmplifyMotionEffectBase.m_activeCameras.ContainsValue(cam))
		{
			AmplifyMotionEffectBase.m_activeCameras.Add(cam.GetComponent<Camera>(), cam);
		}
		foreach (AmplifyMotionObjectBase amplifyMotionObjectBase in AmplifyMotionEffectBase.m_activeObjects.Values)
		{
			amplifyMotionObjectBase.RegisterCamera(cam);
		}
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000A05A8 File Offset: 0x0009E9A8
	internal static void UnregisterCamera(AmplifyMotionCamera cam)
	{
		foreach (AmplifyMotionObjectBase amplifyMotionObjectBase in AmplifyMotionEffectBase.m_activeObjects.Values)
		{
			amplifyMotionObjectBase.UnregisterCamera(cam);
		}
		AmplifyMotionEffectBase.m_activeCameras.Remove(cam.GetComponent<Camera>());
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000A061C File Offset: 0x0009EA1C
	public void UpdateActiveObjects()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		for (int i = 0; i < array.Length; i++)
		{
			if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(array[i]))
			{
				AmplifyMotionEffectBase.TryRegister(array[i], true);
			}
		}
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000A0670 File Offset: 0x0009EA70
	internal static void RegisterObject(AmplifyMotionObjectBase obj)
	{
		AmplifyMotionEffectBase.m_activeObjects.Add(obj.gameObject, obj);
		foreach (AmplifyMotionCamera camera in AmplifyMotionEffectBase.m_activeCameras.Values)
		{
			obj.RegisterCamera(camera);
		}
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x000A06E4 File Offset: 0x0009EAE4
	internal static void UnregisterObject(AmplifyMotionObjectBase obj)
	{
		foreach (AmplifyMotionCamera camera in AmplifyMotionEffectBase.m_activeCameras.Values)
		{
			obj.UnregisterCamera(camera);
		}
		AmplifyMotionEffectBase.m_activeObjects.Remove(obj.gameObject);
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000A0758 File Offset: 0x0009EB58
	internal static bool FindValidTag(Material[] materials)
	{
		foreach (Material material in materials)
		{
			if (material != null)
			{
				string tag = material.GetTag("RenderType", false);
				if (tag == "Opaque" || tag == "TransparentCutout")
				{
					return !material.IsKeywordEnabled("_ALPHABLEND_ON") && !material.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON");
				}
			}
		}
		return false;
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000A07DC File Offset: 0x0009EBDC
	internal static bool CanRegister(GameObject gameObj, bool autoReg)
	{
		if (gameObj.isStatic)
		{
			return false;
		}
		Renderer component = gameObj.GetComponent<Renderer>();
		if (component == null || component.sharedMaterials == null || component.isPartOfStaticBatch)
		{
			return false;
		}
		if (!component.enabled)
		{
			return false;
		}
		if (component.shadowCastingMode == ShadowCastingMode.ShadowsOnly)
		{
			return false;
		}
		if (component.GetType() == typeof(SpriteRenderer))
		{
			return false;
		}
		if (!AmplifyMotionEffectBase.FindValidTag(component.sharedMaterials))
		{
			return false;
		}
		Type type = component.GetType();
		if (type == typeof(MeshRenderer) || type == typeof(SkinnedMeshRenderer))
		{
			return true;
		}
		if (type == typeof(ParticleSystemRenderer) && !autoReg)
		{
			ParticleSystemRenderMode renderMode = (component as ParticleSystemRenderer).renderMode;
			return renderMode == ParticleSystemRenderMode.Mesh || renderMode == ParticleSystemRenderMode.Billboard;
		}
		return false;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000A08C0 File Offset: 0x0009ECC0
	internal static void TryRegister(GameObject gameObj, bool autoReg)
	{
		if (AmplifyMotionEffectBase.CanRegister(gameObj, autoReg) && gameObj.GetComponent<AmplifyMotionObjectBase>() == null)
		{
			AmplifyMotionObjectBase.ApplyToChildren = false;
			gameObj.AddComponent<AmplifyMotionObjectBase>();
			AmplifyMotionObjectBase.ApplyToChildren = true;
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000A08F4 File Offset: 0x0009ECF4
	internal static void TryUnregister(GameObject gameObj)
	{
		AmplifyMotionObjectBase component = gameObj.GetComponent<AmplifyMotionObjectBase>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x000A091A File Offset: 0x0009ED1A
	public void Register(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x000A0933 File Offset: 0x0009ED33
	public static void RegisterS(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x000A094C File Offset: 0x0009ED4C
	public void RegisterRecursively(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
		IEnumerator enumerator = gameObj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				this.RegisterRecursively(transform.gameObject);
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

	// Token: 0x060014CC RID: 5324 RVA: 0x000A09D0 File Offset: 0x0009EDD0
	public static void RegisterRecursivelyS(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
		IEnumerator enumerator = gameObj.transform.GetEnumerator();
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

	// Token: 0x060014CD RID: 5325 RVA: 0x000A0A50 File Offset: 0x0009EE50
	public void Unregister(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000A0A68 File Offset: 0x0009EE68
	public static void UnregisterS(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000A0A80 File Offset: 0x0009EE80
	public void UnregisterRecursively(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
		IEnumerator enumerator = gameObj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				this.UnregisterRecursively(transform.gameObject);
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

	// Token: 0x060014D0 RID: 5328 RVA: 0x000A0B00 File Offset: 0x0009EF00
	public static void UnregisterRecursivelyS(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
		IEnumerator enumerator = gameObj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				AmplifyMotionEffectBase.UnregisterRecursivelyS(transform.gameObject);
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

	// Token: 0x060014D1 RID: 5329 RVA: 0x000A0B80 File Offset: 0x0009EF80
	private void UpdatePostProcess()
	{
		Camera camera = null;
		float num = float.MinValue;
		if (this.m_linkedCamerasChanged)
		{
			this.UpdateLinkedCameras();
		}
		for (int i = 0; i < this.m_linkedCameraKeys.Length; i++)
		{
			if (this.m_linkedCameraKeys[i] != null && this.m_linkedCameraKeys[i].isActiveAndEnabled && this.m_linkedCameraKeys[i].depth > num)
			{
				camera = this.m_linkedCameraKeys[i];
				num = this.m_linkedCameraKeys[i].depth;
			}
		}
		if (this.m_currentPostProcess != null && this.m_currentPostProcess.gameObject != camera.gameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_currentPostProcess);
			this.m_currentPostProcess = null;
		}
		if (this.m_currentPostProcess == null && camera != null && camera != this.m_camera)
		{
			AmplifyMotionPostProcess[] components = base.gameObject.GetComponents<AmplifyMotionPostProcess>();
			if (components != null && components.Length > 0)
			{
				for (int j = 0; j < components.Length; j++)
				{
					UnityEngine.Object.DestroyImmediate(components[j]);
				}
			}
			this.m_currentPostProcess = camera.gameObject.AddComponent<AmplifyMotionPostProcess>();
			this.m_currentPostProcess.Instance = this;
		}
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000A0CD4 File Offset: 0x0009F0D4
	private void LateUpdate()
	{
		if (this.m_baseCamera.AutoStep)
		{
			float num = (!Application.isPlaying) ? Time.fixedDeltaTime : Time.unscaledDeltaTime;
			float fixedDeltaTime = Time.fixedDeltaTime;
			this.m_deltaTime = ((num <= float.Epsilon) ? this.m_deltaTime : num);
			this.m_fixedDeltaTime = ((num <= float.Epsilon) ? this.m_fixedDeltaTime : fixedDeltaTime);
		}
		this.QualitySteps = Mathf.Clamp(this.QualitySteps, 0, 16);
		this.MotionScale = Mathf.Max(this.MotionScale, 0f);
		this.MinVelocity = Mathf.Min(this.MinVelocity, this.MaxVelocity);
		this.DepthThreshold = Mathf.Max(this.DepthThreshold, 0f);
		this.MinResetDeltaDist = Mathf.Max(this.MinResetDeltaDist, 0f);
		this.MinResetDeltaDistSqr = this.MinResetDeltaDist * this.MinResetDeltaDist;
		this.ResetFrameDelay = Mathf.Max(this.ResetFrameDelay, 0);
		this.UpdatePostProcess();
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x000A0DE4 File Offset: 0x0009F1E4
	public void StopAutoStep()
	{
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.StopAutoStep();
		}
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x000A0E44 File Offset: 0x0009F244
	public void StartAutoStep()
	{
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.StartAutoStep();
		}
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000A0EA4 File Offset: 0x0009F2A4
	public void Step(float delta)
	{
		this.m_deltaTime = delta;
		this.m_fixedDeltaTime = delta;
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.Step();
		}
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000A0F14 File Offset: 0x0009F314
	private void UpdateLinkedCameras()
	{
		Dictionary<Camera, AmplifyMotionCamera>.KeyCollection keys = this.m_linkedCameras.Keys;
		Dictionary<Camera, AmplifyMotionCamera>.ValueCollection values = this.m_linkedCameras.Values;
		if (this.m_linkedCameraKeys == null || keys.Count != this.m_linkedCameraKeys.Length)
		{
			this.m_linkedCameraKeys = new Camera[keys.Count];
		}
		if (this.m_linkedCameraValues == null || values.Count != this.m_linkedCameraValues.Length)
		{
			this.m_linkedCameraValues = new AmplifyMotionCamera[values.Count];
		}
		keys.CopyTo(this.m_linkedCameraKeys, 0);
		values.CopyTo(this.m_linkedCameraValues, 0);
		this.m_linkedCamerasChanged = false;
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000A0FB8 File Offset: 0x0009F3B8
	private void FixedUpdate()
	{
		if (this.m_camera.enabled)
		{
			if (this.m_linkedCamerasChanged)
			{
				this.UpdateLinkedCameras();
			}
			this.m_fixedUpdateCB.Clear();
			for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
			{
				if (this.m_linkedCameraValues[i] != null && this.m_linkedCameraValues[i].isActiveAndEnabled)
				{
					this.m_linkedCameraValues[i].FixedUpdateTransform(this, this.m_fixedUpdateCB);
				}
			}
		}
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000A1044 File Offset: 0x0009F444
	private void OnPreRender()
	{
		if (this.m_camera.enabled && (Time.frameCount == 1 || Mathf.Abs(Time.unscaledDeltaTime) > 1.401298E-45f))
		{
			if (this.m_linkedCamerasChanged)
			{
				this.UpdateLinkedCameras();
			}
			this.m_updateCB.Clear();
			for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
			{
				if (this.m_linkedCameraValues[i] != null && this.m_linkedCameraValues[i].isActiveAndEnabled)
				{
					this.m_linkedCameraValues[i].UpdateTransform(this, this.m_updateCB);
				}
			}
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000A10F0 File Offset: 0x0009F4F0
	private void OnPostRender()
	{
		bool qualityChanged = this.QualityLevel != this.m_qualityLevel;
		this.m_qualityLevel = this.QualityLevel;
		this.UpdateRenderTextures(qualityChanged);
		this.ResetObjectId();
		bool flag = this.CameraMotionMult > float.Epsilon;
		bool clearColor = !flag || this.m_starting;
		float num = (this.DepthThreshold <= float.Epsilon) ? float.MaxValue : (1f / this.DepthThreshold);
		this.m_motionScaleNorm = ((this.m_deltaTime < float.Epsilon) ? 0f : (this.MotionScale * (1f / this.m_deltaTime)));
		this.m_fixedMotionScaleNorm = ((this.m_fixedDeltaTime < float.Epsilon) ? 0f : (this.MotionScale * (1f / this.m_fixedDeltaTime)));
		float scale = this.m_starting ? 0f : this.m_motionScaleNorm;
		float fixedScale = this.m_starting ? 0f : this.m_fixedMotionScaleNorm;
		Shader.SetGlobalFloat("_AM_MIN_VELOCITY", this.MinVelocity);
		Shader.SetGlobalFloat("_AM_MAX_VELOCITY", this.MaxVelocity);
		Shader.SetGlobalFloat("_AM_RCP_TOTAL_VELOCITY", 1f / (this.MaxVelocity - this.MinVelocity));
		Shader.SetGlobalVector("_AM_DEPTH_THRESHOLD", new Vector2(this.DepthThreshold, num));
		this.m_motionRT.DiscardContents();
		this.m_baseCamera.PreRenderVectors(this.m_motionRT, clearColor, num);
		for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
		{
			AmplifyMotionCamera amplifyMotionCamera = this.m_linkedCameraValues[i];
			if (amplifyMotionCamera != null && amplifyMotionCamera.Overlay && amplifyMotionCamera.isActiveAndEnabled)
			{
				amplifyMotionCamera.PreRenderVectors(this.m_motionRT, clearColor, num);
				amplifyMotionCamera.RenderVectors(scale, fixedScale, this.QualityLevel);
			}
		}
		if (flag)
		{
			float num2 = (this.m_deltaTime < float.Epsilon) ? 0f : (this.MotionScale * this.CameraMotionMult * (1f / this.m_deltaTime));
			float scale2 = this.m_starting ? 0f : num2;
			this.m_motionRT.DiscardContents();
			this.m_baseCamera.RenderReprojectionVectors(this.m_motionRT, scale2);
		}
		this.m_baseCamera.RenderVectors(scale, fixedScale, this.QualityLevel);
		for (int j = 0; j < this.m_linkedCameraValues.Length; j++)
		{
			AmplifyMotionCamera amplifyMotionCamera2 = this.m_linkedCameraValues[j];
			if (amplifyMotionCamera2 != null && amplifyMotionCamera2.Overlay && amplifyMotionCamera2.isActiveAndEnabled)
			{
				amplifyMotionCamera2.RenderVectors(scale, fixedScale, this.QualityLevel);
			}
		}
		this.m_starting = false;
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000A13E0 File Offset: 0x0009F7E0
	private void ApplyMotionBlur(RenderTexture source, RenderTexture destination, Vector4 blurStep)
	{
		bool flag = this.QualityLevel == Quality.Mobile;
		int pass = (int)(this.QualityLevel + ((!this.Noise) ? 0 : 4));
		RenderTexture renderTexture = null;
		if (flag)
		{
			renderTexture = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, RenderTextureFormat.ARGB32);
			renderTexture.name = "AM-DepthTemp";
			renderTexture.wrapMode = TextureWrapMode.Clamp;
			renderTexture.filterMode = FilterMode.Point;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, source.format);
		temporary.name = "AM-CombinedTemp";
		temporary.wrapMode = TextureWrapMode.Clamp;
		temporary.filterMode = FilterMode.Point;
		temporary.DiscardContents();
		this.m_combineMaterial.SetTexture("_MotionTex", this.m_motionRT);
		source.filterMode = FilterMode.Point;
		Graphics.Blit(source, temporary, this.m_combineMaterial, 0);
		this.m_blurMaterial.SetTexture("_MotionTex", this.m_motionRT);
		if (flag)
		{
			Graphics.Blit(null, renderTexture, this.m_depthMaterial, 0);
			this.m_blurMaterial.SetTexture("_DepthTex", renderTexture);
		}
		if (this.QualitySteps > 1)
		{
			RenderTexture temporary2 = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, source.format);
			temporary2.name = "AM-CombinedTemp2";
			temporary2.filterMode = FilterMode.Point;
			float num = 1f / (float)this.QualitySteps;
			float num2 = 1f;
			RenderTexture renderTexture2 = temporary;
			RenderTexture renderTexture3 = temporary2;
			for (int i = 0; i < this.QualitySteps; i++)
			{
				if (renderTexture3 != destination)
				{
					renderTexture3.DiscardContents();
				}
				this.m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep * num2);
				Graphics.Blit(renderTexture2, renderTexture3, this.m_blurMaterial, pass);
				if (i < this.QualitySteps - 2)
				{
					RenderTexture renderTexture4 = renderTexture3;
					renderTexture3 = renderTexture2;
					renderTexture2 = renderTexture4;
				}
				else
				{
					renderTexture2 = renderTexture3;
					renderTexture3 = destination;
				}
				num2 -= num;
			}
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			this.m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep);
			Graphics.Blit(temporary, destination, this.m_blurMaterial, pass);
		}
		if (flag)
		{
			this.m_combineMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(source, destination, this.m_combineMaterial, 1);
		}
		RenderTexture.ReleaseTemporary(temporary);
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000A162D File Offset: 0x0009FA2D
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_currentPostProcess == null)
		{
			this.PostProcess(source, destination);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000A1654 File Offset: 0x0009FA54
	public void PostProcess(RenderTexture source, RenderTexture destination)
	{
		Vector4 zero = Vector4.zero;
		zero.x = this.MaxVelocity / 1000f;
		zero.y = this.MaxVelocity / 1000f;
		RenderTexture renderTexture = null;
		if (QualitySettings.antiAliasing > 1)
		{
			renderTexture = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			renderTexture.name = "AM-DilatedTemp";
			renderTexture.filterMode = FilterMode.Point;
			this.m_dilationMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(this.m_motionRT, renderTexture, this.m_dilationMaterial, 0);
			this.m_dilationMaterial.SetTexture("_MotionTex", renderTexture);
			Graphics.Blit(renderTexture, this.m_motionRT, this.m_dilationMaterial, 1);
		}
		if (this.DebugMode)
		{
			this.m_debugMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(source, destination, this.m_debugMaterial);
		}
		else
		{
			this.ApplyMotionBlur(source, destination, zero);
		}
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	// Token: 0x04001182 RID: 4482
	[Header("Motion Blur")]
	public Quality QualityLevel = Quality.Standard;

	// Token: 0x04001183 RID: 4483
	public int QualitySteps = 1;

	// Token: 0x04001184 RID: 4484
	public float MotionScale = 3f;

	// Token: 0x04001185 RID: 4485
	public float CameraMotionMult = 1f;

	// Token: 0x04001186 RID: 4486
	public float MinVelocity = 1f;

	// Token: 0x04001187 RID: 4487
	public float MaxVelocity = 10f;

	// Token: 0x04001188 RID: 4488
	public float DepthThreshold = 0.01f;

	// Token: 0x04001189 RID: 4489
	public bool Noise;

	// Token: 0x0400118A RID: 4490
	[Header("Camera")]
	public Camera[] OverlayCameras = new Camera[0];

	// Token: 0x0400118B RID: 4491
	public LayerMask CullingMask = -1;

	// Token: 0x0400118C RID: 4492
	[Header("Objects")]
	public bool AutoRegisterObjs = true;

	// Token: 0x0400118D RID: 4493
	public float MinResetDeltaDist = 1000f;

	// Token: 0x0400118E RID: 4494
	[NonSerialized]
	public float MinResetDeltaDistSqr;

	// Token: 0x0400118F RID: 4495
	public int ResetFrameDelay = 1;

	// Token: 0x04001190 RID: 4496
	[Header("Low-Level")]
	[FormerlySerializedAs("workerThreads")]
	public int WorkerThreads;

	// Token: 0x04001191 RID: 4497
	public bool SystemThreadPool;

	// Token: 0x04001192 RID: 4498
	public bool ForceCPUOnly;

	// Token: 0x04001193 RID: 4499
	public bool DebugMode;

	// Token: 0x04001194 RID: 4500
	private Camera m_camera;

	// Token: 0x04001195 RID: 4501
	private bool m_starting = true;

	// Token: 0x04001196 RID: 4502
	private int m_width;

	// Token: 0x04001197 RID: 4503
	private int m_height;

	// Token: 0x04001198 RID: 4504
	private RenderTexture m_motionRT;

	// Token: 0x04001199 RID: 4505
	private Material m_blurMaterial;

	// Token: 0x0400119A RID: 4506
	private Material m_solidVectorsMaterial;

	// Token: 0x0400119B RID: 4507
	private Material m_skinnedVectorsMaterial;

	// Token: 0x0400119C RID: 4508
	private Material m_clothVectorsMaterial;

	// Token: 0x0400119D RID: 4509
	private Material m_reprojectionMaterial;

	// Token: 0x0400119E RID: 4510
	private Material m_combineMaterial;

	// Token: 0x0400119F RID: 4511
	private Material m_dilationMaterial;

	// Token: 0x040011A0 RID: 4512
	private Material m_depthMaterial;

	// Token: 0x040011A1 RID: 4513
	private Material m_debugMaterial;

	// Token: 0x040011A2 RID: 4514
	private Dictionary<Camera, AmplifyMotionCamera> m_linkedCameras = new Dictionary<Camera, AmplifyMotionCamera>();

	// Token: 0x040011A3 RID: 4515
	internal Camera[] m_linkedCameraKeys;

	// Token: 0x040011A4 RID: 4516
	internal AmplifyMotionCamera[] m_linkedCameraValues;

	// Token: 0x040011A5 RID: 4517
	internal bool m_linkedCamerasChanged = true;

	// Token: 0x040011A6 RID: 4518
	private AmplifyMotionPostProcess m_currentPostProcess;

	// Token: 0x040011A7 RID: 4519
	private int m_globalObjectId = 1;

	// Token: 0x040011A8 RID: 4520
	private float m_deltaTime;

	// Token: 0x040011A9 RID: 4521
	private float m_fixedDeltaTime;

	// Token: 0x040011AA RID: 4522
	private float m_motionScaleNorm;

	// Token: 0x040011AB RID: 4523
	private float m_fixedMotionScaleNorm;

	// Token: 0x040011AC RID: 4524
	private Quality m_qualityLevel;

	// Token: 0x040011AD RID: 4525
	private AmplifyMotionCamera m_baseCamera;

	// Token: 0x040011AE RID: 4526
	private WorkerThreadPool m_workerThreadPool;

	// Token: 0x040011AF RID: 4527
	public static Dictionary<GameObject, AmplifyMotionObjectBase> m_activeObjects = new Dictionary<GameObject, AmplifyMotionObjectBase>();

	// Token: 0x040011B0 RID: 4528
	public static Dictionary<Camera, AmplifyMotionCamera> m_activeCameras = new Dictionary<Camera, AmplifyMotionCamera>();

	// Token: 0x040011B1 RID: 4529
	private static bool m_isD3D = false;

	// Token: 0x040011B2 RID: 4530
	private bool m_canUseGPU;

	// Token: 0x040011B3 RID: 4531
	private const CameraEvent m_updateCBEvent = CameraEvent.BeforeImageEffectsOpaque;

	// Token: 0x040011B4 RID: 4532
	private CommandBuffer m_updateCB;

	// Token: 0x040011B5 RID: 4533
	private const CameraEvent m_fixedUpdateCBEvent = CameraEvent.BeforeImageEffectsOpaque;

	// Token: 0x040011B6 RID: 4534
	private CommandBuffer m_fixedUpdateCB;

	// Token: 0x040011B7 RID: 4535
	private static bool m_ignoreMotionScaleWarning = false;

	// Token: 0x040011B8 RID: 4536
	private static AmplifyMotionEffectBase m_firstInstance = null;
}
