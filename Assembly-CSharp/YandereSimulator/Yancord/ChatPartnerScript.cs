using System;
using System.Collections.Generic;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020005C4 RID: 1476
	public class ChatPartnerScript : MonoBehaviour
	{
		// Token: 0x0600236B RID: 9067 RVA: 0x001C0350 File Offset: 0x001BE750
		private void Awake()
		{
			if (this.MyProfile != null)
			{
				if (this.NameLabel != null)
				{
					this.NameLabel.text = this.MyProfile.FirstName + " " + this.MyProfile.LastName;
				}
				if (this.TagLabel != null)
				{
					this.TagLabel.text = this.MyProfile.GetTag(true);
				}
				if (this.ProfilPictureTexture != null)
				{
					this.ProfilPictureTexture.mainTexture = this.MyProfile.ProfilePicture;
				}
				if (this.StatusTexture != null)
				{
					this.StatusTexture.mainTexture = this.GetStatusTexture(this.MyProfile.CurrentStatus);
				}
				base.gameObject.name = this.MyProfile.FirstName + "_Profile";
			}
			else
			{
				Debug.LogError("[ChatPartnerScript] MyProfile wasn't assgined!");
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x001C0460 File Offset: 0x001BE860
		private Texture2D GetStatusTexture(Status currentStatus)
		{
			switch (currentStatus)
			{
			case Status.Online:
				return this.StatusTextures[1];
			case Status.Idle:
				return this.StatusTextures[2];
			case Status.DontDisturb:
				return this.StatusTextures[3];
			case Status.Invisible:
				return this.StatusTextures[4];
			default:
				return null;
			}
		}

		// Token: 0x04003D36 RID: 15670
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003D37 RID: 15671
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003D38 RID: 15672
		public UILabel TagLabel;

		// Token: 0x04003D39 RID: 15673
		public UITexture ProfilPictureTexture;

		// Token: 0x04003D3A RID: 15674
		public UITexture StatusTexture;

		// Token: 0x04003D3B RID: 15675
		[Space(20f)]
		public List<Texture2D> StatusTextures = new List<Texture2D>();
	}
}
