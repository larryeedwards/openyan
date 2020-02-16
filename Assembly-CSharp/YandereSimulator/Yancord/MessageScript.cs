using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020005C6 RID: 1478
	public class MessageScript : MonoBehaviour
	{
		// Token: 0x06002370 RID: 9072 RVA: 0x001C04F0 File Offset: 0x001BE8F0
		public void Awake()
		{
			if (this.MyProfile != null)
			{
				if (this.NameLabel != null)
				{
					this.NameLabel.text = this.MyProfile.FirstName + " " + this.MyProfile.LastName;
				}
				if (this.ProfilPictureTexture != null)
				{
					this.ProfilPictureTexture.mainTexture = this.MyProfile.ProfilePicture;
				}
				base.gameObject.name = this.MyProfile.FirstName + "_Message";
			}
		}

		// Token: 0x04003D3C RID: 15676
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003D3D RID: 15677
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003D3E RID: 15678
		public UILabel MessageLabel;

		// Token: 0x04003D3F RID: 15679
		public UITexture ProfilPictureTexture;
	}
}
