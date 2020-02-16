using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200030B RID: 779
	[Serializable]
	public sealed class ColorGradingCurve
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x000ADB40 File Offset: 0x000ABF40
		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000ADB6C File Offset: 0x000ABF6C
		public void Cache()
		{
			if (!this.m_Loop)
			{
				return;
			}
			int length = this.curve.length;
			if (length < 2)
			{
				return;
			}
			if (this.m_InternalLoopingCurve == null)
			{
				this.m_InternalLoopingCurve = new AnimationCurve();
			}
			Keyframe key = this.curve[length - 1];
			key.time -= this.m_Range;
			Keyframe key2 = this.curve[0];
			key2.time += this.m_Range;
			this.m_InternalLoopingCurve.keys = this.curve.keys;
			this.m_InternalLoopingCurve.AddKey(key);
			this.m_InternalLoopingCurve.AddKey(key2);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000ADC24 File Offset: 0x000AC024
		public float Evaluate(float t)
		{
			if (this.curve.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.curve.length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x0400144B RID: 5195
		public AnimationCurve curve;

		// Token: 0x0400144C RID: 5196
		[SerializeField]
		private bool m_Loop;

		// Token: 0x0400144D RID: 5197
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x0400144E RID: 5198
		[SerializeField]
		private float m_Range;

		// Token: 0x0400144F RID: 5199
		private AnimationCurve m_InternalLoopingCurve;
	}
}
