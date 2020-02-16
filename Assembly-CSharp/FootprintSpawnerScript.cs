using System;
using UnityEngine;

// Token: 0x020003D9 RID: 985
public class FootprintSpawnerScript : MonoBehaviour
{
	// Token: 0x060019AD RID: 6573 RVA: 0x000F01A8 File Offset: 0x000EE5A8
	private void Start()
	{
		this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
		this.PoolStairs = GameObject.Find("PoolStairs").GetComponent<Collider>();
		this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
		this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
		this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
		this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000F0234 File Offset: 0x000EE634
	private void Update()
	{
		if (this.Debugging)
		{
			Debug.Log(string.Concat(new string[]
			{
				"UpThreshold: ",
				(this.Yandere.transform.position.y + this.UpThreshold).ToString(),
				" | DownThreshold: ",
				(this.Yandere.transform.position.y + this.DownThreshold).ToString(),
				" | CurrentHeight: ",
				base.transform.position.y.ToString()
			}));
		}
		this.CanSpawn = (!this.GardenArea.bounds.Contains(base.transform.position) && !this.PoolStairs.bounds.Contains(base.transform.position) && !this.NEStairs.bounds.Contains(base.transform.position) && !this.NWStairs.bounds.Contains(base.transform.position) && !this.SEStairs.bounds.Contains(base.transform.position) && !this.SWStairs.bounds.Contains(base.transform.position));
		if (!this.FootUp)
		{
			if (base.transform.position.y > this.Yandere.transform.position.y + this.UpThreshold)
			{
				this.FootUp = true;
			}
		}
		else if (base.transform.position.y < this.Yandere.transform.position.y + this.DownThreshold)
		{
			if (this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && this.Yandere.CanMove && !this.Yandere.NearSenpai && this.FootUp)
			{
				AudioSource component = base.GetComponent<AudioSource>();
				if (this.Yandere.Running)
				{
					component.clip = this.RunFootsteps[UnityEngine.Random.Range(0, this.RunFootsteps.Length)];
					component.volume = 0.2f;
					component.Play();
				}
				else
				{
					component.clip = this.WalkFootsteps[UnityEngine.Random.Range(0, this.WalkFootsteps.Length)];
					component.volume = 0.1f;
					component.Play();
				}
			}
			this.FootUp = false;
			if (this.CanSpawn && this.Bloodiness > 0)
			{
				if (base.transform.position.y > -1f && base.transform.position.y < 1f)
				{
					this.Height = 0f;
				}
				else if (base.transform.position.y > 3f && base.transform.position.y < 5f)
				{
					this.Height = 4f;
				}
				else if (base.transform.position.y > 7f && base.transform.position.y < 9f)
				{
					this.Height = 8f;
				}
				else if (base.transform.position.y > 11f && base.transform.position.y < 13f)
				{
					this.Height = 12f;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodyFootprint, new Vector3(base.transform.position.x, this.Height + 0.012f, base.transform.position.z), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, base.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
				gameObject.transform.GetChild(0).GetComponent<FootprintScript>().Yandere = this.Yandere;
				gameObject.transform.parent = this.BloodParent;
				this.Bloodiness--;
			}
		}
	}

	// Token: 0x04001EC0 RID: 7872
	public YandereScript Yandere;

	// Token: 0x04001EC1 RID: 7873
	public GameObject BloodyFootprint;

	// Token: 0x04001EC2 RID: 7874
	public AudioClip[] WalkFootsteps;

	// Token: 0x04001EC3 RID: 7875
	public AudioClip[] RunFootsteps;

	// Token: 0x04001EC4 RID: 7876
	public Transform BloodParent;

	// Token: 0x04001EC5 RID: 7877
	public Collider GardenArea;

	// Token: 0x04001EC6 RID: 7878
	public Collider PoolStairs;

	// Token: 0x04001EC7 RID: 7879
	public Collider NEStairs;

	// Token: 0x04001EC8 RID: 7880
	public Collider NWStairs;

	// Token: 0x04001EC9 RID: 7881
	public Collider SEStairs;

	// Token: 0x04001ECA RID: 7882
	public Collider SWStairs;

	// Token: 0x04001ECB RID: 7883
	public bool Debugging;

	// Token: 0x04001ECC RID: 7884
	public bool CanSpawn;

	// Token: 0x04001ECD RID: 7885
	public bool FootUp;

	// Token: 0x04001ECE RID: 7886
	public float DownThreshold;

	// Token: 0x04001ECF RID: 7887
	public float UpThreshold;

	// Token: 0x04001ED0 RID: 7888
	public float Height;

	// Token: 0x04001ED1 RID: 7889
	public int Bloodiness;

	// Token: 0x04001ED2 RID: 7890
	public int Collisions;
}
