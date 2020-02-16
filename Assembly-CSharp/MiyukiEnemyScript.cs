using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class MiyukiEnemyScript : MonoBehaviour
{
	// Token: 0x06001DBA RID: 7610 RVA: 0x0011A493 File Offset: 0x00118893
	private void Start()
	{
		base.transform.position = this.SpawnPoints[this.ID].position;
		base.transform.rotation = this.SpawnPoints[this.ID].rotation;
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x0011A4D0 File Offset: 0x001188D0
	private void Update()
	{
		if (this.Enemy.activeInHierarchy)
		{
			if (!this.Down)
			{
				this.Float += Time.deltaTime * this.Speed;
				if (this.Float > this.Limit)
				{
					this.Down = true;
				}
			}
			else
			{
				this.Float -= Time.deltaTime * this.Speed;
				if (this.Float < -1f * this.Limit)
				{
					this.Down = false;
				}
			}
			this.Enemy.transform.position += new Vector3(0f, this.Float * Time.deltaTime, 0f);
			if (this.Enemy.transform.position.y > this.SpawnPoints[this.ID].position.y + 1.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 1.5f, this.Enemy.transform.position.z);
			}
			if (this.Enemy.transform.position.y < this.SpawnPoints[this.ID].position.y + 0.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 0.5f, this.Enemy.transform.position.z);
			}
		}
		else
		{
			this.RespawnTimer += Time.deltaTime;
			if (this.RespawnTimer > 5f)
			{
				base.transform.position = this.SpawnPoints[this.ID].position;
				base.transform.rotation = this.SpawnPoints[this.ID].rotation;
				this.Enemy.SetActive(true);
				this.RespawnTimer = 0f;
			}
		}
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x0011A760 File Offset: 0x00118B60
	private void OnTriggerEnter(Collider other)
	{
		if (this.Enemy.activeInHierarchy && other.gameObject.tag == "missile")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(other.gameObject);
			this.Health -= 1f;
			if (this.Health == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, other.transform.position, Quaternion.identity);
				this.Enemy.SetActive(false);
				this.Health = 50f;
				this.ID++;
				if (this.ID >= this.SpawnPoints.Length)
				{
					this.ID = 0;
				}
			}
		}
	}

	// Token: 0x04002555 RID: 9557
	public float Float;

	// Token: 0x04002556 RID: 9558
	public float Limit;

	// Token: 0x04002557 RID: 9559
	public float Speed;

	// Token: 0x04002558 RID: 9560
	public bool Dead;

	// Token: 0x04002559 RID: 9561
	public bool Down;

	// Token: 0x0400255A RID: 9562
	public GameObject DeathEffect;

	// Token: 0x0400255B RID: 9563
	public GameObject HitEffect;

	// Token: 0x0400255C RID: 9564
	public GameObject Enemy;

	// Token: 0x0400255D RID: 9565
	public Transform[] SpawnPoints;

	// Token: 0x0400255E RID: 9566
	public float RespawnTimer;

	// Token: 0x0400255F RID: 9567
	public float Health;

	// Token: 0x04002560 RID: 9568
	public int ID;
}
