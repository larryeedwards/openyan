using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E7 RID: 231
	[AddComponentMenu("Pathfinding/Modifiers/Funnel")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_funnel_modifier.php")]
	[Serializable]
	public class FunnelModifier : MonoModifier
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00045029 File Offset: 0x00043429
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00045030 File Offset: 0x00043430
		public override void Apply(Path p)
		{
			if (p.path == null || p.path.Count == 0 || p.vectorPath == null || p.vectorPath.Count == 0)
			{
				return;
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			List<Funnel.PathPart> list2 = Funnel.SplitIntoParts(p);
			if (list2.Count == 0)
			{
				return;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				Funnel.PathPart part = list2[i];
				if (!part.isLink)
				{
					Funnel.FunnelPortals funnel = Funnel.ConstructFunnelPortals(p.path, part);
					List<Vector3> collection = Funnel.Calculate(funnel, this.unwrap, this.splitAtEveryPortal);
					list.AddRange(collection);
					ListPool<Vector3>.Release(ref funnel.left);
					ListPool<Vector3>.Release(ref funnel.right);
					ListPool<Vector3>.Release(ref collection);
				}
				else
				{
					if (i == 0 || list2[i - 1].isLink)
					{
						list.Add(part.startPoint);
					}
					if (i == list2.Count - 1 || list2[i + 1].isLink)
					{
						list.Add(part.endPoint);
					}
				}
			}
			ListPool<Funnel.PathPart>.Release(ref list2);
			ListPool<Vector3>.Release(ref p.vectorPath);
			p.vectorPath = list;
		}

		// Token: 0x04000604 RID: 1540
		public bool unwrap = true;

		// Token: 0x04000605 RID: 1541
		public bool splitAtEveryPortal;
	}
}
