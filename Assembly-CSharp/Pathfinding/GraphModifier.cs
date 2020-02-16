using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200003E RID: 62
	[ExecuteInEditMode]
	public abstract class GraphModifier : VersionedMonoBehaviour
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000DD84 File Offset: 0x0000C184
		protected static List<T> GetModifiersOfType<T>() where T : GraphModifier
		{
			GraphModifier graphModifier = GraphModifier.root;
			List<T> list = new List<T>();
			while (graphModifier != null)
			{
				T t = graphModifier as T;
				if (t != null)
				{
					list.Add(t);
				}
				graphModifier = graphModifier.next;
			}
			return list;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DDDC File Offset: 0x0000C1DC
		public static void FindAllModifiers()
		{
			GraphModifier[] array = UnityEngine.Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled)
				{
					array[i].OnEnable();
				}
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000DE28 File Offset: 0x0000C228
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			GraphModifier graphModifier = GraphModifier.root;
			switch (type)
			{
			case GraphModifier.EventType.PostScan:
				while (graphModifier != null)
				{
					graphModifier.OnPostScan();
					graphModifier = graphModifier.next;
				}
				break;
			case GraphModifier.EventType.PreScan:
				while (graphModifier != null)
				{
					graphModifier.OnPreScan();
					graphModifier = graphModifier.next;
				}
				break;
			default:
				if (type != GraphModifier.EventType.PostUpdate)
				{
					if (type == GraphModifier.EventType.PostCacheLoad)
					{
						while (graphModifier != null)
						{
							graphModifier.OnPostCacheLoad();
							graphModifier = graphModifier.next;
						}
					}
				}
				else
				{
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPostUpdate();
						graphModifier = graphModifier.next;
					}
				}
				break;
			case GraphModifier.EventType.LatePostScan:
				while (graphModifier != null)
				{
					graphModifier.OnLatePostScan();
					graphModifier = graphModifier.next;
				}
				break;
			case GraphModifier.EventType.PreUpdate:
				while (graphModifier != null)
				{
					graphModifier.OnGraphsPreUpdate();
					graphModifier = graphModifier.next;
				}
				break;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000DF59 File Offset: 0x0000C359
		protected virtual void OnEnable()
		{
			this.RemoveFromLinkedList();
			this.AddToLinkedList();
			this.ConfigureUniqueID();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000DF6D File Offset: 0x0000C36D
		protected virtual void OnDisable()
		{
			this.RemoveFromLinkedList();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DF75 File Offset: 0x0000C375
		protected override void Awake()
		{
			base.Awake();
			this.ConfigureUniqueID();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DF84 File Offset: 0x0000C384
		private void ConfigureUniqueID()
		{
			GraphModifier x;
			if (GraphModifier.usedIDs.TryGetValue(this.uniqueID, out x) && x != this)
			{
				this.Reset();
			}
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DFCB File Offset: 0x0000C3CB
		private void AddToLinkedList()
		{
			if (GraphModifier.root == null)
			{
				GraphModifier.root = this;
			}
			else
			{
				this.next = GraphModifier.root;
				GraphModifier.root.prev = this;
				GraphModifier.root = this;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E004 File Offset: 0x0000C404
		private void RemoveFromLinkedList()
		{
			if (GraphModifier.root == this)
			{
				GraphModifier.root = this.next;
				if (GraphModifier.root != null)
				{
					GraphModifier.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E09E File Offset: 0x0000C49E
		protected virtual void OnDestroy()
		{
			GraphModifier.usedIDs.Remove(this.uniqueID);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E0B1 File Offset: 0x0000C4B1
		public virtual void OnPostScan()
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E0B3 File Offset: 0x0000C4B3
		public virtual void OnPreScan()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E0B5 File Offset: 0x0000C4B5
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E0B7 File Offset: 0x0000C4B7
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E0B9 File Offset: 0x0000C4B9
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E0BB File Offset: 0x0000C4BB
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E0C0 File Offset: 0x0000C4C0
		private void Reset()
		{
			ulong num = (ulong)((long)UnityEngine.Random.Range(0, int.MaxValue));
			ulong num2 = (ulong)((ulong)((long)UnityEngine.Random.Range(0, int.MaxValue)) << 32);
			this.uniqueID = (num | num2);
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x040001CB RID: 459
		private static GraphModifier root;

		// Token: 0x040001CC RID: 460
		private GraphModifier prev;

		// Token: 0x040001CD RID: 461
		private GraphModifier next;

		// Token: 0x040001CE RID: 462
		[SerializeField]
		[HideInInspector]
		protected ulong uniqueID;

		// Token: 0x040001CF RID: 463
		protected static Dictionary<ulong, GraphModifier> usedIDs = new Dictionary<ulong, GraphModifier>();

		// Token: 0x0200003F RID: 63
		public enum EventType
		{
			// Token: 0x040001D1 RID: 465
			PostScan = 1,
			// Token: 0x040001D2 RID: 466
			PreScan,
			// Token: 0x040001D3 RID: 467
			LatePostScan = 4,
			// Token: 0x040001D4 RID: 468
			PreUpdate = 8,
			// Token: 0x040001D5 RID: 469
			PostUpdate = 16,
			// Token: 0x040001D6 RID: 470
			PostCacheLoad = 32
		}
	}
}
