using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200008F RID: 143
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_path_types_demo.php")]
	public class PathTypesDemo : MonoBehaviour
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x000235F0 File Offset: 0x000219F0
		private void Update()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 vector = ray.origin + ray.direction * (ray.origin.y / -ray.direction.y);
			this.end.position = vector;
			if (Input.GetMouseButtonUp(0))
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					this.multipoints.Add(vector);
				}
				if (Input.GetKey(KeyCode.LeftControl))
				{
					this.multipoints.Clear();
				}
				if (Input.mousePosition.x > 225f)
				{
					this.DemoPath();
				}
			}
			if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt) && (this.lastPath == null || this.lastPath.IsDone()))
			{
				this.DemoPath();
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000236EC File Offset: 0x00021AEC
		public void OnGUI()
		{
			GUILayout.BeginArea(new Rect(5f, 5f, 220f, (float)(Screen.height - 10)), string.Empty, "Box");
			switch (this.activeDemo)
			{
			case PathTypesDemo.DemoMode.ABPath:
				GUILayout.Label("Basic path. Finds a path from point A to point B.", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.MultiTargetPath:
				GUILayout.Label("Multi Target Path. Finds a path quickly from one point to many others in a single search.", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.RandomPath:
				GUILayout.Label("Randomized Path. Finds a path with a specified length in a random direction or biased towards some point when using a larger aim strenggth.", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.FleePath:
				GUILayout.Label("Flee Path. Tries to flee from a specified point. Remember to set Flee Strength!", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.ConstantPath:
				GUILayout.Label("Finds all nodes which it costs less than some value to reach.", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.FloodPath:
				GUILayout.Label("Searches the whole graph from a specific point. FloodPathTracer can then be used to quickly find a path to that point", new GUILayoutOption[0]);
				break;
			case PathTypesDemo.DemoMode.FloodPathTracer:
				GUILayout.Label("Traces a path to where the FloodPath started. Compare the claculation times for this path with ABPath!\nGreat for TD games", new GUILayoutOption[0]);
				break;
			}
			GUILayout.Space(5f);
			GUILayout.Label("Note that the paths are rendered without ANY post-processing applied, so they might look a bit edgy", new GUILayoutOption[0]);
			GUILayout.Space(5f);
			GUILayout.Label("Click anywhere to recalculate the path. Hold Alt to continuously recalculate the path while the mouse is pressed.", new GUILayoutOption[0]);
			if (this.activeDemo == PathTypesDemo.DemoMode.ConstantPath || this.activeDemo == PathTypesDemo.DemoMode.RandomPath || this.activeDemo == PathTypesDemo.DemoMode.FleePath)
			{
				GUILayout.Label("Search Distance (" + this.searchLength + ")", new GUILayoutOption[0]);
				this.searchLength = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.searchLength, 0f, 100000f, new GUILayoutOption[0]));
			}
			if (this.activeDemo == PathTypesDemo.DemoMode.RandomPath || this.activeDemo == PathTypesDemo.DemoMode.FleePath)
			{
				GUILayout.Label("Spread (" + this.spread + ")", new GUILayoutOption[0]);
				this.spread = Mathf.RoundToInt(GUILayout.HorizontalSlider((float)this.spread, 0f, 40000f, new GUILayoutOption[0]));
				GUILayout.Label(string.Concat(new object[]
				{
					(this.activeDemo != PathTypesDemo.DemoMode.RandomPath) ? "Flee strength" : "Aim strength",
					" (",
					this.aimStrength,
					")"
				}), new GUILayoutOption[0]);
				this.aimStrength = GUILayout.HorizontalSlider(this.aimStrength, 0f, 1f, new GUILayoutOption[0]);
			}
			if (this.activeDemo == PathTypesDemo.DemoMode.MultiTargetPath)
			{
				GUILayout.Label("Hold shift and click to add new target points. Hold ctr and click to remove all target points", new GUILayoutOption[0]);
			}
			if (GUILayout.Button("A to B path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.ABPath;
			}
			if (GUILayout.Button("Multi Target Path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.MultiTargetPath;
			}
			if (GUILayout.Button("Random Path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.RandomPath;
			}
			if (GUILayout.Button("Flee path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FleePath;
			}
			if (GUILayout.Button("Constant Path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.ConstantPath;
			}
			if (GUILayout.Button("Flood Path", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FloodPath;
			}
			if (GUILayout.Button("Flood Path Tracer", new GUILayoutOption[0]))
			{
				this.activeDemo = PathTypesDemo.DemoMode.FloodPathTracer;
			}
			GUILayout.EndArea();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00023A4C File Offset: 0x00021E4C
		public void OnPathComplete(Path p)
		{
			if (this.lastRender == null)
			{
				return;
			}
			this.ClearPrevious();
			if (p.error)
			{
				return;
			}
			GameObject gameObject = new GameObject("LineRenderer", new Type[]
			{
				typeof(LineRenderer)
			});
			LineRenderer component = gameObject.GetComponent<LineRenderer>();
			component.sharedMaterial = this.lineMat;
			component.startWidth = this.lineWidth;
			component.endWidth = this.lineWidth;
			component.positionCount = p.vectorPath.Count;
			for (int i = 0; i < p.vectorPath.Count; i++)
			{
				component.SetPosition(i, p.vectorPath[i] + this.pathOffset);
			}
			this.lastRender.Add(gameObject);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00023B18 File Offset: 0x00021F18
		private void ClearPrevious()
		{
			for (int i = 0; i < this.lastRender.Count; i++)
			{
				UnityEngine.Object.Destroy(this.lastRender[i]);
			}
			this.lastRender.Clear();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00023B5D File Offset: 0x00021F5D
		private void OnDestroy()
		{
			this.ClearPrevious();
			this.lastRender = null;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00023B6C File Offset: 0x00021F6C
		private void DemoPath()
		{
			Path path = null;
			switch (this.activeDemo)
			{
			case PathTypesDemo.DemoMode.ABPath:
				path = ABPath.Construct(this.start.position, this.end.position, new OnPathDelegate(this.OnPathComplete));
				break;
			case PathTypesDemo.DemoMode.MultiTargetPath:
				base.StartCoroutine(this.DemoMultiTargetPath());
				break;
			case PathTypesDemo.DemoMode.RandomPath:
			{
				RandomPath randomPath = RandomPath.Construct(this.start.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
				randomPath.spread = this.spread;
				randomPath.aimStrength = this.aimStrength;
				randomPath.aim = this.end.position;
				path = randomPath;
				break;
			}
			case PathTypesDemo.DemoMode.FleePath:
			{
				FleePath fleePath = FleePath.Construct(this.start.position, this.end.position, this.searchLength, new OnPathDelegate(this.OnPathComplete));
				fleePath.aimStrength = this.aimStrength;
				fleePath.spread = this.spread;
				path = fleePath;
				break;
			}
			case PathTypesDemo.DemoMode.ConstantPath:
				base.StartCoroutine(this.DemoConstantPath());
				break;
			case PathTypesDemo.DemoMode.FloodPath:
				path = (this.lastFloodPath = FloodPath.Construct(this.end.position, null));
				break;
			case PathTypesDemo.DemoMode.FloodPathTracer:
				if (this.lastFloodPath != null)
				{
					FloodPathTracer floodPathTracer = FloodPathTracer.Construct(this.end.position, this.lastFloodPath, new OnPathDelegate(this.OnPathComplete));
					path = floodPathTracer;
				}
				break;
			}
			if (path != null)
			{
				AstarPath.StartPath(path, false);
				this.lastPath = path;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00023D08 File Offset: 0x00022108
		private IEnumerator DemoMultiTargetPath()
		{
			MultiTargetPath mp = MultiTargetPath.Construct(this.multipoints.ToArray(), this.end.position, null, null);
			this.lastPath = mp;
			AstarPath.StartPath(mp, false);
			yield return base.StartCoroutine(mp.WaitForPath());
			List<GameObject> unused = new List<GameObject>(this.lastRender);
			this.lastRender.Clear();
			for (int i = 0; i < mp.vectorPaths.Length; i++)
			{
				if (mp.vectorPaths[i] != null)
				{
					List<Vector3> list = mp.vectorPaths[i];
					GameObject gameObject;
					if (unused.Count > i && unused[i].GetComponent<LineRenderer>() != null)
					{
						gameObject = unused[i];
						unused.RemoveAt(i);
					}
					else
					{
						gameObject = new GameObject("LineRenderer_" + i, new Type[]
						{
							typeof(LineRenderer)
						});
					}
					LineRenderer component = gameObject.GetComponent<LineRenderer>();
					component.sharedMaterial = this.lineMat;
					component.startWidth = this.lineWidth;
					component.endWidth = this.lineWidth;
					component.positionCount = list.Count;
					for (int j = 0; j < list.Count; j++)
					{
						component.SetPosition(j, list[j] + this.pathOffset);
					}
					this.lastRender.Add(gameObject);
				}
			}
			for (int k = 0; k < unused.Count; k++)
			{
				UnityEngine.Object.Destroy(unused[k]);
			}
			yield break;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00023D24 File Offset: 0x00022124
		public IEnumerator DemoConstantPath()
		{
			ConstantPath constPath = ConstantPath.Construct(this.end.position, this.searchLength, null);
			AstarPath.StartPath(constPath, false);
			this.lastPath = constPath;
			yield return base.StartCoroutine(constPath.WaitForPath());
			this.ClearPrevious();
			List<GraphNode> nodes = constPath.allNodes;
			Mesh mesh = new Mesh();
			List<Vector3> verts = new List<Vector3>();
			bool drawRaysInstead = false;
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				Vector3 a = (Vector3)nodes[i].position + this.pathOffset;
				if (verts.Count == 65000 && !drawRaysInstead)
				{
					Debug.LogError("Too many nodes, rendering a mesh would throw 65K vertex error. Using Debug.DrawRay instead for the rest of the nodes");
					drawRaysInstead = true;
				}
				if (drawRaysInstead)
				{
					Debug.DrawRay(a, Vector3.up, Color.blue);
				}
				else
				{
					GridGraph gridGraph = AstarData.GetGraph(nodes[i]) as GridGraph;
					float d = 1f;
					if (gridGraph != null)
					{
						d = gridGraph.nodeSize;
					}
					verts.Add(a + new Vector3(-0.5f, 0f, -0.5f) * d);
					verts.Add(a + new Vector3(0.5f, 0f, -0.5f) * d);
					verts.Add(a + new Vector3(-0.5f, 0f, 0.5f) * d);
					verts.Add(a + new Vector3(0.5f, 0f, 0.5f) * d);
				}
			}
			Vector3[] vs = verts.ToArray();
			int[] tris = new int[3 * vs.Length / 2];
			int j = 0;
			int num = 0;
			while (j < vs.Length)
			{
				tris[num] = j;
				tris[num + 1] = j + 1;
				tris[num + 2] = j + 2;
				tris[num + 3] = j + 1;
				tris[num + 4] = j + 3;
				tris[num + 5] = j + 2;
				num += 6;
				j += 4;
			}
			Vector2[] uv = new Vector2[vs.Length];
			for (int k = 0; k < uv.Length; k += 4)
			{
				uv[k] = new Vector2(0f, 0f);
				uv[k + 1] = new Vector2(1f, 0f);
				uv[k + 2] = new Vector2(0f, 1f);
				uv[k + 3] = new Vector2(1f, 1f);
			}
			mesh.vertices = vs;
			mesh.triangles = tris;
			mesh.uv = uv;
			mesh.RecalculateNormals();
			GameObject go = new GameObject("Mesh", new Type[]
			{
				typeof(MeshRenderer),
				typeof(MeshFilter)
			});
			MeshFilter fi = go.GetComponent<MeshFilter>();
			fi.mesh = mesh;
			MeshRenderer re = go.GetComponent<MeshRenderer>();
			re.material = this.squareMat;
			this.lastRender.Add(go);
			yield break;
		}

		// Token: 0x040003C2 RID: 962
		public PathTypesDemo.DemoMode activeDemo;

		// Token: 0x040003C3 RID: 963
		public Transform start;

		// Token: 0x040003C4 RID: 964
		public Transform end;

		// Token: 0x040003C5 RID: 965
		public Vector3 pathOffset;

		// Token: 0x040003C6 RID: 966
		public Material lineMat;

		// Token: 0x040003C7 RID: 967
		public Material squareMat;

		// Token: 0x040003C8 RID: 968
		public float lineWidth;

		// Token: 0x040003C9 RID: 969
		public int searchLength = 1000;

		// Token: 0x040003CA RID: 970
		public int spread = 100;

		// Token: 0x040003CB RID: 971
		public float aimStrength;

		// Token: 0x040003CC RID: 972
		private Path lastPath;

		// Token: 0x040003CD RID: 973
		private FloodPath lastFloodPath;

		// Token: 0x040003CE RID: 974
		private List<GameObject> lastRender = new List<GameObject>();

		// Token: 0x040003CF RID: 975
		private List<Vector3> multipoints = new List<Vector3>();

		// Token: 0x02000090 RID: 144
		public enum DemoMode
		{
			// Token: 0x040003D1 RID: 977
			ABPath,
			// Token: 0x040003D2 RID: 978
			MultiTargetPath,
			// Token: 0x040003D3 RID: 979
			RandomPath,
			// Token: 0x040003D4 RID: 980
			FleePath,
			// Token: 0x040003D5 RID: 981
			ConstantPath,
			// Token: 0x040003D6 RID: 982
			FloodPath,
			// Token: 0x040003D7 RID: 983
			FloodPathTracer
		}
	}
}
