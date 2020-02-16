using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pathfinding.Examples
{
	// Token: 0x0200008B RID: 139
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_manager.php")]
	public class TurnBasedManager : MonoBehaviour
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x00022AA8 File Offset: 0x00020EA8
		private void Awake()
		{
			this.eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00022AB8 File Offset: 0x00020EB8
		private void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (this.eventSystem.IsPointerOverGameObject())
			{
				return;
			}
			if (this.state == TurnBasedManager.State.SelectTarget)
			{
				this.HandleButtonUnderRay(ray);
			}
			if ((this.state == TurnBasedManager.State.SelectUnit || this.state == TurnBasedManager.State.SelectTarget) && Input.GetKeyDown(KeyCode.Mouse0))
			{
				TurnBasedAI byRay = this.GetByRay<TurnBasedAI>(ray);
				if (byRay != null)
				{
					this.Select(byRay);
					this.DestroyPossibleMoves();
					this.GeneratePossibleMoves(this.selected);
					this.state = TurnBasedManager.State.SelectTarget;
				}
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00022B54 File Offset: 0x00020F54
		private void HandleButtonUnderRay(Ray ray)
		{
			Astar3DButton byRay = this.GetByRay<Astar3DButton>(ray);
			if (byRay != null && Input.GetKeyDown(KeyCode.Mouse0))
			{
				byRay.OnClick();
				this.DestroyPossibleMoves();
				this.state = TurnBasedManager.State.Move;
				base.StartCoroutine(this.MoveToNode(this.selected, byRay.node));
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00022BB0 File Offset: 0x00020FB0
		private T GetByRay<T>(Ray ray) where T : class
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, this.layerMask))
			{
				return raycastHit.transform.GetComponentInParent<T>();
			}
			return (T)((object)null);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00022BED File Offset: 0x00020FED
		private void Select(TurnBasedAI unit)
		{
			this.selected = unit;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00022BF8 File Offset: 0x00020FF8
		private IEnumerator MoveToNode(TurnBasedAI unit, GraphNode node)
		{
			ABPath path = ABPath.Construct(unit.transform.position, (Vector3)node.position, null);
			path.traversalProvider = unit.traversalProvider;
			AstarPath.StartPath(path, false);
			yield return base.StartCoroutine(path.WaitForPath());
			if (path.error)
			{
				Debug.LogError("Path failed:\n" + path.errorLog);
				this.state = TurnBasedManager.State.SelectTarget;
				this.GeneratePossibleMoves(this.selected);
				yield break;
			}
			unit.targetNode = path.path[path.path.Count - 1];
			yield return base.StartCoroutine(TurnBasedManager.MoveAlongPath(unit, path, this.movementSpeed));
			unit.blocker.BlockAtCurrentPosition();
			this.state = TurnBasedManager.State.SelectUnit;
			yield break;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00022C24 File Offset: 0x00021024
		private static IEnumerator MoveAlongPath(TurnBasedAI unit, ABPath path, float speed)
		{
			if (path.error || path.vectorPath.Count == 0)
			{
				throw new ArgumentException("Cannot follow an empty path");
			}
			float distanceAlongSegment = 0f;
			for (int i = 0; i < path.vectorPath.Count - 1; i++)
			{
				Vector3 p0 = path.vectorPath[Mathf.Max(i - 1, 0)];
				Vector3 p = path.vectorPath[i];
				Vector3 p2 = path.vectorPath[i + 1];
				Vector3 p3 = path.vectorPath[Mathf.Min(i + 2, path.vectorPath.Count - 1)];
				float segmentLength = Vector3.Distance(p, p2);
				while (distanceAlongSegment < segmentLength)
				{
					Vector3 interpolatedPoint = AstarSplines.CatmullRom(p0, p, p2, p3, distanceAlongSegment / segmentLength);
					unit.transform.position = interpolatedPoint;
					yield return null;
					distanceAlongSegment += Time.deltaTime * speed;
				}
				distanceAlongSegment -= segmentLength;
			}
			unit.transform.position = path.vectorPath[path.vectorPath.Count - 1];
			yield break;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00022C50 File Offset: 0x00021050
		private void DestroyPossibleMoves()
		{
			foreach (GameObject obj in this.possibleMoves)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.possibleMoves.Clear();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00022CB8 File Offset: 0x000210B8
		private void GeneratePossibleMoves(TurnBasedAI unit)
		{
			ConstantPath constantPath = ConstantPath.Construct(unit.transform.position, unit.movementPoints * 1000 + 1, null);
			constantPath.traversalProvider = unit.traversalProvider;
			AstarPath.StartPath(constantPath, false);
			constantPath.BlockUntilCalculated();
			foreach (GraphNode graphNode in constantPath.allNodes)
			{
				if (graphNode != constantPath.startNode)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nodePrefab, (Vector3)graphNode.position, Quaternion.identity);
					this.possibleMoves.Add(gameObject);
					gameObject.GetComponent<Astar3DButton>().node = graphNode;
				}
			}
		}

		// Token: 0x040003AE RID: 942
		private TurnBasedAI selected;

		// Token: 0x040003AF RID: 943
		public float movementSpeed;

		// Token: 0x040003B0 RID: 944
		public GameObject nodePrefab;

		// Token: 0x040003B1 RID: 945
		public LayerMask layerMask;

		// Token: 0x040003B2 RID: 946
		private List<GameObject> possibleMoves = new List<GameObject>();

		// Token: 0x040003B3 RID: 947
		private EventSystem eventSystem;

		// Token: 0x040003B4 RID: 948
		public TurnBasedManager.State state;

		// Token: 0x0200008C RID: 140
		public enum State
		{
			// Token: 0x040003B6 RID: 950
			SelectUnit,
			// Token: 0x040003B7 RID: 951
			SelectTarget,
			// Token: 0x040003B8 RID: 952
			Move
		}
	}
}
