using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200024B RID: 587
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x17000246 RID: 582
	// (get) Token: 0x0600122C RID: 4652 RVA: 0x0008F713 File Offset: 0x0008DB13
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x0600122D RID: 4653 RVA: 0x0008F716 File Offset: 0x0008DB16
	// (set) Token: 0x0600122E RID: 4654 RVA: 0x0008F72D File Offset: 0x0008DB2D
	public static bool disableController
	{
		get
		{
			return UICamera.mDisableController && !UIPopupList.isOpen;
		}
		set
		{
			UICamera.mDisableController = value;
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x0600122F RID: 4655 RVA: 0x0008F735 File Offset: 0x0008DB35
	// (set) Token: 0x06001230 RID: 4656 RVA: 0x0008F73C File Offset: 0x0008DB3C
	[Obsolete("Use lastEventPosition instead. It handles controller input properly.")]
	public static Vector2 lastTouchPosition
	{
		get
		{
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06001231 RID: 4657 RVA: 0x0008F744 File Offset: 0x0008DB44
	// (set) Token: 0x06001232 RID: 4658 RVA: 0x0008F7A0 File Offset: 0x0008DBA0
	public static Vector2 lastEventPosition
	{
		get
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == UICamera.ControlScheme.Controller)
			{
				GameObject hoveredObject = UICamera.hoveredObject;
				if (hoveredObject != null)
				{
					Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(hoveredObject.transform);
					Camera camera = NGUITools.FindCameraForLayer(hoveredObject.layer);
					return camera.WorldToScreenPoint(bounds.center);
				}
			}
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06001233 RID: 4659 RVA: 0x0008F7A8 File Offset: 0x0008DBA8
	public static UICamera first
	{
		get
		{
			if (UICamera.list == null || UICamera.list.size == 0)
			{
				return null;
			}
			return UICamera.list[0];
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06001234 RID: 4660 RVA: 0x0008F7D0 File Offset: 0x0008DBD0
	// (set) Token: 0x06001235 RID: 4661 RVA: 0x0008F864 File Offset: 0x0008DC64
	public static UICamera.ControlScheme currentScheme
	{
		get
		{
			if (UICamera.mCurrentKey == KeyCode.None)
			{
				return UICamera.ControlScheme.Touch;
			}
			if (UICamera.mCurrentKey >= KeyCode.JoystickButton0)
			{
				return UICamera.ControlScheme.Controller;
			}
			if (!(UICamera.current != null))
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.mLastScheme == UICamera.ControlScheme.Controller && (UICamera.mCurrentKey == UICamera.current.submitKey0 || UICamera.mCurrentKey == UICamera.current.submitKey1))
			{
				return UICamera.ControlScheme.Controller;
			}
			if (UICamera.current.useMouse)
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.current.useTouch)
			{
				return UICamera.ControlScheme.Touch;
			}
			return UICamera.ControlScheme.Controller;
		}
		set
		{
			if (UICamera.mLastScheme != value)
			{
				if (value == UICamera.ControlScheme.Mouse)
				{
					UICamera.currentKey = KeyCode.Mouse0;
				}
				else if (value == UICamera.ControlScheme.Controller)
				{
					UICamera.currentKey = KeyCode.JoystickButton0;
				}
				else if (value == UICamera.ControlScheme.Touch)
				{
					UICamera.currentKey = KeyCode.None;
				}
				else
				{
					UICamera.currentKey = KeyCode.Alpha0;
				}
				UICamera.mLastScheme = value;
			}
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06001236 RID: 4662 RVA: 0x0008F8C6 File Offset: 0x0008DCC6
	// (set) Token: 0x06001237 RID: 4663 RVA: 0x0008F8D0 File Offset: 0x0008DCD0
	public static KeyCode currentKey
	{
		get
		{
			return UICamera.mCurrentKey;
		}
		set
		{
			if (UICamera.mCurrentKey != value)
			{
				UICamera.ControlScheme controlScheme = UICamera.mLastScheme;
				UICamera.mCurrentKey = value;
				UICamera.mLastScheme = UICamera.currentScheme;
				if (controlScheme != UICamera.mLastScheme)
				{
					UICamera.HideTooltip();
					if (UICamera.mLastScheme == UICamera.ControlScheme.Mouse)
					{
						Cursor.lockState = CursorLockMode.None;
						Cursor.visible = true;
					}
					else if (UICamera.current != null && UICamera.current.autoHideCursor)
					{
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
						UICamera.mMouse[0].ignoreDelta = 2;
					}
					if (UICamera.onSchemeChange != null)
					{
						UICamera.onSchemeChange();
					}
				}
			}
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06001238 RID: 4664 RVA: 0x0008F978 File Offset: 0x0008DD78
	public static Ray currentRay
	{
		get
		{
			return (!(UICamera.currentCamera != null) || UICamera.currentTouch == null) ? default(Ray) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06001239 RID: 4665 RVA: 0x0008F9C6 File Offset: 0x0008DDC6
	public static bool inputHasFocus
	{
		get
		{
			return UICamera.mInputFocus && UICamera.mSelected && UICamera.mSelected.activeInHierarchy;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x0600123A RID: 4666 RVA: 0x0008F9F3 File Offset: 0x0008DDF3
	// (set) Token: 0x0600123B RID: 4667 RVA: 0x0008F9FA File Offset: 0x0008DDFA
	[Obsolete("Use delegates instead such as UICamera.onClick, UICamera.onHover, etc.")]
	public static GameObject genericEventHandler
	{
		get
		{
			return UICamera.mGenericHandler;
		}
		set
		{
			UICamera.mGenericHandler = value;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x0600123C RID: 4668 RVA: 0x0008FA02 File Offset: 0x0008DE02
	public static UICamera.MouseOrTouch mouse0
	{
		get
		{
			return UICamera.mMouse[0];
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x0600123D RID: 4669 RVA: 0x0008FA0B File Offset: 0x0008DE0B
	public static UICamera.MouseOrTouch mouse1
	{
		get
		{
			return UICamera.mMouse[1];
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x0600123E RID: 4670 RVA: 0x0008FA14 File Offset: 0x0008DE14
	public static UICamera.MouseOrTouch mouse2
	{
		get
		{
			return UICamera.mMouse[2];
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x0600123F RID: 4671 RVA: 0x0008FA1D File Offset: 0x0008DE1D
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06001240 RID: 4672 RVA: 0x0008FA2A File Offset: 0x0008DE2A
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06001241 RID: 4673 RVA: 0x0008FA4F File Offset: 0x0008DE4F
	public static GameObject tooltipObject
	{
		get
		{
			return UICamera.mTooltip;
		}
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x0008FA56 File Offset: 0x0008DE56
	public static bool IsPartOfUI(GameObject go)
	{
		return !(go == null) && !(go == UICamera.fallThrough) && NGUITools.FindInParents<UIRoot>(go) != null;
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06001243 RID: 4675 RVA: 0x0008FA84 File Offset: 0x0008DE84
	public static bool isOverUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastOverCheck != frameCount)
			{
				UICamera.mLastOverCheck = frameCount;
				if (UICamera.currentTouch != null)
				{
					UICamera.mLastOverResult = UICamera.currentTouch.isOverUI;
					return UICamera.mLastOverResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastOverResult = true;
						return UICamera.mLastOverResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch2.current))
					{
						UICamera.mLastOverResult = true;
						return UICamera.mLastOverResult;
					}
				}
				UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastOverResult;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06001244 RID: 4676 RVA: 0x0008FB64 File Offset: 0x0008DF64
	public static bool uiHasFocus
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastFocusCheck != frameCount)
			{
				UICamera.mLastFocusCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastFocusResult = true;
					return UICamera.mLastFocusResult;
				}
				if (UICamera.currentTouch != null)
				{
					UICamera.mLastFocusResult = UICamera.currentTouch.isOverUI;
					return UICamera.mLastFocusResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch2.pressed) || UICamera.IsPartOfUI(mouseOrTouch2.current))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
				}
				UICamera.mLastFocusResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastFocusResult;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06001245 RID: 4677 RVA: 0x0008FC6C File Offset: 0x0008E06C
	public static bool interactingWithUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastInteractionCheck != frameCount)
			{
				UICamera.mLastInteractionCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastInteractionResult = true;
					return UICamera.mLastInteractionResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch2.pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
				}
				UICamera.mLastInteractionResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastInteractionResult;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06001246 RID: 4678 RVA: 0x0008FD44 File Offset: 0x0008E144
	// (set) Token: 0x06001247 RID: 4679 RVA: 0x0008FDAC File Offset: 0x0008E1AC
	public static GameObject hoveredObject
	{
		get
		{
			if (UICamera.currentTouch != null && (UICamera.currentScheme != UICamera.ControlScheme.Mouse || UICamera.currentTouch.dragStarted))
			{
				return UICamera.currentTouch.current;
			}
			if (UICamera.mHover && UICamera.mHover.activeInHierarchy)
			{
				return UICamera.mHover;
			}
			UICamera.mHover = null;
			return null;
		}
		set
		{
			if (UICamera.mHover == value)
			{
				return;
			}
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.ShowTooltip(null);
			if (UICamera.mSelected && UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
				UICamera.mSelected = null;
			}
			if (UICamera.mHover)
			{
				UICamera.Notify(UICamera.mHover, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, false);
				}
			}
			UICamera.mHover = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (UICamera.mHover)
			{
				if (UICamera.mHover != UICamera.controller.current && UICamera.mHover.GetComponent<UIKeyNavigation>() != null)
				{
					UICamera.controller.current = UICamera.mHover;
				}
				if (flag)
				{
					UICamera uicamera2 = (!(UICamera.mHover != null)) ? UICamera.list[0] : UICamera.FindCameraForLayer(UICamera.mHover.layer);
					if (uicamera2 != null)
					{
						UICamera.current = uicamera2;
						UICamera.currentCamera = uicamera2.cachedCamera;
					}
				}
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, true);
				}
				UICamera.Notify(UICamera.mHover, "OnHover", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((!(uicamera != null)) ? null : uicamera.cachedCamera);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06001248 RID: 4680 RVA: 0x0008FF98 File Offset: 0x0008E398
	// (set) Token: 0x06001249 RID: 4681 RVA: 0x00090100 File Offset: 0x0008E500
	public static GameObject controllerNavigationObject
	{
		get
		{
			if (UICamera.controller.current && UICamera.controller.current.activeInHierarchy)
			{
				return UICamera.controller.current;
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.current != null && UICamera.current.useController && !UICamera.ignoreControllerInput && UIKeyNavigation.list.size > 0)
			{
				for (int i = 0; i < UIKeyNavigation.list.size; i++)
				{
					UIKeyNavigation uikeyNavigation = UIKeyNavigation.list[i];
					if (uikeyNavigation && uikeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uikeyNavigation.startsSelected)
					{
						UICamera.hoveredObject = uikeyNavigation.gameObject;
						UICamera.controller.current = UICamera.mHover;
						return UICamera.mHover;
					}
				}
				if (UICamera.mHover == null)
				{
					for (int j = 0; j < UIKeyNavigation.list.size; j++)
					{
						UIKeyNavigation uikeyNavigation2 = UIKeyNavigation.list[j];
						if (uikeyNavigation2 && uikeyNavigation2.constraint != UIKeyNavigation.Constraint.Explicit)
						{
							UICamera.hoveredObject = uikeyNavigation2.gameObject;
							UICamera.controller.current = UICamera.mHover;
							return UICamera.mHover;
						}
					}
				}
			}
			UICamera.controller.current = null;
			return null;
		}
		set
		{
			if (UICamera.controller.current != value && UICamera.controller.current)
			{
				UICamera.Notify(UICamera.controller.current, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.controller.current, false);
				}
				UICamera.controller.current = null;
			}
			UICamera.hoveredObject = value;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x0600124A RID: 4682 RVA: 0x00090180 File Offset: 0x0008E580
	// (set) Token: 0x0600124B RID: 4683 RVA: 0x000901B0 File Offset: 0x0008E5B0
	public static GameObject selectedObject
	{
		get
		{
			if (UICamera.mSelected && UICamera.mSelected.activeInHierarchy)
			{
				return UICamera.mSelected;
			}
			UICamera.mSelected = null;
			return null;
		}
		set
		{
			if (UICamera.mSelected == value)
			{
				UICamera.hoveredObject = value;
				UICamera.controller.current = value;
				return;
			}
			UICamera.ShowTooltip(null);
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.mInputFocus = false;
			if (UICamera.mSelected)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
			}
			UICamera.mSelected = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (value != null)
			{
				UIKeyNavigation component = value.GetComponent<UIKeyNavigation>();
				if (component != null)
				{
					UICamera.controller.current = value;
				}
			}
			if (UICamera.mSelected && flag)
			{
				UICamera uicamera2 = (!(UICamera.mSelected != null)) ? UICamera.list[0] : UICamera.FindCameraForLayer(UICamera.mSelected.layer);
				if (uicamera2 != null)
				{
					UICamera.current = uicamera2;
					UICamera.currentCamera = uicamera2.cachedCamera;
				}
			}
			if (UICamera.mSelected)
			{
				UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, true);
				}
				UICamera.Notify(UICamera.mSelected, "OnSelect", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((!(uicamera != null)) ? null : uicamera.cachedCamera);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00090388 File Offset: 0x0008E788
	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (UICamera.mMouse[i].pressed == go)
			{
				return true;
			}
		}
		int j = 0;
		int count = UICamera.activeTouches.Count;
		while (j < count)
		{
			UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[j];
			if (mouseOrTouch.pressed == go)
			{
				return true;
			}
			j++;
		}
		return UICamera.controller.pressed == go;
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x0600124D RID: 4685 RVA: 0x00090414 File Offset: 0x0008E814
	[Obsolete("Use either 'CountInputSources()' or 'activeTouches.Count'")]
	public static int touchCount
	{
		get
		{
			return UICamera.CountInputSources();
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x0009041C File Offset: 0x0008E81C
	public static int CountInputSources()
	{
		int num = 0;
		int i = 0;
		int count = UICamera.activeTouches.Count;
		while (i < count)
		{
			UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
			if (mouseOrTouch.pressed != null)
			{
				num++;
			}
			i++;
		}
		for (int j = 0; j < UICamera.mMouse.Length; j++)
		{
			if (UICamera.mMouse[j].pressed != null)
			{
				num++;
			}
		}
		if (UICamera.controller.pressed != null)
		{
			num++;
		}
		return num;
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x0600124F RID: 4687 RVA: 0x000904BC File Offset: 0x0008E8BC
	public static int dragCount
	{
		get
		{
			int num = 0;
			int i = 0;
			int count = UICamera.activeTouches.Count;
			while (i < count)
			{
				UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
				if (mouseOrTouch.dragged != null)
				{
					num++;
				}
				i++;
			}
			for (int j = 0; j < UICamera.mMouse.Length; j++)
			{
				if (UICamera.mMouse[j].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06001250 RID: 4688 RVA: 0x0009055C File Offset: 0x0008E95C
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06001251 RID: 4689 RVA: 0x00090588 File Offset: 0x0008E988
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.size; i++)
			{
				UICamera uicamera = UICamera.list.buffer[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000905EC File Offset: 0x0008E9EC
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0009062C File Offset: 0x0008EA2C
	private static Rigidbody FindRootRigidbody(Transform trans)
	{
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				break;
			}
			Rigidbody component = trans.GetComponent<Rigidbody>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00090680 File Offset: 0x0008EA80
	private static Rigidbody2D FindRootRigidbody2D(Transform trans)
	{
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				break;
			}
			Rigidbody2D component = trans.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x000906D4 File Offset: 0x0008EAD4
	public static void Raycast(UICamera.MouseOrTouch touch)
	{
		if (!UICamera.Raycast(touch.pos))
		{
			UICamera.mRayHitObject = UICamera.fallThrough;
		}
		if (UICamera.mRayHitObject == null)
		{
			UICamera.mRayHitObject = UICamera.mGenericHandler;
		}
		touch.last = touch.current;
		touch.current = UICamera.mRayHitObject;
		UICamera.mLastPos = touch.pos;
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0009073C File Offset: 0x0008EB3C
	public static bool Raycast(Vector3 inPos)
	{
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				if (UICamera.currentCamera.targetDisplay == 0)
				{
					Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
					if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y))
					{
						if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
						{
							Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
							int layerMask = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
							float num = (uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance;
							if (uicamera.eventType == UICamera.EventType.World_3D)
							{
								UICamera.lastWorldRay = ray;
								if (Physics.Raycast(ray, out UICamera.lastHit, num, layerMask, QueryTriggerInteraction.Ignore))
								{
									UICamera.lastWorldPosition = UICamera.lastHit.point;
									UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
									if (!uicamera.eventsGoToColliders)
									{
										Rigidbody componentInParent = UICamera.mRayHitObject.gameObject.GetComponentInParent<Rigidbody>();
										if (componentInParent != null)
										{
											UICamera.mRayHitObject = componentInParent.gameObject;
										}
									}
									return true;
								}
							}
							else if (uicamera.eventType == UICamera.EventType.UI_3D)
							{
								if (UICamera.mRayHits == null)
								{
									UICamera.mRayHits = new RaycastHit[50];
								}
								int num2 = Physics.RaycastNonAlloc(ray, UICamera.mRayHits, num, layerMask, QueryTriggerInteraction.Collide);
								if (num2 > 1)
								{
									int j = 0;
									while (j < num2)
									{
										GameObject gameObject = UICamera.mRayHits[j].collider.gameObject;
										UIWidget component = gameObject.GetComponent<UIWidget>();
										if (component != null)
										{
											if (component.isVisible)
											{
												if (component.hitCheck == null || component.hitCheck(UICamera.mRayHits[j].point))
												{
													goto IL_291;
												}
											}
										}
										else
										{
											UIRect uirect = NGUITools.FindInParents<UIRect>(gameObject);
											if (!(uirect != null) || uirect.finalAlpha >= 0.001f)
											{
												goto IL_291;
											}
										}
										IL_31B:
										j++;
										continue;
										IL_291:
										UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
										if (UICamera.mHit.depth != 2147483647)
										{
											UICamera.mHit.hit = UICamera.mRayHits[j];
											UICamera.mHit.point = UICamera.mRayHits[j].point;
											UICamera.mHit.go = UICamera.mRayHits[j].collider.gameObject;
											UICamera.mHits.Add(UICamera.mHit);
											goto IL_31B;
										}
										goto IL_31B;
									}
									UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
									for (int k = 0; k < UICamera.mHits.size; k++)
									{
										if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
										{
											UICamera.lastHit = UICamera.mHits[k].hit;
											UICamera.mRayHitObject = UICamera.mHits[k].go;
											UICamera.lastWorldRay = ray;
											UICamera.lastWorldPosition = UICamera.mHits[k].point;
											UICamera.mHits.Clear();
											return true;
										}
									}
									UICamera.mHits.Clear();
								}
								else if (num2 == 1)
								{
									GameObject gameObject2 = UICamera.mRayHits[0].collider.gameObject;
									UIWidget component2 = gameObject2.GetComponent<UIWidget>();
									if (component2 != null)
									{
										if (!component2.isVisible)
										{
											goto IL_851;
										}
										if (component2.hitCheck != null && !component2.hitCheck(UICamera.mRayHits[0].point))
										{
											goto IL_851;
										}
									}
									else
									{
										UIRect uirect2 = NGUITools.FindInParents<UIRect>(gameObject2);
										if (uirect2 != null && uirect2.finalAlpha < 0.001f)
										{
											goto IL_851;
										}
									}
									if (UICamera.IsVisible(UICamera.mRayHits[0].point, UICamera.mRayHits[0].collider.gameObject))
									{
										UICamera.lastHit = UICamera.mRayHits[0];
										UICamera.lastWorldRay = ray;
										UICamera.lastWorldPosition = UICamera.mRayHits[0].point;
										UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
										return true;
									}
								}
							}
							else if (uicamera.eventType == UICamera.EventType.World_2D)
							{
								if (UICamera.m2DPlane.Raycast(ray, out num))
								{
									Vector3 point = ray.GetPoint(num);
									Collider2D collider2D = Physics2D.OverlapPoint(point, layerMask);
									if (collider2D)
									{
										UICamera.lastWorldPosition = point;
										UICamera.mRayHitObject = collider2D.gameObject;
										if (!uicamera.eventsGoToColliders)
										{
											Rigidbody2D rigidbody2D = UICamera.FindRootRigidbody2D(UICamera.mRayHitObject.transform);
											if (rigidbody2D != null)
											{
												UICamera.mRayHitObject = rigidbody2D.gameObject;
											}
										}
										return true;
									}
								}
							}
							else if (uicamera.eventType == UICamera.EventType.UI_2D)
							{
								if (UICamera.m2DPlane.Raycast(ray, out num))
								{
									UICamera.lastWorldPosition = ray.GetPoint(num);
									if (UICamera.mOverlap == null)
									{
										UICamera.mOverlap = new Collider2D[50];
									}
									int num3 = Physics2D.OverlapPointNonAlloc(UICamera.lastWorldPosition, UICamera.mOverlap, layerMask);
									if (num3 > 1)
									{
										int l = 0;
										while (l < num3)
										{
											GameObject gameObject3 = UICamera.mOverlap[l].gameObject;
											UIWidget component3 = gameObject3.GetComponent<UIWidget>();
											if (component3 != null)
											{
												if (component3.isVisible)
												{
													if (component3.hitCheck == null || component3.hitCheck(UICamera.lastWorldPosition))
													{
														goto IL_6A9;
													}
												}
											}
											else
											{
												UIRect uirect3 = NGUITools.FindInParents<UIRect>(gameObject3);
												if (!(uirect3 != null) || uirect3.finalAlpha >= 0.001f)
												{
													goto IL_6A9;
												}
											}
											IL_6F8:
											l++;
											continue;
											IL_6A9:
											UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject3);
											if (UICamera.mHit.depth != 2147483647)
											{
												UICamera.mHit.go = gameObject3;
												UICamera.mHit.point = UICamera.lastWorldPosition;
												UICamera.mHits.Add(UICamera.mHit);
												goto IL_6F8;
											}
											goto IL_6F8;
										}
										UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
										for (int m = 0; m < UICamera.mHits.size; m++)
										{
											if (UICamera.IsVisible(ref UICamera.mHits.buffer[m]))
											{
												UICamera.mRayHitObject = UICamera.mHits[m].go;
												UICamera.mHits.Clear();
												return true;
											}
										}
										UICamera.mHits.Clear();
									}
									else if (num3 == 1)
									{
										GameObject gameObject4 = UICamera.mOverlap[0].gameObject;
										UIWidget component4 = gameObject4.GetComponent<UIWidget>();
										if (component4 != null)
										{
											if (!component4.isVisible)
											{
												goto IL_851;
											}
											if (component4.hitCheck != null && !component4.hitCheck(UICamera.lastWorldPosition))
											{
												goto IL_851;
											}
										}
										else
										{
											UIRect uirect4 = NGUITools.FindInParents<UIRect>(gameObject4);
											if (uirect4 != null && uirect4.finalAlpha < 0.001f)
											{
												goto IL_851;
											}
										}
										if (UICamera.IsVisible(UICamera.lastWorldPosition, gameObject4))
										{
											UICamera.mRayHitObject = gameObject4;
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
			IL_851:;
		}
		return false;
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00090FB0 File Offset: 0x0008F3B0
	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(worldPoint))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00090FEC File Offset: 0x0008F3EC
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(de.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00091031 File Offset: 0x0008F431
	public static bool IsHighlighted(GameObject go)
	{
		return UICamera.hoveredObject == go;
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00091040 File Offset: 0x0008F440
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x0009109F File Offset: 0x0008F49F
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (UICamera.GetKeyDown(up))
		{
			UICamera.currentKey = up;
			return 1;
		}
		if (UICamera.GetKeyDown(down))
		{
			UICamera.currentKey = down;
			return -1;
		}
		return 0;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000910D4 File Offset: 0x0008F4D4
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (UICamera.GetKeyDown(up0))
		{
			UICamera.currentKey = up0;
			return 1;
		}
		if (UICamera.GetKeyDown(up1))
		{
			UICamera.currentKey = up1;
			return 1;
		}
		if (UICamera.GetKeyDown(down0))
		{
			UICamera.currentKey = down0;
			return -1;
		}
		if (UICamera.GetKeyDown(down1))
		{
			UICamera.currentKey = down1;
			return -1;
		}
		return 0;
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00091144 File Offset: 0x0008F544
	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (UICamera.mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float num = UICamera.GetAxis(axis);
			if (num > 0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return 1;
			}
			if (num < -0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x000911C0 File Offset: 0x0008F5C0
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (UICamera.mNotifying > 10)
		{
			return;
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UIPopupList.isOpen && UIPopupList.current.source == go && UIPopupList.isOpen)
		{
			go = UIPopupList.current.gameObject;
		}
		if (go && go.activeInHierarchy)
		{
			UICamera.mNotifying++;
			go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			if (UICamera.mGenericHandler != null && UICamera.mGenericHandler != go)
			{
				UICamera.mGenericHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			}
			UICamera.mNotifying--;
		}
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00091280 File Offset: 0x0008F680
	private void Awake()
	{
		UICamera.mWidth = Screen.width;
		UICamera.mHeight = Screen.height;
		if (Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne)
		{
			UICamera.currentScheme = UICamera.ControlScheme.Controller;
		}
		UICamera.mMouse[0].pos = Input.mousePosition;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].lastPos = UICamera.mMouse[0].pos;
		}
		UICamera.mLastPos = UICamera.mMouse[0].pos;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs != null)
		{
			foreach (string a in commandLineArgs)
			{
				if (a == "-noMouse")
				{
					this.useMouse = false;
				}
				else if (a == "-noTouch")
				{
					this.useTouch = false;
				}
				else if (a == "-noController")
				{
					this.useController = false;
					UICamera.ignoreControllerInput = true;
				}
				else if (a == "-noJoystick")
				{
					this.useController = false;
					UICamera.ignoreControllerInput = true;
				}
				else if (a == "-useMouse")
				{
					this.useMouse = true;
				}
				else if (a == "-useTouch")
				{
					this.useTouch = true;
				}
				else if (a == "-useController")
				{
					this.useController = true;
				}
				else if (a == "-useJoystick")
				{
					this.useController = true;
				}
			}
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x00091433 File Offset: 0x0008F833
	private void OnEnable()
	{
		UICamera.list.Add(this);
		BetterList<UICamera> betterList = UICamera.list;
		if (UICamera.<>f__mg$cache0 == null)
		{
			UICamera.<>f__mg$cache0 = new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc);
		}
		betterList.Sort(UICamera.<>f__mg$cache0);
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x00091467 File Offset: 0x0008F867
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x06001262 RID: 4706 RVA: 0x00091478 File Offset: 0x0008F878
	private void Start()
	{
		BetterList<UICamera> betterList = UICamera.list;
		if (UICamera.<>f__mg$cache1 == null)
		{
			UICamera.<>f__mg$cache1 = new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc);
		}
		betterList.Sort(UICamera.<>f__mg$cache1);
		if (this.eventType != UICamera.EventType.World_3D && this.cachedCamera.transparencySortMode != TransparencySortMode.Orthographic)
		{
			this.cachedCamera.transparencySortMode = TransparencySortMode.Orthographic;
		}
		if (Application.isPlaying)
		{
			if (UICamera.fallThrough == null)
			{
				UIRoot uiroot = NGUITools.FindInParents<UIRoot>(base.gameObject);
				UICamera.fallThrough = ((!(uiroot != null)) ? base.gameObject : uiroot.gameObject);
			}
			this.cachedCamera.eventMask = 0;
			if (!UICamera.ignoreControllerInput && UICamera.disableControllerCheck && this.useController && this.handlesEvents)
			{
				UICamera.disableControllerCheck = false;
				if (!string.IsNullOrEmpty(this.horizontalAxisName) && Mathf.Abs(UICamera.GetAxis(this.horizontalAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
				}
				else if (!string.IsNullOrEmpty(this.verticalAxisName) && Mathf.Abs(UICamera.GetAxis(this.verticalAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
				}
				else if (!string.IsNullOrEmpty(this.horizontalPanAxisName) && Mathf.Abs(UICamera.GetAxis(this.horizontalPanAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
				}
				else if (!string.IsNullOrEmpty(this.verticalPanAxisName) && Mathf.Abs(UICamera.GetAxis(this.verticalPanAxisName)) > 0.1f)
				{
					UICamera.ignoreControllerInput = true;
				}
			}
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x0009163B File Offset: 0x0008FA3B
	private void Update()
	{
		if (UICamera.ignoreAllEvents)
		{
			return;
		}
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.Update)
		{
			this.ProcessEvents();
		}
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00091668 File Offset: 0x0008FA68
	private void LateUpdate()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.LateUpdate)
		{
			this.ProcessEvents();
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != UICamera.mWidth || height != UICamera.mHeight)
		{
			UICamera.mWidth = width;
			UICamera.mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (UICamera.onScreenResize != null)
			{
				UICamera.onScreenResize();
			}
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000916E0 File Offset: 0x0008FAE0
	private void ProcessEvents()
	{
		UICamera.current = this;
		NGUIDebug.debugRaycast = this.debug;
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		else if (this.useMouse)
		{
			this.ProcessMouse();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if ((this.useKeyboard || this.useController) && !UICamera.disableController && !UICamera.ignoreControllerInput)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float num = string.IsNullOrEmpty(this.scrollAxisName) ? 0f : UICamera.GetAxis(this.scrollAxisName);
			if (num != 0f)
			{
				if (UICamera.onScroll != null)
				{
					UICamera.onScroll(UICamera.mHover, num);
				}
				UICamera.Notify(UICamera.mHover, "OnScroll", num);
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.showTooltips && UICamera.mTooltipTime != 0f && !UIPopupList.isOpen && UICamera.mMouse[0].dragged == null && (UICamera.mTooltipTime < RealTime.time || UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift)))
			{
				UICamera.currentTouch = UICamera.mMouse[0];
				UICamera.currentTouchID = -1;
				UICamera.ShowTooltip(UICamera.mHover);
			}
		}
		if (UICamera.mTooltip != null && !NGUITools.GetActive(UICamera.mTooltip))
		{
			UICamera.ShowTooltip(null);
		}
		UICamera.current = null;
		UICamera.currentTouchID = -100;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000918B4 File Offset: 0x0008FCB4
	public void ProcessMouse()
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag2 = true;
				flag = true;
			}
			else if (Input.GetMouseButton(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag = true;
			}
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Touch && UICamera.activeTouches.Count > 0)
		{
			return;
		}
		UICamera.currentTouch = UICamera.mMouse[0];
		Vector2 vector = Input.mousePosition;
		if (UICamera.currentTouch.ignoreDelta == 0)
		{
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
		}
		else
		{
			UICamera.currentTouch.ignoreDelta--;
			UICamera.currentTouch.delta.x = 0f;
			UICamera.currentTouch.delta.y = 0f;
		}
		float sqrMagnitude = UICamera.currentTouch.delta.sqrMagnitude;
		UICamera.currentTouch.pos = vector;
		UICamera.mLastPos = vector;
		bool flag3 = false;
		if (UICamera.currentScheme != UICamera.ControlScheme.Mouse)
		{
			if (sqrMagnitude < 0.001f)
			{
				return;
			}
			UICamera.currentKey = KeyCode.Mouse0;
			flag3 = true;
		}
		else if (sqrMagnitude > 0.001f)
		{
			flag3 = true;
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.currentTouch.pos;
			UICamera.mMouse[j].delta = UICamera.currentTouch.delta;
		}
		if (flag || flag3 || this.mNextRaycast < RealTime.time)
		{
			this.mNextRaycast = RealTime.time + 0.02f;
			UICamera.Raycast(UICamera.currentTouch);
			for (int k = 0; k < 3; k++)
			{
				UICamera.mMouse[k].current = UICamera.currentTouch.current;
			}
		}
		bool flag4 = UICamera.currentTouch.last != UICamera.currentTouch.current;
		bool flag5 = UICamera.currentTouch.pressed != null;
		if (!flag5)
		{
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouchID = -1;
		if (flag4)
		{
			UICamera.currentKey = KeyCode.Mouse0;
		}
		if (!flag && flag3 && (!this.stickyTooltip || flag4))
		{
			if (UICamera.mTooltipTime != 0f)
			{
				UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			}
			else if (UICamera.mTooltip != null)
			{
				UICamera.ShowTooltip(null);
			}
		}
		if (flag3 && UICamera.onMouseMove != null)
		{
			UICamera.onMouseMove(UICamera.currentTouch.delta);
			UICamera.currentTouch = null;
		}
		if (flag4 && (flag2 || (flag5 && !flag)))
		{
			UICamera.hoveredObject = null;
		}
		for (int l = 0; l < 3; l++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(l);
			bool mouseButtonUp = Input.GetMouseButtonUp(l);
			if (mouseButtonDown || mouseButtonUp)
			{
				UICamera.currentKey = KeyCode.Mouse0 + l;
			}
			UICamera.currentTouch = UICamera.mMouse[l];
			UICamera.currentTouchID = -1 - l;
			UICamera.currentKey = KeyCode.Mouse0 + l;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
				UICamera.currentTouch.pressTime = RealTime.time;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
		}
		if (!flag && flag4)
		{
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouchID = -1;
			UICamera.currentKey = KeyCode.Mouse0;
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouch = null;
		UICamera.mMouse[0].last = UICamera.mMouse[0].current;
		for (int m = 1; m < 3; m++)
		{
			UICamera.mMouse[m].last = UICamera.mMouse[0].last;
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x00091D08 File Offset: 0x00090108
	public void ProcessTouches()
	{
		int num = (UICamera.GetInputTouchCount != null) ? UICamera.GetInputTouchCount() : Input.touchCount;
		for (int i = 0; i < num; i++)
		{
			TouchPhase phase;
			int fingerId;
			Vector2 position;
			int tapCount;
			if (UICamera.GetInputTouch == null)
			{
				UnityEngine.Touch touch = Input.GetTouch(i);
				phase = touch.phase;
				fingerId = touch.fingerId;
				position = touch.position;
				tapCount = touch.tapCount;
			}
			else
			{
				UICamera.Touch touch2 = UICamera.GetInputTouch(i);
				phase = touch2.phase;
				fingerId = touch2.fingerId;
				position = touch2.position;
				tapCount = touch2.tapCount;
			}
			UICamera.currentTouchID = ((!this.allowMultiTouch) ? 1 : fingerId);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID, true);
			bool flag = phase == TouchPhase.Began || UICamera.currentTouch.touchBegan;
			bool flag2 = phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
			UICamera.currentTouch.delta = position - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = position;
			UICamera.currentKey = KeyCode.None;
			UICamera.Raycast(UICamera.currentTouch);
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (tapCount > 1)
			{
				UICamera.currentTouch.clickTime = RealTime.time;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.touchBegan = false;
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
		if (num == 0)
		{
			if (UICamera.mUsingTouchEvents)
			{
				UICamera.mUsingTouchEvents = false;
				return;
			}
			if (this.useMouse)
			{
				this.ProcessMouse();
			}
		}
		else
		{
			UICamera.mUsingTouchEvents = true;
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x00091F0C File Offset: 0x0009030C
	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			UICamera.currentTouchID = 1;
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.currentTouch.touchBegan = mouseButtonDown;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressTime = RealTime.time;
				UICamera.activeTouches.Add(UICamera.currentTouch);
			}
			Vector2 vector = Input.mousePosition;
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = vector;
			UICamera.Raycast(UICamera.currentTouch);
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			UICamera.currentKey = KeyCode.None;
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				UICamera.activeTouches.Remove(UICamera.currentTouch);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x00092030 File Offset: 0x00090430
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.controller;
		bool flag = false;
		bool flag2 = false;
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyDown(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyDown(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyDown(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyUp(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyUp(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag2 = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyUp(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		if (flag)
		{
			UICamera.currentTouch.pressTime = RealTime.time;
		}
		if ((flag || flag2) && UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.last = UICamera.currentTouch.current;
		}
		KeyCode keyCode = KeyCode.None;
		if (this.useController && !UICamera.ignoreControllerInput)
		{
			if (!UICamera.disableController && UICamera.currentScheme == UICamera.ControlScheme.Controller && (UICamera.currentTouch.current == null || !UICamera.currentTouch.current.activeInHierarchy))
			{
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			}
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				int direction = UICamera.GetDirection(this.verticalAxisName);
				if (direction != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				int direction2 = UICamera.GetDirection(this.horizontalAxisName);
				if (direction2 != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			float num = string.IsNullOrEmpty(this.horizontalPanAxisName) ? 0f : UICamera.GetAxis(this.horizontalPanAxisName);
			float num2 = string.IsNullOrEmpty(this.verticalPanAxisName) ? 0f : UICamera.GetAxis(this.verticalPanAxisName);
			if (num != 0f || num2 != 0f)
			{
				UICamera.ShowTooltip(null);
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
				if (UICamera.currentTouch.current != null)
				{
					Vector2 vector = new Vector2(num, num2);
					vector *= Time.unscaledDeltaTime;
					if (UICamera.onPan != null)
					{
						UICamera.onPan(UICamera.currentTouch.current, vector);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnPan", vector);
				}
			}
		}
		if ((UICamera.GetAnyKeyDown == null) ? Input.anyKeyDown : UICamera.GetAnyKeyDown())
		{
			int i = 0;
			int num3 = NGUITools.keys.Length;
			while (i < num3)
			{
				KeyCode keyCode2 = NGUITools.keys[i];
				if (keyCode != keyCode2)
				{
					if (UICamera.GetKeyDown(keyCode2))
					{
						if (this.useKeyboard || keyCode2 >= KeyCode.Mouse0)
						{
							if ((this.useController && !UICamera.ignoreControllerInput) || keyCode2 < KeyCode.JoystickButton0)
							{
								if (this.useMouse || keyCode2 < KeyCode.Mouse0 || keyCode2 > KeyCode.Mouse6)
								{
									UICamera.currentKey = keyCode2;
									if (UICamera.onKey != null)
									{
										UICamera.onKey(UICamera.currentTouch.current, keyCode2);
									}
									UICamera.Notify(UICamera.currentTouch.current, "OnKey", keyCode2);
								}
							}
						}
					}
				}
				i++;
			}
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000925AC File Offset: 0x000909AC
	private void ProcessPress(bool pressed, float click, float drag)
	{
		if (pressed)
		{
			if (UICamera.mTooltip != null)
			{
				UICamera.ShowTooltip(null);
			}
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouch.pressStarted = true;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == null && UICamera.currentTouch.current != null)
			{
				UICamera.hoveredObject = UICamera.currentTouch.current;
			}
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, true);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.mSelected != UICamera.currentTouch.pressed)
			{
				UICamera.mInputFocus = false;
				if (UICamera.mSelected)
				{
					UICamera.Notify(UICamera.mSelected, "OnSelect", false);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, false);
					}
				}
				UICamera.mSelected = UICamera.currentTouch.pressed;
				if (UICamera.currentTouch.pressed != null)
				{
					UIKeyNavigation component = UICamera.currentTouch.pressed.GetComponent<UIKeyNavigation>();
					if (component != null)
					{
						UICamera.controller.current = UICamera.currentTouch.pressed;
					}
				}
				if (UICamera.mSelected)
				{
					UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, true);
					}
					UICamera.Notify(UICamera.mSelected, "OnSelect", true);
				}
			}
		}
		else if (UICamera.currentTouch.pressed != null && (UICamera.currentTouch.delta.sqrMagnitude != 0f || UICamera.currentTouch.current != UICamera.currentTouch.last))
		{
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			float sqrMagnitude = UICamera.currentTouch.totalDelta.sqrMagnitude;
			bool flag = false;
			if (!UICamera.currentTouch.dragStarted && UICamera.currentTouch.last != UICamera.currentTouch.current)
			{
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
				UICamera.isDragging = true;
				if (UICamera.onDragStart != null)
				{
					UICamera.onDragStart(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
				if (UICamera.onDragOver != null)
				{
					UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", UICamera.currentTouch.dragged);
				UICamera.isDragging = false;
			}
			else if (!UICamera.currentTouch.dragStarted && drag < sqrMagnitude)
			{
				flag = true;
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
			}
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.mTooltip != null)
				{
					UICamera.ShowTooltip(null);
				}
				UICamera.isDragging = true;
				bool flag2 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
				if (flag)
				{
					if (UICamera.onDragStart != null)
					{
						UICamera.onDragStart(UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				else if (UICamera.currentTouch.last != UICamera.currentTouch.current)
				{
					if (UICamera.onDragOut != null)
					{
						UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				if (UICamera.onDrag != null)
				{
					UICamera.onDrag(UICamera.currentTouch.dragged, UICamera.currentTouch.delta);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
				UICamera.currentTouch.last = UICamera.currentTouch.current;
				UICamera.isDragging = false;
				if (flag2)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
				else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && click < sqrMagnitude)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x00092BC4 File Offset: 0x00090FC4
	private void ProcessRelease(bool isMouse, float drag)
	{
		if (UICamera.currentTouch == null)
		{
			return;
		}
		UICamera.currentTouch.pressStarted = false;
		if (UICamera.currentTouch.pressed != null)
		{
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDragOut != null)
				{
					UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
				if (UICamera.onDragEnd != null)
				{
					UICamera.onDragEnd(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", null);
			}
			if (UICamera.onPress != null)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (isMouse)
			{
				bool flag = this.HasCollider(UICamera.currentTouch.pressed);
				if (flag)
				{
					if (UICamera.mHover == UICamera.currentTouch.current)
					{
						if (UICamera.onHover != null)
						{
							UICamera.onHover(UICamera.currentTouch.current, true);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnHover", true);
					}
					else
					{
						UICamera.hoveredObject = UICamera.currentTouch.current;
					}
				}
			}
			if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.sqrMagnitude < drag))
			{
				if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.pressed == UICamera.currentTouch.current)
				{
					UICamera.ShowTooltip(null);
					float time = RealTime.time;
					if (UICamera.onClick != null)
					{
						UICamera.onClick(UICamera.currentTouch.pressed);
					}
					UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
					if (UICamera.currentTouch.clickTime + 0.35f > time)
					{
						if (UICamera.onDoubleClick != null)
						{
							UICamera.onDoubleClick(UICamera.currentTouch.pressed);
						}
						UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
					}
					UICamera.currentTouch.clickTime = time;
				}
			}
			else if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDrop != null)
				{
					UICamera.onDrop(UICamera.currentTouch.current, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
			}
		}
		UICamera.currentTouch.dragStarted = false;
		UICamera.currentTouch.pressed = null;
		UICamera.currentTouch.dragged = null;
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x00092ECC File Offset: 0x000912CC
	private bool HasCollider(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		Collider component = go.GetComponent<Collider>();
		if (component != null)
		{
			return component.enabled;
		}
		Collider2D component2 = go.GetComponent<Collider2D>();
		return component2 != null && component2.enabled;
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x00092F20 File Offset: 0x00091320
	public void ProcessTouch(bool pressed, bool released)
	{
		if (released)
		{
			UICamera.mTooltipTime = 0f;
		}
		bool flag = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
		float num = (!flag) ? this.touchDragThreshold : this.mouseDragThreshold;
		float num2 = (!flag) ? this.touchClickThreshold : this.mouseClickThreshold;
		num *= num;
		num2 *= num2;
		if (UICamera.currentTouch.pressed != null)
		{
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
			this.ProcessPress(pressed, num2, num);
			if (this.tooltipDelay != 0f && UICamera.currentTouch.deltaTime > this.tooltipDelay && UICamera.currentTouch.pressed == UICamera.currentTouch.current && UICamera.mTooltipTime != 0f && !UICamera.currentTouch.dragStarted)
			{
				UICamera.mTooltipTime = 0f;
				UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				if (this.longPressTooltip)
				{
					UICamera.ShowTooltip(UICamera.currentTouch.pressed);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnLongPress", null);
			}
		}
		else if (flag || pressed || released)
		{
			this.ProcessPress(pressed, num2, num);
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x0009307E File Offset: 0x0009147E
	public static void CancelNextTooltip()
	{
		UICamera.mTooltipTime = 0f;
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x0009308C File Offset: 0x0009148C
	public static bool ShowTooltip(GameObject go)
	{
		if (UICamera.mTooltip != go)
		{
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, false);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", false);
			}
			UICamera.mTooltip = go;
			UICamera.mTooltipTime = 0f;
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, true);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", true);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x0009313A File Offset: 0x0009153A
	public static bool HideTooltip()
	{
		return UICamera.ShowTooltip(null);
	}

	// Token: 0x04000F9B RID: 3995
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x04000F9C RID: 3996
	public static UICamera.GetKeyStateFunc GetKeyDown = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyDown(key);

	// Token: 0x04000F9D RID: 3997
	public static UICamera.GetKeyStateFunc GetKeyUp = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyUp(key);

	// Token: 0x04000F9E RID: 3998
	public static UICamera.GetKeyStateFunc GetKey = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKey(key);

	// Token: 0x04000F9F RID: 3999
	public static UICamera.GetAxisFunc GetAxis = delegate(string axis)
	{
		if (UICamera.ignoreControllerInput)
		{
			return 0f;
		}
		return Input.GetAxis(axis);
	};

	// Token: 0x04000FA0 RID: 4000
	public static UICamera.GetAnyKeyFunc GetAnyKeyDown;

	// Token: 0x04000FA1 RID: 4001
	public static UICamera.GetMouseDelegate GetMouse = (int button) => UICamera.mMouse[button];

	// Token: 0x04000FA2 RID: 4002
	public static UICamera.GetTouchDelegate GetTouch = delegate(int id, bool createIfMissing)
	{
		if (id < 0)
		{
			return UICamera.GetMouse(-id - 1);
		}
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				return UICamera.activeTouches[i];
			}
			i++;
		}
		if (createIfMissing)
		{
			UICamera.MouseOrTouch mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.pressTime = RealTime.time;
			mouseOrTouch.touchBegan = true;
			UICamera.activeTouches.Add(mouseOrTouch);
			UICamera.mTouchIDs.Add(id);
			return mouseOrTouch;
		}
		return null;
	};

	// Token: 0x04000FA3 RID: 4003
	public static UICamera.RemoveTouchDelegate RemoveTouch = delegate(int id)
	{
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				UICamera.mTouchIDs.RemoveAt(i);
				UICamera.activeTouches.RemoveAt(i);
				return;
			}
			i++;
		}
	};

	// Token: 0x04000FA4 RID: 4004
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x04000FA5 RID: 4005
	public UICamera.EventType eventType = UICamera.EventType.UI_3D;

	// Token: 0x04000FA6 RID: 4006
	public bool eventsGoToColliders;

	// Token: 0x04000FA7 RID: 4007
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04000FA8 RID: 4008
	public UICamera.ProcessEventsIn processEventsIn;

	// Token: 0x04000FA9 RID: 4009
	public bool debug;

	// Token: 0x04000FAA RID: 4010
	public bool useMouse = true;

	// Token: 0x04000FAB RID: 4011
	public bool useTouch = true;

	// Token: 0x04000FAC RID: 4012
	public bool allowMultiTouch = true;

	// Token: 0x04000FAD RID: 4013
	public bool useKeyboard = true;

	// Token: 0x04000FAE RID: 4014
	public bool useController = true;

	// Token: 0x04000FAF RID: 4015
	public bool stickyTooltip = true;

	// Token: 0x04000FB0 RID: 4016
	public float tooltipDelay = 1f;

	// Token: 0x04000FB1 RID: 4017
	public bool longPressTooltip;

	// Token: 0x04000FB2 RID: 4018
	public float mouseDragThreshold = 4f;

	// Token: 0x04000FB3 RID: 4019
	public float mouseClickThreshold = 10f;

	// Token: 0x04000FB4 RID: 4020
	public float touchDragThreshold = 40f;

	// Token: 0x04000FB5 RID: 4021
	public float touchClickThreshold = 40f;

	// Token: 0x04000FB6 RID: 4022
	public float rangeDistance = -1f;

	// Token: 0x04000FB7 RID: 4023
	public string horizontalAxisName = "Horizontal";

	// Token: 0x04000FB8 RID: 4024
	public string verticalAxisName = "Vertical";

	// Token: 0x04000FB9 RID: 4025
	public string horizontalPanAxisName;

	// Token: 0x04000FBA RID: 4026
	public string verticalPanAxisName;

	// Token: 0x04000FBB RID: 4027
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x04000FBC RID: 4028
	[Tooltip("If enabled, command-click will result in a right-click event on OSX")]
	public bool commandClick = true;

	// Token: 0x04000FBD RID: 4029
	public KeyCode submitKey0 = KeyCode.Return;

	// Token: 0x04000FBE RID: 4030
	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	// Token: 0x04000FBF RID: 4031
	public KeyCode cancelKey0 = KeyCode.Escape;

	// Token: 0x04000FC0 RID: 4032
	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	// Token: 0x04000FC1 RID: 4033
	public bool autoHideCursor = true;

	// Token: 0x04000FC2 RID: 4034
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x04000FC3 RID: 4035
	public static bool showTooltips = true;

	// Token: 0x04000FC4 RID: 4036
	public static bool ignoreAllEvents = false;

	// Token: 0x04000FC5 RID: 4037
	public static bool ignoreControllerInput = false;

	// Token: 0x04000FC6 RID: 4038
	private static bool mDisableController = false;

	// Token: 0x04000FC7 RID: 4039
	private static Vector2 mLastPos = Vector2.zero;

	// Token: 0x04000FC8 RID: 4040
	public static Vector3 lastWorldPosition = Vector3.zero;

	// Token: 0x04000FC9 RID: 4041
	public static Ray lastWorldRay = default(Ray);

	// Token: 0x04000FCA RID: 4042
	public static RaycastHit lastHit;

	// Token: 0x04000FCB RID: 4043
	public static UICamera current = null;

	// Token: 0x04000FCC RID: 4044
	public static Camera currentCamera = null;

	// Token: 0x04000FCD RID: 4045
	public static UICamera.OnSchemeChange onSchemeChange;

	// Token: 0x04000FCE RID: 4046
	private static UICamera.ControlScheme mLastScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x04000FCF RID: 4047
	public static int currentTouchID = -100;

	// Token: 0x04000FD0 RID: 4048
	private static KeyCode mCurrentKey = KeyCode.Alpha0;

	// Token: 0x04000FD1 RID: 4049
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x04000FD2 RID: 4050
	private static bool mInputFocus = false;

	// Token: 0x04000FD3 RID: 4051
	private static GameObject mGenericHandler;

	// Token: 0x04000FD4 RID: 4052
	public static GameObject fallThrough;

	// Token: 0x04000FD5 RID: 4053
	public static UICamera.VoidDelegate onClick;

	// Token: 0x04000FD6 RID: 4054
	public static UICamera.VoidDelegate onDoubleClick;

	// Token: 0x04000FD7 RID: 4055
	public static UICamera.BoolDelegate onHover;

	// Token: 0x04000FD8 RID: 4056
	public static UICamera.BoolDelegate onPress;

	// Token: 0x04000FD9 RID: 4057
	public static UICamera.BoolDelegate onSelect;

	// Token: 0x04000FDA RID: 4058
	public static UICamera.FloatDelegate onScroll;

	// Token: 0x04000FDB RID: 4059
	public static UICamera.VectorDelegate onDrag;

	// Token: 0x04000FDC RID: 4060
	public static UICamera.VoidDelegate onDragStart;

	// Token: 0x04000FDD RID: 4061
	public static UICamera.ObjectDelegate onDragOver;

	// Token: 0x04000FDE RID: 4062
	public static UICamera.ObjectDelegate onDragOut;

	// Token: 0x04000FDF RID: 4063
	public static UICamera.VoidDelegate onDragEnd;

	// Token: 0x04000FE0 RID: 4064
	public static UICamera.ObjectDelegate onDrop;

	// Token: 0x04000FE1 RID: 4065
	public static UICamera.KeyCodeDelegate onKey;

	// Token: 0x04000FE2 RID: 4066
	public static UICamera.KeyCodeDelegate onNavigate;

	// Token: 0x04000FE3 RID: 4067
	public static UICamera.VectorDelegate onPan;

	// Token: 0x04000FE4 RID: 4068
	public static UICamera.BoolDelegate onTooltip;

	// Token: 0x04000FE5 RID: 4069
	public static UICamera.MoveDelegate onMouseMove;

	// Token: 0x04000FE6 RID: 4070
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x04000FE7 RID: 4071
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x04000FE8 RID: 4072
	public static List<UICamera.MouseOrTouch> activeTouches = new List<UICamera.MouseOrTouch>();

	// Token: 0x04000FE9 RID: 4073
	private static List<int> mTouchIDs = new List<int>();

	// Token: 0x04000FEA RID: 4074
	private static int mWidth = 0;

	// Token: 0x04000FEB RID: 4075
	private static int mHeight = 0;

	// Token: 0x04000FEC RID: 4076
	private static GameObject mTooltip = null;

	// Token: 0x04000FED RID: 4077
	private Camera mCam;

	// Token: 0x04000FEE RID: 4078
	private static float mTooltipTime = 0f;

	// Token: 0x04000FEF RID: 4079
	private float mNextRaycast;

	// Token: 0x04000FF0 RID: 4080
	public static bool isDragging = false;

	// Token: 0x04000FF1 RID: 4081
	private static int mLastInteractionCheck = -1;

	// Token: 0x04000FF2 RID: 4082
	private static bool mLastInteractionResult = false;

	// Token: 0x04000FF3 RID: 4083
	private static int mLastFocusCheck = -1;

	// Token: 0x04000FF4 RID: 4084
	private static bool mLastFocusResult = false;

	// Token: 0x04000FF5 RID: 4085
	private static int mLastOverCheck = -1;

	// Token: 0x04000FF6 RID: 4086
	private static bool mLastOverResult = false;

	// Token: 0x04000FF7 RID: 4087
	private static GameObject mRayHitObject;

	// Token: 0x04000FF8 RID: 4088
	private static GameObject mHover;

	// Token: 0x04000FF9 RID: 4089
	private static GameObject mSelected;

	// Token: 0x04000FFA RID: 4090
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x04000FFB RID: 4091
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x04000FFC RID: 4092
	private static RaycastHit[] mRayHits;

	// Token: 0x04000FFD RID: 4093
	private static Collider2D[] mOverlap;

	// Token: 0x04000FFE RID: 4094
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x04000FFF RID: 4095
	private static float mNextEvent = 0f;

	// Token: 0x04001000 RID: 4096
	private static int mNotifying = 0;

	// Token: 0x04001001 RID: 4097
	private static bool disableControllerCheck = true;

	// Token: 0x04001002 RID: 4098
	private static bool mUsingTouchEvents = true;

	// Token: 0x04001003 RID: 4099
	public static UICamera.GetTouchCountCallback GetInputTouchCount;

	// Token: 0x04001004 RID: 4100
	public static UICamera.GetTouchCallback GetInputTouch;

	// Token: 0x04001007 RID: 4103
	[CompilerGenerated]
	private static BetterList<UICamera>.CompareFunc <>f__mg$cache0;

	// Token: 0x04001008 RID: 4104
	[CompilerGenerated]
	private static BetterList<UICamera>.CompareFunc <>f__mg$cache1;

	// Token: 0x0200024C RID: 588
	public enum ControlScheme
	{
		// Token: 0x0400100A RID: 4106
		Mouse,
		// Token: 0x0400100B RID: 4107
		Touch,
		// Token: 0x0400100C RID: 4108
		Controller
	}

	// Token: 0x0200024D RID: 589
	public enum ClickNotification
	{
		// Token: 0x0400100E RID: 4110
		None,
		// Token: 0x0400100F RID: 4111
		Always,
		// Token: 0x04001010 RID: 4112
		BasedOnDelta
	}

	// Token: 0x0200024E RID: 590
	public class MouseOrTouch
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000934AC File Offset: 0x000918AC
		public float deltaTime
		{
			get
			{
				return RealTime.time - this.pressTime;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000934BA File Offset: 0x000918BA
		public bool isOverUI
		{
			get
			{
				return this.current != null && this.current != UICamera.fallThrough && NGUITools.FindInParents<UIRoot>(this.current) != null;
			}
		}

		// Token: 0x04001011 RID: 4113
		public KeyCode key;

		// Token: 0x04001012 RID: 4114
		public Vector2 pos;

		// Token: 0x04001013 RID: 4115
		public Vector2 lastPos;

		// Token: 0x04001014 RID: 4116
		public Vector2 delta;

		// Token: 0x04001015 RID: 4117
		public Vector2 totalDelta;

		// Token: 0x04001016 RID: 4118
		public Camera pressedCam;

		// Token: 0x04001017 RID: 4119
		public GameObject last;

		// Token: 0x04001018 RID: 4120
		public GameObject current;

		// Token: 0x04001019 RID: 4121
		public GameObject pressed;

		// Token: 0x0400101A RID: 4122
		public GameObject dragged;

		// Token: 0x0400101B RID: 4123
		public float pressTime;

		// Token: 0x0400101C RID: 4124
		public float clickTime;

		// Token: 0x0400101D RID: 4125
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x0400101E RID: 4126
		public bool touchBegan = true;

		// Token: 0x0400101F RID: 4127
		public bool pressStarted;

		// Token: 0x04001020 RID: 4128
		public bool dragStarted;

		// Token: 0x04001021 RID: 4129
		public int ignoreDelta;
	}

	// Token: 0x0200024F RID: 591
	public enum EventType
	{
		// Token: 0x04001023 RID: 4131
		World_3D,
		// Token: 0x04001024 RID: 4132
		UI_3D,
		// Token: 0x04001025 RID: 4133
		World_2D,
		// Token: 0x04001026 RID: 4134
		UI_2D
	}

	// Token: 0x02000250 RID: 592
	// (Invoke) Token: 0x0600127F RID: 4735
	public delegate bool GetKeyStateFunc(KeyCode key);

	// Token: 0x02000251 RID: 593
	// (Invoke) Token: 0x06001283 RID: 4739
	public delegate float GetAxisFunc(string name);

	// Token: 0x02000252 RID: 594
	// (Invoke) Token: 0x06001287 RID: 4743
	public delegate bool GetAnyKeyFunc();

	// Token: 0x02000253 RID: 595
	// (Invoke) Token: 0x0600128B RID: 4747
	public delegate UICamera.MouseOrTouch GetMouseDelegate(int button);

	// Token: 0x02000254 RID: 596
	// (Invoke) Token: 0x0600128F RID: 4751
	public delegate UICamera.MouseOrTouch GetTouchDelegate(int id, bool createIfMissing);

	// Token: 0x02000255 RID: 597
	// (Invoke) Token: 0x06001293 RID: 4755
	public delegate void RemoveTouchDelegate(int id);

	// Token: 0x02000256 RID: 598
	// (Invoke) Token: 0x06001297 RID: 4759
	public delegate void OnScreenResize();

	// Token: 0x02000257 RID: 599
	public enum ProcessEventsIn
	{
		// Token: 0x04001028 RID: 4136
		Update,
		// Token: 0x04001029 RID: 4137
		LateUpdate
	}

	// Token: 0x02000258 RID: 600
	// (Invoke) Token: 0x0600129B RID: 4763
	public delegate void OnCustomInput();

	// Token: 0x02000259 RID: 601
	// (Invoke) Token: 0x0600129F RID: 4767
	public delegate void OnSchemeChange();

	// Token: 0x0200025A RID: 602
	// (Invoke) Token: 0x060012A3 RID: 4771
	public delegate void MoveDelegate(Vector2 delta);

	// Token: 0x0200025B RID: 603
	// (Invoke) Token: 0x060012A7 RID: 4775
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x0200025C RID: 604
	// (Invoke) Token: 0x060012AB RID: 4779
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x0200025D RID: 605
	// (Invoke) Token: 0x060012AF RID: 4783
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x0200025E RID: 606
	// (Invoke) Token: 0x060012B3 RID: 4787
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x0200025F RID: 607
	// (Invoke) Token: 0x060012B7 RID: 4791
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x02000260 RID: 608
	// (Invoke) Token: 0x060012BB RID: 4795
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	// Token: 0x02000261 RID: 609
	private struct DepthEntry
	{
		// Token: 0x0400102A RID: 4138
		public int depth;

		// Token: 0x0400102B RID: 4139
		public RaycastHit hit;

		// Token: 0x0400102C RID: 4140
		public Vector3 point;

		// Token: 0x0400102D RID: 4141
		public GameObject go;
	}

	// Token: 0x02000262 RID: 610
	public class Touch
	{
		// Token: 0x0400102E RID: 4142
		public int fingerId;

		// Token: 0x0400102F RID: 4143
		public TouchPhase phase;

		// Token: 0x04001030 RID: 4144
		public Vector2 position;

		// Token: 0x04001031 RID: 4145
		public int tapCount;
	}

	// Token: 0x02000263 RID: 611
	// (Invoke) Token: 0x060012C0 RID: 4800
	public delegate int GetTouchCountCallback();

	// Token: 0x02000264 RID: 612
	// (Invoke) Token: 0x060012C4 RID: 4804
	public delegate UICamera.Touch GetTouchCallback(int index);
}
