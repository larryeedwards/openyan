using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class YanvaniaDoubleFireballScript : MonoBehaviour
{
	// Token: 0x06002307 RID: 8967 RVA: 0x001B7CC4 File Offset: 0x001B60C4
	private void Start()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, new Vector3(base.transform.position.x, 8f, 0f), Quaternion.identity);
		this.Direction = ((this.Dracula.position.x <= base.transform.position.x) ? 1 : -1);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x001B7D3C File Offset: 0x001B613C
	private void Update()
	{
		if (this.Timer > 1f && !this.SpawnedFirst)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, new Vector3(base.transform.position.x, 7f, 0f), Quaternion.identity);
			this.FirstLavaball = UnityEngine.Object.Instantiate<GameObject>(this.Lavaball, new Vector3(base.transform.position.x, 8f, 0f), Quaternion.identity);
			this.FirstLavaball.transform.localScale = Vector3.zero;
			this.SpawnedFirst = true;
		}
		if (this.FirstLavaball != null)
		{
			this.FirstLavaball.transform.localScale = Vector3.Lerp(this.FirstLavaball.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.FirstPosition += ((this.FirstPosition != 0f) ? (this.FirstPosition * this.Speed) : Time.deltaTime);
			this.FirstLavaball.transform.position = new Vector3(this.FirstLavaball.transform.position.x + this.FirstPosition * (float)this.Direction, this.FirstLavaball.transform.position.y, this.FirstLavaball.transform.position.z);
			this.FirstLavaball.transform.eulerAngles = new Vector3(this.FirstLavaball.transform.eulerAngles.x, this.FirstLavaball.transform.eulerAngles.y, this.FirstLavaball.transform.eulerAngles.z - this.FirstPosition * (float)this.Direction * 36f);
		}
		if (this.Timer > 2f && !this.SpawnedSecond)
		{
			this.SecondLavaball = UnityEngine.Object.Instantiate<GameObject>(this.Lavaball, new Vector3(base.transform.position.x, 7f, 0f), Quaternion.identity);
			this.SecondLavaball.transform.localScale = Vector3.zero;
			this.SpawnedSecond = true;
		}
		if (this.SecondLavaball != null)
		{
			this.SecondLavaball.transform.localScale = Vector3.Lerp(this.SecondLavaball.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.SecondPosition == 0f)
			{
				this.SecondPosition += Time.deltaTime;
			}
			else
			{
				this.SecondPosition += this.SecondPosition * this.Speed;
			}
			this.SecondLavaball.transform.position = new Vector3(this.SecondLavaball.transform.position.x + this.SecondPosition * (float)this.Direction, this.SecondLavaball.transform.position.y, this.SecondLavaball.transform.position.z);
			this.SecondLavaball.transform.eulerAngles = new Vector3(this.SecondLavaball.transform.eulerAngles.x, this.SecondLavaball.transform.eulerAngles.y, this.SecondLavaball.transform.eulerAngles.z - this.SecondPosition * (float)this.Direction * 36f);
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 10f)
		{
			if (this.FirstLavaball != null)
			{
				UnityEngine.Object.Destroy(this.FirstLavaball);
			}
			if (this.SecondLavaball != null)
			{
				UnityEngine.Object.Destroy(this.SecondLavaball);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04003BFB RID: 15355
	public GameObject Lavaball;

	// Token: 0x04003BFC RID: 15356
	public GameObject FirstLavaball;

	// Token: 0x04003BFD RID: 15357
	public GameObject SecondLavaball;

	// Token: 0x04003BFE RID: 15358
	public GameObject LightningEffect;

	// Token: 0x04003BFF RID: 15359
	public Transform Dracula;

	// Token: 0x04003C00 RID: 15360
	public bool SpawnedFirst;

	// Token: 0x04003C01 RID: 15361
	public bool SpawnedSecond;

	// Token: 0x04003C02 RID: 15362
	public float FirstPosition;

	// Token: 0x04003C03 RID: 15363
	public float SecondPosition;

	// Token: 0x04003C04 RID: 15364
	public int Direction;

	// Token: 0x04003C05 RID: 15365
	public float Timer;

	// Token: 0x04003C06 RID: 15366
	public float Speed;
}
