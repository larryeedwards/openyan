using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class TweenLetters : UITweener
{
	// Token: 0x0600117F RID: 4479 RVA: 0x0008C2A4 File Offset: 0x0008A6A4
	private void OnEnable()
	{
		this.mVertexCount = -1;
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Combine(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0008C2D4 File Offset: 0x0008A6D4
	private void OnDisable()
	{
		UILabel uilabel = this.mLabel;
		uilabel.onPostFill = (UIWidget.OnPostFillCallback)Delegate.Remove(uilabel.onPostFill, new UIWidget.OnPostFillCallback(this.OnPostFill));
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0008C2FD File Offset: 0x0008A6FD
	private void Awake()
	{
		this.mLabel = base.GetComponent<UILabel>();
		this.mCurrent = this.hoverOver;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0008C317 File Offset: 0x0008A717
	public override void Play(bool forward)
	{
		this.mCurrent = ((!forward) ? this.hoverOut : this.hoverOver);
		base.Play(forward);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0008C340 File Offset: 0x0008A740
	private void OnPostFill(UIWidget widget, int bufferOffset, List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (verts == null)
		{
			return;
		}
		int count = verts.Count;
		if (verts == null || count == 0)
		{
			return;
		}
		if (this.mLabel == null)
		{
			return;
		}
		try
		{
			int quadsPerCharacter = this.mLabel.quadsPerCharacter;
			int num = count / quadsPerCharacter / 4;
			string printedText = this.mLabel.printedText;
			if (this.mVertexCount != count)
			{
				this.mVertexCount = count;
				this.SetLetterOrder(num);
				this.GetLetterDuration(num);
			}
			Matrix4x4 identity = Matrix4x4.identity;
			Vector3 pos = Vector3.zero;
			Quaternion q = Quaternion.identity;
			Vector3 s = Vector3.one;
			Vector3 b = Vector3.zero;
			Quaternion a = Quaternion.Euler(this.mCurrent.rot);
			Vector3 vector = Vector3.zero;
			Color value = Color.clear;
			float num2 = base.tweenFactor * this.duration;
			for (int i = 0; i < quadsPerCharacter; i++)
			{
				for (int j = 0; j < num; j++)
				{
					int num3 = this.mLetterOrder[j];
					int num4 = i * num * 4 + num3 * 4;
					if (num4 < count)
					{
						float start = this.mLetter[num3].start;
						float num5 = Mathf.Clamp(num2 - start, 0f, this.mLetter[num3].duration) / this.mLetter[num3].duration;
						num5 = this.animationCurve.Evaluate(num5);
						b = TweenLetters.GetCenter(verts, num4, 4);
						Vector2 offset = this.mLetter[num3].offset;
						pos = Vector3.LerpUnclamped(this.mCurrent.pos + new Vector3(offset.x, offset.y, 0f), Vector3.zero, num5);
						q = Quaternion.SlerpUnclamped(a, Quaternion.identity, num5);
						s = Vector3.LerpUnclamped(this.mCurrent.scale, Vector3.one, num5);
						float a2 = Mathf.LerpUnclamped(this.mCurrent.alpha, 1f, num5);
						identity.SetTRS(pos, q, s);
						for (int k = num4; k < num4 + 4; k++)
						{
							vector = verts[k];
							vector -= b;
							vector = identity.MultiplyPoint3x4(vector);
							vector += b;
							verts[k] = vector;
							value = cols[k];
							value.a = a2;
							cols[k] = value;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0008C5EC File Offset: 0x0008A9EC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.mLabel.MarkAsChanged();
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0008C5FC File Offset: 0x0008A9FC
	private void SetLetterOrder(int letterCount)
	{
		if (letterCount == 0)
		{
			this.mLetter = null;
			this.mLetterOrder = null;
			return;
		}
		this.mLetterOrder = new int[letterCount];
		this.mLetter = new TweenLetters.LetterProperties[letterCount];
		for (int i = 0; i < letterCount; i++)
		{
			this.mLetterOrder[i] = ((this.mCurrent.animationOrder != TweenLetters.AnimationLetterOrder.Reverse) ? i : (letterCount - 1 - i));
			int num = this.mLetterOrder[i];
			this.mLetter[num] = new TweenLetters.LetterProperties();
			this.mLetter[num].offset = new Vector2(UnityEngine.Random.Range(-this.mCurrent.offsetRange.x, this.mCurrent.offsetRange.x), UnityEngine.Random.Range(-this.mCurrent.offsetRange.y, this.mCurrent.offsetRange.y));
		}
		if (this.mCurrent.animationOrder == TweenLetters.AnimationLetterOrder.Random)
		{
			System.Random random = new System.Random();
			int j = letterCount;
			while (j > 1)
			{
				int num2 = random.Next(--j + 1);
				int num3 = this.mLetterOrder[num2];
				this.mLetterOrder[num2] = this.mLetterOrder[j];
				this.mLetterOrder[j] = num3;
			}
		}
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0008C73C File Offset: 0x0008AB3C
	private void GetLetterDuration(int letterCount)
	{
		if (this.mCurrent.randomDurations)
		{
			for (int i = 0; i < this.mLetter.Length; i++)
			{
				this.mLetter[i].start = UnityEngine.Random.Range(0f, this.mCurrent.randomness.x * this.duration);
				float num = UnityEngine.Random.Range(this.mCurrent.randomness.y * this.duration, this.duration);
				this.mLetter[i].duration = num - this.mLetter[i].start;
			}
		}
		else
		{
			float num2 = this.duration / (float)letterCount;
			float num3 = 1f - this.mCurrent.overlap;
			float num4 = num2 * (float)letterCount * num3;
			float duration = this.ScaleRange(num2, num4 + num2 * this.mCurrent.overlap, this.duration);
			float num5 = 0f;
			for (int j = 0; j < this.mLetter.Length; j++)
			{
				int num6 = this.mLetterOrder[j];
				this.mLetter[num6].start = num5;
				this.mLetter[num6].duration = duration;
				num5 += this.mLetter[num6].duration * num3;
			}
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0008C88B File Offset: 0x0008AC8B
	private float ScaleRange(float value, float baseMax, float limitMax)
	{
		return limitMax * value / baseMax;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0008C894 File Offset: 0x0008AC94
	private static Vector3 GetCenter(List<Vector3> verts, int firstVert, int length)
	{
		Vector3 a = verts[firstVert];
		for (int i = firstVert + 1; i < firstVert + length; i++)
		{
			a += verts[i];
		}
		return a / (float)length;
	}

	// Token: 0x04000F0D RID: 3853
	public TweenLetters.AnimationProperties hoverOver;

	// Token: 0x04000F0E RID: 3854
	public TweenLetters.AnimationProperties hoverOut;

	// Token: 0x04000F0F RID: 3855
	private UILabel mLabel;

	// Token: 0x04000F10 RID: 3856
	private int mVertexCount = -1;

	// Token: 0x04000F11 RID: 3857
	private int[] mLetterOrder;

	// Token: 0x04000F12 RID: 3858
	private TweenLetters.LetterProperties[] mLetter;

	// Token: 0x04000F13 RID: 3859
	private TweenLetters.AnimationProperties mCurrent;

	// Token: 0x02000237 RID: 567
	public enum AnimationLetterOrder
	{
		// Token: 0x04000F15 RID: 3861
		Forward,
		// Token: 0x04000F16 RID: 3862
		Reverse,
		// Token: 0x04000F17 RID: 3863
		Random
	}

	// Token: 0x02000238 RID: 568
	private class LetterProperties
	{
		// Token: 0x04000F18 RID: 3864
		public float start;

		// Token: 0x04000F19 RID: 3865
		public float duration;

		// Token: 0x04000F1A RID: 3866
		public Vector2 offset;
	}

	// Token: 0x02000239 RID: 569
	[Serializable]
	public class AnimationProperties
	{
		// Token: 0x04000F1B RID: 3867
		public TweenLetters.AnimationLetterOrder animationOrder = TweenLetters.AnimationLetterOrder.Random;

		// Token: 0x04000F1C RID: 3868
		[Range(0f, 1f)]
		public float overlap = 0.5f;

		// Token: 0x04000F1D RID: 3869
		public bool randomDurations;

		// Token: 0x04000F1E RID: 3870
		[MinMaxRange(0f, 1f)]
		public Vector2 randomness = new Vector2(0.25f, 0.75f);

		// Token: 0x04000F1F RID: 3871
		public Vector2 offsetRange = Vector2.zero;

		// Token: 0x04000F20 RID: 3872
		public Vector3 pos = Vector3.zero;

		// Token: 0x04000F21 RID: 3873
		public Vector3 rot = Vector3.zero;

		// Token: 0x04000F22 RID: 3874
		public Vector3 scale = Vector3.one;

		// Token: 0x04000F23 RID: 3875
		public float alpha = 1f;
	}
}
