using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000128 RID: 296
	[ExecuteInEditMode]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_unity_reference_helper.php")]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x00050AE4 File Offset: 0x0004EEE4
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00050AEC File Offset: 0x0004EEEC
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00050AF4 File Offset: 0x0004EEF4
		public void Reset()
		{
			if (string.IsNullOrEmpty(this.guid))
			{
				this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
				Debug.Log("Created new GUID - " + this.guid);
			}
			else
			{
				foreach (UnityReferenceHelper unityReferenceHelper in UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
				{
					if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
					{
						this.guid = Pathfinding.Util.Guid.NewGuid().ToString();
						Debug.Log("Created new GUID - " + this.guid);
						return;
					}
				}
			}
		}

		// Token: 0x0400072F RID: 1839
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}
