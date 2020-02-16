using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x0200007A RID: 122
	public class TinyJsonSerializer
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x0001DA8C File Offset: 0x0001BE8C
		private TinyJsonSerializer()
		{
			this.serializers[typeof(float)] = delegate(object v)
			{
				this.output.Append(((float)v).ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(bool)] = delegate(object v)
			{
				this.output.Append((!(bool)v) ? "false" : "true");
			};
			Dictionary<Type, Action<object>> dictionary = this.serializers;
			Type typeFromHandle = typeof(Version);
			Action<object> action = delegate(object v)
			{
				this.output.Append(v.ToString());
			};
			this.serializers[typeof(int)] = action;
			action = action;
			this.serializers[typeof(uint)] = action;
			dictionary[typeFromHandle] = action;
			this.serializers[typeof(string)] = delegate(object v)
			{
				this.output.AppendFormat("\"{0}\"", v.ToString().Replace("\"", "\\\""));
			};
			this.serializers[typeof(Vector2)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"x\": {0}, \"y\": {1} }}", ((Vector2)v).x.ToString("R", TinyJsonSerializer.invariantCulture), ((Vector2)v).y.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Vector3)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"x\": {0}, \"y\": {1}, \"z\": {2} }}", ((Vector3)v).x.ToString("R", TinyJsonSerializer.invariantCulture), ((Vector3)v).y.ToString("R", TinyJsonSerializer.invariantCulture), ((Vector3)v).z.ToString("R", TinyJsonSerializer.invariantCulture));
			};
			this.serializers[typeof(Pathfinding.Util.Guid)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": \"{0}\" }}", v.ToString());
			};
			this.serializers[typeof(LayerMask)] = delegate(object v)
			{
				this.output.AppendFormat("{{ \"value\": {0} }}", ((LayerMask)v).ToString());
			};
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001DBF0 File Offset: 0x0001BFF0
		public static void Serialize(object obj, StringBuilder output)
		{
			new TinyJsonSerializer
			{
				output = output
			}.Serialize(obj);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001DC14 File Offset: 0x0001C014
		private void Serialize(object obj)
		{
			if (obj == null)
			{
				this.output.Append("null");
				return;
			}
			Type type = obj.GetType();
			Type typeInfo = WindowsStoreCompatibility.GetTypeInfo(type);
			if (this.serializers.ContainsKey(type))
			{
				this.serializers[type](obj);
			}
			else if (typeInfo.IsEnum)
			{
				this.output.Append('"' + obj.ToString() + '"');
			}
			else if (obj is IList)
			{
				this.output.Append("[");
				IList list = obj as IList;
				for (int i = 0; i < list.Count; i++)
				{
					if (i != 0)
					{
						this.output.Append(", ");
					}
					this.Serialize(list[i]);
				}
				this.output.Append("]");
			}
			else if (obj is UnityEngine.Object)
			{
				this.SerializeUnityObject(obj as UnityEngine.Object);
			}
			else
			{
				bool flag = typeInfo.GetCustomAttributes(typeof(JsonOptInAttribute), true).Length > 0;
				this.output.Append("{");
				bool flag2 = false;
				do
				{
					FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					foreach (FieldInfo fieldInfo in fields)
					{
						if (fieldInfo.DeclaringType == type)
						{
							if ((!flag && fieldInfo.IsPublic) || fieldInfo.GetCustomAttributes(typeof(JsonMemberAttribute), true).Length > 0)
							{
								if (flag2)
								{
									this.output.Append(", ");
								}
								flag2 = true;
								this.output.AppendFormat("\"{0}\": ", fieldInfo.Name);
								this.Serialize(fieldInfo.GetValue(obj));
							}
						}
					}
					type = type.BaseType;
				}
				while (type != null);
				this.output.Append("}");
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001DE36 File Offset: 0x0001C236
		private void QuotedField(string name, string contents)
		{
			this.output.AppendFormat("\"{0}\": \"{1}\"", name, contents);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001DE4C File Offset: 0x0001C24C
		private void SerializeUnityObject(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				this.Serialize(null);
				return;
			}
			this.output.Append("{");
			string name = obj.name;
			this.QuotedField("Name", name);
			this.output.Append(", ");
			this.QuotedField("Type", obj.GetType().FullName);
			Component component = obj as Component;
			GameObject gameObject = obj as GameObject;
			if (component != null || gameObject != null)
			{
				if (component != null && gameObject == null)
				{
					gameObject = component.gameObject;
				}
				UnityReferenceHelper unityReferenceHelper = gameObject.GetComponent<UnityReferenceHelper>();
				if (unityReferenceHelper == null)
				{
					Debug.Log("Adding UnityReferenceHelper to Unity Reference '" + obj.name + "'");
					unityReferenceHelper = gameObject.AddComponent<UnityReferenceHelper>();
				}
				unityReferenceHelper.Reset();
				this.output.Append(", ");
				this.QuotedField("GUID", unityReferenceHelper.GetGUID().ToString());
			}
			this.output.Append("}");
		}

		// Token: 0x04000344 RID: 836
		private StringBuilder output = new StringBuilder();

		// Token: 0x04000345 RID: 837
		private Dictionary<Type, Action<object>> serializers = new Dictionary<Type, Action<object>>();

		// Token: 0x04000346 RID: 838
		private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
	}
}
