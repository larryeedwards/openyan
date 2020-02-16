using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200007F RID: 127
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_r_v_o_agent_placer.php")]
	public class RVOAgentPlacer : MonoBehaviour
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0002006C File Offset: 0x0001E46C
		private IEnumerator Start()
		{
			yield return null;
			for (int i = 0; i < this.agents; i++)
			{
				float num = (float)i / (float)this.agents * 3.14159274f * 2f;
				Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * this.ringSize;
				Vector3 target = -vector + this.goalOffset;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, Vector3.zero, Quaternion.Euler(0f, num + 180f, 0f));
				RVOExampleAgent component = gameObject.GetComponent<RVOExampleAgent>();
				if (component == null)
				{
					Debug.LogError("Prefab does not have an RVOExampleAgent component attached");
					yield break;
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = vector;
				component.repathRate = this.repathRate;
				component.SetTarget(target);
				component.SetColor(this.GetColor(num));
			}
			yield break;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00020087 File Offset: 0x0001E487
		public Color GetColor(float angle)
		{
			return AstarMath.HSVToRGB(angle * 57.2957764f, 0.8f, 0.6f);
		}

		// Token: 0x0400036E RID: 878
		public int agents = 100;

		// Token: 0x0400036F RID: 879
		public float ringSize = 100f;

		// Token: 0x04000370 RID: 880
		public LayerMask mask;

		// Token: 0x04000371 RID: 881
		public GameObject prefab;

		// Token: 0x04000372 RID: 882
		public Vector3 goalOffset;

		// Token: 0x04000373 RID: 883
		public float repathRate = 1f;

		// Token: 0x04000374 RID: 884
		private const float rad2Deg = 57.2957764f;
	}
}
