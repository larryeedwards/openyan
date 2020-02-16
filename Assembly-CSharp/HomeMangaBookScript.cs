using System;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class HomeMangaBookScript : MonoBehaviour
{
	// Token: 0x06001CAD RID: 7341 RVA: 0x00105D88 File Offset: 0x00104188
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00105DD0 File Offset: 0x001041D0
	private void Update()
	{
		float y = (this.Manga.Selected != this.ID) ? 0f : (base.transform.eulerAngles.y + Time.deltaTime * this.RotationSpeed);
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, y, base.transform.eulerAngles.z);
	}

	// Token: 0x040021DC RID: 8668
	public HomeMangaScript Manga;

	// Token: 0x040021DD RID: 8669
	public float RotationSpeed;

	// Token: 0x040021DE RID: 8670
	public int ID;
}
