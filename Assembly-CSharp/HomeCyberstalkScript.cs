using System;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class HomeCyberstalkScript : MonoBehaviour
{
	// Token: 0x06001C9C RID: 7324 RVA: 0x001031A0 File Offset: 0x001015A0
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
			this.HomeDarkness.Cyberstalking = true;
			this.HomeDarkness.FadeOut = true;
			base.gameObject.SetActive(false);
			for (int i = 1; i < 26; i++)
			{
				ConversationGlobals.SetTopicLearnedByStudent(i, this.HomeDarkness.HomeCamera.HomeInternet.Student, true);
				ConversationGlobals.SetTopicDiscovered(i, true);
			}
		}
		if (Input.GetButtonDown("B"))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400218A RID: 8586
	public HomeDarknessScript HomeDarkness;
}
