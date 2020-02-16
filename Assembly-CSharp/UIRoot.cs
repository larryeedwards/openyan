using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027B RID: 635
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06001418 RID: 5144 RVA: 0x0009C4CE File Offset: 0x0009A8CE
	public UIRoot.Constraint constraint
	{
		get
		{
			if (this.fitWidth)
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.Fit;
				}
				return UIRoot.Constraint.FitWidth;
			}
			else
			{
				if (this.fitHeight)
				{
					return UIRoot.Constraint.FitHeight;
				}
				return UIRoot.Constraint.Fill;
			}
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06001419 RID: 5145 RVA: 0x0009C4F8 File Offset: 0x0009A8F8
	public UIRoot.Scaling activeScaling
	{
		get
		{
			UIRoot.Scaling scaling = this.scalingStyle;
			if (scaling == UIRoot.Scaling.ConstrainedOnMobiles)
			{
				return UIRoot.Scaling.Flexible;
			}
			return scaling;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x0600141A RID: 5146 RVA: 0x0009C518 File Offset: 0x0009A918
	public int activeHeight
	{
		get
		{
			if (this.activeScaling == UIRoot.Scaling.Flexible)
			{
				Vector2 screenSize = NGUITools.screenSize;
				float num = screenSize.x / screenSize.y;
				if (screenSize.y < (float)this.minimumHeight)
				{
					screenSize.y = (float)this.minimumHeight;
					screenSize.x = screenSize.y * num;
				}
				else if (screenSize.y > (float)this.maximumHeight)
				{
					screenSize.y = (float)this.maximumHeight;
					screenSize.x = screenSize.y * num;
				}
				int num2 = Mathf.RoundToInt((!this.shrinkPortraitUI || screenSize.y <= screenSize.x) ? screenSize.y : (screenSize.y / num));
				return (!this.adjustByDPI) ? num2 : NGUIMath.AdjustByDPI((float)num2);
			}
			UIRoot.Constraint constraint = this.constraint;
			if (constraint == UIRoot.Constraint.FitHeight)
			{
				return this.manualHeight;
			}
			Vector2 screenSize2 = NGUITools.screenSize;
			float num3 = screenSize2.x / screenSize2.y;
			float num4 = (float)this.manualWidth / (float)this.manualHeight;
			if (constraint == UIRoot.Constraint.FitWidth)
			{
				return Mathf.RoundToInt((float)this.manualWidth / num3);
			}
			if (constraint == UIRoot.Constraint.Fit)
			{
				return (num4 <= num3) ? this.manualHeight : Mathf.RoundToInt((float)this.manualWidth / num3);
			}
			if (constraint != UIRoot.Constraint.Fill)
			{
				return this.manualHeight;
			}
			return (num4 >= num3) ? this.manualHeight : Mathf.RoundToInt((float)this.manualWidth / num3);
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x0600141B RID: 5147 RVA: 0x0009C6BC File Offset: 0x0009AABC
	public float pixelSizeAdjustment
	{
		get
		{
			int num = Mathf.RoundToInt(NGUITools.screenSize.y);
			return (num != -1) ? this.GetPixelSizeAdjustment(num) : 1f;
		}
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0009C6F4 File Offset: 0x0009AAF4
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uiroot != null)) ? 1f : uiroot.pixelSizeAdjustment;
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0009C724 File Offset: 0x0009AB24
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.activeScaling == UIRoot.Scaling.Constrained)
		{
			return (float)this.activeHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0009C784 File Offset: 0x0009AB84
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x0009C792 File Offset: 0x0009AB92
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0009C79F File Offset: 0x0009AB9F
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0009C7B0 File Offset: 0x0009ABB0
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			this.UpdateScale(false);
		}
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0009C811 File Offset: 0x0009AC11
	private void Update()
	{
		this.UpdateScale(true);
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0009C81C File Offset: 0x0009AC1C
	public void UpdateScale(bool updateAnchors = true)
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1.401298E-45f || Mathf.Abs(localScale.y - num2) > 1.401298E-45f || Mathf.Abs(localScale.z - num2) > 1.401298E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
					if (updateAnchors)
					{
						base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0009C8D0 File Offset: 0x0009ACD0
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x0009C91C File Offset: 0x0009AD1C
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.list.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.list[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
				}
				i++;
			}
		}
	}

	// Token: 0x0400110D RID: 4365
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x0400110E RID: 4366
	public UIRoot.Scaling scalingStyle;

	// Token: 0x0400110F RID: 4367
	public int manualWidth = 1280;

	// Token: 0x04001110 RID: 4368
	public int manualHeight = 720;

	// Token: 0x04001111 RID: 4369
	public int minimumHeight = 320;

	// Token: 0x04001112 RID: 4370
	public int maximumHeight = 1536;

	// Token: 0x04001113 RID: 4371
	public bool fitWidth;

	// Token: 0x04001114 RID: 4372
	public bool fitHeight = true;

	// Token: 0x04001115 RID: 4373
	public bool adjustByDPI;

	// Token: 0x04001116 RID: 4374
	public bool shrinkPortraitUI;

	// Token: 0x04001117 RID: 4375
	private Transform mTrans;

	// Token: 0x0200027C RID: 636
	public enum Scaling
	{
		// Token: 0x04001119 RID: 4377
		Flexible,
		// Token: 0x0400111A RID: 4378
		Constrained,
		// Token: 0x0400111B RID: 4379
		ConstrainedOnMobiles
	}

	// Token: 0x0200027D RID: 637
	public enum Constraint
	{
		// Token: 0x0400111D RID: 4381
		Fit,
		// Token: 0x0400111E RID: 4382
		Fill,
		// Token: 0x0400111F RID: 4383
		FitWidth,
		// Token: 0x04001120 RID: 4384
		FitHeight
	}
}
