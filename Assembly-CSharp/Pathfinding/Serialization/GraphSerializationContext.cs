using System;
using System.IO;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000074 RID: 116
	public class GraphSerializationContext
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0001C4B2 File Offset: 0x0001A8B2
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, uint graphIndex, GraphMeta meta)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
			this.meta = meta;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001C4D7 File Offset: 0x0001A8D7
		public GraphSerializationContext(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001C4E6 File Offset: 0x0001A8E6
		public void SerializeNodeReference(GraphNode node)
		{
			this.writer.Write((node != null) ? node.NodeIndex : -1);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001C508 File Offset: 0x0001A908
		public GraphNode DeserializeNodeReference()
		{
			int num = this.reader.ReadInt32();
			if (this.id2NodeMapping == null)
			{
				throw new Exception("Calling DeserializeNodeReference when not deserializing node references");
			}
			if (num == -1)
			{
				return null;
			}
			GraphNode graphNode = this.id2NodeMapping[num];
			if (graphNode == null)
			{
				throw new Exception("Invalid id (" + num + ")");
			}
			return graphNode;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001C56B File Offset: 0x0001A96B
		public void SerializeVector3(Vector3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001C5A3 File Offset: 0x0001A9A3
		public Vector3 DeserializeVector3()
		{
			return new Vector3(this.reader.ReadSingle(), this.reader.ReadSingle(), this.reader.ReadSingle());
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001C5CB File Offset: 0x0001A9CB
		public void SerializeInt3(Int3 v)
		{
			this.writer.Write(v.x);
			this.writer.Write(v.y);
			this.writer.Write(v.z);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001C603 File Offset: 0x0001AA03
		public Int3 DeserializeInt3()
		{
			return new Int3(this.reader.ReadInt32(), this.reader.ReadInt32(), this.reader.ReadInt32());
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001C62B File Offset: 0x0001AA2B
		public int DeserializeInt(int defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadInt32();
			}
			return defaultValue;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C662 File Offset: 0x0001AA62
		public float DeserializeFloat(float defaultValue)
		{
			if (this.reader.BaseStream.Position <= this.reader.BaseStream.Length - 4L)
			{
				return this.reader.ReadSingle();
			}
			return defaultValue;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C69C File Offset: 0x0001AA9C
		public UnityEngine.Object DeserializeUnityObject()
		{
			int num = this.reader.ReadInt32();
			if (num == 2147483647)
			{
				return null;
			}
			string text = this.reader.ReadString();
			string text2 = this.reader.ReadString();
			string text3 = this.reader.ReadString();
			Type type = Type.GetType(text2);
			if (type == null)
			{
				Debug.LogError("Could not find type '" + text2 + "'. Cannot deserialize Unity reference");
				return null;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				UnityReferenceHelper[] array = UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[];
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].GetGUID() == text3)
					{
						if (type == typeof(GameObject))
						{
							return array[i].gameObject;
						}
						return array[i].GetComponent(type);
					}
					else
					{
						i++;
					}
				}
			}
			UnityEngine.Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x04000328 RID: 808
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x04000329 RID: 809
		public readonly BinaryReader reader;

		// Token: 0x0400032A RID: 810
		public readonly BinaryWriter writer;

		// Token: 0x0400032B RID: 811
		public readonly uint graphIndex;

		// Token: 0x0400032C RID: 812
		public readonly GraphMeta meta;
	}
}
