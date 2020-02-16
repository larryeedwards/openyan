using System;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class CountdownScript : MonoBehaviour
{
	// Token: 0x0600184D RID: 6221 RVA: 0x000D4D6B File Offset: 0x000D316B
	private void Update()
	{
		this.Sprite.fillAmount = Mathf.MoveTowards(this.Sprite.fillAmount, 0f, Time.deltaTime * this.Speed);
	}

	// Token: 0x04001AB4 RID: 6836
	public UISprite Sprite;

	// Token: 0x04001AB5 RID: 6837
	public float Speed = 0.05f;

	// Token: 0x04001AB6 RID: 6838
	public bool MaskedPhoto;
}
