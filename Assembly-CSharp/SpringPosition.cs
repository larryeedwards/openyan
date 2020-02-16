using System;
using UnityEngine;

// Token: 0x0200022F RID: 559
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x0600113F RID: 4415 RVA: 0x0008ACFA File Offset: 0x000890FA
	private void Start()
	{
		this.mTrans = base.transform;
		if (this.updateScrollView)
		{
			this.mSv = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0008AD24 File Offset: 0x00089124
	private void OnEnable()
	{
		this.mThreshold = 0f;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0008AD34 File Offset: 0x00089134
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).sqrMagnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).sqrMagnitude)
			{
				this.mTrans.position = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).sqrMagnitude * 1E-05f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).sqrMagnitude)
			{
				this.mTrans.localPosition = this.target;
				this.NotifyListeners();
				base.enabled = false;
			}
		}
		if (this.mSv != null)
		{
			this.mSv.UpdateScrollbars(true);
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0008AEDC File Offset: 0x000892DC
	private void NotifyListeners()
	{
		SpringPosition.current = this;
		if (this.onFinished != null)
		{
			this.onFinished();
		}
		if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
		{
			this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
		}
		SpringPosition.current = null;
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x0008AF40 File Offset: 0x00089340
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x04000EE6 RID: 3814
	public static SpringPosition current;

	// Token: 0x04000EE7 RID: 3815
	public Vector3 target = Vector3.zero;

	// Token: 0x04000EE8 RID: 3816
	public float strength = 10f;

	// Token: 0x04000EE9 RID: 3817
	public bool worldSpace;

	// Token: 0x04000EEA RID: 3818
	public bool ignoreTimeScale;

	// Token: 0x04000EEB RID: 3819
	public bool updateScrollView;

	// Token: 0x04000EEC RID: 3820
	public SpringPosition.OnFinished onFinished;

	// Token: 0x04000EED RID: 3821
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04000EEE RID: 3822
	[SerializeField]
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x04000EEF RID: 3823
	private Transform mTrans;

	// Token: 0x04000EF0 RID: 3824
	private float mThreshold;

	// Token: 0x04000EF1 RID: 3825
	private UIScrollView mSv;

	// Token: 0x02000230 RID: 560
	// (Invoke) Token: 0x06001145 RID: 4421
	public delegate void OnFinished();
}
