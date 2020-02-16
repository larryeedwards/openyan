using System;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class DetectClickScript : MonoBehaviour
{
	// Token: 0x060018C9 RID: 6345 RVA: 0x000DFF8C File Offset: 0x000DE38C
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		this.OriginalColor = this.Sprite.color;
	}

	// Token: 0x060018CA RID: 6346 RVA: 0x000DFFB0 File Offset: 0x000DE3B0
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = this.GUICamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 100f) && raycastHit.collider == this.MyCollider && this.Label.color.a == 1f)
			{
				this.Sprite.color = new Color(1f, 1f, 1f, 1f);
				this.Clicked = true;
			}
		}
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x000E004C File Offset: 0x000DE44C
	private void OnTriggerEnter()
	{
		if (this.Label.color.a == 1f)
		{
			this.Sprite.color = Color.white;
		}
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x000E0086 File Offset: 0x000DE486
	private void OnTriggerExit()
	{
		this.Sprite.color = this.OriginalColor;
	}

	// Token: 0x04001C59 RID: 7257
	public Vector3 OriginalPosition;

	// Token: 0x04001C5A RID: 7258
	public Color OriginalColor;

	// Token: 0x04001C5B RID: 7259
	public Collider MyCollider;

	// Token: 0x04001C5C RID: 7260
	public Camera GUICamera;

	// Token: 0x04001C5D RID: 7261
	public UISprite Sprite;

	// Token: 0x04001C5E RID: 7262
	public UILabel Label;

	// Token: 0x04001C5F RID: 7263
	public bool Clicked;
}
