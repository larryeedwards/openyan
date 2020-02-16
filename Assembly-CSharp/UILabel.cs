using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200026E RID: 622
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06001339 RID: 4921 RVA: 0x000970A9 File Offset: 0x000954A9
	public int finalFontSize
	{
		get
		{
			if (this.trueTypeFont)
			{
				return Mathf.RoundToInt(this.mScale * (float)this.mFinalFontSize);
			}
			return Mathf.RoundToInt((float)this.mFinalFontSize * this.mScale);
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x0600133A RID: 4922 RVA: 0x000970E2 File Offset: 0x000954E2
	// (set) Token: 0x0600133B RID: 4923 RVA: 0x000970EA File Offset: 0x000954EA
	private bool shouldBeProcessed
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
			}
			else
			{
				this.mShouldBeProcessed = false;
			}
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x0600133C RID: 4924 RVA: 0x0009710C File Offset: 0x0009550C
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x0600133D RID: 4925 RVA: 0x00097125 File Offset: 0x00095525
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x0600133E RID: 4926 RVA: 0x0009714C File Offset: 0x0009554C
	// (set) Token: 0x0600133F RID: 4927 RVA: 0x000971AC File Offset: 0x000955AC
	public override Material material
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat;
			}
			if (this.mFont != null)
			{
				return this.mFont.material;
			}
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06001340 RID: 4928 RVA: 0x000971B8 File Offset: 0x000955B8
	// (set) Token: 0x06001341 RID: 4929 RVA: 0x00097213 File Offset: 0x00095613
	public override Texture mainTexture
	{
		get
		{
			if (this.mFont != null)
			{
				return this.mFont.texture;
			}
			if (this.mTrueTypeFont != null)
			{
				Material material = this.mTrueTypeFont.material;
				if (material != null)
				{
					return material.mainTexture;
				}
			}
			return null;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06001342 RID: 4930 RVA: 0x0009721C File Offset: 0x0009561C
	// (set) Token: 0x06001343 RID: 4931 RVA: 0x00097224 File Offset: 0x00095624
	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return this.bitmapFont;
		}
		set
		{
			this.bitmapFont = value;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06001344 RID: 4932 RVA: 0x0009722D File Offset: 0x0009562D
	// (set) Token: 0x06001345 RID: 4933 RVA: 0x00097235 File Offset: 0x00095635
	public UIFont bitmapFont
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				base.RemoveFromPanel();
				this.mFont = value;
				this.mTrueTypeFont = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06001346 RID: 4934 RVA: 0x00097262 File Offset: 0x00095662
	// (set) Token: 0x06001347 RID: 4935 RVA: 0x000972A0 File Offset: 0x000956A0
	public Font trueTypeFont
	{
		get
		{
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont;
			}
			return (!(this.mFont != null)) ? null : this.mFont.dynamicFont;
		}
		set
		{
			if (this.mTrueTypeFont != value)
			{
				this.SetActiveFont(null);
				base.RemoveFromPanel();
				this.mTrueTypeFont = value;
				this.shouldBeProcessed = true;
				this.mFont = null;
				this.SetActiveFont(value);
				this.ProcessAndRequest();
				if (this.mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06001348 RID: 4936 RVA: 0x00097304 File Offset: 0x00095704
	// (set) Token: 0x06001349 RID: 4937 RVA: 0x0009731C File Offset: 0x0009571C
	public UnityEngine.Object ambigiousFont
	{
		get
		{
			return this.mFont ?? this.mTrueTypeFont;
		}
		set
		{
			UIFont uifont = value as UIFont;
			if (uifont != null)
			{
				this.bitmapFont = uifont;
			}
			else
			{
				this.trueTypeFont = (value as Font);
			}
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x0600134A RID: 4938 RVA: 0x00097354 File Offset: 0x00095754
	// (set) Token: 0x0600134B RID: 4939 RVA: 0x0009735C File Offset: 0x0009575C
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
					this.MarkAsChanged();
					this.ProcessAndRequest();
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
			}
			if (this.autoResizeBoxCollider)
			{
				base.ResizeCollider();
			}
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x0600134C RID: 4940 RVA: 0x000973E8 File Offset: 0x000957E8
	public int defaultFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null)) ? ((!(this.mFont != null)) ? 16 : this.mFont.defaultSize) : this.mFontSize;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x0600134D RID: 4941 RVA: 0x00097434 File Offset: 0x00095834
	// (set) Token: 0x0600134E RID: 4942 RVA: 0x0009743C File Offset: 0x0009583C
	public int fontSize
	{
		get
		{
			return this.mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (this.mFontSize != value)
			{
				this.mFontSize = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x0600134F RID: 4943 RVA: 0x0009746C File Offset: 0x0009586C
	// (set) Token: 0x06001350 RID: 4944 RVA: 0x00097474 File Offset: 0x00095874
	public FontStyle fontStyle
	{
		get
		{
			return this.mFontStyle;
		}
		set
		{
			if (this.mFontStyle != value)
			{
				this.mFontStyle = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06001351 RID: 4945 RVA: 0x00097496 File Offset: 0x00095896
	// (set) Token: 0x06001352 RID: 4946 RVA: 0x0009749E File Offset: 0x0009589E
	public NGUIText.Alignment alignment
	{
		get
		{
			return this.mAlignment;
		}
		set
		{
			if (this.mAlignment != value)
			{
				this.mAlignment = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06001353 RID: 4947 RVA: 0x000974C0 File Offset: 0x000958C0
	// (set) Token: 0x06001354 RID: 4948 RVA: 0x000974C8 File Offset: 0x000958C8
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06001355 RID: 4949 RVA: 0x000974E3 File Offset: 0x000958E3
	// (set) Token: 0x06001356 RID: 4950 RVA: 0x000974EB File Offset: 0x000958EB
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06001357 RID: 4951 RVA: 0x00097516 File Offset: 0x00095916
	// (set) Token: 0x06001358 RID: 4952 RVA: 0x0009751E File Offset: 0x0009591E
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06001359 RID: 4953 RVA: 0x00097549 File Offset: 0x00095949
	// (set) Token: 0x0600135A RID: 4954 RVA: 0x00097551 File Offset: 0x00095951
	public int spacingX
	{
		get
		{
			return this.mSpacingX;
		}
		set
		{
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x0600135B RID: 4955 RVA: 0x0009756C File Offset: 0x0009596C
	// (set) Token: 0x0600135C RID: 4956 RVA: 0x00097574 File Offset: 0x00095974
	public int spacingY
	{
		get
		{
			return this.mSpacingY;
		}
		set
		{
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x0600135D RID: 4957 RVA: 0x0009758F File Offset: 0x0009598F
	// (set) Token: 0x0600135E RID: 4958 RVA: 0x00097597 File Offset: 0x00095997
	public bool useFloatSpacing
	{
		get
		{
			return this.mUseFloatSpacing;
		}
		set
		{
			if (this.mUseFloatSpacing != value)
			{
				this.mUseFloatSpacing = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x0600135F RID: 4959 RVA: 0x000975B3 File Offset: 0x000959B3
	// (set) Token: 0x06001360 RID: 4960 RVA: 0x000975BB File Offset: 0x000959BB
	public float floatSpacingX
	{
		get
		{
			return this.mFloatSpacingX;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingX, value))
			{
				this.mFloatSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06001361 RID: 4961 RVA: 0x000975DB File Offset: 0x000959DB
	// (set) Token: 0x06001362 RID: 4962 RVA: 0x000975E3 File Offset: 0x000959E3
	public float floatSpacingY
	{
		get
		{
			return this.mFloatSpacingY;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingY, value))
			{
				this.mFloatSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06001363 RID: 4963 RVA: 0x00097603 File Offset: 0x00095A03
	public float effectiveSpacingY
	{
		get
		{
			return (!this.mUseFloatSpacing) ? ((float)this.mSpacingY) : this.mFloatSpacingY;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06001364 RID: 4964 RVA: 0x00097622 File Offset: 0x00095A22
	public float effectiveSpacingX
	{
		get
		{
			return (!this.mUseFloatSpacing) ? ((float)this.mSpacingX) : this.mFloatSpacingX;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06001365 RID: 4965 RVA: 0x00097641 File Offset: 0x00095A41
	// (set) Token: 0x06001366 RID: 4966 RVA: 0x00097649 File Offset: 0x00095A49
	public bool overflowEllipsis
	{
		get
		{
			return this.mOverflowEllipsis;
		}
		set
		{
			if (this.mOverflowEllipsis != value)
			{
				this.mOverflowEllipsis = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06001367 RID: 4967 RVA: 0x00097664 File Offset: 0x00095A64
	// (set) Token: 0x06001368 RID: 4968 RVA: 0x0009766C File Offset: 0x00095A6C
	public int overflowWidth
	{
		get
		{
			return this.mOverflowWidth;
		}
		set
		{
			if (this.mOverflowWidth != value)
			{
				this.mOverflowWidth = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06001369 RID: 4969 RVA: 0x00097687 File Offset: 0x00095A87
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x0600136A RID: 4970 RVA: 0x000976A8 File Offset: 0x00095AA8
	// (set) Token: 0x0600136B RID: 4971 RVA: 0x000976B0 File Offset: 0x00095AB0
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x0600136C RID: 4972 RVA: 0x000976CC File Offset: 0x00095ACC
	// (set) Token: 0x0600136D RID: 4973 RVA: 0x000976D4 File Offset: 0x00095AD4
	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x0600136E RID: 4974 RVA: 0x000976F0 File Offset: 0x00095AF0
	// (set) Token: 0x0600136F RID: 4975 RVA: 0x000976F8 File Offset: 0x00095AF8
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06001370 RID: 4976 RVA: 0x00097714 File Offset: 0x00095B14
	// (set) Token: 0x06001371 RID: 4977 RVA: 0x0009771C File Offset: 0x00095B1C
	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06001372 RID: 4978 RVA: 0x00097725 File Offset: 0x00095B25
	// (set) Token: 0x06001373 RID: 4979 RVA: 0x0009772D File Offset: 0x00095B2D
	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06001374 RID: 4980 RVA: 0x00097736 File Offset: 0x00095B36
	// (set) Token: 0x06001375 RID: 4981 RVA: 0x00097744 File Offset: 0x00095B44
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06001376 RID: 4982 RVA: 0x00097772 File Offset: 0x00095B72
	public override Vector3[] localCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localCorners;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06001377 RID: 4983 RVA: 0x0009778D File Offset: 0x00095B8D
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.worldCorners;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06001378 RID: 4984 RVA: 0x000977A8 File Offset: 0x00095BA8
	public override Vector4 drawingDimensions
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.drawingDimensions;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06001379 RID: 4985 RVA: 0x000977C3 File Offset: 0x00095BC3
	// (set) Token: 0x0600137A RID: 4986 RVA: 0x000977CB File Offset: 0x00095BCB
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.shouldBeProcessed = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x0600137B RID: 4987 RVA: 0x000977FE File Offset: 0x00095BFE
	// (set) Token: 0x0600137C RID: 4988 RVA: 0x00097806 File Offset: 0x00095C06
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x0600137D RID: 4989 RVA: 0x00097822 File Offset: 0x00095C22
	// (set) Token: 0x0600137E RID: 4990 RVA: 0x0009782A File Offset: 0x00095C2A
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.shouldBeProcessed = true;
				}
			}
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x0600137F RID: 4991 RVA: 0x00097856 File Offset: 0x00095C56
	// (set) Token: 0x06001380 RID: 4992 RVA: 0x0009785E File Offset: 0x00095C5E
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06001381 RID: 4993 RVA: 0x0009787F File Offset: 0x00095C7F
	public int quadsPerCharacter
	{
		get
		{
			if (this.mEffectStyle == UILabel.Effect.Shadow)
			{
				return 2;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline)
			{
				return 5;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline8)
			{
				return 9;
			}
			return 1;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06001382 RID: 4994 RVA: 0x000978AD File Offset: 0x00095CAD
	// (set) Token: 0x06001383 RID: 4995 RVA: 0x000978B8 File Offset: 0x00095CB8
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06001384 RID: 4996 RVA: 0x000978C8 File Offset: 0x00095CC8
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06001385 RID: 4997 RVA: 0x0009792F File Offset: 0x00095D2F
	public Vector2 printedSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mCalculatedSize;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06001386 RID: 4998 RVA: 0x0009794A File Offset: 0x00095D4A
	public override Vector2 localSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localSize;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06001387 RID: 4999 RVA: 0x00097965 File Offset: 0x00095D65
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06001388 RID: 5000 RVA: 0x00097987 File Offset: 0x00095D87
	// (set) Token: 0x06001389 RID: 5001 RVA: 0x0009798F File Offset: 0x00095D8F
	public UILabel.Modifier modifier
	{
		get
		{
			return this.mModifier;
		}
		set
		{
			if (this.mModifier != value)
			{
				this.mModifier = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000979B0 File Offset: 0x00095DB0
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000979CF File Offset: 0x00095DCF
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000979EC File Offset: 0x00095DEC
	protected void SetActiveFont(Font fnt)
	{
		if (this.mActiveTTF != fnt)
		{
			Font font = this.mActiveTTF;
			int num;
			if (font != null && UILabel.mFontUsage.TryGetValue(font, out num))
			{
				num = Mathf.Max(0, --num);
				if (num == 0)
				{
					UILabel.mFontUsage.Remove(font);
				}
				else
				{
					UILabel.mFontUsage[font] = num;
				}
			}
			this.mActiveTTF = fnt;
			if (fnt != null)
			{
				int num2 = 0;
				UILabel.mFontUsage[fnt] = num2 + 1;
			}
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x0600138D RID: 5005 RVA: 0x00097A88 File Offset: 0x00095E88
	public string printedText
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mText))
			{
				if (this.mModifier == UILabel.Modifier.None)
				{
					return this.mText;
				}
				if (this.mModifier == UILabel.Modifier.ToLowercase)
				{
					return this.mText.ToLower();
				}
				if (this.mModifier == UILabel.Modifier.ToUppercase)
				{
					return this.mText.ToUpper();
				}
				if (this.mModifier == UILabel.Modifier.Custom && this.customModifier != null)
				{
					return this.customModifier(this.mText);
				}
			}
			return this.mText;
		}
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x00097B1C File Offset: 0x00095F1C
	private static void OnFontChanged(Font font)
	{
		for (int i = 0; i < UILabel.mList.size; i++)
		{
			UILabel uilabel = UILabel.mList[i];
			if (uilabel != null)
			{
				Font trueTypeFont = uilabel.trueTypeFont;
				if (trueTypeFont == font)
				{
					trueTypeFont.RequestCharactersInTexture(uilabel.mText, uilabel.mFinalFontSize, uilabel.mFontStyle);
					uilabel.MarkAsChanged();
					if (uilabel.panel == null)
					{
						uilabel.CreatePanel();
					}
					if (UILabel.mTempDrawcalls == null)
					{
						UILabel.mTempDrawcalls = new BetterList<UIDrawCall>();
					}
					if (uilabel.drawCall != null && !UILabel.mTempDrawcalls.Contains(uilabel.drawCall))
					{
						UILabel.mTempDrawcalls.Add(uilabel.drawCall);
					}
				}
			}
		}
		if (UILabel.mTempDrawcalls != null)
		{
			int j = 0;
			int size = UILabel.mTempDrawcalls.size;
			while (j < size)
			{
				UIDrawCall uidrawCall = UILabel.mTempDrawcalls[j];
				if (uidrawCall.panel != null)
				{
					uidrawCall.panel.FillDrawCall(uidrawCall);
				}
				j++;
			}
			UILabel.mTempDrawcalls.Clear();
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00097C50 File Offset: 0x00096050
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText(false, true);
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x00097C6C File Offset: 0x0009606C
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true, true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount <= 0) ? UILabel.Overflow.ShrinkContent : UILabel.Overflow.ResizeHeight);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int defaultSize = this.mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
			this.fontSize = defaultSize;
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x00097D48 File Offset: 0x00096148
	protected override void OnAnchor()
	{
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				this.mOverflow = UILabel.Overflow.ShrinkContent;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeHeight && this.topAnchor.target != null && this.bottomAnchor.target != null)
		{
			this.mOverflow = UILabel.Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00097DBD File Offset: 0x000961BD
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText(false, true);
		}
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x00097DD8 File Offset: 0x000961D8
	protected override void OnEnable()
	{
		base.OnEnable();
		if (!UILabel.mTexRebuildAdded)
		{
			UILabel.mTexRebuildAdded = true;
			if (UILabel.<>f__mg$cache0 == null)
			{
				UILabel.<>f__mg$cache0 = new Action<Font>(UILabel.OnFontChanged);
			}
			Font.textureRebuilt += UILabel.<>f__mg$cache0;
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00097E14 File Offset: 0x00096214
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.material != null && this.material.shader != null && this.material.shader.name.Contains("Premultiplied"));
		this.ProcessAndRequest();
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x00097EBC File Offset: 0x000962BC
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x00097ECC File Offset: 0x000962CC
	public void ProcessText(bool legacyMode = false, bool full = true)
	{
		if (!this.isValid)
		{
			return;
		}
		this.mChanged = true;
		this.shouldBeProcessed = false;
		float num = this.mDrawRegion.z - this.mDrawRegion.x;
		float num2 = this.mDrawRegion.w - this.mDrawRegion.y;
		NGUIText.rectWidth = ((!legacyMode) ? base.width : ((this.mMaxLineWidth == 0) ? 1000000 : this.mMaxLineWidth));
		NGUIText.rectHeight = ((!legacyMode) ? base.height : ((this.mMaxLineHeight == 0) ? 1000000 : this.mMaxLineHeight));
		NGUIText.regionWidth = ((num == 1f) ? NGUIText.rectWidth : Mathf.RoundToInt((float)NGUIText.rectWidth * num));
		NGUIText.regionHeight = ((num2 == 1f) ? NGUIText.rectHeight : Mathf.RoundToInt((float)NGUIText.rectHeight * num2));
		this.mFinalFontSize = Mathf.Abs((!legacyMode) ? this.defaultFontSize : Mathf.RoundToInt(base.cachedTransform.localScale.x));
		this.mScale = 1f;
		if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 0)
		{
			this.mProcessedText = string.Empty;
			return;
		}
		bool flag = this.trueTypeFont != null;
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				this.mDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			this.mDensity = 1f;
		}
		if (full)
		{
			this.UpdateNGUIText();
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			NGUIText.rectWidth = 1000000;
			NGUIText.regionWidth = 1000000;
			if (this.mOverflowWidth > 0)
			{
				NGUIText.rectWidth = Mathf.Min(NGUIText.rectWidth, this.mOverflowWidth);
				NGUIText.regionWidth = Mathf.Min(NGUIText.regionWidth, this.mOverflowWidth);
			}
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
		}
		if (this.mFinalFontSize > 0)
		{
			bool keepCrisp = this.keepCrisp;
			for (int i = this.mFinalFontSize; i > 0; i--)
			{
				if (keepCrisp)
				{
					this.mFinalFontSize = i;
					NGUIText.fontSize = this.mFinalFontSize;
				}
				else
				{
					this.mScale = (float)i / (float)this.mFinalFontSize;
					NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
				}
				NGUIText.Update(false);
				bool flag2 = NGUIText.WrapText(this.printedText, out this.mProcessedText, false, false, this.mOverflow == UILabel.Overflow.ClampContent && this.mOverflowEllipsis);
				if (this.mOverflow != UILabel.Overflow.ShrinkContent || flag2)
				{
					if (this.mOverflow == UILabel.Overflow.ResizeFreely)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						int num3 = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
						if (num != 1f)
						{
							num3 = Mathf.RoundToInt((float)num3 / num);
						}
						int num4 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if (num2 != 1f)
						{
							num4 = Mathf.RoundToInt((float)num4 / num2);
						}
						if ((num3 & 1) == 1)
						{
							num3++;
						}
						if ((num4 & 1) == 1)
						{
							num4++;
						}
						if (this.mWidth != num3 || this.mHeight != num4)
						{
							this.mWidth = num3;
							this.mHeight = num4;
							if (this.onChange != null)
							{
								this.onChange();
							}
						}
					}
					else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						int num5 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if (num2 != 1f)
						{
							num5 = Mathf.RoundToInt((float)num5 / num2);
						}
						if ((num5 & 1) == 1)
						{
							num5++;
						}
						if (this.mHeight != num5)
						{
							this.mHeight = num5;
							if (this.onChange != null)
							{
								this.onChange();
							}
						}
					}
					else
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
					}
					if (legacyMode)
					{
						base.width = Mathf.RoundToInt(this.mCalculatedSize.x);
						base.height = Mathf.RoundToInt(this.mCalculatedSize.y);
						base.cachedTransform.localScale = Vector3.one;
					}
					break;
				}
				if (--i <= 1)
				{
					break;
				}
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = string.Empty;
			this.mScale = 1f;
		}
		if (full)
		{
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x00098424 File Offset: 0x00096824
	public override void MakePixelPerfect()
	{
		if (this.ambigiousFont != null)
		{
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = Vector3.one;
			if (this.mOverflow == UILabel.Overflow.ResizeFreely)
			{
				this.AssumeNaturalSize();
			}
			else
			{
				int width = base.width;
				int height = base.height;
				UILabel.Overflow overflow = this.mOverflow;
				if (overflow != UILabel.Overflow.ResizeHeight)
				{
					this.mWidth = 100000;
				}
				this.mHeight = 100000;
				this.mOverflow = UILabel.Overflow.ShrinkContent;
				this.ProcessText(false, true);
				this.mOverflow = overflow;
				int num = Mathf.RoundToInt(this.mCalculatedSize.x);
				int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
				num = Mathf.Max(num, base.minWidth);
				num2 = Mathf.Max(num2, base.minHeight);
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				this.mWidth = Mathf.Max(width, num);
				this.mHeight = Mathf.Max(height, num2);
				this.MarkAsChanged();
			}
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x00098594 File Offset: 0x00096994
	public void AssumeNaturalSize()
	{
		if (this.ambigiousFont != null)
		{
			this.mWidth = 100000;
			this.mHeight = 100000;
			this.ProcessText(false, true);
			this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
			this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
			if ((this.mWidth & 1) == 1)
			{
				this.mWidth++;
			}
			if ((this.mHeight & 1) == 1)
			{
				this.mHeight++;
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x0009863A File Offset: 0x00096A3A
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos, false);
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x00098644 File Offset: 0x00096A44
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos, false);
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00098650 File Offset: 0x00096A50
	public int GetCharacterIndexAtPosition(Vector3 worldPos, bool precise)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos, precise);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00098678 File Offset: 0x00096A78
	public int GetCharacterIndexAtPosition(Vector2 localPos, bool precise)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			this.UpdateNGUIText();
			if (precise)
			{
				NGUIText.PrintExactCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			else
			{
				NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int result = (!precise) ? NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos) : NGUIText.GetExactCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos);
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return result;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x00098750 File Offset: 0x00096B50
	public string GetWordAtPosition(Vector3 worldPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(worldPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00098770 File Offset: 0x00096B70
	public string GetWordAtPosition(Vector2 localPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(localPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x00098790 File Offset: 0x00096B90
	public string GetWordAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length)
		{
			int num = printedText.LastIndexOfAny(new char[]
			{
				' ',
				'\n'
			}, characterIndex) + 1;
			int num2 = printedText.IndexOfAny(new char[]
			{
				' ',
				'\n',
				',',
				'.'
			}, characterIndex);
			if (num2 == -1)
			{
				num2 = printedText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					string text = printedText.Substring(num, num3);
					return NGUIText.StripSymbols(text);
				}
			}
		}
		return null;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x0009881D File Offset: 0x00096C1D
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos, true));
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x0009882D File Offset: 0x00096C2D
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos, true));
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00098840 File Offset: 0x00096C40
	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length - 6)
		{
			int num;
			if (printedText[characterIndex] == '[' && printedText[characterIndex + 1] == 'u' && printedText[characterIndex + 2] == 'r' && printedText[characterIndex + 3] == 'l' && printedText[characterIndex + 4] == '=')
			{
				num = characterIndex;
			}
			else
			{
				num = printedText.LastIndexOf("[url=", characterIndex);
			}
			if (num == -1)
			{
				return null;
			}
			num += 5;
			int num2 = printedText.IndexOf("]", num);
			if (num2 == -1)
			{
				return null;
			}
			int num3 = printedText.IndexOf("[/url]", num2);
			if (num3 == -1 || characterIndex <= num3)
			{
				return printedText.Substring(num, num2 - num);
			}
		}
		return null;
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00098918 File Offset: 0x00096D18
	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			this.UpdateNGUIText();
			NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int i = 0;
				int count = UILabel.mTempIndices.Count;
				while (i < count)
				{
					if (UILabel.mTempIndices[i] == currentIndex)
					{
						Vector2 pos = UILabel.mTempVerts[i];
						if (key == KeyCode.UpArrow)
						{
							pos.y += (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.DownArrow)
						{
							pos.y -= (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.Home)
						{
							pos.x -= 1000f;
						}
						else if (key == KeyCode.End)
						{
							pos.x += 1000f;
						}
						int approximateCharacterIndex = NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, pos);
						if (approximateCharacterIndex == currentIndex)
						{
							break;
						}
						UILabel.mTempVerts.Clear();
						UILabel.mTempIndices.Clear();
						return approximateCharacterIndex;
					}
					else
					{
						i++;
					}
				}
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if (key == KeyCode.UpArrow || key == KeyCode.Home)
			{
				return 0;
			}
			if (key == KeyCode.DownArrow || key == KeyCode.End)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00098ADC File Offset: 0x00096EDC
	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		if (caret != null)
		{
			caret.Clear();
		}
		if (highlight != null)
		{
			highlight.Clear();
		}
		if (!this.isValid)
		{
			return;
		}
		string processedText = this.processedText;
		this.UpdateNGUIText();
		int count = caret.verts.Count;
		Vector2 item = new Vector2(0.5f, 0.5f);
		float finalAlpha = this.finalAlpha;
		if (highlight != null && start != end)
		{
			int count2 = highlight.verts.Count;
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
			if (highlight.verts.Count > count2)
			{
				this.ApplyOffset(highlight.verts, count2);
				Color item2 = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha);
				int i = count2;
				int count3 = highlight.verts.Count;
				while (i < count3)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
					i++;
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
		}
		this.ApplyOffset(caret.verts, count);
		Color item3 = new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha);
		int j = count;
		int count4 = caret.verts.Count;
		while (j < count4)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
			j++;
		}
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00098C8C File Offset: 0x0009708C
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (!this.isValid)
		{
			return;
		}
		int num = verts.Count;
		Color color = base.color;
		color.a = this.finalAlpha;
		if (this.mFont != null && this.mFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string processedText = this.processedText;
		int count = verts.Count;
		this.UpdateNGUIText();
		NGUIText.tint = color;
		NGUIText.Print(processedText, verts, uvs, cols);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		Vector2 vector = this.ApplyOffset(verts, count);
		if (this.mFont != null && this.mFont.packedFontShader)
		{
			return;
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int count2 = verts.Count;
			vector.x = this.mEffectDistance.x;
			vector.y = this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, -vector.y);
			if (this.effectStyle == UILabel.Effect.Outline || this.effectStyle == UILabel.Effect.Outline8)
			{
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, -vector.y);
				if (this.effectStyle == UILabel.Effect.Outline8)
				{
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, vector.y);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, -vector.y);
				}
			}
		}
		if (this.onPostFill != null)
		{
			this.onPostFill(this, num, verts, uvs, cols);
		}
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x00098ED4 File Offset: 0x000972D4
	public Vector2 ApplyOffset(List<Vector3> verts, int start)
	{
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float)this.mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		int i = start;
		int count = verts.Count;
		while (i < count)
		{
			Vector3 value = verts[i];
			value.x += num;
			value.y += num2;
			verts[i] = value;
			i++;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x00098FA4 File Offset: 0x000973A4
	public void ApplyShadow(List<Vector3> verts, List<Vector2> uvs, List<Color> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= this.finalAlpha;
		if (this.bitmapFont != null && this.bitmapFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		Color value = color;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts[i]);
			uvs.Add(uvs[i]);
			cols.Add(cols[i]);
			Vector3 value2 = verts[i];
			value2.x += x;
			value2.y += y;
			verts[i] = value2;
			Color color2 = cols[i];
			if (color2.a == 1f)
			{
				cols[i] = value;
			}
			else
			{
				Color value3 = color;
				value3.a = color2.a * color.a;
				cols[i] = value3;
			}
		}
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000990AC File Offset: 0x000974AC
	public int CalculateOffsetToFit(string text)
	{
		this.UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000990E0 File Offset: 0x000974E0
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x0009911A File Offset: 0x0009751A
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x00099158 File Offset: 0x00097558
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = ((!UIPopupList.current.isLocalized) ? UIPopupList.current.value : Localization.Get(UIPopupList.current.value, true));
		}
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x000991A9 File Offset: 0x000975A9
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000991B8 File Offset: 0x000975B8
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		NGUIText.rectHeight = height;
		NGUIText.regionHeight = height;
		bool result = NGUIText.WrapText(text, out final, false);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000991F0 File Offset: 0x000975F0
	public void UpdateNGUIText()
	{
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		NGUIText.fontSize = this.mFinalFontSize;
		NGUIText.fontStyle = this.mFontStyle;
		NGUIText.rectWidth = this.mWidth;
		NGUIText.rectHeight = this.mHeight;
		NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
		NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		NGUIText.gradient = (this.mApplyGradient && (this.mFont == null || !this.mFont.packedFontShader));
		NGUIText.gradientTop = this.mGradientTop;
		NGUIText.gradientBottom = this.mGradientBottom;
		NGUIText.encoding = this.mEncoding;
		NGUIText.premultiply = this.mPremultiply;
		NGUIText.symbolStyle = this.mSymbols;
		NGUIText.maxLines = this.mMaxLineCount;
		NGUIText.spacingX = this.effectiveSpacingX;
		NGUIText.spacingY = this.effectiveSpacingY;
		NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
		if (this.mFont != null)
		{
			NGUIText.bitmapFont = this.mFont;
			for (;;)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if (replacement == null)
				{
					break;
				}
				NGUIText.bitmapFont = replacement;
			}
			if (NGUIText.bitmapFont.isDynamic)
			{
				NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
			}
		}
		else
		{
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (this.mDensity != NGUIText.pixelDensity)
		{
			this.ProcessText(false, false);
			NGUIText.rectWidth = this.mWidth;
			NGUIText.rectHeight = this.mHeight;
			NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
			NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		}
		if (this.alignment == NGUIText.Alignment.Automatic)
		{
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
			{
				NGUIText.alignment = NGUIText.Alignment.Left;
			}
			else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				NGUIText.alignment = NGUIText.Alignment.Right;
			}
			else
			{
				NGUIText.alignment = NGUIText.Alignment.Center;
			}
		}
		else
		{
			NGUIText.alignment = this.alignment;
		}
		NGUIText.Update();
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00099517 File Offset: 0x00097917
	private void OnApplicationPause(bool paused)
	{
		if (!paused && this.mTrueTypeFont != null)
		{
			this.Invalidate(false);
		}
	}

	// Token: 0x04001091 RID: 4241
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x04001092 RID: 4242
	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	// Token: 0x04001093 RID: 4243
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x04001094 RID: 4244
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	// Token: 0x04001095 RID: 4245
	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	// Token: 0x04001096 RID: 4246
	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	// Token: 0x04001097 RID: 4247
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x04001098 RID: 4248
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x04001099 RID: 4249
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x0400109A RID: 4250
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x0400109B RID: 4251
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x0400109C RID: 4252
	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x0400109D RID: 4253
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x0400109E RID: 4254
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x0400109F RID: 4255
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x040010A0 RID: 4256
	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	// Token: 0x040010A1 RID: 4257
	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x040010A2 RID: 4258
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x040010A3 RID: 4259
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x040010A4 RID: 4260
	[HideInInspector]
	[SerializeField]
	private bool mUseFloatSpacing;

	// Token: 0x040010A5 RID: 4261
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingX;

	// Token: 0x040010A6 RID: 4262
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingY;

	// Token: 0x040010A7 RID: 4263
	[HideInInspector]
	[SerializeField]
	private bool mOverflowEllipsis;

	// Token: 0x040010A8 RID: 4264
	[HideInInspector]
	[SerializeField]
	private int mOverflowWidth;

	// Token: 0x040010A9 RID: 4265
	[HideInInspector]
	[SerializeField]
	private UILabel.Modifier mModifier;

	// Token: 0x040010AA RID: 4266
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x040010AB RID: 4267
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x040010AC RID: 4268
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x040010AD RID: 4269
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x040010AE RID: 4270
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x040010AF RID: 4271
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x040010B0 RID: 4272
	[NonSerialized]
	private float mDensity = 1f;

	// Token: 0x040010B1 RID: 4273
	[NonSerialized]
	private bool mShouldBeProcessed = true;

	// Token: 0x040010B2 RID: 4274
	[NonSerialized]
	private string mProcessedText;

	// Token: 0x040010B3 RID: 4275
	[NonSerialized]
	private bool mPremultiply;

	// Token: 0x040010B4 RID: 4276
	[NonSerialized]
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x040010B5 RID: 4277
	[NonSerialized]
	private float mScale = 1f;

	// Token: 0x040010B6 RID: 4278
	[NonSerialized]
	private int mFinalFontSize;

	// Token: 0x040010B7 RID: 4279
	[NonSerialized]
	private int mLastWidth;

	// Token: 0x040010B8 RID: 4280
	[NonSerialized]
	private int mLastHeight;

	// Token: 0x040010B9 RID: 4281
	public UILabel.ModifierFunc customModifier;

	// Token: 0x040010BA RID: 4282
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x040010BB RID: 4283
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x040010BC RID: 4284
	[NonSerialized]
	private static BetterList<UIDrawCall> mTempDrawcalls;

	// Token: 0x040010BD RID: 4285
	private static bool mTexRebuildAdded = false;

	// Token: 0x040010BE RID: 4286
	private static List<Vector3> mTempVerts = new List<Vector3>();

	// Token: 0x040010BF RID: 4287
	private static List<int> mTempIndices = new List<int>();

	// Token: 0x040010C0 RID: 4288
	[CompilerGenerated]
	private static Action<Font> <>f__mg$cache0;

	// Token: 0x0200026F RID: 623
	public enum Effect
	{
		// Token: 0x040010C2 RID: 4290
		None,
		// Token: 0x040010C3 RID: 4291
		Shadow,
		// Token: 0x040010C4 RID: 4292
		Outline,
		// Token: 0x040010C5 RID: 4293
		Outline8
	}

	// Token: 0x02000270 RID: 624
	public enum Overflow
	{
		// Token: 0x040010C7 RID: 4295
		ShrinkContent,
		// Token: 0x040010C8 RID: 4296
		ClampContent,
		// Token: 0x040010C9 RID: 4297
		ResizeFreely,
		// Token: 0x040010CA RID: 4298
		ResizeHeight
	}

	// Token: 0x02000271 RID: 625
	public enum Crispness
	{
		// Token: 0x040010CC RID: 4300
		Never,
		// Token: 0x040010CD RID: 4301
		OnDesktop,
		// Token: 0x040010CE RID: 4302
		Always
	}

	// Token: 0x02000272 RID: 626
	public enum Modifier
	{
		// Token: 0x040010D0 RID: 4304
		None,
		// Token: 0x040010D1 RID: 4305
		ToUppercase,
		// Token: 0x040010D2 RID: 4306
		ToLowercase,
		// Token: 0x040010D3 RID: 4307
		Custom = 255
	}

	// Token: 0x02000273 RID: 627
	// (Invoke) Token: 0x060013B2 RID: 5042
	public delegate string ModifierFunc(string s);
}
