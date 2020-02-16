using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO.Sampled
{
	// Token: 0x02000066 RID: 102
	public class Agent : IAgent
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x000191FC File Offset: 0x000175FC
		public Agent(Vector2 pos, float elevationCoordinate)
		{
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.Position = pos;
			this.ElevationCoordinate = elevationCoordinate;
			this.Layer = RVOLayer.DefaultAgent;
			this.CollidesWith = (RVOLayer)(-1);
			this.Priority = 0.5f;
			this.CalculatedTargetPoint = pos;
			this.CalculatedSpeed = 0f;
			this.SetTarget(pos, 0f, 0f);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000192C0 File Offset: 0x000176C0
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000192C8 File Offset: 0x000176C8
		public Vector2 Position { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000192D1 File Offset: 0x000176D1
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000192D9 File Offset: 0x000176D9
		public float ElevationCoordinate { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000192E2 File Offset: 0x000176E2
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000192EA File Offset: 0x000176EA
		public Vector2 CalculatedTargetPoint { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000192F3 File Offset: 0x000176F3
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000192FB File Offset: 0x000176FB
		public float CalculatedSpeed { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00019304 File Offset: 0x00017704
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001930C File Offset: 0x0001770C
		public bool Locked { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00019315 File Offset: 0x00017715
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0001931D File Offset: 0x0001771D
		public float Radius { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00019326 File Offset: 0x00017726
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0001932E File Offset: 0x0001772E
		public float Height { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00019337 File Offset: 0x00017737
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001933F File Offset: 0x0001773F
		public float AgentTimeHorizon { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00019348 File Offset: 0x00017748
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00019350 File Offset: 0x00017750
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00019359 File Offset: 0x00017759
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00019361 File Offset: 0x00017761
		public int MaxNeighbours { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001936A File Offset: 0x0001776A
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00019372 File Offset: 0x00017772
		public int NeighbourCount { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001937B File Offset: 0x0001777B
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00019383 File Offset: 0x00017783
		public RVOLayer Layer { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001938C File Offset: 0x0001778C
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00019394 File Offset: 0x00017794
		public RVOLayer CollidesWith { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001939D File Offset: 0x0001779D
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000193A5 File Offset: 0x000177A5
		public bool DebugDraw
		{
			get
			{
				return this.debugDraw;
			}
			set
			{
				this.debugDraw = (value && this.simulator != null && !this.simulator.Multithreading);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000193CF File Offset: 0x000177CF
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x000193D7 File Offset: 0x000177D7
		public float Priority { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x000193E0 File Offset: 0x000177E0
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000193E8 File Offset: 0x000177E8
		public Action PreCalculationCallback { private get; set; }

		// Token: 0x06000491 RID: 1169 RVA: 0x000193F1 File Offset: 0x000177F1
		public void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed)
		{
			maxSpeed = Math.Max(maxSpeed, 0f);
			desiredSpeed = Math.Min(Math.Max(desiredSpeed, 0f), maxSpeed);
			this.nextTargetPoint = targetPoint;
			this.nextDesiredSpeed = desiredSpeed;
			this.nextMaxSpeed = maxSpeed;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00019428 File Offset: 0x00017828
		public void SetCollisionNormal(Vector2 normal)
		{
			this.collisionNormal = normal;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00019434 File Offset: 0x00017834
		public void ForceSetVelocity(Vector2 velocity)
		{
			Vector2 vector = this.position + velocity * 1000f;
			this.CalculatedTargetPoint = vector;
			this.nextTargetPoint = vector;
			float magnitude = velocity.magnitude;
			this.CalculatedSpeed = magnitude;
			this.nextDesiredSpeed = magnitude;
			this.manuallyControlled = true;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00019483 File Offset: 0x00017883
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00019488 File Offset: 0x00017888
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.nextMaxSpeed;
			this.desiredSpeed = this.nextDesiredSpeed;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.locked = (this.Locked && !this.manuallyControlled);
			this.position = this.Position;
			this.elevationCoordinate = this.ElevationCoordinate;
			this.collidesWith = this.CollidesWith;
			this.layer = this.Layer;
			if (this.locked)
			{
				this.desiredTargetPointInVelocitySpace = this.position;
				this.desiredVelocity = (this.currentVelocity = Vector2.zero);
			}
			else
			{
				this.desiredTargetPointInVelocitySpace = this.nextTargetPoint - this.position;
				this.currentVelocity = (this.CalculatedTargetPoint - this.position).normalized * this.CalculatedSpeed;
				this.desiredVelocity = this.desiredTargetPointInVelocitySpace.normalized * this.desiredSpeed;
				if (this.collisionNormal != Vector2.zero)
				{
					this.collisionNormal.Normalize();
					float num = Vector2.Dot(this.currentVelocity, this.collisionNormal);
					if (num < 0f)
					{
						this.currentVelocity -= this.collisionNormal * num;
					}
					this.collisionNormal = Vector2.zero;
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00019628 File Offset: 0x00017A28
		public void PreCalculation()
		{
			if (this.PreCalculationCallback != null)
			{
				this.PreCalculationCallback();
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00019640 File Offset: 0x00017A40
		public void PostCalculation()
		{
			if (!this.manuallyControlled)
			{
				this.CalculatedTargetPoint = this.calculatedTargetPoint;
				this.CalculatedSpeed = this.calculatedSpeed;
			}
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
			this.manuallyControlled = false;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00019694 File Offset: 0x00017A94
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			if (this.MaxNeighbours > 0 && !this.locked)
			{
				this.simulator.Quadtree.Query(this.position, this.maxSpeed, this.agentTimeHorizon, this.radius, this);
			}
			this.NeighbourCount = this.neighbours.Count;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00019708 File Offset: 0x00017B08
		private static float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00019710 File Offset: 0x00017B10
		internal float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent || (agent.layer & this.collidesWith) == (RVOLayer)0)
			{
				return rangeSq;
			}
			float sqrMagnitude = (agent.position - this.position).sqrMagnitude;
			if (sqrMagnitude < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(null);
					this.neighbourDists.Add(float.PositiveInfinity);
				}
				int num = this.neighbours.Count - 1;
				if (sqrMagnitude < this.neighbourDists[num])
				{
					while (num != 0 && sqrMagnitude < this.neighbourDists[num - 1])
					{
						this.neighbours[num] = this.neighbours[num - 1];
						this.neighbourDists[num] = this.neighbourDists[num - 1];
						num--;
					}
					this.neighbours[num] = agent;
					this.neighbourDists[num] = sqrMagnitude;
				}
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001984D File Offset: 0x00017C4D
		private static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00019867 File Offset: 0x00017C67
		private static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001987C File Offset: 0x00017C7C
		private Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.simulator.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000198D4 File Offset: 0x00017CD4
		private static void DrawVO(Vector2 circleCenter, float radius, Vector2 origin)
		{
			float num = Mathf.Atan2((origin - circleCenter).y, (origin - circleCenter).x);
			float num2 = radius / (origin - circleCenter).magnitude;
			float num3 = (num2 > 1f) ? 0f : Mathf.Abs(Mathf.Acos(num2));
			Draw.Debug.CircleXZ(Agent.FromXZ(circleCenter), radius, Color.black, num - num3, num + num3);
			Vector2 vector = new Vector2(Mathf.Cos(num - num3), Mathf.Sin(num - num3)) * radius;
			Vector2 vector2 = new Vector2(Mathf.Cos(num + num3), Mathf.Sin(num + num3)) * radius;
			Vector2 p = -new Vector2(-vector.y, vector.x);
			Vector2 p2 = new Vector2(-vector2.y, vector2.x);
			vector += circleCenter;
			vector2 += circleCenter;
			Debug.DrawRay(Agent.FromXZ(vector), Agent.FromXZ(p).normalized * 100f, Color.black);
			Debug.DrawRay(Agent.FromXZ(vector2), Agent.FromXZ(p2).normalized * 100f, Color.black);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00019A34 File Offset: 0x00017E34
		internal void CalculateVelocity(Simulator.WorkerContext context)
		{
			if (this.manuallyControlled)
			{
				return;
			}
			if (this.locked)
			{
				this.calculatedSpeed = 0f;
				this.calculatedTargetPoint = this.position;
				return;
			}
			Agent.VOBuffer vos = context.vos;
			vos.Clear();
			this.GenerateObstacleVOs(vos);
			this.GenerateNeighbourAgentVOs(vos);
			if (!Agent.BiasDesiredVelocity(vos, ref this.desiredVelocity, ref this.desiredTargetPointInVelocitySpace, this.simulator.symmetryBreakingBias))
			{
				this.calculatedTargetPoint = this.desiredTargetPointInVelocitySpace + this.position;
				this.calculatedSpeed = this.desiredSpeed;
				if (this.DebugDraw)
				{
					Draw.Debug.CrossXZ(Agent.FromXZ(this.calculatedTargetPoint), Color.white, 1f);
				}
				return;
			}
			Vector2 vector = Vector2.zero;
			vector = this.GradientDescent(vos, this.currentVelocity, this.desiredVelocity);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.white, 1f);
			}
			this.calculatedTargetPoint = this.position + vector;
			this.calculatedSpeed = Mathf.Min(vector.magnitude, this.maxSpeed);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00019B74 File Offset: 0x00017F74
		private static Color Rainbow(float v)
		{
			Color result = new Color(v, 0f, 0f);
			if (result.r > 1f)
			{
				result.g = result.r - 1f;
				result.r = 1f;
			}
			if (result.g > 1f)
			{
				result.b = result.g - 1f;
				result.g = 1f;
			}
			return result;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00019BF8 File Offset: 0x00017FF8
		private void GenerateObstacleVOs(Agent.VOBuffer vos)
		{
			float num = this.maxSpeed * this.obstacleTimeHorizon;
			for (int i = 0; i < this.simulator.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.simulator.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					if (obstacleVertex2.ignore || (obstacleVertex2.layer & this.collidesWith) == (RVOLayer)0)
					{
						obstacleVertex2 = obstacleVertex2.next;
					}
					else
					{
						float a;
						Vector2 vector = this.To2D(obstacleVertex2.position, out a);
						float b;
						Vector2 vector2 = this.To2D(obstacleVertex2.next.position, out b);
						Vector2 normalized = (vector2 - vector).normalized;
						float num2 = Agent.VO.SignedDistanceFromLine(vector, normalized, this.position);
						if (num2 >= -0.01f && num2 < num)
						{
							float t = Vector2.Dot(this.position - vector, vector2 - vector) / (vector2 - vector).sqrMagnitude;
							float num3 = Mathf.Lerp(a, b, t);
							float sqrMagnitude = (Vector2.Lerp(vector, vector2, t) - this.position).sqrMagnitude;
							if (sqrMagnitude < num * num && (this.simulator.movementPlane == MovementPlane.XY || (this.elevationCoordinate <= num3 + obstacleVertex2.height && this.elevationCoordinate + this.height >= num3)))
							{
								vos.Add(Agent.VO.SegmentObstacle(vector2 - this.position, vector - this.position, Vector2.zero, this.radius * 0.01f, 1f / this.ObstacleTimeHorizon, 1f / this.simulator.DeltaTime));
							}
						}
						obstacleVertex2 = obstacleVertex2.next;
					}
				}
				while (obstacleVertex2 != obstacleVertex && obstacleVertex2 != null && obstacleVertex2.next != null);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00019DE4 File Offset: 0x000181E4
		private void GenerateNeighbourAgentVOs(Agent.VOBuffer vos)
		{
			float num = 1f / this.agentTimeHorizon;
			Vector2 a = this.currentVelocity;
			for (int i = 0; i < this.neighbours.Count; i++)
			{
				Agent agent = this.neighbours[i];
				if (agent != this)
				{
					float num2 = Math.Min(this.elevationCoordinate + this.height, agent.elevationCoordinate + agent.height);
					float num3 = Math.Max(this.elevationCoordinate, agent.elevationCoordinate);
					if (num2 - num3 >= 0f)
					{
						float num4 = this.radius + agent.radius;
						Vector2 vector = agent.position - this.position;
						float num5;
						if (agent.locked || agent.manuallyControlled)
						{
							num5 = 1f;
						}
						else if (agent.Priority > 1E-05f || this.Priority > 1E-05f)
						{
							num5 = agent.Priority / (this.Priority + agent.Priority);
						}
						else
						{
							num5 = 0.5f;
						}
						Vector2 b = Vector2.Lerp(agent.currentVelocity, agent.desiredVelocity, 2f * num5 - 1f);
						Vector2 vector2 = Vector2.Lerp(a, b, num5);
						vos.Add(new Agent.VO(vector, vector2, num4, num, 1f / this.simulator.DeltaTime));
						if (this.DebugDraw)
						{
							Agent.DrawVO(this.position + vector * num + vector2, num4 * num, this.position + vector2);
						}
					}
				}
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00019F94 File Offset: 0x00018394
		private Vector2 GradientDescent(Agent.VOBuffer vos, Vector2 sampleAround1, Vector2 sampleAround2)
		{
			float num;
			Vector2 vector = this.Trace(vos, sampleAround1, out num);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector + this.position), Color.yellow, 0.5f);
			}
			float num2;
			Vector2 vector2 = this.Trace(vos, sampleAround2, out num2);
			if (this.DebugDraw)
			{
				Draw.Debug.CrossXZ(Agent.FromXZ(vector2 + this.position), Color.magenta, 0.5f);
			}
			return (num >= num2) ? vector2 : vector;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001A028 File Offset: 0x00018428
		private static bool BiasDesiredVelocity(Agent.VOBuffer vos, ref Vector2 desiredVelocity, ref Vector2 targetPointInVelocitySpace, float maxBiasRadians)
		{
			float magnitude = desiredVelocity.magnitude;
			float num = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float b;
				vos.buffer[i].Gradient(desiredVelocity, out b);
				num = Mathf.Max(num, b);
			}
			bool result = num > 0f;
			if (magnitude < 0.001f)
			{
				return result;
			}
			float d = Mathf.Min(maxBiasRadians, num / magnitude);
			desiredVelocity += new Vector2(desiredVelocity.y, -desiredVelocity.x) * d;
			targetPointInVelocitySpace += new Vector2(targetPointInVelocitySpace.y, -targetPointInVelocitySpace.x) * d;
			return result;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001A0F4 File Offset: 0x000184F4
		private Vector2 EvaluateGradient(Agent.VOBuffer vos, Vector2 p, out float value)
		{
			Vector2 vector = Vector2.zero;
			value = 0f;
			for (int i = 0; i < vos.length; i++)
			{
				float num;
				Vector2 vector2 = vos.buffer[i].ScaledGradient(p, out num);
				if (num > value)
				{
					value = num;
					vector = vector2;
				}
			}
			Vector2 a = this.desiredVelocity - p;
			float magnitude = a.magnitude;
			if (magnitude > 0.0001f)
			{
				vector += a * (0.1f / magnitude);
				value += magnitude * 0.1f;
			}
			float sqrMagnitude = p.sqrMagnitude;
			if (sqrMagnitude > this.desiredSpeed * this.desiredSpeed)
			{
				float num2 = Mathf.Sqrt(sqrMagnitude);
				if (num2 > this.maxSpeed)
				{
					value += 3f * (num2 - this.maxSpeed);
					vector -= 3f * (p / num2);
				}
				float num3 = 0.2f;
				value += num3 * (num2 - this.desiredSpeed);
				vector -= num3 * (p / num2);
			}
			return vector;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001A21C File Offset: 0x0001861C
		private Vector2 Trace(Agent.VOBuffer vos, Vector2 p, out float score)
		{
			float num = Mathf.Max(this.radius, 0.2f * this.desiredSpeed);
			float num2 = float.PositiveInfinity;
			Vector2 result = p;
			for (int i = 0; i < 50; i++)
			{
				float num3 = 1f - (float)i / 50f;
				num3 = Agent.Sqr(num3) * num;
				float num4;
				Vector2 vector = this.EvaluateGradient(vos, p, out num4);
				if (num4 < num2)
				{
					num2 = num4;
					result = p;
				}
				vector.Normalize();
				vector *= num3;
				Vector2 a = p;
				p += vector;
				if (this.DebugDraw)
				{
					Debug.DrawLine(Agent.FromXZ(a + this.position), Agent.FromXZ(p + this.position), Agent.Rainbow((float)i * 0.1f) * new Color(1f, 1f, 1f, 1f));
				}
			}
			score = num2;
			return result;
		}

		// Token: 0x0400028F RID: 655
		internal float radius;

		// Token: 0x04000290 RID: 656
		internal float height;

		// Token: 0x04000291 RID: 657
		internal float desiredSpeed;

		// Token: 0x04000292 RID: 658
		internal float maxSpeed;

		// Token: 0x04000293 RID: 659
		internal float agentTimeHorizon;

		// Token: 0x04000294 RID: 660
		internal float obstacleTimeHorizon;

		// Token: 0x04000295 RID: 661
		internal bool locked;

		// Token: 0x04000296 RID: 662
		private RVOLayer layer;

		// Token: 0x04000297 RID: 663
		private RVOLayer collidesWith;

		// Token: 0x04000298 RID: 664
		private int maxNeighbours;

		// Token: 0x04000299 RID: 665
		internal Vector2 position;

		// Token: 0x0400029A RID: 666
		private float elevationCoordinate;

		// Token: 0x0400029B RID: 667
		private Vector2 currentVelocity;

		// Token: 0x0400029C RID: 668
		private Vector2 desiredTargetPointInVelocitySpace;

		// Token: 0x0400029D RID: 669
		private Vector2 desiredVelocity;

		// Token: 0x0400029E RID: 670
		private Vector2 nextTargetPoint;

		// Token: 0x0400029F RID: 671
		private float nextDesiredSpeed;

		// Token: 0x040002A0 RID: 672
		private float nextMaxSpeed;

		// Token: 0x040002A1 RID: 673
		private Vector2 collisionNormal;

		// Token: 0x040002A2 RID: 674
		private bool manuallyControlled;

		// Token: 0x040002A3 RID: 675
		private bool debugDraw;

		// Token: 0x040002B3 RID: 691
		internal Agent next;

		// Token: 0x040002B4 RID: 692
		private float calculatedSpeed;

		// Token: 0x040002B5 RID: 693
		private Vector2 calculatedTargetPoint;

		// Token: 0x040002B6 RID: 694
		internal Simulator simulator;

		// Token: 0x040002B7 RID: 695
		private List<Agent> neighbours = new List<Agent>();

		// Token: 0x040002B8 RID: 696
		private List<float> neighbourDists = new List<float>();

		// Token: 0x040002B9 RID: 697
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040002BA RID: 698
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040002BB RID: 699
		private const float DesiredVelocityWeight = 0.1f;

		// Token: 0x040002BC RID: 700
		private const float WallWeight = 5f;

		// Token: 0x02000067 RID: 103
		internal struct VO
		{
			// Token: 0x060004A7 RID: 1191 RVA: 0x0001A314 File Offset: 0x00018714
			public VO(Vector2 center, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				this.weightFactor = 1f;
				this.weightBonus = 0f;
				this.circleCenter = center * inverseDt + offset;
				this.weightFactor = 4f * Mathf.Exp(-Agent.Sqr(center.sqrMagnitude / (radius * radius))) + 1f;
				if (center.magnitude < radius)
				{
					this.colliding = true;
					this.line1 = center.normalized * (center.magnitude - radius - 0.001f) * 0.3f * inverseDeltaTime;
					Vector2 vector = new Vector2(this.line1.y, -this.line1.x);
					this.dir1 = vector.normalized;
					this.line1 += offset;
					this.cutoffDir = Vector2.zero;
					this.cutoffLine = Vector2.zero;
					this.dir2 = Vector2.zero;
					this.line2 = Vector2.zero;
					this.radius = 0f;
				}
				else
				{
					this.colliding = false;
					center *= inverseDt;
					radius *= inverseDt;
					Vector2 b = center + offset;
					float d = center.magnitude - radius + 0.001f;
					this.cutoffLine = center.normalized * d;
					Vector2 vector2 = new Vector2(-this.cutoffLine.y, this.cutoffLine.x);
					this.cutoffDir = vector2.normalized;
					this.cutoffLine += offset;
					float num = Mathf.Atan2(-center.y, -center.x);
					float num2 = Mathf.Abs(Mathf.Acos(radius / center.magnitude));
					this.radius = radius;
					this.line1 = new Vector2(Mathf.Cos(num + num2), Mathf.Sin(num + num2));
					this.dir1 = new Vector2(this.line1.y, -this.line1.x);
					this.line2 = new Vector2(Mathf.Cos(num - num2), Mathf.Sin(num - num2));
					this.dir2 = new Vector2(this.line2.y, -this.line2.x);
					this.line1 = this.line1 * radius + b;
					this.line2 = this.line2 * radius + b;
				}
				this.segmentStart = Vector2.zero;
				this.segmentEnd = Vector2.zero;
				this.segment = false;
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x0001A5B4 File Offset: 0x000189B4
			public static Agent.VO SegmentObstacle(Vector2 segmentStart, Vector2 segmentEnd, Vector2 offset, float radius, float inverseDt, float inverseDeltaTime)
			{
				Agent.VO result = default(Agent.VO);
				result.weightFactor = 1f;
				result.weightBonus = Mathf.Max(radius, 1f) * 40f;
				Vector3 vector = VectorMath.ClosestPointOnSegment(segmentStart, segmentEnd, Vector2.zero);
				if (vector.magnitude <= radius)
				{
					result.colliding = true;
					result.line1 = vector.normalized * (vector.magnitude - radius) * 0.3f * inverseDeltaTime;
					Vector2 vector2 = new Vector2(result.line1.y, -result.line1.x);
					result.dir1 = vector2.normalized;
					result.line1 += offset;
					result.cutoffDir = Vector2.zero;
					result.cutoffLine = Vector2.zero;
					result.dir2 = Vector2.zero;
					result.line2 = Vector2.zero;
					result.radius = 0f;
					result.segmentStart = Vector2.zero;
					result.segmentEnd = Vector2.zero;
					result.segment = false;
				}
				else
				{
					result.colliding = false;
					segmentStart *= inverseDt;
					segmentEnd *= inverseDt;
					radius *= inverseDt;
					Vector2 normalized = (segmentEnd - segmentStart).normalized;
					result.cutoffDir = normalized;
					result.cutoffLine = segmentStart + new Vector2(-normalized.y, normalized.x) * radius;
					result.cutoffLine += offset;
					float sqrMagnitude = segmentStart.sqrMagnitude;
					Vector2 a = -VectorMath.ComplexMultiply(segmentStart, new Vector2(radius, Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude - radius * radius)))) / sqrMagnitude;
					float sqrMagnitude2 = segmentEnd.sqrMagnitude;
					Vector2 a2 = -VectorMath.ComplexMultiply(segmentEnd, new Vector2(radius, -Mathf.Sqrt(Mathf.Max(0f, sqrMagnitude2 - radius * radius)))) / sqrMagnitude2;
					result.line1 = segmentStart + a * radius + offset;
					result.line2 = segmentEnd + a2 * radius + offset;
					result.dir1 = new Vector2(a.y, -a.x);
					result.dir2 = new Vector2(a2.y, -a2.x);
					result.segmentStart = segmentStart;
					result.segmentEnd = segmentEnd;
					result.radius = radius;
					result.segment = true;
				}
				return result;
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0001A865 File Offset: 0x00018C65
			public static float SignedDistanceFromLine(Vector2 a, Vector2 dir, Vector2 p)
			{
				return (p.x - a.x) * dir.y - dir.x * (p.y - a.y);
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0001A898 File Offset: 0x00018C98
			public Vector2 ScaledGradient(Vector2 p, out float weight)
			{
				Vector2 vector = this.Gradient(p, out weight);
				if (weight > 0f)
				{
					vector *= 2f * this.weightFactor;
					weight *= 2f * this.weightFactor;
					weight += 1f + this.weightBonus;
				}
				return vector;
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0001A8F0 File Offset: 0x00018CF0
			public Vector2 Gradient(Vector2 p, out float weight)
			{
				if (this.colliding)
				{
					float num = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						weight = num;
						return new Vector2(-this.dir1.y, this.dir1.x);
					}
					weight = 0f;
					return new Vector2(0f, 0f);
				}
				else
				{
					float num2 = Agent.VO.SignedDistanceFromLine(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					float num3 = Agent.VO.SignedDistanceFromLine(this.line1, this.dir1, p);
					float num4 = Agent.VO.SignedDistanceFromLine(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					Vector2 result;
					if (Vector2.Dot(p - this.line1, this.dir1) > 0f && Vector2.Dot(p - this.line2, this.dir2) < 0f)
					{
						if (!this.segment)
						{
							Vector2 v = p - this.circleCenter;
							float num5;
							result = VectorMath.Normalize(v, out num5);
							weight = this.radius - num5;
							return result;
						}
						if (num2 < this.radius)
						{
							Vector2 b = VectorMath.ClosestPointOnSegment(this.segmentStart, this.segmentEnd, p);
							Vector2 v2 = p - b;
							float num6;
							result = VectorMath.Normalize(v2, out num6);
							weight = this.radius - num6;
							return result;
						}
					}
					if (this.segment && num2 < num3 && num2 < num4)
					{
						weight = num2;
						result = new Vector2(-this.cutoffDir.y, this.cutoffDir.x);
						return result;
					}
					if (num3 < num4)
					{
						weight = num3;
						result = new Vector2(-this.dir1.y, this.dir1.x);
					}
					else
					{
						weight = num4;
						result = new Vector2(-this.dir2.y, this.dir2.x);
					}
					return result;
				}
			}

			// Token: 0x040002BD RID: 701
			private Vector2 line1;

			// Token: 0x040002BE RID: 702
			private Vector2 line2;

			// Token: 0x040002BF RID: 703
			private Vector2 dir1;

			// Token: 0x040002C0 RID: 704
			private Vector2 dir2;

			// Token: 0x040002C1 RID: 705
			private Vector2 cutoffLine;

			// Token: 0x040002C2 RID: 706
			private Vector2 cutoffDir;

			// Token: 0x040002C3 RID: 707
			private Vector2 circleCenter;

			// Token: 0x040002C4 RID: 708
			private bool colliding;

			// Token: 0x040002C5 RID: 709
			private float radius;

			// Token: 0x040002C6 RID: 710
			private float weightFactor;

			// Token: 0x040002C7 RID: 711
			private float weightBonus;

			// Token: 0x040002C8 RID: 712
			private Vector2 segmentStart;

			// Token: 0x040002C9 RID: 713
			private Vector2 segmentEnd;

			// Token: 0x040002CA RID: 714
			private bool segment;
		}

		// Token: 0x02000068 RID: 104
		internal class VOBuffer
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x0001AB2E File Offset: 0x00018F2E
			public VOBuffer(int n)
			{
				this.buffer = new Agent.VO[n];
				this.length = 0;
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x0001AB49 File Offset: 0x00018F49
			public void Clear()
			{
				this.length = 0;
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x0001AB54 File Offset: 0x00018F54
			public void Add(Agent.VO vo)
			{
				if (this.length >= this.buffer.Length)
				{
					Agent.VO[] array = new Agent.VO[this.buffer.Length * 2];
					this.buffer.CopyTo(array, 0);
					this.buffer = array;
				}
				this.buffer[this.length++] = vo;
			}

			// Token: 0x040002CB RID: 715
			public Agent.VO[] buffer;

			// Token: 0x040002CC RID: 716
			public int length;
		}
	}
}
