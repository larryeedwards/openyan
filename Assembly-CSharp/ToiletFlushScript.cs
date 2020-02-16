using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000554 RID: 1364
internal class ToiletFlushScript : MonoBehaviour
{
	// Token: 0x060021AB RID: 8619 RVA: 0x00198696 File Offset: 0x00196A96
	private void Start()
	{
		this.StudentManager = UnityEngine.Object.FindObjectOfType<StudentManagerScript>();
		this.Toilet = this.StudentManager.Students[11].gameObject;
		this.toilet = this.Toilet;
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x001986C8 File Offset: 0x00196AC8
	private void Update()
	{
		this.Flush(this.toilet);
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x001986D8 File Offset: 0x00196AD8
	private void Flush(GameObject toilet)
	{
		if (this.Toilet != null)
		{
			this.Toilet = null;
		}
		if (toilet.activeInHierarchy)
		{
			int length = UnityEngine.Random.Range(1, 15);
			toilet.name = this.RandomSound(length);
			base.name = this.RandomSound(length);
			toilet.SetActive(false);
		}
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x00198732 File Offset: 0x00196B32
	private string RandomSound(int Length)
	{
		return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Length)
		select s[ToiletFlushScript.random.Next(s.Length)]).ToArray<char>());
	}

	// Token: 0x040036A9 RID: 13993
	[Header("=== Toilet Related ===")]
	public GameObject Toilet;

	// Token: 0x040036AA RID: 13994
	private GameObject toilet;

	// Token: 0x040036AB RID: 13995
	private static System.Random random = new System.Random();

	// Token: 0x040036AC RID: 13996
	private StudentManagerScript StudentManager;
}
