using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000141 RID: 321
	public class CustomerSpawner : MonoBehaviour
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x00053FF9 File Offset: 0x000523F9
		private void Start()
		{
			this.spawnRate = GameController.Instance.activeDifficultyVariables.customerSpawnRate;
			this.spawnVariance = GameController.Instance.activeDifficultyVariables.customerSpawnVariance;
			this.isPaused = true;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0005402C File Offset: 0x0005242C
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0005404E File Offset: 0x0005244E
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00054070 File Offset: 0x00052470
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0005407C File Offset: 0x0005247C
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			if (this.timeTillSpawn <= 0f)
			{
				this.timeTillSpawn = this.spawnRate + UnityEngine.Random.Range(-this.spawnVariance, this.spawnVariance);
				this.SpawnCustomer();
			}
			else
			{
				this.timeTillSpawn -= Time.deltaTime;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000540E4 File Offset: 0x000524E4
		private void SpawnCustomer()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.customerPrefabs[UnityEngine.Random.Range(0, this.customerPrefabs.Length)]);
			gameObject.transform.position = base.transform.position;
			AIController component = gameObject.GetComponent<AIController>();
			component.Init();
			component.leaveTarget = base.transform;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0005413B File Offset: 0x0005253B
		public void OpenDoor()
		{
			base.transform.parent.GetComponent<Animator>().SetTrigger("DoorOpen");
		}

		// Token: 0x040007E3 RID: 2019
		public GameObject[] customerPrefabs;

		// Token: 0x040007E4 RID: 2020
		private float spawnRate = 10f;

		// Token: 0x040007E5 RID: 2021
		private float spawnVariance = 5f;

		// Token: 0x040007E6 RID: 2022
		private float timeTillSpawn;

		// Token: 0x040007E7 RID: 2023
		private int spawnedCustomers;

		// Token: 0x040007E8 RID: 2024
		private bool isPaused;
	}
}
