using System;
using UnityEngine;

// Token: 0x02000287 RID: 647
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06001488 RID: 5256 RVA: 0x0009F163 File Offset: 0x0009D563
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x0009F189 File Offset: 0x0009D589
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x0009F191 File Offset: 0x0009D591
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x0009F19C File Offset: 0x0009D59C
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x0009F204 File Offset: 0x0009D604
	protected virtual void Update()
	{
		if (this.mTooltip != UICamera.tooltipObject)
		{
			this.mTooltip = null;
			this.mTarget = 0f;
		}
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 b = this.mSize * 0.25f;
				b.y = -b.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - b, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x0009F328 File Offset: 0x0009D728
	protected virtual void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x0009F370 File Offset: 0x0009D770
	protected virtual void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			this.mTooltip = UICamera.tooltipObject;
			this.text.text = tooltipText;
			this.mPos = UICamera.lastEventPosition;
			Transform transform = this.text.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			this.mSize = this.text.printedSize;
			this.mSize.x = this.mSize.x * localScale.x;
			this.mSize.y = this.mSize.y * localScale.y;
			if (this.background != null)
			{
				Vector4 border = this.background.border;
				this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
				this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
				this.background.width = Mathf.RoundToInt(this.mSize.x);
				this.background.height = Mathf.RoundToInt(this.mSize.y);
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector = new Vector2(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
				this.mPos.x = Mathf.Round(this.mPos.x);
				this.mPos.y = Mathf.Round(this.mPos.y);
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
			this.mTrans.localPosition = this.mPos;
			if (this.tooltipRoot != null)
			{
				this.tooltipRoot.BroadcastMessage("UpdateAnchors");
			}
			else
			{
				this.text.BroadcastMessage("UpdateAnchors");
			}
		}
		else
		{
			this.mTooltip = null;
			this.mTarget = 0f;
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x0009F76F File Offset: 0x0009DB6F
	[Obsolete("Use UITooltip.Show instead")]
	public static void ShowText(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x0009F78C File Offset: 0x0009DB8C
	public static void Show(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0009F7A9 File Offset: 0x0009DBA9
	public static void Hide()
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.mTooltip = null;
			UITooltip.mInstance.mTarget = 0f;
		}
	}

	// Token: 0x0400116A RID: 4458
	protected static UITooltip mInstance;

	// Token: 0x0400116B RID: 4459
	public Camera uiCamera;

	// Token: 0x0400116C RID: 4460
	public UILabel text;

	// Token: 0x0400116D RID: 4461
	public GameObject tooltipRoot;

	// Token: 0x0400116E RID: 4462
	public UISprite background;

	// Token: 0x0400116F RID: 4463
	public float appearSpeed = 10f;

	// Token: 0x04001170 RID: 4464
	public bool scalingTransitions = true;

	// Token: 0x04001171 RID: 4465
	protected GameObject mTooltip;

	// Token: 0x04001172 RID: 4466
	protected Transform mTrans;

	// Token: 0x04001173 RID: 4467
	protected float mTarget;

	// Token: 0x04001174 RID: 4468
	protected float mCurrent;

	// Token: 0x04001175 RID: 4469
	protected Vector3 mPos;

	// Token: 0x04001176 RID: 4470
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x04001177 RID: 4471
	protected UIWidget[] mWidgets;
}
