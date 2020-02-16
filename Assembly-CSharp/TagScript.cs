using System;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class TagScript : MonoBehaviour
{
	// Token: 0x0600214F RID: 8527 RVA: 0x0018C155 File Offset: 0x0018A555
	private void Start()
	{
		this.Sprite.color = new Color(1f, 0f, 0f, 0f);
		this.MainCameraCamera = this.MainCamera.GetComponent<Camera>();
	}

	// Token: 0x06002150 RID: 8528 RVA: 0x0018C18C File Offset: 0x0018A58C
	private void Update()
	{
		if (this.Target != null)
		{
			float num = Vector3.Angle(this.MainCamera.forward, this.MainCamera.position - this.Target.position);
			if (num > 90f)
			{
				Vector2 vector = this.MainCameraCamera.WorldToScreenPoint(this.Target.position);
				base.transform.position = this.UICamera.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 1f));
			}
		}
	}

	// Token: 0x04003560 RID: 13664
	public UISprite Sprite;

	// Token: 0x04003561 RID: 13665
	public Camera UICamera;

	// Token: 0x04003562 RID: 13666
	public Camera MainCameraCamera;

	// Token: 0x04003563 RID: 13667
	public Transform MainCamera;

	// Token: 0x04003564 RID: 13668
	public Transform Target;
}
