using System;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class DontLetSenpaiNoticeYouScript : MonoBehaviour
{
	// Token: 0x06001704 RID: 5892 RVA: 0x000B2114 File Offset: 0x000B0514
	private void Start()
	{
		while (this.ID < this.Letters.Length)
		{
			UILabel uilabel = this.Letters[this.ID];
			uilabel.transform.localScale = new Vector3(10f, 10f, 1f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0f);
			this.Origins[this.ID] = uilabel.transform.localPosition;
			this.ID++;
		}
		this.ID = 0;
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x000B21D8 File Offset: 0x000B05D8
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.Proceed = true;
		}
		if (this.Proceed)
		{
			if (this.ID < this.Letters.Length)
			{
				UILabel uilabel = this.Letters[this.ID];
				uilabel.transform.localScale = Vector3.MoveTowards(uilabel.transform.localScale, Vector3.one, Time.deltaTime * 100f);
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, uilabel.color.a + Time.deltaTime * 10f);
				if (uilabel.transform.localScale == Vector3.one)
				{
					base.GetComponent<AudioSource>().PlayOneShot(this.Slam);
					this.ID++;
				}
			}
			this.ShakeID = 0;
			while (this.ShakeID < this.Letters.Length)
			{
				UILabel uilabel2 = this.Letters[this.ShakeID];
				Vector3 vector = this.Origins[this.ShakeID];
				uilabel2.transform.localPosition = new Vector3(vector.x + UnityEngine.Random.Range(-5f, 5f), vector.y + UnityEngine.Random.Range(-5f, 5f), uilabel2.transform.localPosition.z);
				this.ShakeID++;
			}
		}
	}

	// Token: 0x0400161E RID: 5662
	public UILabel[] Letters;

	// Token: 0x0400161F RID: 5663
	public Vector3[] Origins;

	// Token: 0x04001620 RID: 5664
	public AudioClip Slam;

	// Token: 0x04001621 RID: 5665
	public bool Proceed;

	// Token: 0x04001622 RID: 5666
	public int ShakeID;

	// Token: 0x04001623 RID: 5667
	public int ID;
}
