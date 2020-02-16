using System;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
public class YanvaniaZombieSpawnerScript : MonoBehaviour
{
	// Token: 0x06002348 RID: 9032 RVA: 0x001BE8B8 File Offset: 0x001BCCB8
	private void Update()
	{
		if (this.Yanmont.transform.position.y > 0f)
		{
			this.ID = 0;
			this.SpawnTimer += Time.deltaTime;
			if (this.SpawnTimer > 1f)
			{
				while (this.ID < 4)
				{
					if (this.Zombies[this.ID] == null)
					{
						this.SpawnSide = UnityEngine.Random.Range(1, 3);
						if (this.Yanmont.transform.position.x < this.LeftBoundary + 5f)
						{
							this.SpawnSide = 2;
						}
						if (this.Yanmont.transform.position.x > this.RightBoundary - 5f)
						{
							this.SpawnSide = 1;
						}
						if (this.Yanmont.transform.position.x < this.LeftBoundary)
						{
							this.RelativePoint = this.LeftBoundary;
						}
						else if (this.Yanmont.transform.position.x > this.RightBoundary)
						{
							this.RelativePoint = this.RightBoundary;
						}
						else
						{
							this.RelativePoint = this.Yanmont.transform.position.x;
						}
						if (this.SpawnSide == 1)
						{
							this.SpawnPoints[0].x = this.RelativePoint - 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint - 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint - 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint - 5.5f;
						}
						else
						{
							this.SpawnPoints[0].x = this.RelativePoint + 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint + 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint + 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint + 5.5f;
						}
						this.Zombies[this.ID] = UnityEngine.Object.Instantiate<GameObject>(this.Zombie, this.SpawnPoints[this.ID], Quaternion.identity);
						this.NewZombieScript = this.Zombies[this.ID].GetComponent<YanvaniaZombieScript>();
						this.NewZombieScript.LeftBoundary = this.LeftBoundary;
						this.NewZombieScript.RightBoundary = this.RightBoundary;
						this.NewZombieScript.Yanmont = this.Yanmont;
						break;
					}
					this.ID++;
				}
				this.SpawnTimer = 0f;
			}
		}
	}

	// Token: 0x04003D06 RID: 15622
	public YanvaniaZombieScript NewZombieScript;

	// Token: 0x04003D07 RID: 15623
	public GameObject Zombie;

	// Token: 0x04003D08 RID: 15624
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003D09 RID: 15625
	public float SpawnTimer;

	// Token: 0x04003D0A RID: 15626
	public float RelativePoint;

	// Token: 0x04003D0B RID: 15627
	public float RightBoundary;

	// Token: 0x04003D0C RID: 15628
	public float LeftBoundary;

	// Token: 0x04003D0D RID: 15629
	public int SpawnSide;

	// Token: 0x04003D0E RID: 15630
	public int ID;

	// Token: 0x04003D0F RID: 15631
	public GameObject[] Zombies;

	// Token: 0x04003D10 RID: 15632
	public Vector3[] SpawnPoints;
}
