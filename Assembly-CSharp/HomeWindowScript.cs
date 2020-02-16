using System;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public class HomeWindowScript : MonoBehaviour
{
	// Token: 0x06001CD8 RID: 7384 RVA: 0x0010958C File Offset: 0x0010798C
	private void Start()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x001095E8 File Offset: 0x001079E8
	private void Update()
	{
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, Mathf.Lerp(this.Sprite.color.a, (!this.Show) ? 0f : 1f, Time.deltaTime * 10f));
	}

	// Token: 0x04002296 RID: 8854
	public UISprite Sprite;

	// Token: 0x04002297 RID: 8855
	public bool Show;
}
