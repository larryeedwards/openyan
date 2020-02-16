using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027F RID: 639
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06001446 RID: 5190 RVA: 0x0009D51B File Offset: 0x0009B91B
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06001447 RID: 5191 RVA: 0x0009D528 File Offset: 0x0009B928
	// (set) Token: 0x06001448 RID: 5192 RVA: 0x0009D530 File Offset: 0x0009B930
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06001449 RID: 5193 RVA: 0x0009D539 File Offset: 0x0009B939
	// (set) Token: 0x0600144A RID: 5194 RVA: 0x0009D541 File Offset: 0x0009B941
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x0600144B RID: 5195 RVA: 0x0009D561 File Offset: 0x0009B961
	// (set) Token: 0x0600144C RID: 5196 RVA: 0x0009D569 File Offset: 0x0009B969
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x0600144D RID: 5197 RVA: 0x0009D572 File Offset: 0x0009B972
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0009D57A File Offset: 0x0009B97A
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x0009D584 File Offset: 0x0009B984
	protected virtual void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && this.mFPS > 0)
		{
			this.mDelta += Mathf.Min(1f, RealTime.deltaTime);
			float num = 1f / (float)this.mFPS;
			while (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.frameIndex >= this.mSpriteNames.Count)
				{
					this.frameIndex = 0;
					this.mActive = this.mLoop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
					if (this.mSnap)
					{
						this.mSprite.MakePixelPerfect();
					}
				}
			}
		}
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0009D694 File Offset: 0x0009BA94
	public void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UISpriteData uispriteData = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x0009D764 File Offset: 0x0009BB64
	public void Play()
	{
		this.mActive = true;
	}

	// Token: 0x06001452 RID: 5202 RVA: 0x0009D76D File Offset: 0x0009BB6D
	public void Pause()
	{
		this.mActive = false;
	}

	// Token: 0x06001453 RID: 5203 RVA: 0x0009D778 File Offset: 0x0009BB78
	public void ResetToBeginning()
	{
		this.mActive = true;
		this.frameIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.frameIndex];
			if (this.mSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04001126 RID: 4390
	public int frameIndex;

	// Token: 0x04001127 RID: 4391
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x04001128 RID: 4392
	[HideInInspector]
	[SerializeField]
	protected string mPrefix = string.Empty;

	// Token: 0x04001129 RID: 4393
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x0400112A RID: 4394
	[HideInInspector]
	[SerializeField]
	protected bool mSnap = true;

	// Token: 0x0400112B RID: 4395
	protected UISprite mSprite;

	// Token: 0x0400112C RID: 4396
	protected float mDelta;

	// Token: 0x0400112D RID: 4397
	protected bool mActive = true;

	// Token: 0x0400112E RID: 4398
	protected List<string> mSpriteNames = new List<string>();
}
