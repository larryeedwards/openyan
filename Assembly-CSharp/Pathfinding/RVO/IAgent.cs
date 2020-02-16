using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200006A RID: 106
	public interface IAgent
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004B0 RID: 1200
		// (set) Token: 0x060004B1 RID: 1201
		Vector2 Position { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004B2 RID: 1202
		// (set) Token: 0x060004B3 RID: 1203
		float ElevationCoordinate { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004B4 RID: 1204
		Vector2 CalculatedTargetPoint { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004B5 RID: 1205
		float CalculatedSpeed { get; }

		// Token: 0x060004B6 RID: 1206
		void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed);

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004B7 RID: 1207
		// (set) Token: 0x060004B8 RID: 1208
		bool Locked { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004B9 RID: 1209
		// (set) Token: 0x060004BA RID: 1210
		float Radius { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004BB RID: 1211
		// (set) Token: 0x060004BC RID: 1212
		float Height { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004BD RID: 1213
		// (set) Token: 0x060004BE RID: 1214
		float AgentTimeHorizon { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004BF RID: 1215
		// (set) Token: 0x060004C0 RID: 1216
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004C1 RID: 1217
		// (set) Token: 0x060004C2 RID: 1218
		int MaxNeighbours { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004C3 RID: 1219
		int NeighbourCount { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004C4 RID: 1220
		// (set) Token: 0x060004C5 RID: 1221
		RVOLayer Layer { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004C6 RID: 1222
		// (set) Token: 0x060004C7 RID: 1223
		RVOLayer CollidesWith { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004C8 RID: 1224
		// (set) Token: 0x060004C9 RID: 1225
		bool DebugDraw { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004CA RID: 1226
		[Obsolete]
		List<ObstacleVertex> NeighbourObstacles { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004CB RID: 1227
		// (set) Token: 0x060004CC RID: 1228
		float Priority { get; set; }

		// Token: 0x170000DC RID: 220
		// (set) Token: 0x060004CD RID: 1229
		Action PreCalculationCallback { set; }

		// Token: 0x060004CE RID: 1230
		void SetCollisionNormal(Vector2 normal);

		// Token: 0x060004CF RID: 1231
		void ForceSetVelocity(Vector2 velocity);
	}
}
