using System;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class HomeCursorScript : MonoBehaviour
{
	// Token: 0x06001C98 RID: 7320 RVA: 0x00102F88 File Offset: 0x00101388
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == this.Photograph)
		{
			this.PhotographNull();
		}
		if (other.gameObject == this.Tack)
		{
			this.CircleHighlight.position = new Vector3(this.CircleHighlight.position.x, 100f, this.Highlight.position.z);
			this.Tack = null;
			this.PhotoGallery.UpdateButtonPrompts();
		}
	}

	// Token: 0x06001C99 RID: 7321 RVA: 0x00103014 File Offset: 0x00101414
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 16)
		{
			if (this.Tack == null)
			{
				this.Photograph = other.gameObject;
				this.Highlight.localEulerAngles = this.Photograph.transform.localEulerAngles;
				this.Highlight.localPosition = this.Photograph.transform.localPosition;
				this.Highlight.localScale = new Vector3(this.Photograph.transform.localScale.x * 1.12f, this.Photograph.transform.localScale.y * 1.2f, 1f);
				this.PhotoGallery.UpdateButtonPrompts();
			}
		}
		else if (other.gameObject.name != "SouthWall")
		{
			this.Tack = other.gameObject;
			this.CircleHighlight.position = this.Tack.transform.position;
			this.PhotoGallery.UpdateButtonPrompts();
			this.PhotographNull();
		}
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x0010313C File Offset: 0x0010153C
	private void PhotographNull()
	{
		this.Highlight.position = new Vector3(this.Highlight.position.x, 100f, this.Highlight.position.z);
		this.Photograph = null;
		this.PhotoGallery.UpdateButtonPrompts();
	}

	// Token: 0x04002185 RID: 8581
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x04002186 RID: 8582
	public GameObject Photograph;

	// Token: 0x04002187 RID: 8583
	public Transform Highlight;

	// Token: 0x04002188 RID: 8584
	public GameObject Tack;

	// Token: 0x04002189 RID: 8585
	public Transform CircleHighlight;
}
