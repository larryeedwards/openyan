using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class AboutScript : MonoBehaviour
{
	// Token: 0x060016DD RID: 5853 RVA: 0x000AF918 File Offset: 0x000ADD18
	private void Start()
	{
		foreach (Transform transform in this.Labels)
		{
			Vector3 localPosition = transform.localPosition;
			localPosition.x = 2000f;
			transform.localPosition = localPosition;
		}
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x000AF960 File Offset: 0x000ADD60
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			if (this.SlideID < this.Labels.Length)
			{
				this.SlideIn[this.SlideID] = true;
			}
			this.SlideID++;
		}
		if (this.SlideID < this.Labels.Length + 1)
		{
			this.ID = 0;
			while (this.ID < this.Labels.Length)
			{
				if (this.SlideIn[this.ID])
				{
					Transform transform = this.Labels[this.ID];
					Vector3 localPosition = transform.localPosition;
					localPosition.x = Mathf.Lerp(localPosition.x, 0f, Time.deltaTime);
					transform.localPosition = localPosition;
				}
				this.ID++;
			}
		}
		else
		{
			this.Timer += Time.deltaTime * 10f;
			this.ID = 0;
			while (this.ID < this.Labels.Length)
			{
				if (this.Timer > (float)this.ID)
				{
					this.SlideOut[this.ID] = true;
					Transform transform2 = this.Labels[this.ID];
					Vector3 localPosition2 = transform2.localPosition;
					if (localPosition2.x > 0f)
					{
						localPosition2.x = -0.1f;
						transform2.localPosition = localPosition2;
					}
				}
				this.ID++;
			}
			this.ID = 0;
			while (this.ID < this.Labels.Length)
			{
				if (this.SlideOut[this.ID])
				{
					Transform transform3 = this.Labels[this.ID];
					Vector3 localPosition3 = transform3.localPosition;
					localPosition3.x += localPosition3.x * 0.01f;
					transform3.localPosition = localPosition3;
				}
				this.ID++;
			}
			if (this.SlideID > this.Labels.Length + 1)
			{
				Color color = this.LinkLabel.color;
				color.a += Time.deltaTime;
				this.LinkLabel.color = color;
			}
			if (this.SlideID > this.Labels.Length + 2)
			{
				Color color2 = this.Yuno1.color;
				color2.a += Time.deltaTime;
				this.Yuno1.color = color2;
			}
			if (this.SlideID > this.Labels.Length + 3)
			{
				Color color3 = this.Yuno2.color;
				color3.a += Time.deltaTime;
				this.Yuno2.color = color3;
				Vector3 localScale = this.Yuno2.transform.localScale;
				localScale.x += Time.deltaTime * 0.1f;
				localScale.y += Time.deltaTime * 0.1f;
				this.Yuno2.transform.localScale = localScale;
			}
		}
	}

	// Token: 0x0400149B RID: 5275
	public Transform[] Labels;

	// Token: 0x0400149C RID: 5276
	public bool[] SlideOut;

	// Token: 0x0400149D RID: 5277
	public bool[] SlideIn;

	// Token: 0x0400149E RID: 5278
	public UILabel LinkLabel;

	// Token: 0x0400149F RID: 5279
	public UITexture Yuno1;

	// Token: 0x040014A0 RID: 5280
	public UITexture Yuno2;

	// Token: 0x040014A1 RID: 5281
	public int SlideID;

	// Token: 0x040014A2 RID: 5282
	public int ID;

	// Token: 0x040014A3 RID: 5283
	public float Timer;
}
