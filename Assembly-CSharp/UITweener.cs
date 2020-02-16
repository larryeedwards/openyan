using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000241 RID: 577
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0008B028 File Offset: 0x00089428
	public float amountPerDelta
	{
		get
		{
			if (this.duration == 0f)
			{
				return 1000f;
			}
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs(1f / this.duration) * Mathf.Sign(this.mAmountPerDelta);
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0008B091 File Offset: 0x00089491
	// (set) Token: 0x060011D8 RID: 4568 RVA: 0x0008B099 File Offset: 0x00089499
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
		set
		{
			this.mFactor = Mathf.Clamp01(value);
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0008B0A7 File Offset: 0x000894A7
	public AnimationOrTween.Direction direction
	{
		get
		{
			return (this.amountPerDelta >= 0f) ? AnimationOrTween.Direction.Forward : AnimationOrTween.Direction.Reverse;
		}
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0008B0C0 File Offset: 0x000894C0
	private void Reset()
	{
		if (!this.mStarted)
		{
			this.SetStartToCurrentValue();
			this.SetEndToCurrentValue();
		}
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x0008B0D9 File Offset: 0x000894D9
	protected virtual void Start()
	{
		this.DoUpdate();
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x0008B0E1 File Offset: 0x000894E1
	protected void Update()
	{
		if (!this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x0008B0F4 File Offset: 0x000894F4
	protected void FixedUpdate()
	{
		if (this.useFixedUpdate)
		{
			this.DoUpdate();
		}
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0008B108 File Offset: 0x00089508
	protected void DoUpdate()
	{
		float num = (!this.ignoreTimeScale || this.useFixedUpdate) ? Time.deltaTime : Time.unscaledDeltaTime;
		float num2 = (!this.ignoreTimeScale || this.useFixedUpdate) ? Time.time : Time.unscaledTime;
		if (!this.mStarted)
		{
			num = 0f;
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += ((this.duration != 0f) ? (this.amountPerDelta * num) : 1f);
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.duration == 0f || this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			base.enabled = false;
			if (UITweener.current != this)
			{
				UITweener uitweener = UITweener.current;
				UITweener.current = this;
				if (this.onFinished != null)
				{
					this.mTemp = this.onFinished;
					this.onFinished = new List<EventDelegate>();
					EventDelegate.Execute(this.mTemp);
					for (int i = 0; i < this.mTemp.Count; i++)
					{
						EventDelegate eventDelegate = this.mTemp[i];
						if (eventDelegate != null && !eventDelegate.oneShot)
						{
							EventDelegate.Add(this.onFinished, eventDelegate, eventDelegate.oneShot);
						}
					}
					this.mTemp = null;
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				UITweener.current = uitweener;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x0008B3D7 File Offset: 0x000897D7
	public void SetOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x0008B3E6 File Offset: 0x000897E6
	public void SetOnFinished(EventDelegate del)
	{
		EventDelegate.Set(this.onFinished, del);
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x0008B3F4 File Offset: 0x000897F4
	public void AddOnFinished(EventDelegate.Callback del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x0008B403 File Offset: 0x00089803
	public void AddOnFinished(EventDelegate del)
	{
		EventDelegate.Add(this.onFinished, del);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x0008B411 File Offset: 0x00089811
	public void RemoveOnFinished(EventDelegate del)
	{
		if (this.onFinished != null)
		{
			this.onFinished.Remove(del);
		}
		if (this.mTemp != null)
		{
			this.mTemp.Remove(del);
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x0008B443 File Offset: 0x00089843
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x0008B44C File Offset: 0x0008984C
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.57079637f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.57079637f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.28318548f) / 6.28318548f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve == null) ? num : this.animationCurve.Evaluate(num), isFinished);
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x0008B5A0 File Offset: 0x000899A0
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x0008B637 File Offset: 0x00089A37
	[Obsolete("Use PlayForward() instead")]
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x0008B640 File Offset: 0x00089A40
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0008B649 File Offset: 0x00089A49
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x0008B654 File Offset: 0x00089A54
	public virtual void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		if (!base.enabled)
		{
			base.enabled = true;
			this.mStarted = false;
		}
		this.DoUpdate();
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x0008B6A4 File Offset: 0x00089AA4
	public void ResetToBeginning()
	{
		this.mStarted = false;
		this.mFactor = ((this.amountPerDelta >= 0f) ? 0f : 1f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x0008B6DF File Offset: 0x00089ADF
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x060011ED RID: 4589
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x060011EE RID: 4590 RVA: 0x0008B71C File Offset: 0x00089B1C
	public static T Begin<T>(GameObject go, float duration, float delay = 0f) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t != null && t.tweenGroup != 0)
		{
			t = (T)((object)null);
			T[] components = go.GetComponents<T>();
			int i = 0;
			int num = components.Length;
			while (i < num)
			{
				t = components[i];
				if (t != null && t.tweenGroup == 0)
				{
					break;
				}
				t = (T)((object)null);
				i++;
			}
		}
		if (t == null)
		{
			t = go.AddComponent<T>();
			if (t == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Unable to add ",
					typeof(T),
					" to ",
					NGUITools.GetHierarchy(go)
				}), go);
				return (T)((object)null);
			}
		}
		t.mStarted = false;
		t.mFactor = 0f;
		t.duration = duration;
		t.mDuration = duration;
		t.delay = delay;
		t.mAmountPerDelta = ((duration <= 0f) ? 1000f : Mathf.Abs(1f / duration));
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.onFinished.Clear();
		if (t.mTemp != null)
		{
			t.mTemp.Clear();
		}
		t.enabled = true;
		return t;
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0008B940 File Offset: 0x00089D40
	public virtual void SetStartToCurrentValue()
	{
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0008B942 File Offset: 0x00089D42
	public virtual void SetEndToCurrentValue()
	{
	}

	// Token: 0x04000F44 RID: 3908
	public static UITweener current;

	// Token: 0x04000F45 RID: 3909
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x04000F46 RID: 3910
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x04000F47 RID: 3911
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x04000F48 RID: 3912
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x04000F49 RID: 3913
	[HideInInspector]
	public float delay;

	// Token: 0x04000F4A RID: 3914
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x04000F4B RID: 3915
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x04000F4C RID: 3916
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x04000F4D RID: 3917
	[Tooltip("By default, Update() will be used for tweening. Setting this to 'true' will make the tween happen in FixedUpdate() insted.")]
	public bool useFixedUpdate;

	// Token: 0x04000F4E RID: 3918
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000F4F RID: 3919
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000F50 RID: 3920
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000F51 RID: 3921
	private bool mStarted;

	// Token: 0x04000F52 RID: 3922
	private float mStartTime;

	// Token: 0x04000F53 RID: 3923
	private float mDuration;

	// Token: 0x04000F54 RID: 3924
	private float mAmountPerDelta = 1000f;

	// Token: 0x04000F55 RID: 3925
	private float mFactor;

	// Token: 0x04000F56 RID: 3926
	private List<EventDelegate> mTemp;

	// Token: 0x02000242 RID: 578
	public enum Method
	{
		// Token: 0x04000F58 RID: 3928
		Linear,
		// Token: 0x04000F59 RID: 3929
		EaseIn,
		// Token: 0x04000F5A RID: 3930
		EaseOut,
		// Token: 0x04000F5B RID: 3931
		EaseInOut,
		// Token: 0x04000F5C RID: 3932
		BounceIn,
		// Token: 0x04000F5D RID: 3933
		BounceOut
	}

	// Token: 0x02000243 RID: 579
	public enum Style
	{
		// Token: 0x04000F5F RID: 3935
		Once,
		// Token: 0x04000F60 RID: 3936
		Loop,
		// Token: 0x04000F61 RID: 3937
		PingPong
	}
}
