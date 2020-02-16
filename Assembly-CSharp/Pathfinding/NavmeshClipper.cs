using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F5 RID: 245
	public abstract class NavmeshClipper : VersionedMonoBehaviour
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00046EBF File Offset: 0x000452BF
		public NavmeshClipper()
		{
			this.node = new LinkedListNode<NavmeshClipper>(this);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00046ED4 File Offset: 0x000452D4
		public static void AddEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onEnable(linkedListNode.Value);
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00046F34 File Offset: 0x00045334
		public static void RemoveEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onDisable(linkedListNode.Value);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00046F94 File Offset: 0x00045394
		public static bool AnyEnableListeners
		{
			get
			{
				return NavmeshClipper.OnEnableCallback != null;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00046FA1 File Offset: 0x000453A1
		protected virtual void OnEnable()
		{
			NavmeshClipper.all.AddFirst(this.node);
			if (NavmeshClipper.OnEnableCallback != null)
			{
				NavmeshClipper.OnEnableCallback(this);
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00046FC8 File Offset: 0x000453C8
		protected virtual void OnDisable()
		{
			if (NavmeshClipper.OnDisableCallback != null)
			{
				NavmeshClipper.OnDisableCallback(this);
			}
			NavmeshClipper.all.Remove(this.node);
		}

		// Token: 0x06000914 RID: 2324
		internal abstract void NotifyUpdated();

		// Token: 0x06000915 RID: 2325
		internal abstract Rect GetBounds(GraphTransform transform);

		// Token: 0x06000916 RID: 2326
		public abstract bool RequiresUpdate();

		// Token: 0x06000917 RID: 2327
		public abstract void ForceUpdate();

		// Token: 0x04000654 RID: 1620
		private static Action<NavmeshClipper> OnEnableCallback;

		// Token: 0x04000655 RID: 1621
		private static Action<NavmeshClipper> OnDisableCallback;

		// Token: 0x04000656 RID: 1622
		private static readonly LinkedList<NavmeshClipper> all = new LinkedList<NavmeshClipper>();

		// Token: 0x04000657 RID: 1623
		private readonly LinkedListNode<NavmeshClipper> node;
	}
}
