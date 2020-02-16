using System;
using UnityEngine;

// Token: 0x020001BD RID: 445
[AddComponentMenu("NGUI/Interaction/Drag-Resize Widget")]
public class UIDragResize : MonoBehaviour
{
	// Token: 0x06000D2E RID: 3374 RVA: 0x0006C9F4 File Offset: 0x0006ADF4
	private void OnDragStart()
	{
		if (this.target != null)
		{
			Vector3[] worldCorners = this.target.worldCorners;
			this.mPlane = new Plane(worldCorners[0], worldCorners[1], worldCorners[3]);
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				this.mRayPos = currentRay.GetPoint(distance);
				this.mLocalPos = this.target.cachedTransform.localPosition;
				this.mWidth = this.target.width;
				this.mHeight = this.target.height;
				this.mDragging = true;
			}
		}
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0006CAB4 File Offset: 0x0006AEB4
	private void OnDrag(Vector2 delta)
	{
		if (this.mDragging && this.target != null)
		{
			Ray currentRay = UICamera.currentRay;
			float distance;
			if (this.mPlane.Raycast(currentRay, out distance))
			{
				Transform cachedTransform = this.target.cachedTransform;
				cachedTransform.localPosition = this.mLocalPos;
				this.target.width = this.mWidth;
				this.target.height = this.mHeight;
				Vector3 b = currentRay.GetPoint(distance) - this.mRayPos;
				cachedTransform.position += b;
				Vector3 vector = Quaternion.Inverse(cachedTransform.localRotation) * (cachedTransform.localPosition - this.mLocalPos);
				cachedTransform.localPosition = this.mLocalPos;
				NGUIMath.ResizeWidget(this.target, this.pivot, vector.x, vector.y, this.minWidth, this.minHeight, this.maxWidth, this.maxHeight);
				if (this.updateAnchors)
				{
					this.target.BroadcastMessage("UpdateAnchors");
				}
			}
		}
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0006CBD7 File Offset: 0x0006AFD7
	private void OnDragEnd()
	{
		this.mDragging = false;
	}

	// Token: 0x04000BD4 RID: 3028
	public UIWidget target;

	// Token: 0x04000BD5 RID: 3029
	public UIWidget.Pivot pivot = UIWidget.Pivot.BottomRight;

	// Token: 0x04000BD6 RID: 3030
	public int minWidth = 100;

	// Token: 0x04000BD7 RID: 3031
	public int minHeight = 100;

	// Token: 0x04000BD8 RID: 3032
	public int maxWidth = 100000;

	// Token: 0x04000BD9 RID: 3033
	public int maxHeight = 100000;

	// Token: 0x04000BDA RID: 3034
	public bool updateAnchors;

	// Token: 0x04000BDB RID: 3035
	private Plane mPlane;

	// Token: 0x04000BDC RID: 3036
	private Vector3 mRayPos;

	// Token: 0x04000BDD RID: 3037
	private Vector3 mLocalPos;

	// Token: 0x04000BDE RID: 3038
	private int mWidth;

	// Token: 0x04000BDF RID: 3039
	private int mHeight;

	// Token: 0x04000BE0 RID: 3040
	private bool mDragging;
}
