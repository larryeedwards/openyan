using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

// Token: 0x0200020B RID: 523
[Serializable]
public class PropertyReference
{
	// Token: 0x06000FFE RID: 4094 RVA: 0x000829FF File Offset: 0x00080DFF
	public PropertyReference()
	{
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00082A07 File Offset: 0x00080E07
	public PropertyReference(Component target, string fieldName)
	{
		this.mTarget = target;
		this.mName = fieldName;
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06001000 RID: 4096 RVA: 0x00082A1D File Offset: 0x00080E1D
	// (set) Token: 0x06001001 RID: 4097 RVA: 0x00082A25 File Offset: 0x00080E25
	public Component target
	{
		get
		{
			return this.mTarget;
		}
		set
		{
			this.mTarget = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06001002 RID: 4098 RVA: 0x00082A3C File Offset: 0x00080E3C
	// (set) Token: 0x06001003 RID: 4099 RVA: 0x00082A44 File Offset: 0x00080E44
	public string name
	{
		get
		{
			return this.mName;
		}
		set
		{
			this.mName = value;
			this.mProperty = null;
			this.mField = null;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06001004 RID: 4100 RVA: 0x00082A5B File Offset: 0x00080E5B
	public bool isValid
	{
		get
		{
			return this.mTarget != null && !string.IsNullOrEmpty(this.mName);
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06001005 RID: 4101 RVA: 0x00082A80 File Offset: 0x00080E80
	public bool isEnabled
	{
		get
		{
			if (this.mTarget == null)
			{
				return false;
			}
			MonoBehaviour monoBehaviour = this.mTarget as MonoBehaviour;
			return monoBehaviour == null || monoBehaviour.enabled;
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x00082AC4 File Offset: 0x00080EC4
	public Type GetPropertyType()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			return this.mProperty.PropertyType;
		}
		if (this.mField != null)
		{
			return this.mField.FieldType;
		}
		return typeof(void);
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00082B34 File Offset: 0x00080F34
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return !this.isValid;
		}
		if (obj is PropertyReference)
		{
			PropertyReference propertyReference = obj as PropertyReference;
			return this.mTarget == propertyReference.mTarget && string.Equals(this.mName, propertyReference.mName);
		}
		return false;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x00082B8F File Offset: 0x00080F8F
	public override int GetHashCode()
	{
		return PropertyReference.s_Hash;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00082B96 File Offset: 0x00080F96
	public void Set(Component target, string methodName)
	{
		this.mTarget = target;
		this.mName = methodName;
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00082BA6 File Offset: 0x00080FA6
	public void Clear()
	{
		this.mTarget = null;
		this.mName = null;
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00082BB6 File Offset: 0x00080FB6
	public void Reset()
	{
		this.mField = null;
		this.mProperty = null;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00082BC6 File Offset: 0x00080FC6
	public override string ToString()
	{
		return PropertyReference.ToString(this.mTarget, this.name);
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00082BDC File Offset: 0x00080FDC
	public static string ToString(Component comp, string property)
	{
		if (!(comp != null))
		{
			return null;
		}
		string text = comp.GetType().ToString();
		int num = text.LastIndexOf('.');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		if (!string.IsNullOrEmpty(property))
		{
			return text + "." + property;
		}
		return text + ".[property]";
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x00082C40 File Offset: 0x00081040
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object Get()
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty != null)
		{
			if (this.mProperty.CanRead)
			{
				return this.mProperty.GetValue(this.mTarget, null);
			}
		}
		else if (this.mField != null)
		{
			return this.mField.GetValue(this.mTarget);
		}
		return null;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00082CC8 File Offset: 0x000810C8
	[DebuggerHidden]
	[DebuggerStepThrough]
	public bool Set(object value)
	{
		if (this.mProperty == null && this.mField == null && this.isValid)
		{
			this.Cache();
		}
		if (this.mProperty == null && this.mField == null)
		{
			return false;
		}
		if (value == null)
		{
			try
			{
				if (this.mProperty == null)
				{
					this.mField.SetValue(this.mTarget, null);
					return true;
				}
				if (this.mProperty.CanWrite)
				{
					this.mProperty.SetValue(this.mTarget, null, null);
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
		if (!this.Convert(ref value))
		{
			if (Application.isPlaying)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					"Unable to convert ",
					value.GetType(),
					" to ",
					this.GetPropertyType()
				}));
			}
		}
		else
		{
			if (this.mField != null)
			{
				this.mField.SetValue(this.mTarget, value);
				return true;
			}
			if (this.mProperty.CanWrite)
			{
				this.mProperty.SetValue(this.mTarget, value, null);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00082E20 File Offset: 0x00081220
	[DebuggerHidden]
	[DebuggerStepThrough]
	private bool Cache()
	{
		if (this.mTarget != null && !string.IsNullOrEmpty(this.mName))
		{
			Type type = this.mTarget.GetType();
			this.mField = type.GetField(this.mName);
			this.mProperty = type.GetProperty(this.mName);
		}
		else
		{
			this.mField = null;
			this.mProperty = null;
		}
		return this.mField != null || this.mProperty != null;
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x00082EAC File Offset: 0x000812AC
	private bool Convert(ref object value)
	{
		if (this.mTarget == null)
		{
			return false;
		}
		Type propertyType = this.GetPropertyType();
		Type from;
		if (value == null)
		{
			if (!propertyType.IsClass)
			{
				return false;
			}
			from = propertyType;
		}
		else
		{
			from = value.GetType();
		}
		return PropertyReference.Convert(ref value, from, propertyType);
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00082F00 File Offset: 0x00081300
	public static bool Convert(Type from, Type to)
	{
		object obj = null;
		return PropertyReference.Convert(ref obj, from, to);
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00082F18 File Offset: 0x00081318
	public static bool Convert(object value, Type to)
	{
		if (value == null)
		{
			value = null;
			return PropertyReference.Convert(ref value, to, to);
		}
		return PropertyReference.Convert(ref value, value.GetType(), to);
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00082F3C File Offset: 0x0008133C
	public static bool Convert(ref object value, Type from, Type to)
	{
		if (to.IsAssignableFrom(from))
		{
			return true;
		}
		if (to == typeof(string))
		{
			value = ((value == null) ? "null" : value.ToString());
			return true;
		}
		if (value == null)
		{
			return false;
		}
		if (to == typeof(int))
		{
			if (from == typeof(string))
			{
				int num;
				if (int.TryParse((string)value, out num))
				{
					value = num;
					return true;
				}
			}
			else
			{
				if (from == typeof(float))
				{
					value = Mathf.RoundToInt((float)value);
					return true;
				}
				if (from == typeof(double))
				{
					value = (int)Math.Round((double)value);
				}
			}
		}
		else if (to == typeof(float))
		{
			if (from == typeof(string))
			{
				float num2;
				if (float.TryParse((string)value, out num2))
				{
					value = num2;
					return true;
				}
			}
			else if (from == typeof(double))
			{
				value = (float)((double)value);
			}
			else if (from == typeof(int))
			{
				value = (float)((int)value);
			}
		}
		else if (to == typeof(double))
		{
			if (from == typeof(string))
			{
				double num3;
				if (double.TryParse((string)value, out num3))
				{
					value = num3;
					return true;
				}
			}
			else if (from == typeof(float))
			{
				value = (double)((float)value);
			}
			else if (from == typeof(int))
			{
				value = (double)((int)value);
			}
		}
		return false;
	}

	// Token: 0x04000E10 RID: 3600
	[SerializeField]
	private Component mTarget;

	// Token: 0x04000E11 RID: 3601
	[SerializeField]
	private string mName;

	// Token: 0x04000E12 RID: 3602
	private FieldInfo mField;

	// Token: 0x04000E13 RID: 3603
	private PropertyInfo mProperty;

	// Token: 0x04000E14 RID: 3604
	private static int s_Hash = "PropertyBinding".GetHashCode();
}
