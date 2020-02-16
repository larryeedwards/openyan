using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000FD RID: 253
	public abstract class VersionedMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver, IVersionedMonoBehaviourInternal
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00002058 File Offset: 0x00000458
		protected virtual void Awake()
		{
			if (Application.isPlaying)
			{
				this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00002076 File Offset: 0x00000476
		private void Reset()
		{
			this.version = this.OnUpgradeSerializedData(int.MaxValue, true);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000208A File Offset: 0x0000048A
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000208C File Offset: 0x0000048C
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.version = this.OnUpgradeSerializedData(this.version, false);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000020A1 File Offset: 0x000004A1
		protected virtual int OnUpgradeSerializedData(int version, bool unityThread)
		{
			return 1;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000020A4 File Offset: 0x000004A4
		int IVersionedMonoBehaviourInternal.OnUpgradeSerializedData(int version, bool unityThread)
		{
			return this.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x0400067B RID: 1659
		[SerializeField]
		[HideInInspector]
		private int version;
	}
}
