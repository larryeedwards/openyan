using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06001204 RID: 4612 RVA: 0x0008DE64 File Offset: 0x0008C264
	public bool isPlaying
	{
		get
		{
			return base.enabled;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06001205 RID: 4613 RVA: 0x0008DE6C File Offset: 0x0008C26C
	// (set) Token: 0x06001206 RID: 4614 RVA: 0x0008DE74 File Offset: 0x0008C274
	public int framesPerSecond
	{
		get
		{
			return this.framerate;
		}
		set
		{
			this.framerate = value;
		}
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0008DE80 File Offset: 0x0008C280
	public void Play()
	{
		if (this.frames != null && this.frames.Length > 0)
		{
			if (!base.enabled && !this.loop)
			{
				int num = (this.framerate <= 0) ? (this.frameIndex - 1) : (this.frameIndex + 1);
				if (num < 0 || num >= this.frames.Length)
				{
					this.frameIndex = ((this.framerate >= 0) ? 0 : (this.frames.Length - 1));
				}
			}
			base.enabled = true;
			this.UpdateSprite();
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x0008DF22 File Offset: 0x0008C322
	public void Pause()
	{
		base.enabled = false;
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0008DF2B File Offset: 0x0008C32B
	public void ResetToBeginning()
	{
		this.frameIndex = ((this.framerate >= 0) ? 0 : (this.frames.Length - 1));
		this.UpdateSprite();
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0008DF55 File Offset: 0x0008C355
	private void Start()
	{
		this.Play();
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x0008DF60 File Offset: 0x0008C360
	private void Update()
	{
		if (this.frames == null || this.frames.Length == 0)
		{
			base.enabled = false;
		}
		else if (this.framerate != 0)
		{
			float num = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
			if (this.mUpdate < num)
			{
				this.mUpdate = num;
				int num2 = (this.framerate <= 0) ? (this.frameIndex - 1) : (this.frameIndex + 1);
				if (!this.loop && (num2 < 0 || num2 >= this.frames.Length))
				{
					base.enabled = false;
					return;
				}
				this.frameIndex = NGUIMath.RepeatIndex(num2, this.frames.Length);
				this.UpdateSprite();
			}
		}
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0008E030 File Offset: 0x0008C430
	private void UpdateSprite()
	{
		if (this.mUnitySprite == null && this.mNguiSprite == null)
		{
			this.mUnitySprite = base.GetComponent<SpriteRenderer>();
			this.mNguiSprite = base.GetComponent<UI2DSprite>();
			if (this.mUnitySprite == null && this.mNguiSprite == null)
			{
				base.enabled = false;
				return;
			}
		}
		float num = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
		if (this.framerate != 0)
		{
			this.mUpdate = num + Mathf.Abs(1f / (float)this.framerate);
		}
		if (this.mUnitySprite != null)
		{
			this.mUnitySprite.sprite = this.frames[this.frameIndex];
		}
		else if (this.mNguiSprite != null)
		{
			this.mNguiSprite.nextSprite = this.frames[this.frameIndex];
		}
	}

	// Token: 0x04000F69 RID: 3945
	public int frameIndex;

	// Token: 0x04000F6A RID: 3946
	[SerializeField]
	protected int framerate = 20;

	// Token: 0x04000F6B RID: 3947
	public bool ignoreTimeScale = true;

	// Token: 0x04000F6C RID: 3948
	public bool loop = true;

	// Token: 0x04000F6D RID: 3949
	public Sprite[] frames;

	// Token: 0x04000F6E RID: 3950
	private SpriteRenderer mUnitySprite;

	// Token: 0x04000F6F RID: 3951
	private UI2DSprite mNguiSprite;

	// Token: 0x04000F70 RID: 3952
	private float mUpdate;
}
