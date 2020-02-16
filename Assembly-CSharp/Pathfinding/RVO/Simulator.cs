using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200006D RID: 109
	public class Simulator
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x0001ABCC File Offset: 0x00018FCC
		public Simulator(int workers, bool doubleBuffering, MovementPlane movementPlane)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			this.DesiredDeltaTime = 1f;
			this.movementPlane = movementPlane;
			this.Quadtree = new RVOQuadtree();
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001AC78 File Offset: 0x00019078
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001AC80 File Offset: 0x00019080
		public RVOQuadtree Quadtree { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001AC89 File Offset: 0x00019089
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001AC91 File Offset: 0x00019091
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length > 0;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001ACAC File Offset: 0x000190AC
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0001ACB4 File Offset: 0x000190B4
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001ACC7 File Offset: 0x000190C7
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001ACCF File Offset: 0x000190CF
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001ACD8 File Offset: 0x000190D8
		public void ClearAgents()
		{
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].simulator = null;
			}
			this.agents.Clear();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001AD24 File Offset: 0x00019124
		public void OnDestroy()
		{
			if (this.workers != null)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].Terminate();
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001AD64 File Offset: 0x00019164
		~Simulator()
		{
			this.OnDestroy();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001AD94 File Offset: 0x00019194
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			this.BlockUntilSimulationStepIsDone();
			this.agents.Add(agent2);
			return agent;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001AE27 File Offset: 0x00019227
		[Obsolete("Use AddAgent(Vector2,float) instead")]
		public IAgent AddAgent(Vector3 position)
		{
			return this.AddAgent(new Vector2(position.x, position.z), position.y);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001AE49 File Offset: 0x00019249
		public IAgent AddAgent(Vector2 position, float elevationCoordinate)
		{
			return this.AddAgent(new Agent(position, elevationCoordinate));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001AE58 File Offset: 0x00019258
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			this.BlockUntilSimulationStepIsDone();
			agent2.simulator = null;
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001AED9 File Offset: 0x000192D9
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001AF05 File Offset: 0x00019305
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, bool cycle = true)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity, RVOLayer.DefaultObstacle, cycle);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001AF18 File Offset: 0x00019318
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix, RVOLayer layer = RVOLayer.DefaultObstacle, bool cycle = true)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			this.BlockUntilSimulationStepIsDone();
			for (int i = 0; i < vertices.Length; i++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex
				{
					prev = obstacleVertex2,
					layer = layer,
					height = height
				};
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex2 = obstacleVertex3;
			}
			if (cycle)
			{
				obstacleVertex2.next = obstacleVertex;
				obstacleVertex.prev = obstacleVertex2;
			}
			this.UpdateObstacle(obstacleVertex, vertices, matrix);
			this.obstacles.Add(obstacleVertex);
			return obstacleVertex;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001AFCC File Offset: 0x000193CC
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.layer = RVOLayer.DefaultObstacle;
			obstacleVertex2.layer = RVOLayer.DefaultObstacle;
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex2.ignore = true;
			ObstacleVertex obstacleVertex3 = obstacleVertex;
			Vector2 vector = new Vector2(b.x - a.x, b.z - a.z);
			obstacleVertex3.dir = vector.normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001B090 File Offset: 0x00019490
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			bool flag = matrix == Matrix4x4.identity;
			this.BlockUntilSimulationStepIsDone();
			int i = 0;
			ObstacleVertex obstacleVertex = obstacle;
			while (i < vertices.Length)
			{
				obstacleVertex.position = ((!flag) ? matrix.MultiplyPoint3x4(vertices[i]) : vertices[i]);
				obstacleVertex = obstacleVertex.next;
				i++;
				if (obstacleVertex == obstacle || obstacleVertex == null)
				{
					obstacleVertex = obstacle;
					do
					{
						if (obstacleVertex.next == null)
						{
							obstacleVertex.dir = Vector2.zero;
						}
						else
						{
							Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
							ObstacleVertex obstacleVertex2 = obstacleVertex;
							Vector2 vector2 = new Vector2(vector.x, vector.z);
							obstacleVertex2.dir = vector2.normalized;
						}
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacle && obstacleVertex != null);
					this.ScheduleCleanObstacles();
					this.UpdateObstacles();
					return;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length + " supplied)");
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001B1E8 File Offset: 0x000195E8
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001B1F1 File Offset: 0x000195F1
		private void CleanObstacles()
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B1F3 File Offset: 0x000195F3
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			this.BlockUntilSimulationStepIsDone();
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B21F File Offset: 0x0001961F
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001B228 File Offset: 0x00019628
		private void BuildQuadtree()
		{
			this.Quadtree.Clear();
			if (this.agents.Count > 0)
			{
				Rect bounds = Rect.MinMaxRect(this.agents[0].position.x, this.agents[0].position.y, this.agents[0].position.x, this.agents[0].position.y);
				for (int i = 1; i < this.agents.Count; i++)
				{
					Vector2 position = this.agents[i].position;
					bounds = Rect.MinMaxRect(Mathf.Min(bounds.xMin, position.x), Mathf.Min(bounds.yMin, position.y), Mathf.Max(bounds.xMax, position.x), Mathf.Max(bounds.yMax, position.y));
				}
				this.Quadtree.SetBounds(bounds);
				for (int j = 0; j < this.agents.Count; j++)
				{
					this.Quadtree.Insert(this.agents[j]);
				}
			}
			this.Quadtree.CalculateSpeeds();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001B37C File Offset: 0x0001977C
		private void BlockUntilSimulationStepIsDone()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001B3C8 File Offset: 0x000197C8
		private void PreCalculation()
		{
			for (int i = 0; i < this.agents.Count; i++)
			{
				this.agents[i].PreCalculation();
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001B402 File Offset: 0x00019802
		private void CleanAndUpdateObstaclesIfNecessary()
		{
			if (this.doCleanObstacles)
			{
				this.CleanObstacles();
				this.doCleanObstacles = false;
				this.doUpdateObstacles = true;
			}
			if (this.doUpdateObstacles)
			{
				this.doUpdateObstacles = false;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001B438 File Offset: 0x00019838
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
			}
			if (Time.time - this.lastStep >= this.DesiredDeltaTime)
			{
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int i = 0; i < this.workers.Length; i++)
						{
							this.workers[i].WaitOne();
						}
						for (int j = 0; j < this.agents.Count; j++)
						{
							this.agents[j].PostCalculation();
						}
					}
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int k = 0; k < this.workers.Length; k++)
					{
						this.workers[k].start = k * this.agents.Count / this.workers.Length;
						this.workers[k].end = (k + 1) * this.agents.Count / this.workers.Length;
					}
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].Execute(1);
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].WaitOne();
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num = 0; num < this.workers.Length; num++)
						{
							this.workers[num].WaitOne();
						}
						for (int num2 = 0; num2 < this.agents.Count; num2++)
						{
							this.agents[num2].PostCalculation();
						}
					}
				}
				else
				{
					this.PreCalculation();
					this.CleanAndUpdateObstaclesIfNecessary();
					this.BuildQuadtree();
					for (int num3 = 0; num3 < this.agents.Count; num3++)
					{
						this.agents[num3].BufferSwitch();
					}
					for (int num4 = 0; num4 < this.agents.Count; num4++)
					{
						this.agents[num4].CalculateNeighbours();
						this.agents[num4].CalculateVelocity(this.coroutineWorkerContext);
					}
					for (int num5 = 0; num5 < this.agents.Count; num5++)
					{
						this.agents[num5].PostCalculation();
					}
				}
			}
		}

		// Token: 0x040002F7 RID: 759
		private readonly bool doubleBuffering = true;

		// Token: 0x040002F8 RID: 760
		private float desiredDeltaTime = 0.05f;

		// Token: 0x040002F9 RID: 761
		private readonly Simulator.Worker[] workers;

		// Token: 0x040002FA RID: 762
		private List<Agent> agents;

		// Token: 0x040002FB RID: 763
		public List<ObstacleVertex> obstacles;

		// Token: 0x040002FD RID: 765
		private float deltaTime;

		// Token: 0x040002FE RID: 766
		private float lastStep = -99999f;

		// Token: 0x040002FF RID: 767
		private bool doUpdateObstacles;

		// Token: 0x04000300 RID: 768
		private bool doCleanObstacles;

		// Token: 0x04000301 RID: 769
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x04000302 RID: 770
		public readonly MovementPlane movementPlane;

		// Token: 0x04000303 RID: 771
		private Simulator.WorkerContext coroutineWorkerContext = new Simulator.WorkerContext();

		// Token: 0x0200006E RID: 110
		internal class WorkerContext
		{
			// Token: 0x04000304 RID: 772
			public Agent.VOBuffer vos = new Agent.VOBuffer(16);

			// Token: 0x04000305 RID: 773
			public const int KeepCount = 3;

			// Token: 0x04000306 RID: 774
			public Vector2[] bestPos = new Vector2[3];

			// Token: 0x04000307 RID: 775
			public float[] bestSizes = new float[3];

			// Token: 0x04000308 RID: 776
			public float[] bestScores = new float[4];

			// Token: 0x04000309 RID: 777
			public Vector2[] samplePos = new Vector2[50];

			// Token: 0x0400030A RID: 778
			public float[] sampleSize = new float[50];
		}

		// Token: 0x0200006F RID: 111
		private class Worker
		{
			// Token: 0x060004EF RID: 1263 RVA: 0x0001B7A0 File Offset: 0x00019BA0
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				new Thread(new ThreadStart(this.Run))
				{
					IsBackground = true,
					Name = "RVO Simulator Thread"
				}.Start();
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x0001B807 File Offset: 0x00019C07
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x0001B828 File Offset: 0x00019C28
			public void WaitOne()
			{
				if (!this.terminate)
				{
					this.waitFlag.WaitOne();
				}
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x0001B841 File Offset: 0x00019C41
			public void Terminate()
			{
				this.WaitOne();
				this.terminate = true;
				this.Execute(-1);
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x0001B858 File Offset: 0x00019C58
			public void Run()
			{
				this.runFlag.WaitOne();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity(this.context);
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].BufferSwitch();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task);
								throw new Exception("Invalid Task Number: " + this.task);
							}
							this.simulator.BuildQuadtree();
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					this.waitFlag.Set();
					this.runFlag.WaitOne();
				}
			}

			// Token: 0x0400030B RID: 779
			public int start;

			// Token: 0x0400030C RID: 780
			public int end;

			// Token: 0x0400030D RID: 781
			private readonly AutoResetEvent runFlag = new AutoResetEvent(false);

			// Token: 0x0400030E RID: 782
			private readonly ManualResetEvent waitFlag = new ManualResetEvent(true);

			// Token: 0x0400030F RID: 783
			private readonly Simulator simulator;

			// Token: 0x04000310 RID: 784
			private int task;

			// Token: 0x04000311 RID: 785
			private bool terminate;

			// Token: 0x04000312 RID: 786
			private Simulator.WorkerContext context = new Simulator.WorkerContext();
		}
	}
}
