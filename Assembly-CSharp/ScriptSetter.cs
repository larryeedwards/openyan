using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
public class ScriptSetter : MonoBehaviour
{
	// Token: 0x06002365 RID: 9061 RVA: 0x001BFB38 File Offset: 0x001BDF38
	private void Start()
	{
		Component[] components = base.GetComponents(typeof(Component));
		foreach (Component component in components)
		{
			Debug.Log(string.Concat(new object[]
			{
				"name ",
				component.name,
				" type ",
				component.GetType(),
				" basetype ",
				component.GetType().BaseType
			}));
			foreach (FieldInfo fieldInfo in component.GetType().GetFields())
			{
				object obj = component;
				Debug.Log(fieldInfo.Name + " value is: " + fieldInfo.GetValue(obj));
			}
		}
	}

	// Token: 0x04003D2E RID: 15662
	public StudentScript OldStudent;

	// Token: 0x04003D2F RID: 15663
	public StudentScript NewStudent;
}
