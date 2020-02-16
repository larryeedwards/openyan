using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020001EE RID: 494
[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation : MonoBehaviour
{
	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00075F84 File Offset: 0x00074384
	private float playbackTime
	{
		get
		{
			return Mathf.Clamp01(this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00075FAC File Offset: 0x000743AC
	public bool isPlaying
	{
		get
		{
			if (!(this.mAnim == null))
			{
				IEnumerator enumerator = this.mAnim.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						AnimationState animationState = (AnimationState)obj;
						if (this.mAnim.IsPlaying(animationState.name))
						{
							if (this.mLastDirection == AnimationOrTween.Direction.Forward)
							{
								if (animationState.time < animationState.length)
								{
									return true;
								}
							}
							else
							{
								if (this.mLastDirection != AnimationOrTween.Direction.Reverse)
								{
									return true;
								}
								if (animationState.time > 0f)
								{
									return true;
								}
							}
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
				return false;
			}
			if (this.mAnimator != null)
			{
				if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
				{
					if (this.playbackTime == 0f)
					{
						return false;
					}
				}
				else if (this.playbackTime == 1f)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x000760DC File Offset: 0x000744DC
	public void Finish()
	{
		if (this.mAnim != null)
		{
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (this.mLastDirection == AnimationOrTween.Direction.Forward)
					{
						animationState.time = animationState.length;
					}
					else if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
					{
						animationState.time = 0f;
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
			this.mAnim.Sample();
		}
		else if (this.mAnimator != null)
		{
			this.mAnimator.Play(this.mClip, 0, (this.mLastDirection != AnimationOrTween.Direction.Forward) ? 0f : 1f);
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x000761D0 File Offset: 0x000745D0
	public void Reset()
	{
		if (this.mAnim != null)
		{
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (this.mLastDirection == AnimationOrTween.Direction.Reverse)
					{
						animationState.time = animationState.length;
					}
					else if (this.mLastDirection == AnimationOrTween.Direction.Forward)
					{
						animationState.time = 0f;
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
		else if (this.mAnimator != null)
		{
			this.mAnimator.Play(this.mClip, 0, (this.mLastDirection != AnimationOrTween.Direction.Reverse) ? 0f : 1f);
		}
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x000762B8 File Offset: 0x000746B8
	private void Start()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x000762EC File Offset: 0x000746EC
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		if (deltaTime == 0f)
		{
			return;
		}
		if (this.mAnimator != null)
		{
			this.mAnimator.Update((this.mLastDirection != AnimationOrTween.Direction.Reverse) ? deltaTime : (-deltaTime));
			if (this.isPlaying)
			{
				return;
			}
			this.mAnimator.enabled = false;
			base.enabled = false;
		}
		else
		{
			if (!(this.mAnim != null))
			{
				base.enabled = false;
				return;
			}
			bool flag = false;
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (this.mAnim.IsPlaying(animationState.name))
					{
						float num = animationState.speed * deltaTime;
						animationState.time += num;
						if (num < 0f)
						{
							if (animationState.time > 0f)
							{
								flag = true;
							}
							else
							{
								animationState.time = 0f;
							}
						}
						else if (animationState.time < animationState.length)
						{
							flag = true;
						}
						else
						{
							animationState.time = animationState.length;
						}
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
			this.mAnim.Sample();
			if (flag)
			{
				return;
			}
			base.enabled = false;
		}
		if (this.mNotify)
		{
			this.mNotify = false;
			if (ActiveAnimation.current == null)
			{
				ActiveAnimation.current = this;
				EventDelegate.Execute(this.onFinished);
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
				}
				ActiveAnimation.current = null;
			}
			if (this.mDisableDirection != AnimationOrTween.Direction.Toggle && this.mLastDirection == this.mDisableDirection)
			{
				NGUITools.SetActive(base.gameObject, false);
			}
		}
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0007650C File Offset: 0x0007490C
	private void Play(string clipName, AnimationOrTween.Direction playDirection)
	{
		if (playDirection == AnimationOrTween.Direction.Toggle)
		{
			playDirection = ((this.mLastDirection == AnimationOrTween.Direction.Forward) ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward);
		}
		if (this.mAnim != null)
		{
			base.enabled = true;
			this.mAnim.enabled = false;
			bool flag = string.IsNullOrEmpty(clipName);
			if (flag)
			{
				if (!this.mAnim.isPlaying)
				{
					this.mAnim.Play();
				}
			}
			else if (!this.mAnim.IsPlaying(clipName))
			{
				this.mAnim.Play(clipName);
			}
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
					{
						float num = Mathf.Abs(animationState.speed);
						animationState.speed = num * (float)playDirection;
						if (playDirection == AnimationOrTween.Direction.Reverse && animationState.time == 0f)
						{
							animationState.time = animationState.length;
						}
						else if (playDirection == AnimationOrTween.Direction.Forward && animationState.time == animationState.length)
						{
							animationState.time = 0f;
						}
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
			this.mLastDirection = playDirection;
			this.mNotify = true;
			this.mAnim.Sample();
		}
		else if (this.mAnimator != null)
		{
			if (base.enabled && this.isPlaying && this.mClip == clipName)
			{
				this.mLastDirection = playDirection;
				return;
			}
			base.enabled = true;
			this.mNotify = true;
			this.mLastDirection = playDirection;
			this.mClip = clipName;
			this.mAnimator.Play(this.mClip, 0, (playDirection != AnimationOrTween.Direction.Forward) ? 1f : 0f);
		}
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00076718 File Offset: 0x00074B18
	public static ActiveAnimation Play(Animation anim, string clipName, AnimationOrTween.Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		if (!NGUITools.GetActive(anim.gameObject))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
			UIPanel[] componentsInChildren = anim.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].Refresh();
				i++;
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (AnimationOrTween.Direction)disableCondition;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(clipName, playDirection);
		if (activeAnimation.mAnim != null)
		{
			activeAnimation.mAnim.Sample();
		}
		else if (activeAnimation.mAnimator != null)
		{
			activeAnimation.mAnimator.Update(0f);
		}
		return activeAnimation;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000767F8 File Offset: 0x00074BF8
	public static ActiveAnimation Play(Animation anim, string clipName, AnimationOrTween.Direction playDirection)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00076804 File Offset: 0x00074C04
	public static ActiveAnimation Play(Animation anim, AnimationOrTween.Direction playDirection)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00076810 File Offset: 0x00074C10
	public static ActiveAnimation Play(Animator anim, string clipName, AnimationOrTween.Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		if (enableBeforePlay != EnableCondition.IgnoreDisabledState && !NGUITools.GetActive(anim.gameObject))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
			UIPanel[] componentsInChildren = anim.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].Refresh();
				i++;
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnimator = anim;
		activeAnimation.mDisableDirection = (AnimationOrTween.Direction)disableCondition;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(clipName, playDirection);
		if (activeAnimation.mAnim != null)
		{
			activeAnimation.mAnim.Sample();
		}
		else if (activeAnimation.mAnimator != null)
		{
			activeAnimation.mAnimator.Update(0f);
		}
		return activeAnimation;
	}

	// Token: 0x04000D53 RID: 3411
	public static ActiveAnimation current;

	// Token: 0x04000D54 RID: 3412
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000D55 RID: 3413
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000D56 RID: 3414
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000D57 RID: 3415
	private Animation mAnim;

	// Token: 0x04000D58 RID: 3416
	private AnimationOrTween.Direction mLastDirection;

	// Token: 0x04000D59 RID: 3417
	private AnimationOrTween.Direction mDisableDirection;

	// Token: 0x04000D5A RID: 3418
	private bool mNotify;

	// Token: 0x04000D5B RID: 3419
	private Animator mAnimator;

	// Token: 0x04000D5C RID: 3420
	private string mClip = string.Empty;
}
