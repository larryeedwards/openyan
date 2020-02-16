using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000082 RID: 130
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_procedural_world.php")]
	public class ProceduralWorld : MonoBehaviour
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00021636 File Offset: 0x0001FA36
		private void Start()
		{
			this.Update();
			AstarPath.active.Scan(null);
			base.StartCoroutine(this.GenerateTiles());
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00021658 File Offset: 0x0001FA58
		private void Update()
		{
			Int2 @int = new Int2(Mathf.RoundToInt((this.target.position.x - this.tileSize * 0.5f) / this.tileSize), Mathf.RoundToInt((this.target.position.z - this.tileSize * 0.5f) / this.tileSize));
			this.range = ((this.range >= 1) ? this.range : 1);
			bool flag = true;
			while (flag)
			{
				flag = false;
				foreach (KeyValuePair<Int2, ProceduralWorld.ProceduralTile> keyValuePair in this.tiles)
				{
					if (Mathf.Abs(keyValuePair.Key.x - @int.x) > this.range || Mathf.Abs(keyValuePair.Key.y - @int.y) > this.range)
					{
						keyValuePair.Value.Destroy();
						this.tiles.Remove(keyValuePair.Key);
						flag = true;
						break;
					}
				}
			}
			for (int i = @int.x - this.range; i <= @int.x + this.range; i++)
			{
				for (int j = @int.y - this.range; j <= @int.y + this.range; j++)
				{
					if (!this.tiles.ContainsKey(new Int2(i, j)))
					{
						ProceduralWorld.ProceduralTile proceduralTile = new ProceduralWorld.ProceduralTile(this, i, j);
						IEnumerator enumerator2 = proceduralTile.Generate();
						enumerator2.MoveNext();
						this.tileGenerationQueue.Enqueue(enumerator2);
						this.tiles.Add(new Int2(i, j), proceduralTile);
					}
				}
			}
			for (int k = @int.x - 1; k <= @int.x + 1; k++)
			{
				for (int l = @int.y - 1; l <= @int.y + 1; l++)
				{
					this.tiles[new Int2(k, l)].ForceFinish();
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000218D0 File Offset: 0x0001FCD0
		private IEnumerator GenerateTiles()
		{
			for (;;)
			{
				if (this.tileGenerationQueue.Count > 0)
				{
					IEnumerator generator = this.tileGenerationQueue.Dequeue();
					yield return base.StartCoroutine(generator);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x04000389 RID: 905
		public Transform target;

		// Token: 0x0400038A RID: 906
		public ProceduralWorld.ProceduralPrefab[] prefabs;

		// Token: 0x0400038B RID: 907
		public int range = 1;

		// Token: 0x0400038C RID: 908
		public float tileSize = 100f;

		// Token: 0x0400038D RID: 909
		public int subTiles = 20;

		// Token: 0x0400038E RID: 910
		public bool staticBatching;

		// Token: 0x0400038F RID: 911
		private Queue<IEnumerator> tileGenerationQueue = new Queue<IEnumerator>();

		// Token: 0x04000390 RID: 912
		private Dictionary<Int2, ProceduralWorld.ProceduralTile> tiles = new Dictionary<Int2, ProceduralWorld.ProceduralTile>();

		// Token: 0x02000083 RID: 131
		[Serializable]
		public class ProceduralPrefab
		{
			// Token: 0x04000391 RID: 913
			public GameObject prefab;

			// Token: 0x04000392 RID: 914
			public float density;

			// Token: 0x04000393 RID: 915
			public float perlin;

			// Token: 0x04000394 RID: 916
			public float perlinPower = 1f;

			// Token: 0x04000395 RID: 917
			public Vector2 perlinOffset = Vector2.zero;

			// Token: 0x04000396 RID: 918
			public float perlinScale = 1f;

			// Token: 0x04000397 RID: 919
			public float random = 1f;

			// Token: 0x04000398 RID: 920
			public bool singleFixed;
		}

		// Token: 0x02000084 RID: 132
		private class ProceduralTile
		{
			// Token: 0x0600057A RID: 1402 RVA: 0x0002191F File Offset: 0x0001FD1F
			public ProceduralTile(ProceduralWorld world, int x, int z)
			{
				this.x = x;
				this.z = z;
				this.world = world;
				this.rnd = new System.Random(x * 10007 ^ z * 36007);
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x00021956 File Offset: 0x0001FD56
			// (set) Token: 0x0600057C RID: 1404 RVA: 0x0002195E File Offset: 0x0001FD5E
			public bool destroyed { get; private set; }

			// Token: 0x0600057D RID: 1405 RVA: 0x00021968 File Offset: 0x0001FD68
			public IEnumerator Generate()
			{
				this.ie = this.InternalGenerate();
				GameObject rt = new GameObject(string.Concat(new object[]
				{
					"Tile ",
					this.x,
					" ",
					this.z
				}));
				this.root = rt.transform;
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
					yield return this.ie.Current;
				}
				this.ie = null;
				yield break;
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x00021983 File Offset: 0x0001FD83
			public void ForceFinish()
			{
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
				}
				this.ie = null;
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x000219C0 File Offset: 0x0001FDC0
			private Vector3 RandomInside()
			{
				return new Vector3
				{
					x = ((float)this.x + (float)this.rnd.NextDouble()) * this.world.tileSize,
					z = ((float)this.z + (float)this.rnd.NextDouble()) * this.world.tileSize
				};
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x00021A24 File Offset: 0x0001FE24
			private Vector3 RandomInside(float px, float pz)
			{
				return new Vector3
				{
					x = (px + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize,
					z = (pz + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize
				};
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00021A96 File Offset: 0x0001FE96
			private Quaternion RandomYRot()
			{
				return Quaternion.Euler(360f * (float)this.rnd.NextDouble(), 0f, 360f * (float)this.rnd.NextDouble());
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00021AC8 File Offset: 0x0001FEC8
			private IEnumerator InternalGenerate()
			{
				Debug.Log(string.Concat(new object[]
				{
					"Generating tile ",
					this.x,
					", ",
					this.z
				}));
				int counter = 0;
				float[,] ditherMap = new float[this.world.subTiles + 2, this.world.subTiles + 2];
				for (int i = 0; i < this.world.prefabs.Length; i++)
				{
					ProceduralWorld.ProceduralPrefab pref = this.world.prefabs[i];
					if (pref.singleFixed)
					{
						Vector3 position = new Vector3(((float)this.x + 0.5f) * this.world.tileSize, 0f, ((float)this.z + 0.5f) * this.world.tileSize);
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(pref.prefab, position, Quaternion.identity);
						gameObject.transform.parent = this.root;
					}
					else
					{
						float subSize = this.world.tileSize / (float)this.world.subTiles;
						for (int k = 0; k < this.world.subTiles; k++)
						{
							for (int l = 0; l < this.world.subTiles; l++)
							{
								ditherMap[k + 1, l + 1] = 0f;
							}
						}
						for (int sx = 0; sx < this.world.subTiles; sx++)
						{
							for (int sz = 0; sz < this.world.subTiles; sz++)
							{
								float px = (float)this.x + (float)sx / (float)this.world.subTiles;
								float pz = (float)this.z + (float)sz / (float)this.world.subTiles;
								float perl = Mathf.Pow(Mathf.PerlinNoise((px + pref.perlinOffset.x) * pref.perlinScale, (pz + pref.perlinOffset.y) * pref.perlinScale), pref.perlinPower);
								float density = pref.density * Mathf.Lerp(1f, perl, pref.perlin) * Mathf.Lerp(1f, (float)this.rnd.NextDouble(), pref.random);
								float fcount = subSize * subSize * density + ditherMap[sx + 1, sz + 1];
								int count = Mathf.RoundToInt(fcount);
								ditherMap[sx + 1 + 1, sz + 1] += 0.4375f * (fcount - (float)count);
								ditherMap[sx + 1 - 1, sz + 1 + 1] += 0.1875f * (fcount - (float)count);
								ditherMap[sx + 1, sz + 1 + 1] += 0.3125f * (fcount - (float)count);
								ditherMap[sx + 1 + 1, sz + 1 + 1] += 0.0625f * (fcount - (float)count);
								for (int j = 0; j < count; j++)
								{
									Vector3 p = this.RandomInside(px, pz);
									GameObject ob = UnityEngine.Object.Instantiate<GameObject>(pref.prefab, p, this.RandomYRot());
									ob.transform.parent = this.root;
									counter++;
									if (counter % 2 == 0)
									{
										yield return null;
									}
								}
							}
						}
					}
				}
				ditherMap = null;
				yield return null;
				yield return null;
				if (Application.HasProLicense() && this.world.staticBatching)
				{
					StaticBatchingUtility.Combine(this.root.gameObject);
				}
				yield break;
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x00021AE4 File Offset: 0x0001FEE4
			public void Destroy()
			{
				if (this.root != null)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Destroying tile ",
						this.x,
						", ",
						this.z
					}));
					UnityEngine.Object.Destroy(this.root.gameObject);
					this.root = null;
				}
				this.ie = null;
			}

			// Token: 0x04000399 RID: 921
			private int x;

			// Token: 0x0400039A RID: 922
			private int z;

			// Token: 0x0400039B RID: 923
			private System.Random rnd;

			// Token: 0x0400039C RID: 924
			private ProceduralWorld world;

			// Token: 0x0400039E RID: 926
			private Transform root;

			// Token: 0x0400039F RID: 927
			private IEnumerator ie;
		}
	}
}
