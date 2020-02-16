using System;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class GrowShrinkScript : MonoBehaviour
{
	// Token: 0x06001C60 RID: 7264 RVA: 0x000FD783 File Offset: 0x000FBB83
	private void Start()
	{
		this.OriginalPosition = base.transform.localPosition;
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x000FD7A8 File Offset: 0x000FBBA8
	private void Update()
	{
		this.Timer += Time.deltaTime;
		this.Scale += Time.deltaTime * (this.Strength * this.Speed);
		if (!this.Shrink)
		{
			this.Strength += Time.deltaTime * this.Speed;
			if (this.Strength > this.Threshold)
			{
				this.Strength = this.Threshold;
			}
			if (this.Scale > this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = true;
			}
		}
		else
		{
			this.Strength -= Time.deltaTime * this.Speed;
			float num = this.Threshold * -1f;
			if (this.Strength < num)
			{
				this.Strength = num;
			}
			if (this.Scale < this.Target)
			{
				this.Threshold *= this.Slowdown;
				this.Shrink = false;
			}
		}
		if (this.Timer > 3.33333f)
		{
			this.FallSpeed += Time.deltaTime * 10f;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - this.FallSpeed * this.FallSpeed, base.transform.localPosition.z);
		}
		base.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x000FD960 File Offset: 0x000FBD60
	public void Return()
	{
		base.transform.localPosition = this.OriginalPosition;
		base.transform.localScale = Vector3.zero;
		this.FallSpeed = 0f;
		this.Threshold = 1f;
		this.Slowdown = 0.5f;
		this.Strength = 1f;
		this.Target = 1f;
		this.Scale = 0f;
		this.Speed = 5f;
		this.Timer = 0f;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04002090 RID: 8336
	public float FallSpeed;

	// Token: 0x04002091 RID: 8337
	public float Threshold = 1f;

	// Token: 0x04002092 RID: 8338
	public float Slowdown = 0.5f;

	// Token: 0x04002093 RID: 8339
	public float Strength = 1f;

	// Token: 0x04002094 RID: 8340
	public float Target = 1f;

	// Token: 0x04002095 RID: 8341
	public float Scale;

	// Token: 0x04002096 RID: 8342
	public float Speed = 5f;

	// Token: 0x04002097 RID: 8343
	public float Timer;

	// Token: 0x04002098 RID: 8344
	public bool Shrink;

	// Token: 0x04002099 RID: 8345
	public Vector3 OriginalPosition;
}
