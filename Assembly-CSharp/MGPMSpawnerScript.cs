using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class MGPMSpawnerScript : MonoBehaviour
{
	// Token: 0x06000BDA RID: 3034 RVA: 0x0005B61C File Offset: 0x00059A1C
	private void Start()
	{
		if (this.Wave == 8 || this.Wave == 9)
		{
			this.ID = 1;
			while (this.ID < 100)
			{
				this.SpawnTimers[this.ID] = this.SpawnTimers[this.ID - 1] + 0.1f;
				this.ID++;
			}
			this.ID = 0;
		}
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0005B694 File Offset: 0x00059A94
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.ID < this.SpawnTimers.Length)
		{
			if (this.Timer > this.SpawnTimers[this.ID])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Enemy[this.SpawnEnemies[this.ID]], base.transform.position, Quaternion.identity);
				gameObject.transform.parent = base.transform.parent;
				if (this.SpawnEnemies[this.ID] == 4 || this.SpawnEnemies[this.ID] == 11)
				{
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else if (this.SpawnEnemies[this.ID] == 6 || this.SpawnEnemies[this.ID] == 9 || this.SpawnEnemies[this.ID] == 12)
				{
					gameObject.transform.localScale = new Vector3(128f, 128f, 1f);
				}
				else
				{
					gameObject.transform.localScale = new Vector3(64f, 64f, 1f);
				}
				gameObject.transform.position = this.SpawnPositions[this.ID].position;
				MGPMEnemyScript component = gameObject.GetComponent<MGPMEnemyScript>();
				component.GameplayManager = this.GameplayManager;
				component.Miyuki = this.Miyuki;
				if (this.Wave == 9)
				{
					if (this.ID < 100)
					{
						this.SpawnPositions[this.ID].localPosition = new Vector3(UnityEngine.Random.Range(-100f, 100f), 0f, 0f);
					}
					else if (this.ID == 100)
					{
						this.LoadBearer[1] = gameObject;
					}
					else if (this.ID == 101)
					{
						this.LoadBearer[2] = gameObject;
					}
				}
				this.ID++;
			}
		}
		else if (this.Wave == 9 && this.LoadBearer[1] == null && this.LoadBearer[2] == null)
		{
			this.GameplayManager.Jukebox.volume = Mathf.MoveTowards(this.GameplayManager.Jukebox.volume, 0f, Time.deltaTime * 0.5f);
			if (this.GameplayManager.Jukebox.volume == 0f)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Enemy[this.SpawnEnemies[this.ID]], base.transform.position, Quaternion.identity);
				gameObject2.transform.parent = base.transform.parent;
				gameObject2.transform.localScale = new Vector3(256f, 128f, 1f);
				gameObject2.transform.position = this.SpawnPositions[this.ID].position;
				MGPMEnemyScript component2 = gameObject2.GetComponent<MGPMEnemyScript>();
				component2.GameplayManager = this.GameplayManager;
				component2.HealthBar = this.HealthBar;
				component2.Miyuki = this.Miyuki;
				this.HealthBar.parent.gameObject.SetActive(true);
				this.GameplayManager.Jukebox.clip = this.GameplayManager.FinalBoss;
				this.GameplayManager.Jukebox.volume = 0.5f;
				this.GameplayManager.Jukebox.Play();
				base.enabled = false;
			}
		}
	}

	// Token: 0x04000948 RID: 2376
	public MGPMManagerScript GameplayManager;

	// Token: 0x04000949 RID: 2377
	public MGPMMiyukiScript Miyuki;

	// Token: 0x0400094A RID: 2378
	public Transform[] SpawnPositions;

	// Token: 0x0400094B RID: 2379
	public float[] SpawnTimers;

	// Token: 0x0400094C RID: 2380
	public int[] SpawnEnemies;

	// Token: 0x0400094D RID: 2381
	public GameObject[] LoadBearer;

	// Token: 0x0400094E RID: 2382
	public GameObject[] Enemy;

	// Token: 0x0400094F RID: 2383
	public Transform HealthBar;

	// Token: 0x04000950 RID: 2384
	public float SpawnRate;

	// Token: 0x04000951 RID: 2385
	public float Timer;

	// Token: 0x04000952 RID: 2386
	public bool RandomMode;

	// Token: 0x04000953 RID: 2387
	public int Wave;

	// Token: 0x04000954 RID: 2388
	public int ID;
}
