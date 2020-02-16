using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020005C8 RID: 1480
	[CreateAssetMenu(fileName = "ChatProfile", menuName = "Yancord/Profile", order = 1)]
	public class Profile : ScriptableObject
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x001C05AC File Offset: 0x001BE9AC
		public string GetTag(bool WithHashtag)
		{
			string text = this.Tag;
			if (text.Length > 4)
			{
				text = text.Substring(0, 4);
			}
			return (!WithHashtag) ? text : ("#" + text);
		}

		// Token: 0x04003D4A RID: 15690
		[Header("Personal Information")]
		public string FirstName;

		// Token: 0x04003D4B RID: 15691
		public string LastName;

		// Token: 0x04003D4C RID: 15692
		[Space(20f)]
		[Header("Profile Information")]
		public Texture2D ProfilePicture;

		// Token: 0x04003D4D RID: 15693
		public string Tag = "XXXX";

		// Token: 0x04003D4E RID: 15694
		[Space(20f)]
		[Header("Profile Settings")]
		public Status CurrentStatus;
	}
}
