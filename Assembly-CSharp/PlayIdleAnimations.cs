using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019C RID: 412
[AddComponentMenu("NGUI/Examples/Play Idle Animations")]
public class PlayIdleAnimations : MonoBehaviour
{
	// Token: 0x06000C78 RID: 3192 RVA: 0x000683B8 File Offset: 0x000667B8
	private void Start()
	{
		this.mAnim = base.GetComponentInChildren<Animation>();
		if (this.mAnim == null)
		{
			Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has no Animation component");
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			IEnumerator enumerator = this.mAnim.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					AnimationState animationState = (AnimationState)obj;
					if (animationState.clip.name == "idle")
					{
						animationState.layer = 0;
						this.mIdle = animationState.clip;
						this.mAnim.Play(this.mIdle.name);
					}
					else if (animationState.clip.name.StartsWith("idle"))
					{
						animationState.layer = 1;
						this.mBreaks.Add(animationState.clip);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			if (this.mBreaks.Count == 0)
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000684EC File Offset: 0x000668EC
	private void Update()
	{
		if (this.mNextBreak < Time.time)
		{
			if (this.mBreaks.Count == 1)
			{
				AnimationClip animationClip = this.mBreaks[0];
				this.mNextBreak = Time.time + animationClip.length + UnityEngine.Random.Range(5f, 15f);
				this.mAnim.CrossFade(animationClip.name);
			}
			else
			{
				int num = UnityEngine.Random.Range(0, this.mBreaks.Count - 1);
				if (this.mLastIndex == num)
				{
					num++;
					if (num >= this.mBreaks.Count)
					{
						num = 0;
					}
				}
				this.mLastIndex = num;
				AnimationClip animationClip2 = this.mBreaks[num];
				this.mNextBreak = Time.time + animationClip2.length + UnityEngine.Random.Range(2f, 8f);
				this.mAnim.CrossFade(animationClip2.name);
			}
		}
	}

	// Token: 0x04000B16 RID: 2838
	private Animation mAnim;

	// Token: 0x04000B17 RID: 2839
	private AnimationClip mIdle;

	// Token: 0x04000B18 RID: 2840
	private List<AnimationClip> mBreaks = new List<AnimationClip>();

	// Token: 0x04000B19 RID: 2841
	private float mNextBreak;

	// Token: 0x04000B1A RID: 2842
	private int mLastIndex;
}
