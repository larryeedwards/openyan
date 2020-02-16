using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000007 RID: 7
	public interface IAstarAI
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000085 RID: 133
		Vector3 position { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000086 RID: 134
		Quaternion rotation { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		float maxSpeed { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000089 RID: 137
		Vector3 velocity { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008A RID: 138
		Vector3 desiredVelocity { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008B RID: 139
		float remainingDistance { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008C RID: 140
		bool reachedEndOfPath { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		Vector3 destination { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		bool canSearch { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		bool canMove { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000093 RID: 147
		bool hasPath { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000094 RID: 148
		bool pathPending { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		bool isStopped { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000097 RID: 151
		Vector3 steeringTarget { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152
		// (set) Token: 0x06000099 RID: 153
		Action onSearchPath { get; set; }

		// Token: 0x0600009A RID: 154
		void SearchPath();

		// Token: 0x0600009B RID: 155
		void SetPath(Path path);

		// Token: 0x0600009C RID: 156
		void Teleport(Vector3 newPosition, bool clearPath = true);

		// Token: 0x0600009D RID: 157
		void Move(Vector3 deltaPosition);

		// Token: 0x0600009E RID: 158
		void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x0600009F RID: 159
		void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation);
	}
}
