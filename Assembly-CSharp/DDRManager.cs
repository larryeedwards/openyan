using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x0200012A RID: 298
public class DDRManager : MonoBehaviour
{
	// Token: 0x06000A82 RID: 2690 RVA: 0x00050BDE File Offset: 0x0004EFDE
	private void Start()
	{
		this.minigameCamera.position = this.startPoint.position;
		if (this.DebugMode)
		{
			this.BeginMinigame();
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00050C08 File Offset: 0x0004F008
	public void Update()
	{
		this.minigameCamera.position = Vector3.Slerp(this.minigameCamera.position, this.target.position, this.transitionSpeed * Time.deltaTime);
		this.minigameCamera.rotation = Quaternion.Slerp(this.minigameCamera.rotation, this.target.rotation, this.transitionSpeed * Time.deltaTime);
		if (this.target == null)
		{
			return;
		}
		Vector3 position = this.standPoint.position;
		if (this.LoadedLevel != null)
		{
			this.ddrMinigame.UpdateGame(this.audioSource.time);
			this.GameState.Health -= Time.deltaTime;
			this.GameState.Health = Mathf.Clamp(this.GameState.Health, 0f, 100f);
			if (this.inputManager.TappedLeft)
			{
				this.yandereAnim["f02_danceLeft_00"].time = 0f;
				this.yandereAnim.Play("f02_danceLeft_00");
			}
			else if (this.inputManager.TappedDown)
			{
				this.yandereAnim["f02_danceDown_00"].time = 0f;
				this.yandereAnim.Play("f02_danceDown_00");
			}
			if (this.inputManager.TappedRight)
			{
				this.yandereAnim["f02_danceRight_00"].time = 0f;
				this.yandereAnim.Play("f02_danceRight_00");
			}
			else if (this.inputManager.TappedUp)
			{
				this.yandereAnim["f02_danceUp_00"].time = 0f;
				this.yandereAnim.Play("f02_danceUp_00");
			}
		}
		this.yandereAnim.transform.position = Vector3.Lerp(this.yandereAnim.transform.position, position, 10f * Time.deltaTime);
		if (this.CheckingForEnd && !this.audioSource.isPlaying)
		{
			this.OverlayCanvas.SetActive(false);
			this.GameUI.SetActive(false);
			this.CheckingForEnd = false;
			Debug.Log("End() was called because song ended.");
			base.StartCoroutine(this.End());
		}
		if (this.GameState.Health <= 0f && this.audioSource.pitch < 0.01f)
		{
			this.OverlayCanvas.SetActive(false);
			this.GameUI.SetActive(false);
			if (this.audioSource.isPlaying)
			{
				Debug.Log("End() was called because we ranout of health.");
				base.StartCoroutine(this.End());
			}
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00050EDC File Offset: 0x0004F2DC
	public void BeginMinigame()
	{
		Debug.Log("BeginMinigame() was called.");
		this.yandereAnim["f02_danceMachineIdle_00"].layer = 0;
		this.yandereAnim["f02_danceRight_00"].layer = 1;
		this.yandereAnim["f02_danceLeft_00"].layer = 2;
		this.yandereAnim["f02_danceUp_00"].layer = 1;
		this.yandereAnim["f02_danceDown_00"].layer = 2;
		this.yandereAnim["f02_danceMachineIdle_00"].weight = 1f;
		this.yandereAnim["f02_danceRight_00"].weight = 1f;
		this.yandereAnim["f02_danceLeft_00"].weight = 1f;
		this.yandereAnim["f02_danceUp_00"].weight = 1f;
		this.yandereAnim["f02_danceDown_00"].weight = 1f;
		this.OverlayCanvas.SetActive(true);
		this.GameUI.SetActive(true);
		this.ddrMinigame.LoadLevelSelect(this.levels);
		base.StartCoroutine(this.minigameFlow());
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0005101C File Offset: 0x0004F41C
	public void BootOut()
	{
		this.minigameCamera.position = this.startPoint.position;
		base.StartCoroutine(this.fade(true, this.fadeImage, 5f));
		this.target = this.startPoint;
		this.ddrMinigame.UnloadLevelSelect();
		this.ReturnToNormalGameplay();
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x00051078 File Offset: 0x0004F478
	private IEnumerator minigameFlow()
	{
		this.levelSelect.gameObject.SetActive(true);
		this.defeatScreen.gameObject.SetActive(false);
		this.endScreen.gameObject.SetActive(false);
		this.audioSource.pitch = 1f;
		this.target = this.screenPoint;
		if (!this.booted)
		{
			yield return new WaitForSecondsRealtime(0.2f);
			base.StartCoroutine(this.fade(false, this.fadeImage, 1f));
			while (this.fadeImage.color.a > 0.4f)
			{
				yield return null;
			}
			this.machineScreenAnimation.Play();
			this.booted = true;
		}
		yield return new WaitForEndOfFrame();
		while (Input.GetAxis("A") != 0f)
		{
			yield return null;
		}
		while (this.LoadedLevel == null)
		{
			this.ddrMinigame.UpdateLevelSelect();
			yield return null;
		}
		this.ddrMinigame.LoadLevel(this.LoadedLevel);
		this.GameState = new GameState();
		yield return new WaitForSecondsRealtime(0.2f);
		this.transitionSpeed *= 2f;
		this.target = this.watchPoint;
		this.backgroundVideo.Play();
		this.backgroundVideo.playbackSpeed = 0f;
		base.StartCoroutine(this.fadeGameUI(true));
		this.backgroundVideo.playbackSpeed = 1f;
		this.audioSource.clip = this.LoadedLevel.Song;
		this.audioSource.Play();
		this.CheckingForEnd = true;
		while (this.audioSource.time < this.audioSource.clip.length)
		{
			if (this.GameState.Health <= 0f)
			{
				this.GameState.FinishStatus = DDRFinishStatus.Failed;
				while (this.audioSource.pitch > 0f)
				{
					this.audioSource.pitch = Mathf.MoveTowards(this.audioSource.pitch, 0f, 0.2f * Time.deltaTime);
					if (this.audioSource.pitch == 0f)
					{
						Debug.Log("Pitch reached zero.");
						this.audioSource.time = this.audioSource.clip.length;
						this.OverlayCanvas.SetActive(false);
						this.GameUI.SetActive(false);
					}
					yield return null;
				}
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x00051094 File Offset: 0x0004F494
	private IEnumerator End()
	{
		this.audioSource.Stop();
		this.levelSelect.gameObject.SetActive(false);
		base.StopCoroutine(this.fadeGameUI(true));
		base.StartCoroutine(this.fadeGameUI(false));
		if (this.GameState.FinishStatus == DDRFinishStatus.Complete)
		{
			this.endScreen.gameObject.SetActive(true);
			this.ddrMinigame.UpdateEndcard(this.GameState);
		}
		else
		{
			this.defeatScreen.SetActive(true);
		}
		this.target = this.screenPoint;
		this.LoadedLevel = null;
		this.ddrMinigame.UnloadLevelSelect();
		yield return new WaitForSecondsRealtime(2f);
		base.StartCoroutine(this.fade(true, this.continueText, 1f));
		while (!Input.anyKeyDown || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			yield return null;
		}
		this.ddrMinigame.Unload();
		this.onLevelFinish(this.GameState.FinishStatus);
		yield break;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x000510B0 File Offset: 0x0004F4B0
	private IEnumerator fadeGameUI(bool fadein)
	{
		float destination = (float)((!fadein) ? 0 : 1);
		float amount = (float)((!fadein) ? 1 : 0);
		while (amount != destination)
		{
			amount = Mathf.Lerp(amount, destination, 10f * Time.deltaTime);
			foreach (RawImage rawImage in this.overlayImages)
			{
				Color color = rawImage.color;
				color.a = amount;
				rawImage.color = color;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x000510D4 File Offset: 0x0004F4D4
	private IEnumerator fade(bool fadein, MaskableGraphic graphic, float speed = 1f)
	{
		float destination = (float)((!fadein) ? 0 : 1);
		float amount = (float)((!fadein) ? 1 : 0);
		while (amount != destination)
		{
			amount = Mathf.Lerp(amount, destination, speed * Time.deltaTime);
			Color color = graphic.color;
			color.a = amount;
			graphic.color = color;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x000510FD File Offset: 0x0004F4FD
	private void onLevelFinish(DDRFinishStatus status)
	{
		this.BootOut();
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00051108 File Offset: 0x0004F508
	public void ReturnToNormalGameplay()
	{
		Debug.Log("ReturnToNormalGameplay() was called.");
		this.yandereAnim["f02_danceMachineIdle_00"].weight = 0f;
		this.yandereAnim["f02_danceRight_00"].weight = 0f;
		this.yandereAnim["f02_danceLeft_00"].weight = 0f;
		this.yandereAnim["f02_danceUp_00"].weight = 0f;
		this.yandereAnim["f02_danceDown_00"].weight = 0f;
		this.Yandere.transform.position = this.FinishLocation.position;
		this.Yandere.transform.rotation = this.FinishLocation.rotation;
		this.Yandere.StudentManager.Clock.StopTime = false;
		this.Yandere.MyController.enabled = true;
		this.Yandere.StudentManager.ComeBack();
		this.Yandere.CanMove = true;
		this.Yandere.enabled = true;
		this.Yandere.HeartCamera.enabled = true;
		this.Yandere.HUD.enabled = true;
		this.Yandere.HUD.transform.parent.gameObject.SetActive(true);
		this.Yandere.MainCamera.gameObject.SetActive(true);
		this.Yandere.Jukebox.Volume = this.Yandere.Jukebox.LastVolume;
		this.OriginalRenderer.enabled = true;
		Physics.SyncTransforms();
		base.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x0400073A RID: 1850
	public GameState GameState;

	// Token: 0x0400073B RID: 1851
	public YandereScript Yandere;

	// Token: 0x0400073C RID: 1852
	public Transform FinishLocation;

	// Token: 0x0400073D RID: 1853
	public Renderer OriginalRenderer;

	// Token: 0x0400073E RID: 1854
	public GameObject OverlayCanvas;

	// Token: 0x0400073F RID: 1855
	public GameObject GameUI;

	// Token: 0x04000740 RID: 1856
	[Header("General")]
	public DDRLevel LoadedLevel;

	// Token: 0x04000741 RID: 1857
	[SerializeField]
	private DDRLevel[] levels;

	// Token: 0x04000742 RID: 1858
	[SerializeField]
	private InputManagerScript inputManager;

	// Token: 0x04000743 RID: 1859
	[SerializeField]
	private DDRMinigame ddrMinigame;

	// Token: 0x04000744 RID: 1860
	[SerializeField]
	private AudioSource audioSource;

	// Token: 0x04000745 RID: 1861
	[SerializeField]
	private Transform standPoint;

	// Token: 0x04000746 RID: 1862
	[SerializeField]
	private float transitionSpeed = 2f;

	// Token: 0x04000747 RID: 1863
	[Header("Camera")]
	[SerializeField]
	private Transform minigameCamera;

	// Token: 0x04000748 RID: 1864
	[SerializeField]
	private Transform startPoint;

	// Token: 0x04000749 RID: 1865
	[SerializeField]
	private Transform screenPoint;

	// Token: 0x0400074A RID: 1866
	[SerializeField]
	private Transform watchPoint;

	// Token: 0x0400074B RID: 1867
	[Header("Animation")]
	[SerializeField]
	private Animation machineScreenAnimation;

	// Token: 0x0400074C RID: 1868
	[SerializeField]
	private Animation yandereAnim;

	// Token: 0x0400074D RID: 1869
	[Header("UI")]
	[SerializeField]
	private Image fadeImage;

	// Token: 0x0400074E RID: 1870
	[SerializeField]
	private RawImage[] overlayImages;

	// Token: 0x0400074F RID: 1871
	[SerializeField]
	private VideoPlayer backgroundVideo;

	// Token: 0x04000750 RID: 1872
	[SerializeField]
	private Transform levelSelect;

	// Token: 0x04000751 RID: 1873
	[SerializeField]
	private GameObject endScreen;

	// Token: 0x04000752 RID: 1874
	[SerializeField]
	private GameObject defeatScreen;

	// Token: 0x04000753 RID: 1875
	[SerializeField]
	private Text continueText;

	// Token: 0x04000754 RID: 1876
	[SerializeField]
	private ColorCorrectionCurves gameplayColorCorrection;

	// Token: 0x04000755 RID: 1877
	private Transform target;

	// Token: 0x04000756 RID: 1878
	private bool booted;

	// Token: 0x04000757 RID: 1879
	public bool DebugMode;

	// Token: 0x04000758 RID: 1880
	public bool CheckingForEnd;
}
