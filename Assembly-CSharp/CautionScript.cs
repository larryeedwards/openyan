using System;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class CautionScript : MonoBehaviour
{
	// Token: 0x060017B9 RID: 6073 RVA: 0x000BD108 File Offset: 0x000BB508
	private void Start()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000BD164 File Offset: 0x000BB564
	private void Update()
	{
		if ((this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious) || this.Yandere.Bloodiness > 0f || this.Yandere.Sanity < 33.3333321f || this.Yandere.NearBodies > 0)
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a + Time.deltaTime);
			if (this.Sprite.color.a > 1f)
			{
				this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 1f);
			}
		}
		else
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a - Time.deltaTime);
			if (this.Sprite.color.a < 0f)
			{
				this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
			}
		}
	}

	// Token: 0x040017BA RID: 6074
	public YandereScript Yandere;

	// Token: 0x040017BB RID: 6075
	public UISprite Sprite;
}
