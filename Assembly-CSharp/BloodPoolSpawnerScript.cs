using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200033A RID: 826
public class BloodPoolSpawnerScript : MonoBehaviour
{
	// Token: 0x06001754 RID: 5972 RVA: 0x000B7F74 File Offset: 0x000B6374
	public void Start()
	{
		if (SceneManager.GetActiveScene().name == "SchoolScene")
		{
			this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
		}
		this.BloodParent = GameObject.Find("BloodParent").transform;
		this.Positions = new Vector3[5];
		this.Positions[0] = Vector3.zero;
		this.Positions[1] = new Vector3(0.5f, 0.012f, 0f);
		this.Positions[2] = new Vector3(-0.5f, 0.012f, 0f);
		this.Positions[3] = new Vector3(0f, 0.012f, 0.5f);
		this.Positions[4] = new Vector3(0f, 0.012f, -0.5f);
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000B80D1 File Offset: 0x000B64D1
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.LastBloodPool = other.gameObject;
			this.NearbyBlood++;
		}
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000B8107 File Offset: 0x000B6507
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.NearbyBlood--;
		}
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000B8134 File Offset: 0x000B6534
	private void Update()
	{
		if (this.MyCollider.enabled)
		{
			if (this.Timer > 0f)
			{
				this.Timer -= Time.deltaTime;
			}
			this.SetHeight();
			Vector3 position = base.transform.position;
			if (SceneManager.GetActiveScene().name == "SchoolScene")
			{
				this.CanSpawn = (!this.GardenArea.bounds.Contains(position) && !this.NEStairs.bounds.Contains(position) && !this.NWStairs.bounds.Contains(position) && !this.SEStairs.bounds.Contains(position) && !this.SWStairs.bounds.Contains(position));
			}
			else
			{
				this.CanSpawn = true;
			}
			if (this.CanSpawn && position.y < this.Height + 0.333333343f)
			{
				if (this.NearbyBlood > 0 && this.LastBloodPool == null)
				{
					this.NearbyBlood--;
				}
				if (this.NearbyBlood < 1 && this.Timer <= 0f)
				{
					this.Timer = 0.1f;
					if (this.PoolsSpawned < 10)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
						gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
						gameObject.transform.parent = this.BloodParent;
						this.PoolsSpawned++;
					}
					else if (this.PoolsSpawned < 20)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
						gameObject2.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
						gameObject2.transform.parent = this.BloodParent;
						this.PoolsSpawned++;
						gameObject2.GetComponent<BloodPoolScript>().TargetSize = 1f - (float)(this.PoolsSpawned - 10) * 0.1f;
						if (this.PoolsSpawned == 20)
						{
							base.gameObject.SetActive(false);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000B83FC File Offset: 0x000B67FC
	public void SpawnBigPool()
	{
		this.SetHeight();
		Vector3 a = new Vector3(this.Hips.position.x, this.Height + 0.012f, this.Hips.position.z);
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, a + this.Positions[i], Quaternion.identity);
			gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
			gameObject.transform.parent = this.BloodParent;
		}
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000B84C0 File Offset: 0x000B68C0
	private void SpawnRow(Transform Location)
	{
		Vector3 position = Location.position;
		Vector3 forward = Location.forward;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2f, Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
		gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2.5f, Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
		gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 3f, Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000B85F0 File Offset: 0x000B69F0
	public void SpawnPool(Transform Location)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, Location.position + Location.forward + new Vector3(0f, 0.0001f, 0f), Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000B8674 File Offset: 0x000B6A74
	private void SetHeight()
	{
		float y = base.transform.position.y;
		if (y < 4f)
		{
			this.Height = 0f;
		}
		else if (y < 8f)
		{
			this.Height = 4f;
		}
		else if (y < 12f)
		{
			this.Height = 8f;
		}
		else
		{
			this.Height = 12f;
		}
	}

	// Token: 0x040016D2 RID: 5842
	public RagdollScript Ragdoll;

	// Token: 0x040016D3 RID: 5843
	public GameObject LastBloodPool;

	// Token: 0x040016D4 RID: 5844
	public GameObject BloodPool;

	// Token: 0x040016D5 RID: 5845
	public Transform BloodParent;

	// Token: 0x040016D6 RID: 5846
	public Transform Hips;

	// Token: 0x040016D7 RID: 5847
	public Collider MyCollider;

	// Token: 0x040016D8 RID: 5848
	public Collider GardenArea;

	// Token: 0x040016D9 RID: 5849
	public Collider NEStairs;

	// Token: 0x040016DA RID: 5850
	public Collider NWStairs;

	// Token: 0x040016DB RID: 5851
	public Collider SEStairs;

	// Token: 0x040016DC RID: 5852
	public Collider SWStairs;

	// Token: 0x040016DD RID: 5853
	public Vector3[] Positions;

	// Token: 0x040016DE RID: 5854
	public bool CanSpawn;

	// Token: 0x040016DF RID: 5855
	public int PoolsSpawned;

	// Token: 0x040016E0 RID: 5856
	public int NearbyBlood;

	// Token: 0x040016E1 RID: 5857
	public float Height;

	// Token: 0x040016E2 RID: 5858
	public float Timer;
}
